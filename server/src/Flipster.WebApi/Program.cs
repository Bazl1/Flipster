using Flipster.Modules.Catalog;
using Flipster.Modules.Catalog.Domain.Repositories;
using Flipster.Modules.Catalog.Infrastructure.Persistence.Seeds;
using Flipster.Modules.Chats;
using Flipster.Modules.Users;
using Flipster.Modules.Users.Domain.Repositories;
using Flipster.Modules.Users.Infrastructure.Persistence.Seeds;
using Flipster.Shared.ImageStore.Services;
using Flispter.Shared.Contracts.EventBus;

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
    .AddSingleton<EventBus>()
    .AddAntiforgery()
    .AddUsersModule(builder.Configuration)
    .AddChatsModule(builder.Configuration)
    .AddCatalogModule(builder.Configuration);

var app = builder.Build();

{ 
    using var scope = app.Services.CreateScope();
    if (app.Environment.IsDevelopment())
    {
        new LocationSeed().Seed(scope.ServiceProvider);
        new CategorySeed().Seed(scope.ServiceProvider);
    }
    var eventBus = scope.ServiceProvider.GetRequiredService<EventBus>();
    eventBus.Subscribe(new Flipster.Modules.Catalog.EventHandlers.UserLoggedinHandler(scope.ServiceProvider.GetRequiredService<IViewRepository>()));
    eventBus.Subscribe(new Flipster.Modules.Users.EventHandlers.UserLoggedinHandler(scope.ServiceProvider.GetRequiredService<IFavoriteRepository>()));
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.UseStaticFiles();
// app.UseMiddleware<ErrorHandlingMiddleware>();
// app.UseMiddleware<LoggingMiddleware>();

app
    .MapUsersModuleEndpoints()
    .MapCatalogModuleEndpoints()
    .MapChatsModuleEndpoints();

app.Run();