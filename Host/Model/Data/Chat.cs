using System.Runtime.Serialization;

namespace Host.Model.Data
{
    [DataContract]
    public class Chat
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Id { get; set; }
    }
}
