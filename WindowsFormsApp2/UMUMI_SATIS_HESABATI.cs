using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class UMUMI_SATIS_HESABATI : DevExpress.XtraEditors.XtraForm
    {
        public UMUMI_SATIS_HESABATI()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            getall(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
        }

        public void getall(DateTime D1_, DateTime D2_)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                string queryString =
                  "SELECT * FROM  [dbo].[fn_GAIME_UMUMI_HESABAT] (@pricepoint,@pricepoint1)";



                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricepoint", D1_);
                command.Parameters.AddWithValue("@pricepoint1", D2_);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                //gridView1.Columns[0].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Xəta!\n" + e);
            }
        }

        private void UMUMI_SATIS_HESABATI_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ExcelExport(gridControl1, "Ümumi satış hesabatı");
        }
    }
}