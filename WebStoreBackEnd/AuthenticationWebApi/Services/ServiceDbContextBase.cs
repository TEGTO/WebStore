using Microsoft.EntityFrameworkCore;
using AuthenticationWebApi.Data;

namespace AuthenticationWebApi.Services
{
    public class ServiceDbContextBase
    {
        private readonly IDbContextFactory<WebStoreDbContext> dbContextFactory;

        public ServiceDbContextBase(IDbContextFactory<WebStoreDbContext> contextFactory)
        {
            dbContextFactory = contextFactory;
        }
        protected async Task<WebStoreDbContext> CreateDbContextAsync(CancellationToken cancelentionToken)
        {
            return await dbContextFactory.CreateDbContextAsync(cancelentionToken);
        }
    }
}
