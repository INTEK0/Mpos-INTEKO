using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Model;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.NKA;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
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
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void fCreditPay_Load(object sender, EventArgs e)
        {
            CreditDataLoad();
        }

        private void CreditDataLoad()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
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
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                gridControl1.DataSource = dt;
                                gridView1.Columns["ID"].Visible = false;
                                gridView1.Columns["product_id"].Visible = false;
                                gridView1.OptionsSelection.MultiSelect = true;
                                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }

        private void GetUnitAndTaxData()
        {
            try
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
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }

        private void PeriodicPayDataLoad()
        {
            try
            {
                gridView2.ClearSelection();
                gridControl2.DataSource = null;

                using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    con.Open();
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

                    using (SqlCommand cmd = new SqlCommand(queryString, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                gridControl2.DataSource = dt;
                                gridView2.Columns["kredit_id"].Visible = false;
                                gridView2.Columns["KREDIT_SATISI_AYLIK_ID"].Visible = false;
                                gridView2.Columns["KONTROL"].Visible = false;
                                gridView2.Columns["longidsana"].Visible = false;
                            }
                        }
                    }
                }


                if (gridView2.Columns.ColumnByFieldName("ÖDƏNİŞ ƏT") == null)
                {
                    AddUnboundColumn();
                    AddRepository();
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }

        private void AddUnboundColumn()
        {
            GridColumn unbColumn = gridView2.Columns.AddField("ÖDƏNİŞ ƏT");
            unbColumn.VisibleIndex = gridView2.Columns.Count;
            unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
        }

        private void AddRepository()
        {
            RepositoryItemButtonEdit edit = new RepositoryItemButtonEdit();
            edit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            edit.ButtonClick += edit_ButtonClick;
            edit.Buttons[0].Caption = "ÖDƏNİŞ ƏT";
            edit.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            gridView2.Columns["ÖDƏNİŞ ƏT"].ColumnEdit = edit;
        }

        void edit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string deger2 = "", deger3 = "", deger4 = "", deger5 = "", deger6 = "", handle = null;
            int[] selectedRows = gridView2.GetSelectedRows();
            foreach (var rowHandle in selectedRows)
            {
                handle = rowHandle.ToString();
                index = rowHandle;
                deger2 = gridView2.GetRowCellValue(rowHandle, "KONTROL").ToString();
                deger3 = gridView2.GetRowCellValue(rowHandle, "KREDIT_SATISI_AYLIK_ID").ToString();
                deger6 = gridView2.GetRowCellValue(rowHandle, "KREDİT (AY)").ToString();
                deger4 = gridView2.GetRowCellValue(rowHandle, "longidsana").ToString();
                deger5 = gridView2.GetRowCellValue(rowHandle, "AYLIQ ÖDƏNİŞ").ToString();
            }

            string deger20 = "";


            if (index > 0)
            {
                deger20 = gridView2.GetRowCellValue(index - 1, "KONTROL").ToString();
            }
            else
            {
                deger20 = "Bos";
            }
            if (deger20 == "0")
            {
                XtraMessageBox.Show("Zəhmət olmasa, əvvəlki ayın ödənişini edin.");
            }
            else
            {
                if (deger2 == "1")
                {
                    XtraMessageBox.Show("Ödəniş əvvəllər edilib. Növbəti ödənişi edin");
                }
                else
                {
                    deger5 = gridView2.GetRowCellValue(Convert.ToInt32(handle), "AYLIQ ÖDƏNİŞ").ToString();
                    double deger51 = Math.Round(Convert.ToDouble(deger5), 2);
                    decimal f = Convert.ToDecimal(deger51);

                    _creditPayData = new CreditPayData
                    {
                        KONTROL = deger2,
                        KREDIT_SATISI_AYLIK_ID = deger3,
                        KREDIT_AY = deger6,
                        longidsana = deger4,
                        AYLIQ_ODENIS = deger5
                    };

                    nagkardkredit nk = new nagkardkredit(f, this);
                    nk.ShowDialog();

                   
                }
            }
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                int paramValue = Convert.ToInt32(dr[0]);
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
                    tCreditBalance.Text = (Convert.ToDouble(tCreditAmount.Text)).ToString();
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

        private void bReport_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl2, $"{tCustomerName.Text} - Aylıq kredit ödəniş hesabatı");
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {

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