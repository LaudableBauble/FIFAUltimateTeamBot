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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ckbAuctionItems = new System.Windows.Forms.CheckBox();
            this.ckbUnassigned = new System.Windows.Forms.CheckBox();
            this.ckbClub = new System.Windows.Forms.CheckBox();
            this.ckbWatchList = new System.Windows.Forms.CheckBox();
            this.ckbTradePile = new System.Windows.Forms.CheckBox();
            this.lstvItems = new System.Windows.Forms.ListView();
            this.tabTradePile = new System.Windows.Forms.TabPage();
            this.lstvTradePile = new System.Windows.Forms.ListView();
            this.tabWatchlist = new System.Windows.Forms.TabPage();
            this.lstvWatchList = new System.Windows.Forms.ListView();
            this.tabClub = new System.Windows.Forms.TabPage();
            this.lstvClub = new System.Windows.Forms.ListView();
            this.tabAuctionSearch = new System.Windows.Forms.TabPage();
            this.tabSelectedItem = new System.Windows.Forms.TabPage();
            this.lblFullName = new System.Windows.Forms.Label();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.rtxbLog = new System.Windows.Forms.RichTextBox();
            this.statMain = new System.Windows.Forms.StatusStrip();
            this.statlblCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblItemName = new System.Windows.Forms.Label();
            this.ckbCanBeSold = new System.Windows.Forms.CheckBox();
            this.mnuMain.SuspendLayout();
            this.tabctrlMain.SuspendLayout();
            this.tabItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabTradePile.SuspendLayout();
            this.tabWatchlist.SuspendLayout();
            this.tabClub.SuspendLayout();
            this.tabSelectedItem.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.statMain.SuspendLayout();
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
            this.tabctrlMain.Controls.Add(this.tabTradePile);
            this.tabctrlMain.Controls.Add(this.tabWatchlist);
            this.tabctrlMain.Controls.Add(this.tabClub);
            this.tabctrlMain.Controls.Add(this.tabAuctionSearch);
            this.tabctrlMain.Controls.Add(this.tabSelectedItem);
            this.tabctrlMain.Controls.Add(this.tabLog);
            this.tabctrlMain.Location = new System.Drawing.Point(0, 27);
            this.tabctrlMain.Name = "tabctrlMain";
            this.tabctrlMain.SelectedIndex = 0;
            this.tabctrlMain.Size = new System.Drawing.Size(884, 510);
            this.tabctrlMain.TabIndex = 1;
            // 
            // tabItems
            // 
            this.tabItems.Controls.Add(this.splitContainer1);
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
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(3, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstvItems);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ckbCanBeSold);
            this.splitContainer1.Panel2.Controls.Add(this.lblItemName);
            this.splitContainer1.Size = new System.Drawing.Size(870, 432);
            this.splitContainer1.SplitterDistance = 615;
            this.splitContainer1.TabIndex = 0;
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
            // lstvItems
            // 
            this.lstvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstvItems.Location = new System.Drawing.Point(0, 0);
            this.lstvItems.Name = "lstvItems";
            this.lstvItems.Size = new System.Drawing.Size(615, 432);
            this.lstvItems.TabIndex = 1;
            this.lstvItems.UseCompatibleStateImageBehavior = false;
            // 
            // tabTradePile
            // 
            this.tabTradePile.Controls.Add(this.lstvTradePile);
            this.tabTradePile.Location = new System.Drawing.Point(4, 22);
            this.tabTradePile.Name = "tabTradePile";
            this.tabTradePile.Padding = new System.Windows.Forms.Padding(3);
            this.tabTradePile.Size = new System.Drawing.Size(876, 484);
            this.tabTradePile.TabIndex = 0;
            this.tabTradePile.Text = "Trade Pile";
            this.tabTradePile.UseVisualStyleBackColor = true;
            // 
            // lstvTradePile
            // 
            this.lstvTradePile.Location = new System.Drawing.Point(3, 3);
            this.lstvTradePile.Name = "lstvTradePile";
            this.lstvTradePile.Size = new System.Drawing.Size(870, 478);
            this.lstvTradePile.TabIndex = 0;
            this.lstvTradePile.UseCompatibleStateImageBehavior = false;
            // 
            // tabWatchlist
            // 
            this.tabWatchlist.Controls.Add(this.lstvWatchList);
            this.tabWatchlist.Location = new System.Drawing.Point(4, 22);
            this.tabWatchlist.Name = "tabWatchlist";
            this.tabWatchlist.Size = new System.Drawing.Size(876, 484);
            this.tabWatchlist.TabIndex = 3;
            this.tabWatchlist.Text = "Watch List";
            this.tabWatchlist.UseVisualStyleBackColor = true;
            // 
            // lstvWatchList
            // 
            this.lstvWatchList.Location = new System.Drawing.Point(3, 3);
            this.lstvWatchList.Name = "lstvWatchList";
            this.lstvWatchList.Size = new System.Drawing.Size(870, 478);
            this.lstvWatchList.TabIndex = 1;
            this.lstvWatchList.UseCompatibleStateImageBehavior = false;
            // 
            // tabClub
            // 
            this.tabClub.Controls.Add(this.lstvClub);
            this.tabClub.Location = new System.Drawing.Point(4, 22);
            this.tabClub.Name = "tabClub";
            this.tabClub.Size = new System.Drawing.Size(876, 484);
            this.tabClub.TabIndex = 5;
            this.tabClub.Text = "Club Items";
            this.tabClub.UseVisualStyleBackColor = true;
            // 
            // lstvClub
            // 
            this.lstvClub.Location = new System.Drawing.Point(3, 3);
            this.lstvClub.Name = "lstvClub";
            this.lstvClub.Size = new System.Drawing.Size(870, 478);
            this.lstvClub.TabIndex = 2;
            this.lstvClub.UseCompatibleStateImageBehavior = false;
            // 
            // tabAuctionSearch
            // 
            this.tabAuctionSearch.Location = new System.Drawing.Point(4, 22);
            this.tabAuctionSearch.Name = "tabAuctionSearch";
            this.tabAuctionSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabAuctionSearch.Size = new System.Drawing.Size(876, 484);
            this.tabAuctionSearch.TabIndex = 1;
            this.tabAuctionSearch.Text = "Auction Search";
            this.tabAuctionSearch.UseVisualStyleBackColor = true;
            // 
            // tabSelectedItem
            // 
            this.tabSelectedItem.Controls.Add(this.lblFullName);
            this.tabSelectedItem.Location = new System.Drawing.Point(4, 22);
            this.tabSelectedItem.Name = "tabSelectedItem";
            this.tabSelectedItem.Size = new System.Drawing.Size(876, 484);
            this.tabSelectedItem.TabIndex = 2;
            this.tabSelectedItem.Text = "Selected Item";
            this.tabSelectedItem.UseVisualStyleBackColor = true;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(247, 99);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(35, 13);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "label1";
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
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(3, 36);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(35, 13);
            this.lblItemName.TabIndex = 0;
            this.lblItemName.Text = "label1";
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabTradePile.ResumeLayout(false);
            this.tabWatchlist.ResumeLayout(false);
            this.tabClub.ResumeLayout(false);
            this.tabSelectedItem.ResumeLayout(false);
            this.tabSelectedItem.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.statMain.ResumeLayout(false);
            this.statMain.PerformLayout();
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
        private System.Windows.Forms.TabPage tabTradePile;
        private System.Windows.Forms.TabPage tabAuctionSearch;
        private System.Windows.Forms.StatusStrip statMain;
        private System.Windows.Forms.ListView lstvTradePile;
        private System.Windows.Forms.TabPage tabWatchlist;
        private System.Windows.Forms.TabPage tabSelectedItem;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.ListView lstvWatchList;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.RichTextBox rtxbLog;
        private System.Windows.Forms.TabPage tabClub;
        private System.Windows.Forms.ListView lstvClub;
        private System.Windows.Forms.ToolStripStatusLabel statlblCount;
        private System.Windows.Forms.TabPage tabItems;
        private System.Windows.Forms.ListView lstvItems;
        private System.Windows.Forms.CheckBox ckbAuctionItems;
        private System.Windows.Forms.CheckBox ckbUnassigned;
        private System.Windows.Forms.CheckBox ckbClub;
        private System.Windows.Forms.CheckBox ckbWatchList;
        private System.Windows.Forms.CheckBox ckbTradePile;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.CheckBox ckbCanBeSold;
    }
}

