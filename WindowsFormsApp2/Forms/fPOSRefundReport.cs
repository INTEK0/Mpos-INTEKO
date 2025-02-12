using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
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
using WindowsFormsApp2.Helpers;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fPOSRefundReport : DevExpress.XtraEditors.XtraForm
    {
        public fPOSRefundReport()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void fPOSRefundReport_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();
            DataLoad(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataLoad(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "POS Qaytarma tarixçəsi");
        }

        private void DataLoad(DateTime start, DateTime finish)
        {
            string query = "select * from [dbo].[fn_POS_GAYTARMA] ()  WHERE CAST([TARİX] AS smalldatetime) BETWEEN  @pricePoint and @pricePoint1";
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pricePoint", Convert.ToDateTime(start));
                    cmd.Parameters.AddWithValue("@pricePoint1", Convert.ToDateTime(finish));
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