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
using DevExpress.XtraGrid.Localization;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fAvansReport : DevExpress.XtraEditors.XtraForm
    {
        public fAvansReport()
        {
            InitializeComponent();
            GridPanelText(gridSale);
            GridPanelText(gridPay);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private enum ReportType
        {
            [Description("Avans satışları")]
            AvansSale,
            [Description("Avans ödənişləri")]
            AvansPay
        }

        private void fAvansReport_Load(object sender, EventArgs e)
        {
            var data = Enum.GetValues(typeof(ReportType))
                                                .Cast<ReportType>()
                                                .Select(x => new
                                                {
                                                    Key = (int)x,
                                                    Value = GetEnumDescription(x)
                                                })
                                                .ToList();

            lookReportType.Properties.DataSource = data;
            lookReportType.Properties.DisplayMember = "Value";
            lookReportType.Properties.ValueMember = "Key";
            lookReportType.Properties.PopulateColumns();
            lookReportType.Properties.Columns["Key"].Visible = false;
            lookReportType.EditValue = ReportType.AvansSale;

            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var data = (ReportType)lookReportType.EditValue;
            switch (data)
            {
                case ReportType.AvansSale:
                    AvansSaleReport();
                    break;
                case ReportType.AvansPay:
                    AvansPayReport();
                    break;
            }
        }

        private void AvansSaleReport()
        {
            string query = @"SELECT 
psm.pos_satis_check_main_id as Id, 
psm.pos_nomre as pos_nomre, 
psm.fiscal_id as fiscalId, 
psm.date_ as SaleDate, 
u.AD as Username, 
psm.Prepayment,
psd.satis_giymet as SalePrice,
psd.count_ as Amount,
mad.MEHSUL_ADI as ProductName,
mad.BARKOD as Barcode
FROM [pos_satis_check_main] psm
INNER JOIN pos_satis_check_details psd ON psd.pos_satis_check_main_id = psm.pos_satis_check_main_id
INNER JOIN MAL_ALISI_DETAILS mad ON mad.MAL_ALISI_DETAILS_ID = psd.mal_alisi_details_id
INNER JOIN userParol u ON u.id = psm.user_id_
WHERE psm.Prepayment IS NOT NULL AND psm.PREfiscal_id IS NULL";
            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
        }

        private void AvansPayReport()
        {
            string query = @"SELECT 
psm.pos_satis_check_main_id as Id, 
psm.pos_nomre as pos_nomre, 
psm.fiscal_id as fiscalId, 
psm.date_ as SaleDate, 
u.AD as Username, 
psm.Prepayment,
psd.satis_giymet as SalePrice,
psd.count_ as Amount,
mad.MEHSUL_ADI as ProductName,
mad.BARKOD as Barcode
FROM [pos_satis_check_main] psm
INNER JOIN pos_satis_check_details psd ON psd.pos_satis_check_main_id = psm.pos_satis_check_main_id
INNER JOIN MAL_ALISI_DETAILS mad ON mad.MAL_ALISI_DETAILS_ID = psd.mal_alisi_details_id
INNER JOIN userParol u ON u.id = psm.user_id_
WHERE psm.Prepayment IS NOT NULL AND psm.PREfiscal_id IS NOT NULL";
            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var data = (ReportType)lookReportType.EditValue;
            switch (data)
            {
                case ReportType.AvansSale:
                    FormHelpers.ExcelExport(gridControl1, "Avans satışı hesabatı");
                    break;
                case ReportType.AvansPay:
                    FormHelpers.ExcelExport(gridControl1, "Avans ödənişi hesabatı");
                    break;
            }           
        }

        private void lookReportType_EditValueChanged(object sender, EventArgs e)
        {
            var data = (ReportType)lookReportType.EditValue;
            if (data is ReportType.AvansSale)
            {
                gridControl1.MainView = gridSale;
            }
            else if(data is ReportType.AvansPay)
            {
                gridControl1.MainView = gridPay;
            }

            simpleButton1_Click(null,null);
        }
    }
}