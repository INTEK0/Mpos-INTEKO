using ComponentFactory.Krypton.Toolkit;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Forms
{
    public partial class fQuickAddProduct : DevExpress.XtraEditors.XtraForm
    {
        private BindingList<Product> _products;
        private int _rowCount = 1;
        public fQuickAddProduct()
        {
            InitializeComponent();
        }

        private class Product
        {
            public int No { get; set; }
            public int Id { get; set; }
            public string CategoryName { get; set; }
            public string ProductName { get; set; }
            public string Barcode { get; set; }
            public decimal Amount { get; set; }
            public string UnitName { get; set; }
            public decimal PurchasePrice { get; set; }
            public decimal TotalPurchaseAmount { get => Amount * PurchasePrice; }
            public decimal Percent { get; set; }
            public decimal SalePrice { get; set; }
            public decimal TotalSaleAmount { get => Amount * SalePrice; }
            //public decimal Gain { get => TotalSaleAmount - TotalPurchaseAmount; }
            public decimal GainAmount { get => (Amount * SalePrice) - (Amount * PurchasePrice); }
        }

        private void fQuickAddProduct_Load(object sender, EventArgs e)
        {
            dateTarix.DateTime = DateTime.Now;
            _products = new BindingList<Product>();
            gridControl1.DataSource = _products;
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


                string query = $@"SELECT TOP 1 
m.MAL_ALISI_DETAILS_ID AS Id,
k.KATEGORIYA AS CategoryName,
m.MEHSUL_ADI AS ProductName,
m.BARKOD AS Barcode,
m.ALIS_GIYMETI AS PurchasePrice,
m.SATIS_GIYMETI AS SalePrice,
v.VAHIDLER_NAME AS UnitName
FROM MAL_ALISI_DETAILS m
INNER JOIN VAHIDLER v ON v.VAHIDLER_ID = m.VAHID
INNER JOIN KATEGORIYA k ON k.KATEGORIYA_ID = m.KATEGORIYA
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
                            ProductName = row["ProductName"].ToString(),
                            Barcode = row["Barcode"].ToString(),
                            Amount = 1,
                            UnitName = row["UnitName"].ToString(),
                            PurchasePrice = Convert.ToDecimal(row["PurchasePrice"].ToString()),
                            SalePrice = Convert.ToDecimal(row["SalePrice"].ToString())
                        };
                        _products.Add(product);
                    }
                    _rowCount++;
                    gridControl1.RefreshDataSource();

                }


                tBarcode.Text = null;
                tBarcode.Focus();
            }
        }
    }
}