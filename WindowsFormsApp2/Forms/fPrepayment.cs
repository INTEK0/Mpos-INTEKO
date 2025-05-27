using System;
using System.Linq;
using DevExpress.XtraEditors;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fPrepayment : DevExpress.XtraEditors.XtraForm
    {
        public fPrepayment()
        {
            InitializeComponent();
            GridPanelText(gridAvans);
        }

        private enum SearchType
        {
            All,
            FiscalID,
            ReceiptNo
        }

        private void AvansPayDataLoad(SearchType type)
        {
            gridControlAvans.DataSource = null;
            string query = null;

            switch (type)
            {
                case SearchType.All:
                    query = @"SELECT 
psm.pos_satis_check_main_id as Id,
psm.emeliyyat_nomre as ProccessNo,
psm.pos_nomre as pos_nomre, 
psm.fiscalNum as fiscalId, 
psm.date_ as PayDate,
u.AD as Username, 
psm.Prepayment,
psm.UMUMI_MEBLEG as Total,
psm.UMUMI_MEBLEG - psm.Prepayment as Debt,
customer.AD + ' ' + customer.SOYAD + ' ' + customer.ATAADI as CustomerName,
case
when (psm.NEGD_>0.00 and psm.KART_ <=0.00) then N'NAĞD' 
when (psm.KART_>0.00 and psm.NEGD_< =0.00) then N'KART'
ELSE N'NAĞD-KART' END AS PayType
FROM [pos_satis_check_main] psm
LEFT JOIN MUSTERILER customer ON customer.MUSTERILER_ID = psm.CustomerId
INNER JOIN userParol u ON u.id = psm.user_id_
WHERE psm.Prepayment IS NOT NULL AND psm.Prepayment !=  0 AND psm.PREfiscal_id IS NULL
order by psm.date_ asc";
                    break;
                case SearchType.FiscalID:
                    query = $@"SELECT 
psm.pos_satis_check_main_id as Id,
psm.emeliyyat_nomre as ProccessNo,
psm.pos_nomre as pos_nomre, 
psm.fiscalNum as fiscalId, 
psm.date_ as PayDate,
u.AD as Username, 
psm.Prepayment,
psm.UMUMI_MEBLEG as Total,
psm.UMUMI_MEBLEG - psm.Prepayment as Debt,
customer.AD + ' ' + customer.SOYAD + ' ' + customer.ATAADI as CustomerName,
case
when (psm.NEGD_>0.00 and psm.KART_ <=0.00) then N'NAĞD' 
when (psm.KART_>0.00 and psm.NEGD_< =0.00) then N'KART'
ELSE N'NAĞD-KART' END AS PayType
FROM [pos_satis_check_main] psm
LEFT JOIN MUSTERILER customer ON customer.MUSTERILER_ID = psm.CustomerId
INNER JOIN userParol u ON u.id = psm.user_id_
WHERE psm.Prepayment IS NOT NULL AND psm.Prepayment != 0 AND psm.PREfiscal_id IS NULL AND psm.fiscalNum = N'{tSearch.Text.Trim()}'";
                    break;
                case SearchType.ReceiptNo:
                    query = $@"SELECT 
psm.pos_satis_check_main_id as Id,
psm.emeliyyat_nomre as ProccessNo,
psm.pos_nomre as pos_nomre, 
psm.fiscalNum as fiscalId, 
psm.date_ as PayDate,
u.AD as Username, 
psm.Prepayment,
psm.UMUMI_MEBLEG as Total,
psm.UMUMI_MEBLEG - psm.Prepayment as Debt,
customer.AD + ' ' + customer.SOYAD + ' ' + customer.ATAADI as CustomerName,
case
when (psm.NEGD_>0.00 and psm.KART_ <=0.00) then N'NAĞD' 
when (psm.KART_>0.00 and psm.NEGD_< =0.00) then N'KART'
ELSE N'NAĞD-KART' END AS PayType
FROM [pos_satis_check_main] psm
LEFT JOIN MUSTERILER customer ON customer.MUSTERILER_ID = psm.CustomerId
INNER JOIN userParol u ON u.id = psm.user_id_
WHERE psm.Prepayment IS NOT NULL AND psm.Prepayment != 0 AND psm.PREfiscal_id IS NULL AND psm.pos_nomre = '{tSearch.Text.Trim()}'";
                    break;
            }

            var data = DbProsedures.ConvertToDataTable(query);
            gridControlAvans.DataSource = data;
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            var check = groupControl1.Controls.OfType<CheckEdit>().FirstOrDefault(x => x.Checked);
            if (check == null) { FormHelpers.Alert("Axtarış növü seçilmədi", Enums.MessageType.Warning); return; }

            switch (check.Tag.ToString())
            {
                case "All":
                    AvansPayDataLoad(SearchType.All);
                    break;
                case "FiscalID":
                    AvansPayDataLoad(SearchType.FiscalID);
                    break;
                case "ReceiptNo":
                    AvansPayDataLoad(SearchType.ReceiptNo);
                    break;
            }
        }

        private void bPay_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var fiskal = gridAvans.GetFocusedRowCellValue("fiscalId");

            fPrepaymentPay f = new fPrepaymentPay(fiskal.ToString());
            if (f.ShowDialog() is System.Windows.Forms.DialogResult.OK)
            {
                gridControlAvans.DataSource = null;
            }
        }

        private void bDetail_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int mainId = Convert.ToInt32(gridAvans.GetFocusedRowCellValue("Id"));
            fPrepaymentProducts f = new fPrepaymentProducts(mainId);
            f.ShowDialog();
        }
    }
}