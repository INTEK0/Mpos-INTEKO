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
        //public List<ProductInfo> productInfo = new List<ProductInfo>();
        public fQuickProductList()
        {
            InitializeComponent();
            GridLocalizer.Active = new MyGridLocalizer();
        }

        //public class ProductInfo
        //{
        //    public string barcode { get; set; }
        //    public decimal stockAmount { get; set; } = 0;
        //}

        private void GetDataLoad()
        {
            Cursor.Current = Cursors.WaitCursor;
            gridControl1.DataSource = null;
            string query = @"DECLARE @Result TABLE (
TECHIZATCI_ID int,
TECHIZATCI NVARCHAR(100),
MAL_ALIS_DETAILS_ID int,
PRODUCTNAME NVARCHAR(MAX),
PRODUCTCODE NVARCHAR(100),
PURCHASEPRICE decimal(18, 3),
SALEPRICE decimal(18, 3),
STOCK decimal(9,2),
BARCODE NVARCHAR(100),
EDV NVARCHAR(50));
INSERT INTO @Result
EXEC dbo.gaime_Satis_mal_load;

SELECT 
MAL_ALIS_DETAILS_ID,
TECHIZATCI AS SupplierName,
PRODUCTNAME AS ProductName,
BARCODE AS Barcode,
PURCHASEPRICE AS PurchasePrice,
SALEPRICE AS SalePrice,
STOCK AS Amount
FROM @Result";

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
                    //decimal amount = Decimal.Parse(row["Amount"].ToString());
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