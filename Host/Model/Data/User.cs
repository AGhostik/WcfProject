using System;
using System.Runtime.Serialization;
using System.Text;

namespace Host.Model.Data
{
    [DataContract]
    public class User
    {
        [DataMember] public string Name { get; set; }

        [DataMember] public string Id { get; set; }

        [DataMember] public string Password { get; set; }

        [DataMember] public string Group { get; set; }
    }
}