using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class AuctionResponse
    {
        [DataMember(Name = "auctionInfo")]
        public List<AuctionInfo> AuctionInfo { get; set; }

        [DataMember(Name = "bidTokens")]
        public BidTokens BidTokens { get; set; }

        [DataMember(Name = "credits")]
        public uint Credits { get; set; }

        [DataMember(Name = "currencies")]
        public List<Currency> Currencies { get; set; }
    }
}