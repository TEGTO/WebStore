using AutoMapper;
using MessageQueue.Configuration;
using MessageQueue.Enums;
using MessageQueue.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMessageQueueService messageQueu;

        public UserCartController(IUserCartService userCartService, IMapper mapper, IConfiguration configuration, IMessageQueueService messageQueu)
        {
            this.userCartService = userCartService;
            this.mapper = mapper;
            this.configuration = configuration;
            this.messageQueu = messageQueu;
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
            var sendSettings = GetRabbitMQSendSettings("webstore.usercart.addproduct");
            messageQueu.SendMessage(cartChange, sendSettings);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> RemoveProductFromUserCart([FromBody] UserCartChangeRequest cartChangeDto, CancellationToken cancellationToken)
        {
            UserCartChange cartChange = mapper.Map<UserCartChange>(cartChangeDto);
            await userCartService.RemoveProductFromUserCartAsync(cartChange, cancellationToken);
            var sendSettings = GetRabbitMQSendSettings("webstore.usercart.removeproduct");
            messageQueu.SendMessage(cartChange, sendSettings);
            return Ok();
        }
        private SendSettings GetRabbitMQSendSettings(string routingKey)
        {
            var exchangeSettings = new ExchangeSettings
            {
                Name = configuration["MessageQueue:WebStoreExchange"],
                Type = ExchangeType.Topic,
            };
            var rabbitMQDefaultSendSettings = new SendSettings
            {
                ExchangeSettings = exchangeSettings,
                RoutingKey = routingKey,
            };
            return rabbitMQDefaultSendSettings;
        }
    }
}