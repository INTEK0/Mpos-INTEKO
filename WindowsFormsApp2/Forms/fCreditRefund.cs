using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.CacheData;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.NKA;
using static DTOs;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fCreditRefund : DevExpress.XtraEditors.XtraForm
    {
        private readonly IpModel _terminal = UserCacheService.Terminal;

        public fCreditRefund()
        {
            InitializeComponent();
            FormHelpers.GridPanelText(gridView1);
        }

        private enum SearchType
        {
            All,
            ReceiptNo,
            Date
        }

        private void fCreditRefund_Load(object sender, EventArgs e)
        {
            gridControl1.Focus();
            gridView1.Focus();
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            var check = groupControl1.Controls.OfType<CheckEdit>().FirstOrDefault(x => x.Checked);
            if (check == null) { FormHelpers.Alert("Axtarış növü seçilmədi", Enums.MessageType.Warning); return; }

            switch (check.Tag.ToString())
            {
                case "All":
                    DataLoad(SearchType.All);
                    break;
                case "ReceiptNo":
                    DataLoad(SearchType.ReceiptNo);
                    break;
                case "Date":
                    if (string.IsNullOrWhiteSpace(dateEnd.Text))
                    {
                        FormHelpers.Alert("Tarix seçimi edilmədi", Enums.MessageType.Warning);
                        return;
                    }
                    DataLoad(SearchType.Date);
                    break;
            }
        }

        private void DataLoad(SearchType type)
        {
            gridControl1.DataSource = null;
            string query = null;

            switch (type)
            {
                case SearchType.All:
                    query = @"SELECT 
  ks.KREDIT_SATISI_MAIN_ID as Id, 
  ks.EMELIIYYAT_NOMRE as ProccessNo, 
  ks.TARIX as CreditDate, 
  ks.GAIME_NOMRE as ContractNo, 
  ks.MUSTERI as CustomerName, 
  ks.ReceiptNo, 
  ks.longids as FiscalId, 
  ks.personel as Cashier, 
  CAST(ks.ilkinodenis as decimal(18, 2)) as InitialAmount, 
  CAST(ks.prd_qty * ks.prd_price AS DECIMAL(18, 2)) AS Total, 
  CAST(ks.yekun AS decimal(18, 2)) as CreditAmount 
FROM KREDIT_SATISI_MAIN ks
LEFT JOIN KREDIT_SATISI_MAIN_QAYTARMA kr
    ON kr.KreditSatisMainId = ks.KREDIT_SATISI_MAIN_ID
WHERE kr.KreditSatisMainId IS NULL;
";
                    break;
                case SearchType.ReceiptNo:
                    query = $@"SELECT 
  ks.KREDIT_SATISI_MAIN_ID as Id, 
  ks.EMELIIYYAT_NOMRE as ProccessNo, 
  ks.TARIX as CreditDate, 
  ks.GAIME_NOMRE as ContractNo, 
  ks.MUSTERI as CustomerName, 
  ks.ReceiptNo, 
  ks.longids as FiscalId, 
  ks.personel as Cashier, 
  CAST(ks.ilkinodenis as decimal(18, 2)) as InitialAmount, 
  CAST(ks.prd_qty * ks.prd_price AS DECIMAL(18, 2)) AS Total, 
  CAST(ks.yekun AS decimal(18, 2)) as CreditAmount 
FROM KREDIT_SATISI_MAIN ks
LEFT JOIN KREDIT_SATISI_MAIN_QAYTARMA kr
    ON kr.KreditSatisMainId = ks.KREDIT_SATISI_MAIN_ID
WHERE kr.KreditSatisMainId IS NULL AND ks.ReceiptNo = N'{tSearch.Text.Trim()}';
";
                    break;
                case SearchType.Date:
                    string start = dateStart.DateTime.ToString("yyyy-MM-dd");
                    string end = dateEnd.DateTime.ToString("yyyy-MM-dd");
                    query = $@"SELECT 
  ks.KREDIT_SATISI_MAIN_ID as Id, 
  ks.EMELIIYYAT_NOMRE as ProccessNo, 
  ks.TARIX as CreditDate, 
  ks.GAIME_NOMRE as ContractNo, 
  ks.MUSTERI as CustomerName, 
  ks.ReceiptNo, 
  ks.longids as FiscalId, 
  ks.personel as Cashier, 
  CAST(ks.ilkinodenis as decimal(18, 2)) as InitialAmount, 
  CAST(ks.prd_qty * ks.prd_price AS DECIMAL(18, 2)) AS Total, 
  CAST(ks.yekun AS decimal(18, 2)) as CreditAmount 
FROM KREDIT_SATISI_MAIN ks
LEFT JOIN KREDIT_SATISI_MAIN_QAYTARMA kr
    ON kr.KreditSatisMainId = ks.KREDIT_SATISI_MAIN_ID
WHERE kr.KreditSatisMainId IS NULL AND ks.TARIX BETWEEN '{start}' AND '{end}';
";
                    break;
            }

            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
        }

        private void chDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chDate.Checked)
            {
                labelControl1.Visible = true;
                labelControl2.Visible = true;
                dateStart.Visible = true;
                dateEnd.Visible = true;
                tSearch.Visible = false;
                dateStart.DateTime = DateTime.Now;
            }
            else
            {
                labelControl1.Visible = false;
                labelControl2.Visible = false;
                dateStart.Visible = false;
                dateEnd.Visible = false;
                tSearch.Visible = true;
                dateStart.Clear();
                dateEnd.Clear();
                dateStart.DateTime = DateTime.Now;
            }
        }

        private void chAll_CheckedChanged(object sender, EventArgs e)
        {
            tSearch.Enabled = !chAll.Checked;
        }

        private void bRefund_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Refund();
        }

        private void bDetail_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("CreditSaleId"));
            fCreditRefundDetail f = new fCreditRefundDetail(Id);
            f.ShowDialog();
        }

        private async void Refund()
        {
            int Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            string query = $@"SELECT 
  km.KREDIT_SATISI_MAIN_ID as Id, 
  km.GAIME_NOMRE as ContractNo, 
  km.MUSTERI as CustomerName,
  CAST(km.prd_qty as decimal(18, 3)) as Quantity, 
  CAST(km.prd_price as decimal(18, 2)) as SalePrice, 
  CAST(km.ilkinodenis as decimal(18, 2)) as PayAmount, 
  CAST(km.yekun as decimal(18, 2)) as CreditAmount,
  km.ODEME_TIPI as PaymentTypeId,
  md.MEHSUL_ADI as ProductName,
  md.BARKOD as Barcode,
  md.VERGI_DERECESI as TaxId,
  tax.EDV as TaxName,
  md.VAHID as UnitId,
  unit.VAHIDLER_NAME as UnitName,
  km.longids as LongFiscalId,
  km.shortids as ShortFiscalId,
  km.ReceiptNo as ReceiptNo
FROM 
  [KREDIT_SATISI_MAIN] km
INNER JOIN MAL_ALISI_DETAILS md ON md.MAL_ALISI_DETAILS_ID = km.product_id
INNER JOIN VERGI_DERECESI tax ON tax.EDV_ID = md.VERGI_DERECESI
INNER JOIN VAHIDLER unit ON unit.VAHIDLER_ID = md.VAHID
WHERE
  km.KREDIT_SATISI_MAIN_ID = {Id}";

            var sqlData = DbProsedures.ConvertToDataTable(query);
            CreditSaleRefundDto data = new CreditSaleRefundDto();
            foreach (DataRow row in sqlData.Rows)
            {
                data.Url = _terminal.Ip;
                data.MerchantId = _terminal.MerchantId;
                data.Cashier = _terminal.Cashier;
                data.item = new CreditSaleRefundDto.Item()
                {
                    ProductName = row["ProductName"].ToString(),
                    ProductCode = row["Barcode"].ToString(),
                    Quantity = Decimal.Parse(row["Quantity"].ToString()),
                    QuantityType = Convert.ToInt32(row["UnitId"].ToString()),
                    SalePrice = Decimal.Parse(row["SalePrice"].ToString()),
                    VatType = Convert.ToInt32(row["TaxId"].ToString())
                };
                data.Total = Decimal.Parse(row["PayAmount"].ToString());
                data.CustomerName = row["CustomerName"].ToString();
                data.ParentLongFiscalId = row["LongFiscalId"].ToString();
                data.ParentShortFiscalId = row["ShortFiscalId"].ToString();
                data.ParentDocumentNumber = row["ReceiptNo"].ToString();
                data.creditPayment = Decimal.Parse(row["CreditAmount"].ToString());
                data.PaymentTypeId = Convert.ToInt16(row["PaymentTypeId"].ToString());
            }

            switch (_terminal.Model)
            {
                case "1":
                    var resultSunmi = Sunmi.CreditRefund(data);
                    if (resultSunmi.Item1)
                    {
                        await DbProsedures.Insert_CreditSaleRefund(new DatabaseClasses.CreditSaleRefund
                        {
                            CreditSaleId = Id,
                            Comment = "",
                            PaymentTypeId = data.PaymentTypeId,
                            TotalAmount = data.Total,
                            LongFiscalId = resultSunmi.Item2,
                            ReceiptNo = resultSunmi.Item3,
                            UserId = Properties.Settings.Default.UserID,
                        });
                        gridControl1.DataSource = null;
                    }
                    break;
                case "3":
                    var result = Omnitech.CreditRefund(data);
                    if (result.Item1)
                    {
                        await DbProsedures.Insert_CreditSaleRefund(new DatabaseClasses.CreditSaleRefund
                        {
                            CreditSaleId = Id,
                            Comment = "",
                            PaymentTypeId = data.PaymentTypeId,
                            TotalAmount = data.Total,
                            LongFiscalId = result.Item2,
                            ReceiptNo = result.Item3,
                            UserId = Properties.Settings.Default.UserID,
                        });
                        gridControl1.DataSource = null;
                    }
                    break;
            }
        }
    }
}