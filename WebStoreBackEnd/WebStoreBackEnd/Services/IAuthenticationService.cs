using WebStoreBackEnd.Models;

namespace WebStoreBackEnd.Services
{
    public interface IAuthenticationService
    {
        public Task<bool> CheckAuthenticationModelAsync(AuthenticationModel model, CancellationToken cancellationToken);
    }
}
