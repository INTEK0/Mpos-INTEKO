using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class ANBAR_GALIGI : DevExpress.XtraEditors.XtraForm
    {
        public ANBAR_GALIGI()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Anbar Qalığı");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dateEdit4.Text))
            {
                XtraMessageBox.Show("TARİX ARALIĞI SEÇİLMƏYİB");
            }
            else
            {
                getall( Convert.ToDateTime(dateEdit4.Text));
            }
        }

        public void getall( DateTime D2_)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

                string queryString = "gaime_Satis_mal_load_tarixle  @d1 = @pricepoint1 ";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricepoint1", D2_);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

                gridView1.Columns["TECHIZATCI_ID"].Visible = false;
                gridView1.Columns["MAL_ALISI_DETAILS_ID"].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Xəta!\n" + e);
            }
        }

        private void ANBAR_GALIGI_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            dateEdit4.Text = dateTime.ToShortDateString();
            //dateEdit3.Text = dateTime.ToShortDateString();
        }
    }
}