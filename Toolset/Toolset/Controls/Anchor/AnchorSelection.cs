using System.Windows.Forms;
using Toolset.Enums;

namespace Toolset.Controls.Anchor
{
    public partial class AnchorSelection : UserControl
    {
        #region Property Region

        public AnchorPoints AnchorPoint { get; set; }

        private int _xOffset;
        public int xOffset
        {
            get { return _xOffset; }
            set
            {
                if (_xOffset != value)
                {
                    _xOffset = value;
                    SetArrows();
                }
            }
        }

        private int _yOffset;
        public int yOffset
        {
            get { return _yOffset; }
            set
            {
                if (_yOffset != value)
                {
                    _yOffset = value;
                    SetArrows();
                }
            }
        }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="AnchorSelection"/> user control.
        /// </summary>
        public AnchorSelection()
        {
            InitializeComponent();

            chkCenter.Click += delegate { SetAnchor(AnchorPoints.Center); };
            chkUp.Click += delegate { SetAnchor(AnchorPoints.Up); };
            chkUpRight.Click += delegate { SetAnchor(AnchorPoints.UpRight); };
            chkRight.Click += delegate { SetAnchor(AnchorPoints.Right); };
            chkDownRight.Click += delegate { SetAnchor(AnchorPoints.DownRight); };
            chkDown.Click += delegate { SetAnchor(AnchorPoints.Down); };
            chkDownLeft.Click += delegate { SetAnchor(AnchorPoints.DownLeft); };
            chkLeft.Click += delegate { SetAnchor(AnchorPoints.Left); };
            chkUpLeft.Click += delegate { SetAnchor(AnchorPoints.UpLeft); };
        }

        #endregion

        #region Form Management Region

        /// <summary>
        /// Builds the form around the newly selected anchor point.
        /// </summary>
        /// <param name="anchor">Anchor point.</param>
        private void SetAnchor(AnchorPoints anchor)
        {
            DisableAll();
            switch (anchor)
            {
                case AnchorPoints.Center:
                    chkCenter.Checked = true;
                    AnchorPoint = AnchorPoints.Center;
                    break;
                case AnchorPoints.Up:
                    chkUp.Checked = true;
                    AnchorPoint = AnchorPoints.Up;
                    break;
                case AnchorPoints.UpRight:
                    chkUpRight.Checked = true;
                    AnchorPoint = AnchorPoints.UpRight;
                    break;
                case AnchorPoints.Right:
                    chkRight.Checked = true;
                    AnchorPoint = AnchorPoints.Right;
                    break;
                case AnchorPoints.DownRight:
                    chkDownRight.Checked = true;
                    AnchorPoint = AnchorPoints.DownRight;
                    break;
                case AnchorPoints.Down:
                    chkDown.Checked = true;
                    AnchorPoint = AnchorPoints.Down;
                    break;
                case AnchorPoints.DownLeft:
                    chkDownLeft.Checked = true;
                    AnchorPoint = AnchorPoints.DownLeft;
                    break;
                case AnchorPoints.Left:
                    chkLeft.Checked = true;
                    AnchorPoint = AnchorPoints.Left;
                    break;
                case AnchorPoints.UpLeft:
                    chkUpLeft.Checked = true;
                    AnchorPoint = AnchorPoints.UpLeft;
                    break;
            }
            SetArrows();
        }

        /// <summary>
        /// Disables all checkboxes.
        /// </summary>
        private void DisableAll()
        {
            chkCenter.Checked = false;
            chkUp.Checked = false;
            chkUpRight.Checked = false;
            chkRight.Checked = false;
            chkDownRight.Checked = false;
            chkDown.Checked = false;
            chkDownLeft.Checked = false;
            chkLeft.Checked = false;
            chkUpLeft.Checked = false;
        }

        #endregion

        #region Arrow Region

        /// <summary>
        /// Sets the arrow icons on the checkboxes based on the size difference and the anchor selected.
        /// </summary>
        private void SetArrows()
        {
            SetCenterArrow();
            SetUpArrow();
            SetUpRightArrow();
            SetRightArrow();
            SetDownRightArrow();
            SetDownArrow();
            SetDownLeftArrow();
            SetLeftArrow();
            SetUpLeftArrow();
        }

        private void SetCenterArrow()
        {
            chkCenter.SuspendLayout();
            chkCenter.Image = null;

            if (AnchorPoint == AnchorPoints.Up)
                chkCenter.Image = (yOffset >= 0) ? Icons.arr_down : Icons.arr_up;

            if (AnchorPoint == AnchorPoints.Down)
                chkCenter.Image = (yOffset >= 0) ? Icons.arr_up : Icons.arr_down;

            if (AnchorPoint == AnchorPoints.Left)
                chkCenter.Image = (xOffset >= 0) ? Icons.arr_right : Icons.arr_left;

            if (AnchorPoint == AnchorPoints.Right)
                chkCenter.Image = (xOffset >= 0) ? Icons.arr_left : Icons.arr_right;

            if (AnchorPoint == AnchorPoints.UpLeft)
            {
                chkCenter.Image = Icons.arr_downright;
                if (xOffset < 0 || yOffset < 0)
                    chkCenter.Image = Icons.arr_upleft;
                if (xOffset < 0 && yOffset > 0)
                    chkCenter.Image = Icons.arr_left;
                if (xOffset > 0 && yOffset < 0)
                    chkCenter.Image = Icons.arr_up;
            }

            if (AnchorPoint == AnchorPoints.UpRight)
            {
                chkCenter.Image = Icons.arr_downleft;
                if (xOffset < 0 || yOffset < 0)
                    chkCenter.Image = Icons.arr_upright;
                if (xOffset < 0 && yOffset > 0)
                    chkCenter.Image = Icons.arr_right;
                if (xOffset > 0 && yOffset < 0)
                    chkCenter.Image = Icons.arr_up;
            }

            if (AnchorPoint == AnchorPoints.DownRight)
            {
                chkCenter.Image = Icons.arr_upleft;
                if (xOffset < 0 || yOffset < 0)
                    chkCenter.Image = Icons.arr_downright;
                if (xOffset < 0 && yOffset > 0)
                    chkCenter.Image = Icons.arr_right;
                if (xOffset > 0 && yOffset < 0)
                    chkCenter.Image = Icons.arr_down;
            }

            if (AnchorPoint == AnchorPoints.DownLeft)
            {
                chkCenter.Image = Icons.arr_upright;
                if (xOffset < 0 || yOffset < 0)
                    chkCenter.Image = Icons.arr_downleft;
                if (xOffset < 0 && yOffset > 0)
                    chkCenter.Image = Icons.arr_left;
                if (xOffset > 0 && yOffset < 0)
                    chkCenter.Image = Icons.arr_down;
            }

            chkCenter.ResumeLayout();
        }

        private void SetUpArrow()
        {
            chkUp.SuspendLayout();
            chkUp.Image = null;

            if (AnchorPoint == AnchorPoints.Center)
                chkUp.Image = (yOffset >= 0) ? Icons.arr_up : Icons.arr_down;

            if (AnchorPoint == AnchorPoints.UpLeft)
                chkUp.Image = (xOffset >= 0) ? Icons.arr_right : Icons.arr_left;

            if (AnchorPoint == AnchorPoints.UpRight)
                chkUp.Image = (xOffset >= 0) ? Icons.arr_left : Icons.arr_right;

            if (AnchorPoint == AnchorPoints.Left)
            {
                chkUp.Image = Icons.arr_upright;
                if (xOffset < 0 || yOffset < 0)
                    chkUp.Image = Icons.arr_downleft;
                if (xOffset < 0 && yOffset > 0)
                    chkUp.Image = Icons.arr_left;
                if (xOffset > 0 && yOffset < 0)
                    chkUp.Image = Icons.arr_down;
            }

            if (AnchorPoint == AnchorPoints.Right)
            {
                chkUp.Image = Icons.arr_upleft;
                if (xOffset < 0 || yOffset < 0)
                    chkUp.Image = Icons.arr_downright;
                if (xOffset < 0 && yOffset > 0)
                    chkUp.Image = Icons.arr_right;
                if (xOffset > 0 && yOffset < 0)
                    chkUp.Image = Icons.arr_down;
            }

            chkUp.ResumeLayout();
        }

        private void SetUpRightArrow()
        {
            chkUpRight.SuspendLayout();
            chkUpRight.Image = null;

            if (AnchorPoint == AnchorPoints.Up)
                chkUpRight.Image = (xOffset >= 0) ? Icons.arr_right : Icons.arr_left;

            if (AnchorPoint == AnchorPoints.Right)
                chkUpRight.Image = (yOffset >= 0) ? Icons.arr_up : Icons.arr_down;

            if (AnchorPoint == AnchorPoints.Center)
            {
                chkUpRight.Image = Icons.arr_upright;
                if (xOffset < 0 || yOffset < 0)
                    chkUpRight.Image = Icons.arr_downleft;
                if (xOffset < 0 && yOffset > 0)
                    chkUpRight.Image = Icons.arr_left;
                if (xOffset > 0 && yOffset < 0)
                    chkUpRight.Image = Icons.arr_down;
            }

            chkUpRight.ResumeLayout();
        }

        private void SetRightArrow()
        {
            chkRight.SuspendLayout();
            chkRight.Image = null;

            if (AnchorPoint == AnchorPoints.Center)
                chkRight.Image = (xOffset >= 0) ? Icons.arr_right : Icons.arr_left;

            if (AnchorPoint == AnchorPoints.UpRight)
                chkRight.Image = (yOffset >= 0) ? Icons.arr_down : Icons.arr_up;

            if (AnchorPoint == AnchorPoints.DownRight)
                chkRight.Image = (yOffset >= 0) ? Icons.arr_up : Icons.arr_down;

            if (AnchorPoint == AnchorPoints.Up)
            {
                chkRight.Image = Icons.arr_downright;
                if (xOffset < 0 || yOffset < 0)
                    chkRight.Image = Icons.arr_upleft;
                if (xOffset < 0 && yOffset > 0)
                    chkRight.Image = Icons.arr_left;
                if (xOffset > 0 && yOffset < 0)
                    chkRight.Image = Icons.arr_up;
            }

            if (AnchorPoint == AnchorPoints.Down)
            {
                chkRight.Image = Icons.arr_upright;
                if (xOffset < 0 || yOffset < 0)
                    chkRight.Image = Icons.arr_downleft;
                if (xOffset < 0 && yOffset > 0)
                    chkRight.Image = Icons.arr_left;
                if (xOffset > 0 && yOffset < 0)
                    chkRight.Image = Icons.arr_down;
            }

            chkRight.ResumeLayout();
        }

        private void SetDownRightArrow()
        {
            chkDownRight.SuspendLayout();
            chkDownRight.Image = null;

            if (AnchorPoint == AnchorPoints.Down)
                chkDownRight.Image = (xOffset >= 0) ? Icons.arr_right : Icons.arr_left;

            if (AnchorPoint == AnchorPoints.Right)
                chkDownRight.Image = (yOffset >= 0) ? Icons.arr_down : Icons.arr_up;

            if (AnchorPoint == AnchorPoints.Center)
            {
                chkDownRight.Image = Icons.arr_downright;
                if (xOffset < 0 || yOffset < 0)
                    chkDownRight.Image = Icons.arr_upleft;
                if (xOffset < 0 && yOffset > 0)
                    chkDownRight.Image = Icons.arr_left;
                if (xOffset > 0 && yOffset < 0)
                    chkDownRight.Image = Icons.arr_up;
            }

            chkDownRight.ResumeLayout();
        }

        private void SetDownArrow()
        {
            chkDown.SuspendLayout();
            chkDown.Image = null;

            if (AnchorPoint == AnchorPoints.Center)
                chkDown.Image = (yOffset >= 0) ? Icons.arr_down : Icons.arr_up;

            if (AnchorPoint == AnchorPoints.DownLeft)
                chkDown.Image = (xOffset >= 0) ? Icons.arr_right : Icons.arr_left;

            if (AnchorPoint == AnchorPoints.DownRight)
                chkDown.Image = (xOffset >= 0) ? Icons.arr_left : Icons.arr_right;

            if (AnchorPoint == AnchorPoints.Left)
            {
                chkDown.Image = Icons.arr_downright;
                if (xOffset < 0 || yOffset < 0)
                    chkDown.Image = Icons.arr_upleft;
                if (xOffset < 0 && yOffset > 0)
                    chkDown.Image = Icons.arr_left;
                if (xOffset > 0 && yOffset < 0)
                    chkDown.Image = Icons.arr_up;
            }

            if (AnchorPoint == AnchorPoints.Right)
            {
                chkDown.Image = Icons.arr_downleft;
                if (xOffset < 0 || yOffset < 0)
                    chkDown.Image = Icons.arr_upright;
                if (xOffset < 0 && yOffset > 0)
                    chkDown.Image = Icons.arr_right;
                if (xOffset > 0 && yOffset < 0)
                    chkDown.Image = Icons.arr_up;
            }

            chkDown.ResumeLayout();
        }

        private void SetDownLeftArrow()
        {
            chkDownLeft.SuspendLayout();
            chkDownLeft.Image = null;

            if (AnchorPoint == AnchorPoints.Down)
                chkDownLeft.Image = (xOffset >= 0) ? Icons.arr_left : Icons.arr_right;

            if (AnchorPoint == AnchorPoints.Left)
                chkDownLeft.Image = (yOffset >= 0) ? Icons.arr_down : Icons.arr_up;

            if (AnchorPoint == AnchorPoints.Center)
            {
                chkDownLeft.Image = Icons.arr_downleft;
                if (xOffset < 0 || yOffset < 0)
                    chkDownLeft.Image = Icons.arr_upright;
                if (xOffset < 0 && yOffset > 0)
                    chkDownLeft.Image = Icons.arr_right;
                if (xOffset > 0 && yOffset < 0)
                    chkDownLeft.Image = Icons.arr_up;
            }

            chkDownLeft.ResumeLayout();
        }

        private void SetLeftArrow()
        {
            chkLeft.SuspendLayout();
            chkLeft.Image = null;

            if (AnchorPoint == AnchorPoints.Center)
                chkLeft.Image = (xOffset >= 0) ? Icons.arr_left : Icons.arr_right;

            if (AnchorPoint == AnchorPoints.UpLeft)
                chkLeft.Image = (yOffset >= 0) ? Icons.arr_down : Icons.arr_up;

            if (AnchorPoint == AnchorPoints.DownLeft)
                chkLeft.Image = (yOffset >= 0) ? Icons.arr_up : Icons.arr_down;

            if (AnchorPoint == AnchorPoints.Up)
            {
                chkLeft.Image = Icons.arr_downleft;
                if (xOffset < 0 || yOffset < 0)
                    chkLeft.Image = Icons.arr_upright;
                if (xOffset < 0 && yOffset > 0)
                    chkLeft.Image = Icons.arr_right;
                if (xOffset > 0 && yOffset < 0)
                    chkLeft.Image = Icons.arr_up;
            }

            if (AnchorPoint == AnchorPoints.Down)
            {
                chkLeft.Image = Icons.arr_upleft;
                if (xOffset < 0 || yOffset < 0)
                    chkLeft.Image = Icons.arr_downright;
                if (xOffset < 0 && yOffset > 0)
                    chkLeft.Image = Icons.arr_right;
                if (xOffset > 0 && yOffset < 0)
                    chkLeft.Image = Icons.arr_down;
            }

            chkLeft.ResumeLayout();
        }

        private void SetUpLeftArrow()
        {
            chkUpLeft.SuspendLayout();
            chkUpLeft.Image = null;

            if (AnchorPoint == AnchorPoints.Up)
                chkUpLeft.Image = (xOffset >= 0) ? Icons.arr_left : Icons.arr_right;

            if (AnchorPoint == AnchorPoints.Left)
                chkUpLeft.Image = (yOffset >= 0) ? Icons.arr_up : Icons.arr_down;

            if (AnchorPoint == AnchorPoints.Center)
            {
                chkUpLeft.Image = Icons.arr_upleft;
                if (xOffset < 0 || yOffset < 0)
                    chkUpLeft.Image = Icons.arr_downright;
                if (xOffset < 0 && yOffset > 0)
                    chkUpLeft.Image = Icons.arr_right;
                if (xOffset > 0 && yOffset < 0)
                    chkUpLeft.Image = Icons.arr_down;
            }

            chkUpLeft.ResumeLayout();
        }

        #endregion
    }
}
