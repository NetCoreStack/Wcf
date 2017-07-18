using System.Security.Principal;

namespace NetCoreStack.Wcf
{
    public interface IIdentityProvider
    {
        IPrincipal Principal { get; }
    }
}