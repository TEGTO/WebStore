using Microsoft.EntityFrameworkCore;
using Shared.Services;
using WebStoreApi.Data;
using WebStoreApi.Domain.Entities;
using WebStoreApi.Domain.Models;

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
        public async Task AddProductToUserCartAsync(UserCartChange changeData, CancellationToken cancellationToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                for (int i = 0; i < changeData.Amount; i++)
                {
                    UserProduct userProduct = new UserProduct() { UserEmail = changeData.UserEmail, ProductId = changeData.ProductId };
                    dbContext.Add(userProduct);
                }
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task RemoveProductFromUserCartAsync(UserCartChange changeData, CancellationToken cancellationToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                var userProducts = await dbContext.UserProducts
                    .Where(x => x.UserEmail == changeData.UserEmail && x.ProductId == changeData.ProductId)
                    .Take(changeData.Amount)
                    .ToListAsync(cancellationToken);
                foreach (var userProduct in userProducts)
                {
                    dbContext.UserProducts.Remove(userProduct);
                }
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}