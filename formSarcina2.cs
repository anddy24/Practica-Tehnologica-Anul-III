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
            string deletesql = "DELETE FROM Angajati WHERE CodAngajat = '" + textBox1.Text + "'";

            using (SqlCommand cmd = new SqlCommand(deletesql, co))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
