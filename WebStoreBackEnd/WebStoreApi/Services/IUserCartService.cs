using WebStoreApi.Dtos.ServiceDtos;
using WebStoreApi.Entities;

namespace WebStoreApi.Services
{
    public interface IUserCartService
    {
        public Task<IEnumerable<Product>> GetProductsInUserCartAsync(string user, CancellationToken cancellationToken);
        public Task<int> GetProductsInUserCartAmountAsync(string user, CancellationToken cancellationToken);
        public Task AddProductToUserCartAsync(UserCartChangeServiceRequest changeRequest, CancellationToken cancellationToken);
        public Task RemoveProductFromUserCartAsync(UserCartChangeServiceRequest changeRequest, CancellationToken cancellationToken);
    }
}