using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStoreApi.Models;
using WebStoreApi.Models.Dto;
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
        [Route("product/{userId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByUserId([FromRoute] string userId, CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = await userCartService.GetProductsByUserIdAsync(userId, cancellationToken);
            return Ok(products.Select(mapper.Map<ProductDto>));
        }
        [HttpGet]
        [Route("amount/{userId}")]
        public async Task<ActionResult<int>> GetProductAmountByUserId([FromRoute] string userId, CancellationToken cancellationToken)
        {
            int amount = await userCartService.GetProductsAmountByUserIdAsync(userId, cancellationToken);
            return Ok(amount);
        }
        [HttpPost]
        [Route("product/{userId}")]
        public async Task<IActionResult> AddProductToUserCart([FromRoute] string userId, ProductDto productDto, CancellationToken cancellationToken)
        {
            Product product = mapper.Map<Product>(productDto);
            await userCartService.AddProductToUserCartAsync(userId, product, cancellationToken);
            return Ok();
        }
        [HttpDelete]
        [Route("product/{userId}")]
        public async Task<IActionResult> RemoveProductFromUserCart([FromRoute] string userId, ProductDto productDto, CancellationToken cancellationToken)
        {
            Product product = mapper.Map<Product>(productDto);
            await userCartService.RemoveProductFromUserCartAsync(userId, product, cancellationToken);
            return Ok();
        }
    }
}
