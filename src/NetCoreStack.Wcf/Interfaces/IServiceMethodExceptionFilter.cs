using System;
using System.Reflection;

namespace NetCoreStack.Wcf
{
    public interface IServiceMethodExceptionFilter
    {
        void Invoke(MethodInfo targetMethod, Exception exception);
    }
}