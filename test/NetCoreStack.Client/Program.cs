using NetCoreStack.Wcf.Contracts;
using System;

namespace NetCoreStack.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 2000000; i++)
            {
                CallLoggedMethod(i);
                Console.WriteLine("Iteration: " + i);
            }
        }

        private static void CallLoggedMethod(int i)
        {
            GuidelineServiceReference.GuidelineServiceClient client = new GuidelineServiceReference.GuidelineServiceClient();
            var serviceResult = client.LoggedServiceMethod(new CompositeType
            {
                BoolValue = true,
                IntegerValue = i,
                Echo = "Echo client " + i,
                StringValue = "Some string value!"
            });
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
