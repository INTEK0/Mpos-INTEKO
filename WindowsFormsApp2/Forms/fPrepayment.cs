using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using System;
using System.Linq;
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
            GridLocalizer.Active = new MyGridLocalizer();
            GridPanelText(gridAvans);
        }

        private enum SearchType
        {
            All,
            FiscalID,
            ReceiptNo
        }

        private void fPrepayment_Load(object sender, EventArgs e)
        {

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
customer.AD + ' ' + customer.SOYAD + ' ' + customer.ATAADI as CustomerName
FROM [pos_satis_check_main] psm
INNER JOIN pos_satis_check_details psd ON psd.pos_satis_check_main_id = psm.pos_satis_check_main_id
INNER JOIN MAL_ALISI_DETAILS mad ON mad.MAL_ALISI_DETAILS_ID = psd.mal_alisi_details_id
INNER JOIN MAL_ALISI_MAIN man ON man.MAL_ALISI_MAIN_ID = mad.MAL_ALISI_MAIN_ID
INNER JOIN COMPANY.TECHIZATCI t ON t.TECHIZATCI_ID = man.TECHIZATCI_ID
LEFT JOIN MUSTERILER customer ON customer.MUSTERILER_ID = psm.CustomerId
INNER JOIN userParol u ON u.id = psm.user_id_
WHERE psm.Prepayment IS NOT NULL";
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
customer.AD + ' ' + customer.SOYAD + ' ' + customer.ATAADI as CustomerName
FROM [pos_satis_check_main] psm
INNER JOIN pos_satis_check_details psd ON psd.pos_satis_check_main_id = psm.pos_satis_check_main_id
INNER JOIN MAL_ALISI_DETAILS mad ON mad.MAL_ALISI_DETAILS_ID = psd.mal_alisi_details_id
INNER JOIN MAL_ALISI_MAIN man ON man.MAL_ALISI_MAIN_ID = mad.MAL_ALISI_MAIN_ID
INNER JOIN COMPANY.TECHIZATCI t ON t.TECHIZATCI_ID = man.TECHIZATCI_ID
LEFT JOIN MUSTERILER customer ON customer.MUSTERILER_ID = psm.CustomerId
INNER JOIN userParol u ON u.id = psm.user_id_
WHERE psm.Prepayment IS NOT NULL AND psm.fiscalNum = N'{tSearch.Text.Trim()}'";
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
customer.AD + ' ' + customer.SOYAD + ' ' + customer.ATAADI as CustomerName
FROM [pos_satis_check_main] psm
INNER JOIN pos_satis_check_details psd ON psd.pos_satis_check_main_id = psm.pos_satis_check_main_id
INNER JOIN MAL_ALISI_DETAILS mad ON mad.MAL_ALISI_DETAILS_ID = psd.mal_alisi_details_id
INNER JOIN MAL_ALISI_MAIN man ON man.MAL_ALISI_MAIN_ID = mad.MAL_ALISI_MAIN_ID
INNER JOIN COMPANY.TECHIZATCI t ON t.TECHIZATCI_ID = man.TECHIZATCI_ID
LEFT JOIN MUSTERILER customer ON customer.MUSTERILER_ID = psm.CustomerId
INNER JOIN userParol u ON u.id = psm.user_id_
WHERE psm.Prepayment IS NOT NULL AND psm.pos_nomre = '{tSearch.Text.Trim()}'";
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