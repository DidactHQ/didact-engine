using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DidactEngine.Services.Extensions
{
    public static class OpenApiExtensions
    {
        public static void ConfigureOpenApi(this IHostApplicationBuilder builder)
        {
            string swaggerVersion = "v1";
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(swaggerVersion, new OpenApiInfo
                {
                    Version = swaggerVersion,
                    Title = "Didact REST API",
                    Description = "The central REST API of the Didact Engine."
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            builder.Services.AddEndpointsApiExplorer();
        }
    }
}
