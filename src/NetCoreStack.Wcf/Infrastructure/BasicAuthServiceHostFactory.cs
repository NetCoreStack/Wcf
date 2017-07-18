using Microsoft.Extensions.DependencyInjection;
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace NetCoreStack.Wcf
{
    public class BasicAuthServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            BasicAuthServiceHost serviceHost = new BasicAuthServiceHost(serviceType, baseAddresses);
            return serviceHost;
        }
    }

    public class BasicAuthServiceHost : CustomServiceHostBase
    {
        protected override string Address { get { return "/"; } }

        protected override Binding GetBinding()
        {
            var binding = new BasicHttpsBinding(BasicHttpsSecurityMode.TransportWithMessageCredential);
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            binding.Security.Transport.Realm = "";
            return binding;
        }

        private ServiceCredentials GetServiceCredentials()
        {
            var serviceCredentials = new ServiceCredentials();
            serviceCredentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
            serviceCredentials.UserNameAuthentication.CustomUserNamePasswordValidator = new CustomUserNameValidator();
            return serviceCredentials;
        }

        public BasicAuthServiceHost(Type serviceType, params Uri[] baseAddresses) 
            : base(serviceType, baseAddresses)
        {
        }

        protected override void ApplyConfiguration()
        {
            base.ApplyConfiguration();
            Description.Behaviors.Remove<ServiceCredentials>();
            Description.Behaviors.Add(GetServiceCredentials());

            var hostingEnv = Factory.ApplicationServices.GetService<IHostingEnvironment>();
            if (hostingEnv == null)
            {
                throw new InvalidOperationException($"{nameof(HostingEnvironment)} could not be found!");
            }

            if (hostingEnv.IsProduction())
            {
                // noop
            }
        }
    }
}