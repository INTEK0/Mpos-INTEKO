using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Forms
{
    public partial class fLogs : DevExpress.XtraEditors.XtraForm
    {
        public fLogs()
        {
            InitializeComponent();
            FormHelpers.GridPanelText(gridLogs);
        }

        private void fLogs_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            dateEdit3.Text = dateTime.ToShortDateString();
            dateEdit4.Text = dateTime.ToShortDateString();
            gridLogs.Focus();
            gridControlLogs.Focus();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControlLogs, "Arxiv");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            LogReport(dateEdit3.DateTime, dateEdit4.DateTime);
        }

        private void LogReport(DateTime start, DateTime end)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("LogReport", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", start);
                cmd.Parameters.AddWithValue("@EndDate", end);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                using (DataTable dataTable = new DataTable())
                {
                    da.Fill(dataTable);
                    gridControlLogs.DataSource = dataTable;
                }
            }
        }
    }
}