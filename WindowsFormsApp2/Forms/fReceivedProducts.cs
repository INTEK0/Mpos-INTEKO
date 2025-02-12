using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fReceivedProducts : DevExpress.XtraEditors.XtraForm
    {
        public fReceivedProducts()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void fReceivedProducts_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();

            DataLoad(dateEdit1.DateTime,dateEdit2.DateTime);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Alınan məhsullar");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataLoad(dateEdit1.DateTime, dateEdit2.DateTime);
        }

        private void DataLoad(DateTime start, DateTime finish)
        {
            string query = "SELECT * FROM dbo.ALINAN_MEHSUL(@pricePoint,@pricePoint1)";
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query,connection))
                {
                    cmd.Parameters.AddWithValue("@pricePoint", start);
                    cmd.Parameters.AddWithValue("@pricePoint1", finish);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            gridControl1.DataSource = dt;
                        }
                    }
                }
            }
        }
    }
}