using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStoreApi.Dtos.ControllerDtos;
using WebStoreApi.Entities;
using WebStoreApi.Services;

namespace WebStoreApi.Controllers
{
    [Route("store/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService productsService;
        private readonly IMapper mapper;

        public ProductController(IProductsService productsService, IMapper mapper)
        {
            this.productsService = productsService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts(CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = await productsService.GetAllProductsAsync(cancellationToken);
            return Ok(products.Select(mapper.Map<ProductResponse>));
        }
    }
}
