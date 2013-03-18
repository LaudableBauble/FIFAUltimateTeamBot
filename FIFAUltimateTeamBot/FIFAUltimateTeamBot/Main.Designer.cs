namespace FIFAUltimateTeamBot
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabctrlMain = new System.Windows.Forms.TabControl();
            this.tabItems = new System.Windows.Forms.TabPage();
            this.btnExpandCollapseItem = new System.Windows.Forms.Button();
            this.splitItems = new System.Windows.Forms.SplitContainer();
            this.lstvItems = new System.Windows.Forms.ListView();
            this.ckbCanBeSold = new System.Windows.Forms.CheckBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.ckbAuctionItems = new System.Windows.Forms.CheckBox();
            this.ckbUnassigned = new System.Windows.Forms.CheckBox();
            this.ckbClub = new System.Windows.Forms.CheckBox();
            this.ckbWatchList = new System.Windows.Forms.CheckBox();
            this.ckbTradePile = new System.Windows.Forms.CheckBox();
            this.tabAuctionSearch = new System.Windows.Forms.TabPage();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.rtxbLog = new System.Windows.Forms.RichTextBox();
            this.statMain = new System.Windows.Forms.StatusStrip();
            this.statlblCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblFormation = new System.Windows.Forms.Label();
            this.cmbFormation = new System.Windows.Forms.ComboBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.cmbPosition = new System.Windows.Forms.ComboBox();
            this.lblNationality = new System.Windows.Forms.Label();
            this.cmbNationality = new System.Windows.Forms.ComboBox();
            this.lblLeague = new System.Windows.Forms.Label();
            this.cmbLeague = new System.Windows.Forms.ComboBox();
            this.lblClub = new System.Windows.Forms.Label();
            this.cmbClub = new System.Windows.Forms.ComboBox();
            this.numBidMin = new System.Windows.Forms.NumericUpDown();
            this.lblBidMin = new System.Windows.Forms.Label();
            this.lblBidMax = new System.Windows.Forms.Label();
            this.numBidMax = new System.Windows.Forms.NumericUpDown();
            this.lblBuyNowMin = new System.Windows.Forms.Label();
            this.numBuyNowMin = new System.Windows.Forms.NumericUpDown();
            this.lblBuyNowMax = new System.Windows.Forms.Label();
            this.numBuyNowMax = new System.Windows.Forms.NumericUpDown();
            this.grpbPricing = new System.Windows.Forms.GroupBox();
            this.grpbPages = new System.Windows.Forms.GroupBox();
            this.numStartPage = new System.Windows.Forms.NumericUpDown();
            this.lblStartPage = new System.Windows.Forms.Label();
            this.numPageCount = new System.Windows.Forms.NumericUpDown();
            this.lblPageCount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.mnuMain.SuspendLayout();
            this.tabctrlMain.SuspendLayout();
            this.tabItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitItems)).BeginInit();
            this.splitItems.Panel1.SuspendLayout();
            this.splitItems.Panel2.SuspendLayout();
            this.splitItems.SuspendLayout();
            this.tabAuctionSearch.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.statMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBidMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBidMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBuyNowMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBuyNowMax)).BeginInit();
            this.grpbPricing.SuspendLayout();
            this.grpbPages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStartPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPageCount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.viewMenuItem,
            this.helpMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(884, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "File";
            // 
            // editMenuItem
            // 
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editMenuItem.Text = "Edit";
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewMenuItem.Text = "View";
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "Help";
            // 
            // tabctrlMain
            // 
            this.tabctrlMain.Controls.Add(this.tabItems);
            this.tabctrlMain.Controls.Add(this.tabAuctionSearch);
            this.tabctrlMain.Controls.Add(this.tabLog);
            this.tabctrlMain.Location = new System.Drawing.Point(0, 27);
            this.tabctrlMain.Name = "tabctrlMain";
            this.tabctrlMain.SelectedIndex = 0;
            this.tabctrlMain.Size = new System.Drawing.Size(884, 510);
            this.tabctrlMain.TabIndex = 1;
            // 
            // tabItems
            // 
            this.tabItems.Controls.Add(this.btnExpandCollapseItem);
            this.tabItems.Controls.Add(this.splitItems);
            this.tabItems.Controls.Add(this.ckbAuctionItems);
            this.tabItems.Controls.Add(this.ckbUnassigned);
            this.tabItems.Controls.Add(this.ckbClub);
            this.tabItems.Controls.Add(this.ckbWatchList);
            this.tabItems.Controls.Add(this.ckbTradePile);
            this.tabItems.Location = new System.Drawing.Point(4, 22);
            this.tabItems.Name = "tabItems";
            this.tabItems.Size = new System.Drawing.Size(876, 484);
            this.tabItems.TabIndex = 6;
            this.tabItems.Text = "Items";
            this.tabItems.UseVisualStyleBackColor = true;
            // 
            // btnExpandCollapseItem
            // 
            this.btnExpandCollapseItem.Location = new System.Drawing.Point(811, 22);
            this.btnExpandCollapseItem.Name = "btnExpandCollapseItem";
            this.btnExpandCollapseItem.Size = new System.Drawing.Size(57, 23);
            this.btnExpandCollapseItem.TabIndex = 7;
            this.btnExpandCollapseItem.Text = "--->";
            this.btnExpandCollapseItem.UseVisualStyleBackColor = true;
            this.btnExpandCollapseItem.Click += new System.EventHandler(this.OnExpandOrCollapseItem);
            // 
            // splitItems
            // 
            this.splitItems.Location = new System.Drawing.Point(3, 49);
            this.splitItems.Name = "splitItems";
            // 
            // splitItems.Panel1
            // 
            this.splitItems.Panel1.Controls.Add(this.lstvItems);
            // 
            // splitItems.Panel2
            // 
            this.splitItems.Panel2.Controls.Add(this.ckbCanBeSold);
            this.splitItems.Panel2.Controls.Add(this.lblItemName);
            this.splitItems.Size = new System.Drawing.Size(870, 432);
            this.splitItems.SplitterDistance = 615;
            this.splitItems.TabIndex = 0;
            // 
            // lstvItems
            // 
            this.lstvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstvItems.Location = new System.Drawing.Point(0, 0);
            this.lstvItems.Name = "lstvItems";
            this.lstvItems.Size = new System.Drawing.Size(615, 432);
            this.lstvItems.TabIndex = 1;
            this.lstvItems.UseCompatibleStateImageBehavior = false;
            // 
            // ckbCanBeSold
            // 
            this.ckbCanBeSold.AutoSize = true;
            this.ckbCanBeSold.Location = new System.Drawing.Point(6, 301);
            this.ckbCanBeSold.Name = "ckbCanBeSold";
            this.ckbCanBeSold.Size = new System.Drawing.Size(82, 17);
            this.ckbCanBeSold.TabIndex = 7;
            this.ckbCanBeSold.Text = "Can be sold";
            this.ckbCanBeSold.UseVisualStyleBackColor = true;
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(3, 36);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(35, 13);
            this.lblItemName.TabIndex = 0;
            this.lblItemName.Text = "label1";
            // 
            // ckbAuctionItems
            // 
            this.ckbAuctionItems.AutoSize = true;
            this.ckbAuctionItems.Location = new System.Drawing.Point(175, 3);
            this.ckbAuctionItems.Name = "ckbAuctionItems";
            this.ckbAuctionItems.Size = new System.Drawing.Size(90, 17);
            this.ckbAuctionItems.TabIndex = 6;
            this.ckbAuctionItems.Text = "Auction Items";
            this.ckbAuctionItems.UseVisualStyleBackColor = true;
            // 
            // ckbUnassigned
            // 
            this.ckbUnassigned.AutoSize = true;
            this.ckbUnassigned.Location = new System.Drawing.Point(89, 26);
            this.ckbUnassigned.Name = "ckbUnassigned";
            this.ckbUnassigned.Size = new System.Drawing.Size(110, 17);
            this.ckbUnassigned.TabIndex = 5;
            this.ckbUnassigned.Text = "Unassigned Items";
            this.ckbUnassigned.UseVisualStyleBackColor = true;
            // 
            // ckbClub
            // 
            this.ckbClub.AutoSize = true;
            this.ckbClub.Location = new System.Drawing.Point(89, 3);
            this.ckbClub.Name = "ckbClub";
            this.ckbClub.Size = new System.Drawing.Size(75, 17);
            this.ckbClub.TabIndex = 4;
            this.ckbClub.Text = "Club Items";
            this.ckbClub.UseVisualStyleBackColor = true;
            // 
            // ckbWatchList
            // 
            this.ckbWatchList.AutoSize = true;
            this.ckbWatchList.Location = new System.Drawing.Point(3, 26);
            this.ckbWatchList.Name = "ckbWatchList";
            this.ckbWatchList.Size = new System.Drawing.Size(77, 17);
            this.ckbWatchList.TabIndex = 3;
            this.ckbWatchList.Text = "Watch List";
            this.ckbWatchList.UseVisualStyleBackColor = true;
            // 
            // ckbTradePile
            // 
            this.ckbTradePile.AutoSize = true;
            this.ckbTradePile.Location = new System.Drawing.Point(3, 3);
            this.ckbTradePile.Name = "ckbTradePile";
            this.ckbTradePile.Size = new System.Drawing.Size(74, 17);
            this.ckbTradePile.TabIndex = 2;
            this.ckbTradePile.Text = "Trade Pile";
            this.ckbTradePile.UseVisualStyleBackColor = true;
            // 
            // tabAuctionSearch
            // 
            this.tabAuctionSearch.Controls.Add(this.btnReset);
            this.tabAuctionSearch.Controls.Add(this.btnSearch);
            this.tabAuctionSearch.Controls.Add(this.groupBox1);
            this.tabAuctionSearch.Controls.Add(this.grpbPages);
            this.tabAuctionSearch.Controls.Add(this.grpbPricing);
            this.tabAuctionSearch.Location = new System.Drawing.Point(4, 22);
            this.tabAuctionSearch.Name = "tabAuctionSearch";
            this.tabAuctionSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabAuctionSearch.Size = new System.Drawing.Size(876, 484);
            this.tabAuctionSearch.TabIndex = 1;
            this.tabAuctionSearch.Text = "Auction Search";
            this.tabAuctionSearch.UseVisualStyleBackColor = true;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.rtxbLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Size = new System.Drawing.Size(876, 484);
            this.tabLog.TabIndex = 4;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // rtxbLog
            // 
            this.rtxbLog.Location = new System.Drawing.Point(3, 3);
            this.rtxbLog.Name = "rtxbLog";
            this.rtxbLog.ReadOnly = true;
            this.rtxbLog.Size = new System.Drawing.Size(870, 478);
            this.rtxbLog.TabIndex = 0;
            this.rtxbLog.Text = "";
            // 
            // statMain
            // 
            this.statMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statlblCount});
            this.statMain.Location = new System.Drawing.Point(0, 540);
            this.statMain.Name = "statMain";
            this.statMain.Size = new System.Drawing.Size(884, 22);
            this.statMain.TabIndex = 2;
            this.statMain.Text = "statusStrip1";
            // 
            // statlblCount
            // 
            this.statlblCount.Name = "statlblCount";
            this.statlblCount.Size = new System.Drawing.Size(67, 17);
            this.statlblCount.Text = "Item Count";
            // 
            // cmbLevel
            // 
            this.cmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(6, 60);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(136, 21);
            this.cmbLevel.TabIndex = 0;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(50, 44);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(33, 13);
            this.lblLevel.TabIndex = 1;
            this.lblLevel.Text = "Level";
            // 
            // lblFormation
            // 
            this.lblFormation.AutoSize = true;
            this.lblFormation.Location = new System.Drawing.Point(215, 44);
            this.lblFormation.Name = "lblFormation";
            this.lblFormation.Size = new System.Drawing.Size(53, 13);
            this.lblFormation.TabIndex = 3;
            this.lblFormation.Text = "Formation";
            // 
            // cmbFormation
            // 
            this.cmbFormation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormation.FormattingEnabled = true;
            this.cmbFormation.Location = new System.Drawing.Point(171, 60);
            this.cmbFormation.Name = "cmbFormation";
            this.cmbFormation.Size = new System.Drawing.Size(136, 21);
            this.cmbFormation.TabIndex = 2;
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(380, 44);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(44, 13);
            this.lblPosition.TabIndex = 5;
            this.lblPosition.Text = "Position";
            // 
            // cmbPosition
            // 
            this.cmbPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPosition.FormattingEnabled = true;
            this.cmbPosition.Location = new System.Drawing.Point(336, 60);
            this.cmbPosition.Name = "cmbPosition";
            this.cmbPosition.Size = new System.Drawing.Size(136, 21);
            this.cmbPosition.TabIndex = 4;
            // 
            // lblNationality
            // 
            this.lblNationality.AutoSize = true;
            this.lblNationality.Location = new System.Drawing.Point(50, 104);
            this.lblNationality.Name = "lblNationality";
            this.lblNationality.Size = new System.Drawing.Size(56, 13);
            this.lblNationality.TabIndex = 7;
            this.lblNationality.Text = "Nationality";
            // 
            // cmbNationality
            // 
            this.cmbNationality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNationality.FormattingEnabled = true;
            this.cmbNationality.Location = new System.Drawing.Point(6, 120);
            this.cmbNationality.Name = "cmbNationality";
            this.cmbNationality.Size = new System.Drawing.Size(136, 21);
            this.cmbNationality.TabIndex = 6;
            // 
            // lblLeague
            // 
            this.lblLeague.AutoSize = true;
            this.lblLeague.Location = new System.Drawing.Point(215, 104);
            this.lblLeague.Name = "lblLeague";
            this.lblLeague.Size = new System.Drawing.Size(43, 13);
            this.lblLeague.TabIndex = 9;
            this.lblLeague.Text = "League";
            // 
            // cmbLeague
            // 
            this.cmbLeague.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLeague.FormattingEnabled = true;
            this.cmbLeague.Location = new System.Drawing.Point(171, 120);
            this.cmbLeague.Name = "cmbLeague";
            this.cmbLeague.Size = new System.Drawing.Size(136, 21);
            this.cmbLeague.TabIndex = 8;
            // 
            // lblClub
            // 
            this.lblClub.AutoSize = true;
            this.lblClub.Location = new System.Drawing.Point(380, 104);
            this.lblClub.Name = "lblClub";
            this.lblClub.Size = new System.Drawing.Size(28, 13);
            this.lblClub.TabIndex = 11;
            this.lblClub.Text = "Club";
            // 
            // cmbClub
            // 
            this.cmbClub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClub.FormattingEnabled = true;
            this.cmbClub.Location = new System.Drawing.Point(336, 120);
            this.cmbClub.Name = "cmbClub";
            this.cmbClub.Size = new System.Drawing.Size(136, 21);
            this.cmbClub.TabIndex = 10;
            // 
            // numBidMin
            // 
            this.numBidMin.Location = new System.Drawing.Point(119, 43);
            this.numBidMin.Name = "numBidMin";
            this.numBidMin.Size = new System.Drawing.Size(68, 20);
            this.numBidMin.TabIndex = 12;
            // 
            // lblBidMin
            // 
            this.lblBidMin.AutoSize = true;
            this.lblBidMin.Location = new System.Drawing.Point(13, 45);
            this.lblBidMin.Name = "lblBidMin";
            this.lblBidMin.Size = new System.Drawing.Size(69, 13);
            this.lblBidMin.TabIndex = 13;
            this.lblBidMin.Text = "Min Bid Price";
            // 
            // lblBidMax
            // 
            this.lblBidMax.AutoSize = true;
            this.lblBidMax.Location = new System.Drawing.Point(13, 71);
            this.lblBidMax.Name = "lblBidMax";
            this.lblBidMax.Size = new System.Drawing.Size(72, 13);
            this.lblBidMax.TabIndex = 15;
            this.lblBidMax.Text = "Max Bid Price";
            // 
            // numBidMax
            // 
            this.numBidMax.Location = new System.Drawing.Point(119, 69);
            this.numBidMax.Name = "numBidMax";
            this.numBidMax.Size = new System.Drawing.Size(68, 20);
            this.numBidMax.TabIndex = 14;
            // 
            // lblBuyNowMin
            // 
            this.lblBuyNowMin.AutoSize = true;
            this.lblBuyNowMin.Location = new System.Drawing.Point(13, 97);
            this.lblBuyNowMin.Name = "lblBuyNowMin";
            this.lblBuyNowMin.Size = new System.Drawing.Size(97, 13);
            this.lblBuyNowMin.TabIndex = 17;
            this.lblBuyNowMin.Text = "Min Buy Now Price";
            // 
            // numBuyNowMin
            // 
            this.numBuyNowMin.Location = new System.Drawing.Point(119, 95);
            this.numBuyNowMin.Name = "numBuyNowMin";
            this.numBuyNowMin.Size = new System.Drawing.Size(68, 20);
            this.numBuyNowMin.TabIndex = 16;
            // 
            // lblBuyNowMax
            // 
            this.lblBuyNowMax.AutoSize = true;
            this.lblBuyNowMax.Location = new System.Drawing.Point(13, 123);
            this.lblBuyNowMax.Name = "lblBuyNowMax";
            this.lblBuyNowMax.Size = new System.Drawing.Size(100, 13);
            this.lblBuyNowMax.TabIndex = 19;
            this.lblBuyNowMax.Text = "Max Buy Now Price";
            // 
            // numBuyNowMax
            // 
            this.numBuyNowMax.Location = new System.Drawing.Point(119, 121);
            this.numBuyNowMax.Name = "numBuyNowMax";
            this.numBuyNowMax.Size = new System.Drawing.Size(68, 20);
            this.numBuyNowMax.TabIndex = 18;
            // 
            // grpbPricing
            // 
            this.grpbPricing.Controls.Add(this.numBuyNowMax);
            this.grpbPricing.Controls.Add(this.lblBuyNowMax);
            this.grpbPricing.Controls.Add(this.numBidMin);
            this.grpbPricing.Controls.Add(this.lblBidMin);
            this.grpbPricing.Controls.Add(this.lblBuyNowMin);
            this.grpbPricing.Controls.Add(this.numBidMax);
            this.grpbPricing.Controls.Add(this.numBuyNowMin);
            this.grpbPricing.Controls.Add(this.lblBidMax);
            this.grpbPricing.Location = new System.Drawing.Point(8, 159);
            this.grpbPricing.Name = "grpbPricing";
            this.grpbPricing.Size = new System.Drawing.Size(193, 147);
            this.grpbPricing.TabIndex = 20;
            this.grpbPricing.TabStop = false;
            this.grpbPricing.Text = "Pricing";
            // 
            // grpbPages
            // 
            this.grpbPages.Controls.Add(this.numStartPage);
            this.grpbPages.Controls.Add(this.lblStartPage);
            this.grpbPages.Controls.Add(this.numPageCount);
            this.grpbPages.Controls.Add(this.lblPageCount);
            this.grpbPages.Location = new System.Drawing.Point(207, 159);
            this.grpbPages.Name = "grpbPages";
            this.grpbPages.Size = new System.Drawing.Size(193, 147);
            this.grpbPages.TabIndex = 21;
            this.grpbPages.TabStop = false;
            this.grpbPages.Text = "Pages";
            // 
            // numStartPage
            // 
            this.numStartPage.Location = new System.Drawing.Point(127, 95);
            this.numStartPage.Name = "numStartPage";
            this.numStartPage.Size = new System.Drawing.Size(60, 20);
            this.numStartPage.TabIndex = 12;
            // 
            // lblStartPage
            // 
            this.lblStartPage.AutoSize = true;
            this.lblStartPage.Location = new System.Drawing.Point(32, 97);
            this.lblStartPage.Name = "lblStartPage";
            this.lblStartPage.Size = new System.Drawing.Size(71, 13);
            this.lblStartPage.TabIndex = 13;
            this.lblStartPage.Text = "Starting Page";
            // 
            // numPageCount
            // 
            this.numPageCount.Location = new System.Drawing.Point(127, 121);
            this.numPageCount.Name = "numPageCount";
            this.numPageCount.Size = new System.Drawing.Size(60, 20);
            this.numPageCount.TabIndex = 14;
            // 
            // lblPageCount
            // 
            this.lblPageCount.AutoSize = true;
            this.lblPageCount.Location = new System.Drawing.Point(32, 123);
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.Size = new System.Drawing.Size(89, 13);
            this.lblPageCount.TabIndex = 15;
            this.lblPageCount.Text = "Number of Pages";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbLevel);
            this.groupBox1.Controls.Add(this.lblLevel);
            this.groupBox1.Controls.Add(this.cmbFormation);
            this.groupBox1.Controls.Add(this.lblClub);
            this.groupBox1.Controls.Add(this.lblFormation);
            this.groupBox1.Controls.Add(this.cmbClub);
            this.groupBox1.Controls.Add(this.cmbPosition);
            this.groupBox1.Controls.Add(this.lblLeague);
            this.groupBox1.Controls.Add(this.lblPosition);
            this.groupBox1.Controls.Add(this.cmbLeague);
            this.groupBox1.Controls.Add(this.cmbNationality);
            this.groupBox1.Controls.Add(this.lblNationality);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 147);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(406, 274);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 32);
            this.btnSearch.TabIndex = 23;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(406, 237);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 32);
            this.btnReset.TabIndex = 24;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.statMain);
            this.Controls.Add(this.tabctrlMain);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "Main";
            this.Text = "FIFA 13 Ultimate Team Bot";
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.tabctrlMain.ResumeLayout(false);
            this.tabItems.ResumeLayout(false);
            this.tabItems.PerformLayout();
            this.splitItems.Panel1.ResumeLayout(false);
            this.splitItems.Panel2.ResumeLayout(false);
            this.splitItems.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitItems)).EndInit();
            this.splitItems.ResumeLayout(false);
            this.tabAuctionSearch.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.statMain.ResumeLayout(false);
            this.statMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBidMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBidMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBuyNowMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBuyNowMax)).EndInit();
            this.grpbPricing.ResumeLayout(false);
            this.grpbPricing.PerformLayout();
            this.grpbPages.ResumeLayout(false);
            this.grpbPages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStartPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPageCount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.TabControl tabctrlMain;
        private System.Windows.Forms.TabPage tabAuctionSearch;
        private System.Windows.Forms.StatusStrip statMain;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.RichTextBox rtxbLog;
        private System.Windows.Forms.ToolStripStatusLabel statlblCount;
        private System.Windows.Forms.TabPage tabItems;
        private System.Windows.Forms.ListView lstvItems;
        private System.Windows.Forms.CheckBox ckbAuctionItems;
        private System.Windows.Forms.CheckBox ckbUnassigned;
        private System.Windows.Forms.CheckBox ckbClub;
        private System.Windows.Forms.CheckBox ckbWatchList;
        private System.Windows.Forms.CheckBox ckbTradePile;
        private System.Windows.Forms.SplitContainer splitItems;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.CheckBox ckbCanBeSold;
        private System.Windows.Forms.Button btnExpandCollapseItem;
        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblClub;
        private System.Windows.Forms.ComboBox cmbClub;
        private System.Windows.Forms.Label lblLeague;
        private System.Windows.Forms.ComboBox cmbLeague;
        private System.Windows.Forms.Label lblNationality;
        private System.Windows.Forms.ComboBox cmbNationality;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.ComboBox cmbPosition;
        private System.Windows.Forms.Label lblFormation;
        private System.Windows.Forms.ComboBox cmbFormation;
        private System.Windows.Forms.NumericUpDown numBidMin;
        private System.Windows.Forms.Label lblBidMin;
        private System.Windows.Forms.Label lblBuyNowMax;
        private System.Windows.Forms.NumericUpDown numBuyNowMax;
        private System.Windows.Forms.Label lblBuyNowMin;
        private System.Windows.Forms.NumericUpDown numBuyNowMin;
        private System.Windows.Forms.Label lblBidMax;
        private System.Windows.Forms.NumericUpDown numBidMax;
        private System.Windows.Forms.GroupBox grpbPricing;
        private System.Windows.Forms.GroupBox grpbPages;
        private System.Windows.Forms.NumericUpDown numStartPage;
        private System.Windows.Forms.Label lblStartPage;
        private System.Windows.Forms.NumericUpDown numPageCount;
        private System.Windows.Forms.Label lblPageCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReset;
    }
}

