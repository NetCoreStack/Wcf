using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using NetCoreStack.Wcf.Contracts;

namespace NetCoreStack.Wcf.Hosting
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var environmentName = Environment.GetEnvironmentVariable(SharedConstants.EnvironmentVariableName);
            if (string.IsNullOrEmpty(environmentName))
                environmentName = EnvironmentName.Development;

            _env.EnvironmentName = environmentName;

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddMemoryCache();
            services.AddNetCoreStackWcf(Configuration);

            services.AddScoped<SampleDependency>();
            services.AddScoped<IGuidelineService, GuidelineService>();

            services.AddSingleton(_ => services);
        }

        public void Configure(IApplicationBuilder app, IServiceScopeFactory scopeFactory)
        {
        }
    }
}