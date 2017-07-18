namespace NetCoreStack.Wcf.Hosting.App_Code
{
	public class ServiceInitializer
    {
		public static void AppInitialize()
		{
            var factory = new Factory();
            factory.Build<Startup>();
		}
	}
}