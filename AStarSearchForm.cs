using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace AStarSearchImplementation
{
    public partial class AStarSearchForm : Form
    {
        public AStarSearchForm()
        {
            InitializeComponent();
            
            DrawPanel panel = new DrawPanel();
            panel.Location = new System.Drawing.Point(55, 10);
            panel.Size = new Size(400, 600);            
            Controls.Add(panel);
        }
    }
    
    

}