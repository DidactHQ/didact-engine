using DidactEngine.Services.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DidactEngine.Services.Extensions
{
    public static class DbConfigurationExtensions
    {
        public static void ConfigureDatabase(this IHostApplicationBuilder builder)
        {
            string connStringFactory(string name) => new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString(name))
            {
                ApplicationName = "Didact",
                PersistSecurityInfo = true,
                MultipleActiveResultSets = true,
                WorkstationID = Environment.MachineName,
                TrustServerCertificate = true
            }.ConnectionString;

            builder.Services.AddDbContext<DidactDbContext>(
                (sp, opt) =>
                {
                    opt.UseMemoryCache(sp.GetRequiredService<IMemoryCache>());
                    opt.UseSqlServer(connStringFactory("Didact"), opt => opt.CommandTimeout(110));
                    if (builder.Configuration.GetValue<bool?>("EnableSensitiveDataLogging").GetValueOrDefault())
                    {
                        opt.EnableDetailedErrors();
                        opt.EnableSensitiveDataLogging();
                    }
                });
        }
    }
}
