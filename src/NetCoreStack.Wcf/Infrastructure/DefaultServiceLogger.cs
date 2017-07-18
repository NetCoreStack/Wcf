using System.Reflection;

namespace NetCoreStack.Wcf
{
    public class DefaultServiceLogger : IServiceLogger
    {
        private NetCoreStackMarkerService _markerService;
        public DefaultServiceLogger(NetCoreStackMarkerService markerService)
        {
            _markerService = markerService;
        }

        public void Invoke(string callerId, string serviceName, MethodInfo methodInfo, object args)
        {
            // implement logger
        }
    }
}