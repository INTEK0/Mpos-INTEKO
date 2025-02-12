using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class BANK_NEGD_HESABAT : DevExpress.XtraEditors.XtraForm
    {
        public BANK_NEGD_HESABAT()
        {
            InitializeComponent();
            GridLocalizer.Active = new MyGridLocalizer();
            GridPanelText(gridView1);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ExcelExport(gridControl1,"Satış növ hesabatı");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dateEdit1.Text) || string.IsNullOrEmpty(dateEdit2.Text))
            {
                XtraMessageBox.Show("TARİX ARALIĞI SEÇİLMƏYİB");
            }
            {
                getall(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
            }
        }

        public void getall(DateTime D1_, DateTime D2_)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                // Provide the query string with a parameter placeholder.
                string queryString =
                  " SELECT * FROM  dbo.fn_GAIME__NEGD_KART_HESABAT( @pricepoint,@pricepoint1)";



                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricepoint", D1_);
                command.Parameters.AddWithValue("@pricepoint1", D2_);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                //gridView1.Columns[0].Visible = false;

                //gridView1.OptionsSelection.MultiSelect = true;
                //gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
               
            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }
        }

        private void BANK_NEGD_HESABAT_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();
        }
    }
}