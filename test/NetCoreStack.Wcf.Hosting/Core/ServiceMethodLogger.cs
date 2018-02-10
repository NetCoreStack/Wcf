using SharpRaven.Data;
using System.Reflection;

namespace NetCoreStack.Wcf.Hosting.Core
{
    public class ServiceMethodLogger : IServiceLogger
    {
        private SentryMessage CreateSentryMessage(string serviceName, MethodInfo targetMethod)
        {
            return new SentryMessage($"{serviceName}/{targetMethod.Name}");
        }

        public void Invoke(string callerId, string serviceName, MethodInfo targetMethod, object args, object @return)
        {
            var sentryEvent = new SentryEvent(CreateSentryMessage(serviceName, targetMethod));
            sentryEvent.Tags.Add("method", targetMethod.Name);
            sentryEvent.Tags.Add("identity", callerId);
            sentryEvent.Level = ErrorLevel.Info;
            HostingFactory.RavenClient.Capture(sentryEvent);
        }
    }
}