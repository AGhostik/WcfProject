using System;
using System.Runtime.Serialization;

namespace Host.Model.Data
{
    [DataContract]
    public class Message
    {
        [DataMember] public string Content { get; set; }

        [DataMember] public string Author { get; set; }

        [DataMember] public DateTime Time { get; set; }

        public override string ToString()
        {
            return $"Author: {Author}{Environment.NewLine}Time: {Time}{Environment.NewLine}Content: {Content}";
        }
    }
}