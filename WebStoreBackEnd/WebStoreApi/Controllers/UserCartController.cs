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
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsInUserCart([FromRoute] string userEmail, CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = await userCartService.GetProductsInUserCartAsync(userEmail, cancellationToken);
            return Ok(products.Select(mapper.Map<ProductDto>));
        }
        [HttpGet]
        [Route("amount/{userEmail}")]
        public async Task<ActionResult<int>> GetProductsInUserCartAmount([FromRoute] string userEmail, CancellationToken cancellationToken)
        {
            int amount = await userCartService.GetProductsInUserCartAmountAsync(userEmail, cancellationToken);
            return Ok(amount);
        }
        [HttpPost]
        public async Task<IActionResult> AddProductToUserCart([FromBody] UserCartChangeDto cartChangeDto, CancellationToken cancellationToken)
        {
            UserCartChange cartChange = mapper.Map<UserCartChange>(cartChangeDto);
            await userCartService.AddProductToUserCartAsync(cartChange, cancellationToken);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> RemoveProductFromUserCart([FromBody] UserCartChangeDto cartChangeDto, CancellationToken cancellationToken)
        {
            UserCartChange cartChange = mapper.Map<UserCartChange>(cartChangeDto);
            await userCartService.RemoveProductFromUserCartAsync(cartChange, cancellationToken);
            return Ok();
        }
    }
}
