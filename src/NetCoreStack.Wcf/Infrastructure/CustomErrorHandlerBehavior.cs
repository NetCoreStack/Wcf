using NetCoreStack.Contracts;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using static NetCoreStack.Wcf.SharedConstants;

namespace NetCoreStack.Wcf
{
    public class CustomErrorHandlerBehavior : IServiceErrorHandlerBehavior
    {
        private FaultException<ServiceException> GetFaultException(Exception error)
        {
            long errorCode = 0;
            string contractRuleMessage = null;
            var contractRuleException = error as ContractRuleException;
            if (contractRuleException == null && error.InnerException != null)
                contractRuleException = error.InnerException as ContractRuleException;

            if (contractRuleException != null)
            {
                contractRuleMessage = contractRuleException.Message;
                var exception = new ServiceException(contractRuleMessage, errorCode);
                return new FaultException<ServiceException>(exception, DefaultFaultReason);
            }

            string message = error.Message;
            if (error.InnerException != null)
                message = message + Environment.NewLine + error.InnerException.Message;

            var serviceException = new ServiceException(message, errorCode);
            return new FaultException<ServiceException>(serviceException, DefaultFaultReason);
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                channelDispatcher.ErrorHandlers.Add(this);
            }
        }

        public bool HandleError(Exception error)
        {
            var handler = new CustomExceptionHandler();
            return handler.HandleException(error);
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            FaultException<ServiceException> faultException = GetFaultException(error);
            MessageFault messageFault = faultException.CreateMessageFault();
            fault = Message.CreateMessage(version, messageFault, faultException.Action);
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var svcEndPoint in serviceDescription.Endpoints)
            {
                // Don't check mex
                if (!svcEndPoint.Contract.Name.Equals("IMetadataExchange", StringComparison.CurrentCultureIgnoreCase))
                {
                    foreach (var opDesc in svcEndPoint.Contract.Operations)
                    {
                        // Operation contract has no faults associated with them
                        if (opDesc.Faults.Count == 0)
                        {
                            string msg = string.Format(
                               $"{nameof(CustomErrorHandlerBehavior)} requires a FaultContract(typeof(ServiceException))" +
                                " on each operation contract. The {0} contains no FaultContracts.",
                                opDesc.Name);
                            throw new InvalidOperationException(msg);
                        }
                        // Operation contract has faults
                        var fcExists = from fc in opDesc.Faults
                                       where fc.DetailType == typeof(ServiceException)
                                       select fc;
                        // but not of our custom fault type
                        if (fcExists.Count() == 0)
                        {
                            string msg =
                                $"{nameof(CustomErrorHandlerBehavior)} requires a FaultContract(typeof(ServiceException)) " +
                                " on each operation contract.";
                            throw new InvalidOperationException(msg);
                        }
                    }
                }
            }
        }
    }
}