using System;
using System.Data;
using System.Threading.Tasks;
using WindowsFormsApp2.Helpers.CacheData;
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

        private async void TECHIZATCI_SEC_Load(object sender, EventArgs e)
        {
            await StockProductsList();
        }

        public async Task StockProductsList()
        {
            var data = await StockCacheService.LoadStockAsync();
            gridControl1.DataSource = data;
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
                techizatci_adi = dr["TƏCHİZATÇI"].ToString();
                mehsul_adi = dr["MƏHSUL ADI"].ToString();
                satis_giymeti = dr["SATIŞ QİYMƏTİ"].ToString();
                mal_det_id = Convert.ToInt32(dr["MAL_ALISI_DETAILS_ID"].ToString());
                anbar_g = dr["ANBAR QALIĞI"].ToString();
                edv_ = dr["EDV"].ToString();
            }
        }
    }
}