﻿using System.Reflection;

namespace NetCoreStack.Wcf
{
    public class DefaultServiceLogger : IServiceLogger
    {
        public void Invoke(string callerId, string serviceName, MethodInfo targetMethod, object args, object @return)
        {
            
        }
    }
}