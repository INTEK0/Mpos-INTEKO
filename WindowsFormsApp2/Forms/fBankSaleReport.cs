using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Forms
{
    public partial class fBankSaleReport : DevExpress.XtraEditors.XtraForm
    {
        public fBankSaleReport()
        {
            InitializeComponent();
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            var start = dateStart.DateTime;
            var end = dateEnd.DateTime;
            DataLoad(start, end.AddDays(1));
        }

        private void bPrint_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Qaimə satış hesabatı");
        }

        private void DataLoad(DateTime start,DateTime end)
        {
            const string query = @"SELECT * FROM fn_BankSaleReport(@start,@end)";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query,con))
            {
                cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = start;
                cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = end;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                using (DataTable dt = new DataTable())
                {
                    da.Fill(dt);
                    gridView1.OptionsView.ShowColumnHeaders = true;
                    gridControl1.DataSource = dt;
                    gridView1.BestFitColumns();
                }
            }
        }

        private void fBankSaleReport_Shown(object sender, EventArgs e)
        {
            var currentDate = DateTime.Now;
            dateStart.DateTime = currentDate;
            dateEnd.DateTime = currentDate;
        }
    }
}