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
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FIFAUltimateTeamBot
{
    /// <summary>
    /// All data pertaining to the bot is located in here; ie. the items, the login credentials, the statistics etc.
    /// No logic is performed here, the data manager only does as it is commanded.
    /// </summary>
    public static class DataManager
    {
        #region Fields
        private static LoginCredentials _LoginCredentials;
        private static ConcurrentDictionary<long, PlayerItem> _Items;
        private static ConcurrentDictionary<long, Item> _ResourceData;
        private static ConcurrentDictionary<long, StatPackage> _Statistics;
        #endregion

        #region Indexes
        /*public PlayerItem this[int index]
        {
            get { return _Items[index]; }
        }*/
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the data manager.
        /// </summary>
        /// <param name="loginCredentials">The login credentials to use.</param>
        public static void Initialize(LoginCredentials loginCredentials)
        {
            _LoginCredentials = loginCredentials;
            _Items = new ConcurrentDictionary<long, PlayerItem>();
            _ResourceData = new ConcurrentDictionary<long, Item>();
            _Statistics = new ConcurrentDictionary<long, StatPackage>();
        }

        /// <summary>
        /// Add or update an item's auction info.
        /// Auction information only exists if an item is in anyone's trade pile.
        /// </summary>
        /// <param name="auctionInfo">The auction info to add or update to.</param>
        public static PlayerItem AddOrUpdate(AuctionInfo auctionInfo)
        {
            //Look for the item.
            var item = Exists(auctionInfo.ItemData.Id) ? _Items[auctionInfo.ItemData.Id] : null;

            //If the item already exists, just update its auction info. Otherwise we create a new player item from scratch.
            if (item != null) { item.AuctionInfo = auctionInfo; }
            else
            {
                //Create the player item and add the auction info to it.
                item = new PlayerItem();
                item.AuctionInfo = auctionInfo;

                //Add the item to the list.
                _Items.TryAdd(item.AuctionInfo.ItemData.Id, item);
            }

            return item;
        }
        /// <summary>
        /// Add or update an item's item data.
        /// Item data always exists but is usually contained within the auction information, something which thus makes it unnecessary to add on its own.
        /// </summary>
        /// <param name="itemData">The item data to add or update to.</param>
        public static PlayerItem AddOrUpdate(ItemData itemData)
        {
            //Look for the item.
            var item = Exists(itemData.Id) ? _Items[itemData.Id] : null;

            //If the item already exists, just update its item data. Otherwise we create a new player item from scratch.
            if (item != null) { item.AuctionInfo.ItemData = itemData; }
            else
            {
                //Create the player item and add the item data to it.
                item = new PlayerItem();
                item.AuctionInfo.ItemData = itemData;

                //Add the item to the list.
                _Items.TryAdd(item.AuctionInfo.ItemData.Id, item);
            }

            return item;
        }
        /// <summary>
        /// Add or update an item's statistics.
        /// Statistics exist for every item the bot has access to.
        /// </summary>
        /// <param name="statPackage">The item stats to add or update.</param>
        public static StatPackage AddOrUpdate(StatPackage statPackage)
        {
            //If the stats already exists, just update it. Otherwise add it from scratch.
            if (StatisticsExists(statPackage.ItemID)) { _Statistics[statPackage.ItemID] = statPackage; }
            else
            {
                //Add the item to the list.
                _Statistics.TryAdd(statPackage.ItemID, statPackage);
            }

            return statPackage;
        }
        /// <summary>
        /// Add or update an item's resource data.
        /// Resource data is not uniquely tied to any specific item but instead to all items of the same kind.
        /// Ie. for every specific Lionel Messi card there is only one set of resource data.
        /// </summary>
        /// <param name="resourceData">The resource data to add or update to.</param>
        public static void AddOrUpdate(Item resourceData, long resourceId)
        {
            //Look for the resource data.
            var data = ResourceDataExists(resourceId) ? _ResourceData[resourceId] : null;

            //If the data already exists, just update it. Otherwise add it to the dictionary.
            if (data != null) { data = resourceData; }
            else { _ResourceData.AddOrUpdate(resourceId, resourceData, (key, oldValue) => resourceData); }
        }

        /// <summary>
        /// Remove an item.
        /// </summary>
        /// <param name="id">The id of the item to remove.</param>
        public static bool Remove(long id)
        {
            PlayerItem item;
            return _Items.TryRemove(id, out item);
        }
        /// <summary>
        /// Remove some items.
        /// </summary>
        /// <param name="ids">The ids of the items to remove.</param>
        public static void Remove(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                PlayerItem item;
                _Items.TryRemove(id, out item);
            }
        }
        /// <summary>
        /// Remove some items.
        /// </summary>
        /// <param name="items">The items to remove.</param>
        public static void Remove(IEnumerable<PlayerItem> items)
        {
            Remove(items.Select(item => item.AuctionInfo.ItemData.Id));
        }
        /// <summary>
        /// Removes a sequence of player items based on a predicate.
        /// </summary>
        /// <param name="filter">The predicate filter to apply.</param>
        public static void RemoveAll(Func<PlayerItem, bool> filter)
        {
            Remove(_Items.Values.Where(filter));
        }

        /// <summary>
        /// Determines whether any player item of a sequence satisfies a condition.
        /// </summary>
        /// <param name="filter">The predicate filter to apply.</param>
        /// <returns>The filtered player items.</returns>
        public static bool Any(Func<PlayerItem, bool> filter)
        {
            return _Items.Values.Any(filter);
        }
        /// <summary>
        /// Filters a sequence of player items based on a predicate.
        /// </summary>
        /// <param name="filter">The predicate filter to apply.</param>
        /// <returns>The filtered player items.</returns>
        public static IEnumerable<PlayerItem> Where(Func<PlayerItem, bool> filter)
        {
            return _Items.Values.Where(filter);
        }
        /// <summary>
        /// Returns a number that represents how many player items in the specified sequence satisfy a condition.
        /// </summary>
        /// <param name="filter">The predicate filter to apply.</param>
        /// <returns>The number of player items that satisfy the condition.</returns>
        public static int Count(Func<PlayerItem, bool> filter)
        {
            return _Items.Values.Count(filter);
        }
        /// <summary>
        /// Whether certain player item exists.
        /// </summary>
        /// <param name="id">The id of the item to look for.</param>
        /// <returns>Whether the item exists or not.</returns>
        public static bool Exists(long id)
        {
            PlayerItem item;
            return _Items.TryGetValue(id, out item);
        }
        /// <summary>
        /// Whether certain resource data exists.
        /// </summary>
        /// <param name="resourceId">The resource id of the data to look for.</param>
        /// <returns>Whether the resource data exists or not.</returns>
        public static bool ResourceDataExists(long resourceId)
        {
            Item item;
            return _ResourceData.TryGetValue(resourceId, out item);
        }
        /// <summary>
        /// Whether certain statistics exists.
        /// </summary>
        /// <param name="id">The id of the statistics to look for.</param>
        /// <returns>Whether the statistics exists or not.</returns>
        public static bool StatisticsExists(long id)
        {
            StatPackage stats;
            return _Statistics.TryGetValue(id, out stats);
        }
        /// <summary>
        /// Get the statistics for a certain item.
        /// </summary>
        /// <param name="id">The id of the item.</param>
        /// <returns>The stat package.</returns>
        public static StatPackage GetStat(long id)
        {
            if (StatisticsExists(id)) { return _Statistics[id]; }
            else { return new StatPackage() { ItemID = id }; }
        }

        /// <summary>
        /// Load the stats of all items.
        /// </summary>
        public static List<StatPackage> LoadStats()
        {
            return (List<StatPackage>)new XmlSerializer(typeof(List<StatPackage>)).Deserialize(new XmlTextReader(@"Data\stats.xml"));
        }
        /// <summary>
        /// Save the updated stats of a certain set of items.
        /// </summary>
        public static void SaveStats()
        {
            try
            {
                //Load all the stats and update the one of interest.
                /*var stats = LoadStats();
                foreach (var item in _Statistics.Values.ToList())
                {
                    var s = stats.Find(x => x.ItemID == item.ItemID);
                    if (s != null) { s = item; }
                    else { stats.Add(item); }
                }*/
                new XmlSerializer(typeof(List<StatPackage>)).Serialize(new XmlTextWriter(@"Data\stats.xml", null), _Statistics.Values.ToList());
            }
            catch (IOException e)
            {

            }
        }
        /// <summary>
        /// Load the resource data of all items.
        /// </summary>
        public static List<KeyValuePair<long, Item>> LoadResourceData()
        {
            return (List<KeyValuePair<long, Item>>)new XmlSerializer(typeof(List<KeyValuePair<long, Item>>)).Deserialize(new XmlTextReader(@"Data\resourceData.xml"));
        }
        /// <summary>
        /// Save the resource data to disk.
        /// </summary>
        public static void SaveResourceData()
        {
            try
            {
                //Load all the data and update the ones of interest.
                /*var resourceData = LoadResourceData();
                foreach (var item in _ResourceData.Values.ToList())
                {
                    var data = resourceData.Find(x => x.ClubId == item.ClubId && x.LastName.Equals(item.LastName));
                    if (data != null) { data = item; }
                    else { resourceData.Add(item); }
                }*/
                var resources = new List<KeyValuePair<long, Item>>();
                _ResourceData.ToList().ForEach(x => resources.Add(new KeyValuePair<long, Item>() { Key = x.Key, Value = x.Value }));
                new XmlSerializer(typeof(List<KeyValuePair<long, Item>>)).Serialize(new XmlTextWriter(@"Data\resourceData.xml", null), resources);
            }
            catch (IOException) { }
        }
        /// <summary>
        /// Filter a sequence of players.
        /// </summary>
        /// <param name="players">The players to filter.</param>
        /// <param name="filter">The filter to use.</param>
        /// <returns>The filtered set of players.</returns>
        public static List<PlayerItem> Filter(List<PlayerItem> players, FilterParameters filter)
        {
            //Filter the set of players.
            var filtered = players.Where(x => x.AuctionInfo.ItemData.Rating >= filter.MinRating);
            filtered = players.Where(x => x.AuctionInfo.ItemData.Rating <= (filter.MaxRating <= 0 ? 100 : filter.MaxRating));
            filtered = players.Where(x => x.AuctionInfo.StartingBid >= filter.MinBidPrice);
            if (filter.MaxBidPrice > 0) { filtered = players.Where(x => x.AuctionInfo.StartingBid <= filter.MaxBidPrice); }
            filtered = players.Where(x => x.AuctionInfo.BuyNowPrice >= filter.MinBuyNowPrice);
            if (filter.MaxBuyNowPrice > 0) { filtered = players.Where(x => x.AuctionInfo.BuyNowPrice <= filter.MaxBuyNowPrice); }

            return filtered.ToList();
        }
        #endregion

        #region Properties
        /// <summary>
        /// The login credentials used to login to Ultimate Team.
        /// </summary>
        public static LoginCredentials LoginCredentials
        {
            get { return _LoginCredentials; }
            set { _LoginCredentials = value; }
        }
        /// <summary>
        /// The trade items that is of interest at the moment. Includes those in the trade pile, watchlist and search.
        /// The key is the player id.
        /// </summary>
        public static ConcurrentDictionary<long, PlayerItem> Items
        {
            get { return new ConcurrentDictionary<long, PlayerItem>(_Items); }
        }
        /// <summary>
        /// The resource data. The key is the resource id.
        /// </summary>
        public static ConcurrentDictionary<long, Item> ResourceData
        {
            get { return new ConcurrentDictionary<long, Item>(_ResourceData); }
        }
        /// <summary>
        /// The statistics. The key is the item id.
        /// </summary>
        public static ConcurrentDictionary<long, StatPackage> Statistics
        {
            get { return new ConcurrentDictionary<long, StatPackage>(_Statistics); }
        }
        #endregion
    }
}
