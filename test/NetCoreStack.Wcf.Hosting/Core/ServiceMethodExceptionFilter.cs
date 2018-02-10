using SharpRaven.Data;
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
                var sentryEvent = new SentryEvent(exception);
                var identity = _identityProvider.Principal.Identity?.Name;

                sentryEvent.Tags.Add("method", targetMethod.Name);
                sentryEvent.Tags.Add("identity", identity);

                HostingFactory.RavenClient.Capture(new SentryEvent(exception));
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