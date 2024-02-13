using Flipster.Modules.Identity;
using Flipster.Modules.Identity.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

builder.Services
    .AddIdentityModule(builder.Configuration);

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
}

app.UseCors();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("api/auth")
    .MapAuthEndpoints();

app.Run();