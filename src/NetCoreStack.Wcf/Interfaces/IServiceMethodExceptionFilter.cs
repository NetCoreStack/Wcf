using System;

namespace NetCoreStack.Wcf
{
    public interface IServiceMethodExceptionFilter
    {
        void Invoke(Exception exception);
    }
}