using System;

namespace NetCoreStack.Wcf
{
    public interface IApplicationBuilder
    {
        IServiceProvider ApplicationServices { get; set; }
    }
}