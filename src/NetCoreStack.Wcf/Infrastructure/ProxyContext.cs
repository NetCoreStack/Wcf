using System;

namespace NetCoreStack.Wcf
{
    public class ProxyContext
    {
        public Type ContractType { get; set; }
        public IServiceProvider ApplicationServices { get; set; }

        public ProxyContext(Type contractType, IServiceProvider applicationServices)
        {
            ContractType = contractType;
            ApplicationServices = applicationServices;
        }
    }
}