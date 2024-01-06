using OneApply;
var builder = WebApplication.CreateBuilder(args);

builder.AddDependencyInjection();

var app = builder.Build();

app.AddMiddleware();
app.Run();
