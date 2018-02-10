using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreStack.Wcf
{
    public static class ServiceCollectionExtension
    {
        public static void AddNetCoreStackWcf(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        }
    }
}
