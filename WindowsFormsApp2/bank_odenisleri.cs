using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using DevExpress.Data;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;

namespace WindowsFormsApp2
{
    public partial class bank_odenisleri : DevExpress.XtraEditors.XtraForm
    {
        //BindingList<SupplierPay> debtList = new BindingList<SupplierPay>();
        public static int t_odenis_user_id;
        public bank_odenisleri(int t_user_id)
        {
            InitializeComponent();
            FormHelpers.GridPanelText(gridView1);
            t_odenis_user_id = t_user_id;
        }
        private void bank_odenisleri_Load(object sender, EventArgs e)
        {
            dateEdit1.Text = DateTime.Now.ToShortDateString();
            tProccesNo.Enabled = false;
            tProccesNo.Text = DbProsedures.GET_SupplierDebtPayProccessNo();
            lookUpEdit8GEtData_yeni_anbar();
            radioButton2.Checked = true;

            gridView1.Columns["BORC"].Summary.Add(SummaryItemType.Sum, "BORC");
            gridView1.Columns["ESAS_BORC"].Summary.Add(SummaryItemType.Sum, "ESAS_BORC");
            gridView1.Columns["EDV_BORC"].Summary.Add(SummaryItemType.Sum, "EDV_BORC");
        }

        private void lookUpEdit8GEtData_yeni_anbar()
        {
            string strQuery = @"SELECT 
    c.TECHIZATCI_ID,
    c.SIRKET_ADI AS [TƏCHİZATÇI ADI],
CAST(ISNULL(SUM(CAST(sd.Amount AS DECIMAL(18,2))), 0) AS DECIMAL(18,2)) AS TotalDebt,
ISNULL(SUM(t.QIYMET), 0) AS TotalPurchaseAmount

FROM COMPANY.TECHIZATCI c
LEFT JOIN (
    SELECT 
        M.TECHIZATCI_ID,
        CAST(SUM(MD.ALIS_GIYMETI * MD.MIGDARI) AS DECIMAL(18,2)) AS QIYMET
    FROM MAL_ALISI_MAIN M
    INNER JOIN MAL_ALISI_DETAILS MD 
        ON M.MAL_ALISI_MAIN_ID = MD.MAL_ALISI_MAIN_ID
    GROUP BY M.TECHIZATCI_ID
) t 
    ON t.TECHIZATCI_ID = c.TECHIZATCI_ID

LEFT JOIN COMPANY.SupplierDebt sd 
    ON sd.SupplierId = c.TECHIZATCI_ID
WHERE 
    c.IsDeleted = 0
GROUP BY 
    c.TECHIZATCI_ID,
    c.SIRKET_ADI;";

            var result = DbProsedures.ConvertToDataTable(strQuery);

            FormHelpers.ControlLoad(result, lookUpEdit1, "TƏCHİZATÇI ADI", "TECHIZATCI_ID");
        }

        /// <summary>
        /// Deaktiv edilib
        /// </summary>
        /// <param name="paramValue"></param>
        private async void getsum(int paramValue)
        {
            //var debt = await DbProsedures.GET_SupplierTotalDebt(paramValue);
            //textEdit14.Text = debt.totalAmount.ToString("N2");
            //textEdit2.Text = debt.mainAmount.ToString("N2");
            //textEdit1.Text = debt.taxAmount.ToString("N2");
        }

        private void getall(int paramValue)
        {
            try
            {
                string queryString = @"
SELECT 
    MAL_ALISI_MAIN_ID,
    SupplierDebtId,
    [FAKTURA NÖMRƏ] as ContractNo,
    TARIX,
    CAST(SUM(ISNULL(ESAS_BORC, 0.00)) AS decimal(18, 3)) AS ESAS_BORC,
    CAST(SUM(ISNULL(EDV_BORC, 0.00)) AS decimal(18, 3)) AS EDV_BORC,
    CAST(SUM(ISNULL(BORC, 0.00)) AS decimal(18, 3)) AS BORC,
    0.00 AS payEdv,
    0.00 AS payDebt
FROM (
    SELECT 
        f.MAL_ALISI_MAIN_ID,
        f.SupplierDebtId,
        f.[FAKTURA NÖMRƏ],
        f.TARIX,
        f.QİYMƏT - ISNULL(t.odenis, 0.00) AS BORC,
        ISNULL(f.ESAS_BORC, 0.00) - ISNULL(t.ESAS_BORC_ODENIS, 0.00) AS ESAS_BORC,
        ISNULL(f.VERGI, 0.00) - ISNULL(t.EDV_BORC, 0.00) AS EDV_BORC,
        0 AS 'ÖDƏNİŞ'
    FROM dbo.fn_TECHIZATCI_BORC(@pricePoint) f
    LEFT JOIN (
        SELECT 
            MAL_ALISI_MAIN_ID,
            SupplierDebtId,
            SUM(ISNULL(ESAS_BORC_ODENIS, 0.00)) AS ESAS_BORC_ODENIS,
            SUM(ISNULL(EDV_BORC, 0.00)) AS EDV_BORC,
            SUM(ISNULL(ESAS_BORC_ODENIS, 0.00)) + SUM(ISNULL(EDV_BORC, 0.00)) AS odenis
        FROM TECHIZATCI_ODENIS
        GROUP BY MAL_ALISI_MAIN_ID, SupplierDebtId
    ) t ON (
        (f.MAL_ALISI_MAIN_ID IS NOT NULL AND f.MAL_ALISI_MAIN_ID = t.MAL_ALISI_MAIN_ID)
        OR
        (f.MAL_ALISI_MAIN_ID IS NULL AND f.SupplierDebtId = t.SupplierDebtId)
    )
) o
WHERE BORC > 0.00
GROUP BY 
    MAL_ALISI_MAIN_ID,
    SupplierDebtId,
    [FAKTURA NÖMRƏ],
    TARIX
ORDER BY TARIX;
";
                using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
                using (SqlCommand cmd = new SqlCommand(queryString, connection))
                {
                    cmd.Parameters.AddWithValue("@pricePoint", paramValue);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            gridControl1.DataSource = dt;
                            gridControl1.RefreshDataSource();
                            
                            var mainAmount = Convert.ToDecimal(gridView1.Columns["ESAS_BORC"].SummaryText);
                            decimal taxAmount = Convert.ToDecimal(gridView1.Columns["EDV_BORC"].SummaryText);
                            decimal totalAmount = Convert.ToDecimal(gridView1.Columns["BORC"].SummaryText);
                            textEdit2.Text = mainAmount.ToString("N2");
                            textEdit1.Text = taxAmount.ToString("N2");
                            textEdit14.Text = totalAmount.ToString("N2");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }

        }

        private void lookUpEdit1_TextChanged(object sender, EventArgs e)
        {
            textEdit2.Text = "";
            textEdit1.Text = "";
            textEdit14.Text = "";
            getall(Convert.ToInt32(lookUpEdit1.EditValue));
            getsum(Convert.ToInt32(lookUpEdit1.EditValue));

        }

        public void refresh()
        {
            int a = Convert.ToInt32(lookUpEdit1.EditValue);
            if (a > 0)
            {
                getall(Convert.ToInt32(lookUpEdit1.EditValue));
                getsum(Convert.ToInt32(lookUpEdit1.EditValue));
            }
        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                gridView1.UpdateCurrentRow();
                int[] selectedRows = gridView1.GetSelectedRows();
                if (selectedRows.Length > 0)
                {
                    int insertData = 0;
                    foreach (int item in selectedRows)
                    {
                        var row = gridView1.GetDataRow(item);
                        decimal yekunborc = Convert.ToDecimal(row["payDebt"].ToString());
                        decimal edv = Convert.ToDecimal(row["payEdv"].ToString());

                        decimal odenilen = yekunborc + edv;

                        if (odenilen > 0)
                        {
                            int productMainId = string.IsNullOrWhiteSpace(row[0].ToString()) ? 0 : Convert.ToInt32(row[0].ToString());
                            int supplierDebtId = string.IsNullOrWhiteSpace(row[1].ToString()) ? 0 : Convert.ToInt32(row[1].ToString());
                            int supplierId = Convert.ToInt32(lookUpEdit1.EditValue);
                            int resultId = await DbProsedures.InsertSupplierPay(new DatabaseClasses.SupplierDebtPay
                            {
                                ProductMainId = productMainId,
                                SupplierDebtId = supplierDebtId,
                                SupplierId = supplierId,
                                Pay = odenilen,
                                PaymentType = radio,
                                Comment = memoEdit1.Text.Trim(),
                                PayDate = dateEdit1.DateTime,
                                ProccessNo = tProccesNo.Text,
                                ContractNo = row[2].ToString(), //Alış fakturasının nömrəsi
                                GaimeNo = tContractNo.Text,
                                MainDebtAmount = yekunborc,
                                TaxDebtAmount = edv,
                            });
                            insertData += resultId;
                        }
                        else
                        {
                            FormHelpers.Alert("Ödəniləcək məbləğ daxil edilmədi", Enums.MessageType.Warning);
                            return;
                        }

                        
                        
                    }
                    if (insertData > 0)
                    {
                        FormHelpers.Alert("Ödəniş uğurla tamamlandı", Enums.MessageType.Success);
                        tProccesNo.Clear();
                        tContractNo.Clear();
                        tProccesNo.Text = DbProsedures.GET_SupplierDebtPayProccessNo();
                        getall(Convert.ToInt32(lookUpEdit1.EditValue));
                        getsum(Convert.ToInt32(lookUpEdit1.EditValue));
                        insertData = 0;
                    }
                }
                else
                    FormHelpers.Alert("Seçim edilmədi", Enums.MessageType.Warning);
            }
            /*
            //int conf = 0;

            //foreach (int i in gridView1.GetSelectedRows())
            //{
            //    DataRow row = gridView1.GetDataRow(i);

            //    decimal yekunborc = Convert.ToDecimal(row["YEKUN BORC ÖDƏ"].ToString());
            //    decimal edv = Convert.ToDecimal(row["ƏDV ÖDƏ"].ToString());

            //    decimal odenilen = yekunborc + edv;

            //    int productMainId = string.IsNullOrWhiteSpace(row[0].ToString()) ? 0 : Convert.ToInt32(row[0].ToString());
            //    int supplierDebtId = string.IsNullOrWhiteSpace(row[1].ToString()) ? 0 : Convert.ToInt32(row[1].ToString());
            //    int supplierId = Convert.ToInt32(lookUpEdit1.EditValue);
            //    int resultId = await DbProsedures.InsertSupplierPay(new DatabaseClasses.SupplierDebtPay
            //    {
            //        ProductMainId = productMainId,
            //        SupplierDebtId = supplierDebtId,
            //        SupplierId = supplierId,
            //        Pay = odenilen,
            //        PaymentType = radio,
            //        Comment = memoEdit1.Text.Trim(),
            //        PayDate = dateEdit1.DateTime,
            //        ProccessNo = tProccesNo.Text,
            //        ContractNo = row[2].ToString(), //Alış fakturasının nömrəsi
            //        GaimeNo = tContractNo.Text,
            //        MainDebtAmount = yekunborc,
            //        TaxDebtAmount = edv,
            //    });


            //    conf = conf + resultId;

            //}

            //if (conf > 0)
            //{
            //    FormHelpers.Alert("Ödəniş uğurla tamamlandı", Enums.MessageType.Success);
            //    tProccesNo.Clear();
            //    tContractNo.Clear();
            //    tProccesNo.Text = DbProsedures.GET_SupplierDebtPayProccessNo();
            //    getall(Convert.ToInt32(lookUpEdit1.EditValue));
            //    getsum(Convert.ToInt32(lookUpEdit1.EditValue));
            //    gridControl1.RefreshDataSource();
            //}
            */
        }

        private static string radio = "NAĞD";

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tContractNo.Enabled = true;
            radio = "BANK";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            tContractNo.Enabled = false;
            radio = "NAĞD";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.Alert("Müvəqqəti olaraq deaktiv edilmiştir", Enums.MessageType.Info);
            //FormHelpers.OpenForm<TECHIZATCI_ODENILENLER>(this);
        }

        private class SupplierPay
        {
            public int MAL_ALISI_MAIN_ID { get; set; }
            public int SupplierDebtId { get; set; }
            public string ContractNo { get; set; }
            public DateTime TARIX { get; set; }
            public decimal ESAS_BORC { get; set; }
            public decimal EDV_BORC { get; set; }
            public decimal BORC { get; set; }
            public decimal payEdv { get; set; }
            public decimal payDebt { get; set; }
        }

        private void gridView1_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ErrorText = "Dəstəklənməyən simvol !";
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;
        }

        private void gridView1_ShownEditor(object sender, EventArgs e)
        {
            if (gridView1.ActiveEditor is DevExpress.XtraEditors.TextEdit editor)
            {
                editor.Properties.Mask.EditMask = "N2";
                editor.Properties.Mask.UseMaskAsDisplayFormat = true;

                editor.KeyPress += (s, ke) =>
                {
                    if (ke.KeyChar == '.')
                        ke.KeyChar = ',';
                };
            }
        }
    }
}