﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetCoreStack.Client.GuidelineServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GuidelineServiceReference.IGuidelineService")]
    public interface IGuidelineService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGuidelineService/ReturnPrimitive", ReplyAction="http://tempuri.org/IGuidelineService/ReturnPrimitiveResponse")]
        NetCoreStack.Wcf.Contracts.ServiceResult<int> ReturnPrimitive();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGuidelineService/ReturnPrimitive", ReplyAction="http://tempuri.org/IGuidelineService/ReturnPrimitiveResponse")]
        System.Threading.Tasks.Task<NetCoreStack.Wcf.Contracts.ServiceResult<int>> ReturnPrimitiveAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGuidelineService/RefTypeParameter", ReplyAction="http://tempuri.org/IGuidelineService/RefTypeParameterResponse")]
        NetCoreStack.Wcf.Contracts.ServiceResult<NetCoreStack.Wcf.Contracts.CompositeType> RefTypeParameter(NetCoreStack.Wcf.Contracts.CompositeType model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGuidelineService/RefTypeParameter", ReplyAction="http://tempuri.org/IGuidelineService/RefTypeParameterResponse")]
        System.Threading.Tasks.Task<NetCoreStack.Wcf.Contracts.ServiceResult<NetCoreStack.Wcf.Contracts.CompositeType>> RefTypeParameterAsync(NetCoreStack.Wcf.Contracts.CompositeType model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGuidelineService/ThrowException", ReplyAction="http://tempuri.org/IGuidelineService/ThrowExceptionResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(NetCoreStack.Wcf.Contracts.ServiceResult<NetCoreStack.Wcf.Contracts.CompositeType>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(NetCoreStack.Wcf.Contracts.ServiceResult<int>))]
        NetCoreStack.Wcf.Contracts.ServiceResult ThrowException();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGuidelineService/ThrowException", ReplyAction="http://tempuri.org/IGuidelineService/ThrowExceptionResponse")]
        System.Threading.Tasks.Task<NetCoreStack.Wcf.Contracts.ServiceResult> ThrowExceptionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGuidelineService/ThrowContractRuleException", ReplyAction="http://tempuri.org/IGuidelineService/ThrowContractRuleExceptionResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(NetCoreStack.Wcf.Contracts.ServiceResult<NetCoreStack.Wcf.Contracts.CompositeType>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(NetCoreStack.Wcf.Contracts.ServiceResult<int>))]
        NetCoreStack.Wcf.Contracts.ServiceResult ThrowContractRuleException(long requiredParam);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGuidelineService/ThrowContractRuleException", ReplyAction="http://tempuri.org/IGuidelineService/ThrowContractRuleExceptionResponse")]
        System.Threading.Tasks.Task<NetCoreStack.Wcf.Contracts.ServiceResult> ThrowContractRuleExceptionAsync(long requiredParam);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGuidelineServiceChannel : NetCoreStack.Client.GuidelineServiceReference.IGuidelineService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GuidelineServiceClient : System.ServiceModel.ClientBase<NetCoreStack.Client.GuidelineServiceReference.IGuidelineService>, NetCoreStack.Client.GuidelineServiceReference.IGuidelineService {
        
        public GuidelineServiceClient() {
        }
        
        public GuidelineServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GuidelineServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GuidelineServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GuidelineServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public NetCoreStack.Wcf.Contracts.ServiceResult<int> ReturnPrimitive() {
            return base.Channel.ReturnPrimitive();
        }
        
        public System.Threading.Tasks.Task<NetCoreStack.Wcf.Contracts.ServiceResult<int>> ReturnPrimitiveAsync() {
            return base.Channel.ReturnPrimitiveAsync();
        }
        
        public NetCoreStack.Wcf.Contracts.ServiceResult<NetCoreStack.Wcf.Contracts.CompositeType> RefTypeParameter(NetCoreStack.Wcf.Contracts.CompositeType model) {
            return base.Channel.RefTypeParameter(model);
        }
        
        public System.Threading.Tasks.Task<NetCoreStack.Wcf.Contracts.ServiceResult<NetCoreStack.Wcf.Contracts.CompositeType>> RefTypeParameterAsync(NetCoreStack.Wcf.Contracts.CompositeType model) {
            return base.Channel.RefTypeParameterAsync(model);
        }
        
        public NetCoreStack.Wcf.Contracts.ServiceResult ThrowException() {
            return base.Channel.ThrowException();
        }
        
        public System.Threading.Tasks.Task<NetCoreStack.Wcf.Contracts.ServiceResult> ThrowExceptionAsync() {
            return base.Channel.ThrowExceptionAsync();
        }
        
        public NetCoreStack.Wcf.Contracts.ServiceResult ThrowContractRuleException(long requiredParam) {
            return base.Channel.ThrowContractRuleException(requiredParam);
        }
        
        public System.Threading.Tasks.Task<NetCoreStack.Wcf.Contracts.ServiceResult> ThrowContractRuleExceptionAsync(long requiredParam) {
            return base.Channel.ThrowContractRuleExceptionAsync(requiredParam);
        }
    }
}
