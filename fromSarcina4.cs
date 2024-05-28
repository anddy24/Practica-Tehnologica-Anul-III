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
    public partial class formSarcina4 : Form
    {
        SqlConnection co;
        public formSarcina4()
        {
            InitializeComponent();
            co = new SqlConnection(@"Data Source =DESKTOP-9LE00L5; Database =GestiuneAngajati; Trusted_Connection=yes;");
            co.Open();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int retirementAgeWomen = 57;
            int retirementAgeMen = 62;

            DateTime retirementThresholdWomen = DateTime.Today.AddYears(-retirementAgeWomen);
            DateTime retirementThresholdMen = DateTime.Today.AddYears(-retirementAgeMen);

            string selectQuery = $"SELECT Nume, Prenume, DataNasterii, Sex FROM Angajati WHERE (Sex = 'F' AND DataNasterii <= '{retirementThresholdWomen:yyyy-MM-dd}') OR (Sex = 'M' AND DataNasterii <= '{retirementThresholdMen:yyyy-MM-dd}') ORDER BY DataNasterii ASC";

            {

                using (SqlCommand cmd = new SqlCommand(selectQuery, co))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        string message = "Lista angajaților de vârstă pensionară:\n";

                        while (reader.Read())
                        {
                            string nume = reader["Nume"].ToString();
                            string prenume = reader["Prenume"].ToString();
                            DateTime dataNasterii = Convert.ToDateTime(reader["DataNasterii"]);
                            string sex = reader["Sex"].ToString();

                            DateTime today = DateTime.Today;
                            int age = today.Year - dataNasterii.Year;
                            if (dataNasterii > today.AddYears(-age))
                            {
                                age--;
                            }

                            message += $"{nume} {prenume} - Sex: {sex}, Vârstă: {age}\n";
                        }

                        MessageBox.Show(message, "Lista Angajaților de Vârstă Pensionară", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Nu există angajați care să fi împlinit vârsta de pensionare.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int codAngajat;
            if (!int.TryParse(textBox1.Text, out codAngajat))
            {
                MessageBox.Show("Codul angajatului nu este valid.");
                return;
            }

            string query = @"
        SELECT dbo.CalculateAge(DataNasterii) AS Age 
        FROM Angajati 
        WHERE CodAngajat = @CodAngajat";

            using (SqlCommand command = new SqlCommand(query, co))
            {
                command.Parameters.AddWithValue("@CodAngajat", codAngajat);

                try
                {
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        int age = Convert.ToInt32(result);
                        MessageBox.Show($"Vârsta angajatului este: {age}");
                    }
                    else
                    {
                        MessageBox.Show("Angajatul nu a fost găsit.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare la calcularea vârstei: " + ex.Message);
                }
            }
        }
    }
}
