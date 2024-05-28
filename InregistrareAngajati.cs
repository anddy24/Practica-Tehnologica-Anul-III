using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaTehnologica
{
    public partial class InregistrareAngajati : Form
    {
        SqlConnection co;
        public InregistrareAngajati()
        {
            InitializeComponent();
            co = new SqlConnection(@"Data Source =DESKTOP-9LE00L5; Database =GestiuneAngajati; Trusted_Connection=yes;");
            co.Open();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlCommand command = new SqlCommand("AddNewEmployee", co))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CodAngajat", textBox1.Text);
                command.Parameters.AddWithValue("@Nume", textBox2.Text);
                command.Parameters.AddWithValue("@Prenume", textBox3.Text);
                command.Parameters.AddWithValue("@DataNasterii", DateTime.Parse(textBox4.Text));
                command.Parameters.AddWithValue("@Sex", comboBox1.Text);
                command.Parameters.AddWithValue("@CodProfesie", int.Parse(comboBox2.Text));
                command.Parameters.AddWithValue("@Salariu", textBox5.Text);
                command.Parameters.AddWithValue("@StagiuMunca", int.Parse(textBox6.Text));

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Datele au fost inserate cu succes!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare la inserarea datelor: " + ex.Message);
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void For_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
