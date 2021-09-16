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
    public partial class Vendeg : Form
    {
        public Vendeg()
        {
            InitializeComponent();
        }

        public void Frissites()
        {
            string friss = "select * from vendeg_vevo ";
            Database.parancs.CommandText = friss;
            Database.adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(Database.parancs);
            DataTable tabla = new DataTable();
            Database.adapter.Fill(tabla);
            dataGridView1.DataSource = tabla;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new KezdoLap().Show();
            this.Hide();
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int kivalCella = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            //MessageBox.Show("lásd az értéket: " + kivalCella);
            int id = kivalCella;

            Database.parancs.CommandText = $"delete from vendeg_vevo where id = '{id}' ";

            DialogResult d = MessageBox.Show($"Biztosan törölni kívánja a kiválasztott vendég vevőt? \n A kiválasztott vendég vevő azonosítója: {id}", "Törlés",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

            if (d == DialogResult.Yes)
            {
                Database.parancs.ExecuteNonQuery();
                Frissites();
            }
            else if (d == DialogResult.No)
            {

            }

            
        }

        private void Vendeg_Load(object sender, EventArgs e)
        {
            Frissites();
        }

        private void Vendeg_FormClosing(object sender, FormClosingEventArgs e)
        {
            Database.kapcsolatBont();
            Application.Exit();
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
