using System;

namespace NetCoreStack.Wcf
{
    public class DefaultApplicationBuilder : IApplicationBuilder
    {
        public IServiceProvider ApplicationServices { get; set; }

        public DefaultApplicationBuilder()
        {
        }
    }
}