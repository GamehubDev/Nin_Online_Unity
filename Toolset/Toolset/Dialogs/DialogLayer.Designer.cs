namespace Toolset.Dialogs
{
    partial class DialogLayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogLayer));
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.spinOpacity = new System.Windows.Forms.NumericUpDown();
            this.groupMap = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbVisible = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinOpacity)).BeginInit();
            this.groupMap.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.SystemColors.Control;
            this.panelFooter.Controls.Add(this.panel1);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 156);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(397, 48);
            this.panelFooter.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Opacity:";
            // 
            // spinOpacity
            // 
            this.spinOpacity.Location = new System.Drawing.Point(92, 25);
            this.spinOpacity.Name = "spinOpacity";
            this.spinOpacity.Size = new System.Drawing.Size(73, 23);
            this.spinOpacity.TabIndex = 6;
            this.spinOpacity.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // groupMap
            // 
            this.groupMap.Controls.Add(this.txtName);
            this.groupMap.Controls.Add(this.label1);
            this.groupMap.Location = new System.Drawing.Point(12, 6);
            this.groupMap.Name = "groupMap";
            this.groupMap.Size = new System.Drawing.Size(372, 63);
            this.groupMap.TabIndex = 23;
            this.groupMap.TabStop = false;
            this.groupMap.Text = "Layer";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(97, 23);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(250, 23);
            this.txtName.TabIndex = 1;
            this.txtName.Text = "New Layer";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbVisible);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.spinOpacity);
            this.groupBox2.Location = new System.Drawing.Point(12, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(371, 64);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Layer Properties";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Visible:";
            // 
            // cmbVisible
            // 
            this.cmbVisible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVisible.FormattingEnabled = true;
            this.cmbVisible.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbVisible.Location = new System.Drawing.Point(247, 25);
            this.cmbVisible.Name = "cmbVisible";
            this.cmbVisible.Size = new System.Drawing.Size(98, 23);
            this.cmbVisible.TabIndex = 10;
            // 
            // DialogLayer
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(397, 204);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.groupMap);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogLayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Layer";
            this.panel1.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinOpacity)).EndInit();
            this.groupMap.ResumeLayout(false);
            this.groupMap.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown spinOpacity;
        private System.Windows.Forms.GroupBox groupMap;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbVisible;
    }
}