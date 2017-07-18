using System;

namespace NetCoreStack.Wcf
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MethodLogAttribute : Attribute
    {
        public MethodLogAttribute()
        {

        }
    }
}
