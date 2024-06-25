using WebStoreApi.Models;

namespace WebStoreApi.Services
{
    public interface IUserCartService
    {
        public Task<IEnumerable<Product>> GetProductsByUserIdAsync(string userId, CancellationToken cancellationToken);
        public Task<int> GetProductsAmountByUserIdAsync(string userId, CancellationToken cancellationToken);
        public Task AddProductToUserCartAsync(string userId, Product product, CancellationToken cancellationToken);
        public Task RemoveProductFromUserCartAsync(string userId, Product product, CancellationToken cancellationToken);
    }
}