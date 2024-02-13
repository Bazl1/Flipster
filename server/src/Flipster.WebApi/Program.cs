using Flipster.Modules.Identity;
using Flipster.Modules.Identity.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

builder.Services
    .AddIdentityModule(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("api/auth")
    .MapAuthEndpoints();

app.Run();