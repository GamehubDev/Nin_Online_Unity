namespace Toolset.Docking
{
    partial class DockProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DockProject));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripSplitButton();
            this.btnNewMap = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewTileset = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewTerrain = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnRename = new System.Windows.Forms.ToolStripButton();
            this.btnProperties = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.treeProject = new Toolset.Controls.DoubleBuffer.DblTreeView();
            this.imgIcons = new System.Windows.Forms.ImageList(this.components);
            this.contextProject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain.SuspendLayout();
            this.contextProject.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnRename,
            this.btnProperties,
            this.btnDelete});
            this.toolStripMain.Location = new System.Drawing.Point(5, 432);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(274, 25);
            this.toolStripMain.TabIndex = 11;
            this.toolStripMain.Text = "Standard";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewMap,
            this.btnNewTileset,
            this.btnNewTerrain});
            this.btnNew.Image = global::Toolset.Icons.application_view_icons;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(32, 22);
            this.btnNew.Text = "New Item";
            // 
            // btnNewMap
            // 
            this.btnNewMap.Image = global::Toolset.Icons.map;
            this.btnNewMap.Name = "btnNewMap";
            this.btnNewMap.Size = new System.Drawing.Size(111, 22);
            this.btnNewMap.Text = "Map";
            // 
            // btnNewTileset
            // 
            this.btnNewTileset.Image = global::Toolset.Icons.images;
            this.btnNewTileset.Name = "btnNewTileset";
            this.btnNewTileset.Size = new System.Drawing.Size(111, 22);
            this.btnNewTileset.Text = "Tileset";
            // 
            // btnNewTerrain
            // 
            this.btnNewTerrain.Image = global::Toolset.Icons.brick;
            this.btnNewTerrain.Name = "btnNewTerrain";
            this.btnNewTerrain.Size = new System.Drawing.Size(111, 22);
            this.btnNewTerrain.Text = "Terrain";
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Toolset.Icons.folder;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.Text = "Open";
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
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = global::Toolset.Icons.bin_empty;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Delete";
            // 
            // treeProject
            // 
            this.treeProject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeProject.HideSelection = false;
            this.treeProject.ImageIndex = 0;
            this.treeProject.ImageList = this.imgIcons;
            this.treeProject.Indent = 20;
            this.treeProject.ItemHeight = 20;
            this.treeProject.Location = new System.Drawing.Point(5, 5);
            this.treeProject.Name = "treeProject";
            this.treeProject.SelectedImageIndex = 0;
            this.treeProject.ShowLines = false;
            this.treeProject.Size = new System.Drawing.Size(274, 427);
            this.treeProject.TabIndex = 12;
            // 
            // imgIcons
            // 
            this.imgIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgIcons.ImageStream")));
            this.imgIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgIcons.Images.SetKeyName(0, "cog.png");
            this.imgIcons.Images.SetKeyName(1, "folder.png");
            this.imgIcons.Images.SetKeyName(2, "map.png");
            this.imgIcons.Images.SetKeyName(3, "images.png");
            this.imgIcons.Images.SetKeyName(4, "brick.png");
            // 
            // contextProject
            // 
            this.contextProject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen,
            this.mnuRename,
            this.mnuDelete,
            this.mnuProperties});
            this.contextProject.Name = "contextProject";
            this.contextProject.Size = new System.Drawing.Size(128, 92);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Image = global::Toolset.Icons.folder;
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(127, 22);
            this.mnuOpen.Text = "Open";
            // 
            // mnuRename
            // 
            this.mnuRename.Image = global::Toolset.Icons.textfield_rename;
            this.mnuRename.Name = "mnuRename";
            this.mnuRename.Size = new System.Drawing.Size(127, 22);
            this.mnuRename.Text = "Rename";
            // 
            // mnuDelete
            // 
            this.mnuDelete.Image = global::Toolset.Icons.bin_empty;
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(127, 22);
            this.mnuDelete.Text = "Delete";
            // 
            // mnuProperties
            // 
            this.mnuProperties.Image = global::Toolset.Icons.cog;
            this.mnuProperties.Name = "mnuProperties";
            this.mnuProperties.Size = new System.Drawing.Size(127, 22);
            this.mnuProperties.Text = "Properties";
            // 
            // DockProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 462);
            this.Controls.Add(this.treeProject);
            this.Controls.Add(this.toolStripMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DockProject";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Project Explorer";
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.contextProject.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripSplitButton btnNew;
        private System.Windows.Forms.ToolStripMenuItem btnNewTileset;
        private System.Windows.Forms.ToolStripMenuItem btnNewMap;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnRename;
        private System.Windows.Forms.ToolStripButton btnProperties;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private Controls.DoubleBuffer.DblTreeView treeProject;
        private System.Windows.Forms.ImageList imgIcons;
        private System.Windows.Forms.ContextMenuStrip contextProject;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuRename;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuProperties;
        private System.Windows.Forms.ToolStripMenuItem btnNewTerrain;
    }
}