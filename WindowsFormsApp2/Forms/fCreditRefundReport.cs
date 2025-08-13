using System;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Forms
{
    public partial class fCreditRefundReport : DevExpress.XtraEditors.XtraForm
    {
        public fCreditRefundReport()
        {
            InitializeComponent();
            FormHelpers.GridPanelText(gridView1);
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void bReport_Click(object sender, EventArgs e)
        {
           FormHelpers.ExcelExport(gridControl1, "Kredit satış qaytarma hesabatı");
        }

        private void DataLoad()
        {
            string start = dateStart.DateTime.ToString("yyyy-MM-dd");
            string end = dateEnd.DateTime.AddDays(1).ToString("yyyy-MM-dd");
            dynamic query = $@"SELECT 
cr.Id, 
cr.KreditSatisMainId as CreditSaleId,
u.AD as Cashier,
cs.EMELIIYYAT_NOMRE as ProccessNo,
cr.RefundDate,
cr.ReceiptNo,
cs.GAIME_NOMRE as ContractNo,
cs.MUSTERI as CustomerName,
CASE
WHEN cr.PaymentTypeId = 0 then N'NİSYƏ'
WHEN cr.PaymentTypeId = 1 then N'NAĞD'
WHEN cr.PaymentTypeId = 2 then N'KART'
WHEN cr.PaymentTypeId = 3 then N'NAĞD - KART'
END PaymentType,
cr.TotalAmount
FROM KREDIT_SATISI_MAIN_QAYTARMA cr
INNER JOIN 
KREDIT_SATISI_MAIN cs ON cs.KREDIT_SATISI_MAIN_ID = cr.KreditSatisMainId
INNER JOIN 
userParol u ON u.id = cr.UserId
WHERE cr.RefundDate BETWEEN '{start}' AND '{end}'";

            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
        }

        private void bDetail_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("CreditSaleId"));
            fCreditRefundDetail f = new fCreditRefundDetail(Id);
            f.ShowDialog();
        }

        private void fCreditRefundReport_Load(object sender, EventArgs e)
        {
            dateStart.DateTime = DateTime.Now;
            dateEnd.DateTime = DateTime.Now;
        }
    }
}