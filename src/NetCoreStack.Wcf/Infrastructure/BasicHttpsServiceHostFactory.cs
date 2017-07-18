using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;

namespace NetCoreStack.Wcf
{
    public class BasicHttpsServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            BasicHttpsServiceHost serviceHost = new BasicHttpsServiceHost(serviceType, baseAddresses);
            return serviceHost;
        }
    }

    public class BasicHttpsServiceHost : CustomServiceHostBase
    {
        protected override string Address { get { return "/"; } }

        protected override Binding GetBinding()
        {
            return new BasicHttpsBinding();
        }

        public BasicHttpsServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
        }
    }
}