using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PracticaTehnologica
{
    public partial class side : Form
    {
        SqlConnection co;

        formAfisare afisare;
        InregistrareAngajati sarcina1;
        formExport export;
        formSarcina2 sarcina2;
        formSarcina3 sarcina3;
        formSarcina4 sarcina4;
        formSarcina5 sarcina5;
        formSarcina6 sarcina6;
        formSarcina7 sarcina7;
        formSarcina8 sarcina8;
        formSarcina9 sarcina9;



        public side()
        {
            InitializeComponent();
            co = new SqlConnection(@"Data Source =DESKTOP-9LE00L5; Database =GestiuneAngajati; Trusted_Connection=yes;");
            co.Open();
            sidebar.Hide();
        }

        public void EnableAdminFeatures()
        {
            // activam functii specifice adminului
            button16.Enabled = true;
            button17.Enabled = true;
        }

        public void DisableRegularUserFeatures()
        {
            // dezactivam functii care afecteaza baza de date, cum ar operatiile pentru delete, insert, update
            button16.Enabled = false;
            button17.Enabled = false;
            if (sarcina3 != null)
            {
                sarcina3.Button1.Enabled = false;
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            pictureBox2.Hide();
            panel2.Hide();
            if (afisare != null)
            {
                afisare.Close();
            }

            if (afisare == null)
            {
                afisare = new formAfisare();
                afisare.FormClosed += Afisare_FormClosed;
                afisare.MdiParent = this;
                afisare.Dock = DockStyle.Fill;
                afisare.Show();
            }
            else
            {
                afisare.Activate();
            }
        }

        private void Afisare_FormClosed(object sender, FormClosedEventArgs e)
        {
            afisare = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        bool menuExpand = false;
        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if(menuExpand == false)
            {
                menuContainer.Height += 15;
                if(menuContainer.Height >= 397) {
                    menuTransition.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                menuContainer.Height -= 15;
                if(menuContainer.Height <= 40)
                {
                    menuTransition.Stop();
                    menuExpand = false;
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            menuTransition.Start();
        }

        bool sidebarExpand = true;
        private void sidebarTransation_Tick(object sender, EventArgs e)
        {

            if (sidebarExpand)
            {
                sidebar.Width -= 5;
                if (sidebar.Width <= 40)
                {
                    sidebarExpand = false;
                    sidebarTransation.Stop();

                    button12.Width = sidebar.Width;
                    button13.Width = sidebar.Width;
                    button14.Width = sidebar.Width;
                    menuContainer.Width = sidebar.Width;
                }
                
            }
            else
            {
                sidebar.Width += 5;
                if (sidebar.Width >= 200 )
                {
                    sidebarExpand = true;
                    sidebarTransation.Stop();

                    button12.Width = sidebar.Width;
                    button13.Width = sidebar.Width;
                    button14.Width = sidebar.Width;
                    menuContainer.Width = sidebar.Width;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            sidebarTransation.Start();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            pictureBox2.Hide();
            panel2.Hide();
            if (sarcina1 != null)
            {
                sarcina1.Close();
            }

            if (sarcina1 == null)
            {
                sarcina1 = new InregistrareAngajati();
                sarcina1.FormClosed += Sarcina1_FormClosed;
                sarcina1.MdiParent = this;
                sarcina1.Dock = DockStyle.Fill;
                sarcina1.Show();
            }
            else
            {
                sarcina1.Activate();
            }
        }

        private void Sarcina1_FormClosed(object sender, FormClosedEventArgs e)
        {
            sarcina1 = null;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            pictureBox2.Hide();
            panel2.Hide();
            if (export != null)
            {
                export.Close();
            }

            if (export == null)
            {
                export = new formExport();
                export.FormClosed += Export_FormClosed;
                export.MdiParent = this;
                export.Dock = DockStyle.Fill;
                export.Show();
            }
            else
            {
                export.Activate();
            }
        }

        private void Export_FormClosed(object sender, FormClosedEventArgs e)
        {
            export = null;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            pictureBox2.Hide();
            panel2.Hide();
            if (sarcina2 != null)
            {
                sarcina2.Close();
            }

            if (sarcina2 == null)
            {
                sarcina2 = new formSarcina2();
                sarcina2.FormClosed += Sarcina2_FormClosed;
                sarcina2.MdiParent = this;
                sarcina2.Dock = DockStyle.Fill;
                sarcina2.Show();
            }
            else
            {
                sarcina2.Activate();
            }
        }

        private void Sarcina2_FormClosed(object sender, FormClosedEventArgs e)
        {
            sarcina2 = null;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            pictureBox2.Hide();
            panel2.Hide();
            if (sarcina3 != null)
            {
                sarcina3.Close();
            }

            if (sarcina3 == null)
            {
                sarcina3 = new formSarcina3();
                sarcina3.FormClosed += Sarcina3_FormClosed;
                sarcina3.MdiParent = this;
                sarcina3.Dock = DockStyle.Fill;
                sarcina3.Show();
                DisableRegularUserFeatures();
            }
            else
            {
                sarcina3.Activate();
            }
        }
        private void Sarcina3_FormClosed(object sender, FormClosedEventArgs e)
        {
            sarcina3 = null;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            pictureBox2.Hide();
            panel2.Hide();
            if (sarcina4 != null)
            {
                sarcina4.Close();
            }

            if (sarcina4 == null)
            {
                sarcina4 = new formSarcina4();
                sarcina4.FormClosed += Sarcina4_FormClosed;
                sarcina4.MdiParent = this;
                sarcina4.Dock = DockStyle.Fill;
                sarcina4.Show();
            }
            else
            {
                sarcina4.Activate();
            }
        }

        private void Sarcina4_FormClosed(object sender, FormClosedEventArgs e)
        {
            sarcina4 = null;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            pictureBox2.Hide();
            panel2.Hide();
            if (sarcina5 != null)
            {
                sarcina5.Close();
            }

            if (sarcina5 == null)
            {
                sarcina5 = new formSarcina5();
                sarcina5.FormClosed += Sarcina5_FormClosed;
                sarcina5.MdiParent = this;
                sarcina5.Dock = DockStyle.Fill;
                sarcina5.Show();
            }
            else
            {
                sarcina5.Activate();
            }
        }
        private void Sarcina5_FormClosed(object sender, FormClosedEventArgs e)
        {
            sarcina5 = null;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            pictureBox2.Hide();
            panel2.Hide();
            if (sarcina6 != null)
            {
                sarcina6.Close();
            }
            if (sarcina6 == null)
            {
                sarcina6 = new formSarcina6();
                sarcina6.FormClosed += Sarcina6_FormClosed;
                sarcina6.MdiParent = this;
                sarcina6.Dock = DockStyle.Fill;
                sarcina6.Show();
            }
            else
            {
                sarcina6.Activate();
            }
        }
        private void Sarcina6_FormClosed(object sender, FormClosedEventArgs e)
        {
            sarcina6 = null;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            pictureBox2.Hide();
            panel2.Hide();
            if (sarcina7 != null)
            {
                sarcina7.Close();
            }

            if (sarcina7 == null)
            {
                sarcina7 = new formSarcina7();
                sarcina7.FormClosed += Sarcina7_FormClosed;
                sarcina7.MdiParent = this;
                sarcina7.Dock = DockStyle.Fill;
                sarcina7.Show();
            }
            else
            {
                sarcina7.Activate();
            }
        }
        private void Sarcina7_FormClosed(object sender, FormClosedEventArgs e)
        {
            sarcina7 = null;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            pictureBox2.Hide();
            panel2.Hide();
            if (sarcina8 != null)
            {
                sarcina8.Close();
            }
            if (sarcina8 == null)
            {
                sarcina8 = new formSarcina8();
                sarcina8.FormClosed += Sarcina8_FormClosed;
                sarcina8.MdiParent = this;
                sarcina8.Dock = DockStyle.Fill;
                sarcina8.Show();
            }
            else
            {
                sarcina8.Activate();
            }
        }
        private void Sarcina8_FormClosed(object sender, FormClosedEventArgs e)
        {
            sarcina8 = null;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            pictureBox2.Hide();
            panel2.Hide();
            if (sarcina9 != null)
            {
                sarcina9.Close();
            }

            if (sarcina9 == null)
            {
                sarcina9 = new formSarcina9();
                sarcina9.FormClosed += Sarcina9_FormClosed;
                sarcina9.MdiParent = this;
                sarcina9.Dock = DockStyle.Fill;
                sarcina9.Show();
            }
            else
            {
                sarcina9.Activate();
            }
        }
        private void Sarcina9_FormClosed(object sender, FormClosedEventArgs e)
        {
            sarcina9 = null;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void sidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string numeUtilizator = textBox1.Text;
            string parola = textBox2.Text;

            if (string.IsNullOrEmpty(numeUtilizator) || string.IsNullOrEmpty(parola))
            {
                MessageBox.Show("Introduceți numele utilizatorului și parola.");
                return;
            }

            string query = "EXEC sp_VerifyUser @nume_utilizator, @parola";

            using (SqlCommand command = new SqlCommand(query, co))
            {
                command.Parameters.AddWithValue("@nume_utilizator", numeUtilizator);
                command.Parameters.AddWithValue("@parola", parola);

                try
                {
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string tipUtilizator = result.ToString();
                        if (tipUtilizator == "admin")
                        {
                            MessageBox.Show("Autentificare reușită! Aveți acces complet.");
                            EnableAdminFeatures();

                            panel3.Hide();
                            sidebar.Show();
                            
                        }
                        else if (tipUtilizator == "regular user")
                        {
                            MessageBox.Show("Autentificare reușită! Aveți acces limitat.");
                            DisableRegularUserFeatures();

                            panel3.Hide();
                            sidebar.Show();
                        }
                        else
                        {
                            MessageBox.Show("Tipul de utilizator necunoscut.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nume utilizator sau parola incorectă.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare la autentificare: " + ex.Message);
                }
            }
        }
    }
}
