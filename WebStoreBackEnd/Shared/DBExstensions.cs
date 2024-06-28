using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Shared
{
    public static class DBExstensions
    {
        public static IApplicationBuilder ConfigureMigration<TContext>(this IApplicationBuilder builder) where TContext : DbContext
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<IApplicationBuilder>>();
                try
                {
                    if (configuration["EF_MIGRATION"] == "true")
                    {
                        logger.LogInformation("Applying database migrations...");
                        var context = services.GetRequiredService<TContext>();
                        context.Database.Migrate();
                        logger.LogInformation("Database migrations applied successfully.");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
            return builder;
        }
    }
}
