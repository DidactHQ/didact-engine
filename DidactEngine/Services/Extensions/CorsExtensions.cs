namespace DidactEngine.Services.Extensions
{
    public static class CorsExtensions
    {
        public static void ConfigureCors(this IHostApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "DevelopmentCORS",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:8080");
                        policy.AllowAnyMethod();
                        policy.AllowAnyHeader();
                        policy.AllowCredentials();
                    });
            });
        }
    }
}
