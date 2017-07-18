using System;
using System.ServiceModel.Dispatcher;

namespace NetCoreStack.Wcf
{
    public class CustomExceptionHandler : ExceptionHandler
    {
        public override bool HandleException(Exception exception)
        {
            return true;
        }
    }
}