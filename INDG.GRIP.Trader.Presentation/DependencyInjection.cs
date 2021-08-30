using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using INDG.GRIP.Trader.Presentation.Filters;

namespace INDG.GRIP.Trader.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers(opt =>
            {
                opt.Filters.Add(new CustomExceptionFilterAttribute());
            });
            
            return services;
        }

        public static IApplicationBuilder PresentationMap(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            return app;
        }
    }
}