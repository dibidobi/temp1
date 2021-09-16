using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Barkacs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int w = 0;

        private void belepes()
        {
            string fhELL = "", passELL = "";

            string fh = tbFelhasz.Text;
            string pass = tbPass.Text;
            string sql = "select nev, pass from admin";
            Database.parancs.CommandText = sql;
            Database.eredmeny = Database.parancs.ExecuteReader();

            while (Database.eredmeny.Read())
            {
                fhELL = Database.eredmeny[0].ToString();
                passELL = Database.eredmeny[1].ToString();
            }

            Database.eredmeny.Close();  //while ciklus végén le kell zárni az eredmény olvasását

            if (fhELL.Equals(fh) && passELL.Equals(pass))
            {
                //MessageBox.Show("Sikeres Belépés!");
                this.Hide();
                new KezdoLap().Show();
            }
            else
            {
                MessageBox.Show("Sikertelen Belépés! \nFelhasználó vagy jelszó hiba!");
                tbPass.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            belepes();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Database.kapcsol();
        }

        private void tbPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {           
                belepes();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (w % 2 == 0 )
            {
                //tbPass.UseSystemPasswordChar = true;
                pictureBox2.BackgroundImage = Barkacs.Properties.Resources.passHide;
                tbPass.PasswordChar = '\0'; 
            }
            else
            {
                //tbPass.UseSystemPasswordChar = false;
                pictureBox2.BackgroundImage = Barkacs.Properties.Resources.passShow;
                tbPass.PasswordChar = '*';
            }
            w++;
        }

    }
}
