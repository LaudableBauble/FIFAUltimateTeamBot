using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    internal class Shard
    {
        [DataMember(Name = "clientFacingIpPort")]
        public string ClientFacingIpPort { get; set; }

        [DataMember(Name = "clientProtocol")]
        public string ClientProtocol { get; set; }

        [DataMember(Name = "content")]
        public List<string> Content { get; set; }

        [DataMember(Name = "customdata1")]
        public List<string> CustomData1 { get; set; }

        [DataMember(Name = "platforms")]
        public List<string> Platforms { get; set; }

        [DataMember(Name = "shardId")]
        public string ShardId { get; set; }
    }
}