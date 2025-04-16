using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using FluentValidation;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Validations;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;

namespace WindowsFormsApp2.Forms
{
    public partial class fDiscountProduct : DevExpress.XtraEditors.XtraForm
    {
        private List<DiscountProduct> _products = new List<DiscountProduct>();
        private DiscountProduct _product;
        public fDiscountProduct()
        {
            InitializeComponent();
        }

        private enum SearchType
        {
            [Description("Hamısı")]
            All,
            [Description("Endirimli məhsullar")]
            DiscountProduct
        }

        private void fDiscountProduct_Load(object sender, EventArgs e)
        {
            SearchTypeLoad();
            ProductsDataLoad();
        }

        private void SearchTypeLoad()
        {
            var data = Enum.GetValues(typeof(SearchType))
               .Cast<SearchType>()
               .Select(x => new
               {
                   Key = (int)x,
                   Value = GetEnumDescription(x)
               })
               .ToList();

            lookSearchType.Properties.DataSource = data;
            lookSearchType.Properties.ValueMember = "Key";
            lookSearchType.Properties.DisplayMember = "Value";
            lookSearchType.Properties.Columns.Clear();
            lookSearchType.Properties.Columns.Add(new LookUpColumnInfo("Value", "Axtarış növü"));
            lookSearchType.Properties.Columns.Add(new LookUpColumnInfo("Key", 0) { Visible = false });
            lookSearchType.EditValue = SearchType.All;
        }

        private void ProductsDataLoad()
        {
            Cursor.Current = Cursors.WaitCursor;
            gridControl1.DataSource = null;
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
PRODUCTNAME AS ProductName,
BARCODE AS Barcode,
PURCHASEPRICE AS PurchasePrice,
SALEPRICE AS SalePrice,
ISNULL(dp.DiscountTotal,0) AS DiscountTotal,
dp.StartDate,
dp.EndDate,
ISNULL(dp.Status,0) AS [Status]
FROM @Result rs
LEFT JOIN DISCOUNT_PRODUCTS dp ON dp.ProductId = rs.MAL_ALIS_DETAILS_ID ";


            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
            Cursor.Current = Cursors.Default;
        }
        
        private void bAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private async void Add()
        {
            if (gridView1.RowCount > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                int[] selectedRows = gridView1.GetSelectedRows();
                foreach (var item in selectedRows)
                {
                    var row = gridView1.GetDataRow(item);

                    _product = new DiscountProduct();
                    _product.ProductId = Convert.ToInt32(row["Id"].ToString());
                    _product.DiscountPercent = (decimal)tPercent.EditValue;
                    _product.DiscountAmount = (decimal)tAmount.EditValue;
                    _product.DiscountTotal = (decimal)tTotal.EditValue;
                    _product.StartDate = dateStart.DateTime;
                    _product.EndDate = dateEnd.DateTime;
                    _product.Status = Convert.ToBoolean(row["Id"].ToString());
                    _product.UserId = Properties.Settings.Default.UserID;

                    var validator = new DiscountProductValidation();
                    var validateResult = validator.Validate(_product);

                    if (!validateResult.IsValid)
                    {
                        foreach (var error in validateResult.Errors)
                        {
                            FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                            return;
                        }
                    }

                    _products.Add(_product);
                }

                if (_products.Count > 0)
                {
                    await Task.Run(() => DbProsedures.INSERT_DiscountProduct(_products));
                }
            }
        }
    }
}