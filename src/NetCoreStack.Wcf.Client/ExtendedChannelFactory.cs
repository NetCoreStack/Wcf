using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace NetCoreStack.Wcf.Client
{
    /// <summary>
    /// Creates an extended channel
    /// </summary>
    /// <typeparam name="T">The channel's type</typeparam>
    public class ExtendedChannelFactory<T> : ChannelFactory<T>
    {
        private EndpointAddress _address;
        private Uri _via;
        public T Proxy { get; set; }

        public ExtendedChannelFactory(ServiceEndpoint ep)
            : base(ep)
        {
        }

        public T CreateProxy()
        {
            if (_address != null)
            {
                return base.CreateChannel(_address, null);
            }
            throw new ArgumentNullException("EndpointAdress and Via params must be specified");
        }

        public override T CreateChannel(EndpointAddress address, Uri via)
        {
            _address = address;
            _via = via;
            var extendedProxy = new ExtendedRealProxy<T>(this, typeof(T), base.CreateChannel(address, via));
            Proxy = (T)extendedProxy.GetTransparentProxy();
            return Proxy;
        }
    }
}
