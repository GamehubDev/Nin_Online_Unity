using System;
using System.IO;
using System.Windows.Forms;

namespace Toolset.Dialogs
{
    public partial class DialogProject : Form
    {
        #region Property Region

        public string ProjectName { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogProject"/> class.
        /// </summary>
        public DialogProject()
        {
            InitializeComponent();

            txtLocation.Text = Application.StartupPath;

            btnOK.Click += btnOK_Click;
            btnBrowse.Click += btnBrowse_Click;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogProject"/> class.
        /// Effectively puts the dialog in 'edit mode'.
        /// Disables the directory input and populates the fields with the corresponding data.
        /// </summary>
        /// <param name="name">Project Name.</param>
        /// <param name="author">Project Author.</param>
        /// <param name="description">Project Description.</param>
        /// <param name="path">Project Path.</param>
        public DialogProject(string name, string author, string description, string path)
        {
            InitializeComponent();

            txtName.Text = name;
            txtAuthor.Text = author;
            txtDescription.Text = description;
            txtLocation.Text = path;

            btnOK.Click += btnOK_Click;

            txtLocation.Enabled = false;
            btnBrowse.Enabled = false;

            Text = @"Edit Project";
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

            ProjectName = txtName.Text;
            Author = txtAuthor.Text;
            Description = txtDescription.Text;
            FilePath = txtLocation.Text;
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnBrowse"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new CrystalLib.Toolset.Dialogs.VistaFolderBrowserDialog())
            {
                folderDialog.SelectedPath = txtLocation.Text;

                var folderResult = folderDialog.ShowDialog();

                if (folderResult == DialogResult.OK)
                {
                    txtLocation.Text = folderDialog.SelectedPath;
                }
            }
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
                MessageBox.Show(@"Please enter a project name.", Text);
                return false;
            }

            if (txtLocation.Enabled)
            {
                if (!Directory.Exists(txtLocation.Text))
                {
                    MessageBox.Show(@"Please enter a valid file path.", Text);
                    return false;
                }

                if (File.Exists(Path.Combine(txtLocation.Text, @"Project.xml")))
                {
                    MessageBox.Show(@"A project already exists in this location.", Text);
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}