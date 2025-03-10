using DidactEngine.Services.Contexts;
using DidactEngine.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure Engine
builder.ConfigureCors();
builder.ConfigureOpenApi();
builder.ConfigureDatabase();
builder.ConfigureApplicationServices();

// Build Engine
var app = builder.Build();

// Configure middleware
app.ConfigureMiddleware();

// Run Engine
await app.StartEngine<DidactDbContext>();