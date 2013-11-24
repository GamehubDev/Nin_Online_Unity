using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AuthServer
{
    public partial class DoubleClickTextBox : TextBox
    {
        string lastText = "";
        public bool saved = false;
        public string setName = "";
        bool internalChange = false;
        public DoubleClickTextBox()
        {
            InitializeComponent();
        }
        public event EventHandler MyEvent
        {
            add
            {
                Console.WriteLine("add operation");
            }

            remove
            {
                Console.WriteLine("remove operation");
            }
        }    
        protected override void OnTextChanged(EventArgs e)
        {
            int result;
            if (int.TryParse(this.Text, out result))
            {
                if (this.Text != lastText)
                {
                    if (!internalChange)
                    {
                        SetColor(false);
                        saved = false;
                    }
                    internalChange = false;

                    lastText = this.Text;
                    base.OnTextChanged(e);
                }
            }
            else
            {
                this.Text = lastText;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.Focus();
            this.SelectAll();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Select();
            this.Parent.Focus();

        }
        public void Save()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "settings.ini";
            List<string> settings = new List<string>();
            using (FileStream f = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamReader r = new StreamReader(f))
                {
                    string temp = r.ReadToEnd();

                    settings.AddRange(temp.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
            using (FileStream f = new FileStream(path, FileMode.Truncate))
            {
                if (settings.Count > 0)
                {
                    string newSettings = "";
                    foreach (string set in settings)
                    {
                        if (set.Contains(setName))
                        {
                            newSettings += setName + "=" + this.Text + "\r\n";
                            saved = true;
                        }
                        else newSettings += set + "\r\n";
                    }
                    using (StreamWriter s = new StreamWriter(f))
                    {
                        s.Write(newSettings);
                    }
                }
                else throw new Exception("Settings file is incorrect!");
            
            }
        }
        public void Load()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "settings.ini";
            List<string> settings = new List<string>();

            using (FileStream f = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamReader r = new StreamReader(f))
                {
                    string temp = r.ReadToEnd();

                    settings.AddRange(temp.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

                }
                if (settings.Count > 0)
                {
                    foreach (string set in settings)
                    {
                        if (set.Contains(setName))
                        {
                            internalChange = true;
                            this.Text = set.Split('=')[1].Trim();
                            saved = true;
                            break;
                        }
                    }

                }
                else throw new Exception("Settings file is incorrect!");
            }
        }

        public void SetColor(bool saved)
        {
            if (saved)
            {
                if (this.BackColor == Color.Red)
                {
                    this.BackColor = Color.Lime;
                }
                else if (this.BackColor == Color.FromArgb(255, 128, 128))
                {
                    this.BackColor = Color.FromArgb(128, 255, 128);
                }
                else if (this.BackColor == Color.FromArgb(255, 192, 192))
                {
                    this.BackColor = Color.FromArgb(192, 255, 192);
                }

            }
            else
            {
                if (this.BackColor == Color.Lime)
                {
                    this.BackColor = Color.Red;
                }
                else if (this.BackColor == Color.FromArgb(128, 255, 128))
                {
                    this.BackColor = Color.FromArgb(255, 128, 128);
                }
                else if (this.BackColor == Color.FromArgb(192, 255, 192))
                {
                    this.BackColor = Color.FromArgb(255, 192, 192);
                }
            }

        }

    }
}
