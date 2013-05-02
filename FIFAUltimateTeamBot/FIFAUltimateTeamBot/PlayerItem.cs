using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Model;
using UltimateTeam.Toolkit.Request;

namespace FIFAUltimateTeamBot
{
    public class PlayerItem
    {
        #region Fields
        private AuctionInfo _AuctionInfo;
        private DateTime _LastUpdated;
        private TradeItemLocation _Location;
        private bool _IsLocked;
        private bool _Update;
        private bool _Remove;
        private bool _IsAllowedToBeAuctioned;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a player item.
        /// </summary>
        public PlayerItem()
        {
            _AuctionInfo = new AuctionInfo();
            _LastUpdated = DateTime.Now;
            _Location = TradeItemLocation.Unknown;
            _IsLocked = false;
            _Update = false;
            _Remove = false;
            _IsAllowedToBeAuctioned = false;
        }
        /// <summary>
        /// Create a player item.
        /// </summary>
        /// <param name="auctionInfo">The player's auction info.</param>
        public PlayerItem(AuctionInfo auctionInfo)
        {
            _AuctionInfo = auctionInfo;
            _LastUpdated = DateTime.Now;
            _Location = TradeItemLocation.Unknown;
            _IsLocked = false;
            _Update = false;
            _Remove = false;
            _IsAllowedToBeAuctioned = false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reset the auction info, emulating a new arrival in the trade pile.
        /// </summary>
        public void ResetAuctionInfo()
        {
            _AuctionInfo.SellerEstablished = "0";
            _AuctionInfo.StartingBid = 0;
            _AuctionInfo.CurrentPrice = 0;
            _Location = TradeItemLocation.TradePile;
        }
        /// <summary>
        /// Prepare for a fresh auction. This resets the setup from the previous auction.
        /// </summary>
        public void PrepareForAuction()
        {
            //Return to default configuration.
            _AuctionInfo.Expires = -1;
            _AuctionInfo.TradeState = "expired";
            _AuctionInfo.SellerEstablished = RequestBase.Persona.UserClubList[0].Established;
            _AuctionInfo.BidState = "highest";

            //Decide the price.
            var resource = ResourceItem;
            if (resource != null && resource.DefaultBidPrice > 0)
            {
                _IsAllowedToBeAuctioned = resource.IsAllowedToBeAuctioned;
                _AuctionInfo.StartingBid = (uint)resource.DefaultBidPrice;
                _AuctionInfo.BuyNowPrice = (uint)resource.DefaultBuyNowPrice;
            }
            else { _IsAllowedToBeAuctioned = false; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// The player item's resource item. This is data that does not change.
        /// </summary>
        public ResourceItem ResourceItem
        {
            get { return DataManager.ResourceDataExists(_AuctionInfo.ItemData.ResourceId) ? DataManager.ResourceData[_AuctionInfo.ItemData.ResourceId] : null; }
        }
        /// <summary>
        /// The auction information regarding the player. This is data that changes over time and needs to be updated.
        /// </summary>
        public AuctionInfo AuctionInfo
        {
            get { return _AuctionInfo; }
            set { _AuctionInfo = value; _LastUpdated = DateTime.Now; _Update = false; }
        }
        /// <summary>
        /// The time this player item was last updated.
        /// </summary>
        public DateTime LastUpdated
        {
            get { return _LastUpdated; }
        }
        /// <summary>
        /// Whether the player item is locked, ie. there is currently a request or process that is modifying it in some way.
        /// </summary>
        public bool IsLocked
        {
            get { return _IsLocked; }
            set { _IsLocked = value; }
        }
        /// <summary>
        /// Whether the player item needs to be updated, ie. the auction data may be obsolete.
        /// </summary>
        public bool Update
        {
            get { return _Update; }
            set { _Update = value; }
        }
        /// <summary>
        /// Whether the player item needs to be removed from the RequestManager's internal list of items.
        /// </summary>
        public bool Remove
        {
            get { return _Remove; }
            set { _Remove = value; }
        }
        /// <summary>
        /// Whether the player item is allowed to be auctioned.
        /// </summary>
        public bool IsAllowedToBeAuctioned
        {
            get
            {
                return ResourceItem != null && ResourceItem.IsAllowedToBeAuctioned && _IsAllowedToBeAuctioned;
            }
            set
            {
                var resource = ResourceItem;
                var stats = DataManager.GetStat(_AuctionInfo.ItemData.Id);
                if (resource != null && resource.IsAllowedToBeAuctioned) { _IsAllowedToBeAuctioned = value; }
                else { _IsAllowedToBeAuctioned = false; }
                if (stats != null) { stats.IsAllowedToBeAuctioned = _IsAllowedToBeAuctioned; }
            }
        }
        /// <summary>
        /// The location of the player item.
        /// </summary>
        public TradeItemLocation Location
        {
            get
            {
                /*if (_AuctionInfo.SellerEstablished.Equals(RequestBase.Persona.UserClubList.First().Established) || _AuctionInfo.SellerEstablished.Equals("0"))
                {
                    return TradeItemLocation.TradePile;
                }
                else if (_AuctionInfo.SellerEstablished.Equals("-1")) { return TradeItemLocation.Unassigned; }
                else { return TradeItemLocation.WatchList; }*/
                return _Location;
            }
            set { _Location = value; }
        }
        #endregion
    }
}
