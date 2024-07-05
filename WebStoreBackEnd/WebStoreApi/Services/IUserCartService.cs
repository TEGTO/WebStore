using WebStoreApi.Domain.Entities;
using WebStoreApi.Domain.Models;

namespace WebStoreApi.Services
{
    public interface IUserCartService
    {
        public Task<IEnumerable<Product>> GetProductsInUserCartAsync(string user, CancellationToken cancellationToken);
        public Task<int> GetProductsInUserCartAmountAsync(string user, CancellationToken cancellationToken);
        public Task AddProductToUserCartAsync(UserCartChange changeData, CancellationToken cancellationToken);
        public Task RemoveProductFromUserCartAsync(UserCartChange changeData, CancellationToken cancellationToken);
    }
}