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
    public partial class formSarcina2 : Form
    {
        SqlConnection co;
        public formSarcina2()
        {
            InitializeComponent();
            co = new SqlConnection(@"Data Source =DESKTOP-9LE00L5; Database =GestiuneAngajati; Trusted_Connection=yes;");
            co.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string deletesql = "DELETE FROM Angajati WHERE CodAngajat = @CodAngajat";
                using (SqlCommand cmd = new SqlCommand(deletesql, co))
                {
                    cmd.Parameters.AddWithValue("@CodAngajat", textBox1.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Angajatul a fost șters cu succes!");
                    }
                    else
                    {
                        MessageBox.Show("Nu s-a găsit niciun angajat cu acest cod.");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("A apărut o eroare în timpul ștergerii angajatului: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare neașteptată: " + ex.Message);
            }
        }
    }
}
