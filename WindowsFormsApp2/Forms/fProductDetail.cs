using System;
using System.Drawing;
using System.IO;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fProductDetail : BaseForm
    {
        private readonly ProductDetail _productDetail;
        public fProductDetail(ProductDetail productDetail)
        {
            InitializeComponent();
            _productDetail = productDetail;
            GridPanelText(gridSales);
            GridPanelText(gridPurchases);
        }

        public async void DataLoad()
        {
            if (_productDetail != null)
            {
                this.Text = $@"Məhsul haqqında - {_productDetail.ProductName}";
                tSupplierName.Text = _productDetail.SupplierName;
                tProductName.Text = _productDetail.ProductName;
                tBarcode.Text = _productDetail.Barcode;
                tProductCode.Text = _productDetail.ProductCode;
                tPurchasePrice.Text = _productDetail.PurchasePrice.ToString("C2");
                tSalePrice.Text = _productDetail.SalePrice.ToString("C2");
                tTaxtType.Text = _productDetail.TaxName;
                tStockAmount.Text = $@"{_productDetail.StockAmount.ToString("N2")} - {_productDetail.UnitName}";
                //ImageFromByteArray(_productDetail.ProductImage);
                picImage.Properties.NullText = "Şəkil yoxdur";
                gridControl1.MainView = gridPurchases;
                if (_productDetail.Barcode != "Yoxdur")
                {
                    var data = await DbProsedures.Get_ProductPurchasesDataAsync(_productDetail.Barcode.Trim());
                    gridControl1.DataSource = data;
                }
            }
        }

        private void ImageFromByteArray(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return;

            using (MemoryStream ms = new MemoryStream(imageData))
            {
                Image image = Image.FromStream(ms);
                picImage.Image = image;
            }
        }

        private async void chSaleHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (_productDetail.Barcode is "Yoxdur")
                return;
            gridControl1.DataSource = null;
            gridControl1.MainView = gridSales;
            var data = await DbProsedures.Get_ProductSalesDataAsync(_productDetail.Barcode.Trim());
            gridControl1.DataSource = data;
        }

        private async void chPurchaseHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (_productDetail.Barcode is "Yoxdur")
                return;
            gridControl1.DataSource = null;
            gridControl1.MainView = gridPurchases;
            var data = await DbProsedures.Get_ProductPurchasesDataAsync(_productDetail.Barcode.Trim());
            gridControl1.DataSource = data;
        }

        private void fProductDetail_Load(object sender, EventArgs e)
        {
            DataLoad();
        }
    }
}