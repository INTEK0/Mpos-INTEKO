using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraGrid.Localization;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fQuickProductList : DevExpress.XtraEditors.XtraForm
    {
        public List<string> _barcodes = new List<string>();
        public fQuickProductList()
        {
            InitializeComponent();
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void GetDataLoad()
        {
            Cursor.Current = Cursors.WaitCursor;
            gridControl1.DataSource = null;
            string query = @"
-- 1. Məhsul məlumatları
WITH ProductDetails AS (
    SELECT 
        t.TECHIZATCI_ID,
        t.SIRKET_ADI,
        md.MAL_ALISI_DETAILS_ID,
        md.MEHSUL_ADI,
        md.MEHSUL_KODU,
        md.ALIS_GIYMETI,
        md.SATIS_GIYMETI,
        md.BARKOD,
        ROW_NUMBER() OVER (
            PARTITION BY t.TECHIZATCI_ID, md.MEHSUL_ADI, md.MEHSUL_KODU 
            ORDER BY md.MAL_ALISI_DETAILS_ID DESC
        ) AS rn
    FROM MAL_ALISI_DETAILS md
    INNER JOIN MAL_ALISI_MAIN m ON md.MAL_ALISI_MAIN_ID = m.MAL_ALISI_MAIN_ID
    INNER JOIN COMPANY.TECHIZATCI t ON m.TECHIZATCI_ID = t.TECHIZATCI_ID
    WHERE t.IsDeleted = 0
),

-- 2. Stok Hesablanması
StockAmount AS (
    SELECT 
        a_m.mal_details_id,
        ISNULL(a_m.migdar_, 0) 
        - ISNULL(gs.MIGDARI, 0)
        + ISNULL(gsg.gaime_gaytarma_migdar, 0)
        - ISNULL(pos_s.count_, 0)
        + ISNULL(pos_g.say, 0)
        - ISNULL(m_g_det.m_g_mig, 0) AS migdar_
    FROM (
        SELECT mal_details_id, SUM(migdar) AS migdar_
        FROM ANBAR_MAGAZA
        GROUP BY mal_details_id
    ) a_m

    LEFT JOIN (
        SELECT MAL_DETAILS_ID, SUM(CAST(REPLACE(MIGDARI, ',', '.') AS DECIMAL(9,2))) AS MIGDARI
        FROM GAIME_SATISI_DETAILS
        GROUP BY MAL_DETAILS_ID
    ) gs ON gs.MAL_DETAILS_ID = a_m.mal_details_id

    LEFT JOIN (
        SELECT gtd.MAL_DETAILS_ID, SUM(fgr.migdar) AS gaime_gaytarma_migdar
        FROM gaime_satis_gaytarma fgr
        INNER JOIN GAIME_SATISI_DETAILS gtd ON gtd.GAIME_SATISI_DETAILS_ID = fgr.gaime_satis_details_id
        GROUP BY gtd.MAL_DETAILS_ID
    ) gsg ON gsg.MAL_DETAILS_ID = a_m.mal_details_id

    LEFT JOIN (
        SELECT pd.mal_alisi_details_id, SUM(pd.count_) AS count_
        FROM pos_satis_check_details pd
        GROUP BY pd.mal_alisi_details_id
    ) pos_s ON pos_s.mal_alisi_details_id = a_m.mal_details_id

    LEFT JOIN (
        SELECT d.MAL_ALISI_DETAILS_ID, SUM(d.MIGDARI) AS m_g_mig
        FROM MAL_GEYTARMA_DETAILS d
        GROUP BY d.MAL_ALISI_DETAILS_ID
    ) m_g_det ON m_g_det.MAL_ALISI_DETAILS_ID = a_m.mal_details_id

    LEFT JOIN (
        SELECT psd.mal_alisi_details_id, SUM(pgm.say) AS say
        FROM pos_gaytarma_manual pgm
        INNER JOIN pos_satis_check_details psd 
            ON pgm.pos_satis_check_details = psd.pos_satis_check_details_id
        GROUP BY psd.mal_alisi_details_id
    ) pos_g ON pos_g.mal_alisi_details_id = a_m.mal_details_id
)

-- 3. Nəticə
SELECT 
    pd.SIRKET_ADI AS SupplierName,
    pd.MEHSUL_ADI AS ProductName,
    pd.BARKOD AS Barcode,
    pd.ALIS_GIYMETI AS PurchasePrice,
    pd.SATIS_GIYMETI AS SalePrice,
    sa.migdar_ AS Amount
FROM ProductDetails pd
INNER JOIN StockAmount sa ON sa.mal_details_id = pd.MAL_ALISI_DETAILS_ID
WHERE pd.rn = 1;

";

            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
            Cursor.Current = Cursors.Default;
        }

        private void fQuickProductList_Load(object sender, EventArgs e)
        {
            GetDataLoad();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                int[] selectedRows = gridView1.GetSelectedRows();
                foreach (int item in selectedRows)
                {
                    var row = gridView1.GetDataRow(item);
                    string barcode = row["Barcode"].ToString();
                    _barcodes.Add(barcode);
                }
                if (_barcodes.Count > 0)
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            OpenForm<fAddProduct>();
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            GetDataLoad();
        }
    }
}