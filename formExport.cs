using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace PracticaTehnologica
{
    public partial class formExport : Form
    {
        SqlConnection co;

        public formExport()
        {
            InitializeComponent();
            co = new SqlConnection(@"Data Source =DESKTOP-9LE00L5; Database =GestiuneAngajati; Trusted_Connection=yes;");
            co.Open();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                int selectedMonth = Convert.ToInt32(comboBox2.SelectedItem);
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string excelFilePath = Path.Combine(documentsPath, $"EmployeesBornInMonth{selectedMonth}.xlsx");

                ExportEmployeesBornInMonth(selectedMonth, excelFilePath);
            }
            else
            {
                MessageBox.Show("Please select a month from the ComboBox.");
            }
        }

        private void ExportEmployeesBornInMonth(int month, string excelFilePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            try
            {
                string query = "SELECT CodAngajat, Nume, Prenume, DataNasterii, Sex, CodProfesie, Salariu, StagiuMunca " +
                               "FROM Angajati " +
                               "WHERE MONTH(DataNasterii) = @Month";

                using (SqlCommand cmd = new SqlCommand(query, co))
                {
                    cmd.Parameters.AddWithValue("@Month", month);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        using (ExcelPackage pck = new ExcelPackage())
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Employees");
                            ws.Cells["A1"].LoadFromDataTable(dt, true);
                            FileInfo fi = new FileInfo(excelFilePath);
                            pck.SaveAs(fi);
                        }
                    }
                }
                MessageBox.Show("Data exported successfully to " + excelFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
