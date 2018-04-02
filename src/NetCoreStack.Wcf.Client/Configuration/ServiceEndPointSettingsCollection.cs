using System.Configuration;

namespace NetCoreStack.Wcf.Client
{
    public class ServiceEndPointSettingsCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return "EndPoint";
            }
        }

        public ServiceEndPointSettingElement this[int index]
        {
            get
            {
                return (ServiceEndPointSettingElement)base.BaseGet(index);
            }
        }

        public new ServiceEndPointSettingElement this[string name]
        {
            get
            {
                return (ServiceEndPointSettingElement)base.BaseGet(name);
            }
        }


        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceEndPointSettingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServiceEndPointSettingElement)element).Name;
        }

        public int IndexOf(ServiceEndPointSettingElement applicationSettingElement)
        {
            return base.BaseIndexOf(applicationSettingElement);
        }
    }
}
