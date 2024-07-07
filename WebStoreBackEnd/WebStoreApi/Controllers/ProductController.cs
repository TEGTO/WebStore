using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using WebStoreApi.Domain.Dtos;
using WebStoreApi.Domain.Entities;
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
        [OutputCache(Duration = 60)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts(CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = await productsService.GetAllProductsAsync(cancellationToken);
            return Ok(products.Select(mapper.Map<ProductResponse>));
        }
    }
}
