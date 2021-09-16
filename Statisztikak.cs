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
    public partial class Statisztikak : Form
    {
        public Statisztikak()
        {
            InitializeComponent();
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

        private void button4_Click(object sender, EventArgs e)
        {
            new Rendelesek().Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new Vendeg().Show();
            this.Hide();
        }

        private void Statisztikak_FormClosing(object sender, FormClosingEventArgs e)
        {
            Database.kapcsolatBont();
            Application.Exit();
        }

        private void Statisztikak_Load(object sender, EventArgs e)
        {
            string sql = "SELECT DISTINCT varos FROM vasarlok ORDER BY varos; ";
            Database.parancs.CommandText = sql;
            Database.eredmeny = Database.parancs.ExecuteReader();
            while (Database.eredmeny.Read())
            {
                comboBoxVaros.Items.Add(Database.eredmeny[0] );
            }
            Database.eredmeny.Close();
        }

        private void buttonSzures_Click(object sender, EventArgs e)
        {
            string szures = "", rend = "";
            string varos = "";
            
            if (radioButtonMind.Checked) szures += " 1 ";
            else if (radioButtonFFI.Checked) szures += " neme = 'f' ";
            else if (radioButtonNo.Checked) szures += " neme = 'n' ";
            
            if (tBoxNevKeres.Text != "") szures += " AND nev LIKE '%" + tBoxNevKeres.Text + "%'";

            if (comboBoxVaros.SelectedIndex > -1)
            {
                varos = comboBoxVaros.SelectedItem.ToString();
                szures += " AND varos = '" + varos + "'";
            }

            if (radioNovNev.Checked) rend += "ORDER BY  nev ";
            else if (radioNovVaros.Checked) rend += "ORDER BY  varos ";
            else if (radioCsokkNev.Checked) rend += "ORDER BY  nev DESC ";
            else if (radioCsokkVaros.Checked) rend += "ORDER BY  varos DESC ";

            string sql = "SELECT nev as Név, szuldat as Született, varos as Lakhely, email as 'E-mail', telefon as Telefon, " +
                         "regisztral_dat as Regisztráció FROM vasarlok WHERE "+szures+" "+rend;

            //MessageBox.Show(sql);
            Database.parancs.CommandText = sql;
            Database.adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(Database.parancs);
            DataTable tabla = new DataTable();
            Database.adapter.Fill(tabla);
            dataGridView1.DataSource = tabla;

            string ossz = "SELECT COUNT(*) FROM vasarlok WHERE " + szures;
            Database.parancs.CommandText = ossz;
            Database.eredmeny = Database.parancs.ExecuteReader();
            while (Database.eredmeny.Read())
            {
                labelOssz.Text = Database.eredmeny[0].ToString();
            }
            Database.eredmeny.Close();
            

        }
    }
}
