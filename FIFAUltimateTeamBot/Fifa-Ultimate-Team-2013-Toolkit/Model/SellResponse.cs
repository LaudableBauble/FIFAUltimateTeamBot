using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class SellResponse
    {
        [DataMember(Name = "id")]
        public long TradeId { get; set; }
    }
}