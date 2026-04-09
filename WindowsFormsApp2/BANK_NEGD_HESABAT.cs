using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class BANK_NEGD_HESABAT : DevExpress.XtraEditors.XtraForm
    {
        public BANK_NEGD_HESABAT()
        {
            InitializeComponent();
            GridPanelText(gridView1);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ExcelExport(gridControl1,"Satış növ hesabatı");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dateEdit1.Text) || string.IsNullOrEmpty(dateEdit2.Text))
                XtraMessageBox.Show("TARİX ARALIĞI SEÇİLMƏYİB");
            else
                getall(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
        }

        public void getall(DateTime D1_, DateTime D2_)
        {
            try
            {
                string queryString = " SELECT * FROM  dbo.fn_GAIME__NEGD_KART_HESABAT(@pricepoint,@pricepoint1)";
                using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
                using (SqlCommand cmd = new SqlCommand(queryString,con))
                {
                    cmd.Parameters.AddWithValue("@pricepoint", D1_);
                    cmd.Parameters.AddWithValue("@pricepoint1", D2_);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                }
            }
            catch (Exception e)
            {
               ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }

        private void BANK_NEGD_HESABAT_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();
        }
    }
}