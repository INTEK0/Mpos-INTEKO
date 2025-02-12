using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class ANBAR_MENFEET : DevExpress.XtraEditors.XtraForm
    {
        public ANBAR_MENFEET()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void ANBAR_MENFEET_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();
            getall();
        }

        public void getall()
        {
            gridControl1.DataSource = DbProsedures.ConvertToDataTable("EXEC ANBAR_MENFEET");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GetallData_id_(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
        }

        public void GetallData_id_(DateTime D1_, DateTime D2_)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                string queryString = "EXEC [dbo].[ANBAR_MENFEET]  @d1 = @pricepoint  ,@d2=@pricepoint1 ";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricepoint", D1_);
                command.Parameters.AddWithValue("@pricepoint1", D2_);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Anbar mənfəət hesabatı");
        }
    }
}