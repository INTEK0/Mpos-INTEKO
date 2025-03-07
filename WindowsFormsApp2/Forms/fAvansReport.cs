﻿using System;
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
            [Description("Avans ödənişləri")]
            AvansPay,
            [Description("Avans satışları")]
            AvansSale,
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
            lookReportType.EditValue = ReportType.AvansPay;

            DateTime dateTime = DateTime.UtcNow.Date;

            dateStart.Text = dateTime.ToShortDateString();
            dateFinish.Text = dateTime.ToShortDateString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var data = (ReportType)lookReportType.EditValue;
            switch (data)
            {
                case ReportType.AvansSale:
                    AvansSaleReport(dateStart.DateTime, dateFinish.DateTime);
                    break;
                case ReportType.AvansPay:
                    AvansPayReport(dateStart.DateTime, dateFinish.DateTime);
                    break;
            }
        }

        private void AvansSaleReport(DateTime start, DateTime finish)
        {
            string query = $@"SELECT 
psm.pos_satis_check_main_id as Id, 
psm.pos_nomre as pos_nomre, 
psm.PREfiscal_id as fiscalId, 
psm.PREdate_ as SaleDate,
u.AD as Username, 
psm.Prepayment,
psd.satis_giymet as SalePrice,
psd.count_ as Amount,
t.SIRKET_ADI as SupplierName,
mad.MEHSUL_ADI as ProductName,
mad.BARKOD as Barcode,
customer.AD + ' ' + customer.SOYAD + ' ' + customer.ATAADI as CustomerName,
case
when (psm.NEGD_>0.00 and psm.KART_ <=0.00) then N'NAĞD' 
when (psm.KART_>0.00 and psm.NEGD_< =0.00) then N'KART'
ELSE N'NAĞD-KART' END AS PayType
FROM [pos_satis_check_main] psm
INNER JOIN pos_satis_check_details psd ON psd.pos_satis_check_main_id = psm.pos_satis_check_main_id
INNER JOIN MAL_ALISI_DETAILS mad ON mad.MAL_ALISI_DETAILS_ID = psd.mal_alisi_details_id
INNER JOIN MAL_ALISI_MAIN man ON man.MAL_ALISI_MAIN_ID = mad.MAL_ALISI_MAIN_ID
INNER JOIN COMPANY.TECHIZATCI t ON t.TECHIZATCI_ID = man.TECHIZATCI_ID
LEFT JOIN MUSTERILER customer ON customer.MUSTERILER_ID = psm.CustomerId
INNER JOIN userParol u ON u.id = psm.user_id_
WHERE psm.Prepayment IS NOT NULL AND psm.PREfiscal_id IS NOT NULL
AND CAST(psm.PREdate_ AS DATE) BETWEEN '{start.ToString("yyyy-MM-dd")}' AND '{finish.AddDays(1).ToString("yyyy-MM-dd")}';";
            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
        }

        private void AvansPayReport(DateTime start, DateTime finish)
        {
            string query = $@"SELECT 
psm.pos_satis_check_main_id as Id, 
psm.pos_nomre as pos_nomre, 
psm.fiscal_id as fiscalId, 
psm.date_ as PayDate, 
u.AD as Username, 
psm.Prepayment,
psd.satis_giymet as SalePrice,
psd.count_ as Amount,
t.SIRKET_ADI as SupplierName,
mad.MEHSUL_ADI as ProductName,
mad.BARKOD as Barcode,
customer.AD + ' ' + customer.SOYAD + ' ' + customer.ATAADI as CustomerName,
case
when (psm.NEGD_>0.00 and psm.KART_ <=0.00) then N'NAĞD' 
when (psm.KART_>0.00 and psm.NEGD_< =0.00) then N'KART'
ELSE N'NAĞD-KART' END AS PayType
FROM [pos_satis_check_main] psm
INNER JOIN pos_satis_check_details psd ON psd.pos_satis_check_main_id = psm.pos_satis_check_main_id
INNER JOIN MAL_ALISI_DETAILS mad ON mad.MAL_ALISI_DETAILS_ID = psd.mal_alisi_details_id
INNER JOIN MAL_ALISI_MAIN man ON man.MAL_ALISI_MAIN_ID = mad.MAL_ALISI_MAIN_ID
INNER JOIN COMPANY.TECHIZATCI t ON t.TECHIZATCI_ID = man.TECHIZATCI_ID
LEFT JOIN MUSTERILER customer ON customer.MUSTERILER_ID = psm.CustomerId
INNER JOIN userParol u ON u.id = psm.user_id_
WHERE psm.Prepayment IS NOT NULL AND psm.PREfiscal_id IS NULL
AND CAST(psm.date_ AS DATE) BETWEEN '{start.ToString("yyyy-MM-dd")}' AND '{finish.AddDays(1).ToString("yyyy-MM-dd")}';";
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
            else if (data is ReportType.AvansPay)
            {
                gridControl1.MainView = gridPay;
            }

            simpleButton1_Click(null, null);
        }
    }
}