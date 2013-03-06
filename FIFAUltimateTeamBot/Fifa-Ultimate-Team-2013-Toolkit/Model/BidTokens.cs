using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class BidTokens
    {
        [DataMember(Name = "count")]
        public uint Count { get; set; }

        [DataMember(Name = "updateTime")]
        public uint UpdateTime { get; set; }
    }
}