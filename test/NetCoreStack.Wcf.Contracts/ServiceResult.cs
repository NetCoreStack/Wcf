using System.Runtime.Serialization;

namespace NetCoreStack.Wcf.Contracts
{
    [DataContract]
    public class ServiceResult
    {
        [DataMember]
        public string FaultCode { get; set; }

        [DataMember]
        public MessageResultState State { get; set; }

        [DataMember]
        public string Message { get; set; }

        public ServiceResult(string message = "",
            MessageResultState state = MessageResultState.Success,
            string faultCode = "")
        {
            Message = message;
            State = state;
            FaultCode = faultCode;
        }
    }

    [DataContract]
    public sealed class ServiceResult<T> : ServiceResult
    {
        [DataMember]
        public T Result { get; set; }

        public ServiceResult(T result, string message = "",
            MessageResultState state = MessageResultState.Success,
            string faultCode = "")
            : base(message, state, faultCode)
        {
            Result = result;
        }
    }
}
