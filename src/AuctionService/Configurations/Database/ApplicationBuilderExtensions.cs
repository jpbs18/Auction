using AuctionService.Data;

namespace AuctionService.Configurations.Database
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDatabaseInitializer(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();
            DbInitializer.Initialize(context);
        }
    }
}
