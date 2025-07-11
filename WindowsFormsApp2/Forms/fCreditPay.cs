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
        public readonly IpModel _IpModel = FormHelpers.GetIpModel();
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
[DATE_] 'MÜQAVİLƏ TARİXİ' ,
(SELECT MAX([DATE2_]) FROM [KREDIT_SATISI_AYLIKODEME] WHERE kredit_id =[KREDIT_SATISI_MAIN_ID]) 'SON ÖDƏNİŞ TARİXİ',
(SELECT SUM(ODENILEN_MEBLEG)  FROM [KREDIT_SATISI_AYLIKODEME] WHERE kredit_id =[KREDIT_SATISI_MAIN_ID]) 'CƏM ÖDƏNİLƏN MƏBLƏĞ',
(SELECT  ODENILEN_MEBLEG FROM [KREDIT_SATISI_AYLIKODEME] WHERE kredit_id =[KREDIT_SATISI_MAIN_ID] AND DATE2_ = 
(SELECT MAX([DATE2_])  FROM [KREDIT_SATISI_AYLIKODEME] WHERE kredit_id =[KREDIT_SATISI_MAIN_ID]) ) 'SON ÖDƏNİŞ MƏBLƏĞİ' ,
[product_id],
ilkinodenis,
prd_qty 'MİQDAR'
FROM [KREDIT_SATISI_MAIN]";
            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
        }

        private void GetUnitAndTaxData()
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                con.Open();

                string query = $@"SELECT  
[VAHID], 
[VERGI_DERECESI] 
FROM  [MAL_ALISI_DETAILS] 
WHERE 
[MAL_ALISI_DETAILS_ID] = {_productId}";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
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
        }

        private void PeriodicPayDataLoad()
        {
            gridView2.ClearSelection();
            gridControl2.DataSource = null;
            string queryString = $@"SELECT  [KREDIT_SATISI_AYLIK_ID],
[kredit_id],
[taksitno] AS N'KREDİT (AY)',
[DATEODEMEGUNU_] AS  'QRAFİK ÜZRƏ ÖDƏNİŞ TARİXİ',
[ODENILECEK_MEBLEG] AS  'AYLIQ ÖDƏNİŞ' ,
[ODENILEN_MEBLEG] 'ÖDƏNİŞ',
CASE WHEN [ODENILEN_MEBLEG]>=ODENILECEK_MEBLEG THEN 1 ELSE 0 END AS KONTROL,
[longidsana]  
FROM  [KREDIT_SATISI_AYLIKODEME] 
where kredit_id={_creditMainId}";
            var data = DbProsedures.ConvertToDataTable(queryString);
            gridControl2.DataSource = data;
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

















            //string deger2 = "", deger3 = "", deger4 = "", deger5 = "", deger6 = "", handle = null;
            //foreach (var rowHandle in gridView2.GetSelectedRows())
            //{
            //    handle = rowHandle.ToString();
            //    index = rowHandle;
            //    deger2 = gridView2.GetRowCellValue(rowHandle, "KONTROL").ToString();
            //    deger3 = gridView2.GetRowCellValue(rowHandle, "KREDIT_SATISI_AYLIK_ID").ToString();
            //    deger6 = gridView2.GetRowCellValue(rowHandle, "KREDİT (AY)").ToString();
            //    deger4 = gridView2.GetRowCellValue(rowHandle, "longidsana").ToString();
            //    deger5 = gridView2.GetRowCellValue(rowHandle, "AYLIQ ÖDƏNİŞ").ToString();
            //}

            //string deger20 = "";


            //if (index > 0)
            //{
            //    deger20 = gridView2.GetRowCellValue(index - 1, "KONTROL").ToString();
            //}
            //else
            //{
            //    deger20 = "Bos";
            //}
            //if (deger20 == "0")
            //{
            //    XtraMessageBox.Show("Zəhmət olmasa, əvvəlki ayın ödənişini edin.");
            //}
            //else
            //{
            //    if (deger2 == "1")
            //    {
            //        XtraMessageBox.Show("Ödəniş əvvəllər edilib. Növbəti ödənişi edin");
            //    }
            //    else
            //    {
            //        deger5 = gridView2.GetRowCellValue(Convert.ToInt32(handle), "AYLIQ ÖDƏNİŞ").ToString();
            //        double deger51 = Math.Round(Convert.ToDouble(deger5), 2);
            //        decimal f = Convert.ToDecimal(deger51);

            //        _creditPayData = new CreditPayData
            //        {
            //            KONTROL = deger2,
            //            KREDIT_SATISI_AYLIK_ID = deger3,
            //            KREDIT_AY = deger6,
            //            longidsana = deger4,
            //            AYLIQ_ODENIS = deger5
            //        };

            //        //nagkardkredit nk = new nagkardkredit(f, this);
            //        //nk.ShowDialog();

            //        fPay pay = new fPay(f);
            //        if (pay.ShowDialog() is DialogResult.OK)
            //        {
            //            Payment(pay.Result.Total, pay.Result.Cash, pay.Result.Card, pay.Result.IncomingSum);
            //        }
            //    }
            //}
        }

        private void Payment(decimal Total, decimal Cash, decimal Card, decimal IncomingSum)
        {
            //string uuid = Guid.NewGuid().ToString();
            //int vatType = Convert.ToInt16(_taxId);
            //int quantityType = Convert.ToInt16(_unitId);
            //DTOs.CreditPayDto payData = new DTOs.CreditPayDto()
            //{
            //    item = new CreditPayDto.Item
            //    {
            //        Name = tProductName.Text,
            //        Code = _productId,
            //        Quantity = 1,
            //        SalePrice = deger51,
            //        RealPrice = Convert.ToDecimal(degeryek),
            //        quantityType = quantityType,
            //        vatType = vatType
            //    },
            //    documentUUID = uuid,
            //    IncomingSum = IncomingSum,
            //    CashPayment = Cash,
            //    CardPayment = Card,
            //    CreditContract = tContractNo.Text,
            //    ParenDocumentId = _creditPayData.longidsana,
            //    Url = _terminal.Ip,
            //    CustomerName = tCustomerName.Text,

            //};

            //switch (_terminal.Model)
            //{
            //    case "1":
            //        bool SunmiIsSuccess = false;
            //        if (SunmiIsSuccess)
            //        {
            //            CreditDataLoad();
            //            PeriodicPayDataLoad();
            //            //DbProsedures.InsertCustomerDebt(CustomerDebtType.CreditPay, 
            //            //    DateTime.Now, 
            //            //    Convert.ToInt32(_customerId), deger51);
            //        }
            //        break;
            //    case "3":
                    
            //        break;
            //}
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                _creditMainId = dr[0].ToString();
                _customerId = dr["MÜŞTƏRİ ID"].ToString();
                tContractNo.Text = dr["MÜQAVİLƏ NÖMRƏSİ"].ToString();
                tContractDate.Text = dr["MÜQAVİLƏ TARİXİ"].ToString();
                tCustomerName.Text = dr["AD SOYAD ATA ADI"].ToString();
                tProductName.Text = dr["MƏHSULUN ADI"].ToString();
                tCreditAmount.Text = dr["KREDİT MƏBLƏĞİ"].ToString();
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
                {
                    tCreditBalance.Text = Convert.ToDouble(tCreditAmount.Text).ToString();
                }
                else
                {
                    tCreditBalance.Text = (Convert.ToDouble(tCreditAmount.Text) - Convert.ToDouble(tTotalPay.Text)).ToString();
                }
                _productId = dr["product_id"].ToString();
                GetUnitAndTaxData();
                PeriodicPayDataLoad();
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
                    cashierName = _IpModel.Cashier,
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

                bool result = Sunmi.CreditPay(root, _IpModel.Ip, _creditPayData.KREDIT_SATISI_AYLIK_ID);
                if (result)
                {
                    CreditDataLoad();
                    PeriodicPayDataLoad();
                    DbProsedures.InsertCustomerDebt(CustomerDebtType.CreditPay, DateTime.Now, Convert.ToInt32(_customerId), deger51);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}