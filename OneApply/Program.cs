using OneApply;
var builder = WebApplication.CreateBuilder(args);

builder.AddDependencyInjection();

var app = builder.Build();
app.SeedRolesToDatabase();
app.AddMiddleware();
app.Run();
