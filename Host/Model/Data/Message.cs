using System;
using System.Runtime.Serialization;

namespace Host.Model.Data
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public DateTime Time { get; set; }
    }
}
