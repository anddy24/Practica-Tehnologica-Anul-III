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
    public partial class formSarcina6 : Form
    {
        SqlConnection co;
        public formSarcina6()
        {
            InitializeComponent();
            co = new SqlConnection(@"Data Source =DESKTOP-9LE00L5; Database =GestiuneAngajati; Trusted_Connection=yes;");
            co.Open();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string selectQuery = @"
        SELECT Nume, Prenume, CONVERT(VARCHAR(10), DataNasterii, 104) AS DataNasterii, StagiuMunca
        FROM Angajati
        WHERE Sex = 'F' AND StagiuMunca < 5
    ";

            using (SqlCommand cmd = new SqlCommand(selectQuery, co))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        adapter.Fill(dt);

                        textBox2.Clear();

                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (var item in row.ItemArray)
                            {
                                textBox2.AppendText(item.ToString() + "    ");
                            }
                            textBox2.AppendText(Environment.NewLine);
                        }
                    }
                }
            }
        }
    }
}
