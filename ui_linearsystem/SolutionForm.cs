using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ui_linearsystem
{
    public partial class SolutionForm : Form
    {
        private Form1 mainForm;
        private StringBuilder solutionSteps;

        // Constructor
        public SolutionForm( StringBuilder solutionSteps)
        {
            InitializeComponent();
          
            this.solutionSteps = solutionSteps;
        }
        private void SolutionForm_Load_1(object sender, EventArgs e)
        {
            richTextBox2.Text = solutionSteps.ToString();
            richTextBox2.ReadOnly = true;
        }

       
    }
   

}


