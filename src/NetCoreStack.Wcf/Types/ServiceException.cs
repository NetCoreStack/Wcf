using System.Runtime.Serialization;

namespace NetCoreStack.Wcf
{
    [DataContract]
    public class ServiceException
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public long ErrorCode { get; set; }

        public ServiceException(string message, long errorCode = 0)
        {
            Message = message;
            ErrorCode = errorCode;
        }
    }
}
