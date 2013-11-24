using System;
using System.IO;
using System.Windows.Forms;
using Toolset.Controls;
using Toolset.Controls.Console;

namespace Toolset.Docking
{
    public partial class DockConsole : ToolWindow
    {
        TextWriter _writer = null;

        #region Constructor region

        /// <summary>
        /// Initializes a new instance of the <see cref="DockConsole"/> form.
        /// </summary>
        public DockConsole()
        {
            InitializeComponent();

            Load += frmConsole_Load;
            Resize += frmConsole_Resize;

            lstConsole.ColumnWidthChanged += lstConsole_ColumnWidthChanged;
            lstConsole.ColumnWidthChanging += lstConsole_ColumnWidthChanging;

            btnDelete.Click += btnDelete_Click;
        }

        #endregion

        #region Event Handler Region

        /// <summary>
        /// Handles the Load event of the <see cref="DockConsole"/> form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmConsole_Load(object sender, EventArgs e)
        {
            _writer = new ListViewStreamWriter(lstConsole);
            Console.SetOut(_writer);

            Console.WriteLine(@"Crystal Toolset initialized.");
        }

        /// <summary>
        /// Handles the Resize event of the <see cref="DockConsole"/> form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmConsole_Resize(object sender, EventArgs e)
        {
            ResizeList();
        }

        /// <summary>
        /// Handles the ColumnWidthChanging event of the <see cref="lstConsole"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ColumnWidthChangingEventArgs"/> instance containing the event data.</param>
        private void lstConsole_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Handles the ColumnWidthChanged event of the <see cref="lstConsole"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ColumnWidthChangedEventArgs"/> instance containing the event data.</param>
        private void lstConsole_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            ResizeList();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnDelete"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            lstConsole.Items.Clear();
        }

        #endregion

        #region Form Management Region

        private void ResizeList()
        {
            if (columnName.Width != lstConsole.Width - 21)
                columnName.Width = lstConsole.Width - 21;
        }

        #endregion
    }
}