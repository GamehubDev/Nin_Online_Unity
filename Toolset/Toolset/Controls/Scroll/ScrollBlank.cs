using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace Toolset.Controls.Scroll
{
    public partial class ScrollBlank : UserControl, INotifyPropertyChanged
    {
        #region Property Changed Region

        /// <summary>
        /// Event raised when a property is changed on the <see cref="ScrollBlank"/> control.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Property Region

        private int _objectWidth;

        /// <summary>
        /// Width used when calculating the scrollable area of <see cref="ScrollBlank"/> control.
        /// </summary>
        public int ObjectWidth
        {
            get { return _objectWidth; }
            set
            {
                if (value == _objectWidth) return;
                _objectWidth = value;
                OnPropertyChanged("ObjectWidth");
            }
        }

        private int _objectHeight;

        /// <summary>
        /// Height used when calculating the scrollable area of <see cref="ScrollBlank"/> control.
        /// </summary>
        public int ObjectHeight
        {
            get { return _objectHeight; }
            set
            {
                if (value == _objectHeight) return;
                _objectHeight = value;
                OnPropertyChanged("ObjectHeight");
            }
        }

        private int _xOffset;

        /// <summary>
        /// Scrolled x offset of <see cref="ScrollBlank"/> control.
        /// </summary>
        public int XOffset 
        {
            get { return _xOffset; }
            set
            {
                if (value == _xOffset) return;
                _xOffset = value;
                OnPropertyChanged("XOffset");
            }
        }

        private int _yOffset;

        /// <summary>
        /// Scrolled y offset of <see cref="ScrollBlank"/> control.
        /// </summary>
        public int YOffset 
        {
            get { return _yOffset; }
            set
            {
                if (value == _yOffset) return;
                _yOffset = value;
                OnPropertyChanged("YOffset");
            }
        }

        private int _mouseX;

        /// <summary>
        /// X co-ordinate of the mouse on the <see cref="ScrollBlank"/> control.
        /// </summary>
        public int MouseX
        {
            get { return _mouseX; }
            set
            {
                if (value == _mouseX) return;
                _mouseX = value;
                OnPropertyChanged("MouseX");
            }
        }

        private int _mouseY;

        /// <summary>
        /// Y co-ordinate of the mouse on the <see cref="ScrollBlank"/> control.
        /// </summary>
        public int MouseY
        {
            get { return _mouseY; }
            set
            {
                if (value == _mouseY) return;
                _mouseY = value;
                OnPropertyChanged("MouseY");
            }
        }

        private int _mouseXOffset;

        /// <summary>
        /// Sum of the x co-ordinate of the mouse and the x offset on the <see cref="ScrollBlank"/> control.
        /// </summary>
        public int MouseXOffset
        {
            get { return _mouseXOffset; }
            set
            {
                if (value == _mouseXOffset) return;
                _mouseXOffset = value;
                OnPropertyChanged("MouseXOffset");
            }
        }

        private int _mouseYOffset;

        /// <summary>
        /// Sum of the y co-ordinate of the mouse and the y offset on the <see cref="ScrollBlank"/> control.
        /// </summary>
        public int MouseYOffset
        {
            get { return _mouseYOffset; }
            set
            {
                if (value == _mouseYOffset) return;
                _mouseYOffset = value;
                OnPropertyChanged("MouseYOffset");
            }
        }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollBlank"/> control.
        /// </summary>
        public ScrollBlank()
        {
            InitializeComponent();

            Load += ScrollableBase_Load;

            scrollH.Scroll += scrollH_Scroll;
            scrollV.Scroll += scrollV_Scroll;

            PropertyChanged += ScrollableBase_PropertyChanged;
            Resize += ScrollableBase_Resize;
            MouseMove += ScrollableBase_MouseMove;
            MouseWheel += ScrollableBase_MouseWheel;
        }

        #endregion

        #region Event Handler Region

        /// <summary>
        /// Handles the PropertyChanged event of the <see cref="ScrollBlank"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ScrollableBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ObjectWidth" || e.PropertyName == "ObjectHeight")
            {
                CheckOverflow();
            }

            if (e.PropertyName == "XOffset" || e.PropertyName == "YOffset" || e.PropertyName == "MouseX" || e.PropertyName == "MouseY")
            {
                MouseXOffset = MouseX + XOffset; MouseYOffset = MouseY + YOffset;
                if (scrollH.Value != XOffset)
                    scrollH.Value = XOffset;
                if (scrollV.Value != YOffset)
                    scrollV.Value = YOffset;
            }
        }

        /// <summary>
        /// Handles the Load event of the <see cref="ScrollBlank"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollableBase_Load(object sender, EventArgs e)
        {
            OrderControls();
            CheckOverflow();
        }

        /// <summary>
        /// Handles the Resize event of the <see cref="ScrollBlank"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollableBase_Resize(object sender, EventArgs e)
        {
            OrderControls();
            CheckOverflow();
        }

        /// <summary>
        /// Handles the MouseMove event of the <see cref="ScrollBlank"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ScrollableBase_MouseMove(object sender, MouseEventArgs e)
        {
            MouseX = e.X;
            MouseY = e.Y;
        }

        /// <summary>
        /// Handles the MouseWheel event of the <see cref="ScrollBlank"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ScrollableBase_MouseWheel(object sender, MouseEventArgs e)
        {
            bool isHor = ModifierKeys == Keys.Control || scrollV.Enabled == false;

            if (isHor)
            {
                if (scrollH.Enabled)
                {
                    if (e.Delta < 0)
                    {
                        if ((scrollH.Value + scrollH.LargeChange) > scrollH.Maximum)
                            scrollH.Value = scrollH.Maximum;
                        else
                            scrollH.Value += scrollH.LargeChange;
                    }
                    else if (e.Delta > 0)
                    {
                        if ((scrollH.Value - scrollH.LargeChange) < scrollH.Minimum)
                            scrollH.Value = scrollH.Minimum;
                        else
                            scrollH.Value -= scrollH.LargeChange;
                    }
                    XOffset = scrollH.Value;
                }
            }
            else
            {
                if (scrollV.Enabled)
                {
                    if (e.Delta < 0)
                    {
                        if ((scrollV.Value + scrollV.LargeChange) > scrollV.Maximum)
                            scrollV.Value = scrollV.Maximum;
                        else
                            scrollV.Value += scrollV.LargeChange;
                    }
                    else if (e.Delta > 0)
                    {
                        if ((scrollV.Value - scrollV.LargeChange) < scrollV.Minimum)
                            scrollV.Value = scrollV.Minimum;
                        else
                            scrollV.Value -= scrollV.LargeChange;
                    }
                    YOffset = scrollV.Value;
                }
            }
        }

        /// <summary>
        /// Handles the Scroll event of the <see cref="scrollH"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void scrollH_Scroll(object sender, ScrollEventArgs e)
        {
            if (scrollH.Value > (scrollH.Maximum - scrollH.LargeChange))
                scrollH.Value = scrollH.Maximum;

            XOffset = scrollH.Value;
        }

        /// <summary>
        /// Handles the Scroll event of the <see cref="scrollV"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void scrollV_Scroll(object sender, ScrollEventArgs e)
        {
            if (scrollV.Value > (scrollV.Maximum - scrollV.LargeChange))
                scrollV.Value = scrollV.Maximum;

            YOffset = scrollV.Value;
        }

        #endregion

        #region Scrolling Region

        /// <summary>
        /// Re-positions the controls based on the size of the <see cref="ScrollBlank"/> control.
        /// </summary>
        private void OrderControls()
        {
            scrollH.Left = 0;
            scrollH.Width = Width - 17;
            scrollH.Top = Height - 17;

            scrollV.Top = 0;
            scrollV.Height = Height - 17;
            scrollV.Left = Width - 17;

            picBlank.Left = Width - 17;
            picBlank.Top = Height - 17;
        }

        /// <summary>
        /// Sets the properties of the <see cref="scrollH"/> and <see cref="scrollV"/> controls based 
        /// on the size of the <see cref="ScrollBlank"/> control.
        /// </summary>
        public void CheckOverflow()
        {
            scrollH.Enabled = false;
            scrollV.Enabled = false;

            if (ObjectWidth > 0)
            {
                if (ObjectWidth > Width - 17)
                {
                    scrollH.Enabled = true;
                    scrollH.Maximum = (Width - ObjectWidth - 17) * -1;
                    XOffset = scrollH.Value;
                }
                else
                    XOffset = 0;
            }
            else
                XOffset = 0;

            if (ObjectHeight > 0)
            {
                if (ObjectHeight > Height - 17)
                {
                    scrollV.Enabled = true;
                    scrollV.Maximum = (Height - ObjectHeight - 17) * -1;
                    YOffset = scrollV.Value;
                }
                else
                    YOffset = 0;
            }
            else
                YOffset = 0;

            scrollV.LargeChange = scrollV.Maximum / 10;
            scrollH.LargeChange = scrollH.Maximum / 10;
        }

        #endregion
    }
}