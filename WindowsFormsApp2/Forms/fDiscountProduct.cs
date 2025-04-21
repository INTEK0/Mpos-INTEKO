using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraReports.Design;
using FluentValidation;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.Validations;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses.ProductDetail;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fDiscountProduct : BaseForm
    {
        private List<DiscountProduct> _products = new List<DiscountProduct>();
        private DiscountProduct _product;
        private Enums.Operation _operation = Operation.Add;
        private int _highlightedRowHandle = -1;
        public fDiscountProduct()
        {
            InitializeComponent();
            GridPanelText(gridView1);
        }

        private enum SearchType
        {
            [Description("Hamısı")]
            All,
            [Description("Endirimli məhsullar")]
            DiscountProduct,
            [Description("Endirimsiz məhsullar")]
            NotDiscountProduct
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

        private void ProductsDataLoad(SearchType type = SearchType.All)
        {
            Cursor.Current = Cursors.WaitCursor;
            gridControl1.DataSource = null;
            string query = null;

            switch (type)
            {
                case SearchType.All:
                    query = @"
DECLARE @Result TABLE (
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
rs.TECHIZATCI AS SupplierName,
rs.PRODUCTNAME AS ProductName,
rs.BARCODE AS Barcode,
rs.PURCHASEPRICE AS PurchasePrice,
rs.SALEPRICE AS SalePrice,
ISNULL(dp.DiscountTotal,0) AS DiscountTotal,
ISNULL(rs.SALEPRICE - dp.DiscountTotal,0) AS NewSalePrice,
dp.StartDate,
dp.EndDate,
ISNULL(dp.Status,0) AS [Status],
CASE 
   WHEN ISNULL(dp.Status, 0) = 1 THEN N'Aktiv'
   ELSE N'Deaktiv'
END AS StatusName
FROM @Result rs
LEFT JOIN DISCOUNT_PRODUCTS dp ON dp.Barcode = rs.BARCODE";
                    colNewSalePrice.Visible = true;
                    break;
                case SearchType.DiscountProduct:
                    query = @"
DECLARE @Result TABLE (
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
rs.TECHIZATCI AS SupplierName,
rs.PRODUCTNAME AS ProductName,
rs.BARCODE AS Barcode,
rs.PURCHASEPRICE AS PurchasePrice,
rs.SALEPRICE AS SalePrice,
ISNULL(dp.DiscountTotal,0) AS DiscountTotal,
ISNULL(rs.SALEPRICE - dp.DiscountTotal,0) AS NewSalePrice,
dp.StartDate,
dp.EndDate,
ISNULL(dp.Status,0) AS [Status],
CASE 
   WHEN ISNULL(dp.Status, 0) = 1 THEN N'Aktiv'
   ELSE N'Deaktiv'
END AS StatusName
FROM @Result rs
INNER JOIN DISCOUNT_PRODUCTS dp ON dp.Barcode = rs.BARCODE";
                    colNewSalePrice.Visible = true;

                    break;
                case SearchType.NotDiscountProduct:
                    query = @"
DECLARE @Result TABLE (
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
rs.TECHIZATCI AS SupplierName,
rs.PRODUCTNAME AS ProductName,
rs.BARCODE AS Barcode,
rs.PURCHASEPRICE AS PurchasePrice,
rs.SALEPRICE AS SalePrice,
0 AS DiscountTotal,
'' AS StartDate,
'' AS EndDate,
0 AS [Status],
N'Deaktiv' AS StatusName
FROM @Result rs";
                    colNewSalePrice.Visible = false;
                    break;
            }

            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
            Cursor.Current = Cursors.Default;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            switch (_operation)
            {
                case Operation.Add:
                    Add();
                    break;
                case Operation.Update:
                    Edit();
                    break;
            }
        }

        private async void Add()
        {
            if (gridView1.RowCount > 0)
            {
                _products.Clear();
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                int[] selectedRows = gridView1.GetSelectedRows();
                foreach (var item in selectedRows)
                {

                    var row = gridView1.GetDataRow(item);

                    CultureInfo azCulture = new CultureInfo("az-AZ");
                    decimal salePrice = Convert.ToDecimal(row["SalePrice"].ToString());
                    decimal totalDiscount = 0;
                    if (Decimal.Parse(tPercent.Text, NumberStyles.Any, azCulture) > 0)
                    {
                        totalDiscount = (salePrice * Decimal.Parse(tPercent.Text)) / 100;
                    }
                    else
                    {
                        totalDiscount = Decimal.Parse(tAmount.Text, NumberStyles.Any, azCulture);
                    }



                    _product = new DiscountProduct();
                    _product.Barcode = row["Barcode"].ToString();
                    _product.DiscountPercent = Decimal.Parse(tPercent.Text, NumberStyles.Any, azCulture);
                    _product.DiscountAmount = Decimal.Parse(tAmount.Text, NumberStyles.Any, azCulture);
                    _product.DiscountTotal = totalDiscount;
                    _product.StartDate = dateStart.DateTime;
                    _product.EndDate = dateEnd.DateTime;
                    _product.Status = toggleStatus.IsOn;
                    _product.SalePrice = salePrice;
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
                    Cursor.Current = Cursors.WaitCursor;
                    await Task.Run(() => DbProsedures.INSERT_DiscountProductAsync(_products));
                    Clear();
                    ProductsDataLoad((SearchType)lookSearchType.EditValue);
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private async void Edit()
        {
            Cursor.Current = Cursors.WaitCursor;
            decimal totalDiscount = 0;
            if (Decimal.Parse(tPercent.Text, NumberStyles.Any, new CultureInfo("az-AZ")) > 0)
            {
                totalDiscount = (_product.SalePrice * Decimal.Parse(tPercent.Text)) / 100;
            }
            else
            {
                totalDiscount = _product.SalePrice - Decimal.Parse(tAmount.Text, NumberStyles.Any, new CultureInfo("az-AZ"));
            }



            _product.Barcode = _product.Barcode;
            _product.DiscountPercent = Decimal.Parse(tPercent.Text, NumberStyles.Any, new CultureInfo("az-AZ"));
            _product.DiscountAmount = Decimal.Parse(tAmount.Text, NumberStyles.Any, new CultureInfo("az-AZ"));
            _product.DiscountTotal = totalDiscount;
            _product.StartDate = dateStart.DateTime;
            _product.EndDate = dateEnd.DateTime;
            _product.Status = toggleStatus.IsOn;
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

            await Task.Run(() => DbProsedures.UPDATE_DiscountProductAsync(_product));
            Clear();
            ProductsDataLoad((SearchType)lookSearchType.EditValue);
            bEditCancel_Click(null, null);
        }

        private void Clear()
        {
            tPercent.EditValue = 0;
            tAmount.EditValue = 0;
            dateStart.Clear();
            dateEnd.Clear();
            _operation = Operation.Add;
        }

        private void tPercent_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tPercent.Text) && Decimal.Parse(tPercent.Text) > 0)
            {
                tAmount.EditValue = 0;
            }
        }

        private void tAmount_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tAmount.Text) && Decimal.Parse(tAmount.Text) > 0)
            {
                tPercent.EditValue = 0;
            }
        }

        private void lookSearchType_EditValueChanged(object sender, EventArgs e)
        {
            ProductsDataLoad((SearchType)lookSearchType.EditValue);
        }

        private void chStatus_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (_operation is Operation.Add)
            {
                string barcode = gridView1.GetFocusedRowCellValue("Barcode").ToString();
                string date = gridView1.GetFocusedRowCellValue("StartDate").ToString();
                decimal salePrice = Decimal.Parse(gridView1.GetFocusedRowCellValue("SalePrice").ToString());

                if (string.IsNullOrWhiteSpace(date))
                    return;


                if (!string.IsNullOrWhiteSpace(barcode))
                {
                    _product = new DiscountProduct();
                    using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
                    {
                        connection.Open();
                        string query = "SELECT TOP 1 * FROM  DISCOUNT_PRODUCTS WHERE Barcode = @barcode";
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.Add("@barcode", SqlDbType.NVarChar, 100).Value = barcode;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {

                                    _product = FormHelpers.MapReaderToObject<DiscountProduct>(reader);
                                    _product.SalePrice = salePrice;

                                    var method = this.GetType().GetMethod("ReceiveData");
                                    if (method != null)
                                    {
                                        method.MakeGenericMethod(_product.GetType()).Invoke(this, new object[] { _product });
                                    }

                                    int rowHandle = gridView1.FocusedRowHandle;

                                    if (_highlightedRowHandle == rowHandle)
                                        _highlightedRowHandle = -1;
                                    else
                                        _highlightedRowHandle = rowHandle;

                                    gridView1.RefreshRow(rowHandle);
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void ReceiveData<T>(T data)
        {
            if (data is DatabaseClasses.DiscountProduct product)
            {
                tPercent.Text = product.DiscountPercent.ToString();
                tAmount.Text = product.DiscountAmount.ToString();
                dateStart.EditValue = product.StartDate;
                dateEnd.EditValue = product.EndDate;
                toggleStatus.IsOn = product.Status;
                bAdd.Text = "Düzəliş et";
                bAdd.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
                tablePanel1.Columns[1].Visible = true;
                bActive.Enabled = false;
                bDeactive.Enabled = false;
                bDelete.Enabled = false;
                _operation = Operation.Update;
            }
        }

        private async void bDelete_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                _products.Clear();
                Cursor.Current = Cursors.WaitCursor;
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();
                int[] selectedRows = gridView1.GetSelectedRows();
                foreach (var item in selectedRows)
                {
                    var row = gridView1.GetDataRow(item);

                    _product = new DiscountProduct();
                    _product.Barcode = row["Barcode"].ToString();
                    _products.Add(_product);
                }

                if (_products.Count > 0)
                {
                    await Task.Run(() => DbProsedures.DELETE_DiscountProductAsync(_products));
                    Clear();
                    ProductsDataLoad((SearchType)lookSearchType.EditValue);
                }
            }
        }

        private void bEditCancel_Click(object sender, EventArgs e)
        {
            bAdd.Text = "Endirim et";
            bAdd.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            tablePanel1.Columns[1].Visible = false;
            bActive.Enabled = true;
            bDeactive.Enabled = true;
            bDelete.Enabled = true;
            _highlightedRowHandle = -1;
            gridView1.RefreshRow(_highlightedRowHandle);
            gridView1.Focus();
            Clear();

        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == _highlightedRowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
            else
            {
                e.Appearance.BackColor = Color.Transparent;
            }

            if (e.Column == colStatus)
            {
                e.Appearance.Font = new Font("Nunito",10, FontStyle.Bold);
                if (e.CellValue != null)
                {
                    if (e.CellValue.ToString() == "Aktiv")
                    {
                        e.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
                    }
                    else
                    {
                        e.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
                    }
                }
            }
        }

        private async void chStatus_Toggled(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;

            string barcode = gridView1.GetFocusedRowCellValue("Barcode").ToString();
            string date = gridView1.GetFocusedRowCellValue("StartDate").ToString();
            bool currentStatus = Convert.ToBoolean(gridView1.GetFocusedRowCellValue("Status"));

            if (string.IsNullOrWhiteSpace(date))
                return;

            bool newStatus = !currentStatus;

            await DbProsedures.DiscountProduct_UpdateStatusAsync(barcode, newStatus);
            gridView1.SetFocusedRowCellValue("Status", newStatus);
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            ProductsDataLoad((SearchType)lookSearchType.EditValue);
        }

        private async void bDeactive_Click(object sender, EventArgs e)
        {
            var selectedRows = gridView1.GetSelectedRows();

            
            foreach (var rowHandle in selectedRows)
            {
                string barcode = gridView1.GetFocusedRowCellValue("Barcode").ToString();
                string date = gridView1.GetFocusedRowCellValue("StartDate").ToString();

                if (string.IsNullOrWhiteSpace(date))
                    break;

                await DbProsedures.DiscountProduct_UpdateStatusAsync(barcode, false);
                gridView1.SetRowCellValue(rowHandle, "Status", false);
            }
        }

        private async void bActive_Click(object sender, EventArgs e)
        {
            var selectedRows = gridView1.GetSelectedRows();


            foreach (var rowHandle in selectedRows)
            {
                string barcode = gridView1.GetFocusedRowCellValue("Barcode").ToString();
                string date = gridView1.GetFocusedRowCellValue("StartDate").ToString();

                if (string.IsNullOrWhiteSpace(date))
                    break;

                await DbProsedures.DiscountProduct_UpdateStatusAsync(barcode, true);
                gridView1.SetRowCellValue(rowHandle, "Status", true);
            }
        }
    }
}