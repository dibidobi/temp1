using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Barkacs
{
    public partial class Vasarlok : Form
    {
        public Vasarlok()
        {
            InitializeComponent();
        }

        public void Frissites()
        {
            string friss = "select * from vasarlok ";
            Database.parancs.CommandText = friss;
            Database.adapter = new MySqlDataAdapter(Database.parancs);
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

        private void Vasarlok_FormClosing(object sender, FormClosingEventArgs e)
        {
            Database.kapcsolatBont();
            Application.Exit();
        }

        private void Vasarlok_Load(object sender, EventArgs e)
        {
            Frissites();
        }

        private void buttonAddShow_Click(object sender, EventArgs e)
        {
            AddPanel.Visible = true;

            if (UpdatePanel.Visible == true || KeresPanel.Visible == true)
            {
                UpdatePanel.Visible = false;
                KeresPanel.Visible = false;
            }
        }

        private void buttonUpdateShow_Click(object sender, EventArgs e)
        {
            UpdatePanel.Visible = true;

            if (AddPanel.Visible == true || KeresPanel.Visible == true)
            {
                AddPanel.Visible = false;
                KeresPanel.Visible = false;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int kivalCella = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            //MessageBox.Show("lásd az értéket: " + kivalCella);
            int id = kivalCella;

            Database.parancs.CommandText = $"delete from vasarlok where id = '{id}' ";

            DialogResult d = MessageBox.Show($"Biztosan törölni kívánja a kiválasztott vásárlót? \n A kiválasztott vásárló azonosítója: {id}", "Törlés",
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

        private void buttonKeresShow_Click(object sender, EventArgs e)
        {
            KeresPanel.Visible = true;

            if (AddPanel.Visible == true || UpdatePanel.Visible == true)
            {
                AddPanel.Visible = false;
                UpdatePanel.Visible = false;
            }
        }

        private void buttonKeres_Click(object sender, EventArgs e)
        {
            string tN = tbNevKeres.Text;
            string tT = tbTelKeres.Text;
            string sql = "select * from vasarlok  where 1 ";

            if (tN != "")
                sql += $" and nev like '%{tN}%' ";

            if (tT != "")
                sql += $" and telefon like '%{tT}%' ";


            Database.parancs.CommandText = sql;
            Database.adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(Database.parancs);
            DataTable tabla = new DataTable();
            Database.adapter.Fill(tabla);
            dataGridView1.DataSource = tabla;
        }

        private void tbNevKeres_TextChanged(object sender, EventArgs e)
        {
            string tN = tbNevKeres.Text;
            string tT = tbTelKeres.Text;

            if (tN == "" && tT == "")
            {
                string sql = "select * from vasarlok  where 1 ";

                Database.parancs.CommandText = sql;
                Database.adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(Database.parancs);
                DataTable tabla = new DataTable();
                Database.adapter.Fill(tabla);
                dataGridView1.DataSource = tabla;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            if (UpdatePanel.Visible == true)
            {
                Database.parancs.CommandText = $"select * from vasarlok where id = {id}";
                Database.eredmeny = Database.parancs.ExecuteReader();

                while (Database.eredmeny.Read())
                {
                    tBoxNevModos.Text = Database.eredmeny[1].ToString();
                    tBoxFelhModos.Text = Database.eredmeny[2].ToString();
                    cBoxNemModos.Text = Database.eredmeny[3].ToString();
                    tBoxSzulModos.Text = Database.eredmeny[4].ToString();
                    tBoxIranyszModos.Text = Database.eredmeny[5].ToString();
                    tBoxVarosModos.Text = Database.eredmeny[6].ToString();
                    tBoxCimModos.Text = Database.eredmeny[7].ToString();
                    tBoxEmailModos.Text = Database.eredmeny[8].ToString();
                    tBoxPassModos.Text = Database.eredmeny[9].ToString();
                    tBoxTelModos.Text = Database.eredmeny[10].ToString();
                    tBoxRegDatModos.Text = Database.eredmeny[11].ToString();
                }

                Database.eredmeny.Close();

            }
        }

        private void buttonModos_Click(object sender, EventArgs e)
        {
            int temp = 0;
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value); 
            string nev = tBoxNevModos.Text;
            string felh = tBoxFelhModos.Text;
            string neme = cBoxNemModos.SelectedItem.ToString();
            string szul = tBoxSzulModos.Text;
            //Database.parancs.Parameters.Add(szul, MySqlDbType.Date);//dátum típusúvá alakítja
            Int32.TryParse(tBoxIranyszModos.Text, out temp);
            int iranysz = temp;
            temp = 0;
                        
            string varos = tBoxVarosModos.Text;
            string cim = tBoxCimModos.Text;
            string email = tBoxEmailModos.Text;
            string pass = tBoxPassModos.Text;
            string tel = tBoxTelModos.Text;
            string regD = tBoxRegDatModos.Text;
            //new MySqlParameter(regD, MySqlDbType.Date);

            if (nev == "" && neme == "" && szul == "" && iranysz == 0 && varos == "" && 
                cim == "" && email == "" && pass == "" && tel == "" && regD == "")
                MessageBox.Show("Hiba!!!!!!!");


            Database.parancs.CommandText = $"update vasarlok set nev = '{nev}', felhaszNev = '{felh}', neme = '{neme}', szuldat = '{szul}', " +
                                           $" iranyitoszam = '{iranysz}', varos = '{varos}', cim = '{cim}', email = '{email}', " +
                                           $"pass = '{pass}', telefon = '{tel}', regisztral_dat = '{regD}' where id = '{id}' ";
            //MessageBox.Show(Database.parancs.CommandText+"");
            Database.parancs.ExecuteNonQuery();

            Frissites();
            //dataGridView1.Rows[id - 1].Selected = true;
        }

        private void buttonFelvesz_Click(object sender, EventArgs e)
        {
            int temp = 0;
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            string nev = tBoxNevAdd.Text;
            string felh = tBoxFelhAdd.Text;

            string neme = "";
            if (cBoxNemAdd.SelectedIndex < 0)
                MessageBox.Show("Nincs kiválasztva a vásárló neme!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                neme = cBoxNemAdd.SelectedItem.ToString();

            string szul = tBoxSzulAdd.Text;

            Int32.TryParse(tBoxIranyszAdd.Text, out temp);
            int iranysz = temp;
            temp = 0;

            string varos = tBoxVarosAdd.Text;
            string cim = tBoxCimAdd.Text;
            string email = tBoxEmailAdd.Text;
            string pass = tBoxPassAdd.Text;
            string tel = tBoxTelAdd.Text;
            string regD = tBoxRegDatAdd.Text;

            if (nev == "" && neme == "" && szul == "" && iranysz == 0 && varos == "" &&
                cim == "" && email == "" && pass == "" && tel == "" && regD == "")
                MessageBox.Show("Hiba!!!!!!!");

            if(nev == "" || neme == "" || szul == "" || iranysz == 0 || varos == "" ||
                cim == "" || email == "" || pass == "" || tel == "" || regD == "")
            {
                MessageBox.Show("Valami hiba történt a felvétel során! \n Lehetséges, hogy üres mezőt adott meg!", "Hiba",
                                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Database.parancs.CommandText = $"insert into vasarlok values (null, '{nev}', '{felh}', '{neme}', '{szul}', '{iranysz}', " +
                                           $" '{varos}', '{cim}', '{email}', '{pass}', '{tel}', '{regD}' )";
                Database.parancs.ExecuteNonQuery();
            }
            /**/
            Frissites();

            tBoxNevAdd.Text = "";
            tBoxFelhAdd.Text = "";
            cBoxNemAdd.Text = "";
            tBoxSzulAdd.Text = "";
            tBoxIranyszAdd.Text = "";
            tBoxVarosAdd.Text = "";
            tBoxCimAdd.Text = "";
            tBoxEmailAdd.Text = "";
            tBoxPassAdd.Text = "";
            tBoxTelAdd.Text = "";
            tBoxRegDatAdd.Text = "";
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
