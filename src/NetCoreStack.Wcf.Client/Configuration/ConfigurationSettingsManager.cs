using System;
using System.Configuration;

namespace NetCoreStack.Wcf.Client
{
    public static class ConfigurationSettingsManager
    {
        public static Configuration Configuration { get; }

        public static CustomConfigurationSection CustomSettings { get; }

        static ConfigurationSettingsManager()
        {
            Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            CustomSettings = Configuration.GetSection(nameof(CustomConfigurationSection)) as CustomConfigurationSection;

            if (CustomSettings == null)
            {
                throw new InvalidOperationException($"{nameof(CustomConfigurationSection)} not found in the config file");
            }
        }

        public static string GetApplicationSetting(string key)
        {
            KeyValueConfigurationElement element = Configuration.AppSettings.Settings[key];
            if (element != null && !string.IsNullOrEmpty(element.Value))
            {
                return element.Value;
            }

            return null;
        }

        public static ServiceEndPointSettingElement GetServiceEndPointSetting(string key)
        {
            ServiceEndPointSettingElement setting = CustomSettings.ServiceEndPointSettings[key];
            return setting;
        }

        public static string GetServiceEndPointBaseAddress(string key)
        {
            string value = null;
            ServiceEndPointSettingElement setting = CustomSettings.ServiceEndPointSettings[key];
            if (setting != null)
            {
                value = setting.BaseAddress;
            }
            else
            {
                value = null;
            }
            return value;
        }

        public static ServiceEndPointSettingsCollection GetServiceEndPointSettings()
        {
            return CustomSettings.ServiceEndPointSettings;
        }
    }
}