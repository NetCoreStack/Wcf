using NetCoreStack.Wcf.Contracts;
using System;

namespace NetCoreStack.Wcf.Hosting
{
    public class TcpService : ServiceBase, ITcpService
    {
        public TcpService()
        {
        }

        public ServiceResult<CompositeType> RefTypeParameter(CompositeType model)
        {
            return new ServiceResult<CompositeType>(new CompositeType
            {
                BoolValue = true,
                Echo = "Echo from server " + DateTime.Now.ToString(),
                StringValue = "Some string value"
            });
        }
    }
}