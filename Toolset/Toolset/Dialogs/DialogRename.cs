using System;
using System.Windows.Forms;

namespace Toolset.Dialogs
{
    public partial class DialogRename : Form
    {
        #region Property Region

        public string NewName { get; set; }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogRename"/> form.
        /// </summary>
        public DialogRename()
        {
            InitializeComponent();

            btnOK.Click += btnOK_Click;
        }

        /// <summary>
        /// Initializes a new instace of the <see cref="DialogRename"/> form and populates the fields with the old data.
        /// </summary>
        /// <param name="oldName">Current name of the object.</param>
        public DialogRename(string oldName)
        {
            InitializeComponent();

            btnOK.Click += btnOK_Click;

            txtName.Text = oldName;

            Text = @"Rename " + oldName;
        }

        #endregion

        #region Event Handler Region

        /// <summary>
        /// Handles the Click event of the <see cref="btnOK"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                DialogResult = DialogResult.None;
                return;
            }

            NewName = txtName.Text;
        }

        #endregion

        #region Method Region

        /// <summary>
        /// Validates the input areas of the <see cref="DialogProject"/> form.
        /// </summary>
        /// <returns>Returns false if validation fails, true if validation succeedes.</returns>
        private bool ValidateForm()
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show(@"Please enter a new name.", Text);
                return false;
            }

            return true;
        }

        #endregion
    }
}
