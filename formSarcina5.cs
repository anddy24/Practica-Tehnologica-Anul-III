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
    public partial class formSarcina5 : Form
    {
        SqlConnection co;
        public formSarcina5()
        {
            InitializeComponent();
            co = new SqlConnection(@"Data Source =DESKTOP-9LE00L5; Database =GestiuneAngajati; Trusted_Connection=yes;");
            co.Open();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Selectați un departament din lista derulantă.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string departmentName = comboBox1.SelectedItem.ToString();

            string selectQuery = $@"
        SELECT AVG(Salariu) AS SalariuMediu
        FROM Angajati AS A
        INNER JOIN Profesii AS P ON A.CodProfesie = P.CodProfesie
        INNER JOIN Departamente AS D ON P.CodDepartament = D.CodDepartament
        WHERE A.Sex = 'M' AND D.Denumire = @DepartmentName
    ";

            using (SqlCommand cmd = new SqlCommand(selectQuery, co))
            {
                cmd.Parameters.AddWithValue("@DepartmentName", departmentName);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    decimal averageSalary = Convert.ToDecimal(result);
                    MessageBox.Show($"Salariul mediu lunar al bărbaților din departamentul \"{departmentName}\" este: {averageSalary:C}", "Salariu Mediu Lunar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Nu există angajați bărbați în departamentul \"{departmentName}\".", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        SELECT dbo.GetDepartmentName(Profesii.CodDepartament) AS DepartmentName
        FROM Angajati 
        JOIN Profesii ON Angajati.CodProfesie = Profesii.CodProfesie
        WHERE Angajati.CodAngajat = @CodAngajat";

            using (SqlCommand command = new SqlCommand(query, co))
            {
                command.Parameters.AddWithValue("@CodAngajat", codAngajat);

                try
                {
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string departmentName = result.ToString();
                        MessageBox.Show($"Numele departamentului este: {departmentName}");
                    }
                    else
                    {
                        MessageBox.Show("Angajatul nu a fost găsit.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare la obținerea numelui departamentului: " + ex.Message);
                }
            }
        }
    }
}
