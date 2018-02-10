using System;
using System.Reflection;

namespace NetCoreStack.Wcf.Hosting.Core
{
    public class ServiceMethodExceptionFilter : IServiceMethodExceptionFilter
    {
        private readonly IIdentityProvider _identityProvider;

        public ServiceMethodExceptionFilter(IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public void Invoke(MethodInfo targetMethod, Exception exception)
        {
            bool captured = false;
            try
            {
                captured = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!captured)
                {
                    // fallback log
                }
            }
        }
    }
}