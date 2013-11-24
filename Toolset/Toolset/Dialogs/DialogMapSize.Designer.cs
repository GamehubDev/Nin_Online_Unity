using Toolset.Enums;

namespace Toolset.Dialogs
{
    partial class DialogMapSize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogMapSize));
            this.panelFooter = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupMap = new System.Windows.Forms.GroupBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTileHeight = new System.Windows.Forms.Label();
            this.lblTileWidth = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.anchorSelection = new Toolset.Controls.Anchor.AnchorSelection();
            this.spinTileHeight = new System.Windows.Forms.NumericUpDown();
            this.spinTileWidth = new System.Windows.Forms.NumericUpDown();
            this.chkRelative = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.spinHeight = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.spinWidth = new System.Windows.Forms.NumericUpDown();
            this.lblNewSize = new System.Windows.Forms.Label();
            this.panelFooter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupMap.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinTileHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTileWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.SystemColors.Control;
            this.panelFooter.Controls.Add(this.panel1);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 353);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(396, 48);
            this.panelFooter.TabIndex = 23;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(187, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 48);
            this.panel1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(13, 11);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 26);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(107, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 26);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupMap
            // 
            this.groupMap.Controls.Add(this.lblHeight);
            this.groupMap.Controls.Add(this.lblSize);
            this.groupMap.Controls.Add(this.lblWidth);
            this.groupMap.Location = new System.Drawing.Point(12, 12);
            this.groupMap.Name = "groupMap";
            this.groupMap.Size = new System.Drawing.Size(183, 86);
            this.groupMap.TabIndex = 24;
            this.groupMap.TabStop = false;
            this.groupMap.Text = "Map Size -";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(15, 55);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(61, 15);
            this.lblHeight.TabIndex = 12;
            this.lblHeight.Text = "Height: 14";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize.Location = new System.Drawing.Point(66, 2);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(67, 12);
            this.lblSize.TabIndex = 11;
            this.lblSize.Text = "640 x 480 pixels";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(15, 27);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(57, 15);
            this.lblWidth.TabIndex = 0;
            this.lblWidth.Text = "Width: 19";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTileHeight);
            this.groupBox1.Controls.Add(this.lblTileWidth);
            this.groupBox1.Location = new System.Drawing.Point(201, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 86);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tile Size";
            // 
            // lblTileHeight
            // 
            this.lblTileHeight.AutoSize = true;
            this.lblTileHeight.Location = new System.Drawing.Point(15, 55);
            this.lblTileHeight.Name = "lblTileHeight";
            this.lblTileHeight.Size = new System.Drawing.Size(83, 15);
            this.lblTileHeight.TabIndex = 12;
            this.lblTileHeight.Text = "Tile Height: 32";
            // 
            // lblTileWidth
            // 
            this.lblTileWidth.AutoSize = true;
            this.lblTileWidth.Location = new System.Drawing.Point(15, 27);
            this.lblTileWidth.Name = "lblTileWidth";
            this.lblTileWidth.Size = new System.Drawing.Size(79, 15);
            this.lblTileWidth.TabIndex = 0;
            this.lblTileWidth.Text = "Tile Width: 32";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.anchorSelection);
            this.groupBox2.Controls.Add(this.spinTileHeight);
            this.groupBox2.Controls.Add(this.spinTileWidth);
            this.groupBox2.Controls.Add(this.chkRelative);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.spinHeight);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.spinWidth);
            this.groupBox2.Controls.Add(this.lblNewSize);
            this.groupBox2.Location = new System.Drawing.Point(12, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 237);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "New Size -";
            // 
            // anchorSelection
            // 
            this.anchorSelection.AnchorPoint = AnchorPoints.Center;
            this.anchorSelection.Location = new System.Drawing.Point(138, 116);
            this.anchorSelection.Name = "anchorSelection";
            this.anchorSelection.Size = new System.Drawing.Size(102, 103);
            this.anchorSelection.TabIndex = 25;
            this.anchorSelection.xOffset = 0;
            this.anchorSelection.yOffset = 0;
            // 
            // spinTileHeight
            // 
            this.spinTileHeight.Location = new System.Drawing.Point(263, 59);
            this.spinTileHeight.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.spinTileHeight.Name = "spinTileHeight";
            this.spinTileHeight.Size = new System.Drawing.Size(73, 23);
            this.spinTileHeight.TabIndex = 23;
            this.spinTileHeight.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // spinTileWidth
            // 
            this.spinTileWidth.Location = new System.Drawing.Point(263, 27);
            this.spinTileWidth.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.spinTileWidth.Name = "spinTileWidth";
            this.spinTileWidth.Size = new System.Drawing.Size(73, 23);
            this.spinTileWidth.TabIndex = 22;
            this.spinTileWidth.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // chkRelative
            // 
            this.chkRelative.AutoSize = true;
            this.chkRelative.Enabled = false;
            this.chkRelative.Location = new System.Drawing.Point(18, 87);
            this.chkRelative.Name = "chkRelative";
            this.chkRelative.Size = new System.Drawing.Size(67, 19);
            this.chkRelative.TabIndex = 21;
            this.chkRelative.Text = "Relative";
            this.chkRelative.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(186, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "Tile Height:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(186, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 15);
            this.label9.TabIndex = 18;
            this.label9.Text = "Tile Width:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "Height:";
            // 
            // spinHeight
            // 
            this.spinHeight.Location = new System.Drawing.Point(92, 58);
            this.spinHeight.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.spinHeight.Name = "spinHeight";
            this.spinHeight.Size = new System.Drawing.Size(73, 23);
            this.spinHeight.TabIndex = 15;
            this.spinHeight.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "Width:";
            // 
            // spinWidth
            // 
            this.spinWidth.Location = new System.Drawing.Point(92, 26);
            this.spinWidth.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.spinWidth.Name = "spinWidth";
            this.spinWidth.Size = new System.Drawing.Size(73, 23);
            this.spinWidth.TabIndex = 13;
            this.spinWidth.Value = new decimal(new int[] {
            19,
            0,
            0,
            0});
            // 
            // lblNewSize
            // 
            this.lblNewSize.AutoSize = true;
            this.lblNewSize.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewSize.Location = new System.Drawing.Point(66, 2);
            this.lblNewSize.Name = "lblNewSize";
            this.lblNewSize.Size = new System.Drawing.Size(67, 12);
            this.lblNewSize.TabIndex = 12;
            this.lblNewSize.Text = "640 x 480 pixels";
            // 
            // DialogMapSize
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(396, 401);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupMap);
            this.Controls.Add(this.panelFooter);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogMapSize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Map Size";
            this.panelFooter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupMap.ResumeLayout(false);
            this.groupMap.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinTileHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTileWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupMap;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTileHeight;
        private System.Windows.Forms.Label lblTileWidth;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblNewSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown spinHeight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown spinWidth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkRelative;
        private System.Windows.Forms.NumericUpDown spinTileHeight;
        private System.Windows.Forms.NumericUpDown spinTileWidth;
        private Controls.Anchor.AnchorSelection anchorSelection;
    }
}