using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class MEHSUL_ALIS_HESABATI : DevExpress.XtraEditors.XtraForm
    {
        public MEHSUL_ALIS_HESABATI()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void MEHSUL_ALIS_HESABATI_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();

            lookupedittextxhange_main();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lookUpEdit1.Text))
                {
                    GetallData_id_(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));

                }
                else
                {
                    GetallData_tech_id(Convert.ToDateTime(dateEdit1.Text),
                                                      Convert.ToDateTime(dateEdit2.Text),
                                                      Convert.ToInt32(lookUpEdit1.EditValue.ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xəta!\n" + ex);
            }
        }

        public void GetallData_tech_id(DateTime D1_, DateTime D2_, int te_id)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            string queryString = "EXEC dbo.MEHSUL_ALIS_HESABAT_t_id  @d1 = @pricepoint  ,@d2=@pricepoint1 ,@t_id =@pricepoint2  ";


            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@pricepoint", D1_);
            command.Parameters.AddWithValue("@pricepoint1", D2_);
            command.Parameters.AddWithValue("@pricepoint2", te_id);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        public void GetallData_id_(DateTime D1_, DateTime D2_)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            string queryString = "EXEC MEHSUL_ALIS_HESABAT  @d1 = @pricepoint  ,@d2=@pricepoint1 ";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@pricepoint", D1_);
            command.Parameters.AddWithValue("@pricepoint1", D2_);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Məhsul alış Hesabatı");
        }


        private void lookupedittextxhange_main()
        {
            string query = "select TECHIZATCI_ID,SIRKET_ADI AS N'TƏCHİZATÇI ADI' from COMPANY.TECHIZATCI";
            var data = DbProsedures.ConvertToDataTable(query);
            lookUpEdit1.Properties.DisplayMember = "TƏCHİZATÇI ADI";
            lookUpEdit1.Properties.ValueMember = "TECHIZATCI_ID";
            lookUpEdit1.Properties.DataSource = data;
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns[0].Visible = false;
        }
    }
}