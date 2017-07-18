using System;
using System.Security.Principal;

namespace NetCoreStack.Wcf
{
    public abstract class ServiceBase : IServiceBase
    {
        public IServiceProvider ApplicationServices { get; set; }

        public IPrincipal Principal { get; set; }
    }
}