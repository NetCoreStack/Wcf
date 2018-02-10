using System;

namespace NetCoreStack.Wcf
{
    public class DefaultServiceMethodExceptionFilter : IServiceMethodExceptionFilter
    {
        public void Invoke(Exception exception)
        {
            return;
        }
    }
}
