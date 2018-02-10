using NetCoreStack.Wcf.Contracts;
using System;

namespace NetCoreStack.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            CallLoggedMethod();   
        }

        private static void CallLoggedMethod()
        {
            GuidelineServiceReference.GuidelineServiceClient client = new GuidelineServiceReference.GuidelineServiceClient();
            var serviceResult = client.LoggedServiceMethod(new CompositeType { BoolValue = true, Echo = "Echo client", StringValue = "Some string value!" });
            Console.WriteLine(serviceResult.Result);
            Console.ReadLine();
        }

        private static void CallRefTypeParameterOperation()
        {
            GuidelineServiceReference.GuidelineServiceClient client = new GuidelineServiceReference.GuidelineServiceClient();
            var serviceResult = client.RefTypeParameter(new CompositeType { });
            Console.WriteLine(serviceResult.Result.Echo);
            Console.ReadLine();
        }

        private static void ThrowException()
        {
            GuidelineServiceReference.GuidelineServiceClient client = new GuidelineServiceReference.GuidelineServiceClient();
            var serviceResult = client.ThrowException();
            Console.ReadLine();
        }
    }
}
