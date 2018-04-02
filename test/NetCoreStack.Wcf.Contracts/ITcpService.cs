using NetCoreStack.Contracts;
using System.ServiceModel;

namespace NetCoreStack.Wcf.Contracts
{
    [ServiceContract]
    public interface ITcpService
    {
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        ServiceResult<CompositeType> RefTypeParameter(CompositeType model);
    }
}