using System;
using System.Data;
using System.Windows.Forms;

namespace Barkacs
{
    public partial class Termekek : Form
    {
        public Termekek()
        {
            InitializeComponent();
        }

        public void Frissites()
        {
            string friss = "select * from termekek ";
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

        private void Termekek_FormClosing(object sender, FormClosingEventArgs e)
        {
            Database.kapcsolatBont();
            Application.Exit();
        }

        private void Termekek_Load(object sender, EventArgs e)
        {
            Frissites();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //menuP.Visible = false;
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

        private void buttonKeresShow_Click(object sender, EventArgs e)
        {
            KeresPanel.Visible = true;

            if (AddPanel.Visible == true || UpdatePanel.Visible == true)
            {
                AddPanel.Visible = false;
                UpdatePanel.Visible = false;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int kivalCella = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            //MessageBox.Show("lásd az értéket: " + kivalCella);
            int id = kivalCella;

            Database.parancs.CommandText = $"delete from termekek where id = '{id}' ";

            DialogResult d = MessageBox.Show($"Biztosan törölni kívánja a kiválasztott terméket? \n A kiválasztott termék azonosítója: {id}", "Törlés",
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            
            if (UpdatePanel.Visible == true)
            {
                Database.parancs.CommandText = $"select * from termekek where id = {id}";
                Database.eredmeny = Database.parancs.ExecuteReader();

                while (Database.eredmeny.Read())
                {
                    tBoxNevModos.Text = Database.eredmeny[1].ToString();
                    tBoxMarkaModos.Text = Database.eredmeny[2].ToString();
                    tBoxMennyModos.Text = Database.eredmeny[3].ToString();
                    cBoxEggyModos.Text = Database.eredmeny[4].ToString();
                    tBoxBeszArModos.Text = Database.eredmeny[5].ToString();
                    tBoxEladArModos.Text = Database.eredmeny[6].ToString();
                    tBoxAfaModos.Text = Database.eredmeny[7].ToString();
                    tBoxBruttoModos.Text = Database.eredmeny[8].ToString();
                }

                Database.eredmeny.Close();

            }
        }

        private void buttonKeres_Click(object sender, EventArgs e)
        {
            string tN = tbNevKeres.Text;
            string tM = tbMarkaKeres.Text;
            string sql = "select * from termekek  where 1 ";

            if (tN != "")
                sql += $" and nev like '%{tN}%' ";

            if (tM != "")
                sql += $" and marka like '%{tM}%' ";

            
            Database.parancs.CommandText = sql;
            Database.adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(Database.parancs);
            DataTable tabla = new DataTable();
            Database.adapter.Fill(tabla);
            dataGridView1.DataSource = tabla;
        }

        private void tbNevKeres_TextChanged(object sender, EventArgs e)
        {
            string tN = tbNevKeres.Text;
            string tM = tbMarkaKeres.Text;

            if (tN == "" && tM == "")
            {
                string sql = "select * from termekek  where 1 ";

                Database.parancs.CommandText = sql;
                Database.adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(Database.parancs);
                DataTable tabla = new DataTable();
                Database.adapter.Fill(tabla);
                dataGridView1.DataSource = tabla;
            }
        }

        private void buttonFelvesz_Click(object sender, EventArgs e)
        {
            int temp = 0;

            string nev = tBoxNevAdd.Text;
            string marka = tBoxMarkaAdd.Text;
            Int32.TryParse(tBoxMennyAdd.Text, out temp);
            int menny = temp;
            temp = 0;

            string meggys = "";
            if (cBoxEggyAdd.SelectedIndex < 0)
                MessageBox.Show("Nincs kiválasztva mennyiségi egység!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
              meggys = cBoxEggyAdd.SelectedItem.ToString();
                

            Int32.TryParse(tBoxBeszArAdd.Text, out temp);
            int beszAr = temp;
            temp = 0;

            Int32.TryParse(tBoxEladArAdd.Text, out temp);
            int eladAr = temp;
            temp = 0;

            Int32.TryParse(tBoxAfaAdd.Text, out temp);
            int afa = temp;
            temp = 0;

            Int32.TryParse(tBoxBruttoAdd.Text, out temp);
            int brutto = temp;
            temp = 0;

            if (nev == "" || marka == "" || menny == 0 || meggys == "" || beszAr == 0 ||
                eladAr == 0 || afa == 0 || brutto == 0 )
            {
                MessageBox.Show("Valami hiba történt a felvétel során! \n Lehetséges, hogy üres mezőt adott meg!", "Hiba",
                                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Database.parancs.CommandText = $"insert into termekek values (null,'{nev}', '{marka}', '{menny}', " +
                                           $"'{meggys}', '{beszAr}', '{eladAr}', '{afa}', '{brutto}')";
                Database.parancs.ExecuteNonQuery();
            }
            /**/
            Frissites();

            tBoxNevAdd.Text = "";
            tBoxMarkaAdd.Text = "";
            tBoxMennyAdd.Text = "";
            cBoxEggyAdd.Text = "";
            tBoxBeszArAdd.Text = "";
            tBoxEladArAdd.Text = "";
            tBoxAfaAdd.Text = "";
            tBoxBruttoAdd.Text = "";
        }

        private void buttonModos_Click(object sender, EventArgs e)
        {
            int temp = 0;
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            string nev = tBoxNevModos.Text;
            string marka = tBoxMarkaModos.Text;
            Int32.TryParse(tBoxMennyModos.Text, out temp);
            int menny = temp;
            temp = 0;
            //MessageBox.Show(cBoxEggyModos+"\n"+ tBoxNevAdd+"\n"+ tBoxBeszArAdd);
            string meggys = cBoxEggyModos.SelectedItem.ToString();

            Int32.TryParse(tBoxBeszArModos.Text, out temp);
            int beszAr = temp;
            temp = 0;

            Int32.TryParse(tBoxEladArModos.Text, out temp);
            int eladAr = temp;
            temp = 0;
            
            Int32.TryParse(tBoxAfaModos.Text, out temp);
            int afa = temp;
            temp = 0;

            Int32.TryParse(tBoxBruttoModos.Text, out temp);
            int brutto = temp;
            temp = 0;

            if (nev == "" && marka == "" && menny == 0 && meggys == "" && beszAr == 0 && eladAr == 0 && afa == 0 && brutto == 00)
                MessageBox.Show("Hiba!!!!!!!");

            
            Database.parancs.CommandText = $"update termekek set nev = '{nev}', marka = '{marka}', mennyiseg = '{menny}', " +
                                           $" m_egys = '{meggys}', besz_ar = '{beszAr}', eladasi_ar = '{eladAr}', afa = {afa}, " +
                                           $"brutto_ar = '{brutto}' where id = '{id}' ";
            Database.parancs.ExecuteNonQuery();

            Frissites();
            dataGridView1.Rows[id - 1].Selected = true;
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
