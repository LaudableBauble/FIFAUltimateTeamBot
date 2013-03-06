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

            //Initialize the Request Manager.
            RequestManager.Initialize(LoadLoginCredentials());

            //Subscribe to some events.
            lstvTradePile.MouseDoubleClick += OnJumpToItem;
            lstvWatchList.MouseDoubleClick += OnJumpToItem;
            lstvClub.MouseDoubleClick += OnJumpToItem;
            lstvClub.ItemChecked += OnAllowForAuction;
            tabctrlMain.Selected += OnTabSelectedChange;
            RequestManager.OnUpdateItems += OnUpdateTradeItems;
            RequestManager.OnMoveItems += OnMoveTradeItems;
            RequestManager.OnSellItems += OnSellTradeItems;
            RequestManager.OnRemoveItems += OnRemoveTradeItems;

            //Setup the list views.
            SetUpListViews();
        }

        /// <summary>
        /// Load the login credentials and give them to the Request Manager.
        /// </summary>
        public LoginCredentials LoadLoginCredentials()
        {
            return (LoginCredentials)new XmlSerializer(typeof(LoginCredentials)).Deserialize(new XmlTextReader(@"Data\login.xml"));
        }
        /// <summary>
        /// Set up the list views.
        /// </summary>
        public void SetUpListViews()
        {
            //Enable the correct settings.
            lstvTradePile.View = View.Details;
            lstvTradePile.LabelEdit = false;
            lstvTradePile.AllowColumnReorder = false;
            lstvTradePile.GridLines = true;
            lstvTradePile.FullRowSelect = true;

            lstvWatchList.View = View.Details;
            lstvWatchList.LabelEdit = false;
            lstvWatchList.AllowColumnReorder = false;
            lstvWatchList.GridLines = true;
            lstvWatchList.FullRowSelect = true;

            lstvClub.View = View.Details;
            lstvClub.LabelEdit = false;
            lstvClub.AllowColumnReorder = false;
            lstvClub.GridLines = true;
            lstvClub.FullRowSelect = true;
            lstvClub.CheckBoxes = true;


            //Reset the list views.
            lstvTradePile.Clear();
            lstvWatchList.Clear();
            lstvClub.Clear();

            //Add the columns.
            lstvTradePile.Columns.Add("First Name", -2, HorizontalAlignment.Left);
            lstvTradePile.Columns.Add("Last Name", -2, HorizontalAlignment.Left);
            lstvTradePile.Columns.Add("Rating", -2, HorizontalAlignment.Left);
            lstvTradePile.Columns.Add("Position", -2, HorizontalAlignment.Left);
            lstvTradePile.Columns.Add("Card Type", -2, HorizontalAlignment.Left);
            lstvTradePile.Columns.Add("Bid", -2, HorizontalAlignment.Left);
            lstvTradePile.Columns.Add("Time", -2, HorizontalAlignment.Left);
            lstvTradePile.Columns.Add("Pace", -2, HorizontalAlignment.Left);
            lstvTradePile.Columns.Add("Shooting", -2, HorizontalAlignment.Left);
            lstvTradePile.Columns.Add("Passing", -2, HorizontalAlignment.Left);
            lstvTradePile.Columns.Add("Dribbling", -2, HorizontalAlignment.Left);
            lstvTradePile.Columns.Add("Defending", -2, HorizontalAlignment.Left);
            lstvTradePile.Columns.Add("Heading", -2, HorizontalAlignment.Left);

            lstvWatchList.Columns.Add("First Name", -2, HorizontalAlignment.Left);
            lstvWatchList.Columns.Add("Last Name", -2, HorizontalAlignment.Left);
            lstvWatchList.Columns.Add("Rating", -2, HorizontalAlignment.Left);
            lstvWatchList.Columns.Add("Position", -2, HorizontalAlignment.Left);
            lstvWatchList.Columns.Add("Card Type", -2, HorizontalAlignment.Left);
            lstvWatchList.Columns.Add("Bid", -2, HorizontalAlignment.Left);
            lstvWatchList.Columns.Add("Time", -2, HorizontalAlignment.Left);
            lstvWatchList.Columns.Add("Pace", -2, HorizontalAlignment.Left);
            lstvWatchList.Columns.Add("Shooting", -2, HorizontalAlignment.Left);
            lstvWatchList.Columns.Add("Passing", -2, HorizontalAlignment.Left);
            lstvWatchList.Columns.Add("Dribbling", -2, HorizontalAlignment.Left);
            lstvWatchList.Columns.Add("Defending", -2, HorizontalAlignment.Left);
            lstvWatchList.Columns.Add("Heading", -2, HorizontalAlignment.Left);

            lstvClub.Columns.Add("First Name", -2, HorizontalAlignment.Left);
            lstvClub.Columns.Add("Last Name", -2, HorizontalAlignment.Left);
            lstvClub.Columns.Add("Rating", -2, HorizontalAlignment.Left);
            lstvClub.Columns.Add("Position", -2, HorizontalAlignment.Left);
            lstvClub.Columns.Add("Card Type", -2, HorizontalAlignment.Left);
            lstvClub.Columns.Add("Pile", -2, HorizontalAlignment.Left);
            lstvClub.Columns.Add("Pace", -2, HorizontalAlignment.Left);
            lstvClub.Columns.Add("Shooting", -2, HorizontalAlignment.Left);
            lstvClub.Columns.Add("Passing", -2, HorizontalAlignment.Left);
            lstvClub.Columns.Add("Dribbling", -2, HorizontalAlignment.Left);
            lstvClub.Columns.Add("Defending", -2, HorizontalAlignment.Left);
            lstvClub.Columns.Add("Heading", -2, HorizontalAlignment.Left);
        }
        /// <summary>
        /// Update the trade pile and watch list.
        /// </summary>
        public void UpdateListViews()
        {
            //Check if this method has been accessed from another thread, ie. not from the GUI thread, and rectify it.
            if (lstvTradePile.InvokeRequired) { Invoke((MethodInvoker)(() => UpdateListViews())); return; }

            //Add all trade pile items.
            foreach (PlayerItem trade in RequestManager.TradeItems.FindAll(item => item.Location == TradeItemLocation.TradePile))
            {
                //If this item has already been added, just update it. Otherwise create it.
                var item = lstvTradePile.Items.Count != 0 ? lstvTradePile.Items.Cast<ListViewItem>().FirstOrDefault(lstvItem => lstvItem.Tag == trade) : null;
                if (item == null)
                {
                    //Add an item.
                    item = new ListViewItem(trade.ResourceData.FirstName, 0);
                    item.Tag = trade;

                    //The data.
                    item.SubItems.Add(trade.ResourceData.LastName);
                    item.SubItems.Add(trade.ResourceData.Rating.ToString());
                    item.SubItems.Add(trade.AuctionInfo.ItemData.PreferredPosition);
                    item.SubItems.Add(trade.ResourceData.CardType.ToString());
                    item.SubItems.Add(trade.AuctionInfo.CurrentPrice.ToString());
                    item.SubItems.Add(trade.AuctionInfo.Expires.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute1.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute2.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute3.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute4.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute5.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute6.ToString());

                    //Add the item row to the list view.
                    lstvTradePile.Items.Add(item);
                }
                else
                {
                    //Just update what's neccessary.
                    item.SubItems[5].Text = trade.AuctionInfo.CurrentPrice.ToString();
                    item.SubItems[6].Text = trade.AuctionInfo.Expires.ToString();
                }
            }

            //Add all watch list items.
            foreach (PlayerItem trade in RequestManager.TradeItems.FindAll(item => item.Location == TradeItemLocation.WatchList))
            {
                //If this item has already been added, just update it. Otherwise create it.
                var item = lstvWatchList.Items.Count != 0 ? lstvWatchList.Items.Cast<ListViewItem>().FirstOrDefault(lstvItem => lstvItem.Tag == trade) : null;
                if (item == null)
                {
                    //Add an item.
                    item = new ListViewItem(trade.ResourceData.FirstName, 0);
                    item.Tag = trade;

                    //The data.
                    item.SubItems.Add(trade.ResourceData.LastName);
                    item.SubItems.Add(trade.ResourceData.Rating.ToString());
                    item.SubItems.Add(trade.AuctionInfo.ItemData.PreferredPosition);
                    item.SubItems.Add(trade.ResourceData.CardType.ToString());
                    item.SubItems.Add(trade.AuctionInfo.CurrentPrice.ToString());
                    item.SubItems.Add(trade.AuctionInfo.Expires.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute1.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute2.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute3.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute4.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute5.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute6.ToString());

                    //Add the item row to the list view.
                    lstvWatchList.Items.Add(item);
                }
                else
                {
                    //Just update what's neccessary.
                    item.SubItems[5].Text = trade.AuctionInfo.CurrentPrice.ToString();
                    item.SubItems[6].Text = trade.AuctionInfo.Expires.ToString();
                    item.Checked = trade.IsAllowedToBeSold;
                }
            }

            //Add all club and unassigned items.
            foreach (PlayerItem trade in RequestManager.TradeItems.FindAll(item =>
                item.Location == TradeItemLocation.Club || item.Location == TradeItemLocation.Unassigned))
            {
                //If this item has already been added, just update it. Otherwise create it.
                ListViewItem item = lstvClub.Items.Count != 0 ? lstvClub.Items.Cast<ListViewItem>().FirstOrDefault(lstvItem => lstvItem.Tag == trade) : null;
                if (item == null)
                {
                    //Add an item.
                    item = new ListViewItem(trade.ResourceData.FirstName, 0);
                    item.Tag = trade;

                    //The data.
                    item.SubItems.Add(trade.ResourceData.LastName);
                    item.SubItems.Add(trade.ResourceData.Rating.ToString());
                    item.SubItems.Add(trade.AuctionInfo.ItemData.PreferredPosition);
                    item.SubItems.Add(trade.ResourceData.CardType.ToString());
                    item.SubItems.Add(trade.Location.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute1.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute2.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute3.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute4.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute5.ToString());
                    item.SubItems.Add(trade.ResourceData.Attribute6.ToString());

                    //Add the item row to the list view.
                    lstvClub.Items.Add(item);
                }
                else
                {
                    //Just update what's neccessary.
                    item.SubItems[5].Text = trade.Location.ToString();
                    item.Checked = trade.IsAllowedToBeSold;
                }
            }
        }
        /// <summary>
        /// Display the selected item on the Selected Item tab.
        /// </summary>
        public void DisplaySelectedItem()
        {
            //Display the full name.
            lblFullName.Text = _SelectedItem.ResourceData.FirstName + " " + _SelectedItem.ResourceData.LastName;
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
        /// Remove the items from the list views.
        /// </summary>
        /// <param name="items">The items to remove.</param>
        public void RemoveItems(List<PlayerItem> items)
        {
            //Check if this method has been accessed from another thread, ie. not from the GUI thread, and rectify it.
            if (lstvTradePile.InvokeRequired) { Invoke((MethodInvoker)(() => RemoveItems(items))); return; }

            //Remove the items.
            items.ForEach(item =>
                {
                    //The trade pile.
                    var toRemove = lstvTradePile.Items.Cast<ListViewItem>().SingleOrDefault(lstvItem => lstvItem.Tag == item);
                    if (toRemove != null) { lstvTradePile.Items.Remove(toRemove); }
                    //The watch list.
                    toRemove = lstvWatchList.Items.Cast<ListViewItem>().SingleOrDefault(lstvItem => lstvItem.Tag == item);
                    if (toRemove != null) { lstvWatchList.Items.Remove(toRemove); }
                    //The club/unassigned.
                    toRemove = lstvClub.Items.Cast<ListViewItem>().SingleOrDefault(lstvItem => lstvItem.Tag == item);
                    if (toRemove != null) { lstvClub.Items.Remove(toRemove); }
                });
        }

        /// <summary>
        /// If any trade items have been updated, display the changes on the list view as well.
        /// </summary>
        public void OnUpdateTradeItems(List<PlayerItem> tradeItems)
        {
            UpdateListViews();

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
            //Remove the items.
            RemoveItems(tradeItems);

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
            //Remove the items.
            RemoveItems(tradeItems);

            //Log the items.
            Log("Sold " + tradeItems.Count + @" items.\line", true);
            tradeItems.ForEach(item => Log("\t- " + item.ResourceData.FirstName + " " + item.ResourceData.LastName + " for " +
                item.AuctionInfo.CurrentPrice + "\t\t\t(" + item.AuctionInfo.TradeId + @")\line",
                false));
        }
        /// <summary>
        /// Jump to the selected item.
        /// </summary>
        public void OnJumpToItem(object sender, EventArgs e)
        {
            try
            {
                //Update the selected item and refresh it.
                _SelectedItem = (PlayerItem)(sender as ListView).SelectedItems[0].Tag;
                RequestManager.UpdateItems(new List<PlayerItem>() { _SelectedItem });

                //Jump to the Selected Item tab page and load it up.
                tabctrlMain.SelectedTab = tabSelectedItem;
                DisplaySelectedItem();
            }
            catch (Exception) { }
        }
        /// <summary>
        /// Change the sell state of an item.
        /// </summary>
        public void OnAllowForAuction(object sender, ItemCheckedEventArgs e)
        {
            (e.Item.Tag as PlayerItem).IsAllowedToBeSold = e.Item.Checked;
        }
        /// <summary>
        /// Change the sell state of an item.
        /// </summary>
        public void OnTabSelectedChange(object sender, TabControlEventArgs e)
        {
            //Get the newly selected tab.
            var tab = e.TabPage;

            //Get the number of items in the selected tab.
            int count = 0;
            if (tab == tabTradePile) { count = lstvTradePile.Items.Count; }
            else if (tab == tabWatchlist) { count = lstvWatchList.Items.Count; }
            if (tab == tabClub) { count = lstvClub.Items.Count; }

            //Change the item count to reflect the number of items in the selected tab.
            statlblCount.Text = count + " items.";
        }
    }
}
