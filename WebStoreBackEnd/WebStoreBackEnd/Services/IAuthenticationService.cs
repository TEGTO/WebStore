using WebStoreBackEnd.Models;

namespace WebStoreBackEnd.Services
{
    public interface IAuthenticationService
    {
        public Task<bool> CheckAuthenticationModelAsync(AuthenticationModel model, CancellationToken cancellationToken);
        public Task RegisterNewModelAsync(AuthenticationModel model, CancellationToken cancellationToken);
    }
}
