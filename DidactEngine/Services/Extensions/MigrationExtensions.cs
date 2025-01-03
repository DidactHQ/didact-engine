using Microsoft.EntityFrameworkCore;

namespace DidactEngine.Services.Extensions
{
    public static class MigrationExtensions
    {
        public static async Task StartEngine<T>(this IHost app) where T : DbContext
        {
            var logger = app.Services.GetRequiredService<ILogger<T>>();

            try
            {
                using (var scope = app.Services.CreateScope())
                using (var dbContext = scope.ServiceProvider.GetRequiredService<T>())

                    try
                    {
                        logger.LogInformation("Attempting to migrate the database on engine startup...");
                        await dbContext.Database.MigrateAsync();
                        logger.LogInformation("Database migrated successfully.");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Unhandled exception while applying migrations for {T}", typeof(T));
                        throw;
                    }

                app.Run();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "An unhandled exception occurred during bootstrapping");
                await app.StopAsync();
            }
        }
    }
}
