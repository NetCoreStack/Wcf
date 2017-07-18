using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreStack.Wcf
{
    public static class InstanceTypeExtensions
    {
        public static Type GetImmediateInterface(this Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            var interfaces = serviceType.GetInterfaces();
            var result = new HashSet<Type>(interfaces);
            foreach (Type i in interfaces)
            {
                result.ExceptWith(i.GetInterfaces());
            }

            result.ExceptWith(new List<Type> { typeof(IServiceBase) });
            return result.FirstOrDefault();
        }
    }
}