using Toolset.Controls;
using Toolset.Controls.FixedListView;

namespace Toolset.Docking
{
    partial class DockTerrain
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Beach");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Dead Grass");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Dirt Water");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Grass");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Lava");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Tiled Water");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Volcanic Water");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Water");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Water 2");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DockTerrain));
            this.lstTerrain = new Toolset.Controls.FixedListView.FixedListView();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnRename = new System.Windows.Forms.ToolStripButton();
            this.btnProperties = new System.Windows.Forms.ToolStripButton();
            this.btnDuplicate = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.contextTerrain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain.SuspendLayout();
            this.contextTerrain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstTerrain
            // 
            this.lstTerrain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTerrain.HideSelection = false;
            this.lstTerrain.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9});
            this.lstTerrain.Location = new System.Drawing.Point(5, 5);
            this.lstTerrain.MultiSelect = false;
            this.lstTerrain.Name = "lstTerrain";
            this.lstTerrain.ShowItemToolTips = true;
            this.lstTerrain.Size = new System.Drawing.Size(247, 298);
            this.lstTerrain.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstTerrain.TabIndex = 0;
            this.lstTerrain.TileSize = new System.Drawing.Size(38, 38);
            this.lstTerrain.UseCompatibleStateImageBehavior = false;
            this.lstTerrain.View = System.Windows.Forms.View.Tile;
            // 
            // toolStripMain
            // 
            this.toolStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnRename,
            this.btnProperties,
            this.btnDuplicate,
            this.btnDelete});
            this.toolStripMain.Location = new System.Drawing.Point(5, 303);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(247, 25);
            this.toolStripMain.TabIndex = 19;
            this.toolStripMain.Text = "Standard";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Toolset.Icons.page_white;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.Text = "New";
            // 
            // btnRename
            // 
            this.btnRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRename.Image = global::Toolset.Icons.textfield_rename;
            this.btnRename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(23, 22);
            this.btnRename.Text = "Rename";
            // 
            // btnProperties
            // 
            this.btnProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProperties.Image = global::Toolset.Icons.cog;
            this.btnProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(23, 22);
            this.btnProperties.Text = "Properties";
            // 
            // btnDuplicate
            // 
            this.btnDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDuplicate.Image = global::Toolset.Icons.shape_move_backwards;
            this.btnDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDuplicate.Name = "btnDuplicate";
            this.btnDuplicate.Size = new System.Drawing.Size(23, 22);
            this.btnDuplicate.Text = "Duplicate";
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = global::Toolset.Icons.bin_empty;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Delete";
            // 
            // contextTerrain
            // 
            this.contextTerrain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRename,
            this.mnuProperties,
            this.mnuDuplicate,
            this.mnuDelete});
            this.contextTerrain.Name = "contextLayers";
            this.contextTerrain.Size = new System.Drawing.Size(128, 92);
            // 
            // mnuRename
            // 
            this.mnuRename.Image = global::Toolset.Icons.textfield_rename;
            this.mnuRename.Name = "mnuRename";
            this.mnuRename.Size = new System.Drawing.Size(127, 22);
            this.mnuRename.Text = "Rename";
            // 
            // mnuProperties
            // 
            this.mnuProperties.Image = global::Toolset.Icons.cog;
            this.mnuProperties.Name = "mnuProperties";
            this.mnuProperties.Size = new System.Drawing.Size(127, 22);
            this.mnuProperties.Text = "Properties";
            // 
            // mnuDuplicate
            // 
            this.mnuDuplicate.Image = global::Toolset.Icons.shape_move_backwards;
            this.mnuDuplicate.Name = "mnuDuplicate";
            this.mnuDuplicate.Size = new System.Drawing.Size(127, 22);
            this.mnuDuplicate.Text = "Duplicate";
            // 
            // mnuDelete
            // 
            this.mnuDelete.Image = global::Toolset.Icons.bin_empty;
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(127, 22);
            this.mnuDelete.Text = "Delete";
            // 
            // DockTerrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 333);
            this.Controls.Add(this.lstTerrain);
            this.Controls.Add(this.toolStripMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DockTerrain";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Terrain";
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.contextTerrain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FixedListView lstTerrain;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnRename;
        private System.Windows.Forms.ToolStripButton btnProperties;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnDuplicate;
        private System.Windows.Forms.ContextMenuStrip contextTerrain;
        private System.Windows.Forms.ToolStripMenuItem mnuRename;
        private System.Windows.Forms.ToolStripMenuItem mnuProperties;
        private System.Windows.Forms.ToolStripMenuItem mnuDuplicate;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
    }
}