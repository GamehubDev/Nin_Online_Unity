using Toolset.Controls;
using Toolset.Controls.FixedListView;

namespace Toolset.Docking
{
    partial class DockHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DockHistory));
            this.lstHistory = new Toolset.Controls.FixedListView.FixedListView();
            this.columnIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgIcons = new System.Windows.Forms.ImageList(this.components);
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnRedo = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstHistory
            // 
            this.lstHistory.AutoArrange = false;
            this.lstHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnIcon,
            this.columnName});
            this.lstHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHistory.FullRowSelect = true;
            this.lstHistory.GridLines = true;
            this.lstHistory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstHistory.HideSelection = false;
            this.lstHistory.Location = new System.Drawing.Point(5, 5);
            this.lstHistory.MultiSelect = false;
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(274, 427);
            this.lstHistory.SmallImageList = this.imgIcons;
            this.lstHistory.TabIndex = 13;
            this.lstHistory.UseCompatibleStateImageBehavior = false;
            this.lstHistory.View = System.Windows.Forms.View.Details;
            // 
            // columnIcon
            // 
            this.columnIcon.Text = "";
            this.columnIcon.Width = 23;
            // 
            // columnName
            // 
            this.columnName.Text = "Action";
            this.columnName.Width = 116;
            // 
            // imgIcons
            // 
            this.imgIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgIcons.ImageStream")));
            this.imgIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgIcons.Images.SetKeyName(0, "paintbrush.png");
            this.imgIcons.Images.SetKeyName(1, "eraser.png");
            this.imgIcons.Images.SetKeyName(2, "paintcan.png");
            this.imgIcons.Images.SetKeyName(3, "bricks.png");
            this.imgIcons.Images.SetKeyName(4, "brick.png");
            this.imgIcons.Images.SetKeyName(5, "map.png");
            // 
            // toolStripMain
            // 
            this.toolStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUndo,
            this.btnRedo,
            this.btnDelete});
            this.toolStripMain.Location = new System.Drawing.Point(5, 432);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(274, 25);
            this.toolStripMain.TabIndex = 14;
            this.toolStripMain.Text = "Standard";
            // 
            // btnUndo
            // 
            this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUndo.Enabled = false;
            this.btnUndo.Image = global::Toolset.Icons.arrow_undo;
            this.btnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(23, 22);
            this.btnUndo.Text = "Undo";
            // 
            // btnRedo
            // 
            this.btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRedo.Enabled = false;
            this.btnRedo.Image = global::Toolset.Icons.arrow_redo;
            this.btnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(23, 22);
            this.btnRedo.Text = "Redo";
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
            // DockHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 462);
            this.Controls.Add(this.lstHistory);
            this.Controls.Add(this.toolStripMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DockHistory";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "History";
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FixedListView lstHistory;
        private System.Windows.Forms.ColumnHeader columnIcon;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripButton btnRedo;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ImageList imgIcons;
    }
}