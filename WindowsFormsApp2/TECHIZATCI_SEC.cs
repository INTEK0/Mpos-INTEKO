using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class TECHIZATCI_SEC : DevExpress.XtraEditors.XtraForm
    {
        private readonly GAIME_SATISI_LAYOUT frm1;
        private static string techizatci_adi;
        private static string mehsul_adi;
        private static string satis_giymeti;
        private static int mal_det_id;
        private static string anbar_g;
        private static string edv_;

        public TECHIZATCI_SEC(GAIME_SATISI_LAYOUT frm_)
        {
            InitializeComponent();
            frm1 = frm_;
            GridPanelText(gridView1);
        }

        public async Task GetAllData()
        {
            string query = "SELECT * FROM [VW_WAREHOUSE_STOCK]";

            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandTimeout = 120;
                await con.OpenAsync();
                Cursor.Current = Cursors.WaitCursor;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    gridControl1.DataSource = dt;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            frm1.techizatci_axtar(techizatci_adi, mehsul_adi, satis_giymeti, mal_det_id, anbar_g, edv_);

            // frm1.lookUpEdit8GEtData_yeni(mal_det_id);

            this.Close();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                techizatci_adi = dr["SupplierName"].ToString();
                mehsul_adi = dr["ProductName"].ToString();
                satis_giymeti = dr["SalePrice"].ToString();
                mal_det_id = Convert.ToInt32(dr["MAL_ALISI_DETAILS_ID"].ToString());
                anbar_g = dr["StockQuantity"].ToString();
                edv_ = dr["TaxName"].ToString();
            }
        }

        private async void TECHIZATCI_SEC_Shown(object sender, EventArgs e)
        {
            try
            {
                await GetAllData();
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
            }
        }
    }
}