using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class MUSTERI_ODENIS_HESABAT : DevExpress.XtraEditors.XtraForm
    {
        public MUSTERI_ODENIS_HESABAT()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ExcelExport(gridControl1, "Müştəri ödənişləri hesabatı");
        }

        private void MUSTERI_ODENIS_HESABAT_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dateEdit2.Text) || string.IsNullOrEmpty(dateEdit1.Text))
            {
                XtraMessageBox.Show("TARİX ARALIĞI SEÇİLMƏYİB");
            }
            else
            {
                LOAD(Convert.ToDateTime(dateEdit2.Text.ToString()), Convert.ToDateTime(dateEdit1.Text));
            }
        }

        private void LOAD(DateTime d1, DateTime d2)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

                // Provide the query string with a parameter placeholder.
                string queryString = "select * from [dbo].[musteri_borclu_all_HESABAT] (@pricePoint,@pricePoint1) ";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", d1);
                command.Parameters.AddWithValue("@pricePoint1", d2);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                  gridView1.Columns[0].Visible = false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }
        }
    }
}