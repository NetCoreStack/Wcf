using System.Configuration;

namespace NetCoreStack.Wcf.Client
{
    public sealed class CustomConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty(nameof(ServiceEndPointSettings))]
        public ServiceEndPointSettingsCollection ServiceEndPointSettings
        {
            get
            {
                return (ServiceEndPointSettingsCollection)this[nameof(ServiceEndPointSettings)];
            }
        }
    }
}