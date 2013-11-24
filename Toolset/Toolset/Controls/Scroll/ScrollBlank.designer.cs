namespace Toolset.Controls.Scroll
{
    partial class ScrollBlank
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picBlank = new System.Windows.Forms.PictureBox();
            this.scrollH = new System.Windows.Forms.HScrollBar();
            this.scrollV = new System.Windows.Forms.VScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.picBlank)).BeginInit();
            this.SuspendLayout();
            // 
            // picBlank
            // 
            this.picBlank.BackColor = System.Drawing.SystemColors.Control;
            this.picBlank.Location = new System.Drawing.Point(283, 283);
            this.picBlank.Name = "picBlank";
            this.picBlank.Size = new System.Drawing.Size(17, 17);
            this.picBlank.TabIndex = 5;
            this.picBlank.TabStop = false;
            // 
            // scrollH
            // 
            this.scrollH.Location = new System.Drawing.Point(0, 283);
            this.scrollH.Name = "scrollH";
            this.scrollH.Size = new System.Drawing.Size(283, 17);
            this.scrollH.TabIndex = 4;
            // 
            // scrollV
            // 
            this.scrollV.Location = new System.Drawing.Point(283, 0);
            this.scrollV.Name = "scrollV";
            this.scrollV.Size = new System.Drawing.Size(17, 283);
            this.scrollV.TabIndex = 3;
            // 
            // ScrollBlank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.picBlank);
            this.Controls.Add(this.scrollH);
            this.Controls.Add(this.scrollV);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ScrollBlank";
            this.Size = new System.Drawing.Size(300, 300);
            ((System.ComponentModel.ISupportInitialize)(this.picBlank)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBlank;
        private System.Windows.Forms.HScrollBar scrollH;
        private System.Windows.Forms.VScrollBar scrollV;
    }
}
