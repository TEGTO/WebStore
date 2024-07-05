using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStoreApi.Dtos.ControllerDtos;
using WebStoreApi.Dtos.ServiceDtos;
using WebStoreApi.Entities;
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

        public UserCartController(IUserCartService userCartService, IMapper mapper)
        {
            this.userCartService = userCartService;
            this.mapper = mapper;
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
            UserCartChangeServiceRequest cartChange = mapper.Map<UserCartChangeServiceRequest>(cartChangeDto);
            await userCartService.AddProductToUserCartAsync(cartChange, cancellationToken);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> RemoveProductFromUserCart([FromBody] UserCartChangeRequest cartChangeDto, CancellationToken cancellationToken)
        {
            UserCartChangeServiceRequest cartChange = mapper.Map<UserCartChangeServiceRequest>(cartChangeDto);
            await userCartService.RemoveProductFromUserCartAsync(cartChange, cancellationToken);
            return Ok();
        }
    }
}