using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Model;
using UltimateTeam.Toolkit.Request;
using UltimateTeam.Toolkit.Parameter;
using System.Collections.Concurrent;

namespace FIFAUltimateTeamBot
{
    /// <summary>
    /// This is where the logic of the FUT bot resides. All decisions are taken from here.
    /// </summary>
    public static class LogicManager
    {
        #region Fields
        private static bool _IsActive;
        #endregion

        #region Methods
        /// <summary>
        /// The update loop of the logic manager. This method is called at initialize and carried on throughout the whole process.
        /// The logic manager cannot function properly without this running.
        /// </summary>
        private async static void Update()
        {
            //Stop here if the logic manager is not active.
            if (!_IsActive) { return; }

            //Login and load all items. It is important that the first request is to login.
            RequestManager.Login();
            RequestManager.LoadTradePile();
            RequestManager.LoadWatchList();
            RequestManager.LoadUnassignedItems();
            RequestManager.LoadClubItems();

            //While the logic manager is active.
            while (_IsActive)
            {
                //Manage the the list of trade items.
                DataManager.RemoveAll(item => item.Remove && !item.IsLocked);

                //Look for items that need to be updated and do so.
                if (DataManager.Any(item => item.Update && !item.IsLocked)) { RequestManager.UpdateItems(DataManager.Where(item => !item.IsLocked).ToList()); }

                //Go through all trade items of interest (ie. those in the TradeItems list) and see if they need to be updated.
                DataManager.Where(item => IsItTimeToUpdate(item) && item.Location == TradeItemLocation.TradePile
                    && !item.IsLocked).ToList().ForEach(item => item.Update = true);

                //Look for expired items in the trade pile.
                var isExpired = DataManager.Where(item => item.Location == TradeItemLocation.TradePile && item.AuctionInfo.Expires <= 0 && !item.IsLocked);
                if (isExpired.Count() > 0)
                {
                    //Remove all sold items from the trade pile.
                    var toRemove = isExpired.Where(item => item.AuctionInfo.CurrentPrice != 0);
                    RequestManager.RemoveItemsFromTradePile(toRemove.ToList());

                    //Relist all expired items that did not get sold. (Make sure that new items are sold for an appropriate sum).
                    var toRelist = isExpired.Where(item => item.AuctionInfo.CurrentPrice == 0);
                    foreach (PlayerItem item in toRelist.Where(item => item.AuctionInfo.StartingBid <= 150)) { item.PrepareForAuction(); }
                    RequestManager.SellItems(toRelist.ToList());
                }

                //If there is less than 40 items up for sale in the trade pile, try to add some from the watchlist or unassigned items.
                if (DataManager.Count(item => item.Location == TradeItemLocation.TradePile) < 40)
                {
                    //Get the watch list and choose which of them to move to the trade pile.
                    var watchList = DataManager.Where(item => item.Location != TradeItemLocation.TradePile && item.IsAllowedToBeSold && !item.IsLocked);
                    RequestManager.MoveItems(watchList.Take(40 - DataManager.Count(item => item.Location == TradeItemLocation.TradePile)).ToList());
                }

                //Handle all stockpiled requests.
                await RequestManager.HandleRequestsAsync();
            }
        }

        /// <summary>
        /// Check if a player trade item needs to be updated or not.
        /// </summary>
        /// <param name="tradeItem">The trade item in question.</param>
        /// <returns>Whether the trade item needs to be updated or not.</returns>
        private static bool IsItTimeToUpdate(PlayerItem tradeItem)
        {
            //Get the difference in time since the item was last updated.
            TimeSpan sinceLastUpdate = DateTime.Now.Subtract(tradeItem.LastUpdated);

            //The remaining time for the trade item in seconds.
            int remainingTime = tradeItem.AuctionInfo.Expires;

            //Depending on how much time the trade item has remaining until it expires, decide whether it should be updated now or not.
            if (remainingTime > 0)
            {
                /* Update every:
                 * - 2 minutes when over two minutes left.
                 * - 5 seconds when less 30 seconds left.
                 * - 2 seconds when less than 15 seconds left.
                 */
                if (remainingTime > 120 && sinceLastUpdate.TotalSeconds > 120) { return true; }
                if (remainingTime > 15 && remainingTime < 120 && sinceLastUpdate.TotalSeconds > 5) { return true; }
                if (remainingTime < 15 && sinceLastUpdate.TotalSeconds > 2) { return true; }
            }

            //Default behaviour.
            return false;
        }
        /// <summary>
        /// Start the logic manager.
        /// </summary>
        public static void Start()
        {
            //If the logic manager is inactive, start it.
            if (!_IsActive)
            {
                //The logic manager is now active.
                _IsActive = true;

                //Start churning the butter.
                ThreadPool.QueueUserWorkItem(obj => Update());
            }
        }
        #endregion

        #region Properties
        #endregion
    }
}
