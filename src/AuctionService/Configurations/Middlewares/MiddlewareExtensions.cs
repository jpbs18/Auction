using AuctionService.Middlewares;

namespace AuctionService.Configurations.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseApplicationMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorMiddleware>();
            return app;
        }
    }
}
