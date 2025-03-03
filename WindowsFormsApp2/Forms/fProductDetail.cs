using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.Utils.About;
using System.Windows.Forms;
using DevExpress.XtraGrid.Localization;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.Helpers.FormHelpers;
using WindowsFormsApp2.Helpers.Messages;

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
            GridLocalizer.Active = new MyGridLocalizer();
        }

        public async void DataLoad()
        {
            if (_productDetail != null)
            {
                this.Text = $"Məhsul haqqında - {_productDetail.ProductName}";
                string supplierName = string.Join(", ", _productDetail.Suppliers.Select(x=> x.Name));
                string UnitName = string.Join(", ", _productDetail.Units.Select(x => x.Name));
                tSupplierName.Text = supplierName;
                tProductName.Text = _productDetail.ProductName;
                tBarcode.Text = _productDetail.Barcode;
                tProductCode.Text = _productDetail.ProductCode;
                tPurchasePrice.Text = _productDetail.PurchasePrice.ToString("C2");
                tSalePrice.Text = _productDetail.SalePrice.ToString("C2");
                tTaxtType.Text = _productDetail.TaxName;
                tStockAmount.Text = $"{_productDetail.StockAmount.ToString("N2")} - {UnitName}";
                ImageFromByteArray(_productDetail.ProductImage);

                gridControl1.MainView = gridPurchases;
                var data = await DbProsedures.Get_ProductPurchasesDataAsync(_productDetail.Barcode.Trim());
                gridControl1.DataSource = data;
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

        private void bProductDelete_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show($"{tProductName.Text} məhsulunu silmək istədiyinizə əminsiniz ?", nameof(HeaderMessage.Xəbərdarlıq), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int response = DbProsedures.DeleteProduct(new ProductsDetail
                {
                    SupplierName = "",
                    Barocde = tBarcode.Text.Trim(),
                    ProductId = _productDetail.ProductId,
                });

                if (response == 1)
                {
                    ReadyMessages.SUCCESS_DEFAULT_MESSAGE($"{_productDetail.ProductName} ({_productDetail.Barcode}) məhsulu uğurla silindi");
                }
            }
        }

        private async void chSaleHistory_CheckedChanged(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridControl1.MainView = gridSales;
            var data = await DbProsedures.Get_ProductSalesDataAsync(_productDetail.Barcode.Trim());
            gridControl1.DataSource = data;
        }

        private async void chPurchaseHistory_CheckedChanged(object sender, EventArgs e)
        {
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