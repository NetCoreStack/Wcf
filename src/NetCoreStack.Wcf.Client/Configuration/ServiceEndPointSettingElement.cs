using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreStack.Wcf.Client
{
    public class ServiceEndPointSettingElement : ConfigurationElement
    {
        [ConfigurationProperty("Name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return Convert.ToString(this["Name"]);
            }
            set
            {
                this["Name"] = value;
            }
        }

        [ConfigurationProperty("BaseAddress", IsRequired = true)]
        public string BaseAddress
        {
            get
            {
                return Convert.ToString(this["BaseAddress"]);
            }
            set
            {
                this["BaseAddress"] = value;
            }
        }

        [ConfigurationProperty("Binding", IsRequired = false)]
        public string Binding
        {
            get
            {
                return Convert.ToString(this["Binding"]);
            }
            set
            {
                this["Binding"] = value;
            }
        }

        [ConfigurationProperty("BindingConfiguration", IsRequired = false)]
        public string BindingConfiguration
        {
            get
            {
                return Convert.ToString(this["BindingConfiguration"]);
            }
            set
            {
                this["BindingConfiguration"] = value;
            }
        }
    }
}
