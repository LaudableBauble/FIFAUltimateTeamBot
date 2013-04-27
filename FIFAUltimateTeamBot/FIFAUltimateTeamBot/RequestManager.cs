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
using System.Windows.Forms;

namespace FIFAUltimateTeamBot
{
    public static class RequestManager
    {
        #region Fields
        private static bool _IsLoggedIn;
        private static LinkedList<Func<Task>> _RequestQueue;

        public delegate void TradeItemsEventHandler(List<PlayerItem> tradeItems);
        public delegate void CreditsEventHandler(CreditsResponse credits);
        public static event TradeItemsEventHandler OnLoadTradePile;
        public static event TradeItemsEventHandler OnLoadWatchList;
        public static event TradeItemsEventHandler OnLoadUnassigned;
        public static event TradeItemsEventHandler OnLoadClub;
        public static event TradeItemsEventHandler OnUpdateItems;
        public static event TradeItemsEventHandler OnMoveItems;
        public static event TradeItemsEventHandler OnAuctionItems;
        public static event TradeItemsEventHandler OnSearchItems;
        public static event TradeItemsEventHandler OnRemoveItems;
        public static event CreditsEventHandler OnLoadCredits;
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the request manager.
        /// </summary>
        public static void Initialize()
        {
            //Store the user information.
            _IsLoggedIn = false;
            _RequestQueue = new LinkedList<Func<Task>>();
        }

        /// <summary>
        /// Handle all stockpiled request tasks asynchronously and serially.
        /// </summary>
        public async static Task HandleRequestsAsync()
        {
            //While there is still requests in the queue, keep going.
            while (_RequestQueue.Count != 0)
            {
                //Start the first method and then remove it from the queue.
                try
                {
                    await _RequestQueue.First()();
                    _RequestQueue.RemoveFirst();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(RequestException))
                    {
                        //Depending on the error, do different things.
                        switch ((e as RequestException).Error.Reason)
                        {
                            default:
                                {
                                    //An exception occurred, try to fix it by login in again.
                                    _RequestQueue.AddFirst(() => LoginAsync());
                                    break;
                                }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Login to Ultimate Team.
        /// </summary>
        public static void Login()
        {
            //Add the login request to the request queue.
            _RequestQueue.AddLast(() => LoginAsync());
        }
        /// <summary>
        /// Load all auction items in the trade pile.
        /// </summary>
        public static void LoadTradePile()
        {
            //Add the load trade pile request to the request queue.
            _RequestQueue.AddLast(() => LoadTradePileAsync());
        }
        /// <summary>
        /// Remove the specified trade items from the trade pile.
        /// </summary>
        /// <param name="tradeItems">The trade items to remove.</param>
        /// <returns></returns>
        public static void RemoveItemsFromTradePile(List<PlayerItem> tradeItems)
        {
            //Validation check.
            if (!_IsLoggedIn) { throw new ArgumentException("You are not logged in yet!"); }
            if (tradeItems == null) { throw new ArgumentNullException("The list of trade items cannot be null!"); }
            if (tradeItems.Count() == 0) { return; }

            //Lock the items and schedule them for removal.
            tradeItems.ForEach(item => { item.IsLocked = true; item.Remove = true; });

            //Remove the trade items from the trade pile.
            tradeItems.ForEach(item =>
            {
                if (item.AuctionInfo.TradeId != 0) { _RequestQueue.AddLast(() => new TradeRequest().RemoveFromTradePile(item.AuctionInfo.TradeId)); }
            });

            //Notify all interested parties.
            RemoveItemsEventInvoke(tradeItems);
        }
        /// <summary>
        /// Load all auction items in the watch list.
        /// </summary>
        public static void LoadWatchList()
        {
            //Add the load watch list request to the request queue.
            _RequestQueue.AddLast(() => LoadWatchListAsync());
        }
        /// <summary>
        /// Load all unassigned items.
        /// </summary>
        public static void LoadUnassignedItems()
        {
            _RequestQueue.AddLast(() => LoadUnassignedAsync());
        }
        /// <summary>
        /// Load all club items.
        /// </summary>
        public static void LoadClubItems()
        {
            _RequestQueue.AddLast(() => LoadClubAsync());
        }
        /// <summary>
        /// Update the specified trade items by refreshing their auction info and item data.
        /// </summary>
        public static void UpdateItems(List<PlayerItem> tradeItems)
        {
            //Validation check.
            if (tradeItems == null) { throw new ArgumentNullException("The list of trade items cannot be null!"); }
            if (tradeItems.Count() == 0) { return; }

            //Lock the items.
            tradeItems.ForEach(item => item.IsLocked = true);

            //Add the load trade pile request to the request queue.
            _RequestQueue.AddLast(() => LoadItemsAsync(tradeItems));
        }
        /// <summary>
        /// Move all specified trade items to the trade pile.
        /// </summary>
        public static void MoveItems(List<PlayerItem> tradeItems)
        {
            //Validation check.
            if (tradeItems == null) { throw new ArgumentNullException("The list of trade items cannot be null!"); }
            if (tradeItems.Count() == 0) { return; }

            //Lock the items.
            tradeItems.ForEach(item => item.IsLocked = true);

            //Add the move items request to the request queue.
            _RequestQueue.AddLast(() => MoveItemsAsync(tradeItems));
        }
        /// <summary>
        /// Auction all specified trade items.
        /// </summary>
        public static void AuctionItems(List<PlayerItem> tradeItems)
        {
            //Validation check.
            if (tradeItems == null) { throw new ArgumentNullException("The list of trade items cannot be null!"); }
            if (tradeItems.Count() == 0) { return; }

            //Lock the items.
            tradeItems.ForEach(item => item.IsLocked = true);

            //Add the auction items request to the request queue.
            _RequestQueue.AddLast(() => AuctionItemsAsync(tradeItems));
        }
        /// <summary>
        /// Search for some items.
        /// </summary>
        /// <param name="searchParameters"></param>
        public static void SearchItems(PlayerSearchParameters searchParameters)
        {
            //Validation check.
            if (!_IsLoggedIn) { throw new ArgumentException("You have not logged in yet!"); }
            if (searchParameters == null) { throw new ArgumentException("The search parameters must not be null!"); }

            //Add the search request to the request queue.
            _RequestQueue.AddLast(() => SearchItemsAsync(searchParameters));
        }
        /// <summary>
        /// Get amount of credits and unopened packs.
        /// </summary>
        public static void LoadCredits()
        {
            _RequestQueue.AddLast(() => LoadCreditsAsync());
        }

        /// <summary>
        /// Login to Ultimate Team.
        /// </summary>
        private async static Task LoginAsync()
        {
            //Validation check.
            if (string.IsNullOrWhiteSpace(DataManager.LoginCredentials.Username)) { throw new ArgumentException("The username/email is not valid!"); }
            if (string.IsNullOrWhiteSpace(DataManager.LoginCredentials.Password)) { throw new ArgumentException("The password is not valid!"); }
            if (string.IsNullOrWhiteSpace(DataManager.LoginCredentials.SecretAnswerHash)) { throw new ArgumentException("The secret answer is not valid!"); }

            //Try to login.
            try
            {
                var loginRequest = new LoginRequest();
                await loginRequest.LoginAsync(DataManager.LoginCredentials.Username, DataManager.LoginCredentials.Password, DataManager.LoginCredentials.SecretAnswerHash);
                _IsLoggedIn = true;
            }
            catch (Exception e)
            {
                _IsLoggedIn = false;
                MessageBox.Show("Login Request\n\nException: " + e.InnerException.ToString() + "\nMessage: " + e.Message + "\n\nStack Trace:\n" + e.StackTrace);
            }
        }
        /// <summary>
        /// Update the specified trade items by refreshing their auction info and item data.
        /// </summary>
        /// <param name="tradeItems">The trade items to update.</param>
        /// <returns></returns>
        private async static Task LoadItemsAsync(List<PlayerItem> tradeItems)
        {
            //Validation check.
            if (!_IsLoggedIn) { throw new ArgumentException("You are not logged in yet!"); }
            if (tradeItems == null) { throw new ArgumentNullException("The list of trade items cannot be null!"); }
            if (tradeItems.Count() == 0) { return; }

            //Get the refreshed auction info and item data and update all the trade items.
            var tradeIds = tradeItems.FindAll(xItem => xItem.AuctionInfo.TradeId != 0).Select(yItem => yItem.AuctionInfo.TradeId);
            if (tradeIds.Count() == 0) { return; }
            var response = await new TradeRequest().GetTradeStatuses(tradeIds);

            response.AuctionInfo.ForEach(async auction =>
            {
                DataManager.AddOrUpdate(auction);
                //If the item's resource data has not been loaded yet, do so.
                if (!DataManager.ResourceDataExists(auction.ItemData.ResourceId))
                {
                    DataManager.AddOrUpdate(await new ItemRequest().GetItemAsync(auction.ItemData.ResourceId), auction.ItemData.ResourceId);
                }
            });

            //Notify all interested parties.
            UpdateItemsEventInvoke(tradeItems);
        }
        /// <summary>
        /// Load all auction items in the trade pile.
        /// </summary>
        private async static Task LoadTradePileAsync()
        {
            try
            {
                //Validation check.
                if (!_IsLoggedIn) { throw new ArgumentException("You are not logged in yet!"); }

                //Get the trade pile auction info.
                var response = await new TradeRequest().GetTradePile();
                List<AuctionInfo> auctions = response.AuctionInfo;

                //If no items were found, stop here.
                if (auctions.Count == 0) { return; }

                //Get the player info and add to the trade pile.
                foreach (AuctionInfo auction in response.AuctionInfo)
                {
                    //Save the auction data.
                    var item = DataManager.AddOrUpdate(auction);

                    //If the item's resource data has not been loaded yet, do so.
                    if (!DataManager.ResourceDataExists(auction.ItemData.ResourceId))
                    {
                        DataManager.AddOrUpdate(await new ItemRequest().GetItemAsync(auction.ItemData.ResourceId), auction.ItemData.ResourceId);
                    }

                    item.Location = TradeItemLocation.TradePile;
                    item.IsAllowedToBeSold = true;
                }

                //Notify all interested parties.
                LoadTradePileEventInvoke(DataManager.Where(item => auctions.Any(auction => auction == item.AuctionInfo)).ToList());
            }
            catch (Exception e)
            {
                MessageBox.Show("Trade Pile Request\n\nException: " + e.InnerException.ToString() + "\nMessage: " + e.Message + "\n\nStack Trace:\n" + e.StackTrace);
            }
        }
        /// <summary>
        /// Load all trade items in the watch pile.
        /// </summary>
        private async static Task LoadWatchListAsync()
        {
            //Validation check.
            if (!_IsLoggedIn) { throw new ArgumentException("You are not logged in yet!"); }

            //Get the watch list auction info.
            var response = await new TradeRequest().GetWatchList();
            List<AuctionInfo> auctions = response.AuctionInfo;

            //If no items were found, stop here.
            if (auctions.Count == 0) { return; }

            //Get the player info and add to the watch list.
            foreach (AuctionInfo auction in response.AuctionInfo)
            {
                //Save the auction data.
                var item = DataManager.AddOrUpdate(auction);

                //If the item's resource data has not been loaded yet, do so.
                if (!DataManager.ResourceDataExists(auction.ItemData.ResourceId))
                {
                    DataManager.AddOrUpdate(await new ItemRequest().GetItemAsync(auction.ItemData.ResourceId), auction.ItemData.ResourceId);
                }

                item.Location = TradeItemLocation.WatchList;
            }

            //Notify all interested parties.
            LoadWatchListEventInvoke(DataManager.Where(item => auctions.Any(auction => auction == item.AuctionInfo)).ToList());
        }
        /// <summary>
        /// Load all unassigned items.
        /// </summary>
        private async static Task LoadUnassignedAsync()
        {
            //Validation check.
            if (!_IsLoggedIn) { throw new ArgumentException("You are not logged in yet!"); }

            //Get the item data.
            var response = await new TradeRequest().GetUnassignedItems();
            var itemData = response.ItemData;

            //If no items were found, stop here.
            if (itemData.Count == 0) { return; }

            //Get the player info.
            foreach (var data in itemData)
            {
                //Save the item data.
                var item = DataManager.AddOrUpdate(data);

                //If the item's resource data has not been loaded yet, do so.
                if (!DataManager.ResourceDataExists(data.ResourceId))
                {
                    DataManager.AddOrUpdate(await new ItemRequest().GetItemAsync(data.ResourceId), data.ResourceId);
                }

                item.Location = TradeItemLocation.Unassigned;
                item.AuctionInfo.SellerEstablished = "-1";
            }

            //Notify all interested parties.
            LoadUnassignedEventInvoke(DataManager.Where(item => itemData.Any(data => data == item.AuctionInfo.ItemData)).ToList());
        }
        /// <summary>
        /// Load all player items in the club.
        /// </summary>
        private async static Task LoadClubAsync()
        {
            //Validation check.
            if (!_IsLoggedIn) { throw new ArgumentException("You are not logged in yet!"); }

            //Get the club item data.
            var response = await new TradeRequest().GetClubPlayerItems(200);
            var itemData = response.ItemData;

            //If no items were found, stop here.
            if (itemData.Count == 0) { return; }

            //Get the player info.
            foreach (var data in itemData)
            {
                //Save the item data.
                var item = DataManager.AddOrUpdate(data);

                //If the item's resource data has not been loaded yet, do so.
                if (!DataManager.ResourceDataExists(data.ResourceId))
                {
                    DataManager.AddOrUpdate(await new ItemRequest().GetItemAsync(data.ResourceId), data.ResourceId);
                }

                item.Location = TradeItemLocation.Club;
                item.AuctionInfo.SellerEstablished = "-1";
            }

            //Notify all interested parties.
            LoadClubEventInvoke(DataManager.Where(item => itemData.Any(data => data == item.AuctionInfo.ItemData)).ToList());
        }
        /// <summary>
        /// Move the specified trade items to the trade pile.
        /// </summary>
        /// <param name="tradeItems">The trade items to move.</param>
        private async static Task MoveItemsAsync(IEnumerable<PlayerItem> tradeItems)
        {
            //Validation check.
            if (!_IsLoggedIn) { throw new ArgumentException("You have not logged in yet!"); }

            //For each given item, try to move it and then reset their auction info.
            foreach (PlayerItem item in tradeItems)
            {
                await new TradeRequest().MoveToTradePile(item.AuctionInfo.ItemData.Id);
                item.ResetAuctionInfo();
            }

            //Notify all interested parties.
            MoveItemsEventInvoke(tradeItems.ToList());
        }
        /// <summary>
        /// Auction the specified trade items.
        /// </summary>
        /// <param name="auctions">The trade items to auction.</param>
        private async static Task AuctionItemsAsync(IEnumerable<PlayerItem> auctions)
        {
            //Validation check.
            if (!_IsLoggedIn) { throw new ArgumentException("You have not logged in yet!"); }
            if (auctions.Any(item => !item.IsAllowedToBeSold)) { throw new ArgumentException("One of the items are not allowed to be sold!"); }

            //For each given auction, try to auction it.
            foreach (PlayerItem auction in auctions)
            {
                var response = await new ListAuctionRequest().AuctionItem(auction.AuctionInfo);
                auction.AuctionInfo.TradeId = response.TradeId;
            }

            //Notify all interested parties.
            AuctionItemsEventInvoke(auctions.ToList());
        }
        /// <summary>
        /// Search for players on the global auction.
        /// </summary>
        /// <returns></returns>
        private async static Task SearchItemsAsync(PlayerSearchParameters searchParameters)
        {
            //Validation check.
            if (!_IsLoggedIn) { throw new ArgumentException("You have not logged in yet!"); }
            if (searchParameters == null) { throw new ArgumentException("The search parameters must not be null!"); }

            //Search for players.
            var response = await new SearchRequest().SearchAsync(searchParameters);
            List<AuctionInfo> auctions = response.AuctionInfo;

            //If no items were found, stop here.
            if (auctions.Count == 0) { return; }

            foreach (AuctionInfo auction in response.AuctionInfo)
            {
                //Save the auction data.
                var item = DataManager.AddOrUpdate(auction);

                //If the item's resource data has not been loaded yet, do so.
                if (!DataManager.ResourceDataExists(auction.ItemData.ResourceId))
                {
                    DataManager.AddOrUpdate(await new ItemRequest().GetItemAsync(auction.ItemData.ResourceId), auction.ItemData.ResourceId);
                }

                item.Location = TradeItemLocation.Auction;
            }

            //Notify all interested parties.
            SearchItemsEventInvoke(DataManager.Where(item => auctions.Any(auction => auction == item.AuctionInfo)).ToList());
        }
        /// <summary>
        /// Get amount of credits and unopened packs.
        /// </summary>
        private async static Task LoadCreditsAsync()
        {
            try
            {
                //Validation check.
                if (!_IsLoggedIn) { throw new ArgumentException("You are not logged in yet!"); }

                //Notify all interested parties.
                LoadCreditsEventInvoke(await new CreditsRequest().GetCreditsAsync());
            }
            catch (Exception e)
            {
                MessageBox.Show("Credits Request\n\nException: " + e.InnerException.ToString() + "\nMessage: " + e.Message + "\n\nStack Trace:\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// The trade pile has been loaded. Let the world know.
        /// </summary>
        private static void LoadTradePileEventInvoke(List<PlayerItem> tradeItems)
        {
            //Unlock all items and disable the update flag.
            tradeItems.ForEach(item => { item.IsLocked = false; item.Update = false; });

            if (OnLoadTradePile != null) { OnLoadTradePile(tradeItems); }
        }
        /// <summary>
        /// The watch list has been loaded. Let the world know.
        /// </summary>
        private static void LoadWatchListEventInvoke(List<PlayerItem> tradeItems)
        {
            //Unlock all items and disable the update flag.
            tradeItems.ForEach(item => { item.IsLocked = false; item.Update = false; });

            if (OnLoadWatchList != null) { OnLoadWatchList(tradeItems); }
        }
        /// <summary>
        /// The unassigned items has been loaded. Let the world know.
        /// </summary>
        private static void LoadUnassignedEventInvoke(List<PlayerItem> tradeItems)
        {
            //Unlock all items and disable the update flag.
            tradeItems.ForEach(item => { item.IsLocked = false; item.Update = false; });

            if (OnLoadUnassigned != null) { OnLoadUnassigned(tradeItems); }
        }
        /// <summary>
        /// The items in the club has been loaded. Let the world know.
        /// </summary>
        private static void LoadClubEventInvoke(List<PlayerItem> tradeItems)
        {
            //Unlock all items and disable the update flag.
            tradeItems.ForEach(item => { item.IsLocked = false; item.Update = false; });

            if (OnLoadClub != null) { OnLoadClub(tradeItems); }
        }
        /// <summary>
        /// Some trade items have seen their auction info and item data updated. Let the world know.
        /// </summary>
        private static void UpdateItemsEventInvoke(List<PlayerItem> tradeItems)
        {
            //Unlock all items and disable the update flag.
            tradeItems.ForEach(item => { item.IsLocked = false; item.Update = false; });

            if (OnUpdateItems != null) { OnUpdateItems(tradeItems); }
        }
        /// <summary>
        /// Some trade items have been moved from the watch list to the trade pile. Let the world know.
        /// </summary>
        private static void MoveItemsEventInvoke(List<PlayerItem> tradeItems)
        {
            //Unlock all items.
            tradeItems.ForEach(item => item.IsLocked = false);

            if (OnMoveItems != null) { OnMoveItems(tradeItems); }
        }
        /// <summary>
        /// Some trade items have been auctioned. Let the world know.
        /// </summary>
        private static void AuctionItemsEventInvoke(List<PlayerItem> tradeItems)
        {
            //Unlock the items and force them to update.
            tradeItems.ForEach(item => { item.Update = true; item.IsLocked = false; });

            if (OnAuctionItems != null) { OnAuctionItems(tradeItems); }
        }
        /// <summary>
        /// A search for players has been done. Let the world know.
        /// </summary>
        private static void SearchItemsEventInvoke(List<PlayerItem> tradeItems)
        {
            if (OnSearchItems != null) { OnSearchItems(tradeItems); }
        }
        /// <summary>
        /// Some trade items have been sold and subsequently removed from the trade pile. Let the world know.
        /// </summary>
        private static void RemoveItemsEventInvoke(List<PlayerItem> tradeItems)
        {
            //Unlock the items and force them to update.
            tradeItems.ForEach(item => { item.Update = true; item.IsLocked = false; });

            if (OnRemoveItems != null) { OnRemoveItems(tradeItems); }
        }
        /// <summary>
        /// The amount of credits and unopened packs have been loaded. Let the world know.
        /// </summary>
        private static void LoadCreditsEventInvoke(CreditsResponse credits)
        {
            if (OnLoadCredits != null) { OnLoadCredits(credits); }
        }
        #endregion

        #region Properties
        #endregion
    }
}
