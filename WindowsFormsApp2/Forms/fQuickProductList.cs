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
            string query = @"WITH RankedData AS (
    SELECT 
        m.MAL_ALISI_DETAILS_ID AS Id,
        t.SIRKET_ADI AS SupplierName,
        m.MEHSUL_ADI AS ProductName,
        m.BARKOD AS Barcode,
        m.ALIS_GIYMETI AS PurchasePrice,
        m.SATIS_GIYMETI AS SalePrice,
        v.VAHIDLER_NAME AS UnitName,
		SUM(a.migdar) OVER(PARTITION BY m.BARKOD, m.MEHSUL_ADI) AS Amount,
        ROW_NUMBER() OVER(PARTITION BY m.BARKOD, m.MEHSUL_ADI ORDER BY m.MAL_ALISI_DETAILS_ID DESC) AS RowNum
    FROM MAL_ALISI_DETAILS m
    INNER JOIN VAHIDLER v ON v.VAHIDLER_ID = m.VAHID
    INNER JOIN VERGI_DERECESI vd ON vd.EDV_ID = m.VERGI_DERECESI
    INNER JOIN MAL_ALISI_MAIN ma ON ma.MAL_ALISI_MAIN_ID = m.MAL_ALISI_MAIN_ID
    INNER JOIN COMPANY.TECHIZATCI t ON t.TECHIZATCI_ID = ma.TECHIZATCI_ID
	INNER JOIN ANBAR_MAGAZA a ON a.mal_details_id = m.MAL_ALISI_DETAILS_ID
    WHERE m.IsDeleted = 0
)
SELECT * 
FROM RankedData 
WHERE RowNum = 1 -- Eyni barkod və məhsul adı olanlardan sadəcə ən böyük ID'li olanını al
ORDER BY SupplierName, Id DESC;
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