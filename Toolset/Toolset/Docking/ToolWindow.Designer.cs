namespace Toolset.Docking
{
    partial class ToolWindow
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
            this.SuspendLayout();
            // 
            // ToolWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.DockAreas = ((CrystalLib.Toolset.Docking.DockAreas)(((((CrystalLib.Toolset.Docking.DockAreas.Float | CrystalLib.Toolset.Docking.DockAreas.DockLeft) 
            | CrystalLib.Toolset.Docking.DockAreas.DockRight) 
            | CrystalLib.Toolset.Docking.DockAreas.DockTop) 
            | CrystalLib.Toolset.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ToolWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ToolWindow";
            this.ResumeLayout(false);

        }

        #endregion
    }
}