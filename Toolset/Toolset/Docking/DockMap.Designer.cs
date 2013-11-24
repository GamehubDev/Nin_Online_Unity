namespace Toolset.Docking
{
    partial class DockMap
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DockMap));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.maoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtName = new System.Windows.Forms.ToolStripTextBox();
            this.mnuProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSize = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuTabSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTabClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTabCloseAllBut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTabCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTabFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMap = new Toolset.Controls.Scroll.ScrollMap();
            this.mnuMain.SuspendLayout();
            this.mnuTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.maoToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(5, 5);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(274, 24);
            this.mnuMain.TabIndex = 1;
            this.mnuMain.Text = "menuStrip1";
            this.mnuMain.Visible = false;
            // 
            // maoToolStripMenuItem
            // 
            this.maoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtName,
            this.mnuProperties,
            this.toolStripSeparator1,
            this.mnuSize,
            this.mnuGrid,
            this.mnuExport});
            this.maoToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.maoToolStripMenuItem.MergeIndex = 2;
            this.maoToolStripMenuItem.Name = "maoToolStripMenuItem";
            this.maoToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.maoToolStripMenuItem.Text = "Map";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Enabled = false;
            this.txtName.ForeColor = System.Drawing.Color.DarkGray;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 16);
            this.txtName.Text = "[Map Name]";
            // 
            // mnuProperties
            // 
            this.mnuProperties.Image = global::Toolset.Icons.cog;
            this.mnuProperties.Name = "mnuProperties";
            this.mnuProperties.Size = new System.Drawing.Size(160, 22);
            this.mnuProperties.Text = "Properties";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // mnuSize
            // 
            this.mnuSize.Image = global::Toolset.Icons.size;
            this.mnuSize.Name = "mnuSize";
            this.mnuSize.Size = new System.Drawing.Size(160, 22);
            this.mnuSize.Text = "Edit Map Size";
            // 
            // mnuExport
            // 
            this.mnuExport.Enabled = false;
            this.mnuExport.Image = global::Toolset.Icons.folder_picture;
            this.mnuExport.Name = "mnuExport";
            this.mnuExport.Size = new System.Drawing.Size(160, 22);
            this.mnuExport.Text = "Export as image";
            // 
            // mnuGrid
            // 
            this.mnuGrid.Image = global::Toolset.Icons.grid_icon;
            this.mnuGrid.Name = "mnuGrid";
            this.mnuGrid.Size = new System.Drawing.Size(160, 22);
            this.mnuGrid.Text = "Show Grid";
            // 
            // mnuTab
            // 
            this.mnuTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTabSave,
            this.toolStripSeparator10,
            this.mnuTabClose,
            this.mnuTabCloseAllBut,
            this.mnuTabCloseAll,
            this.toolStripSeparator11,
            this.mnuTabFolder});
            this.mnuTab.Name = "mnuTab";
            this.mnuTab.Size = new System.Drawing.Size(202, 126);
            // 
            // mnuTabSave
            // 
            this.mnuTabSave.Image = global::Toolset.Icons.disk;
            this.mnuTabSave.Name = "mnuTabSave";
            this.mnuTabSave.Size = new System.Drawing.Size(201, 22);
            this.mnuTabSave.Text = "Save [Map Name]";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(198, 6);
            // 
            // mnuTabClose
            // 
            this.mnuTabClose.Name = "mnuTabClose";
            this.mnuTabClose.Size = new System.Drawing.Size(201, 22);
            this.mnuTabClose.Text = "Close";
            // 
            // mnuTabCloseAllBut
            // 
            this.mnuTabCloseAllBut.Enabled = false;
            this.mnuTabCloseAllBut.Name = "mnuTabCloseAllBut";
            this.mnuTabCloseAllBut.Size = new System.Drawing.Size(201, 22);
            this.mnuTabCloseAllBut.Text = "Close All But This";
            // 
            // mnuTabCloseAll
            // 
            this.mnuTabCloseAll.Enabled = false;
            this.mnuTabCloseAll.Name = "mnuTabCloseAll";
            this.mnuTabCloseAll.Size = new System.Drawing.Size(201, 22);
            this.mnuTabCloseAll.Text = "Close All";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(198, 6);
            // 
            // mnuTabFolder
            // 
            this.mnuTabFolder.Name = "mnuTabFolder";
            this.mnuTabFolder.Size = new System.Drawing.Size(201, 22);
            this.mnuTabFolder.Text = "Open Containing Folder";
            // 
            // viewMap
            // 
            this.viewMap.BackColor = System.Drawing.Color.White;
            this.viewMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewMap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewMap.Location = new System.Drawing.Point(5, 29);
            this.viewMap.Map = null;
            this.viewMap.MouseX = 0;
            this.viewMap.MouseXOffset = 0;
            this.viewMap.MouseY = 0;
            this.viewMap.MouseYOffset = 0;
            this.viewMap.Name = "viewMap";
            this.viewMap.ObjectHeight = 0;
            this.viewMap.ObjectWidth = 0;
            this.viewMap.RenderWindow = null;
            this.viewMap.Size = new System.Drawing.Size(274, 228);
            this.viewMap.TabIndex = 0;
            this.viewMap.TileGrid = false;
            this.viewMap.TileX = 0;
            this.viewMap.TileY = 0;
            this.viewMap.View = null;
            this.viewMap.XOffset = 0;
            this.viewMap.YOffset = 0;
            // 
            // DockMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.viewMap);
            this.Controls.Add(this.mnuMain);
            this.DockAreas = CrystalLib.Toolset.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.Name = "DockMap";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabPageContextMenuStrip = this.mnuTab;
            this.Text = "Map Name";
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.mnuTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.Scroll.ScrollMap viewMap;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem maoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSize;
        private System.Windows.Forms.ToolStripMenuItem mnuProperties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuExport;
        private System.Windows.Forms.ToolStripTextBox txtName;
        private System.Windows.Forms.ToolStripMenuItem mnuGrid;
        private System.Windows.Forms.ContextMenuStrip mnuTab;
        private System.Windows.Forms.ToolStripMenuItem mnuTabSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem mnuTabClose;
        private System.Windows.Forms.ToolStripMenuItem mnuTabCloseAllBut;
        private System.Windows.Forms.ToolStripMenuItem mnuTabCloseAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem mnuTabFolder;
    }
}