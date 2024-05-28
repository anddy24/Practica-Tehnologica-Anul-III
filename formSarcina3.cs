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
    public partial class formSarcina3 : Form
    {
        SqlConnection co;
        public formSarcina3()
        {
            InitializeComponent();
            co = new SqlConnection(@"Data Source =DESKTOP-9LE00L5; Database =GestiuneAngajati; Trusted_Connection=yes;");
            co.Open();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string selectQuery = "SELECT TOP 1 Nume, Prenume, DataNasterii FROM Angajati ORDER BY DataNasterii DESC";

            using (SqlCommand cmd = new SqlCommand(selectQuery, co))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string nume = reader["Nume"].ToString();
                        string prenume = reader["Prenume"].ToString();
                        DateTime dataNasterii = Convert.ToDateTime(reader["DataNasterii"]);

                        TimeSpan age = DateTime.Now - dataNasterii;
                        int years = (int)(age.TotalDays / 365.25);

                        MessageBox.Show($"Cel mai tânăr angajat: {nume} {prenume}. Vârsta: {years} ani.");
                    }
                    else
                    {
                        MessageBox.Show("Nu există date în tabel!");
                    }
                }
            }
        }

        private void formSarcina3_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public Button Button1
        {
            get { return button1; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlCommand command = new SqlCommand("UpdateEmployeeSalary", co))
            {
                command.CommandType = CommandType.StoredProcedure;

                int codAngajat;
                if (int.TryParse(textBox2.Text, out codAngajat))
                {
                    command.Parameters.AddWithValue("@CodAngajat", codAngajat);
                }
                else
                {
                    MessageBox.Show("Codul angajatului nu este valid.");
                    return;
                }

                decimal newSalary;
                if (decimal.TryParse(textBox1.Text, out newSalary))
                {
                    command.Parameters.AddWithValue("@NewSalary", newSalary);
                }
                else
                {
                    MessageBox.Show("Salariul nu este valid.");
                    return;
                }

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Salariul a fost actualizat cu succes!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare la actualizarea salariului: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlCommand command = new SqlCommand("GetEmployeesByDepartment", co))
            {
                command.CommandType = CommandType.StoredProcedure;

                int codDepartament;
                if (int.TryParse(textBox3.Text, out codDepartament))
                {
                    command.Parameters.AddWithValue("@CodDepartament", codDepartament);
                }
                else
                {
                    MessageBox.Show("Codul departamentului nu este valid.");
                    return;
                }

                try
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                    MessageBox.Show("Datele au fost încărcate cu succes!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare la încărcarea datelor: " + ex.Message);
                }
            }
        }
    }
}
