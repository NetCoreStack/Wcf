using System;
using System.Security.Principal;

namespace NetCoreStack.Wcf
{
    public class InstanceInvokeContext
    {
        public Type ContractType { get; }

        public IPrincipal Principal { get; }

        public bool CanExecute { get; set; } = true;

        public InstanceInvokeContext(Type contractType, IPrincipal principal)
        {
            ContractType = contractType;
            Principal = principal;
        }
    }
}