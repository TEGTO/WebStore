using WebStoreApi.Models;

namespace WebStoreApi.Services
{
    public interface IProductsService
    {
        public Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken);
    }
}