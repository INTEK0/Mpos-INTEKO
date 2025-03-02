using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp2.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class ANBAR_GALIGI : BaseForm
    {
        private ProductDetail _productDetail;
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

        private void getall( DateTime D2_)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    string queryString = "gaime_Satis_mal_load_tarixle  @d1 = @pricepoint1 ";
                    using (SqlCommand cmd = new SqlCommand(queryString, con))
                    {
                        cmd.Parameters.AddWithValue("@pricepoint1", D2_);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                gridControl1.DataSource = dt;
                                gridView1.Columns["TECHIZATCI_ID"].Visible = false;
                                gridView1.Columns["MAL_ALISI_DETAILS_ID"].Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE("Xəta!\n" + e);
            }
        }

        private void ANBAR_GALIGI_Load(object sender, EventArgs e)
        {
            dateEdit4.DateTime = DateTime.Now;
            //dateEdit3.Text = dateTime.ToShortDateString();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            string barcode = gridView1.GetFocusedRowCellValue("BARKOD").ToString();

            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                string query = $"EXEC SELECT_PRODUCT_DATA_LOAD '{barcode}'";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ProductDetail _detail = FormHelpers.MapReaderToObject<DatabaseClasses.ProductDetail>(reader);
                            
                            fProductDetail detail = new fProductDetail(_detail);
                            detail.ShowDialog();
                        }
                    }
                }
            }
        }
    }
}