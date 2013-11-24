using Toolset.Controls;
using Toolset.Controls.FixedListView;

namespace Toolset.Docking
{
    partial class DockLayers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DockLayers));
            this.mnuDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.contextLayers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuUp = new System.Windows.Forms.ToolStripMenuItem();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgIcons = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbOpacity = new Toolset.Controls.CustomComboBox.CustomComboBox();
            this.lblOpacity = new System.Windows.Forms.Label();
            this.btnShowHide = new System.Windows.Forms.ToolStripButton();
            this.lstLayers = new FixedListView();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnDuplicate = new System.Windows.Forms.ToolStripButton();
            this.btnDown = new System.Windows.Forms.ToolStripButton();
            this.btnUp = new System.Windows.Forms.ToolStripButton();
            this.btnRename = new System.Windows.Forms.ToolStripButton();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.pnlPopup = new System.Windows.Forms.Panel();
            this.trackOpacity = new System.Windows.Forms.TrackBar();
            this.contextLayers.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.pnlPopup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackOpacity)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuDown
            // 
            this.mnuDown.Image = global::Toolset.Icons.bullet_down;
            this.mnuDown.Name = "mnuDown";
            this.mnuDown.Size = new System.Drawing.Size(138, 22);
            this.mnuDown.Text = "Move Down";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(135, 6);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Image = global::Toolset.Icons.bin_empty;
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(138, 22);
            this.mnuDelete.Text = "Delete";
            // 
            // mnuDuplicate
            // 
            this.mnuDuplicate.Image = global::Toolset.Icons.shape_move_backwards;
            this.mnuDuplicate.Name = "mnuDuplicate";
            this.mnuDuplicate.Size = new System.Drawing.Size(138, 22);
            this.mnuDuplicate.Text = "Duplicate";
            // 
            // mnuRename
            // 
            this.mnuRename.Image = global::Toolset.Icons.textfield_rename;
            this.mnuRename.Name = "mnuRename";
            this.mnuRename.Size = new System.Drawing.Size(138, 22);
            this.mnuRename.Text = "Rename";
            // 
            // contextLayers
            // 
            this.contextLayers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRename,
            this.mnuDuplicate,
            this.mnuDelete,
            this.toolStripSeparator2,
            this.mnuUp,
            this.mnuDown});
            this.contextLayers.Name = "contextLayers";
            this.contextLayers.Size = new System.Drawing.Size(139, 120);
            // 
            // mnuUp
            // 
            this.mnuUp.Image = global::Toolset.Icons.bullet_up;
            this.mnuUp.Name = "mnuUp";
            this.mnuUp.Size = new System.Drawing.Size(138, 22);
            this.mnuUp.Text = "Move up";
            // 
            // columnName
            // 
            this.columnName.Text = "Layer Name";
            this.columnName.Width = 105;
            // 
            // columnIcon
            // 
            this.columnIcon.Text = "";
            this.columnIcon.Width = 23;
            // 
            // imgIcons
            // 
            this.imgIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgIcons.ImageStream")));
            this.imgIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgIcons.Images.SetKeyName(0, "eye2_disable.png");
            this.imgIcons.Images.SetKeyName(1, "eye2.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbOpacity);
            this.panel1.Controls.Add(this.lblOpacity);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 33);
            this.panel1.TabIndex = 19;
            // 
            // cmbOpacity
            // 
            this.cmbOpacity.AllowResizeDropDown = false;
            this.cmbOpacity.ControlSize = new System.Drawing.Size(1, 1);
            this.cmbOpacity.DropDownControl = null;
            this.cmbOpacity.DropDownSizeMode = Toolset.Controls.CustomComboBox.CustomComboBox.SizeMode.UseControlSize;
            this.cmbOpacity.DropSize = new System.Drawing.Size(121, 106);
            this.cmbOpacity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOpacity.FormattingEnabled = true;
            this.cmbOpacity.Location = new System.Drawing.Point(63, 4);
            this.cmbOpacity.Name = "cmbOpacity";
            this.cmbOpacity.Size = new System.Drawing.Size(73, 23);
            this.cmbOpacity.TabIndex = 4;
            this.cmbOpacity.Text = "100%";
            // 
            // lblOpacity
            // 
            this.lblOpacity.AutoSize = true;
            this.lblOpacity.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblOpacity.Location = new System.Drawing.Point(6, 7);
            this.lblOpacity.Name = "lblOpacity";
            this.lblOpacity.Size = new System.Drawing.Size(51, 15);
            this.lblOpacity.TabIndex = 1;
            this.lblOpacity.Text = "Opacity:";
            this.lblOpacity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnShowHide
            // 
            this.btnShowHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowHide.Enabled = false;
            this.btnShowHide.Image = global::Toolset.Icons.shape_move_front;
            this.btnShowHide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowHide.Name = "btnShowHide";
            this.btnShowHide.Size = new System.Drawing.Size(23, 22);
            this.btnShowHide.Text = "Show/Hide all other layers";
            // 
            // lstLayers
            // 
            this.lstLayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLayers.CheckBoxes = true;
            this.lstLayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnIcon,
            this.columnName});
            this.lstLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLayers.FullRowSelect = true;
            this.lstLayers.GridLines = true;
            this.lstLayers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstLayers.HideSelection = false;
            this.lstLayers.Location = new System.Drawing.Point(5, 38);
            this.lstLayers.MultiSelect = false;
            this.lstLayers.Name = "lstLayers";
            this.lstLayers.Size = new System.Drawing.Size(241, 394);
            this.lstLayers.StateImageList = this.imgIcons;
            this.lstLayers.TabIndex = 21;
            this.lstLayers.TileSize = new System.Drawing.Size(16, 16);
            this.lstLayers.UseCompatibleStateImageBehavior = false;
            this.lstLayers.View = System.Windows.Forms.View.Details;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // btnDuplicate
            // 
            this.btnDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDuplicate.Image = global::Toolset.Icons.shape_move_backwards;
            this.btnDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDuplicate.Name = "btnDuplicate";
            this.btnDuplicate.Size = new System.Drawing.Size(23, 22);
            this.btnDuplicate.Text = "Duplicate";
            // 
            // btnDown
            // 
            this.btnDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDown.Image = global::Toolset.Icons.bullet_down;
            this.btnDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(23, 22);
            this.btnDown.Text = "Move down";
            // 
            // btnUp
            // 
            this.btnUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUp.Image = global::Toolset.Icons.bullet_up;
            this.btnUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(23, 22);
            this.btnUp.Text = "Move up";
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
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Toolset.Icons.page_white;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.Text = "New";
            // 
            // toolStripMain
            // 
            this.toolStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnRename,
            this.btnUp,
            this.btnDown,
            this.btnDuplicate,
            this.btnDelete,
            this.toolStripSeparator1,
            this.btnShowHide});
            this.toolStripMain.Location = new System.Drawing.Point(5, 432);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(241, 25);
            this.toolStripMain.TabIndex = 18;
            this.toolStripMain.Text = "Standard";
            // 
            // pnlPopup
            // 
            this.pnlPopup.Controls.Add(this.trackOpacity);
            this.pnlPopup.Location = new System.Drawing.Point(68, 38);
            this.pnlPopup.Name = "pnlPopup";
            this.pnlPopup.Size = new System.Drawing.Size(141, 28);
            this.pnlPopup.TabIndex = 22;
            this.pnlPopup.Visible = false;
            // 
            // trackOpacity
            // 
            this.trackOpacity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackOpacity.LargeChange = 1;
            this.trackOpacity.Location = new System.Drawing.Point(0, 0);
            this.trackOpacity.Maximum = 100;
            this.trackOpacity.Name = "trackOpacity";
            this.trackOpacity.Size = new System.Drawing.Size(141, 28);
            this.trackOpacity.TabIndex = 1;
            this.trackOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackOpacity.Value = 100;
            // 
            // DockLayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 462);
            this.Controls.Add(this.pnlPopup);
            this.Controls.Add(this.lstLayers);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStripMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DockLayers";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Layers";
            this.contextLayers.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.pnlPopup.ResumeLayout(false);
            this.pnlPopup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackOpacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem mnuDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuDuplicate;
        private System.Windows.Forms.ToolStripMenuItem mnuRename;
        private System.Windows.Forms.ContextMenuStrip contextLayers;
        private System.Windows.Forms.ToolStripMenuItem mnuUp;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnIcon;
        private System.Windows.Forms.ImageList imgIcons;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblOpacity;
        private System.Windows.Forms.ToolStripButton btnShowHide;
        private FixedListView lstLayers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnDuplicate;
        private System.Windows.Forms.ToolStripButton btnDown;
        private System.Windows.Forms.ToolStripButton btnUp;
        private System.Windows.Forms.ToolStripButton btnRename;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private Controls.CustomComboBox.CustomComboBox cmbOpacity;
        private System.Windows.Forms.Panel pnlPopup;
        private System.Windows.Forms.TrackBar trackOpacity;
    }
}