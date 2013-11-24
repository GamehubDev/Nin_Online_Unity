using System;
using System.Runtime.InteropServices;

namespace Toolset.Controls.DoubleBuffer
{

    internal class NativeInterop
    {
        #region Field Region

        public const int WM_PRINTCLIENT = 0x0318;
        public const int PRF_CLIENT = 0x00000004;

        #endregion

        #region Property Region

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public static bool IsWinXP
        {
            get
            {
                var OS = Environment.OSVersion;
                return (OS.Platform == PlatformID.Win32NT) &&
                    ((OS.Version.Major > 5) || ((OS.Version.Major == 5) && (OS.Version.Minor == 1)));
            }
        }

        public static bool IsWinVista
        {
            get
            {
                var OS = Environment.OSVersion;
                return (OS.Platform == PlatformID.Win32NT) && (OS.Version.Major >= 6);
            }
        }

        #endregion
    }
}