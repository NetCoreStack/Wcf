using System;

namespace NetCoreStack.Wcf.Client
{
    public class DefaultEndpointAddressResolver
    {
        public string Resolve<TContract>(string endpointBaseUrl)
        {
            return Resolve(typeof(TContract), endpointBaseUrl);
        }

        public string Resolve(Type contractType, string endpointBaseUrl)
        {
            string assemblyName = contractType.Assembly.GetName().Name;
            string baseAddress = ConfigurationSettingsManager.GetServiceEndPointBaseAddress(assemblyName);

            if (string.IsNullOrEmpty(baseAddress))
            {
                return string.Format("{0}/{1}.{2}", endpointBaseUrl.TrimEnd('/'), contractType.Name.Substring(1).TrimStart('/'), "svc");
            }

            return string.Format("{0}/{1}.{2}", baseAddress.TrimEnd('/'), contractType.Name.Substring(1).TrimStart('/'), "svc");
        }

        public string ResolveWithEndpoint(Type contractType, string endpointBaseUrl)
        {
            if (string.IsNullOrEmpty(endpointBaseUrl))
            {
                throw new ArgumentNullException("EndpointBaseUrl param required");
            }
            return string.Format("{0}/{1}.{2}", endpointBaseUrl.TrimEnd('/'), contractType.Name.Substring(1).TrimStart('/'), "svc");
        }
    }
}
