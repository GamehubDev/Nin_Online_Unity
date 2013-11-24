using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Toolset.Controls.Console
{
    public class ListViewStreamWriter : TextWriter
    {
        ListView _output = null;
        int index = 0;

        public ListViewStreamWriter(ListView output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            base.Write(value);

            if (_output.Items.Count == 0)
            {
                _output.Items.Add("");
                index = 0;
            }

            if (value == 10)
            {
                index += 1;
                return;
            }

            if (index > _output.Items.Count - 1)
            {
                _output.Items.Add("");
            }

            var tmp = value.ToString(CultureInfo.InvariantCulture);
            _output.Items[index].SubItems[0].Text += tmp;

            _output.Items[_output.Items.Count - 1].EnsureVisible();
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
