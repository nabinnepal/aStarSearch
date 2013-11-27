using System.Drawing;
using System.Windows.Forms;

namespace AStarSearchImplementation
{
    public partial class AStarSearchForm : Form
    {
        public AStarSearchForm()
        {
            InitializeComponent();
            
            var panel = new DrawPanel();
            panel.Location = new Point(55, 10);
            panel.Size = new Size(400, 600);            
            Controls.Add(panel);
            
        }
    }
    
    

}