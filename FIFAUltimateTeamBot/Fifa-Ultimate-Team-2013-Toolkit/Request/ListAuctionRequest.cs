using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constant;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class ListAuctionRequest : RequestBase
    {
        public ListAuctionRequest()
        {
            Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("sdch"));
        }

        public async Task<SellResponse> AuctionItem(AuctionInfo auctionInfo)
        {
            //Make sure you are able to sell the item, ie. check for permission and if it's expired.
            if (auctionInfo.Expires != -1) { throw new ArgumentNullException("Item has not expired!"); }
            if (auctionInfo.SellerEstablished != RequestBase.Persona.UserClubList[0].Established && !auctionInfo.BidState.Equals("highest"))
            {
                throw new ArgumentException("You must be the owner!");
            }

            var content = string.Format("{{\"itemData\":{{\"id\":{0}}},\"buyNowPrice\":{1},\"duration\":{2},\"startingBid\":{3}}}",
            auctionInfo.ItemData.Id, auctionInfo.BuyNowPrice, "3600", auctionInfo.StartingBid);

            var response = await Client.SendAsync(CreateRequestMessage(content, Resources.AuctionHouse, HttpMethod.Post));
            response.EnsureSuccessStatusCode();

            var sellResponse = new SellResponse();
            var stream = await response.Content.ReadAsStreamAsync();

            /*try { sellResponse = await Deserialize<SellResponse>(response); }
            catch (Exception) { throw new RequestException(await Deserialize<ErrorResponse>(response)); }*/

            return await Deserialize<SellResponse>(response);
        }
    }
}