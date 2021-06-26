using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CallerForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            Discovery.RoverController controller = new Discovery.RoverController();

            string message = "5 5" + Environment.NewLine +
                "1 2 N" + Environment.NewLine +
                "LMLMLMLMM" + Environment.NewLine +
                "3 3 E" + Environment.NewLine +
                "MMRMMRMRRMMM";

            string result = controller.MoveRovers(message);

            MessageBox.Show(result);

            // 1 3 N
            // 5 1 E
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
