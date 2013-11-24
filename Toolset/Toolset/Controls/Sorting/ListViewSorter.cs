using System.Collections;
using System.Windows.Forms;

namespace Toolset.Controls.Sorting
{
    class ListViewSorter : IComparer
    {
        readonly NaturalComparer comparer = new NaturalComparer();

        public int Compare(object x0, object y0)
        {
            var x = ((ListViewItem)x0).Text;
            var y = ((ListViewItem)y0).Text;

            return comparer.Compare(x, y);
        }
    }
}