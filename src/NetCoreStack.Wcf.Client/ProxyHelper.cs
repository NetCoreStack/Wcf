using System;
using System.Collections.Concurrent;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace NetCoreStack.Wcf.Client
{
    public class ProxyHelper
    {
        public static string DefaultEndpointBaseUrl { get; set; }
        public static Type DefaultBindingType { get; set; }
        public static string DefaultBindingConfigurationName { get; set; }
        public static bool UseExtendedRealChannelFactory { get; set; }
        public DefaultEndpointAddressResolver EndpointAddressResolver { get; set; }
        public DefaultSurrogateBehavior DataContractSurrogateBehavior { get; set; }
        public IClientMessageInspector ClientMessageInspectorBehavior { get; set; }
        private static ConcurrentDictionary<Type, Lazy<ChannelFactory>> ChannelFactoryCache { get; set; }

        public ProxyHelper()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            EndpointAddressResolver = new DefaultEndpointAddressResolver();
            ClientMessageInspectorBehavior = new ClientMessageInspectorBehavior();
            DataContractSurrogateBehavior = new DefaultSurrogateBehavior();
            DefaultEndpointBaseUrl = ConfigurationSettingsManager.GetApplicationSetting("DefaultEndpointBaseUrl");
            var bindingName = String.IsNullOrEmpty(ConfigurationSettingsManager.GetApplicationSetting("DefaultBindingType")) ? "BasicHttpBinding" : ConfigurationSettingsManager.GetApplicationSetting("DefaultBindingType");
            DefaultBindingType = Type.GetType((bindingName == "CustomBinding" ? String.Format("System.ServiceModel.Channels.{0}, System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", bindingName) : String.Format("System.ServiceModel.{0}, System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", bindingName)));
            DefaultBindingConfigurationName = String.IsNullOrEmpty(ConfigurationSettingsManager.GetApplicationSetting("DefaultBindingConfigurationName")) ? string.Empty : ConfigurationSettingsManager.GetApplicationSetting("DefaultBindingConfigurationName");
            ChannelFactoryCache = new ConcurrentDictionary<Type, Lazy<ChannelFactory>>();
        }

        private ServiceEndpoint PrepareProxyAndGetServiceEndpoint(Type contractType)
        {
            var contractFullNameWithoutAssembly = String.Format("{0}.{1}", contractType.Namespace, contractType.Name);
            var endpointSetting = ConfigurationSettingsManager.GetServiceEndPointSetting(contractFullNameWithoutAssembly);

            if (endpointSetting == null)
            {
                string assemblyName = contractType.Assembly.GetName().Name;
                endpointSetting = ConfigurationSettingsManager.GetServiceEndPointSetting(assemblyName);
            }

            Type bindingType = DefaultBindingType;
            string bindingConfigurationName = DefaultBindingConfigurationName;
            string endpointBaseAddress = DefaultEndpointBaseUrl;

            if (endpointSetting != null)
            {
                if (!string.IsNullOrEmpty(endpointSetting.Binding))
                {
                    bindingType = Type.GetType((endpointSetting.Binding == "CustomBinding"
                        ? String.Format(
                            "System.ServiceModel.Channels.{0}, System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
                            endpointSetting.Binding)
                        : String.Format(
                            "System.ServiceModel.{0}, System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
                            endpointSetting.Binding)));
                }
                if (!string.IsNullOrEmpty(endpointSetting.BindingConfiguration))
                {
                    bindingConfigurationName = endpointSetting.BindingConfiguration;
                }
                if (!string.IsNullOrEmpty(endpointSetting.BaseAddress))
                {
                    endpointBaseAddress = endpointSetting.BaseAddress;
                }
            }

            var binding = string.IsNullOrEmpty(bindingConfigurationName)
                    ? (Binding)Activator.CreateInstance(bindingType)
                    : (Binding)Activator.CreateInstance(bindingType, bindingConfigurationName);

            var endPoint = new ServiceEndpoint(ContractDescription.GetContract(contractType),
                        binding,
                        new EndpointAddress(EndpointAddressResolver.Resolve(contractType, endpointBaseAddress)));

            if (DataContractSurrogateBehavior != null)
            {
                foreach (var operation in endPoint.Contract.Operations)
                {
                    operation.OperationBehaviors.Add(DataContractSurrogateBehavior);
                }
            }

            if (ClientMessageInspectorBehavior != null)
            {
                endPoint.EndpointBehaviors.Add((IEndpointBehavior)ClientMessageInspectorBehavior);
            }

            return endPoint;
        }

        private dynamic GetChannelFactoryFromCache(Type contractType, Lazy<ChannelFactory> cf)
        {
            dynamic dcf = cf.Value;
            if (((ChannelFactory)dcf).State == CommunicationState.Faulted)
            {
                if (ChannelFactoryCache.TryRemove(contractType, out cf))
                {
                    var serviceEndPoint = PrepareProxyAndGetServiceEndpoint(contractType);

                    dcf = ChannelFactoryCache.GetOrAdd(contractType,
                          new Lazy<ChannelFactory>(() =>
                                  (ChannelFactory)Activator.CreateInstance(typeof(ChannelFactory<>)
                                  .MakeGenericType(contractType), serviceEndPoint))).Value;
                }
            }

            return dcf.CreateChannel();
        }

        public TContract CreateProxy<TContract>() where TContract : class
        {
            return (TContract)CreateProxy(typeof(TContract));
        }

        public object CreateProxy(Type contractType)
        {
            // cache exist check
            if (ChannelFactoryCache.ContainsKey(contractType))
            {
                Lazy<ChannelFactory> cf;
                if (ChannelFactoryCache.TryGetValue(contractType, out cf))
                {
                    if (cf != null)
                    {
                        return GetChannelFactoryFromCache(contractType, cf);
                    }
                }
            }

            ServiceEndpoint endPoint = PrepareProxyAndGetServiceEndpoint(contractType);

            dynamic channelFactory;

            channelFactory = ChannelFactoryCache.GetOrAdd(contractType,
                new Lazy<ChannelFactory>(() =>
                    (ChannelFactory)Activator.CreateInstance(typeof(ChannelFactory<>).MakeGenericType(contractType), endPoint))).Value;

            return channelFactory.CreateChannel();
        }
    }
}