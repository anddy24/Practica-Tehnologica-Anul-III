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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PracticaTehnologica
{
    public partial class formSarcina8 : Form
    {
        SqlConnection co;
        public formSarcina8()
        {
            InitializeComponent();
            co = new SqlConnection(@"Data Source =DESKTOP-9LE00L5; Database =GestiuneAngajati; Trusted_Connection=yes;");
            co.Open();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string selectQuery = @"
        SELECT A.Nume, A.Prenume, O.Luna, O.OreLucrate
        FROM Angajati A
        LEFT JOIN OreLucrate O ON A.CodAngajat = O.CodAngajat
    ";


            using (SqlCommand cmd = new SqlCommand(selectQuery, co))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                using (DataTable dt = new DataTable())
                {
                    adapter.Fill(dt);
                    textBox2.Clear();

                    textBox2.AppendText("Nume\tPrenume\tLuna\tOre Lucrate" + Environment.NewLine);

                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            textBox2.AppendText(item.ToString() + "\t"); // Append tab character to separate columns
                        }
                        textBox2.AppendText(Environment.NewLine);
                    }
                }
            }


        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int codAngajat;
            if (!int.TryParse(textBox1.Text, out codAngajat))
            {
                MessageBox.Show("Codul angajatului nu este valid.");
                return;
            }

            string query = "SELECT dbo.GetTotalWorkedHours(@CodAngajat) AS TotalWorkedHours";

            using (SqlCommand command = new SqlCommand(query, co))
            {
                command.Parameters.AddWithValue("@CodAngajat", codAngajat);

                try
                {
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        int totalWorkedHours = Convert.ToInt32(result);
                        MessageBox.Show($"Totalul orelor lucrate este: {totalWorkedHours}");
                    }
                    else
                    {
                        MessageBox.Show("Angajatul nu a fost găsit.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare la calcularea totalului orelor lucrate: " + ex.Message);
                }
            }
        }
    }
}
