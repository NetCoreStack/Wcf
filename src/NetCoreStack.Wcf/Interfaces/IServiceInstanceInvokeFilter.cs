namespace NetCoreStack.Wcf
{
    public interface IServiceInstanceInvokeFilter
    {
        void Invoke(InstanceInvokeContext context);
    }
}