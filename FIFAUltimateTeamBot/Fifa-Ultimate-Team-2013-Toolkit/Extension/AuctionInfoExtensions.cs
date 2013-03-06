using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Extension
{
    public static class AuctionInfoExtensions
    {
        public static uint CalculateBid(this AuctionInfo auctionInfo)
        {
            var currentPrice = auctionInfo.CurrentPrice == 0 ? auctionInfo.StartingBid : auctionInfo.CurrentPrice;

            if (currentPrice < 1000)
                return currentPrice + 50;

            if (currentPrice < 10000)
                return currentPrice + 100;

            if (currentPrice < 50000)
                return currentPrice + 250;

            if (currentPrice < 100000)
                return currentPrice + 500;

            return currentPrice + 1000;
        }
    }
}