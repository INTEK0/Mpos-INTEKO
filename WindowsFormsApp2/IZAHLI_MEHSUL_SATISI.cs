using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class IZAHLI_MEHSUL_SATISI : DevExpress.XtraEditors.XtraForm
    {
        public IZAHLI_MEHSUL_SATISI()
        {
            InitializeComponent();
            GridPanelText(gridView1);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ExcelExport(gridControl1, "İzahlı məhsul satışı hesabatı");
        }

        private void IZAHLI_MEHSUL_SATISI_Load(object sender, EventArgs e)
        {
            ClinicModuleShow();
            DateTime dateTime = DateTime.Now;

            dateEdit3.Text = dateTime.ToShortDateString();
            dateEdit4.Text = dateTime.ToShortDateString();
            gridView1.OptionsView.ShowColumnHeaders = false;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            getall(Convert.ToDateTime(dateEdit3.Text), Convert.ToDateTime(dateEdit4.Text).AddDays(1));
        }

        public void getall(DateTime D1_, DateTime D2_)
        {
            try
            {
                string queryString = "SELECT * FROM  dbo.fn_IZAHLI_SATIS_HESABAT(@pricepoint,@pricepoint1) order by 1 asc";

                using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@pricepoint", D1_);
                    command.Parameters.AddWithValue("@pricepoint1", D2_);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridView1.OptionsView.ShowColumnHeaders = true;
                    gridControl1.DataSource = dt;
                    gridView1.BestFitColumns();
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }

        private void ClinicModuleShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("ClinicModule").ToString());
            if (control)
            {
                colCustomer.Visible = true;
                colDoctor.Visible = true;
            }
            else
            {
                colCustomer.Visible = false;
                colDoctor.Visible = false;
            }
        }
    }
}