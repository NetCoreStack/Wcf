using System.Reflection;

namespace NetCoreStack.Wcf.Hosting.Core
{
    public class ServiceMethodLogger : IServiceLogger
    {
        public void Invoke(string callerId, string serviceName, MethodInfo targetMethod, object args, object @return)
        {

        }
    }
}