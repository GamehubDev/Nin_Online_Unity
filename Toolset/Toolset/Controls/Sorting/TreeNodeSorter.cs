using System.Collections;
using System.Windows.Forms;

namespace Toolset.Controls.Sorting
{
    class TreeNodeSorter : IComparer
    {
        readonly NaturalComparer comparer = new NaturalComparer();

        public int Compare(object x0, object y0)
        {
            var x = ((TreeNode)x0).Text;
            var y = ((TreeNode)y0).Text;

            return comparer.Compare(x, y);
        }
    }
}