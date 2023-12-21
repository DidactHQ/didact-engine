using DidactCore.Flows;
using DidactEngine.Hubs;
using DidactEngine.Services;
using DidactEngine.Services.BackgroundServices;
using DidactEngine.Services.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Runtime.Loader;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

#region Configure DbContext and Gateway.

var connStringFactory = (string name) => new SqlConnectionStringBuilder(
    builder.Configuration.GetConnectionString(name))
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

#endregion Configure DbContext and Gateway.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();

// Register Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

// Register BackgroundServices
//builder.Services.AddHostedService<DatabaseEngineBackgroundService>();
//builder.Services.AddHostedService<AssemblyReaderBackgroundService>();
builder.Services.AddHostedService<WorkerBackgroundService>();

builder.Services.AddSingleton<ExecutionManager>();
builder.Services.AddSignalR();

#region Add Flow Repository

//var flowRepositoryBackgroundServiceHashCode = typeof(FlowRepositoryBackgroundService).GetHashCode();
//var aqn = typeof(FlowRepositoryBackgroundService).FullName;
//Console.WriteLine($"See the hash code below:\n\n{flowRepositoryBackgroundServiceHashCode}");
//Console.WriteLine($"\n\nThe assembly qualified name is:\n\n{aqn}");

//var currentAssembly = Assembly.GetExecutingAssembly();
//var test = currentAssembly.GetTypes().FirstOrDefault(t => t.AssemblyQualifiedName == aqn);
//Console.WriteLine($"Test:\n\n{test.AssemblyQualifiedName}");

using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
    .SetMinimumLevel(LogLevel.Trace)
    .AddConsole());

ILogger _logger = loggerFactory.CreateLogger<Program>();

var flowLibraryAssembly = Assembly.LoadFile(@"C:\Users\falco\source\repos\didact-core\DidactCore\bin\Debug\netstandard2.1\DidactCore.dll");
var flowTypes2 = flowLibraryAssembly.GetTypes().Where(t => t.GetInterface("IFlow") is not null);
foreach (var type in flowTypes2)
{
    if (true)
    {
        var interfaces = type.GetInterfaces().Select(i => i.Name).ToList();
        var interfacesString = string.Join(", ", interfaces);
        _logger.LogInformation("The type name is: {type}", type.Name);
        _logger.LogInformation("The type's interfaces are: {interfaces}", interfacesString);
    }

    var method = type.GetMethod("ExecuteAsync");
    var assemblyVersion = flowLibraryAssembly.GetName().Version;
    //var x = ActivatorUtilities.CreateInstance(builder.Build().Services, type);
}

//var someFlowType = flowLibraryAssembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IFlow))).FirstOrDefault();
//_logger.LogInformation("Here is an IFlow implemented class: {class}", someFlowType.Name);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevelopmentCORS");
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHub<BlockFlowStateMetricsHub>($"/hubs/{nameof(BlockFlowStateMetricsHub).ToLower()}");

var logger = app.Services.GetRequiredService<ILogger<Program>>();

try
{
    using (var scope = app.Services.CreateScope())
    using (var dbContext = scope.ServiceProvider.GetRequiredService<DidactDbContext>())
    try
    {
        logger.LogInformation("Attempting to migrate the database on engine startup...");
        dbContext.Database.Migrate();
        logger.LogInformation("Database migrated successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Unhandled exception while applying migrations for {T}", typeof(DidactDbContext));
        throw;
    }

    app.Run();
    return 0;
}
catch (Exception ex)
{
    logger.LogCritical(ex, "An unhandled exception occurred during bootstrapping");
    return 1;
}