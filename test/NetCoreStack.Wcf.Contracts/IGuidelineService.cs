using System.ServiceModel;

namespace NetCoreStack.Wcf.Contracts
{
    [ServiceContract]
    public interface IGuidelineService
    {
        [OperationContract]        
        ServiceResult<int> ReturnPrimitive();

        [OperationContract]
        ServiceResult<CompositeType> RefTypeParameter(CompositeType model);

        [OperationContract]
        ServiceResult ThrowException();

        [OperationContract]
        ServiceResult ThrowContractRuleException(long requiredParam);
    }
}