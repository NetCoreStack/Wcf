using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel;
using System.Threading.Tasks;

namespace NetCoreStack.Wcf.Client
{
    internal class ExtendedRealProxy<T> : RealProxy
    {
        private ExtendedChannelFactory<T> _factory;
        private Type _proxyType;
        private T _channel;

        public ExtendedRealProxy(ExtendedChannelFactory<T> factory, Type proxyType, T channel)
            : base(proxyType)
        {
            _factory = factory;
            _proxyType = proxyType;
            _channel = channel;
        }

        private async Task Handle(Task taskMethod, object channel)
        {
            await taskMethod;
        }

        /// <summary>
        /// See <see cref="RealProxy.Invoke"/>
        /// </summary>
        public override IMessage Invoke(IMessage message)
        {
            var methodCall = message as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;

            var success = false;
            try
            {
                _channel = _factory.CreateProxy();
                var result = methodInfo.Invoke(_channel, methodCall.InArgs);
                success = true;
                return new ReturnMessage(
                      result, // Operation result
                      null, // Out arguments
                      0, // Out arguments count
                      methodCall.LogicalCallContext, // Call context
                      methodCall); // Original message
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (!success)
                {
                    if (_channel is IClientChannel)
                    {
                        var clientChannel = (IClientChannel)_channel;
                        if (clientChannel != null) clientChannel.Abort();
                    }
                }
                else
                {
                    if (_channel is IClientChannel)
                    {
                        var clientChannel = (IClientChannel)_channel;
                        if (clientChannel != null) clientChannel.Close();
                    }
                }
            }
        }
    }
}
