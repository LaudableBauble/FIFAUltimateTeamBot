using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class Attribute
    {
        [DataMember(Name = "index")]
        public uint Index { get; set; }

        [DataMember(Name = "value")]
        public uint Value { get; set; }
    }
}