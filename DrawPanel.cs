using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace AStarSearchImplementation
{
    /// <summary>
    /// 
    /// </summary>
    public class DrawPanel : Panel
    {
        private bool mouseDown;
        private bool searchDone;
        private int numRows = 20;//default value
        private int cellWidth;
        int startI = -1, startJ = -1, goalI = -1, goalJ = -1;
        private Color[] tColor;
        private readonly int[,] grid;
        private List<Node> solution;
        private readonly ButtonsPanel bPanel;
        /// <summary>
        /// 
        /// </summary>
        public DrawPanel()
        {
            MouseDown += DrawPanel_MouseDown;
            MouseMove += DrawPanel_MouseMove;
            MouseUp += DrawPanel_MouseUp;
            InitializeColors();
            grid = new int[numRows, numRows];
            
            bPanel=new ButtonsPanel();
            AddButtonPanel(bPanel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bPanelOnStartClicked(object sender, EventArgs e)
        {
            if (startI != -1 && goalI != -1)
            {
                AStarSearch search = new AStarSearch(grid, new Location(startI, startJ), new Location(goalI, goalJ));
                solution = search.ExecuteSearch();
                if (solution != null)
                {
                    searchDone = true;
                    Invalidate();
                }
            }
            else
            {
                MessageBox.Show("Please select start and end location");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DrawPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        protected void DrawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                UpdateCells(e);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DrawPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            UpdateCells(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void UpdateCells(MouseEventArgs e)
        {
            int current = bPanel.Current;
            
            int x = e.X / numRows;
            int y = e.Y / numRows;
            
            if (x >= grid.GetLength(0) || y >= grid.GetLength(1))
                return;

            grid[x, y] = current;
            
            if (current == Constants.START && startI != -1)
            {
                grid[startI, startJ] = Constants.EMPTY;
                DrawCell(startI, startJ);
                startI = -1; startJ = -1;
            }
            if (current == Constants.FINISH && goalI != -1)
            {
                grid[goalI, goalJ] = Constants.EMPTY;
                DrawCell(goalI, goalJ);
                goalI = -1; goalJ = -1;
            }
            DrawCell(x, y);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.Black, 1, 1, this.Width - 1, this.Height - 1);
            g.DrawRectangle(new Pen(Color.Azure), 0, 0, this.Width, this.Height);
            cellWidth = Width / numRows;
            DrawAll();
            if (searchDone) 
                DrawSolution(solution);

        }
        /// <summary>
        /// 
        /// </summary>
        private void DrawAll()
        {
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numRows; j++)
                {
                    DrawCell(i, j);
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void DrawCell(int i, int j)
        {
            Graphics g = CreateGraphics();

            int left = i * cellWidth, top = j * cellWidth;
            g.DrawRectangle(new Pen(Color.LightGray), left, top, cellWidth, cellWidth);

            if (grid[i, j] == Constants.START)
            {
                startI = i; startJ = j;
                g.DrawRectangle(new Pen(tColor[Constants.START]), left, top, cellWidth, cellWidth);
                g.FillRectangle(Brushes.Black, left + 1, top + 1, cellWidth - 1, cellWidth - 1);
            }
            else if (grid[i, j] == Constants.FINISH)
            {
                goalI = i; goalJ = j;
                g.DrawRectangle(new Pen(tColor[Constants.FINISH]), left, top, cellWidth, cellWidth);
                g.FillRectangle(Brushes.Black, left + 1, top + 1, cellWidth - 1, cellWidth - 1);
            }
            else if (grid[i, j] == Constants.NOTHING || grid[i, j] == Constants.EMPTY)
            {
                g.FillRectangle(Brushes.Black, left + 1, top + 1, cellWidth - 1, cellWidth - 1);
            }
            else if (grid[i, j] == Constants.SOLID)
            {
                g.FillRectangle(new SolidBrush(Color.White), left + 1, top + 1, cellWidth - 1, cellWidth - 1);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        private void DrawSolution(IEnumerable<Node> nodes)
        {
            Graphics g = CreateGraphics();

            foreach (var node in nodes)
            {
                int left = node.Location.X * cellWidth, top = node.Location.Y * cellWidth;
                g.FillRectangle(Brushes.Violet, left + 1, top + 1, cellWidth - 1, cellWidth - 1);
                Thread.Sleep(100);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitializeColors()
        {
            tColor = new Color[Constants.NUMCOLORS];
            tColor[Constants.EMPTY] = Color.Black;
            tColor[Constants.SOLID] = Color.White;
            tColor[Constants.START] = Color.Green;
            tColor[Constants.FINISH] = Color.Red;
        }

        private void AddButtonPanel(ButtonsPanel buttonsPanel)
        {
            buttonsPanel.Width = 400;
            buttonsPanel.Height = 100;
            buttonsPanel.Location = new Point(10, 410);
            buttonsPanel.BackColor = Color.Black;
            buttonsPanel.OnStartClicked += bPanelOnStartClicked;
            Controls.Add(buttonsPanel);
        }
        public int NumberOfRows
        {
            get { return numRows; }
            set { numRows = value; }
        }
        public Color[] Colors
        {
            get { return tColor; }
        }
    }
}
