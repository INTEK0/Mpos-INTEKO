using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.NKA;
using static DTOs;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fCreditPay : DevExpress.XtraEditors.XtraForm
    {
        private string _unitId, _taxId, _productId, _creditMainId, _customerId;
        private int index = 0;
        private CreditPayData _creditPayData;
        private readonly IpModel _terminal = GetIpModel();

        private class CreditPayData
        {
            public string KONTROL { get; set; }
            public string KREDIT_SATISI_AYLIK_ID { get; set; }
            public string KREDIT_AY { get; set; }
            public string longidsana { get; set; }
            public string AYLIQ_ODENIS { get; set; }
        }

        public fCreditPay()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridPanelText(gridView2);
        }

        private void fCreditPay_Load(object sender, EventArgs e)
        {
            CreditDataLoad();
        }

        private void CreditDataLoad()
        {
            string query = @"SELECT [KREDIT_SATISI_MAIN_ID] ID,
[GAIME_NOMRE] 'MÜQAVİLƏ NÖMRƏSİ',
[ODENILEN_MEBLEG] 'KREDİT MƏBLƏĞİ',
[musteri_id] AS N'MÜŞTƏRİ ID',
[MUSTERI] 'AD SOYAD ATA ADI',
[ZAMIN] 'ZAMIN AD SOYAD',
[personel] 'SATIŞ PERSONEL',
[product_name] 'MƏHSULUN ADI',
[taksit] 'KREDİT MÜDDƏTİ(AY)',
[prd_price] * prd_qty AS 'YEKUN MƏBLƏĞ',
[ayliktutar] 'QRAFİK ÜZRƏ ÖDƏNİŞ',
[prd_price] 'SATIŞ QİYMƏTİ',
ISNULL([prd_price] * prd_qty - ISNULL(ilkinodenis, 0), 0)
-
ISNULL((
  SELECT SUM(ODENILEN_MEBLEG)
  FROM [KREDIT_SATISI_AYLIKODEME]
  WHERE kredit_id = [KREDIT_SATISI_MAIN_ID]
), 0) AS N'QALIQ BORC',
[DATE_] 'MÜQAVİLƏ TARİXİ' ,
(SELECT MAX([DATE2_]) FROM [KREDIT_SATISI_AYLIKODEME] WHERE kredit_id =[KREDIT_SATISI_MAIN_ID]) 'SON ÖDƏNİŞ TARİXİ',
(SELECT SUM(ODENILEN_MEBLEG)  FROM [KREDIT_SATISI_AYLIKODEME] WHERE kredit_id =[KREDIT_SATISI_MAIN_ID]) 'CƏM ÖDƏNİLƏN MƏBLƏĞ',
(SELECT  ODENILEN_MEBLEG FROM [KREDIT_SATISI_AYLIKODEME] WHERE kredit_id =[KREDIT_SATISI_MAIN_ID] AND DATE2_ = 
(SELECT MAX([DATE2_])  FROM [KREDIT_SATISI_AYLIKODEME] WHERE kredit_id =[KREDIT_SATISI_MAIN_ID]) ) 'SON ÖDƏNİŞ MƏBLƏĞİ' ,
[product_id],
ilkinodenis,
prd_qty 'MİQDAR'
FROM [KREDIT_SATISI_MAIN]
WHERE
  (
    ([prd_price] * prd_qty - ISNULL(ilkinodenis, 0)) -
    ISNULL((
      SELECT SUM(ODENILEN_MEBLEG)
      FROM [KREDIT_SATISI_AYLIKODEME]
      WHERE kredit_id = [KREDIT_SATISI_MAIN_ID]
    ), 0)
  ) > 0
ORDER BY KREDIT_SATISI_MAIN_ID DESC";
            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
            gridView1.RefreshData();
        }

        private void GetUnitAndTaxData()
        {
            string query = $@"SELECT  
[VAHID], 
[VERGI_DERECESI] 
FROM  [MAL_ALISI_DETAILS] 
WHERE 
[MAL_ALISI_DETAILS_ID] = {_productId}";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        _unitId = dr["VAHID"].ToString();
                        _taxId = dr["VERGI_DERECESI"].ToString();
                    }
                }
            }
        }

        private void PeriodicPayDataLoad()
        {
            string queryString = $@"SELECT  [KREDIT_SATISI_AYLIK_ID],
[kredit_id],
[taksitno] AS N'KREDİT (AY)',
[DATEODEMEGUNU_] AS  'QRAFİK ÜZRƏ ÖDƏNİŞ TARİXİ',
[ODENILECEK_MEBLEG] AS  'AYLIQ ÖDƏNİŞ' ,
[ODENILEN_MEBLEG] 'ÖDƏNİŞ',
CASE WHEN [ODENILEN_MEBLEG]>=ODENILECEK_MEBLEG THEN 1 ELSE 0 END AS KONTROL,
[longidsana],
[longids]
FROM  [KREDIT_SATISI_AYLIKODEME] 
where kredit_id={_creditMainId}";
            var data = DbProsedures.ConvertToDataTable(queryString);
            gridControl2.DataSource = data;
            gridView2.RefreshData();
        }

        private void bPay_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int[] selectedRows = gridView2.GetSelectedRows();
            if (selectedRows.Length == 0)
                return;

            int rowHandle = selectedRows[0];
            int prevRowHandle = rowHandle - 1;


            string kontrol = gridView2.GetRowCellValue(rowHandle, "KONTROL")?.ToString();
            string kreditId = gridView2.GetRowCellValue(rowHandle, "KREDIT_SATISI_AYLIK_ID")?.ToString();
            string kreditAy = gridView2.GetRowCellValue(rowHandle, "KREDİT (AY)")?.ToString();
            string longId = gridView2.GetRowCellValue(rowHandle, "longidsana")?.ToString();
            string aylikOdenis = gridView2.GetRowCellValue(rowHandle, "AYLIQ ÖDƏNİŞ")?.ToString();


            string prevKontrol = prevRowHandle >= 0
                ? gridView2.GetRowCellValue(prevRowHandle, "KONTROL")?.ToString()
                : "Bos";

            if (prevKontrol == "0")
            {
                XtraMessageBox.Show("Zəhmət olmasa, əvvəlki ayın ödənişini edin.");
                return;
            }

            if (kontrol == "1")
            {
                XtraMessageBox.Show("Ödəniş əvvəllər edilib. Növbəti ödənişi edin");
                return;
            }

            if (!double.TryParse(aylikOdenis, out double parsedAmount))
            {
                XtraMessageBox.Show("Ödəniş məbləği düzgün deyil.");
                return;
            }

            decimal amount = Convert.ToDecimal(Math.Round(parsedAmount, 2));

            _creditPayData = new CreditPayData
            {
                KONTROL = kontrol,
                KREDIT_SATISI_AYLIK_ID = kreditId,
                KREDIT_AY = kreditAy,
                longidsana = longId,
                AYLIQ_ODENIS = aylikOdenis
            };

            fPay pay = new fPay(amount);
            if (pay.ShowDialog() == DialogResult.OK)
            {
                Payment(pay.Result.Total, pay.Result.Cash, pay.Result.Card, pay.Result.IncomingSum);
            }
        }

        private void Payment(decimal Total, decimal Cash, decimal Card, decimal IncomingSum)
        {
            string uuid = Guid.NewGuid().ToString();
            int vatType = Convert.ToInt16(_taxId);
            int quantityType = Convert.ToInt16(_unitId);
            decimal pay = Math.Round(Convert.ToDecimal(_creditPayData.KREDIT_AY) * Convert.ToDecimal(_creditPayData.AYLIQ_ODENIS), 2);
            decimal residue = Convert.ToDecimal(tCreditBalance.Text) - Convert.ToDecimal(tCreditPeriodAmount.Text);
            residue = Math.Max(residue, 0);
            DTOs.CreditPayDto payData = new DTOs.CreditPayDto()
            {
                Url = _terminal.Ip,
                MerchantId = _terminal.MerchantId,
                item = new CreditPayDto.Item
                {
                    Name = tProductName.Text,
                    Code = _productId,
                    Quantity = Convert.ToDecimal(tAmount.Text),
                    SalePrice = Convert.ToDecimal(tSalePrice.Text),
                    quantityType = quantityType,
                    VatType = vatType
                },
                documentUUID = uuid,
                IncomingSum = IncomingSum,
                CashPayment = Cash,
                CardPayment = Card,
                Residue = residue,
                paymentNumber = Convert.ToInt32(_creditPayData.KREDIT_AY),
                CreditContract = tContractNo.Text,
                ParenDocumentId = _creditPayData.longidsana,
                CustomerName = tCustomerName.Text,
                CreditMonthId = Convert.ToInt32(_creditPayData.KREDIT_SATISI_AYLIK_ID),
            };

            switch (_terminal.Model)
            {
                case "1":
                    if (Sunmi.CreditPay(payData))
                    {
                        DbProsedures.InsertCustomerDebt(CustomerDebtType.CreditPay,
                            DateTime.Now,
                            Convert.ToInt32(_customerId),
                            Math.Round(Convert.ToDecimal(_creditPayData.AYLIQ_ODENIS), 2));
                        RefreshData();
                    }
                    break;
                case "2":
                    if (Cash > 0 && Card > 0)
                    {
                        FormHelpers.Alert("Kreditin bir ödənişi yalnız bir növ ödənişlə həyata keçirilə bilər.", MessageType.Warning);
                        break;
                    }

                    if (AzSmart.CreditPay(payData))
                    {
                        DbProsedures.InsertCustomerDebt(CustomerDebtType.CreditPay,
                            DateTime.Now,
                            Convert.ToInt32(_customerId),
                            Math.Round(Convert.ToDecimal(_creditPayData.AYLIQ_ODENIS), 2));
                        RefreshData();
                    }
                    break;
                case "3":
                    if (Omnitech.CreditPay(payData))
                    {
                        DbProsedures.InsertCustomerDebt(CustomerDebtType.CreditPay,
                            DateTime.Now,
                            Convert.ToInt32(_customerId),
                            Math.Round(Convert.ToDecimal(_creditPayData.AYLIQ_ODENIS), 2));
                        RefreshData();

                    }
                    break;
            }
        }

        private void bRefund_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var control = gridView2.GetFocusedRowCellValue("ÖDƏNİŞ").ToString();

            if (string.IsNullOrWhiteSpace(control))
                return;


            if (XtraMessageBox.Show("Ödənişi geri qaytarmaq istədiyinizə əminsiniz ?", "Bildiriş", MessageBoxButtons.YesNo, MessageBoxIcon.Question) is DialogResult.Yes)
            {

                Refund();
            }
        }

        private async void Refund()
        {
            try
            {
                var Id = gridView2.GetFocusedRowCellValue("KREDIT_SATISI_AYLIK_ID").ToString();

                string query = $@"SELECT 
  ka.KREDIT_SATISI_AYLIK_ID as Id, 
  ka.kredit_id as CreditSaleId, 
  km.GAIME_NOMRE as ContractNo, 
  km.MUSTERI as CustomerName, 
  CAST(km.prd_qty as decimal(18, 3)) as Quantity, 
  CAST(km.prd_price as decimal(18, 2)) as SalePrice, 
  CAST(ka.ODENILEN_MEBLEG as decimal(18, 2)) as PayAmount,
  ka.PaymentTypeId,
  md.MEHSUL_ADI as ProductName, 
  md.BARKOD as Barcode, 
  md.VERGI_DERECESI as TaxId, 
  tax.EDV as TaxName, 
  md.VAHID as UnitId, 
  unit.VAHIDLER_NAME as UnitName, 
  ka.longids as LongFiscalId, 
  ka.shortids as ShortFiscalId, 
  ka.ReceiptNo 
FROM 
  KREDIT_SATISI_AYLIKODEME ka 
  INNER JOIN KREDIT_SATISI_MAIN km ON km.KREDIT_SATISI_MAIN_ID = ka.kredit_id 
  INNER JOIN MAL_ALISI_DETAILS md ON md.MAL_ALISI_DETAILS_ID = km.product_id 
  INNER JOIN VERGI_DERECESI tax ON tax.EDV_ID = md.VERGI_DERECESI 
  INNER JOIN VAHIDLER unit ON unit.VAHIDLER_ID = md.VAHID 
WHERE 
  ka.KREDIT_SATISI_AYLIK_ID = {Id}
";

                var sqlData = DbProsedures.ConvertToDataTable(query);
                CreditPayRefundDto data = new CreditPayRefundDto();
                foreach (DataRow row in sqlData.Rows)
                {
                    data.Url = _terminal.Ip;
                    data.MerchantId = _terminal.MerchantId;
                    data.Cashier = _terminal.Cashier;
                    data.item = new CreditPayRefundDto.Item()
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
                    data.PaymentTypeId = Convert.ToInt16(row["PaymentTypeId"].ToString());
                }

                switch (_terminal.Model)
                {
                    case "3":

                        var result = Omnitech.CreditPayRefund(data);
                        if (result.Item1)
                        {
                            await DbProsedures.Insert_CreditPayRefund(new DatabaseClasses.CreditPayRefund()
                            {
                                CreditPayId = Convert.ToInt32(Id),
                                UserId = DbProsedures.GetUser().Id,
                                FiscalId = result.Item2,
                                ReceiptNo = result.Item3
                            });
                            RefreshData();
                        }

                        break;
                }
            }
            catch (Exception e)
            {
                Helpers.Messages.ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
                FormHelpers.Log($"Kredit ödənişinin geri qaytarılması zamanı xəta yarandı. {e.Message}");
            }

        }

        private void RefreshData()
        {
            CreditDataLoad();
            PeriodicPayDataLoad();
            LoadCreditDetailsSelectedRow();
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                _creditMainId = dr["ID"].ToString();
                _customerId = dr["MÜŞTƏRİ ID"].ToString();
                tContractNo.Text = dr["MÜQAVİLƏ NÖMRƏSİ"].ToString();
                tContractDate.Text = dr["MÜQAVİLƏ TARİXİ"].ToString();
                tCustomerName.Text = dr["AD SOYAD ATA ADI"].ToString();
                tProductName.Text = dr["MƏHSULUN ADI"].ToString();
                tCreditPeriod.Text = dr["KREDİT MÜDDƏTİ(AY)"].ToString();
                tCreditPeriodAmount.Text = dr["QRAFİK ÜZRƏ ÖDƏNİŞ"].ToString();
                tLastPayDate.Text = dr["SON ÖDƏNİŞ TARİXİ"].ToString();
                tLastPayAmount.Text = dr["SON ÖDƏNİŞ MƏBLƏĞİ"].ToString();
                tTotalPay.Text = dr["CƏM ÖDƏNİLƏN MƏBLƏĞ"].ToString();
                tDownPayment.Text = dr["ilkinodenis"].ToString();
                tSalePrice.Text = dr["SATIŞ QİYMƏTİ"].ToString();
                tAmount.Text = dr["MİQDAR"].ToString();
                tTotal.Text = dr["YEKUN MƏBLƏĞ"].ToString();
                tCreditAmount.Text = (Convert.ToDouble(tTotal.Text) - Convert.ToDouble(tDownPayment.Text)).ToString();

                if (tTotalPay.Text == "")
                    tCreditBalance.Text = Convert.ToDouble(tCreditAmount.Text).ToString();
                else
                    tCreditBalance.Text = Math.Max((Convert.ToDouble(dr["QALIQ BORC"].ToString())), 0).ToString();

                _productId = dr["product_id"].ToString();
                GetUnitAndTaxData();
                PeriodicPayDataLoad();
            }
        }

        private void LoadCreditDetailsSelectedRow()
        {
            if (!string.IsNullOrWhiteSpace(_creditMainId))
            {
                string query = @"SELECT 
  [KREDIT_SATISI_MAIN_ID] ID, 
  [GAIME_NOMRE] N'MÜQAVİLƏ NÖMRƏSİ', 
  [ODENILEN_MEBLEG] N'KREDİT MƏBLƏĞİ', 
  [musteri_id] AS N'MÜŞTƏRİ ID', 
  [MUSTERI] N'AD SOYAD ATA ADI', 
  [ZAMIN] N'ZAMIN AD SOYAD', 
  [personel] N'SATIŞ PERSONEL', 
  [product_name] N'MƏHSULUN ADI', 
  [taksit] N'KREDİT MÜDDƏTİ(AY)', 
  [prd_price] * prd_qty AS N'YEKUN MƏBLƏĞ', 
  [ayliktutar] N'QRAFİK ÜZRƏ ÖDƏNİŞ', 
  [prd_price] N'SATIŞ QİYMƏTİ',
  ISNULL([prd_price] * prd_qty - ISNULL(ilkinodenis, 0), 0)
  -
  ISNULL((
    SELECT SUM(ODENILEN_MEBLEG)
    FROM [KREDIT_SATISI_AYLIKODEME]
    WHERE kredit_id = [KREDIT_SATISI_MAIN_ID]
  ), 0) AS N'QALIQ BORC',
  [DATE_] N'MÜQAVİLƏ TARİXİ', 
  (
    SELECT 
      MAX([DATE2_]) 
    FROM 
      [KREDIT_SATISI_AYLIKODEME] 
    WHERE 
      kredit_id = [KREDIT_SATISI_MAIN_ID]
  ) N'SON ÖDƏNİŞ TARİXİ', 
  (
    SELECT 
      SUM(ODENILEN_MEBLEG) 
    FROM 
      [KREDIT_SATISI_AYLIKODEME] 
    WHERE 
      kredit_id = [KREDIT_SATISI_MAIN_ID]
  ) N'CƏM ÖDƏNİLƏN MƏBLƏĞ', 
  (
    SELECT 
      ODENILEN_MEBLEG 
    FROM 
      [KREDIT_SATISI_AYLIKODEME] 
    WHERE 
      kredit_id = [KREDIT_SATISI_MAIN_ID] 
      AND DATE2_ = (
        SELECT 
          MAX([DATE2_]) 
        FROM 
          [KREDIT_SATISI_AYLIKODEME] 
        WHERE 
          kredit_id = [KREDIT_SATISI_MAIN_ID]
      )
  ) N'SON ÖDƏNİŞ MƏBLƏĞİ', 
  [product_id], 
  ilkinodenis, 
  prd_qty N'MİQDAR' 
FROM 
  [KREDIT_SATISI_MAIN] 
where 
  [KREDIT_SATISI_MAIN_ID] = @Id";
                using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", _creditMainId);
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            _creditMainId = dr["ID"].ToString();
                            _customerId = dr["MÜŞTƏRİ ID"].ToString();
                            tContractNo.Text = dr["MÜQAVİLƏ NÖMRƏSİ"].ToString();
                            tContractDate.Text = dr["MÜQAVİLƏ TARİXİ"].ToString();
                            tCustomerName.Text = dr["AD SOYAD ATA ADI"].ToString();
                            tProductName.Text = dr["MƏHSULUN ADI"].ToString();
                            tCreditPeriod.Text = dr["KREDİT MÜDDƏTİ(AY)"].ToString();
                            tCreditPeriodAmount.Text = dr["QRAFİK ÜZRƏ ÖDƏNİŞ"].ToString();
                            tLastPayDate.Text = dr["SON ÖDƏNİŞ TARİXİ"].ToString();
                            tLastPayAmount.Text = dr["SON ÖDƏNİŞ MƏBLƏĞİ"].ToString();
                            tTotalPay.Text = dr["CƏM ÖDƏNİLƏN MƏBLƏĞ"].ToString();
                            tDownPayment.Text = dr["ilkinodenis"].ToString();
                            tSalePrice.Text = dr["SATIŞ QİYMƏTİ"].ToString();
                            tAmount.Text = dr["MİQDAR"].ToString();
                            tTotal.Text = dr["YEKUN MƏBLƏĞ"].ToString();
                            tCreditAmount.Text = (Convert.ToDouble(tTotal.Text) - Convert.ToDouble(tDownPayment.Text)).ToString();

                            if (tTotalPay.Text == "")
                                tCreditBalance.Text = Convert.ToDouble(tCreditAmount.Text).ToString();
                            else
                                tCreditBalance.Text = Math.Max((Convert.ToDouble(dr["QALIQ BORC"].ToString())), 0).ToString();

                            _productId = dr["product_id"].ToString();
                        }
                    }
                }
            }
        }

        public void gelen_data_negd_pos(decimal cash_, decimal card_, decimal umumi_mebleg_)
        {
            string casha = cash_.ToString();
            string card = card_.ToString();
            string umumi = umumi_mebleg_.ToString();

            decimal deger51 = Math.Round(Convert.ToDecimal(_creditPayData.AYLIQ_ODENIS), 2);
            double deger9 = Math.Round(Convert.ToDouble(_creditPayData.KREDIT_AY) * Convert.ToDouble(_creditPayData.AYLIQ_ODENIS), 2);

            double degerodenena = Math.Round(Convert.ToDouble(_creditPayData.KREDIT_AY), 2);

            double degeryek = Math.Round(Convert.ToDouble(tTotal.Text), 2);
            double yekunodenens = Convert.ToDouble(tCreditBalance.Text) - deger9;
            double yekunodenens2 = 0;
            if (yekunodenens < 0)
            {
                yekunodenens2 = 0;
            }
            else
            {
                yekunodenens2 = Math.Round(yekunodenens, 2);
            }

            try
            {
                string uuid = Guid.NewGuid().ToString();

                int vatType = Convert.ToInt16(_taxId);
                int quantityType = Convert.ToInt16(_unitId);
                decimal salePrice = Convert.ToDecimal(tSalePrice.Text);




                Sunmi.Item item = new Sunmi.Item()
                {
                    name = tProductName.Text,
                    code = _productId,
                    quantity = 1,
                    salePrice = deger51,
                    realPrice = degeryek,
                    vatType = vatType,
                    quantityType = quantityType
                };

                Sunmi.Data data = new Sunmi.Data()
                {
                    documentUUID = uuid,
                    parentDocumentId = _creditPayData.longidsana,
                    cashPayment = cash_,
                    cardPayment = card_,
                    cashierName = _terminal.Cashier,
                    residue = yekunodenens2,
                    paymentNumber = _creditPayData.KREDIT_AY,
                    creditContract = tContractNo.Text,
                    creditPayer = tCustomerName.Text,
                    clientName = tCustomerName.Text,
                    items = new List<Sunmi.Item> { item }
                };

                Sunmi.RootObject root = new Sunmi.RootObject
                {
                    data = data,
                    operation = "credit"
                };

                //bool result = Sunmi.CreditPay(root, _terminal.Ip, _creditPayData.KREDIT_SATISI_AYLIK_ID);
                //if (result)
                //{
                //    CreditDataLoad();
                //    PeriodicPayDataLoad();
                //    DbProsedures.InsertCustomerDebt(CustomerDebtType.CreditPay, DateTime.Now, Convert.ToInt32(_customerId), deger51);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}