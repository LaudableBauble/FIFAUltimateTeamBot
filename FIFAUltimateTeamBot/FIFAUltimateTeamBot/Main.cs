using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Windows.Forms;
using UltimateTeam.Toolkit.Parameter;
using UltimateTeam.Toolkit.Request;
using UltimateTeam.Toolkit.Model;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FIFAUltimateTeamBot
{
    public partial class Main : Form
    {
        PlayerItem _SelectedItem;

        public Main()
        {
            InitializeComponent();

            //Initialize the managers.
            DataManager.Initialize(LoadLoginCredentials());
            RequestManager.Initialize();

            //Subscribe to some events.
            ckbTradePile.CheckedChanged += OnCheckedChanged;
            ckbWatchList.CheckedChanged += OnCheckedChanged;
            ckbClub.CheckedChanged += OnCheckedChanged;
            ckbUnassigned.CheckedChanged += OnCheckedChanged;
            ckbAuctionItems.CheckedChanged += OnCheckedChanged;
            ckbCanBeSold.CheckedChanged += OnItemAllowedToBeSoldChanged;
            lstvItems.ItemSelectionChanged += OnItemSelectionChanged;
            RequestManager.OnLoadTradePile += OnLoadTradePile;
            RequestManager.OnLoadWatchList += OnLoadWatchList;
            RequestManager.OnLoadUnassigned += OnLoadUnassigned;
            RequestManager.OnLoadClub += OnLoadClub;
            RequestManager.OnUpdateItems += OnUpdateTradeItems;
            RequestManager.OnMoveItems += OnMoveTradeItems;
            RequestManager.OnSellItems += OnSellTradeItems;
            RequestManager.OnRemoveItems += OnRemoveTradeItems;

            //Initialize the GUI.
            SetupList();
            SetupAuctionSearch();
        }

        /// <summary>
        /// Load the login credentials and give them to the Request Manager.
        /// </summary>
        public LoginCredentials LoadLoginCredentials()
        {
            return (LoginCredentials)new XmlSerializer(typeof(LoginCredentials)).Deserialize(new XmlTextReader(@"Data\login.xml"));
        }
        /// <summary>
        /// Load the stats of all items.
        /// </summary>
        public List<StatPackage> LoadStats()
        {
            return (List<StatPackage>)new XmlSerializer(typeof(List<StatPackage>)).Deserialize(new XmlTextReader(@"Data\stats.xml"));
        }
        /// <summary>
        /// Save the updated stats of a certain set of items.
        /// </summary>
        public void SaveStats(List<PlayerItem> items)
        {
            //Load all the stats and update the one of interest.
            var stats = LoadStats();
            foreach (var item in items.Select(item => item.Stats))
            {
                var s = stats.Find(x => x.ItemID == item.ItemID);
                if (s != null) { s = item; }
                else { stats.Add(item); }
            }
            //new XmlSerializer(typeof(List<StatPackage>)).Serialize(new XmlTextWriter(@"Data\stats.xml", null), stats);
        }
        /// <summary>
        /// Setup everything to do with the auction search's GUI.
        /// </summary>
        public void SetupAuctionSearch()
        {
            //Initialize the dropdown lists.
            foreach (Level item in Enum.GetValues(typeof(Level)).Cast<Level>()) { cmbLevel.Items.Add(item); }
            foreach (Formation item in Formation.GetAll()) { cmbFormation.Items.Add(item); }
            foreach (Position item in Position.GetAll()) { cmbPosition.Items.Add(item); }
            foreach (Nation item in Nation.GetAll()) { cmbNationality.Items.Add(item); }
            foreach (League item in League.GetAll()) { cmbLeague.Items.Add(item); }
            foreach (Team item in Team.GetAll()) { cmbClub.Items.Add(item); }

        }
        /// <summary>
        /// Set up the list view.
        /// </summary>
        public void SetupList()
        {
            //Enable the correct settings.
            lstvItems.View = View.Details;
            lstvItems.LabelEdit = false;
            lstvItems.AllowColumnReorder = false;
            lstvItems.GridLines = true;
            lstvItems.FullRowSelect = true;

            //Reset the list view.
            lstvItems.Clear();

            //Add the columns.
            lstvItems.Columns.Add("First Name", -2, HorizontalAlignment.Left);
            lstvItems.Columns.Add("Last Name", -2, HorizontalAlignment.Left);
            lstvItems.Columns.Add("Rating", -2, HorizontalAlignment.Left);
            lstvItems.Columns.Add("Position", -2, HorizontalAlignment.Left);
            lstvItems.Columns.Add("Card Type", -2, HorizontalAlignment.Left);
            lstvItems.Columns.Add("Pile", -2, HorizontalAlignment.Left);
            lstvItems.Columns.Add("Bid", -2, HorizontalAlignment.Left);
            lstvItems.Columns.Add("Time", -2, HorizontalAlignment.Left);
            lstvItems.Columns.Add("Pace", -2, HorizontalAlignment.Left);
            lstvItems.Columns.Add("Shooting", -2, HorizontalAlignment.Left);
            lstvItems.Columns.Add("Passing", -2, HorizontalAlignment.Left);
            lstvItems.Columns.Add("Dribbling", -2, HorizontalAlignment.Left);
            lstvItems.Columns.Add("Defending", -2, HorizontalAlignment.Left);
            lstvItems.Columns.Add("Heading", -2, HorizontalAlignment.Left);
        }
        /// <summary>
        /// Load a list of items.
        /// </summary>
        public void LoadList(List<PlayerItem> items)
        {
            //Check if this method has been accessed from another thread, ie. not from the GUI thread, and rectify it.
            if (lstvItems.InvokeRequired) { Invoke((MethodInvoker)(() => LoadList(items))); return; }

            //Pause the drawing of the list view until a later date.
            lstvItems.BeginUpdate();

            //Clear the list view.
            lstvItems.Items.Clear();

            //For all specified items, either add them to the list.
            foreach (PlayerItem item in items)
            {
                //Add an item.
                var lstvItem = new ListViewItem(item.ResourceData.FirstName, 0);
                lstvItem.Tag = item;

                //The data.
                lstvItem.SubItems.Add(item.ResourceData.LastName);
                lstvItem.SubItems.Add(item.ResourceData.Rating.ToString());
                lstvItem.SubItems.Add(item.AuctionInfo.ItemData.PreferredPosition);
                lstvItem.SubItems.Add(item.ResourceData.CardType.ToString());
                lstvItem.SubItems.Add(item.Location.ToString());
                lstvItem.SubItems.Add(item.AuctionInfo.CurrentPrice.ToString());
                lstvItem.SubItems.Add(item.AuctionInfo.Expires.ToString());
                lstvItem.SubItems.Add(item.ResourceData.Attribute1.ToString());
                lstvItem.SubItems.Add(item.ResourceData.Attribute2.ToString());
                lstvItem.SubItems.Add(item.ResourceData.Attribute3.ToString());
                lstvItem.SubItems.Add(item.ResourceData.Attribute4.ToString());
                lstvItem.SubItems.Add(item.ResourceData.Attribute5.ToString());
                lstvItem.SubItems.Add(item.ResourceData.Attribute6.ToString());

                //Add the item row to the list view.
                lstvItems.Items.Add(lstvItem);
            }

            //Continue the drawing of the list view now that the update is complete.
            lstvItems.EndUpdate();

            //Change the item count to reflect the number of items in the selected tab.
            statlblCount.Text = lstvItems.Items.Count + " item(s).";
        }
        /// <summary>
        /// Update the list of items.
        /// </summary>
        public void UpdateList()
        {
            //Filter the list of items to display.
            var items = RequestManager.TradeItems;
            items = !ckbTradePile.Checked ? items.Where(item => item.Location != TradeItemLocation.TradePile).ToList() : items;
            items = !ckbWatchList.Checked ? items.Where(item => item.Location != TradeItemLocation.WatchList).ToList() : items;
            items = !ckbClub.Checked ? items.Where(item => item.Location != TradeItemLocation.Club).ToList() : items;
            items = !ckbUnassigned.Checked ? items.Where(item => item.Location != TradeItemLocation.Unassigned).ToList() : items;
            items = !ckbAuctionItems.Checked ? items.Where(item => item.Location != TradeItemLocation.Auction).ToList() : items;

            //Update the list view.
            LoadList(items);
        }
        /// <summary>
        /// Log a piece of text.
        /// </summary>
        /// <param name="text">The text to log.</param>
        /// <param name="isHeader">Whether to log a header.</param>
        public void Log(string text, bool isHeader)
        {
            //Check if this method has been accessed from another thread, ie. not from the GUI thread, and rectify it.
            if (rtxbLog.InvokeRequired) { Invoke((MethodInvoker)(() => Log(text, isHeader))); return; }

            //The full text to log.
            if (isHeader) { text = @"{\rtf1\ansi\b " + DateTime.Now.ToString("HH.mm.ss") + @": \b0" + text + "}"; }
            else { text = @"{\rtf1\ansi\" + text + "}"; }

            //Append the text to the textbox.
            if (isHeader) { rtxbLog.AppendRTFText(text); }
            else { rtxbLog.AppendRTFText(text, Color.DimGray); }

            //Save the text to the log file.
            File.AppendAllText(@"Data\log.txt", text);
        }

        /// <summary>
        /// The trade pile has been loaded, display the changes on the list view as well.
        /// </summary>
        public void OnLoadTradePile(List<PlayerItem> tradeItems)
        {
            //Update the items' stats.
            tradeItems.ForEach(x =>
            {
                var stats = LoadStats().Find(y => x.AuctionInfo.ItemData.Id == y.ItemID);
                x.Stats = stats != null ? stats : x.Stats;
            });

            //Log the items.
            Log("Loaded " + tradeItems.Count + @" items from the Trade Pile.\line", true);
            /*tradeItems.ForEach(item => Log("\t- " + item.ItemData.FirstName + " " + item.ItemData.LastName + "\t\t\t(" + item.AuctionInfo.ItemData.Id + @")\line",
                false));*/
        }
        /// <summary>
        /// The watch list has been loaded, display the changes on the list view as well.
        /// </summary>
        public void OnLoadWatchList(List<PlayerItem> tradeItems)
        {
            UpdateList();

            //Update the items' stats.
            tradeItems.ForEach(x =>
            {
                var stats = LoadStats().Find(y => x.AuctionInfo.ItemData.Id == y.ItemID);
                x.Stats = stats != null ? stats : x.Stats;
            });

            //Log the items.
            Log("Loaded " + tradeItems.Count + @" items from the Watch List.\line", true);
            /*tradeItems.ForEach(item => Log("\t- " + item.ItemData.FirstName + " " + item.ItemData.LastName + "\t\t\t(" + item.AuctionInfo.ItemData.Id + @")\line",
                false));*/
        }
        /// <summary>
        /// The unassigned items have been loaded, display the changes on the list view as well.
        /// </summary>
        public void OnLoadUnassigned(List<PlayerItem> tradeItems)
        {
            UpdateList();

            //Update the items' stats.
            tradeItems.ForEach(x =>
            {
                var stats = LoadStats().Find(y => x.AuctionInfo.ItemData.Id == y.ItemID);
                x.Stats = stats != null ? stats : x.Stats;
            });

            //Log the items.
            Log("Loaded " + tradeItems.Count + @" unassigned items.\line", true);
            /*tradeItems.ForEach(item => Log("\t- " + item.ItemData.FirstName + " " + item.ItemData.LastName + "\t\t\t(" + item.AuctionInfo.ItemData.Id + @")\line",
                false));*/
        }
        /// <summary>
        /// The items in the club have been loaded, display the changes on the list view as well.
        /// </summary>
        public void OnLoadClub(List<PlayerItem> tradeItems)
        {
            UpdateList();

            //Update the items' stats.
            tradeItems.ForEach(x =>
            {
                var stats = LoadStats().Find(y => x.AuctionInfo.ItemData.Id == y.ItemID);
                x.Stats = stats != null ? stats : x.Stats;
            });

            //Log the items.
            Log("Loaded " + tradeItems.Count + @" items from the Club.\line", true);
            /*tradeItems.ForEach(item => Log("\t- " + item.ItemData.FirstName + " " + item.ItemData.LastName + "\t\t\t(" + item.AuctionInfo.ItemData.Id + @")\line",
                false));*/
        }
        /// <summary>
        /// If any trade items have been updated, display the changes on the list view as well.
        /// </summary>
        public void OnUpdateTradeItems(List<PlayerItem> tradeItems)
        {
            UpdateList();

            //Log the items.
            Log("Updated " + tradeItems.Count + @" items.\line", true);
            /*tradeItems.ForEach(item => Log("\t- " + item.ItemData.FirstName + " " + item.ItemData.LastName + "\t\t\t(" + item.AuctionInfo.ItemData.Id + @")\line",
                false));*/
        }
        /// <summary>
        /// If any trade items have been moved from the watch list to the trade pile, log them.
        /// </summary>
        public void OnMoveTradeItems(List<PlayerItem> tradeItems)
        {
            UpdateList();

            //Log the items.
            Log("Moved " + tradeItems.Count + @" items to trade pile.\line", true);
            tradeItems.ForEach(item => Log("\t- " + item.ResourceData.FirstName + " " + item.ResourceData.LastName + "\t\t\t(" + item.AuctionInfo.TradeId + @")\line",
                false));
        }
        /// <summary>
        /// If any trade items have been auctioned, log them.
        /// </summary>
        public void OnSellTradeItems(List<PlayerItem> tradeItems)
        {
            //These items have been auctioned; log the stats.
            tradeItems.ForEach(item => item.Stats.TimesAuctioned++);
            SaveStats(tradeItems);

            //Log the items.
            Log("Auctioned " + tradeItems.Count + @" items.\line", true);
            /*tradeItems.ForEach(item => Log("\t- " + item.ItemData.FirstName + " " + item.ItemData.LastName + "\t\t\t(" + item.AuctionInfo.TradeId + @")\line",
                false));*/
        }
        /// <summary>
        /// If any trade items have been sold and subsequently removed from the trade pile, log them.
        /// </summary>
        public void OnRemoveTradeItems(List<PlayerItem> tradeItems)
        {
            //These items have been sold; log the stats.
            tradeItems.ForEach(item => { item.Stats.Sold = DateTime.Now; item.Stats.SoldFor = (int)item.AuctionInfo.CurrentPrice; });
            SaveStats(tradeItems);

            UpdateList();

            //Log the items.
            Log("Sold " + tradeItems.Count + @" items.\line", true);
            tradeItems.ForEach(item => Log("\t- " + item.ResourceData.FirstName + " " + item.ResourceData.LastName + " for " +
                item.AuctionInfo.CurrentPrice + "\t\t\t(" + item.AuctionInfo.TradeId + @")\line",
                false));
        }
        /// <summary>
        /// Change the sell state of an item.
        /// </summary>
        public void OnAllowForAuction(object sender, ItemCheckedEventArgs e)
        {
            (e.Item.Tag as PlayerItem).IsAllowedToBeSold = e.Item.Checked;
        }

        /// <summary>
        /// If a sorting checkbox has been checked or unchecked, update the list of items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCheckedChanged(object sender, EventArgs e)
        {
            //Filter the list of items to display.
            UpdateList();
        }
        /// <summary>
        /// If an item in the list view has been selected, display it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemSelectionChanged(object sender, EventArgs e)
        {
            //Get the item.
            _SelectedItem = (PlayerItem)(sender as ListView).SelectedItems[0].Tag;

            //Display the full name.
            lblItemName.Text = _SelectedItem.ResourceData.FirstName + " " + _SelectedItem.ResourceData.LastName;
            //Whether the item is allowed to be sold or not.
            ckbCanBeSold.Checked = _SelectedItem.IsAllowedToBeSold;
        }
        /// <summary>
        /// Change whether a selected item is allowed to be sold or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemAllowedToBeSoldChanged(object sender, EventArgs e)
        {
            _SelectedItem.IsAllowedToBeSold = ckbCanBeSold.Checked;
        }
        /// <summary>
        /// Either expand or collapse the panel that displays the currently selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExpandOrCollapseItem(object sender, EventArgs e)
        {
            //Either expand or collapse the panel.
            splitItems.Panel2Collapsed = !splitItems.Panel2Collapsed;

            //Change the text of the button to reflect the change.
            if (splitItems.Panel2Collapsed) { btnExpandCollapseItem.Text = "<---"; }
            else { btnExpandCollapseItem.Text = "--->"; }
        }
    }
}
