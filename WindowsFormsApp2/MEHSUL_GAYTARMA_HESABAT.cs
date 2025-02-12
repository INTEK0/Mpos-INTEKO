using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class MEHSUL_GAYTARMA_HESABAT : DevExpress.XtraEditors.XtraForm
    {
        public MEHSUL_GAYTARMA_HESABAT()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Məhsul qaytarma hesabatı");
        }

        private void MEHSUL_GAYTARMA_HESABAT_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();

            lookupedittextxhange_main();
        }

        private void lookupedittextxhange_main()
        {
            string query = "select TECHIZATCI_ID,SIRKET_ADI AS N'TƏCHİZATÇI ADI' from COMPANY.TECHIZATCI WHERE IsDeleted = 0";
            var data = DbProsedures.ConvertToDataTable(query);
            lookUpEdit1.Properties.DisplayMember = "TƏCHİZATÇI ADI";
            lookUpEdit1.Properties.ValueMember = "TECHIZATCI_ID";
            lookUpEdit1.Properties.DataSource = data;
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns[0].Visible = false;
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            try
            {
                if (string.IsNullOrWhiteSpace(lookUpEdit1.Text))
                {
                    GetallData(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
                }
                else
                {
                    GetallData_t_id(Convert.ToDateTime(dateEdit1.Text),
                                   Convert.ToDateTime(dateEdit2.Text),
                                   Convert.ToInt32(lookUpEdit1.EditValue.ToString())
                                   );
                }
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE("Xəta!\n" + ex.Message);
            }
        }

        public void GetallData_t_id(DateTime D1_, DateTime D2_, int _t_id)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            string queryString = "SELECT * FROM dbo.GAYTARMA_HESABAT_t_id (cast(@pricePoint AS DATE) , CAST(@pricePoint1 AS DATE),@pricePoint2)  ";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@pricepoint", D1_);
            command.Parameters.AddWithValue("@pricepoint1", D2_);
            command.Parameters.AddWithValue("@pricePoint2", _t_id);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        public void GetallData(DateTime D1_, DateTime D2_)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            string queryString = "SELECT * FROM dbo.GAYTARMA_HESABAT (cast(@pricePoint AS DATE) , CAST(@pricePoint1 AS DATE))  ";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@pricepoint", D1_);
            command.Parameters.AddWithValue("@pricepoint1", D2_);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
    }
}