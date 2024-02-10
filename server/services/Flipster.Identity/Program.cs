using Flipster.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);


builder.Services
    .AddDatabase()
    .AddAuth(builder.Configuration);

var app = builder.Build();

//app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.Run();