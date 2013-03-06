using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    internal class Shards
    {
        [DataMember(Name = "shardInfo")]
        public List<Shard> ShardInfo { get; set; }
    }
}