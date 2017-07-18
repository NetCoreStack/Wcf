using System;
using System.Reflection;

namespace NetCoreStack.Wcf
{
    internal static class ProxyHelper
    {
        internal static readonly MethodInfo CreateProxyDelegate = 
            typeof(ProxyHelper).GetTypeInfo().GetDeclaredMethod("CreateProxyOfT");

        internal static object CreateProxy(Type serviceContract)
        {
            var genericRegistry = CreateProxyDelegate.MakeGenericMethod(serviceContract);
            return genericRegistry.Invoke(null, null);
        }

        internal static TService CreateProxyOfT<TService>()
        {
            dynamic proxy = InstanceDispatchProxy.Create<TService, InstanceDispatchProxy>();
            var context = new ProxyContext(typeof(TService), Factory.ApplicationServices);
            proxy.Initialize(context);
            return proxy;
        }
    }
}