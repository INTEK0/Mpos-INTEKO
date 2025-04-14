using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;

namespace WindowsFormsApp2.Forms
{
    public partial class fQuickAddProduct : DevExpress.XtraEditors.XtraForm
    {
        private BindingList<Product> _products = new BindingList<Product>();
        private BindingList<TestProduct> _TestProduct = new BindingList<TestProduct>();
        private int _rowCount = 1;
        private readonly string _barcode;
        private decimal _stockAmount;
        public fQuickAddProduct(string barcode = null)
        {
            InitializeComponent();
            _barcode = barcode;
        }

        private class Product
        {
            public int No { get; set; }
            public int Id { get; set; }
            public string SupplierName { get; set; }
            public string CategoryName { get; set; }
            public string ProductName { get; set; }
            public string Barcode { get; set; }
            public decimal StockAmount { get; set; }
            public decimal Amount { get; set; }
            public string UnitName { get; set; }
            public string TaxName { get; set; }
            public decimal PurchasePrice { get; set; }
            public decimal TotalPurchaseAmount { get => Amount * PurchasePrice; }
            private decimal _percent;
            public decimal Percent
            {
                get => _percent;
                set
                {
                    _percent = value;
                    SalePrice = PurchasePrice * (1 + (_percent / 100));
                }
            }
            public decimal SalePrice { get; set; }
            public decimal TotalSaleAmount { get => Amount * SalePrice; }
            public decimal GainAmount { get => TotalSaleAmount - TotalPurchaseAmount; }
        }

        private class TestProduct
        {
            public int Id { get; set; }
            public string SupplierName { get; set; }
            public string CategoryName { get; set; }
            public string ProductName { get; set; }
            public string Barcode { get; set; }
            public decimal PurchasePrice { get; set; }
            public decimal SalePrice { get; set; }
            public string UnitName { get; set; }
            public string TaxName { get; set; }
            public decimal Amount { get; set; }
        }

        private void StockListDataLoad()
        {
            string query = @"
DECLARE @Result TABLE (
TECHIZATCI_ID int,
TECHIZATCI NVARCHAR(100),
MAL_ALIS_DETAILS_ID int,
PRODUCTNAME NVARCHAR(500),
PRODUCTCODE NVARCHAR(100),
PURCHASEPRICE decimal(18, 3),
SALEPRICE decimal(18, 3),
STOCK decimal(9,2),
BARCODE NVARCHAR(100),
EDV NVARCHAR(50));
INSERT INTO @Result
EXEC dbo.gaime_Satis_mal_load;

SELECT 
MAL_ALIS_DETAILS_ID AS Id,
TECHIZATCI AS SupplierName,
k.KATEGORIYA AS CategoryName, 
PRODUCTNAME AS ProductName,
BARCODE AS Barcode,
v.VAHIDLER_NAME AS UnitName,
PURCHASEPRICE AS PurchasePrice,
SALEPRICE AS SalePrice,
vd.EDV AS TaxName,
STOCK AS Amount
FROM @Result r 
INNER JOIN MAL_ALISI_DETAILS mad ON mad.MAL_ALISI_DETAILS_ID = r.MAL_ALIS_DETAILS_ID
INNER JOIN KATEGORIYA k ON k.KATEGORIYA_ID = mad.KATEGORIYA
INNER JOIN VAHIDLER v ON v.VAHIDLER_ID = mad.VAHID
INNER JOIN VERGI_DERECESI vd ON vd.EDV_ID = mad.VERGI_DERECESI
";
            var data = DbProsedures.ConvertToDataTable(query);

            foreach (DataRow item in data.Rows)
            {
                TestProduct product = new TestProduct();
                product.Id = Convert.ToInt32(item["Id"].ToString());
                product.SupplierName = item["SupplierName"].ToString();
                product.ProductName = item["ProductName"].ToString();
                product.CategoryName = item["CategoryName"].ToString();
                product.Barcode = item["Barcode"].ToString();
                product.TaxName = item["TaxName"].ToString();
                product.UnitName = item["UnitName"].ToString();
                product.PurchasePrice = Convert.ToDecimal(item["PurchasePrice"].ToString());
                product.SalePrice = Convert.ToDecimal(item["SalePrice"].ToString());
                product.Amount = Convert.ToDecimal(item["Amount"].ToString());
                _TestProduct.Add(product);
            }

        }

        private void fQuickAddProduct_Load(object sender, EventArgs e)
        {
            StockListDataLoad();

            dateTarix.DateTime = DateTime.Now;
            gridControl1.DataSource = _products;
            if (!string.IsNullOrWhiteSpace(_barcode))
            {
                tBarcode.Text = _barcode;
                tBarcode_KeyDown(tBarcode, new KeyEventArgs(Keys.Enter));
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            tBarcode.Focus();
        }

        private void tBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;

                var data = _TestProduct.FirstOrDefault(x => x.Barcode == tBarcode.Text.Trim());

                if (data != null)
                {
                    var existingProduct = _products.FirstOrDefault(X=> X.Barcode == data.Barcode);
                    if (existingProduct!= null)
                    {
                        existingProduct.Amount += 1;
                    }
                    else
                    {
                        Product product = new Product
                        {
                            No = _rowCount,
                            Id = data.Id,
                            SupplierName = data.SupplierName,
                            CategoryName = data.CategoryName,
                            ProductName = data.ProductName,
                            Barcode = data.Barcode,
                            StockAmount = data.Amount,
                            Amount = 1,
                            UnitName = data.UnitName,
                            TaxName = data.TaxName,
                            PurchasePrice = data.PurchasePrice,
                            SalePrice = data.SalePrice
                        };
                        _products.Add(product);
                        _rowCount++;
                    }

                }
                

#if BEFORE_CODE
                string query = $@"SELECT TOP 1 
m.MAL_ALISI_DETAILS_ID AS Id,
t.SIRKET_ADI AS SupplierName,
k.KATEGORIYA AS CategoryName,
m.MEHSUL_ADI AS ProductName,
m.BARKOD AS Barcode,
m.ALIS_GIYMETI AS PurchasePrice,
m.SATIS_GIYMETI AS SalePrice,
v.VAHIDLER_NAME AS UnitName,
vd.EDV AS TaxName,
a.migdar AS StockAmount
FROM MAL_ALISI_DETAILS m
INNER JOIN VAHIDLER v ON v.VAHIDLER_ID = m.VAHID
INNER JOIN VERGI_DERECESI vd ON vd.EDV_ID  = m.VERGI_DERECESI
INNER JOIN KATEGORIYA k ON k.KATEGORIYA_ID = m.KATEGORIYA
INNER JOIN MAL_ALISI_MAIN ma ON ma.MAL_ALISI_MAIN_ID = m.MAL_ALISI_MAIN_ID
INNER JOIN COMPANY.TECHIZATCI t ON t.TECHIZATCI_ID = ma.TECHIZATCI_ID
INNER JOIN ANBAR_MAGAZA a ON m.MAL_ALISI_DETAILS_ID = a.mal_details_id
WHERE BARKOD IN (SELECT BARKOD FROM MAL_ALISI_DETAILS WHERE BARKOD =  N'{tBarcode.Text}')
ORDER BY MAL_ALISI_DETAILS_ID DESC;";

                var data = DbProsedures.ConvertToDataTable(query);
                if (data.Rows.Count > 0)
                {
                    DataRow row = data.Rows[0];

                    int productId = Convert.ToInt32(row["Id"].ToString());
                    var existingProduct = _products.FirstOrDefault(x => x.Id == productId);
                    if (existingProduct != null)
                    {
                        existingProduct.Amount += 1;
                    }
                    else
                    {
                        Product product = new Product
                        {
                            No = _rowCount,
                            Id = productId,
                            SupplierName = row["SupplierName"].ToString(),
                            CategoryName = row["CategoryName"].ToString(),
                            ProductName = row["ProductName"].ToString(),
                            Barcode = row["Barcode"].ToString(),
                            StockAmount = Convert.ToDecimal(row["StockAmount"].ToString()),
                            Amount = 1,
                            UnitName = row["UnitName"].ToString(),
                            TaxName = row["TaxName"].ToString(),
                            PurchasePrice = Convert.ToDecimal(row["PurchasePrice"].ToString()),
                            SalePrice = Convert.ToDecimal(row["SalePrice"].ToString())
                        };
                        _products.Add(product);
                        _rowCount++;
                    }

                    //gridControl1.RefreshDataSource();
                    gridView1.RefreshData();
                }
#endif
                gridView1.RefreshData();
                tBarcode.Text = null;
                tBarcode.Focus();
            }
        }

        private void bDeleteRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int productId = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id").ToString());
            var product = _products.FirstOrDefault(x => x.Id == productId);

            _products.Remove(product);
            gridControl1.RefreshDataSource();
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {

                int[] selectedRows = gridView1.GetSelectedRows();
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                foreach (int item in selectedRows)
                {
                    Product row = gridView1.GetRow(item) as Product;
                    if (row != null)
                    {
                        _products.Remove(row);
                    }
                }
                gridControl1.DataSource = null;
                gridControl1.DataSource = _products;
            }
        }

        private void panelControl1_Click(object sender, EventArgs e)
        {
            tBarcode.Text = null;
            tBarcode.Focus();
        }

        private async void bAdd_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                gridView1.EndDataUpdate();
                var groupped = _products.GroupBy(x => x.SupplierName);
                foreach (var group in groupped)
                {
                    string proccessNo = DbProsedures.GET_ProductProcessNo();
                    int addMainProduct = DbProsedures.InsertProductMain(new ProductsMain
                    {
                        FakturaNo = tInvoiceNo.Text,
                        SupplierName = group.Key,
                        Date = dateTarix.DateTime,
                        PaymentType = "NAĞD",
                        ProccessNo = proccessNo,
                        Status = "MƏHSUL ALIŞI"
                    });
                    foreach (var item in group)
                    {
                        ProductsDetail product = new ProductsDetail();
                        product.ProductMainId = addMainProduct;
                        product.CategoryName = item.CategoryName;
                        product.Barocde = item.Barcode;
                        product.ProductName = item.ProductName;
                        product.ProductCode = item.Barcode;
                        product.WarehouseName = "- MƏRKƏZ ANBAR - (ƏSAS)";
                        product.Quantity = item.Amount;
                        product.UnitName = item.UnitName;
                        product.CurrencyName = "AZN";
                        product.TaxName = item.TaxName;
                        product.PurchasePrice = item.PurchasePrice;
                        product.SalePrice = item.SalePrice;
                        product.DiscountPercent = "0";
                        product.DiscountAZN = "0";
                        product.DiscountAmount = "0,000";
                        product.TotalAmount = item.TotalPurchaseAmount.ToString();
                        product.IstehsalTarixi = "";
                        product.BitisTarixi = "";
                        product.XeberdarEt = 0.ToString();
                        product.imageBytes = null;


                        int? IsSuccess = await DbProsedures.InsertProductDetails(product);
                        if (IsSuccess < 0 || IsSuccess is null)
                        {
                            ReadyMessages.ERROR_DEFAULT_MESSAGE($"{group.Key} təchizatçısına məxsus {item.ProductName} məhsulu sistemə əlavə edilərkən xəta yarandı");
                        }
                    }
                }
                Cursor.Current = Cursors.WaitCursor;
                Close();
            }
        }

        private void gridView1_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ErrorText = "Dəstəklənməyən simvol !";
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;
        }

        private void gridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gridView1.FocusedColumn.FieldName == "Amount")
            {
                if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode is Keys.Enter)
            {
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                tBarcode.Focus();
                e.Handled = true;
            }
        }

        private void bSelectedProducts_Click(object sender, EventArgs e)
        {
            fQuickProductList f = new fQuickProductList();
            if (f.ShowDialog() is DialogResult.OK)
            {
                foreach (var item in f._barcodes)
                {
                    tBarcode.Text = item;
                    tBarcode_KeyDown(tBarcode, new KeyEventArgs(Keys.Enter));
                }
            }
        }
    }
}