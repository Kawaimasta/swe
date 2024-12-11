using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Lvl1 : Form
    {
        public Lvl1()
        {
            InitializeComponent();
        }

        private void Lvl1_Load(object sender, EventArgs e)
        {

        }

        public void loadform(object Form) 
        { 
            if  (this.MainPanel.Controls.Count>0) 
                this.MainPanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.MainPanel.Controls.Add(f);
            this.MainPanel.Tag = f;
            f.Show();
        }

        private void btnClock_Click(object sender, EventArgs e)
        {
            loadform(new Clockinout());
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            loadform(new Attendance());
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            loadform(new Request());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadform(new ScheduleForm());
        }
    }
}
