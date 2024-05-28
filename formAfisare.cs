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
    public partial class formAfisare : Form
    {
        SqlConnection co;
        public formAfisare()
        {
            InitializeComponent();
            co = new SqlConnection(@"Data Source =DESKTOP-9LE00L5; Database =GestiuneAngajati; Trusted_Connection=yes;");
            co.Open();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string query = "SELECT CodAngajat, Nume, Prenume, DataNasterii, Sex, CodProfesie, Salariu, StagiuMunca FROM Angajati";

            SqlDataAdapter adapter = new SqlDataAdapter(query, co);
            System.Data.DataTable dt = new System.Data.DataTable();
            adapter.Fill(dt);

            for (int i = 0; i < 12; i++)
            {
                Label label = (Label)this.Controls.Find("label" + (12 + i), true)[0];
                label.Text = string.Empty;
            }

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Label label = (Label)this.Controls.Find("label" + (12 + i), true)[0];
                label.Text = dt.Columns[i].ColumnName;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Label label = (Label)this.Controls.Find("label" + (12 + j), true)[0];
                    if (dt.Columns[j].ColumnName == "DataNasterii") 
                    {
                        DateTime dateValue = Convert.ToDateTime(row[j]);
                        label.Text += "\n" + dateValue.ToString("dd.MM.yyyy");
                    }
                    else
                    {
                        label.Text += "\n" + row[j].ToString();
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = @"
SELECT A.CodAngajat, A.Nume, A.Prenume, A.DataNasterii, A.Sex, A.CodProfesie, A.Salariu, A.StagiuMunca,
       D.Denumire AS DenumireDepartament, P.Denumire AS DenumireProfesie
FROM Angajati A
LEFT JOIN Profesii P ON A.CodProfesie = P.CodProfesie
LEFT JOIN Departamente D ON P.CodDepartament = D.CodDepartament";

            SqlDataAdapter adapter = new SqlDataAdapter(query, co);
            System.Data.DataTable dt = new System.Data.DataTable();
            adapter.Fill(dt);


            for (int i = 0; i < 12; i++) 
            {
                Label label = (Label)this.Controls.Find("label" + (12 + i), true)[0];
                label.Text = string.Empty;
            }

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Label label = (Label)this.Controls.Find("label" + (12 + i), true)[0];
                label.Text = dt.Columns[i].ColumnName;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Label label = (Label)this.Controls.Find("label" + (12 + j), true)[0];
                    if (dt.Columns[j].ColumnName == "DataNasterii")
                    {
                        DateTime dateValue = Convert.ToDateTime(row[j]);
                        label.Text += "\n" + dateValue.ToString("dd.MM.yyyy");
                    }
                    else
                    {
                        label.Text += "\n" + row[j].ToString();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "SELECT CodAngajat, Luna, OreLucrate FROM OreLucrate";

            SqlDataAdapter adapter = new SqlDataAdapter(query, co);
            System.Data.DataTable dt = new System.Data.DataTable();
            adapter.Fill(dt);

            for (int i = 0; i < 12; i++)
            {
                Label label = (Label)this.Controls.Find("label" + (12 + i), true)[0];
                label.Text = string.Empty;
            }

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Label label = (Label)this.Controls.Find("label" + (12 + i), true)[0];
                label.Text = dt.Columns[i].ColumnName;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Label label = (Label)this.Controls.Find("label" + (12 + j), true)[0];
                    label.Text += "\n" + row[j].ToString();
                }
            }
        }

        private void formAfisare_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT CodProfesie, Denumire, CodDepartament FROM Profesii";

            SqlDataAdapter adapter = new SqlDataAdapter(query, co);
            System.Data.DataTable dt = new System.Data.DataTable();
            adapter.Fill(dt);

            for (int i = 0; i < 12; i++)
            {
                Label label = (Label)this.Controls.Find("label" + (12 + i), true)[0];
                label.Text = string.Empty;
            }

            for (int i = 0; i < 3; i++)
            {
                Label label = (Label)this.Controls.Find("label" + (12 + i), true)[0];
                label.Text = string.Empty;
            }

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Label label = (Label)this.Controls.Find("label" + (12 + i), true)[0];
                label.Text = dt.Columns[i].ColumnName;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Label label = (Label)this.Controls.Find("label" + (12 + j), true)[0];
                    label.Text += "\n" + row[j].ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT CodDepartament, Denumire FROM Departamente";

            SqlDataAdapter adapter = new SqlDataAdapter(query, co);
            System.Data.DataTable dt = new System.Data.DataTable();
            adapter.Fill(dt);

            for (int i = 0; i < 12; i++)
            {
                Label label = (Label)this.Controls.Find("label" + (12 + i), true)[0];
                label.Text = string.Empty;
            }

            for (int i = 0; i < 2; i++)
            {
                Label label = (Label)this.Controls.Find("label" + (12 + i), true)[0];
                label.Text = string.Empty;
            }

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Label label = (Label)this.Controls.Find("label" + (12 + i), true)[0];
                label.Text = dt.Columns[i].ColumnName;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Label label = (Label)this.Controls.Find("label" + (12 + j), true)[0];
                    label.Text += "\n" + row[j].ToString();
                }
            }
        }
    }
}
