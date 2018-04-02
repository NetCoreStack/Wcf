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
            var pathAndQuery = baseAddress.PathAndQuery;
            if (pathAndQuery.StartsWith("/"))
            {
                pathAndQuery = pathAndQuery.Substring(1);
            }

            endpoints.Add(new Uri($"{scheme}://{host}:{port}/{pathAndQuery}"));

            endpoints.AddRange(baseAddresses);

            var serviceHost = new ServiceHost(serviceType, endpoints.ToArray());

            // endpoint http
            serviceHost.AddServiceEndpoint(interfaceType, new BasicHttpBinding(), "");

            // endpoint net.tcp
            var netTcpBinding = new NetTcpBinding(SecurityMode.None)
            {
                PortSharingEnabled = true,
                MaxReceivedMessageSize = 1024 * 1024 * 10
            };
            serviceHost.AddServiceEndpoint(interfaceType, netTcpBinding, "");

            // endpoint mex
            ServiceMetadataBehavior mexBehavior = serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (mexBehavior != null)
            {
                mexBehavior.HttpGetEnabled = false;
                mexBehavior.HttpsGetEnabled = false;
            }

            serviceHost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
            serviceHost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

            return serviceHost;
        }
    }
}