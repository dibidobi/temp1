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
    public partial class Rendelesek : Form
    {
        public Rendelesek()
        {
            InitializeComponent();
        }

        public void Frissites()
        {
            string friss = "select * from  szamla inner join szamla_reszlet on szamla_reszlet.id = szamla.reszletID ";
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

        private void button7_Click(object sender, EventArgs e)
        {
            new Vendeg().Show();
            this.Hide();
        }

        private void Rendelesek_FormClosing(object sender, FormClosingEventArgs e)
        {
            Database.kapcsolatBont();
            Application.Exit();
        }

        private void Rendelesek_Load(object sender, EventArgs e)
        {
            Frissites();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int kivalCella = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value); //számla id-ja
            int kivalCella1 = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value); //számla részlet id-ja
            //MessageBox.Show("lásd az értéket: " + kivalCella);
            int id = kivalCella;
            int resz_id = kivalCella1;

            Database.parancs.CommandText = $"delete from szamla where id = '{id}'; " +
                                           $"delete from szamla_reszlet where id = '{resz_id}';  ";

            DialogResult d = MessageBox.Show($"Biztosan törölni kívánja a kiválasztott rendelést? " +
                $"\n A kiválasztott rendelés azonosítója: {id} \n A kiválasztott rendelés részl azonosítója: {resz_id}", "Törlés",
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

        private void button5_Click(object sender, EventArgs e)
        {
            new Statisztikak().Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            int Vid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[2].Value);
            int Rid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[5].Value);
            int Tid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[6].Value);

            int db = 0;
            int ressz = 0;
            int ossz = 0;

            if (szamlaPanel.Visible == true)
            {
                Database.parancs.CommandText = $"select nev, iranyitoszam, varos, cim, telefon from vasarlok " +
                      $"inner join szamla on vasarlok.id = szamla.vasarloID where szamla.vasarloID = {Vid}";
                Database.eredmeny = Database.parancs.ExecuteReader();
                while (Database.eredmeny.Read())
                {
                    labelVevNev.Text = Database.eredmeny[0].ToString();
                    labelVevIrsz.Text = Database.eredmeny[1].ToString();
                    labelVevVaros.Text = Database.eredmeny[2].ToString();
                    labelVevCim.Text = Database.eredmeny[3].ToString();
                    labelVevTel.Text = Database.eredmeny[4].ToString();
                }
                Database.eredmeny.Close();

                Database.parancs.CommandText = $"select nev, iranyitoszam, varos, cim, telefon from vendeg_vevo " +
                      $"inner join szamla on vendeg_vevo.id = szamla.vasarloID where szamla.vasarloID = {Vid}";
                Database.eredmeny = Database.parancs.ExecuteReader();
                while (Database.eredmeny.Read())
                {
                    labelVevNev.Text = Database.eredmeny[0].ToString();
                    labelVevIrsz.Text = Database.eredmeny[1].ToString();
                    labelVevVaros.Text = Database.eredmeny[2].ToString();
                    labelVevCim.Text = Database.eredmeny[3].ToString();
                    labelVevTel.Text = Database.eredmeny[4].ToString();
                }
                Database.eredmeny.Close();

                Database.parancs.CommandText = $"SELECT nev, brutto_ar FROM termekek  WHERE id = {Tid}";
                Database.eredmeny = Database.parancs.ExecuteReader();
                while (Database.eredmeny.Read())
                {
                    labelTermNev.Text = Database.eredmeny[0].ToString(); 
                    labelTermAr.Text = Database.eredmeny[1].ToString();
                    ressz = Convert.ToInt32(labelTermAr.Text);
                }
                Database.eredmeny.Close();

                Database.parancs.CommandText = $"SELECT mennyiseg FROM szamla_reszlet WHERE id = {Rid}";
                Database.eredmeny = Database.parancs.ExecuteReader();
                while (Database.eredmeny.Read())
                {
                    labelTermDB.Text = Database.eredmeny[0].ToString();
                    db = Convert.ToInt32(labelTermDB.Text);
                }
                Database.eredmeny.Close();

                string temp = "";
                Database.parancs.CommandText = $"select * from  szamla " +
                      $"inner join szamla_reszlet on szamla_reszlet.id = szamla.reszletID where szamla.id = {id}";
                Database.eredmeny = Database.parancs.ExecuteReader();
                while (Database.eredmeny.Read())
                {
                    temp = Database.eredmeny[4].ToString(); 
                }
                Database.eredmeny.Close();
                //temp.Replace("0:00:00", "");
                string[] stemp = temp.Split('.');
                
                labelDatum.Text = temp.Split('.')[0] + temp.Split('.')[1] + temp.Split('.')[2];

                ossz = db * ressz;
                labelOsszAr.Text = ossz.ToString();
            }
        }

       
    }
}
