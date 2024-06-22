using Microsoft.EntityFrameworkCore;
using WebStoreBackEnd.Data;
using WebStoreBackEnd.Models;

namespace WebStoreBackEnd.Services
{
    public class AuthenticationService : ServiceDbContextBase, IAuthenticationService
    {
        public AuthenticationService(IDbContextFactory<WebStoreDbContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<bool> CheckAuthenticationModelAsync(AuthenticationModel model, CancellationToken cancellationToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                return await dbContext.Users
                   .AnyAsync(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password), cancellationToken);
            }
        }
        public async Task RegisterNewModelAsync(AuthenticationModel model, CancellationToken cancellationToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                if (await dbContext.Users.AnyAsync(x => x.Email.Equals(model.Email), cancellationToken))
                {
                    throw new InvalidDataException("User with this email already exists!");
                }
                else
                {
                    await dbContext.AddAsync(new User { Email = model.Email, Password = model.Password });
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
