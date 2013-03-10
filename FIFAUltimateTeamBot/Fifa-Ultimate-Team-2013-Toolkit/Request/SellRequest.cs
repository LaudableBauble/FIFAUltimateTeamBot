using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class SellRequest : RequestBase
    {
        public async Task<SellResponse> SellItem(AuctionInfo auctionInfo)
        {
            //Make sure you are able to sell the item, ie. check for permission and if it's expired.
            if (auctionInfo.Expires != -1) { throw new ArgumentNullException("Item has not expired!"); }
            if (auctionInfo.SellerEstablished != RequestBase.Persona.UserClubList[0].Established && !auctionInfo.BidState.Equals("highest"))
            {
                throw new ArgumentException("You must be the owner!");
            }

            var uri = "https://utas.fut.ea.com/ut/game/fifa13/auctionhouse";
            var content = new StringContent(string.Format("{{\"itemData\":{{\"id\":{0}}},\"buyNowPrice\":{1},\"duration\":{2},\"startingBid\":{3}}}",
            auctionInfo.ItemData.Id, auctionInfo.BuyNowPrice, "3600", auctionInfo.StartingBid), Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = content };

            requestMessage.Headers.TryAddWithoutValidation("X-UT-SID", SessionId);
            requestMessage.Headers.TryAddWithoutValidation("X-HTTP-Method-Override", "POST");

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            var sellResponse = new SellResponse();
            var stream = await response.Content.ReadAsStreamAsync();

            try { sellResponse = JsonDeserializer.Deserialize<SellResponse>(stream); }
            catch (Exception) { throw new RequestException(JsonDeserializer.Deserialize<ErrorResponse>(stream)); }

            return sellResponse;
        }
    }
}