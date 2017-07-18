using Microsoft.Extensions.DependencyInjection;
using System;

namespace NetCoreStack.Wcf
{
    public interface IStartup
    {
        IServiceProvider ConfigureServices(IServiceCollection services);

        void Configure(IApplicationBuilder app);
    }
}