using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class AuctionInfo
    {
        [DataMember(Name = "bidState")]
        public string BidState { get; set; }

        [DataMember(Name = "buyNowPrice")]
        public uint BuyNowPrice { get; set; }

        [DataMember(Name = "currentBid")]
        public uint CurrentPrice { get; set; }

        [DataMember(Name = "expires")]
        public int Expires { get; set; }

        [DataMember(Name = "itemData")]
        public ItemData ItemData { get; set; }

        [DataMember(Name = "offers")]
        public uint Offers { get; set; }

        [DataMember(Name = "sellerEstablished")]
        public string SellerEstablished { get; set; }

        [DataMember(Name = "sellerId")]
        public uint SellerId { get; set; }

        [DataMember(Name = "sellerName")]
        public string SellerName { get; set; }

        [DataMember(Name = "startingBid")]
        public uint StartingBid { get; set; }

        [DataMember(Name = "tradeId")]
        public long TradeId { get; set; }

        [DataMember(Name = "tradeState")]
        public string TradeState { get; set; }

        //public string Watched { get; set; }
    }
}