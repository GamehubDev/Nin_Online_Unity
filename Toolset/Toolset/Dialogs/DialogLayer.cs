using System;
using System.Windows.Forms;

namespace Toolset.Dialogs
{
    public partial class DialogLayer : Form
    {
        #region Property Region

        public string LayerName { get; set; }
        public int LayerOpacity { get; set; }
        public bool LayerVisible { get; set; }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogLayer"/> form.
        /// </summary>
        public DialogLayer()
        {
            InitializeComponent();

            cmbVisible.SelectedIndex = 0;

            btnOK.Click += btnOK_Click;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogLayer"/> form.
        /// Effectively puts the form in 'edit mode'.
        /// Populates the fields with the corresponding data.
        /// </summary>
        /// <param name="name">Name of the layer.</param>
        /// <param name="opacity">Opacity of the layer.</param>
        /// <param name="visible">Visibility of the layer.</param>
        public DialogLayer(string name, int opacity, bool visible)
        {
            InitializeComponent();

            txtName.Text = name;
            spinOpacity.Value = opacity;

            if (visible)
                cmbVisible.SelectedIndex = 0;
            else
                cmbVisible.SelectedIndex = 1;

            btnOK.Click += btnOK_Click;

            Text = @"Duplicate Layer";
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

            LayerName = txtName.Text;
            LayerOpacity = (int)spinOpacity.Value;

            if (cmbVisible.SelectedIndex == 0)
                LayerVisible = true;
            else
                LayerVisible = false;
        }

        #endregion

        #region Form Management Region

        /// <summary>
        /// Validates the input areas of the <see cref="DialogLayer"/> form.
        /// </summary>
        /// <returns>Returns false if validation fails, true if validation succeedes.</returns>
        private bool ValidateForm()
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show(@"Please enter a layer name.", Text);
                return false;
            }

            return true;
        }

        #endregion
    }
}
