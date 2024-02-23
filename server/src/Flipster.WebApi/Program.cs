using Flipster.Modules.Catalog;
using Flipster.Modules.Catalog.Infrastructure.Persistence.Seeds;
using Flipster.Modules.Users;
using Flipster.Modules.Users.Infrastructure.Persistence.Seeds;
using Flipster.Shared.ImageStore.Services;
using Flipster.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors(opt => opt
        .AddDefaultPolicy(policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()));

builder.Services
    .AddTransient<IImageService, ImageService>()
    .AddAntiforgery()
    .AddUsersModule(builder.Configuration)
    .AddCatalogModule(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    new LocationSeed().Seed(scope.ServiceProvider);
    new CategorySeed().Seed(scope.ServiceProvider);
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.UseStaticFiles();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

app
    .MapUsersModuleEndpoints()
    .MapCatalogModuleEndpoints();

app.Run();