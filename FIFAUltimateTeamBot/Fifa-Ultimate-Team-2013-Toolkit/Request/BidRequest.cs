using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class BidRequest : RequestBase
    {
        public async Task<AuctionResponse> PlaceBid(AuctionInfo auctionInfo, uint bidAmount)
        {
            var uriString = string.Format("https://utas.s2.fut.ea.com/ut/game/fifa13/trade/{0}/bid", auctionInfo.TradeId);
            var uri = new Uri(uriString);
            var content = new StringContent(string.Format("{{\"bid\":{0}}}", bidAmount), Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = content };

            requestMessage.Headers.TryAddWithoutValidation("X-Ut-Sid", SessionId);
            requestMessage.Headers.TryAddWithoutValidation("x-http-method-override", "PUT");

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            var auctionResponse = JsonDeserializer.Deserialize<AuctionResponse>(await response.Content.ReadAsStreamAsync());

            return auctionResponse;
        }
    }
}