using System;

namespace NetCoreStack.Wcf.Hosting.Core
{
    public class ServiceMethodExceptionFilter : IServiceMethodExceptionFilter
    {
        public void Invoke(Exception exception)
        {
            return;
        }
    }
}