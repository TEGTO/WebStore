using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Configuration;
using RabbitMQ.Enums;
using RabbitMQ.RabbitMQ;
using WebStoreApi.Domain.Dtos;
using WebStoreApi.Domain.Entities;
using WebStoreApi.Domain.Models;
using WebStoreApi.Services;

namespace WebStoreApi.Controllers
{
    [Authorize]
    [Route("store/usercart")]
    [ApiController]
    public class UserCartController : ControllerBase
    {
        private readonly IUserCartService userCartService;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IRabitMQProducer rabitMQProducer;

        public UserCartController(IUserCartService userCartService, IMapper mapper, IConfiguration configuration, IRabitMQProducer rabitMQProducer)
        {
            this.userCartService = userCartService;
            this.mapper = mapper;
            this.configuration = configuration;
            this.rabitMQProducer = rabitMQProducer;
        }

        [HttpGet]
        [Route("product/{userEmail}")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsInUserCart([FromRoute] string userEmail, CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = await userCartService.GetProductsInUserCartAsync(userEmail, cancellationToken);
            return Ok(products.Select(mapper.Map<ProductResponse>));
        }
        [HttpGet]
        [Route("amount/{userEmail}")]
        public async Task<ActionResult<int>> GetProductsInUserCartAmount([FromRoute] string userEmail, CancellationToken cancellationToken)
        {
            int amount = await userCartService.GetProductsInUserCartAmountAsync(userEmail, cancellationToken);
            return Ok(amount);
        }
        [HttpPost]
        public async Task<IActionResult> AddProductToUserCart([FromBody] UserCartChangeRequest cartChangeDto, CancellationToken cancellationToken)
        {
            UserCartChange cartChange = mapper.Map<UserCartChange>(cartChangeDto);
            await userCartService.AddProductToUserCartAsync(cartChange, cancellationToken);
            var sendSettings = GetRabbitMQSendSettings("webstore-addproduct");
            rabitMQProducer.SendMessage(cartChange, sendSettings);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> RemoveProductFromUserCart([FromBody] UserCartChangeRequest cartChangeDto, CancellationToken cancellationToken)
        {
            UserCartChange cartChange = mapper.Map<UserCartChange>(cartChangeDto);
            await userCartService.RemoveProductFromUserCartAsync(cartChange, cancellationToken);
            var sendSettings = GetRabbitMQSendSettings("webstore-removeproduct");
            return Ok();
        }
        private RabbitMQSendSettings GetRabbitMQSendSettings(string queueName)
        {
            var rabbitMQDefaultSendSettings = new RabbitMQSendSettings
            {
                ExchangeName = configuration["RabbitMQ:WebStoreExchange"],
                RoutingKey = configuration["RabbitMQ:RoutingKey"],
                QueueName = queueName,
                ExchangeType = ExchangeTypeEnum.Direct,
            };
            return rabbitMQDefaultSendSettings;
        }
    }
}