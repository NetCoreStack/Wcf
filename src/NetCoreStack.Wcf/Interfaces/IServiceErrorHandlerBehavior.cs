using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace NetCoreStack.Wcf
{
    public interface IServiceErrorHandlerBehavior : IErrorHandler, IServiceBehavior
    {
    }
}