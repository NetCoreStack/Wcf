using System;
using System.Transactions;

namespace NetCoreStack.Wcf
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TransactionAttribute : Attribute
    {
        public IsolationLevel IsolationLevel { get; set; }
        public TransactionScopeOption TransactionScopeOption { get; set; }

        public TransactionAttribute(TransactionScopeOption scopeOption = TransactionScopeOption.Required,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            TransactionScopeOption = scopeOption;
            IsolationLevel = isolationLevel;
        }
    }
}
