using Flipster.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options =>
    {
        options.LowercaseUrls = true;
        options.LowercaseQueryStrings = true;
    });

builder.Services
    .AddCors("http://localhost:5173")
    .AddDatabase()
    .AddServices()
    .AddAuth(builder.Configuration);

var app = builder.Build();

app.UseCors();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();