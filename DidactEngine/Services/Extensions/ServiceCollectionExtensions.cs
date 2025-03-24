using DidactEngine.Services.BackgroundServices;

namespace DidactEngine.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddMemoryCache();

            // Register BackgroundServices
            builder.Services.AddHostedService<WorkerBackgroundService>();
            // Register Flow helper services from DidactCore.
            //builder.Services.AddSingleton<IFlowExecutor, FlowExecutor>();
            // Register repositories from DidactCore.
            //builder.Services.AddScoped<IFlowRepository>();
        }
    }
}
