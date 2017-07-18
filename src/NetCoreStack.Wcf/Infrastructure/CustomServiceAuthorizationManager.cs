using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace NetCoreStack.Wcf.Infrastructure
{
    public class CustomServiceAuthorizationManager : ServiceAuthorizationManager
    {
        public CustomServiceAuthorizationManager()
        {
        }

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            string action = operationContext.RequestContext.RequestMessage.Headers.Action;
            //http wsdl get ise
            if (action.Equals("http://schemas.xmlsoap.org/ws/2004/09/transfer/Get"))
            {
                return true;
            }

            OperationContext context = OperationContext.Current;
            ServiceSecurityContext security = context.ServiceSecurityContext;

            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = (RemoteEndpointMessageProperty)prop[RemoteEndpointMessageProperty.Name];
            string ipAddress = endpoint.Address;

            security.AuthorizationContext.Properties["Principal"] = new GenericPrincipal(security.PrimaryIdentity, null);

            return true;
        }
    }
}