using Flipster.Modules.Adverts;
using Flipster.Modules.Adverts.Data;
using Flipster.Modules.Adverts.Data.Seeds;
using Flipster.Modules.Adverts.Endpoints;
using Flipster.Modules.Adverts.Entities;
using Flipster.Modules.Identity;
using Flipster.Modules.Identity.Endpoints;
using Flipster.Modules.Images;
using Flipster.Modules.Images.Endpoints;
using Flipster.Modules.Locations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddAntiforgery();

builder.Services
    .AddIdentityModule(builder.Configuration)
    .AddImagesModule(builder.Configuration)
    .AddAdvertsModule(builder.Configuration)
    .AddLocationsModule(builder.Configuration);

builder.Services.AddCors(options
    => options.AddDefaultPolicy(policy 
        => policy
            .WithOrigins("http://localhost:5173")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    {
        new CategorySeed().Seed(app.Services.CreateScope().ServiceProvider);
    }
}

app.UseCors();
app.UseStaticFiles();
app.UseRouting();

app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("api/auth")
    .MapAuthEndpoints();
app.MapGroup("api/users")
    .MapUsersEndpoints();

app.MapGroup("api/images")
    .MapImagesEndpoints();

app.MapGroup("api/locations")
    .MapLocationsEndpoints();

app.MapGroup("api/adverts")
    .MapAdvertsEndpoints();
app.MapGroup("api/categories")
    .MapCategoriesEndpoints();

app.Run();