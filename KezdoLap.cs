using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barkacs
{
    public partial class KezdoLap : Form
    {
        public KezdoLap()
        {
            InitializeComponent();
        }

        private void KezdoLap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Database.kapcsolatBont();
            Application.Exit();
        }

        private void KezdoLap_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Termekek().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Vasarlok().Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new Vendeg().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Rendelesek().Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Statisztikak().Show();
            this.Hide();
        }
    }
}
