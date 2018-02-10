using System;
using System.Reflection;

namespace NetCoreStack.Wcf
{
    public class DefaultServiceMethodExceptionFilter : IServiceMethodExceptionFilter
    {
        public void Invoke(MethodInfo targetMethod, Exception exception)
        {
            return;
        }
    }
}
