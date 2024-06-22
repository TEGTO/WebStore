using WebStoreBackEnd.Models;

namespace WebStoreBackEnd.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<bool> CheckAuthenticationModelAsync(AuthenticationModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
