using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constant;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class BidRequest : RequestBase
    {
        public async Task<AuctionResponse> PlaceBid(AuctionInfo auctionInfo, uint bidAmount)
        {
            var content = string.Format("{{\"bid\":{0}}}", bidAmount);
            var uriString = string.Format(Resources.Bid, auctionInfo.TradeId);
            var response = await Client.SendAsync(CreateRequestMessage(content, uriString, HttpMethod.Put));
            response.EnsureSuccessStatusCode();

            return await Deserialize<AuctionResponse>(response);
        }
    }
}