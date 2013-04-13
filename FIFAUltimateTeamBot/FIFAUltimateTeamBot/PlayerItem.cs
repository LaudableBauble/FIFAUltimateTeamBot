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
        private StatPackage _Stats;
        private DateTime _LastUpdated;
        private TradeItemLocation _Location;
        private bool _IsLocked;
        private bool _Update;
        private bool _Remove;
        private bool _IsAllowedToBeSold;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a player item.
        /// </summary>
        public PlayerItem()
        {
            _AuctionInfo = new AuctionInfo();
            _Stats = null;
            _LastUpdated = DateTime.Now;
            _Location = TradeItemLocation.Unknown;
            _IsLocked = false;
            _Update = false;
            _Remove = false;
            _IsAllowedToBeSold = false;
        }
        /// <summary>
        /// Create a player item.
        /// </summary>
        /// <param name="auctionInfo">The player's auction info.</param>
        public PlayerItem(AuctionInfo auctionInfo)
        {
            _AuctionInfo = auctionInfo;
            _Stats = new StatPackage(_AuctionInfo.ItemData.Id);
            _LastUpdated = DateTime.Now;
            _Location = TradeItemLocation.Unknown;
            _IsLocked = false;
            _Update = false;
            _Remove = false;
            _IsAllowedToBeSold = false;
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
            if (_AuctionInfo.ItemData.Rating >= 84) { _AuctionInfo.StartingBid = 1400; _AuctionInfo.BuyNowPrice = 1900; }
            else if (_AuctionInfo.ItemData.Rating == 83) { _AuctionInfo.StartingBid = 1200; _AuctionInfo.BuyNowPrice = 1700; }
            else if (_AuctionInfo.ItemData.Rating == 82) { _AuctionInfo.StartingBid = 1000; _AuctionInfo.BuyNowPrice = 1500; }
            if (_AuctionInfo.ItemData.Rating <= 81) { _AuctionInfo.StartingBid = 900; _AuctionInfo.BuyNowPrice = 1300; }

            if (DataManager.ResourceDataExists(_AuctionInfo.ItemData.Id))
            {
                if (DataManager.ResourceData[_AuctionInfo.ItemData.Id].LastName.Equals("Sturridge"))
                {
                    _AuctionInfo.StartingBid = 3300; _AuctionInfo.BuyNowPrice = 3800;
                }
            }

            //Special prices.
            switch (_AuctionInfo.ItemData.ResourceId)
            {
                //Normal Reus.
                case 1342365630: { _AuctionInfo.StartingBid = 5300; _AuctionInfo.BuyNowPrice = 5900; break; }
                //Normal Muller.
                case 1342366876: { _AuctionInfo.StartingBid = 1900; _AuctionInfo.BuyNowPrice = 2500; break; }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// The player item's resource data. This is data that does not change.
        /// </summary>
        public Item ResourceData
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
        /// The auction stats of this player item.
        /// </summary>
        public StatPackage Stats
        {
            get { return _Stats; }
            set { _Stats = value; }
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
        public bool IsAllowedToBeSold
        {
            get { return _IsAllowedToBeSold; }
            set { _IsAllowedToBeSold = value; }
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
