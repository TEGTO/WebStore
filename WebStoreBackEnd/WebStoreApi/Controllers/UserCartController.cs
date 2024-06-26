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
        [Route("product/{userEmail}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByUserId([FromRoute] string userEmail, CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = await userCartService.GetProductsInUserCartAsync(userEmail, cancellationToken);
            return Ok(products.Select(mapper.Map<ProductDto>));
        }
        [HttpGet]
        [Route("amount/{userEmail}")]
        public async Task<ActionResult<int>> GetProductAmountByUserId([FromRoute] string userEmail, CancellationToken cancellationToken)
        {
            int amount = await userCartService.GetProductsInUserCartAmountAsync(userEmail, cancellationToken);
            return Ok(amount);
        }
        [HttpPost]
        [Route("product/{userEmail}")]
        public async Task<IActionResult> AddProductToUserCart([FromRoute] string userEmail, ProductDto productDto, CancellationToken cancellationToken)
        {
            Product product = mapper.Map<Product>(productDto);
            await userCartService.AddProductToUserCartAsync(userEmail, product, cancellationToken);
            return Ok();
        }
        [HttpDelete]
        [Route("product/{userEmail}")]
        public async Task<IActionResult> RemoveProductFromUserCart([FromRoute] string userEmail, ProductDto productDto, CancellationToken cancellationToken)
        {
            Product product = mapper.Map<Product>(productDto);
            await userCartService.RemoveProductFromUserCartAsync(userEmail, product, cancellationToken);
            return Ok();
        }
    }
}
