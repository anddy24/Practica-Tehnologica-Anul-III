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
    public partial class formSarcina7 : Form
    {
        SqlConnection co;
        public formSarcina7()
        {
            InitializeComponent();
            co = new SqlConnection(@"Data Source =DESKTOP-9LE00L5; Database =GestiuneAngajati; Trusted_Connection=yes;");
            co.Open();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Selectați un cod de profesie din lista derulantă.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedProfessionCode = Convert.ToInt32(comboBox3.SelectedItem);

            string selectQuery = @"
        SELECT COUNT(*) AS TotalAngajati
        FROM Angajati
        WHERE CodProfesie = @ProfessionCode
    ";

            using (SqlCommand cmd = new SqlCommand(selectQuery, co))
            {
                cmd.Parameters.AddWithValue("@ProfessionCode", selectedProfessionCode);

                object result = cmd.ExecuteScalar();
                int totalAngajati = Convert.ToInt32(result);

                MessageBox.Show($"Numărul de angajați pentru codul profesiei {selectedProfessionCode} este: {totalAngajati}", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
