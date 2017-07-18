using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;

namespace NetCoreStack.Wcf
{
    public class BasicHttpServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            BasicHttpServiceHost serviceHost = new BasicHttpServiceHost(serviceType, baseAddresses);

            var serviceBehavior = serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            serviceBehavior.InstanceContextMode = InstanceContextMode.PerCall;
            serviceBehavior.AddressFilterMode = AddressFilterMode.Any;

            return serviceHost;
        }
    }

    public class BasicHttpServiceHost : CustomServiceHostBase
    {
        protected override string Address { get { return "/"; } }

        protected override Binding GetBinding()
        {
            return new BasicHttpBinding();
        }

        public BasicHttpServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
        }
    }
}
