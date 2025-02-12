using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class techizatci_odenisleri_hesabar : DevExpress.XtraEditors.XtraForm
    {
        public techizatci_odenisleri_hesabar()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void techizatci_odenisleri_hesabar_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ExcelExport(gridControl1, "Təchizatçı ödənişləri hesabatı");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dateEdit2.Text) || string.IsNullOrEmpty(dateEdit1.Text))
            {
                Alert("TARİX ARALIĞI SEÇİLMƏYİB", Helpers.Enums.MessageType.Warning);
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
                using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    string queryString = "select * from dbo.TECHIZATCI_borclu_all_HESABAT (@pricePoint,@pricePoint1) ";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@pricePoint", d1);
                        command.Parameters.AddWithValue("@pricePoint1", d2);
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                gridControl1.DataSource = dt;
                                gridView1.Columns[0].Visible = false;
                            }
                        }
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