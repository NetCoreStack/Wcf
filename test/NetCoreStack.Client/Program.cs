using NetCoreStack.Wcf.Contracts;
using System;

namespace NetCoreStack.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            GuidelineServiceReference.GuidelineServiceClient client = new GuidelineServiceReference.GuidelineServiceClient();
            var response = client.RefTypeParameter(new CompositeType { });
            Console.WriteLine(response.Result.Echo);
            Console.ReadLine();
        }
    }
}
