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
    public partial class formSarcina9 : Form
    {
        SqlConnection co;
        public formSarcina9()
        {
            InitializeComponent();
            co = new SqlConnection(@"Data Source =DESKTOP-9LE00L5; Database =GestiuneAngajati; Trusted_Connection=yes;");
            co.Open();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem == null)
            {
                MessageBox.Show("Selectați luna pentru afișarea topului.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedMonth = Convert.ToInt32(comboBox4.SelectedItem);

            string selectQuery = $@"
        SELECT TOP 3 A.Nume, A.Prenume, O.OreLucrate
        FROM Angajati A
        INNER JOIN OreLucrate O ON A.CodAngajat = O.CodAngajat
        WHERE O.Luna = {selectedMonth}
        ORDER BY O.OreLucrate DESC
    ";

            using (SqlCommand cmd = new SqlCommand(selectQuery, co))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    StringBuilder resultBuilder = new StringBuilder();
                    resultBuilder.AppendLine($"Top 3 cei mai buni lucrători ai lunii {selectedMonth}:");
                    resultBuilder.AppendLine();

                    while (reader.Read())
                    {
                        string nume = reader["Nume"].ToString();
                        string prenume = reader["Prenume"].ToString();
                        int oreLucrate = Convert.ToInt32(reader["OreLucrate"]);

                        resultBuilder.AppendLine($"Nume: {nume}, Prenume: {prenume}, Ore lucrate: {oreLucrate}");
                    }

                    MessageBox.Show(resultBuilder.ToString(), "Top Lucrători", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
