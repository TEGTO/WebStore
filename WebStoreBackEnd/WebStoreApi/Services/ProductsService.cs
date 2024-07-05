using Microsoft.EntityFrameworkCore;
using Shared.Services;
using WebStoreApi.Data;
using WebStoreApi.Entities;

namespace WebStoreApi.Services
{
    public class ProductsService : ServiceDbBase<WebStoreDbContext>, IProductsService
    {
        public ProductsService(IDbContextFactory<WebStoreDbContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            List<Product> products = new List<Product>();
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                products.AddRange(dbContext.Products);
            }
            return products;
        }
    }
}
