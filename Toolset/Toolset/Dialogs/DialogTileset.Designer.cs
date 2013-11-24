namespace Toolset.Dialogs
{
    partial class DialogTileset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogTileset));
            this.groupTileset = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.spinTileHeight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.spinTileWidth = new System.Windows.Forms.NumericUpDown();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupTileset.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinTileHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTileWidth)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupTileset
            // 
            this.groupTileset.Controls.Add(this.btnBrowse);
            this.groupTileset.Controls.Add(this.txtLocation);
            this.groupTileset.Controls.Add(this.label4);
            this.groupTileset.Controls.Add(this.txtName);
            this.groupTileset.Controls.Add(this.label1);
            this.groupTileset.Location = new System.Drawing.Point(14, 14);
            this.groupTileset.Name = "groupTileset";
            this.groupTileset.Size = new System.Drawing.Size(391, 112);
            this.groupTileset.TabIndex = 12;
            this.groupTileset.TabStop = false;
            this.groupTileset.Text = "New Tileset";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(286, 66);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(88, 26);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // txtLocation
            // 
            this.txtLocation.ForeColor = System.Drawing.Color.Black;
            this.txtLocation.Location = new System.Drawing.Point(83, 67);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(196, 23);
            this.txtLocation.TabIndex = 7;
            this.txtLocation.Text = "C:\\Program Files\\Toolset\\Project\\";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Path:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(83, 28);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(291, 23);
            this.txtName.TabIndex = 1;
            this.txtName.Text = "New Tileset";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.spinTileHeight);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.spinTileWidth);
            this.groupBox2.Location = new System.Drawing.Point(14, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(391, 69);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tiles";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tile Height:";
            // 
            // spinTileHeight
            // 
            this.spinTileHeight.Location = new System.Drawing.Point(288, 27);
            this.spinTileHeight.Name = "spinTileHeight";
            this.spinTileHeight.Size = new System.Drawing.Size(86, 23);
            this.spinTileHeight.TabIndex = 4;
            this.spinTileHeight.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tile Width:";
            // 
            // spinTileWidth
            // 
            this.spinTileWidth.Location = new System.Drawing.Point(101, 27);
            this.spinTileWidth.Name = "spinTileWidth";
            this.spinTileWidth.Size = new System.Drawing.Size(86, 23);
            this.spinTileWidth.TabIndex = 0;
            this.spinTileWidth.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.SystemColors.Control;
            this.panelFooter.Controls.Add(this.panel1);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 218);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(419, 48);
            this.panelFooter.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(210, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 48);
            this.panel1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(13, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 26);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(107, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 26);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // DialogTileset
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(419, 266);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.groupTileset);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogTileset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Tileset";
            this.groupTileset.ResumeLayout(false);
            this.groupTileset.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinTileHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTileWidth)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupTileset;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown spinTileHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown spinTileWidth;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}