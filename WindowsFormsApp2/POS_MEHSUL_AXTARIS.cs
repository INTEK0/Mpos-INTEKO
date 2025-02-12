using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class POS_MEHSUL_AXTARIS : DevExpress.XtraEditors.XtraForm
    {
        private DataTable dt;
        private SqlDataAdapter da;
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        private const String CATEGORIES_TABLE = "Categories";
        private const string barkod = "BARKOD";
        public POS_MEHSUL_AXTARIS()
        {
            InitializeComponent();
            GridLocalizer.Active = new MyGridLocalizer();
            GridPanelText(gridView1);
        }

        private void POS_MEHSUL_AXTARIS_Load(object sender, EventArgs e)
        {
            Auto();
            dt = new DataTable(CATEGORIES_TABLE);
            dt.Columns.Add(barkod, typeof(System.String));
        }

        public void getall(string a)
        {
            string paramValue = a;
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

                string queryString = "select * from dbo.mehsul_Adi_axtar (@pricePoint)";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", paramValue);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

                //foreach (DataRow row in dt.Rows)
                //{
                //    textBox2.Text = row["BARKOD"].ToString();
                //}
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DATALOAD_MESSAGE("Xəta!\n" + e.Message);
            }

        }


        public void Auto()
        {
            da = new SqlDataAdapter("select * from dbo.POS_autocomplete_search_mehsul_Adi_distinct()", Properties.Settings.Default.SqlCon);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    coll.Add(dt.Rows[i]["MEHSUL_ADI"].ToString());
                }
            }
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = coll;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                getall(textBox1.Text);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                getall_barkod(textBox2.Text);
            }
        }

        public void getall_barkod(string a)
        {
            string paramValue = a;
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

                string queryString = "select * from  dbo.mehsul_Adi_axtar_barkod (@pricePoint)";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", paramValue);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;

                //foreach (DataRow row in dt.Rows)
                //{
                //    textBox1.Text = row["MƏHSUL ADI"].ToString();
                //}
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DATALOAD_MESSAGE("Xəta!\n" + e.Message);
            }

        }

    }
}