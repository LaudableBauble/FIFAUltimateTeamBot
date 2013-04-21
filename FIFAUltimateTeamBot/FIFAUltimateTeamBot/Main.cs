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
            RequestManager.OnAuctionItems += OnAuctionTradeItems;
            RequestManager.OnRemoveItems += OnRemoveTradeItems;
            RequestManager.OnSearchItems += OnSearchTradeItems;
            RequestManager.OnLoadCredits += OnLoadCredits;

            //Initialize the GUI.
            SetupList();
            SetupAuctionSearch();
            UpdateInfoStats();
        }

        /// <summary>
        /// Load the login credentials and give them to the Request Manager.
        /// </summary>
        public LoginCredentials LoadLoginCredentials()
        {
            return (LoginCredentials)new XmlSerializer(typeof(LoginCredentials)).Deserialize(new XmlTextReader(@"Data\login.xml"));
        }
        /// <summary>
        /// Setup everything to do with the auction search's GUI.
        /// </summary>
        public void SetupAuctionSearch()
        {
            //Add the 'any' alternative.
            cmbFormation.Items.Add("Any");
            cmbPosition.Items.Add("Any");
            cmbNationality.Items.Add("Any");
            cmbLeague.Items.Add("Any");
            cmbClub.Items.Add("Any");

            //Initialize the dropdown lists.
            foreach (Level item in Enum.GetValues(typeof(Level)).Cast<Level>()) { cmbLevel.Items.Add(item); }
            foreach (Formation item in Formation.GetAll()) { cmbFormation.Items.Add(item); }
            foreach (Position item in Position.GetAll()) { cmbPosition.Items.Add(item); }
            foreach (Nation item in Nation.GetAll()) { cmbNationality.Items.Add(item); }
            foreach (League item in League.GetAll()) { cmbLeague.Items.Add(item); }
            foreach (Team item in Team.GetAll()) { cmbClub.Items.Add(item); }

            //Auto select the top option.
            cmbLevel.SelectedIndex = 0;
            cmbFormation.SelectedIndex = 0;
            cmbPosition.SelectedIndex = 0;
            cmbNationality.SelectedIndex = 0;
            cmbLeague.SelectedIndex = 0;
            cmbClub.SelectedIndex = 0;

            //Enable the correct settings.
            lstvAuctionItems.View = View.Details;
            lstvAuctionItems.LabelEdit = false;
            lstvAuctionItems.AllowColumnReorder = false;
            lstvAuctionItems.GridLines = true;
            lstvAuctionItems.FullRowSelect = true;

            //Reset the list view.
            lstvAuctionItems.Clear();

            //Add the columns.
            lstvAuctionItems.Columns.Add("First Name", -2, HorizontalAlignment.Left);
            lstvAuctionItems.Columns.Add("Last Name", -2, HorizontalAlignment.Left);
            lstvAuctionItems.Columns.Add("Rating", -2, HorizontalAlignment.Left);
            lstvAuctionItems.Columns.Add("Position", -2, HorizontalAlignment.Left);
            lstvAuctionItems.Columns.Add("Card Type", -2, HorizontalAlignment.Left);
            lstvAuctionItems.Columns.Add("Bid", -2, HorizontalAlignment.Left);
            lstvAuctionItems.Columns.Add("Time", -2, HorizontalAlignment.Left);
            lstvAuctionItems.Columns.Add("Pace", -2, HorizontalAlignment.Left);
            lstvAuctionItems.Columns.Add("Shooting", -2, HorizontalAlignment.Left);
            lstvAuctionItems.Columns.Add("Passing", -2, HorizontalAlignment.Left);
            lstvAuctionItems.Columns.Add("Dribbling", -2, HorizontalAlignment.Left);
            lstvAuctionItems.Columns.Add("Defending", -2, HorizontalAlignment.Left);
            lstvAuctionItems.Columns.Add("Heading", -2, HorizontalAlignment.Left);
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

            //Remove all items that are not to be updated.
            RemoveItems(lstvItems.Items.Cast<ListViewItem>().Select(x => (PlayerItem)x.Tag).Where(y => !items.Contains(y)).ToList());

            //For all specified items, either add them to the list.
            foreach (PlayerItem item in items)
            {
                //If this item has already been added, just update it. Otherwise create it.
                ListViewItem lstvItem = lstvItems.Items.Count != 0 ? lstvItems.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Tag == item) : null;
                if (lstvItem == null)
                {
                    //Add an item.
                    lstvItem = new ListViewItem(item.ResourceData.FirstName, 0);
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
                else
                {
                    //Just update what's neccessary.
                    lstvItem.SubItems[5].Text = item.Location.ToString();
                    lstvItem.SubItems[6].Text = item.AuctionInfo.CurrentPrice.ToString();
                    lstvItem.SubItems[7].Text = item.AuctionInfo.Expires.ToString();
                }
            }

            //Auto set the width of the columns.
            lstvItems.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstvItems.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            //Continue the drawing of the list view now that the update is complete.
            lstvItems.EndUpdate();

            //Change the item count to reflect the number of items in the selected tab.
            statlblCount.Text = lstvItems.Items.Count + " item(s).";
        }
        /// <summary>
        /// Load a list of auction items.
        /// </summary>
        public void LoadAuctionList(List<PlayerItem> items)
        {
            //Check if this method has been accessed from another thread, ie. not from the GUI thread, and rectify it.
            if (lstvItems.InvokeRequired) { Invoke((MethodInvoker)(() => LoadAuctionList(items))); return; }

            //Pause the drawing of the list view until a later date.
            lstvAuctionItems.BeginUpdate();

            //Remove all items that are not to be updated.
            RemoveItems(lstvAuctionItems.Items.Cast<ListViewItem>().Select(x => (PlayerItem)x.Tag).Where(y => !items.Contains(y)).ToList());

            //For all specified items, either add them to the list.
            foreach (PlayerItem item in items)
            {
                //If this item has already been added, just update it. Otherwise create it.
                ListViewItem lstvItem = lstvAuctionItems.Items.Count != 0 ? lstvAuctionItems.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Tag == item) : null;
                if (lstvItem == null)
                {
                    //Add an item.
                    lstvItem = new ListViewItem(item.ResourceData.FirstName, 0);
                    lstvItem.Tag = item;

                    //The data.
                    lstvItem.SubItems.Add(item.ResourceData.LastName);
                    lstvItem.SubItems.Add(item.ResourceData.Rating.ToString());
                    lstvItem.SubItems.Add(item.AuctionInfo.ItemData.PreferredPosition);
                    lstvItem.SubItems.Add(item.ResourceData.CardType.ToString());
                    lstvItem.SubItems.Add(item.AuctionInfo.CurrentPrice.ToString());
                    lstvItem.SubItems.Add(item.AuctionInfo.Expires.ToString());
                    lstvItem.SubItems.Add(item.ResourceData.Attribute1.ToString());
                    lstvItem.SubItems.Add(item.ResourceData.Attribute2.ToString());
                    lstvItem.SubItems.Add(item.ResourceData.Attribute3.ToString());
                    lstvItem.SubItems.Add(item.ResourceData.Attribute4.ToString());
                    lstvItem.SubItems.Add(item.ResourceData.Attribute5.ToString());
                    lstvItem.SubItems.Add(item.ResourceData.Attribute6.ToString());

                    //Add the item row to the list view.
                    lstvAuctionItems.Items.Add(lstvItem);
                }
                else
                {
                    //Just update what's neccessary.
                    lstvItem.SubItems[5].Text = item.AuctionInfo.CurrentPrice.ToString();
                    lstvItem.SubItems[6].Text = item.AuctionInfo.Expires.ToString();
                }
            }

            //Auto set the width of the columns.
            lstvAuctionItems.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            //Continue the drawing of the list view now that the update is complete.
            lstvAuctionItems.EndUpdate();

            //Change the item count to reflect the number of items in the selected list.
            statlblCount.Text = lstvAuctionItems.Items.Count + " item(s).";
        }
        /// <summary>
        /// Remove the items from the list view.
        /// </summary>
        /// <param name="items">The items to remove.</param>
        public void RemoveItems(List<PlayerItem> items)
        {
            //Check if this method has been accessed from another thread, ie. not from the GUI thread, and rectify it.
            if (lstvItems.InvokeRequired) { Invoke((MethodInvoker)(() => RemoveItems(items))); return; }

            //Remove the items.
            items.ForEach(item =>
            {
                //The trade pile.
                var toRemove = lstvItems.Items.Cast<ListViewItem>().SingleOrDefault(lstvItem => lstvItem.Tag == item);
                if (toRemove != null) { lstvItems.Items.Remove(toRemove); }
            });
        }
        /// <summary>
        /// Filter the list of items.
        /// </summary>
        public void FilterList()
        {
            //Filter the list of items to display.
            var items = DataManager.Items.Values;
            items = !ckbTradePile.Checked ? items.Where(item => item.Location != TradeItemLocation.TradePile).ToList() : items;
            items = !ckbWatchList.Checked ? items.Where(item => item.Location != TradeItemLocation.WatchList).ToList() : items;
            items = !ckbClub.Checked ? items.Where(item => item.Location != TradeItemLocation.Club).ToList() : items;
            items = !ckbUnassigned.Checked ? items.Where(item => item.Location != TradeItemLocation.Unassigned).ToList() : items;
            items = !ckbAuctionItems.Checked ? items.Where(item => item.Location != TradeItemLocation.Auction).ToList() : items;

            //Update the list view.
            LoadList(items.ToList());
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
        /// Update the front page info stats.
        /// </summary>
        public void UpdateInfoStats()
        {
            try
            {
                //Check if this method has been accessed from another thread, ie. not from the GUI thread, and rectify it.
                if (lblInfoItems.InvokeRequired) { Invoke((MethodInvoker)(() => UpdateInfoStats())); return; }

                //Get the data.
                var items = DataManager.Items.Values.ToList();
                var trade = items.Count(x => x.Location == TradeItemLocation.TradePile);
                var watch = items.Count(x => x.Location == TradeItemLocation.WatchList);
                var unassigned = items.Count(x => x.Location == TradeItemLocation.Unassigned);
                var club = items.Count(x => x.Location == TradeItemLocation.Club);
                var spent = 0;
                items.ForEach(x => spent += (int)x.AuctionInfo.ItemData.LastSalePrice);

                //Populate the label with text.
                lblInfoItems.Text = "You own " + (trade + watch + unassigned + club) + " item(s):" +
                    "\n     - " + trade + " in the Trade Pile." +
                        "\n     - " + watch + " in the Watch List." +
                        "\n     - " + unassigned + " in the Unassigned Pile." +
                        "\n     - " + club + " in the Club." +
                        "\n\nThey are worth (ie. you have spent) " + spent + " coins on them." +
                        "\nYour total wealth is thus " + (spent + int.Parse(lblCredits.Text.Split(' ')[1])) + " coins.";

                //Get the data.
                var stats = DataManager.Statistics.Values.ToList();
                var sold = stats.Count(x => x.Sold.Year > 1);
                var soldFor = 0;
                stats.ForEach(x => soldFor += (int)x.SoldFor);
                var first = stats.Where(x => x.Sold.Year > 1).OrderBy(x => x.Sold).ToList();
                var days = first != null ? (int)Math.Ceiling(DateTime.Now.Subtract(first[0].Sold).TotalDays) : 1;
                lblInfoStats.Text = "A total of " + sold + " players have been sold for a collective sum of " +
                    soldFor + " coins (" + (soldFor / days) + " coins/day)." + "\n\nTop five sold players:";

                //The toplist.
                var toplist = new Dictionary<long, int>();
                foreach (var playerSold in stats.Where(x => x.Sold.Year > 1))
                {
                    if (toplist.ContainsKey(playerSold.ResourceID)) { toplist[playerSold.ResourceID]++; }
                    else { toplist.Add(playerSold.ResourceID, 1); }
                }
                int count = 0;
                foreach (var i in toplist.ToList().OrderBy(x => x.Value).Reverse())
                {
                    if (count >= 5) { break; }

                    var name = DataManager.ResourceDataExists(i.Key) ? DataManager.ResourceData[i.Key].FirstName + " " +
                        DataManager.ResourceData[i.Key].LastName : "invalid";
                    lblInfoStats.Text += "\n     - " + name + " (" + i.Value + ")";

                    count++;
                }
            }
            catch { }
        }

        /// <summary>
        /// The trade pile has been loaded, display the changes on the list view as well.
        /// </summary>
        public void OnLoadTradePile(List<PlayerItem> tradeItems)
        {
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
            FilterList();

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
            FilterList();

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
            FilterList();

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
            FilterList();

            //These items have been updated; make sure their stats are also up to date.
            foreach (var item in tradeItems)
            {
                //Get the stats and update them if necessary.
                var stats = DataManager.GetStat(item.AuctionInfo.ItemData.Id);
                if (stats.Acquired.Year <= 1)
                {
                    stats.ResourceID = item.AuctionInfo.ItemData.ResourceId;
                    stats.Acquired = DateTime.Now;
                    stats.AcquiredFor = (int)item.AuctionInfo.ItemData.LastSalePrice;
                    DataManager.AddOrUpdate(stats);
                }
            }

            //Update the front page stats.
            UpdateInfoStats();

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
            FilterList();

            //Log the items.
            Log("Moved " + tradeItems.Count + @" items to trade pile.\line", true);
            tradeItems.ForEach(item => Log("\t- " + item.ResourceData.FirstName + " " + item.ResourceData.LastName + "\t\t\t(" + item.AuctionInfo.TradeId + @")\line",
                false));
        }
        /// <summary>
        /// If any trade items have been auctioned, log them.
        /// </summary>
        public void OnAuctionTradeItems(List<PlayerItem> tradeItems)
        {
            //These items have been auctioned; log the stats.
            foreach (var item in tradeItems)
            {
                //Get the stats and update them.
                var stats = DataManager.GetStat(item.AuctionInfo.ItemData.Id);
                stats.TimesAuctioned++;
            }

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
            foreach (var item in tradeItems)
            {
                //Get the stats and update them.
                var stats = DataManager.GetStat(item.AuctionInfo.ItemData.Id);
                stats.Sold = DateTime.Now;
                stats.SoldFor = (int)item.AuctionInfo.CurrentPrice;
            }

            //Update the front page stats.
            UpdateInfoStats();

            FilterList();

            //Log the items.
            Log("Sold " + tradeItems.Count + @" items.\line", true);
            tradeItems.ForEach(item => Log("\t- " + item.ResourceData.FirstName + " " + item.ResourceData.LastName + " for " +
                item.AuctionInfo.CurrentPrice + "\t\t\t(" + item.AuctionInfo.TradeId + @")\line",
                false));
        }
        /// <summary>
        /// If any trade items have been found after an auction search.
        /// </summary>
        /// <param name="tradeItems">The found trade items.</param>
        public void OnSearchTradeItems(List<PlayerItem> tradeItems)
        {
            LoadAuctionList(tradeItems);
        }
        /// <summary>
        /// If the amount of credits and unopened packs have been loaded, display them.
        /// </summary>
        /// <param name="credits">The amount of credits and unopened packs.</param>
        public void OnLoadCredits(CreditsResponse credits)
        {
            //Check if this method has been accessed from another thread, ie. not from the GUI thread, and rectify it.
            if (lblCredits.InvokeRequired) { Invoke((MethodInvoker)(() => OnLoadCredits(credits))); return; }

            //Update the label.
            lblCredits.Text = "Credits: " + credits.Credits;
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
            FilterList();
        }
        /// <summary>
        /// If an item in the list view has been selected, display it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemSelectionChanged(object sender, EventArgs e)
        {
            //If no item is selected, stop here.
            if ((sender as ListView).SelectedItems.Count == 0) { return; }

            //Get the item.
            _SelectedItem = (PlayerItem)(sender as ListView).SelectedItems[0].Tag;

            //Display the full name.
            lblItemName.Text = _SelectedItem.ResourceData.FirstName + " " + _SelectedItem.ResourceData.LastName;

            //Display some statistics.
            lblTimesAuctioned.Text = "Times Auctioned: " + DataManager.GetStat(_SelectedItem.AuctionInfo.ItemData.Id).TimesAuctioned;

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
        /// <summary>
        /// Start or stop the bot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMainStart_Click(object sender, EventArgs e)
        {
            //Start the bot.
            LogicManager.Start();

            //Jump to the log.
            tabctrlMain.SelectedIndex = 3;

            //Log the event.
            Log(@"Program started!\line", true);
        }
        /// <summary>
        /// Search for auctions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var page = (uint)numStartPage.Value;
            var level = (Level)Enum.Parse(typeof(Level), cmbLevel.SelectedItem.ToString());
            var formation = cmbFormation.SelectedItem.Equals("Any") ? null : (Formation)cmbFormation.SelectedItem;
            var league = cmbFormation.SelectedItem.Equals("Any") ? null : (League)cmbLeague.SelectedItem;
            var nation = cmbNationality.SelectedItem.Equals("Any") ? null : (Nation)cmbNationality.SelectedItem;
            var position = cmbPosition.SelectedItem.Equals("Any") ? null : (Position)cmbPosition.SelectedItem;
            var team = cmbClub.SelectedItem.Equals("Any") ? null : (Team)cmbClub.SelectedItem;

            //Setup the search parameters.
            var searchParameters = new PlayerSearchParameters
            {
                Page = page,
                Level = level,
                Formation = formation != null ? formation.Value : "",
                League = league != null ? league.Value : 0,
                Nation = nation != null ? nation.Value : 0,
                Position = position != null ? position.Value : "",
                Team = team != null ? team.Value : 0
            };

            //Make the search.
            RequestManager.SearchItems(searchParameters);

            //Jump to the list of items.
            tabctrlAuction.SelectedTab = tabAuctionItems;
        }
    }
}
