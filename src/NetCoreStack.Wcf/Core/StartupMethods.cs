using Microsoft.Extensions.DependencyInjection;
using System;

namespace NetCoreStack.Wcf
{
    public class StartupMethods
    {
        internal static Func<IServiceCollection, IServiceProvider> DefaultBuildServiceProvider = s => s.BuildServiceProvider();

        public StartupMethods(Action<IApplicationBuilder> configure)
            : this(configure, configureServices: null)
        {
        }

        public StartupMethods(Action<IApplicationBuilder> configure, Func<IServiceCollection, IServiceProvider> configureServices)
        {
            ConfigureDelegate = configure;
            ConfigureServicesDelegate = configureServices ?? DefaultBuildServiceProvider;
        }

        public Func<IServiceCollection, IServiceProvider> ConfigureServicesDelegate { get; }
        public Action<IApplicationBuilder> ConfigureDelegate { get; }
    }
}
