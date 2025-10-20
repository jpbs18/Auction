using AuctionService.Configurations.Database;
using AuctionService.Configurations.Middlewares;
using AuctionService.Configurations.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseDatabaseInitializer();
app.UseApplicationMiddleware();
app.UseAuthorization();
app.MapControllers();
app.Run();
