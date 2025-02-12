using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
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
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ExcelExport(gridControl1, "İzahlı məhsul qaytarma hesabatı");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            getall(Convert.ToDateTime(dateEdit3.Text), Convert.ToDateTime(dateEdit4.Text));
        }
        public void getall(DateTime D1_, DateTime D2_)
        {
            string queryString = "SELECT * FROM  dbo.IZAHLI_GAYTARMA_HESABAT( @pricepoint,@pricepoint1) order by 1 ASC";
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricepoint", D1_);
                command.Parameters.AddWithValue("@pricepoint1", D2_);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

                gridView1.Columns["TARİX"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["TARİX"].DisplayFormat.FormatString = "dd-MM-yyyy HH:mm:ss";
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }
        private void izahli_mehsul_gaytarma_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit3.Text = dateTime.ToShortDateString();
            dateEdit4.Text = dateTime.ToShortDateString();
        }
    }
}