using Leinhart.Web;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();

WebApplication app = builder.Build();

app.MapControllers();

await app.RunAsync();
