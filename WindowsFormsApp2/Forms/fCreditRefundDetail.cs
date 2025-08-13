using System;
using System.Data;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Forms
{
    public partial class fCreditRefundDetail : DevExpress.XtraEditors.XtraForm
    {
        private readonly int _Id;
        public fCreditRefundDetail(int id)
        {
            InitializeComponent();
            _Id = id;
            FormHelpers.GridPanelText(gridView1);
        }

        private void fCreditRefundDetail_Load(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void DataLoad()
        {
            dynamic query = $@"SELECT 
  km.KREDIT_SATISI_MAIN_ID as Id,
  km.EMELIIYYAT_NOMRE as ProccessNo, 
  km.TARIX as CreditDate, 
  km.GAIME_NOMRE as ContractNo, 
  km.MUSTERI as CustomerName,
  CAST(km.prd_qty as decimal(18, 3)) as Quantity, 
  CAST(km.prd_price as decimal(18, 2)) as SalePrice, 
  CAST((km.prd_qty * prd_price) as decimal(18, 2)) as TotalAmount, 
  km.product_id as ProductId,
  supplier.SIRKET_ADI as SupplierName,
  category.KATEGORIYA AS CategoryName,
  md.MEHSUL_ADI as ProductName,
  md.BARKOD as Barcode,
  tax.EDV as TaxName,
  unit.VAHIDLER_NAME as UnitName
FROM 
  [KREDIT_SATISI_MAIN] km
INNER JOIN MAL_ALISI_DETAILS md ON md.MAL_ALISI_DETAILS_ID = km.product_id
INNER JOIN MAL_ALISI_MAIN ma ON ma.MAL_ALISI_MAIN_ID = md.MAL_ALISI_MAIN_ID
INNER JOIN COMPANY.TECHIZATCI supplier ON supplier.TECHIZATCI_ID = ma.TECHIZATCI_ID
INNER JOIN KATEGORIYA category ON category.KATEGORIYA_ID = md.KATEGORIYA
INNER JOIN VERGI_DERECESI tax ON tax.EDV_ID = md.VERGI_DERECESI
INNER JOIN VAHIDLER unit ON unit.VAHIDLER_ID = md.VAHID
WHERE
  km.KREDIT_SATISI_MAIN_ID = {_Id}";

            var data = DbProsedures.ConvertToDataTable(query);
            foreach (DataRow row in data.Rows)
            {
                tProccessNo.Text = row["ProccessNo"].ToString();
                tCustomerName.Text = row["CustomerName"].ToString();
                tDate.Text = Convert.ToDateTime(row["CreditDate"]).ToString("dd.MM.yyyy");
                tContractNo.Text = row["ContractNo"].ToString();
                this.Text = $"Kredit satış məlumatları - Müq: {row["ContractNo"]}";
            }
            gridControl1.DataSource = data;
        }
    }
}