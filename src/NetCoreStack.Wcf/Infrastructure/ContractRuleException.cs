using System;

namespace NetCoreStack.Wcf
{
    public class ContractRuleException : Exception
    {
        public ContractRuleException(string message)
            :base(message)
        {

        }
    }
}