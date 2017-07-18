using System.Reflection;

namespace NetCoreStack.Wcf
{
    public interface IServiceLogger
    {
        void Invoke(string callerId, string serviceName, MethodInfo methodInfo, object args);
    }
}
