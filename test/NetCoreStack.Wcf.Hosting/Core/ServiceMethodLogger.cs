using SharpRaven.Data;
using System;
using System.Reflection;

namespace NetCoreStack.Wcf.Hosting.Core
{
    public class ServiceMethodLogger : IServiceLogger
    {
        private SentryMessage CreateSentryMessage(string serviceName, MethodInfo targetMethod)
        {
            return new SentryMessage($"{serviceName}/{targetMethod.Name}?uuid="+Guid.NewGuid().ToString("N"));
        }

        public void Invoke(string callerId, string serviceName, MethodInfo targetMethod, object args, object @return)
        {
            var sentryEvent = new SentryEvent(CreateSentryMessage(serviceName, targetMethod));
            sentryEvent.Extra = new { method = targetMethod.Name, args, @return };
            sentryEvent.Tags.Add("method", targetMethod.Name);
            sentryEvent.Level = ErrorLevel.Info;
            HostingFactory.RavenClient.Capture(sentryEvent);
        }
    }
}