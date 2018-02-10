namespace NetCoreStack.Wcf
{
    public class DefaultServiceInstanceInvokeFilter : IServiceInstanceInvokeFilter
    {
        public void Invoke(InstanceInvokeContext context)
        {
            var identity = context.Principal.Identity;
            // allow all
            context.CanExecute = true;
        }
    }
}
