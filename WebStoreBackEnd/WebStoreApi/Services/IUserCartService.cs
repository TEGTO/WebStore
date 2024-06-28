using WebStoreApi.Models;

namespace WebStoreApi.Services
{
    public interface IUserCartService
    {
        public Task<IEnumerable<Product>> GetProductsInUserCartAsync(string user, CancellationToken cancellationToken);
        public Task<int> GetProductsInUserCartAmountAsync(string user, CancellationToken cancellationToken);
        public Task AddProductToUserCartAsync(UserCartChange userCartChange, CancellationToken cancellationToken);
        public Task RemoveProductFromUserCartAsync(UserCartChange userCartChange, CancellationToken cancellationToken);
    }
}