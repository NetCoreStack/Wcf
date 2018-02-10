using SharpRaven;

namespace NetCoreStack.Wcf.Hosting.Core
{
    public static class HostingFactory
    {
        public static RavenClient RavenClient { get; }

        static HostingFactory()
        {
            RavenClient = new RavenClient("<DSN-placeholder>");
        }
    }
}