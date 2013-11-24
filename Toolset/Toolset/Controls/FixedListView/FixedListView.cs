using System.Drawing;
using System.Windows.Forms;

namespace Toolset.Controls.FixedListView
{
    public class FixedListView : ListView
    {
        protected override void WndProc(ref Message m)
        {
            // Swallow mouse messages that are not in the client area
            if (m.Msg >= 0x201 && m.Msg <= 0x209)
            {
                var pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                var hit = HitTest(pos);
                switch (hit.Location)
                {
                    case ListViewHitTestLocations.AboveClientArea:
                    case ListViewHitTestLocations.BelowClientArea:
                    case ListViewHitTestLocations.LeftOfClientArea:
                    case ListViewHitTestLocations.RightOfClientArea:
                    case ListViewHitTestLocations.None:
                        return;
                }
            }
            base.WndProc(ref m);
        }
    }
}
