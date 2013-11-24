namespace Toolset.Docking
{
    partial class DockTileset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DockTileset));
            this.viewTexture = new Toolset.Controls.Scroll.ScrollTileset();
            this.cmbTilesets = new System.Windows.Forms.ComboBox();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnRename = new System.Windows.Forms.ToolStripButton();
            this.btnProperties = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewTexture
            // 
            this.viewTexture.BackColor = System.Drawing.Color.White;
            this.viewTexture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewTexture.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewTexture.Location = new System.Drawing.Point(5, 28);
            this.viewTexture.MouseX = 0;
            this.viewTexture.MouseXOffset = 0;
            this.viewTexture.MouseY = 0;
            this.viewTexture.MouseYOffset = 0;
            this.viewTexture.Name = "viewTexture";
            this.viewTexture.ObjectHeight = 0;
            this.viewTexture.ObjectWidth = 0;
            this.viewTexture.RenderWindow = null;
            this.viewTexture.Size = new System.Drawing.Size(274, 404);
            this.viewTexture.Sprite = null;
            this.viewTexture.TabIndex = 2;
            this.viewTexture.Texture = null;
            this.viewTexture.TileGrid = true;
            this.viewTexture.View = null;
            this.viewTexture.XOffset = 0;
            this.viewTexture.YOffset = 0;
            // 
            // cmbTilesets
            // 
            this.cmbTilesets.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbTilesets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTilesets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTilesets.FormattingEnabled = true;
            this.cmbTilesets.Location = new System.Drawing.Point(5, 5);
            this.cmbTilesets.Name = "cmbTilesets";
            this.cmbTilesets.Size = new System.Drawing.Size(274, 23);
            this.cmbTilesets.Sorted = true;
            this.cmbTilesets.TabIndex = 3;
            // 
            // toolStripMain
            // 
            this.toolStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnRename,
            this.btnProperties,
            this.btnDelete,
            this.toolStripSeparator1,
            this.btnGrid});
            this.toolStripMain.Location = new System.Drawing.Point(5, 432);
            this.toolStripMain.Margin = new System.Windows.Forms.Padding(6);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripMain.Size = new System.Drawing.Size(274, 25);
            this.toolStripMain.TabIndex = 14;
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGrid
            // 
            this.btnGrid.Checked = true;
            this.btnGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGrid.Enabled = false;
            this.btnGrid.Image = global::Toolset.Icons.grid_icon;
            this.btnGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGrid.Name = "btnGrid";
            this.btnGrid.Size = new System.Drawing.Size(23, 22);
            this.btnGrid.Text = "Show/Hide Grid";
            // 
            // DockTileset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 462);
            this.Controls.Add(this.viewTexture);
            this.Controls.Add(this.cmbTilesets);
            this.Controls.Add(this.toolStripMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DockTileset";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Tilesets";
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.Scroll.ScrollTileset viewTexture;
        private System.Windows.Forms.ComboBox cmbTilesets;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnRename;
        private System.Windows.Forms.ToolStripButton btnProperties;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnGrid;
    }
}