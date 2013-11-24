namespace Toolset.Dialogs
{
    partial class DialogTerrain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogTerrain));
            this.panelFooter = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupTileset = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTileset = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.viewTileset = new Toolset.Controls.Scroll.ScrollSelect();
            this.panelFooter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupTileset.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.SystemColors.Control;
            this.panelFooter.Controls.Add(this.panel1);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 492);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(509, 48);
            this.panelFooter.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(300, 0);
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
            // groupTileset
            // 
            this.groupTileset.Controls.Add(this.label3);
            this.groupTileset.Controls.Add(this.cmbTileset);
            this.groupTileset.Controls.Add(this.label2);
            this.groupTileset.Controls.Add(this.cmbType);
            this.groupTileset.Controls.Add(this.txtName);
            this.groupTileset.Controls.Add(this.label1);
            this.groupTileset.Location = new System.Drawing.Point(12, 12);
            this.groupTileset.Name = "groupTileset";
            this.groupTileset.Size = new System.Drawing.Size(485, 92);
            this.groupTileset.TabIndex = 18;
            this.groupTileset.TabStop = false;
            this.groupTileset.Text = "Terrain";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 26;
            this.label3.Text = "Tileset:";
            // 
            // cmbTileset
            // 
            this.cmbTileset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTileset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTileset.FormattingEnabled = true;
            this.cmbTileset.Items.AddRange(new object[] {
            "RM Condensed - Cliff",
            "RM Condensed - Ground",
            "RM Large"});
            this.cmbTileset.Location = new System.Drawing.Point(237, 56);
            this.cmbTileset.Name = "cmbTileset";
            this.cmbTileset.Size = new System.Drawing.Size(234, 23);
            this.cmbTileset.Sorted = true;
            this.cmbTileset.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 15);
            this.label2.TabIndex = 24;
            this.label2.Text = "Type:";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "RM Condensed - Ground",
            "RM Condensed - Cliff",
            "RM Large"});
            this.cmbType.Location = new System.Drawing.Point(59, 56);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(122, 23);
            this.cmbType.TabIndex = 23;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(59, 27);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(412, 23);
            this.txtName.TabIndex = 1;
            this.txtName.Text = "New Terrain";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // viewTileset
            // 
            this.viewTileset.BackColor = System.Drawing.Color.White;
            this.viewTileset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewTileset.Location = new System.Drawing.Point(12, 116);
            this.viewTileset.MouseX = 0;
            this.viewTileset.MouseXOffset = 0;
            this.viewTileset.MouseY = 0;
            this.viewTileset.MouseYOffset = 0;
            this.viewTileset.Name = "viewTileset";
            this.viewTileset.ObjectHeight = 0;
            this.viewTileset.ObjectWidth = 0;
            this.viewTileset.RenderWindow = null;
            this.viewTileset.SelectionHeight = 0;
            this.viewTileset.SelectionWidth = 0;
            this.viewTileset.SelectionX = 0;
            this.viewTileset.SelectionY = 0;
            this.viewTileset.Size = new System.Drawing.Size(485, 363);
            this.viewTileset.Sprite = null;
            this.viewTileset.TabIndex = 19;
            this.viewTileset.Texture = null;
            this.viewTileset.TileHeight = 0;
            this.viewTileset.TileWidth = 0;
            this.viewTileset.View = null;
            this.viewTileset.XOffset = 0;
            this.viewTileset.YOffset = 0;
            // 
            // DialogTerrain
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(509, 540);
            this.Controls.Add(this.viewTileset);
            this.Controls.Add(this.groupTileset);
            this.Controls.Add(this.panelFooter);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogTerrain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Terrain";
            this.panelFooter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupTileset.ResumeLayout(false);
            this.groupTileset.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupTileset;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTileset;
        private Controls.Scroll.ScrollSelect viewTileset;

    }
}