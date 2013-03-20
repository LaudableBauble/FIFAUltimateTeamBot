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
    public static class RequestManager
    {
        #region Fields
        private static bool _IsLoggedIn;
        private static bool _IsActive;
        private static List<PlayerItem> _TradeItems;
        private static LinkedList<Func<Task>> _RequestQueue;

        public delegate void TradeItemsEventHandler(List<PlayerItem> tradeItems);
        public static event TradeItemsEventHandler OnLoadTradePile;
        public static event TradeItemsEventHandler OnLoadWatchList;
        public static event TradeItemsEventHandler OnLoadUnassigned;
        public static event TradeItemsEventHandler OnLoadClub;
        public static event TradeItemsEventHandler OnUpdateItems;
        public static event TradeItemsEventHandler OnMoveItems;
        public static event TradeItemsEventHandler OnSellItems;
        public static event TradeItemsEventHandler OnSearchItems;
        public static event TradeItemsEventHandler OnRemoveItems;
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the request manager, something which includes trying to login.
        /// </summary>
        public static void Initialize()
        {
            //Store the user information.
            _IsLoggedIn = false;
            _IsActive = true;
            _TradeItems = new List<PlayerItem>();
            _RequestQueue = new LinkedList<Func<Task>>();

            //Login and get all items in the trade pile and watch list. It is important that the first request is to login.
            Login();
            LoadTradePile();
            LoadWatchList();
            LoadUnassignedItems();
            LoadClubItems();

            //Start handling requests.
            ThreadPool.QueueUserWorkItem(obj => Update());
        }
        /// <summary>
        /// The update loop of the request manager. This method is called at initialize and carried on throughout the whole process.
        /// The request manager cannot function properly without this running.
        /// </summary>
        private async static void Update()
        {
            //While the request manager is active.
            while (_IsActive)
            {
                //Manage the the list of trade items.
                _TradeItems.RemoveAll(item => item.Remove && !item.IsLocked);

                //Look for items that need to be updated and do so.
                if (_TradeItems.Any(item => item.Update && !item.IsLocked)) { UpdateItems(_TradeItems.Where(item => !item.IsLocked).ToList()); }

                //Go through all trade items of interest (ie. those in the TradeItems list) and see if they need to be updated.
                _TradeItems.Where(item => IsItTimeToUpdate(item) && item.Location == TradeItemLocation.TradePile
                    && !item.IsLocked).ToList().ForEach(item => item.Update = true);

                //Look for expired items in the trade pile.
                var isExpired = _TradeItems.Where(item => item.Location == TradeItemLocation.TradePile && item.AuctionInfo.Expires <= 0 && !item.IsLocked);
                if (isExpired.Count() > 0)
                {
                    //Remove all sold items from the trade pile.
                    var toRemove = isExpired.Where(item => item.AuctionInfo.CurrentPrice != 0);
                    RemoveItemsFromTradePile(toRemove.ToList());

                    //Relist all expired items that did not get sold. (Make sure that new items are sold for an appropriate sum).
                    var toRelist = isExpired.Where(item => item.AuctionInfo.CurrentPrice == 0);
                    foreach (PlayerItem item in toRelist.Where(item => item.AuctionInfo.StartingBid <= 150)) { item.PrepareForAuction(); }
                    SellItems(toRelist.ToList());
                }

                //If there is less than 40 items up for sale in the trade pile, try to add some from the watchlist or unassigned items.
                if (_TradeItems.Count(item => item.Location == TradeItemLocation.TradePile) < 40)
                {
                    //Get the watch list and choose which of them to move to the trade pile.
                    var watchList = _TradeItems.Where(item => item.Location != TradeItemLocation.TradePile && item.IsAllowedToBeSold && !item.IsLocked);
                    MoveItems(watchList.Take(40 - _TradeItems.Count(item => item.Location == TradeItemLocation.TradePile)).ToList());
                }

                //Handle all stockpiled requests.
                await HandleRequestsAsync();
            }
        }

        /// <summary>
        /// Handle all stockpiled request tasks asynchronously and serially.
        /// </summary>
        private async static Task HandleRequestsAsync()
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
                catch (RequestException e)
                {
                    //Depending on the error, do different things.
                    switch (e.Error.Reason)
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
                 * - 2 minutes when over two minuutes left.
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
        private static void RemoveItemsFromTradePile(List<PlayerItem> tradeItems)
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
        /// Sell all specified trade items.
        /// </summary>
        public static void SellItems(List<PlayerItem> tradeItems)
        {
            //Validation check.
            if (tradeItems == null) { throw new ArgumentNullException("The list of trade items cannot be null!"); }
            if (tradeItems.Count() == 0) { return; }

            //Lock the items.
            tradeItems.ForEach(item => item.IsLocked = true);

            //Add the sell items request to the request queue.
            _RequestQueue.AddLast(() => SellItemsAsync(tradeItems));
        }
        /// <summary>
        /// Search for some items.
        /// </summary>
        /// <param name="searchParameters"></param>
        public static void SearchItems(PlayerSearchParameters searchParameters)
        {
            //Validation check.
            if (!_IsLoggedIn) { throw new ArgumentException("You have not logged in yet!"); }
            if (searchParameters != null) { throw new ArgumentException("The search parameters must not be null!"); }

            //Add the search request to the request queue.
            _RequestQueue.AddLast(() => SearchItemsAsync(searchParameters));
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
            catch (Exception) { _IsLoggedIn = false; }
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
                PlayerItem tradeItem = tradeItems.First(item => item.AuctionInfo.TradeId == auction.TradeId);
                tradeItem.AuctionInfo = auction;
                if (tradeItem.ResourceData == null) { tradeItem.ResourceData = await new ItemRequest().GetItemAsync(auction.ItemData.ResourceId); }
            });

            //Notify all interested parties.
            UpdateItemsEventInvoke(tradeItems);
        }
        /// <summary>
        /// Load all auction items in the trade pile.
        /// </summary>
        private async static Task LoadTradePileAsync()
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
                //Get the player item if it has been loaded before.
                PlayerItem tradeItem = _TradeItems.Find(item => item.AuctionInfo.ItemData.Id == auction.ItemData.Id);

                //Get the item data.
                Item itemData = await new ItemRequest().GetItemAsync(auction.ItemData.ResourceId);

                //If the player item existed before, just update its auction info and item data. Otherwise create one and keep track of it.
                if (tradeItem != null)
                {
                    tradeItem.AuctionInfo = auction;
                    tradeItem.ResourceData = itemData;
                    tradeItem.Location = TradeItemLocation.TradePile;
                    tradeItem.IsAllowedToBeSold = true;
                }
                else { _TradeItems.Add(new PlayerItem(itemData, auction) { Location = TradeItemLocation.TradePile, IsAllowedToBeSold = true }); }
            }

            //Notify all interested parties.
            LoadTradePileEventInvoke(_TradeItems.Where(item => auctions.Any(auction => auction == item.AuctionInfo)).ToList());
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
                //Get the player item if it has been loaded before.
                PlayerItem tradeItem = _TradeItems.Find(item => item.AuctionInfo.TradeId == auction.TradeId);

                //Get the item data.
                Item itemData = await new ItemRequest().GetItemAsync(auction.ItemData.ResourceId);

                //If the player item existed before, just update its auction info and item data. Otherwise create one and keep track of it.
                if (tradeItem != null)
                {
                    tradeItem.AuctionInfo = auction;
                    tradeItem.ResourceData = itemData;
                    tradeItem.Location = TradeItemLocation.WatchList;
                }
                else { _TradeItems.Add(new PlayerItem(itemData, auction) { Location = TradeItemLocation.WatchList }); }
            }

            //Notify all interested parties.
            LoadWatchListEventInvoke(_TradeItems.Where(item => auctions.Any(auction => auction == item.AuctionInfo)).ToList());
        }
        /// <summary>
        /// Load all unassigned items.
        /// </summary>
        private async static Task LoadUnassignedAsync()
        {
            //Validation check.
            if (!_IsLoggedIn) { throw new ArgumentException("You are not logged in yet!"); }

            //Get the watch list item data.
            var response = await new TradeRequest().GetUnassignedItems();
            var itemData = response.ItemData;

            //If no items were found, stop here.
            if (itemData.Count == 0) { return; }

            //Get the player info and add to the watch list.
            foreach (var data in itemData)
            {
                //Get the player item if it has been loaded before.
                PlayerItem tradeItem = _TradeItems.Find(item => item.AuctionInfo.ItemData.Id == data.Id);

                //Get the resource data.
                Item resourceData = await new ItemRequest().GetItemAsync(data.ResourceId);

                //If the player item existed before, just update its auction info and item data. Otherwise create one and keep track of it.
                if (tradeItem != null)
                {
                    tradeItem.ResourceData = resourceData;
                    tradeItem.AuctionInfo.ItemData = data;
                    tradeItem.AuctionInfo.SellerEstablished = "-1";
                    tradeItem.Location = TradeItemLocation.Unassigned;
                }
                else
                {
                    //Create the trade item.
                    PlayerItem item = new PlayerItem(resourceData, new AuctionInfo()
                    {
                        ItemData = data,
                        SellerEstablished = "-1"
                    });
                    item.Location = TradeItemLocation.Unassigned;

                    //Add the item to the list.
                    _TradeItems.Add(item);
                }
            }

            //Notify all interested parties.
            LoadUnassignedEventInvoke(_TradeItems.Where(item => itemData.Any(data => data == item.AuctionInfo.ItemData)).ToList());
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
                //Get the player item if it has been loaded before.
                PlayerItem tradeItem = _TradeItems.Find(item => item.AuctionInfo.ItemData.Id == data.Id);

                //Get the resource data.
                Item resourceData = await new ItemRequest().GetItemAsync(data.ResourceId);

                //If the player item existed before, just update its auction info and item data. Otherwise create one and keep track of it.
                if (tradeItem != null)
                {
                    tradeItem.ResourceData = resourceData;
                    tradeItem.AuctionInfo.ItemData = data;
                    tradeItem.AuctionInfo.SellerEstablished = "-1";
                    tradeItem.Location = TradeItemLocation.Club;
                }
                else
                {
                    //Create the trade item.
                    PlayerItem item = new PlayerItem(resourceData, new AuctionInfo()
                    {
                        ItemData = data,
                        SellerEstablished = "-1"
                    });
                    item.Location = TradeItemLocation.Club;

                    //Add the item to the list.
                    _TradeItems.Add(item);
                }
            }

            //Notify all interested parties.
            LoadClubEventInvoke(_TradeItems.Where(item => itemData.Any(data => data == item.AuctionInfo.ItemData)).ToList());
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
        /// Sell the specified trade items.
        /// </summary>
        /// <param name="auctions">The trade items to sell.</param>
        private async static Task SellItemsAsync(IEnumerable<PlayerItem> auctions)
        {
            //Validation check.
            if (!_IsLoggedIn) { throw new ArgumentException("You have not logged in yet!"); }
            if (auctions.Any(item => !item.IsAllowedToBeSold)) { throw new ArgumentException("One of the items are not allowed to be sold!"); }

            //For each given auction, try to sell it.
            foreach (PlayerItem auction in auctions)
            {
                var response = await new SellRequest().SellItem(auction.AuctionInfo);
                auction.AuctionInfo.TradeId = response.TradeId;
            }

            //Notify all interested parties.
            SellItemsEventInvoke(auctions.ToList());
        }
        /// <summary>
        /// Search for players on the global auction.
        /// </summary>
        /// <returns></returns>
        private async static Task SearchItemsAsync(PlayerSearchParameters searchParameters)
        {
            //Validation check.
            if (!_IsLoggedIn) { throw new ArgumentException("You have not logged in yet!"); }
            if (searchParameters != null) { throw new ArgumentException("The search parameters must not be null!"); }

            //Search for players.
            var response = await new SearchRequest().SearchAsync(searchParameters);

            //The list to return.
            var items = new List<PlayerItem>();

            foreach (AuctionInfo auction in response.AuctionInfo)
            {
                Item itemData = await new ItemRequest().GetItemAsync(auction.ItemData.ResourceId);
                items.Add(new PlayerItem(itemData, auction) { Location = TradeItemLocation.Auction });
            }

            //Notify all interested parties.
            SearchItemsEventInvoke(items);
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
        private static void SellItemsEventInvoke(List<PlayerItem> tradeItems)
        {
            //Unlock the items and force them to update.
            tradeItems.ForEach(item => { item.Update = true; item.IsLocked = false; });

            if (OnSellItems != null) { OnSellItems(tradeItems); }
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
        #endregion

        #region Properties
        /// <summary>
        /// The trade items that is of interest at the moment. Includes those in the trade pile, watchlist and search.
        /// </summary>
        public static List<PlayerItem> TradeItems
        {
            get { return new List<PlayerItem>(_TradeItems); }
        }
        #endregion
    }
}
