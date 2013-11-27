using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Diagnostics;

namespace AStarSearchImplementation
{
    /// <summary>
    /// 
    /// </summary>
    public class DrawPanel : Panel
    {
        #region Fields
        private bool _mouseDown;
        private bool _searchDone;
        private int _numRows = 20;//default value
        private int _cellWidth;
        int _startI = -1, _startJ = -1, _goalI = -1, _goalJ = -1;
        private readonly int[,] _grid;
        private List<INode> _solution;
        private readonly ButtonsPanel _bPanel; 
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public DrawPanel()
        {
            MouseDown += DrawPanel_MouseDown;
            MouseMove += DrawPanel_MouseMove;
            MouseUp += DrawPanel_MouseUp;
            _grid = new int[_numRows, _numRows];

            _bPanel = new ButtonsPanel();
            AddButtonPanel(_bPanel);
        } 
        #endregion

        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnStartClicked(object sender, EventArgs e)
        {
            if (_startI != -1 && _goalI != -1)
            {
                ISearchStrategy strategy = new AStarSearchStrategy();
                //ISearchStrategy strategy = new BFSSearchStrategy();
                //ISearchStrategy strategy = new DFSSearchStrategy();

                var search = new PathFinder(_grid, new Location(_startI, _startJ),
                    new Location(_goalI, _goalJ), strategy);

                _solution = search.ExecuteSearch();

                _bPanel.NodesExplored = string.Format("Nodes: {0}", strategy.NoOfNodesExplored);

                if (_solution != null)
                {
                    _searchDone = true;
                    Invalidate();
                }
                else
                {
                    MessageBox.Show("No Solution found!!!");
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
            _mouseDown = false;
        }

        protected void DrawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown)
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
            _mouseDown = true;
            UpdateCells(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.Black, 1, 1, Width - 1, Height - 1);
            g.DrawRectangle(new Pen(Color.Azure), 0, 0, Width, Height);
            _cellWidth = Width / _numRows;
            DrawAll();
            if (_searchDone)
                DrawSolution(_solution);

        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void UpdateCells(MouseEventArgs e)
        {
            int current = _bPanel.Current;

            int x = e.X / _numRows;
            int y = e.Y / _numRows;

            if (x >= _grid.GetLength(0) || y >= _grid.GetLength(1))
                return;

            _grid[x, y] = current;

            if (current == Constants.START && _startI != -1)
            {
                _grid[_startI, _startJ] = Constants.EMPTY;
                DrawCell(_startI, _startJ);
                _startI = -1; _startJ = -1;
            }
            if (current == Constants.FINISH && _goalI != -1)
            {
                _grid[_goalI, _goalJ] = Constants.EMPTY;
                DrawCell(_goalI, _goalJ);
                _goalI = -1; _goalJ = -1;
            }
            DrawCell(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawAll()
        {
            for (int i = 0; i < _numRows; i++)
            {
                for (int j = 0; j < _numRows; j++)
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

            int left = i * _cellWidth, top = j * _cellWidth;
            g.DrawRectangle(new Pen(Color.LightGray), left, top, _cellWidth, _cellWidth);

            if (_grid[i, j] == Constants.START)
            {
                _startI = i; _startJ = j;
                g.DrawRectangle(new Pen(Colors.START), left, top, _cellWidth, _cellWidth);
                g.FillRectangle(Brushes.Black, left + 1, top + 1, _cellWidth - 1, _cellWidth - 1);
            }
            else if (_grid[i, j] == Constants.FINISH)
            {
                _goalI = i; _goalJ = j;
                g.DrawRectangle(new Pen(Colors.FINISH), left, top, _cellWidth, _cellWidth);
                g.FillRectangle(Brushes.Black, left + 1, top + 1, _cellWidth - 1, _cellWidth - 1);
            }
            else if (_grid[i, j] == Constants.EMPTY)
            {
                g.FillRectangle(Brushes.Black, left + 1, top + 1, _cellWidth - 1, _cellWidth - 1);
            }
            else if (_grid[i, j] == Constants.SOLID)
            {
                g.FillRectangle(new SolidBrush(Colors.SOLID), left + 1, top + 1, _cellWidth - 1, _cellWidth - 1);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        private void DrawSolution(IEnumerable<INode> nodes)
        {
            Graphics g = CreateGraphics();

            foreach (var node in nodes)
            {
                int left = node.Location.X * _cellWidth, top = node.Location.Y * _cellWidth;
                g.FillRectangle(Brushes.Violet, left + 1, top + 1, _cellWidth - 1, _cellWidth - 1);
                Thread.Sleep(100);
            }
        }
        

        private void AddButtonPanel(ButtonsPanel buttonsPanel)
        {
            buttonsPanel.Width = 400;
            buttonsPanel.Height = 100;
            buttonsPanel.Location = new Point(10, 410);
            buttonsPanel.BackColor = Color.Black;
            buttonsPanel.OnStartClicked += OnStartClicked;
            Controls.Add(buttonsPanel);
        } 
        #endregion

        #region Properties
        public int NumberOfRows
        {
            get { return _numRows; }
            set { _numRows = value; }
        }
        
        #endregion
    }

    
}
