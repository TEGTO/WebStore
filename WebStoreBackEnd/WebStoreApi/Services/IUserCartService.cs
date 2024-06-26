using WebStoreApi.Models;

namespace WebStoreApi.Services
{
    public interface IUserCartService
    {
        public Task<IEnumerable<Product>> GetProductsInUserCartAsync(string user, CancellationToken cancellationToken);
        public Task<int> GetProductsInUserCartAmountAsync(string user, CancellationToken cancellationToken);
        public Task AddProductToUserCartAsync(string user, Product product, CancellationToken cancellationToken);
        public Task RemoveProductFromUserCartAsync(string user, Product product, CancellationToken cancellationToken);
    }
}