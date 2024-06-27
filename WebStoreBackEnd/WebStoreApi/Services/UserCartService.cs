using HorseBets.Api.Shared.Services;
using Microsoft.EntityFrameworkCore;
using WebStoreApi.Data;
using WebStoreApi.Models;

namespace WebStoreApi.Services
{
    public class UserCartService : ServiceDbBase<WebStoreDbContext>, IUserCartService
    {
        public UserCartService(IDbContextFactory<WebStoreDbContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsInUserCartAsync(string user, CancellationToken cancellationToken)
        {
            List<Product> products = new List<Product>();
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                var userProducts = await dbContext.UserProducts.Where(x => x.UserEmail == user)
                    .Select(x => x.Product).ToListAsync(cancellationToken);
                products.AddRange(userProducts);
            }
            return products;
        }
        public async Task<int> GetProductsInUserCartAmountAsync(string user, CancellationToken cancellationToken)
        {
            int amount = 0;
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                var userProducts = await dbContext.UserProducts.Where(x => x.UserEmail == user)
                    .Select(x => x.Product).ToListAsync(cancellationToken);
                amount = userProducts.Count();
            }
            return amount;
        }
        public async Task AddProductToUserCartAsync(string user, Product product, CancellationToken cancellationToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                UserProduct userProduct = new UserProduct() { UserEmail = user, ProductId = product.Id };
                dbContext.Add(userProduct);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task RemoveProductFromUserCartAsync(string user, int productId, CancellationToken cancellationToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                var userProduct = await dbContext.UserProducts.FirstAsync(x => x.UserEmail == user && x.ProductId == productId, cancellationToken);
                dbContext.Remove(userProduct);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
