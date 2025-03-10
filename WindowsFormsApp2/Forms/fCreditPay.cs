using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fCreditPay : DevExpress.XtraEditors.XtraForm
    {
        public readonly IpModel _IpModel = FormHelpers.GetIpModel();
        private string _unitName, _taxName, _productId, _CreditMainId;
        private int index = 0;


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

        private void GetUnitData()
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
                                _unitName = dr["VAHID"].ToString();
                                _taxName = dr["VERGI_DERECESI"].ToString();
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
where kredit_id={_CreditMainId}";

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

                    //nagkardkredit nk = new nagkardkredit(f, this);
                    //nk.ShowDialog();
                }
            }
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                int paramValue = Convert.ToInt32(dr[0]);
                _CreditMainId = dr[0].ToString();
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
                GetUnitData();
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


    }
}