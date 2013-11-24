using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Toolset.Controls.DoubleBuffer
{

    class DblTreeView : TreeView
    {
        #region Field Region

        private const int TV_FIRST = 0x1100;
        private const int TVM_SETBKCOLOR = TV_FIRST + 29;
        private const int TVM_SETEXTENDEDSTYLE = TV_FIRST + 44;

        private const int TVS_EX_DOUBLEBUFFER = 0x0004;

        #endregion

        #region Constructor Region

        public DblTreeView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            if (!NativeInterop.IsWinVista)
                SetStyle(ControlStyles.UserPaint, true);

            SetWindowTheme(Handle, "explorer", null);
        }

        #endregion

        #region Method Region

        private void UpdateExtendedStyles()
        {
            var Style = 0;

            if (DoubleBuffered)
                Style |= TVS_EX_DOUBLEBUFFER;

            if (Style != 0)
                NativeInterop.SendMessage(Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)Style);
        }

        #endregion

        #region Override Region

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateExtendedStyles();
            if (!NativeInterop.IsWinXP)
                NativeInterop.SendMessage(Handle, TVM_SETBKCOLOR, IntPtr.Zero, (IntPtr)ColorTranslator.ToWin32(BackColor));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint))
            {
                var m = new Message
                    {
                        HWnd = Handle,
                        Msg = NativeInterop.WM_PRINTCLIENT,
                        WParam = e.Graphics.GetHdc(),
                        LParam = (IntPtr) NativeInterop.PRF_CLIENT
                    };
                DefWndProc(ref m);
                e.Graphics.ReleaseHdc(m.WParam);
            }
            base.OnPaint(e);
        }

        #endregion

        #region Win7 Theme Region

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern int SetWindowTheme(IntPtr hWnd, string appName, string partList);

        #endregion
    }
}