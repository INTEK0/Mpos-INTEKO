using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;

namespace WindowsFormsApp2.Forms
{
    public partial class fSaleProfitReport : DevExpress.XtraEditors.XtraForm
    {
        public fSaleProfitReport()
        {
            InitializeComponent();
        }

        private void fSaleProfitReport_Load(object sender, EventArgs e)
        {
            dateEdit1.Text = DateTime.Now.ToShortDateString();
            dateEdit2.Text = DateTime.Now.ToShortDateString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GetReport(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text).AddDays(1));
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Satış üzrə mənfəət hesabatı");
        }

        private void GetReport(DateTime start, DateTime end)
        {
            try
            {
                string query = "EXEC [dbo].[ANBAR_MENFEET]  @d1=@start, @d2=@end";
                using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
                using (SqlCommand cmd = new SqlCommand(query,con))
                {
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);
                        gridControl1.DataSource = dt;
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }
    }
}