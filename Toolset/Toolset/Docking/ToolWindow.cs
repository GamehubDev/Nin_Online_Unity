using System;
using System.Windows.Forms;
using CrystalLib.Toolset.Docking;

namespace Toolset.Docking
{
    public partial class ToolWindow : DockContent
    {
        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindow"/> form.
        /// </summary>
        public ToolWindow()
        {
            InitializeComponent();

            FormClosing += ToolWindow_FormClosing;
            Resize += ToolWindow_Resize;
        }

        #endregion

        #region Events Region

        /// <summary>
        /// Handles the FormClosing event of the <see cref="ToolWindow"/> form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void ToolWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            e.Cancel = true;
            Hide();
        }

        /// <summary>
        /// Handles the Resize event of the <see cref="ToolWindow"/> form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void ToolWindow_Resize(object sender, EventArgs e)
        {
            Font = new System.Drawing.Font("Segoe UI", 9f);
        }

        #endregion
    }
}