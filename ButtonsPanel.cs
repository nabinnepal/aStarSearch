using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AStarSearchImplementation
{
    /// <summary>
    /// 
    /// </summary>
    public class ButtonsPanel : Panel
    {
        private int _current;
        public event EventHandler OnStartClicked;

        public ButtonsPanel()
        {
            MouseClick += new MouseEventHandler(ButtonsPanel_MouseClick);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonsPanel_MouseClick(object sender, MouseEventArgs e)
        {
            int xValue = e.X / 20;
            
            if(IsBlankRegion(xValue))
                return;
            
            _current = xValue / 2;
            
            if (_current > 3)
            {
                if (OnStartClicked != null)
                    OnStartClicked(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public void DrawButtons(Graphics g)
        {
            int pad = 20;
            int left = pad;
            int top = 20;
            Pen pen = new Pen(Color.LightGray);
            for (int i = Constants.EMPTY; i <= Constants.FINISH; i++)
            {
                switch (i)
                {
                    case 0:
                        break;
                    case 1:
                        g.FillRectangle(Brushes.White, left + 1, top + 1, 19, 19);
                        break;
                    case 2:
                        pen = new Pen(Color.Green);
                        break;
                    case 3:
                        pen = new Pen(Color.Red);
                        break;
                }
                g.DrawRectangle(pen, left, top, 20, 20);
                left += pad + 20;
            }
            g.DrawRectangle(new Pen(Color.LightGray), left, top, 24, 20);
            g.DrawString("Go", new Font("Times New Roman", 12), Brushes.White, left + 1, top + 1);
        }
        /// <summary>
        /// 
        /// </summary>
        public int Current
        {
            get
            {
                return _current;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawButtons(e.Graphics);
        }

        private static bool IsBlankRegion(int currentValue)
        {
            return currentValue % 2 == 0;
        }

    }
}
