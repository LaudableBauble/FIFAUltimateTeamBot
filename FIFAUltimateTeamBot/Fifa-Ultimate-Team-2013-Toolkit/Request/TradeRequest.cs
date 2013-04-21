using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class TradeRequest : RequestBase
    {
        public async Task<AuctionResponse> GetTradeStatuses(IEnumerable<long> tradeIds)
        {
            var uriString = string.Format("https://utas.s2.fut.ea.com/ut/game/fifa13/trade?tradeIds={0}", string.Join("%2C", tradeIds));
            uriString = string.Format("https://utas.fut.ea.com/ut/game/fifa13/trade?tradeIds={0}", string.Join("%2C", tradeIds));

            var uri = new Uri(uriString);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = new StringContent(" ") };

            requestMessage.Headers.TryAddWithoutValidation("X-UT-SID", SessionId);
            requestMessage.Headers.TryAddWithoutValidation("X-HTTP-Method-Override", "GET");
            requestMessage.Headers.TryAddWithoutValidation("X-UT-Embed-Error", "true");

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            return await Deserialize<AuctionResponse>(response);
        }
        /// <summary>
        /// Get all items in the trade pile.
        /// </summary>
        /// <returns></returns>
        public async Task<AuctionResponse> GetTradePile()
        {
            var uriString = "https://utas.fut.ea.com/ut/game/fifa13/tradepile";

            var uri = new Uri(uriString);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = new StringContent(" ") };

            requestMessage.Headers.TryAddWithoutValidation("X-UT-SID", SessionId);
            requestMessage.Headers.TryAddWithoutValidation("X-HTTP-Method-Override", "GET");
            requestMessage.Headers.TryAddWithoutValidation("X-UT-Embed-Error", "true");

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            return await Deserialize<AuctionResponse>(response);
        }
        /// <summary>
        /// Get all items in the watch list.
        /// </summary>
        /// <returns></returns>
        public async Task<AuctionResponse> GetWatchList()
        {
            var uriString = "https://utas.fut.ea.com/ut/game/fifa13/watchlist";

            var uri = new Uri(uriString);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = new StringContent(" ") };

            requestMessage.Headers.TryAddWithoutValidation("X-UT-SID", SessionId);
            requestMessage.Headers.TryAddWithoutValidation("X-HTTP-Method-Override", "GET");
            requestMessage.Headers.TryAddWithoutValidation("X-UT-Embed-Error", "true");

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            return await Deserialize<AuctionResponse>(response);
        }
        /// <summary>
        /// Get all items in the watch list.
        /// </summary>
        /// <returns></returns>
        public async Task<ItemDataResponse> GetUnassignedItems()
        {
            var uriString = "https://utas.fut.ea.com/ut/game/fifa13/purchased/items";

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uriString) { Content = new StringContent(" ") };

            requestMessage.Headers.TryAddWithoutValidation("X-UT-SID", SessionId);
            requestMessage.Headers.TryAddWithoutValidation("X-HTTP-Method-Override", "GET");
            requestMessage.Headers.TryAddWithoutValidation("X-UT-Embed-Error", "true");

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            return await Deserialize<ItemDataResponse>(response);
        }
        /// <summary>
        /// Get all player items in the club.
        /// </summary>
        /// <returns></returns>
        public async Task<ItemDataResponse> GetClubPlayerItems(int count)
        {
            var uriString = string.Format("https://utas.fut.ea.com/ut/game/fifa13/club?count={0}&type=1&level=10&start=0", count);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uriString) { Content = new StringContent(" ") };

            requestMessage.Headers.TryAddWithoutValidation("X-UT-SID", SessionId);
            requestMessage.Headers.TryAddWithoutValidation("X-HTTP-Method-Override", "GET");
            requestMessage.Headers.TryAddWithoutValidation("X-UT-Embed-Error", "true");

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            return await Deserialize<ItemDataResponse>(response);
        }
        /// <summary>
        /// Move item to trade pile.
        /// </summary>
        public async Task MoveToTradePile(long playerId)
        {
            if (playerId == 0) { throw new ArgumentNullException("Invalid player id!"); }

            var uri = "https://utas.fut.ea.com/ut/game/fifa13/item";

            var content = new StringContent(string.Format("{{\"itemData\":[{{\"pile\":\"trade\",\"id\":\"{0}\"}}]}}",
                playerId), Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = content };

            requestMessage.Headers.TryAddWithoutValidation("X-UT-SID", SessionId);
            requestMessage.Headers.TryAddWithoutValidation("X-HTTP-Method-Override", "PUT");
            requestMessage.Headers.TryAddWithoutValidation("X-UT-Embed-Error", "true");

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
        }
        /// <summary>
        /// Move item to trade pile.
        /// </summary>
        public async Task MoveToTradePile(AuctionInfo auctionInfo)
        {
            if (auctionInfo.TradeId == 0) { throw new ArgumentNullException("Invalid trade id!"); }

            var uri = "https://utas.fut.ea.com/ut/game/fifa13/item";

            var content = new StringContent(string.Format("{{\"itemData\":[{{\"tradeId\":{0},\"pile\":\"trade\",\"id\":\"{1}\"}}]}}",
                auctionInfo.TradeId, auctionInfo.ItemData.Id), Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = content };

            requestMessage.Headers.TryAddWithoutValidation("X-UT-SID", SessionId);
            requestMessage.Headers.TryAddWithoutValidation("X-HTTP-Method-Override", "PUT");
            requestMessage.Headers.TryAddWithoutValidation("X-UT-Embed-Error", "true");

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
        }
        /// <summary>
        /// Remove item from trade pile.
        /// </summary>
        public async Task RemoveFromTradePile(long tradeId)
        {
            var uriString = string.Format("https://utas.fut.ea.com/ut/game/fifa13/trade/{0}", tradeId);

            var uri = new Uri(uriString);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = new StringContent(" ") };

            requestMessage.Headers.TryAddWithoutValidation("X-UT-SID", SessionId);
            requestMessage.Headers.TryAddWithoutValidation("X-HTTP-Method-Override", "DELETE");
            requestMessage.Headers.TryAddWithoutValidation("X-UT-Embed-Error", "true");

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
        }
    }
}