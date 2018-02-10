using System.Reflection;

namespace NetCoreStack.Wcf
{
    public interface IServiceLogger
    {
        void Invoke(string callerId, string serviceName, MethodInfo targetMethod, object args, object @return);
    }
}
