using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace NetCoreStack.Wcf
{
    public abstract class CustomServiceHostBase : ServiceHost
    {
        protected abstract string Address { get; }

        protected abstract Binding GetBinding();

        private static readonly int maxReceivedMessageSize = 1024 * 1024 * 10;

        public CustomServiceHostBase(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            var interfaceType = serviceType.GetInterfaces().Except(new[] { typeof(IServiceBase), typeof(IDisposable) }).FirstOrDefault();

            var binding = GetBinding();
            if (binding is HttpBindingBase bindingBase)
            {
                bindingBase.MaxReceivedMessageSize = maxReceivedMessageSize;
            }

            if (binding is NetTcpBinding netTcpBinding)
            {
                netTcpBinding.MaxReceivedMessageSize = maxReceivedMessageSize;
            }

            ServiceEndpoint endpoint = this.AddServiceEndpoint(interfaceType, binding, Address);
            Description.Behaviors.Add(new CustomInstanceProvider(serviceType));
        }

        //Overriding ApplyConfiguration() allows us to 
        //alter the ServiceDescription prior to opening
        //the service host. 
        protected override void ApplyConfiguration()
        {
            //First, we call base.ApplyConfiguration()
            //to read any configuration that was provided for
            //the service we're hosting. After this call,
            //this.ServiceDescription describes the service
            //as it was configured.
            base.ApplyConfiguration();

            Description.Behaviors.Add(new CustomErrorHandlerBehavior());
            
            var serviceBehavior = Description.Behaviors.Find<ServiceBehaviorAttribute>();
            if (serviceBehavior != null)
            {
                serviceBehavior.InstanceContextMode = InstanceContextMode.PerCall;
                serviceBehavior.AddressFilterMode = AddressFilterMode.Any;
            }

            ServiceDebugBehavior debugBehavior = this.Description.Behaviors.Find<ServiceDebugBehavior>();
            if (debugBehavior == null)
            {
                debugBehavior = new ServiceDebugBehavior();
                Description.Behaviors.Add(debugBehavior);
            }
            debugBehavior.IncludeExceptionDetailInFaults = true;

            //Now that we've populated the ServiceDescription, we can reach into it
            //and do interesting things (in this case, we'll add an instance of
            //ServiceMetadataBehavior if it's not already there.
            ServiceMetadataBehavior mexBehavior = Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (mexBehavior == null)
            {
                mexBehavior = new ServiceMetadataBehavior();
                mexBehavior.HttpGetEnabled = true;
                mexBehavior.HttpsGetEnabled = true;
                Description.Behaviors.Add(mexBehavior);
            }
            else
            {
                //Metadata behavior has already been configured, 
                //so we don't have any work to do.
                return;
            }

            //Add a metadata endpoint at http or https base address
            foreach (Uri baseAddress in BaseAddresses)
            {
                if (baseAddress.Scheme == Uri.UriSchemeHttp || baseAddress.Scheme == Uri.UriSchemeHttps)
                {
                    mexBehavior.HttpGetEnabled = true;
                    AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
                }

                if (baseAddress.Scheme == Uri.UriSchemeNetTcp)
                {
                    AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
                }
            }
        }
    }
}