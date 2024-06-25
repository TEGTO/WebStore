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

        public async Task<IEnumerable<Product>> GetProductsByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            List<Product> products = new List<Product>();
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                var userProducts = await dbContext.UserProducts.Where(x => x.UserId == userId)
                    .Select(x => x.Product).ToListAsync(cancellationToken);
                products.AddRange(userProducts);
            }
            return products;
        }
        public async Task<int> GetProductsAmountByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            int amount = 0;
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                var userProducts = await dbContext.UserProducts.Where(x => x.UserId == userId)
                    .Select(x => x.Product).ToListAsync(cancellationToken);
                amount = userProducts.Count();
            }
            return amount;
        }
        public async Task AddProductToUserCartAsync(string userId, Product product, CancellationToken cancellationToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                UserProduct userProduct = new UserProduct() { UserId = userId, ProductId = product.Id };
                dbContext.Add(userProduct);
                dbContext.SaveChanges();
            }
        }
        public async Task RemoveProductFromUserCartAsync(string userId, Product product, CancellationToken cancellationToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                var userProduct = await dbContext.UserProducts.FirstAsync(x => x.UserId == userId && x.ProductId == product.Id, cancellationToken);
                dbContext.Remove(userProduct);
                dbContext.SaveChanges();
            }
        }
    }
}
