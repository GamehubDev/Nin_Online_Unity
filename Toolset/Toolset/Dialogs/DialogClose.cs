using System.Collections.Generic;
using System.Windows.Forms;

namespace Toolset.Dialogs
{
    public partial class DialogClose : Form
    {
        #region Constructor Region

        public DialogClose(IEnumerable<string> files)
        {
            InitializeComponent();

            foreach (var file in files)
            {
                lstFiles.Items.Add(file);
            }
        }

        #endregion
    }
}