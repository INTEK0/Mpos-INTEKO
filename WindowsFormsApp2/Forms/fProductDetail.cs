using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fProductDetail:BaseForm 
    {
        private readonly ProductDetail _productDetail;
        public fProductDetail( ProductDetail productDetail)
        {
            InitializeComponent();
            _productDetail = productDetail;
            GridPanelText(gridSales);
            GridPanelText(gridPurchases);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        public void DataLoad(ProductDetail product)
        {
            if (product != null)
            {
                this.Text = $"Məhsul haqqında - {product.ProductName}";
                tSupplierName.Text = product.SupplierName;
                tProductName.Text = product.ProductName;
                tBarcode.Text = product.Barcode;
                tProductCode.Text = product.ProductCode;
                tPurchasePrice.Text = product.PurchasePrice.ToString("C2");
                tSalePrice.Text = product.SalePrice.ToString("C2");
                tTaxtType.Text = product.TaxName;
                tStockAmount.Text = $"{product.StockAmount.ToString("N2")} - {product.UnitName}";
                ImageFromByteArray(product.ProductImage);
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

        }

        private void chSaleHistory_CheckedChanged(object sender, EventArgs e)
        {
            gridControl1.MainView = gridSales;
        }

        private void chPurchaseHistory_CheckedChanged(object sender, EventArgs e)
        {
            gridControl1.MainView = gridPurchases;
        }

        private void fProductDetail_Load(object sender, EventArgs e)
        {
            DataLoad(_productDetail);

            var data = DbProsedures.Get_ProductSalesData(_productDetail.Barcode.Trim());
            gridControl1.DataSource = data;
        }
    }
}