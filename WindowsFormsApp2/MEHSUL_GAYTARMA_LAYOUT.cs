using System;
using System.ComponentModel;
using System.Data;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;

namespace WindowsFormsApp2
{
    public partial class MEHSUL_GAYTARMA_LAYOUT : DevExpress.XtraEditors.XtraForm
    {
        public MEHSUL_GAYTARMA_LAYOUT()
        {
            InitializeComponent();
            FormHelpers.GridPanelText(gridView1);
        }

        private void MEHSUL_GAYTARMA_LAYOUT_Load(object sender, EventArgs e)
        {
            dateTarix.Properties.MaxDate = DateTime.Today;
            dateTarix.DateTime = DateTime.Now;
            tProccessNo.Text = DbProsedures.GET_ProductReturnProcessNo();
           

            SupplierDataLoad();
        }

        private void SupplierDataLoad()
        {
            string query = "SELECT TECHIZATCI_ID, SIRKET_ADI AS N'ŞİRKƏT ADI' FROM COMPANY.TECHIZATCI WHERE IsDeleted = 0";
            var data = DbProsedures.ConvertToDataTable(query);
            lookSupplier.Properties.DisplayMember = "ŞİRKƏT ADI";
            lookSupplier.Properties.ValueMember = "TECHIZATCI_ID";
            lookSupplier.Properties.DataSource = data;
            lookSupplier.Properties.PopulateColumns();
            lookSupplier.Properties.Columns[0].Visible = false;
        }


        private void simpleButton6_Click(object sender, EventArgs e)
        {
            try
            {
                gridView1.UpdateCurrentRow();

                int[] selectedRows = gridView1.GetSelectedRows();

                if (selectedRows.Length > 0)
                {
                    foreach (int item in selectedRows)
                    {
                        var row = gridView1.GetRow(item) as RefundProduct;

                        if (row.RefundQuantity <= 0)
                        {
                            FormHelpers.Alert("Seçili olan sətirdə qaytarılacaq miqdar daxil edilmədi", Enums.MessageType.Warning);
                            return;
                        }

                        int result = DbProsedures.InsertRefundProductMain(tProccessNo.Text, Convert.ToDateTime(dateTarix.Text));
                        if (result > 0)
                        {
                            DbProsedures.InsertRefundProductDetail(result, row.Id, row.RefundQuantity, tComment.Text);

                            FormHelpers.OperationLog(new Helpers.DB.DatabaseClasses.OperationLogs
                            {
                                OperationType = Enums.OperationType.RefundProduct,
                                OperationId = result
                            });
                            FormHelpers.Log($"{row.SupplierName} təchizatçısının {row.ProductName} məhsulunun {row.RefundQuantity} {row.UnitName} qaytarması edildi");
                        }
                    }
                    FormHelpers.Alert("Məhsul qaytarılması uğurla edildi", Enums.MessageType.Success);
                    RefundProductsDataLoad(lookSupplier.Text);
                    tTotalAmount.Text = DbProsedures.GET_ProductReturnDebtTotal((int)lookSupplier.EditValue);
                    Clear();
                }
                else
                {
                    FormHelpers.Alert("Məhsul seçimi edilmədi", Enums.MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
            }
        }

        private void RefundProductsDataLoad(string supplierName)
        {
            string query = $@"SELECT 
  Id, 
  Date, 
  SupplierName, 
  ContractNo, 
  ProductName, 
  ProductCode, 
  UnitName, 
  Quantity, 
  PurchasePrice, 
  Warehouse, 
  RefundQuantity 
    FROM[dbo].[gaytarilacag_mallar] 
WHERE 
  SupplierName = N'{supplierName}' 
  AND Quantity > 0.00";

            var data = DbProsedures.ConvertToDataTable(query);

            BindingList<RefundProduct> products = new BindingList<RefundProduct>();
            foreach (DataRow item in data.Rows)
            {
                RefundProduct product = new RefundProduct();
                product.Id = Convert.ToInt32(item["Id"].ToString());
                product.Date = Convert.ToDateTime(item["Date"].ToString());
                product.SupplierName = item["SupplierName"].ToString();
                product.ContractNo = item["ContractNo"].ToString();
                product.ProductName = item["ProductName"].ToString();
                product.ProductCode = item["ProductCode"].ToString();
                product.UnitName = item["UnitName"].ToString();
                product.Quantity = Convert.ToDecimal(item["Quantity"].ToString());
                product.PurchasePrice = Convert.ToDecimal(item["PurchasePrice"].ToString());
                product.Warehouse = item["Warehouse"].ToString();
                product.RefundQuantity = 0;
                products.Add(product);
            }

            gridControl1.DataSource = products;
        }
        
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FormHelpers.OpenForm<MEHSUL_GAYTARMA_HESABAT>();
        }

        private void lookSupplier_TextChanged(object sender, EventArgs e)
        {
            if (lookSupplier.EditValue != null)
            {
                tTotalAmount.Text = DbProsedures.GET_ProductReturnDebtTotal((int)lookSupplier.EditValue);
                RefundProductsDataLoad(lookSupplier.Text);
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            gridControl1.DataSource = null;
            tComment.Clear();
            lookSupplier.Clear();
            SupplierDataLoad();
            tTotalAmount.Clear();
            tProccessNo.Text = DbProsedures.GET_ProductReturnProcessNo();
            dateTarix.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private class RefundProduct
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public string SupplierName { get; set; }
            public string ContractNo { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public string UnitName { get; set; }
            public decimal Quantity { get; set; }
            public decimal PurchasePrice { get; set; }
            public string Warehouse { get; set; }
            public decimal RefundQuantity { get; set; }
        }
    }
}