using System.Runtime.Serialization;

namespace NetCoreStack.Wcf.Contracts
{
    [DataContract]
    public class CompositeType
    {
        [DataMember]
        public bool BoolValue { get; set; }

        [DataMember]
        public string StringValue { get; set; }

        [DataMember]
        public string Echo { get; set; }
    }
}
