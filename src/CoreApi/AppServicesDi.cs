using Bainah.Core.Interfaces;
using Bainah.Infrastructure.Repositories;
using Bainah.Infrastructure.Security;
using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Security;

namespace CoreApi
{
    public static class AppServicesDi
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IOtpService, OtpService>();
            services.AddScoped<IPageSection, PageSectionRepository>();
            services.AddScoped<IWebPageRepository, WebPageRepository>();
        }

    }
}
