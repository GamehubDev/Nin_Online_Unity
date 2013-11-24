namespace Toolset.Docking
{
    partial class DockConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DockConsole));
            this.lstConsole = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstConsole
            // 
            this.lstConsole.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName});
            this.lstConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstConsole.FullRowSelect = true;
            this.lstConsole.GridLines = true;
            this.lstConsole.Location = new System.Drawing.Point(5, 5);
            this.lstConsole.Name = "lstConsole";
            this.lstConsole.Size = new System.Drawing.Size(767, 234);
            this.lstConsole.TabIndex = 0;
            this.lstConsole.UseCompatibleStateImageBehavior = false;
            this.lstConsole.View = System.Windows.Forms.View.Details;
            // 
            // columnName
            // 
            this.columnName.Text = "Output";
            this.columnName.Width = 744;
            // 
            // toolStripMain
            // 
            this.toolStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDelete});
            this.toolStripMain.Location = new System.Drawing.Point(5, 239);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(767, 25);
            this.toolStripMain.TabIndex = 15;
            this.toolStripMain.Text = "Standard";
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
            // DockConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 269);
            this.Controls.Add(this.lstConsole);
            this.Controls.Add(this.toolStripMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DockConsole";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Console";
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstConsole;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ColumnHeader columnName;
    }
}