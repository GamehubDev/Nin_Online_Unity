namespace Toolset.Dialogs
{
    partial class DialogMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogMap));
            this.groupMap = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.spinTileHeight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.spinTileWidth = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.spinHeight = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.spinWidth = new System.Windows.Forms.NumericUpDown();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupMap.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinTileHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTileWidth)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinWidth)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupMap
            // 
            this.groupMap.Controls.Add(this.txtName);
            this.groupMap.Controls.Add(this.label1);
            this.groupMap.Location = new System.Drawing.Point(12, 12);
            this.groupMap.Name = "groupMap";
            this.groupMap.Size = new System.Drawing.Size(372, 63);
            this.groupMap.TabIndex = 17;
            this.groupMap.TabStop = false;
            this.groupMap.Text = "Map";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(97, 23);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(250, 23);
            this.txtName.TabIndex = 1;
            this.txtName.Text = "New Map";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.spinTileHeight);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.spinTileWidth);
            this.groupBox3.Location = new System.Drawing.Point(201, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(183, 96);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tile Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Tile Height:";
            // 
            // spinTileHeight
            // 
            this.spinTileHeight.Location = new System.Drawing.Point(92, 54);
            this.spinTileHeight.Name = "spinTileHeight";
            this.spinTileHeight.Size = new System.Drawing.Size(73, 23);
            this.spinTileHeight.TabIndex = 8;
            this.spinTileHeight.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tile Width:";
            // 
            // spinTileWidth
            // 
            this.spinTileWidth.Location = new System.Drawing.Point(92, 23);
            this.spinTileWidth.Name = "spinTileWidth";
            this.spinTileWidth.Size = new System.Drawing.Size(73, 23);
            this.spinTileWidth.TabIndex = 6;
            this.spinTileWidth.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblSize);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.spinHeight);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.spinWidth);
            this.groupBox2.Location = new System.Drawing.Point(12, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(183, 96);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Map Size - ";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize.Location = new System.Drawing.Point(66, 2);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(67, 12);
            this.lblSize.TabIndex = 10;
            this.lblSize.Text = "640 x 480 pixels";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "Height:";
            // 
            // spinHeight
            // 
            this.spinHeight.Location = new System.Drawing.Point(92, 57);
            this.spinHeight.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.spinHeight.Name = "spinHeight";
            this.spinHeight.Size = new System.Drawing.Size(73, 23);
            this.spinHeight.TabIndex = 8;
            this.spinHeight.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Width:";
            // 
            // spinWidth
            // 
            this.spinWidth.Location = new System.Drawing.Point(92, 25);
            this.spinWidth.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.spinWidth.Name = "spinWidth";
            this.spinWidth.Size = new System.Drawing.Size(73, 23);
            this.spinWidth.TabIndex = 6;
            this.spinWidth.Value = new decimal(new int[] {
            19,
            0,
            0,
            0});
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.SystemColors.Control;
            this.panelFooter.Controls.Add(this.panel1);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 192);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(397, 48);
            this.panelFooter.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(188, 0);
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
            // DialogMap
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(397, 240);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.groupMap);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Map";
            this.groupMap.ResumeLayout(false);
            this.groupMap.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinTileHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTileWidth)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinWidth)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupMap;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown spinTileHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown spinTileWidth;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown spinHeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown spinWidth;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}