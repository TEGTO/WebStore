using WebStoreApi.Domain.Entities;

namespace WebStoreApi.Services
{
    public interface IProductsService
    {
        public Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken);
    }
}