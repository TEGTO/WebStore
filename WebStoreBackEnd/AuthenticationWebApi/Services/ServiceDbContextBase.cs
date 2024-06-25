using Microsoft.EntityFrameworkCore;
using AuthenticationWebApi.Data;

namespace AuthenticationWebApi.Services
{
    public class ServiceDbContextBase
    {
        private readonly IDbContextFactory<AuthIdentityDbContext> dbContextFactory;

        public ServiceDbContextBase(IDbContextFactory<AuthIdentityDbContext> contextFactory)
        {
            dbContextFactory = contextFactory;
        }
        protected async Task<AuthIdentityDbContext> CreateDbContextAsync(CancellationToken cancelentionToken)
        {
            return await dbContextFactory.CreateDbContextAsync(cancelentionToken);
        }
    }
}
