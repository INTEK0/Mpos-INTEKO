using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class izahli_mehsul_gaytarma : DevExpress.XtraEditors.XtraForm
    {
        public izahli_mehsul_gaytarma()
        {
            InitializeComponent();
            GridPanelText(gridView1);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ExcelExport(gridControl1, "İzahlı məhsul qaytarma hesabatı");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            getall(Convert.ToDateTime(dateEdit3.Text), Convert.ToDateTime(dateEdit4.Text).AddDays(1));
        }
        public void getall(DateTime start, DateTime end)
        {
            string queryString = "SELECT * FROM  dbo.IZAHLI_GAYTARMA_HESABAT( @pricepoint,@pricepoint1) order by 1 ASC";
            try
            {
                using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
                using (SqlCommand cmd = new SqlCommand(queryString,con))
                {
                    cmd.Parameters.AddWithValue("@pricepoint", start);
                    cmd.Parameters.AddWithValue("@pricepoint1", end);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);
                        gridControl1.DataSource = dt;
                        gridView1.BestFitColumns();
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }
        private void izahli_mehsul_gaytarma_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            dateEdit3.Text = dateTime.ToShortDateString();
            dateEdit4.Text = dateTime.ToShortDateString();
        }
    }
}