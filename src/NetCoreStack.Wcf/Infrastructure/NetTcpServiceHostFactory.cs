using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace NetCoreStack.Wcf
{
    public class NetTcpServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            List<Uri> endpoints = new List<Uri>();
            //foreach (Uri baseAddress in baseAddresses)
            //{
            //    var scheme = "net.tcp";
            //    var host = baseAddress.Host;
            //    var port = baseAddress.Port;
            //    var pathAndQuery = baseAddress.PathAndQuery;

            //    if (pathAndQuery.StartsWith("/"))
            //    {
            //        pathAndQuery = pathAndQuery.Substring(1);
            //    }

            //    endpoints.Add(new Uri($"{scheme}://{host}:{port}/{pathAndQuery}"));
            //}

            var interfaceType = serviceType.GetInterfaces().Except(new[] { typeof(IServiceBase), typeof(IDisposable) }).FirstOrDefault();

            var baseAddress = baseAddresses[0];
            var scheme = "net.tcp";
            var host = baseAddress.Host;
            var port = baseAddress.Port;

            endpoints.Add(new Uri($"{scheme}://{host}:{port}"));
            endpoints.AddRange(baseAddresses);

            NetTcpServiceHost serviceHost = new NetTcpServiceHost(serviceType, endpoints.ToArray());

            serviceHost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

            var hostAddress = serviceHost.BaseAddresses;

            ServiceMetadataBehavior mexBehavior = serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (mexBehavior != null)
            {
                mexBehavior.HttpsGetEnabled = false;
                mexBehavior.HttpGetEnabled = false;
            }

                // AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

                // var interfaceType = TypeHelper.GetFirstImmediateInterface(serviceType);


                return serviceHost;
        }
    }

    public class NetTcpServiceHost : CustomServiceHostBase
    {
        protected override string Address { get { return ""; } }

        protected override Binding GetBinding()
        {
            return new NetTcpBinding(SecurityMode.None) { PortSharingEnabled = true };
        }

        public NetTcpServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
        }
    }
}
