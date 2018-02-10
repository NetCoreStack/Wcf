using Microsoft.Extensions.DependencyInjection;
using NetCoreStack.Contracts;
using System;
using System.Reflection;
using System.Transactions;

namespace NetCoreStack.Wcf
{
    public class InstanceDispatchProxy : DispatchProxy
    {
        public ProxyContext ProxyContext { get; private set; }

        protected IServiceScopeFactory ScopeFactory
        {
            get
            {
                return ProxyContext?.ApplicationServices?.GetService<IServiceScopeFactory>();
            }
        }

        protected IIdentityProvider IdentityProvider
        {
            get
            {
                return ProxyContext?.ApplicationServices?.GetService<IIdentityProvider>();
            }
        }

        public InstanceDispatchProxy()
        {

        }

        public void Initialize(ProxyContext proxyContext)
        {
            ProxyContext = proxyContext;
        }

        protected TransactionScope CreateTransactionScope(TransactionScopeOption scopeOption, IsolationLevel isolationLevel)
        {
            return new TransactionScope(scopeOption, new TransactionOptions { IsolationLevel = isolationLevel });
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            using (var scope = ScopeFactory.CreateScope())
            {
                var instance = scope.ServiceProvider.GetService(ProxyContext.ContractType);
                if (instance == null)
                    throw new InvalidOperationException($"{scope.ServiceProvider} make sure register the contract type of {ProxyContext.ContractType.FullName}");

                var serviceBase = instance as ServiceBase;
                if (serviceBase == null)
                    throw new InvalidOperationException($"{instance.GetType().FullName} make sure implement type of {typeof(ServiceBase).FullName}");

                var identityProvider = scope.ServiceProvider.GetService<IIdentityProvider>();
                var invocationFilter = scope.ServiceProvider.GetService<IServiceInstanceInvokeFilter>();
                var exceptionFilter = scope.ServiceProvider.GetService<IServiceMethodExceptionFilter>();
                serviceBase.ApplicationServices = scope.ServiceProvider;
                serviceBase.Principal = identityProvider?.Principal;
                var callerId = identityProvider.Principal.Identity?.Name;

                var invocationContext = new InstanceInvokeContext(ProxyContext.ContractType, identityProvider?.Principal);
                invocationFilter?.Invoke(invocationContext);
                if (!invocationContext.CanExecute)
                {
                    var reason = $"{ DateTime.Now.ToString() } - CallerId: { callerId } - Unauthorized!";
                    throw new ContractRuleException(reason);
                }

                object returnObj = null;
                var serviceLog = targetMethod.GetCustomAttribute<MethodLogAttribute>(false);
                var transactionAttribute = targetMethod.GetCustomAttribute<TransactionAttribute>(false);
                if (transactionAttribute != null)
                {
                    using (var tranScope = CreateTransactionScope(transactionAttribute.TransactionScopeOption, transactionAttribute.IsolationLevel))
                    {
                        try
                        {
                            returnObj = targetMethod.Invoke(instance, args);
                        }
                        catch (Exception ex)
                        {
                            exceptionFilter?.Invoke(targetMethod, ex);
                            throw ex;
                        }

                        tranScope.Complete();
                    }

                    return returnObj;
                }
                
                try
                {
                    returnObj = targetMethod.Invoke(instance, args);
                }
                catch (Exception ex)
                {
                    exceptionFilter?.Invoke(targetMethod, ex);
                    throw ex;
                }

                if (serviceLog != null)
                {
                    var serviceName = instance.GetType().Name;
                    var serviceLogger = scope.ServiceProvider.GetService<IServiceLogger>();
                    serviceLogger.Invoke(callerId: callerId, 
                        serviceName: serviceName, 
                        targetMethod: targetMethod, 
                        args: args, 
                        @return: returnObj);
                }

                return returnObj;
            }
        }
    }
}