using NetCoreStack.Contracts;
using System.ServiceModel;

namespace NetCoreStack.Wcf.Contracts
{
    [ServiceContract]
    public interface IGuidelineService
    {
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        ServiceResult<int> ReturnPrimitive();

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [MethodLog]
        ServiceResult<int> LoggedServiceMethod(CompositeType parameter);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        ServiceResult<CompositeType> RefTypeParameter(CompositeType model);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        ServiceResult ThrowException();

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        ServiceResult ThrowContractRuleException(long requiredParam);
    }
}