using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Clockinout : Form
    {
        public Clockinout()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm:ss tt");
        }

        private void Clockinout_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000; 
            timer1.Start();
        }

        private void btnClockinOut_Click(object sender, EventArgs e)
        {
            if (btnClockinOut.Text == "Clock in")
            {
                btnClockinOut.Text = "Clock out";
            }
            else
            {
               btnClockinOut.Text = "Clock in";
            }
        }
    }
}
