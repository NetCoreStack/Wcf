using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace NetCoreStack.Wcf
{
    public class Factory
    {
        private IServiceCollection _services;
        private IApplicationBuilder _appBuilder;
        private StartupMethods _methods;
        private IHostingEnvironment _hostingEnvironment;
        public static IServiceProvider ApplicationServices { get; private set; }

        public Factory()
        {
            _services = new ServiceCollection();
            _appBuilder = new DefaultApplicationBuilder();
            _hostingEnvironment = new HostingEnvironment();
            _services.AddSingleton<IApplicationBuilder>(_appBuilder);
            _services.AddSingleton<IHostingEnvironment>(_hostingEnvironment);
            ApplicationServices = _services.BuildServiceProvider();
            _appBuilder.ApplicationServices = ApplicationServices;
        }

        public void Build<TStartup>()
        {
            _methods = StartupLoader.LoadMethods(ApplicationServices, typeof(TStartup), _hostingEnvironment.EnvironmentName);

            _services.AddOptions();
            _services.AddSingleton<IServiceInstanceInvokeFilter, DefaultServiceInstanceInvokeFilter>();

            // overridable
            _services.TryAdd(ServiceDescriptor.Scoped<IIdentityProvider, DefaultIdentityProvider>());
            _services.TryAdd(ServiceDescriptor.Scoped<IServiceMethodExceptionFilter, DefaultServiceMethodExceptionFilter>());

            _methods.ConfigureServicesDelegate(_services);
            _methods.ConfigureDelegate(_appBuilder);
            ApplicationServices = _services.BuildServiceProvider();
        }
    }
}