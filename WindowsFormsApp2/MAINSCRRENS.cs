using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.CodeParser;
using DevExpress.DataAccess.Native.Data;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Localization;
using Licence.Services;
using Microsoft.Win32;
using Newtonsoft.Json;
using WindowsFormsApp2.Forms;
using WindowsFormsApp2.Forms.PrintPages;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.CacheData;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class MAINSCRRENS : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public string productname, productprice, barcodesa;
        public MAINSCRRENS(int xuser)
        {
            InitializeComponent();
            //if (xuser < 1)
            //{
            //    accordionControlElement5.Visible = false;
            //    accordionControlElement6.Visible = false;
            //    accordionControlElement58.Visible = false;
            //    accordionControlElement8.Visible = false;
            //    accordionControlElement9.Visible = false;
            //    accordionControlElement10.Visible = false;
            //    accordionControlElement17.Visible = false;
            //    accordionControlElement28.Visible = false;
            //    accordionControlElement32.Visible = false;
            //    accordionControlElement33.Visible = false;
            //    accordionControlElement35.Visible = false;
            //    accordionControlElement36.Visible = false;
            //    accordionControlElement43.Visible = false;
            //    accordionControlElement47.Visible = false;
            //    accordionControlElement48.Visible = false;
            //    accordionControlElement50.Visible = false;
            //    accordionControlElement54.Visible = false;
            //    tabLog.Visible = false;
            //    tabDatabase.Visible = false;
            //    accordionControlElement54.Visible = false;
            //    chStockAmount.Enabled = false;
            //    chActive.Enabled = false;
            //    chDeactive.Enabled = false;
            //    tabLog.PageEnabled = false;
            //    tabDatabase.PageEnabled = false;
            //}
            GridPanelText(gridProducts);
            GridPanelText(gridLogs);
        }

        private void accordionControlElement13_Click(object sender, EventArgs e)
        {
            FormHelpers.Alert("Bu modul aktiv deyildir. Servis xidmətinə müraciət edin", Enums.MessageType.Warning);
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.Customers)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<fAddCustomer>();
        }

        private void accordionControlElement49_Click(object sender, EventArgs e)
        {
            OpenForm<fAddDoctor>();
        }

        private void accordionControlElement14_Click(object sender, EventArgs e)
        {
            FormHelpers.Alert("Bu modul aktiv deyildir. Servis xidmətinə müraciət edin", Enums.MessageType.Warning);
            //OpenForm<ANBARDAN_ANBARA>();
        }

        private void accordionControlElement15_Click(object sender, EventArgs e)
        {
            FormHelpers.Alert("Bu modul aktiv deyildir. Servis xidmətinə müraciət edin", Enums.MessageType.Warning);
            //OpenForm<ANBARDAN_OBYEKTE>();
        }

        private void accordionControlElement16_Click(object sender, EventArgs e)
        {
            FormHelpers.Alert("Bu modul aktiv deyildir. Servis xidmətinə müraciət edin", Enums.MessageType.Warning);
            //OpenForm<OBYEKTDEN_ANBARA>();
        }

        private void accordionControlElement11_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.BankSale)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<GAIME_SATISI_LAYOUT>(Properties.Settings.Default.UserID, this);
        }

        private void accordionControlElement12_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.BankSale)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<QAIME_SATISI_QAYTARMA_LAYOUT>();
        }

        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
            OpenForm<fAddProduct>();
        }

        private void accordionControlElement9_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.RefundProduct)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<MEHSUL_GAYTARMA_LAYOUT>();
        }

        private void accordionControlElement41_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.PosSale)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }

            POS_LAYOUT_NEW f = Application.OpenForms.OfType<POS_LAYOUT_NEW>().FirstOrDefault();
            if (f != null)
            {
                f.WindowState = FormWindowState.Maximized;
                f.BringToFront();
                f.Activate();
            }
            else
            {
                f = new POS_LAYOUT_NEW();
                f.Show();
            }
        }

        private void accordionControlElement31_Click(object sender, EventArgs e)
        {
            OpenForm<ANBAR_GALIGI>();
        }

        private void accordionControlElement32_Click(object sender, EventArgs e)
        {
            OpenForm<MEHSUL_ALIS_HESABATI>();
        }

        private void accordionControlElement33_Click(object sender, EventArgs e)
        {
            OpenForm<ANBAR_MENFEET>();
        }

        private void accordionControlElement37_Click(object sender, EventArgs e)
        {
            OpenForm<UMUMI_SATIS_HESABATI>();
        }

        private void accordionControlElement38_Click(object sender, EventArgs e)
        {
            OpenForm<BANK_NEGD_HESABAT>();
        }

        private void accordionControlElement40_Click(object sender, EventArgs e)
        {
            OpenForm<fKassalar>();
        }

        private void accordionControlElement48_Click(object sender, EventArgs e)
        {
            EXCELL_IMPORT f = new EXCELL_IMPORT();
            if (f.ShowDialog() is DialogResult.OK)
            {
                lRefresh_Click(null, null);
            }
        }

        private void accordionControlElement50_Click(object sender, EventArgs e)
        {
            OpenForm<fTereziler>();
        }

        private void accordionControlElement52_Click(object sender, EventArgs e)
        {
            OpenForm<BankTTNM>();
        }

        private void accordionControlElement51_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.ScalesProductDownload)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }

            try
            {
                string message = "Excel faylına istəyə görə bütün məhsulları vəya KQ olan məhsulları yazdıra bilərsiniz.\n\n" +
             "Yes/Да - Bütün məhsulları yazdır\n" +
             "No/Нет - Vahidi KQ olan məhsulları yazdır\n" +
             "Cancel/Отмена - Ləğv et";

                DialogResult result = MessageBox.Show(message, "Mesaj", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                string query = string.Empty;

                switch (result)
                {
                    case DialogResult.Yes:
                        query = "exec InsertIntoTerazimalzemeAllProducts";
                        break;
                    case DialogResult.No:
                        query = "exec InsertIntoTerazimalzemeFilteredByVahid";
                        break;
                    default: return;

                }


                using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.ExecuteNonQuery();
                        gridView2.ClearSelection();
                        gridControl2.DataSource = null;
                    }
                }

                var terezi = DbProsedures.GetTerezi();

                if (terezi == null)
                {
                    FormHelpers.Alert("Tərəzi seçimi edilməyib", MessageType.Warning);
                    return;
                }

                string queryString = null;

                switch (terezi.ModelName.Trim())
                {
                    case "Rongta RLS 1100":
                        queryString = @"SELECT  ROW_NUMBER() OVER(ORDER BY [MƏHSUL ADI]) AS Hotkey,
REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE([MƏHSUL ADI],N'Ə','E'),N'ə','e'),N'ı','i'),N'ü','u'),N'ğ','g'),N'Ğ','G' ),N'Ü','U'),N'Ş','S'),N'ş','s'),N'Ç','C'),N'ç','c')  as Name  ,
[MAL_ALISI_DETAILS_ID] as LFCode,
[MAL_ALISI_DETAILS_ID] as Code ,
7 AS [Barcode Type],
CAST([SATIŞ QİYMƏTİ] * 100 AS INT) AS [Unit Price],
4 AS [Unit Weight],
0 AS [Department],
0 AS [Unit Amount] ,
15 AS [Shelf Time],
0 AS [PT Weight],
0 AS [Pack Type],
0 AS [Tare],
0 AS [Error(%)],
0 AS [Message1],
0 AS [Message2],
0 AS [Label],
0 AS [Discount/Table],
0 AS [Account],
0 AS [sPluFieldTitle20],
0 AS [Account],	
0 AS [Recommend days],
0 AS [nutrition],
0 AS [Ice(%)] FROM[terazimalzeme]";
                        break;
                    case "MERC LB 1100":
                        queryString = @"SELECT
[MAL_ALISI_DETAILS_ID], 
REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE([MƏHSUL ADI],N'Ə','E'),N'ə','e'),N'ı','i'),N'ü','u'),N'ğ','g'),N'Ğ','G' ),N'Ü','U'),N'Ş','S'),N'ş','s'),N'Ç','C'),N'ç','c'),
[MAL_ALISI_DETAILS_ID],
[MAL_ALISI_DETAILS_ID],
07,
CAST([SATIŞ QİYMƏTİ] * 100 AS INT) AS SALEPRİCE,
4,
0,
0,
000,
15,
0,
0,
000,
0,
1,
0,
0,
0,
4
FROM[terazimalzeme]";
                        break;
                }

                var data = DbProsedures.ConvertToDataTable(queryString);

                gridControl2.DataSource = data;

                gridView2.OptionsView.ShowColumnHeaders = false;



                if (terezi.ModelName.Trim() is "Rongta RLS 1100")
                {
                    string filePath = string.Empty;
                    if (!string.IsNullOrWhiteSpace(terezi.FilePath))
                    {
                        string directoryPath = Path.GetDirectoryName(terezi.FilePath); //plu.exe ni almadan filePath alır
                        filePath = $@"{directoryPath}\rtPLU_EN.TXP"; //C:\Program Files (x86)\RLS1000\rtPLU_EN.TXP
                    }
                    else
                    {
                        using (SaveFileDialog saveFile = new SaveFileDialog())
                        {
                            saveFile.Filter = "TXP Faylı|*.txp";
                            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            saveFile.OverwritePrompt = true;
                            saveFile.FileName = "rtPLU_EN.TXP";
                            if (saveFile.ShowDialog() is DialogResult.OK)
                            {
                                filePath = saveFile.FileName;
                            }
                        }
                    }

                    using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                    {
                        for (int i = 0; i < gridView2.RowCount; i++)
                        {
                            var values = new List<string>();
                            for (int j = 0; j < gridView2.VisibleColumns.Count; j++)
                            {
                                var value = gridView2.GetRowCellValue(i, gridView2.VisibleColumns[j])?.ToString()?.Trim() ?? "";
                                values.Add(value);
                            }
                            string line = string.Join("\t", values);
                            writer.WriteLine(line);
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(terezi.FilePath))
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Process.Start(terezi.FilePath);
                        Cursor.Current = Cursors.Default;
                    }
                    Alert($"{terezi.ModelName} tərəzisinin məhsulları export edildi", MessageType.Success);
                    #region BEFORE CODE
                    //using (SaveFileDialog saveFile = new SaveFileDialog())
                    //{
                    //    saveFile.Filter = "Excel Faylı|*.xls";
                    //    saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    //    saveFile.OverwritePrompt = true;
                    //    saveFile.FileName = "Terezi_Mehsullar.xls";
                    //    if (saveFile.ShowDialog() is DialogResult.OK)
                    //    {
                    //        gridView2.ExportToCsv(saveFile.FileName, new DevExpress.XtraPrinting.CsvExportOptions { Separator = "\t" });
                    //        Alert($"{terezi.ModelName} tərəzisinin məhsulları export edildi", MessageType.Success);
                    //    }
                    //}
                    #endregion BEFORE CODE
                }
                else
                {
                    string filePath = string.Empty;

                    if (!string.IsNullOrWhiteSpace(terezi.FilePath))
                    {
                        string directoryPath = Path.GetDirectoryName(terezi.FilePath); //plu.exe ni almadan filePath alır
                        string parentPath = Directory.GetParent(directoryPath).FullName; //bin folderindəndə çıxaraq  LB-MNE papkasının içində olur
                        filePath = $@"{parentPath}\demos\PLU.CSV";
                    }
                    else
                    {
                        using (SaveFileDialog saveFile = new SaveFileDialog())
                        {
                            saveFile.Filter = "CSV Faylı|*.csv";
                            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            saveFile.OverwritePrompt = true;
                            saveFile.FileName = "PLU.csv";
                            if (saveFile.ShowDialog() is DialogResult.OK)
                            {
                                filePath = saveFile.FileName;
                            }
                        }
                    }

                    using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                    {
                        for (int i = 0; i < gridView2.RowCount; i++)
                        {
                            var values = new List<string>();
                            for (int j = 0; j < gridView2.VisibleColumns.Count; j++)
                            {
                                var value = gridView2.GetRowCellValue(i, gridView2.VisibleColumns[j])?.ToString()?.Trim() ?? "";
                                values.Add(value);
                            }
                            string line = string.Join(",", values);
                            writer.WriteLine(line);
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(terezi.FilePath))
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Process.Start(terezi.FilePath);
                        Cursor.Current = Cursors.Default;
                    }
                    Alert($"{terezi.ModelName} tərəzisinin məhsulları export edildi", MessageType.Success);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void accordionControlElement67_Click(object sender, EventArgs e)
        {
            fTereziStockPrint f = new fTereziStockPrint();
            f.ShowDialog();
        }

        private void accordionControlElement18_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.Suppliers)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<bank_odenisleri>(UserCacheService.User.Id);
        }

        private void accordionControlElement19_Click(object sender, EventArgs e)
        {
            FormHelpers.Alert("Bu modul aktiv deyildir. Servis xidmətinə müraciət edin", Enums.MessageType.Warning);
        }

        private void accordionControlElement20_Click(object sender, EventArgs e)
        {
            OpenForm<MUSTERI_ODENISLERI>(Properties.Settings.Default.UserID);
        }

        private void accordionControlElement22_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.Customers)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<fAddCustomer>();
        }

        private void accordionControlElement53_Click(object sender, EventArgs e)
        {
            tabPaneSettings.SelectedPage = tabModul;
        }

        private void accordionControlElement23_Click(object sender, EventArgs e)
        {
            OpenForm<fAddGuarantor>();
        }

        private void accordionControlElement24_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.Credit)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<KREDITSATISLAYOUTSA>();
        }

        private void accordionControlElement25_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.Credit)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<fCreditPay>();
        }

        private void accordionControlElement26_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.Report)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<KREDITHESABATI>();
        }

        private void accordionControlElement27_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.Report)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<KREDITODENISHESABAT1>();
        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
            OpenForm<fCompany>();
        }

        private void accordionControlElement42_Click(object sender, EventArgs e)
        {
            OpenForm<MEHSUL_GAYTARMA_HESABAT>();
        }

        private void accordionControlElement43_Click(object sender, EventArgs e)
        {
            OpenForm<TECHIZATCI_ODENISI_HESABATI>();
        }

        private void accordionControlElement44_Click(object sender, EventArgs e)
        {
            OpenForm<IZAHLI_MEHSUL_SATISI>();
        }

        private void accordionControlElement45_Click(object sender, EventArgs e)
        {
            OpenForm<izahli_mehsul_gaytarma>();
        }

        private void accordionControlElement46_Click(object sender, EventArgs e)
        {
            OpenForm<MUSTERI_ODENIS_HESABAT>();
        }

        private void accordionControlElement47_Click(object sender, EventArgs e)
        {
            OpenForm<techizatci_odenisleri_hesabar>();
        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            navigationFrame1.SelectedPage = pageBranch;
            LoadBranches();
            //FormHelpers.Alert("Bu modul aktiv deyildir. Servis xidmətinə müraciət edin", Enums.MessageType.Warning);
            //OpenForm<Magaza>();
        }

        private void accordionControlElement55_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.ProductBarcodePrint)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<fPrintBarcode>();
        }

        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            //switch (e.CloseReason)
            //{
            //    case CloseReason.UserClosing:
            //    case CloseReason.TaskManagerClosing:
            //    case CloseReason.FormOwnerClosing:
            //    case CloseReason.ApplicationExitCall:
            //        FormHelpers.Log("Sistemdən çıxış etdi");
            //        if (Application.OpenForms.Count >= 1)
            //        {
            //            Application.Exit();
            //        }
            //        break;
            //}
        }

        private async void MainScreen_Load(object sender, EventArgs e)
        {
            lMposVersion.Text = Application.ProductVersion;
            chStockAmount.Checked = false;
            ProductNegativeStatus();
            HotSalesShow();
            SendToKassaShow();
            TerminalReceiptPrintShow();
            XPrinterReceiptPrintShow();
            OtherPayShow();
            SuccessMessageVisibleShow();
            Get_StockDecreasingAmountShow();
            ClinicModuleShow();
            SysAdminControl();
            await LicenceCheck();
            if (!UserCacheService.User.UserRole.Report)
            {
                accordionControlElement30.Enabled = false;
            }
            if (!UserCacheService.User.UserRole.Backups)
            {
                tabDatabase.PageVisible = false;
            }
            if (!UserCacheService.User.UserRole.Logs)
            {
                tabLog.PageVisible = false;
            }
        }

        private async Task LicenceCheck()
        {
            lLicenceExpireDate.Text = "-";
            lLicenceExpireDate.ForeColor = Color.Black;
            return;

            var licenceUser = await LicenseService.Instance.RequestKeyControl(LicenseService.Instance.GetLicenceKey());
            if (licenceUser is null)
            {
                lLicenceExpireDate.Text = "-";
                lLicenceExpireDate.ForeColor = Color.Black;
            }
            else
            {
                DateTime expireDate = licenceUser.LicenceExpireDate.Date;
                lLicenceExpireDate.Text = expireDate.ToString("dd.MM.yyyy");

                int daysRemaining = (expireDate - DateTime.Today).Days;

                if (daysRemaining <= 2)
                {
                    lLicenceExpireDate.ForeColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
                }
                else if (daysRemaining <= 5)
                {
                    lLicenceExpireDate.ForeColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
                }
                else
                {
                    lLicenceExpireDate.ForeColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
                }

                lLicenceExpireDate.ToolTip = $"Lisenziyanın bitmə müddətinə {daysRemaining} gün qalıb";
            }
        }

        private async void MAINSCRRENS_Activated(object sender, EventArgs e)
        {
            try
            {
                //StockDecreasingAmountLoad(); //Miqdarı az olan məhsullar
                await TotalSalesInformation(); //Cari satış hesabatı
                await TotalRefundInformation(); //Cari qaytarma hesabatı
                await TotalPurchaseInformation(); //Cari alış hesabatı
                await StockProductsList(); //Anbar qalığı

                BestsellingProducts(); //Ən çox satılan məhsullar
                ExpensesDataLoad(); //Cari xərclər
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DATALOAD_MESSAGE(ex.Message);
            }

        }

        private async void lRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // StockDecreasingAmountLoad(); //Miqdarı az olan məhsullar
                await TotalSalesInformation(); //Cari satış hesabatı
                await TotalRefundInformation(); //Cari qaytarma hesabatı
                await TotalPurchaseInformation(); //Cari alış hesabatı
                await StockProductsList();//Anbar qalığı

                BestsellingProducts(); //Ən çox satılan məhsullar
                ExpensesDataLoad(); //Cari xərclər
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DATALOAD_MESSAGE(ex.Message);
            }
        }

        private void ExpensesDataLoad()
        {


            string query = @"WITH Headers AS (
    SELECT DISTINCT Header FROM IncomeAndExpensesData
)
SELECT 
    h.Header, 
    COALESCE(SUM(i.Amount), 0) AS Amount
FROM Headers h
LEFT JOIN IncomeAndExpensesData i 
    ON h.Header = i.Header 
    AND i.Date = CAST(GETDATE() AS DATE) AND i.Type = 4
GROUP BY h.Header;";

            var data = DbProsedures.ConvertToDataTable(query);
            gridControlExpenses.DataSource = data;
            gridExpenses.ViewCaption = $"XƏRCLƏR - {DateTime.Now.ToString("dd.MM.yyyy")}";
            gridExpenses.OptionsView.ShowFooter = true;
            gridExpenses.Columns["Amount"].Summary.Clear();
            GridColumnSummaryItem summaryItem = new GridColumnSummaryItem
            {
                FieldName = "Amount",
                SummaryType = DevExpress.Data.SummaryItemType.Sum,
                DisplayFormat = "{0:N2}"
            };
            gridExpenses.Columns["Amount"].Summary.Add(summaryItem);

        }

        private void BestsellingProducts(string count = "5")
        {
            gridView1.ViewCaption = $"{DateTime.Now.ToString("MMMM")} ayında ən çox satılan {count} məhsul";
            string query = $@"SELECT TOP {count}
    m.[MEHSUL_ADI] AS ProductName, 
    ISNULL(SUM(t.TotalAmount),0) AS TotalAmount
FROM [dbo].[MAL_ALISI_DETAILS] m
LEFT JOIN 
(
    -- pos_satis_check_details cədvəlindəki datalar
    SELECT [mal_alisi_details_id], SUM(CAST(count_ AS DECIMAL(18, 2))) AS TotalAmount
    FROM [dbo].[pos_satis_check_details] p
    JOIN [dbo].[pos_satis_check_main] pm ON p.[pos_satis_check_main_id] = pm.[pos_satis_check_main_id]
    WHERE MONTH(pm.date_) = MONTH(GETDATE()) 
    AND YEAR(pm.date_) = YEAR(GETDATE())
    GROUP BY [mal_alisi_details_id]

    UNION ALL

    -- GAIME_SATISI_DETAILS cədvəlindəki datalar
    SELECT [MAL_DETAILS_ID] AS mal_alisi_details_id, SUM(CAST(MIGDARI AS DECIMAL(18, 2))) AS TotalAmount
    FROM [dbo].[GAIME_SATISI_DETAILS] g
    WHERE MONTH(g.TARIX) = MONTH(GETDATE()) 
    AND YEAR(g.TARIX) = YEAR(GETDATE())
    GROUP BY [MAL_DETAILS_ID]
) t 
ON m.[MAL_ALISI_DETAILS_ID] = t.[mal_alisi_details_id]
GROUP BY m.[MEHSUL_ADI]
ORDER BY TotalAmount DESC;";
            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
        }

        /// <summary>
        /// CARİ SATIŞ HESABATI
        /// </summary>
        private async Task TotalSalesInformation()
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                await con.OpenAsync();

                string query = $@"SELECT 
    ROUND(SUM(t.TotalSalePrice), 2, 1) AS TotalSalePrice,
    ROUND(SUM(t.SalesCount), 2, 1) AS TotalSalesCount
FROM (
    -- pos_satis_check_details cədvəlindəki datalar
      SELECT 
          ISNULL(SUM(CAST(pm.UMUMI_MEBLEG AS DECIMAL(18, 5))), 0) AS TotalSalePrice,
        ISNULL(COUNT(DISTINCT pm.pos_nomre), 0) AS SalesCount
    FROM [dbo].[pos_satis_check_main] pm
    WHERE CAST(pm.date_ AS DATE) = CAST(GETDATE() AS DATE)

    UNION ALL

    -- GAIME_SATISI_DETAILS cədvəlindəki datalar
    SELECT 
        ISNULL(SUM(CAST(g.YEKUN_MEBLEG AS DECIMAL(18, 5))), 0) AS TotalSalePrice,
       ISNULL(COUNT(DISTINCT g.GAIME_SATISI_DETAILS_ID), 0) AS SalesCount
    FROM [dbo].[GAIME_SATISI_DETAILS] g
    WHERE CAST(g.TARIX AS DATE) = CAST(GETDATE() AS DATE)
) t;
";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        if (await dr.ReadAsync())
                        {
                            lSalePriceTotal.Text = Convert.ToDecimal(dr["TotalSalePrice"]).ToString("C2");
                            lSalesCount.Text = Convert.ToDecimal(dr["TotalSalesCount"]).ToString("N0");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// CARİ QAYTARMA HESABATI
        /// </summary>
        private async Task TotalRefundInformation()
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                await con.OpenAsync();
                string query = $@"SELECT 
                ISNULL(SUM(t.TotalRefundPrice),0) AS TotalRefundPrice,
                ISNULL(SUM(t.RefundCount),0) AS TotalRefundCount
                FROM (
                -- pos_gaytarma_manual cədvəlindəki datalar
                SELECT 
                ISNULL(SUM(CAST(pm.UMUMI_MEBLEG AS DECIMAL(18, 5))), 0) AS TotalRefundPrice,
                ISNULL(COUNT(DISTINCT pg.pos_gaytarma_manual_id), 0) AS RefundCount
                FROM [dbo].pos_gaytarma_manual pg
                INNER JOIN pos_satis_check_main pm ON pm.pos_satis_check_main_id = pg.pos_satis_check_main_id
                WHERE CAST(pg.date_ AS DATE) = CAST(GETDATE() AS DATE)

                UNION ALL
 
                -- gaime_satis_gaytarma cədvəlindəki datalar
                SELECT 
                ISNULL(SUM(CAST(gd.SATIS_GIYMETI AS DECIMAL(18, 2)) * CAST(g.migdar AS DECIMAL(18, 2))),0) AS TotalRefundPrice,
                ISNULL(COUNT(DISTINCT gd.GAIME_SATISI_DETAILS_ID), 0) AS RefundCount
                FROM [dbo].gaime_satis_gaytarma g
                JOIN [dbo].GAIME_SATISI_DETAILS gd ON g.gaime_satis_details_id = gd.GAIME_SATISI_DETAILS_ID
                WHERE CAST(g.tarix_ AS DATE) = CAST(GETDATE() AS DATE)) t;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        if (await dr.ReadAsync())
                        {
                            lRefuntPrice.Text = Convert.ToDecimal(dr["TotalRefundPrice"]).ToString("C2");
                            lRefundCount.Text = dr["TotalRefundCount"].ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// CARİ ALIŞ HESABATI
        /// </summary>
        private async Task TotalPurchaseInformation()
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                await con.OpenAsync();
                string query = $@"SELECT 
                ISNULL(SUM(t.TotalPruchasePrice),0) AS TotalPurchasePrice,
                ISNULL(SUM(t.PurchaseCount),0) AS TotalPurchaseCount
                FROM (
                -- MAL_ALISI_MAIN cədvəlindəki datalar
                SELECT
                ISNULL(SUM(CAST(md.MIGDARI AS DECIMAL(18, 2)) * CAST(md.ALIS_GIYMETI AS DECIMAL(18, 2))),0) AS TotalPruchasePrice,
                ISNULL(SUM(CAST(md.MIGDARI AS DECIMAL(18, 2))),0) AS PurchaseCount
                FROM [dbo].MAL_ALISI_MAIN ma
                JOIN [dbo].MAL_ALISI_DETAILS md ON ma.MAL_ALISI_MAIN_ID = md.MAL_ALISI_MAIN_ID
                WHERE CAST(ma.date_ AS DATE) = CAST(GETDATE() AS DATE)) t;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        if (await dr.ReadAsync())
                        {
                            lPurchaseTotalPrice.Text = Convert.ToDecimal(dr["TotalPurchasePrice"]).ToString("C2");
                            lPurchaseCount.Text = dr["TotalPurchaseCount"].ToString();
                        }
                    }
                }
            }
        }

        private async void chShowStock_CheckedChanged(object sender, EventArgs e)
        {
            CheckButton checkEdit = (CheckButton)sender;
            if (checkEdit.Checked)
            {
                await StockProductsList();
            }
        }

        private void chStockDecreasingAmount_CheckedChanged(object sender, EventArgs e)
        {
            //CheckButton checkEdit = (CheckButton)sender;
            //if (checkEdit.Checked)
            //{
            //    StockDecreasingAmountLoad();
            //}
        }

        private void bGridExcelExport_Click(object sender, EventArgs e)
        {
            if (chShowStock.Checked)
            {
                FormHelpers.ExcelExport(gridControlProducts, "Anbar Qalığı");
            }
        }

        private void accordionControlElement56_Click(object sender, EventArgs e)
        {
            fKassaReport f = new fKassaReport();
            f.ShowDialog();
        }

        private void accordionControlElement59_Click(object sender, EventArgs e)
        {
            navigationFrame1.SelectedPage = pageDashboard;
        }

        private void SysAdminControl()
        {
            if (Properties.Settings.Default.UserID is 0)
            {
                accordionControlElement66.Visible = true;//Printer module
            }
        }


        private void accordionControlElement58_Click(object sender, EventArgs e)
        {
            navigationFrame1.SelectedPage = pageProducts;
        }

        //private void StockProductsList()
        //{
        //    try
        //    {
        //        Cursor.Current = Cursors.WaitCursor;
        //        gridView2.ViewCaption = "Anbar qalığı";
        //        using (SqlConnection con = new SqlConnection())
        //        {
        //            con.ConnectionString = Properties.Settings.Default.SqlCon;
        //            string query = $@"EXEC dbo.gaime_Satis_mal_load;";

        //            using (SqlCommand cmd = new SqlCommand(query, con))
        //            {
        //                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //                {
        //                    DataTable dataTable = new DataTable();
        //                    da.Fill(dataTable);
        //                    gridControl2.DataSource = dataTable;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ReadyMessages.ERROR_DATALOAD_MESSAGE(e.Message);
        //    }

        //}


        /// <summary>
        /// Anbar qalığını göstərilməsi
        /// </summary>
        private async Task StockProductsList()
        {
            Cursor.Current = Cursors.WaitCursor;
            var data = await StockCacheService.LoadStockAsync();
            gridControlProducts.DataSource = data;
            gridProducts.RefreshData();
            lStockCount.Text = gridProducts.DataRowCount.ToString();
            Cursor.Current = Cursors.Default;
        }

        private void accordionControlElement54_Click_1(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.Users)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<fUser>();
        }

        /// <summary>
        /// Miqdarı az olan məhsulları göstərilməsi
        /// </summary>
        private void StockDecreasingAmountLoad()
        {

            //Cursor.Current = Cursors.WaitCursor;
            //if (chStockAmount.Checked)
            //{
            //    gridProducts.ViewCaption = "Miqdarı az olan məhsullar";
            //    using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            //    {
            //        string query = $@"exec [StockDecreasingAmount]";

            //        using (SqlCommand cmd = new SqlCommand(query, con))
            //        {
            //            await con.OpenAsync();

            //            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            //            {
            //                DataTable dataTable = new DataTable();
            //                await Task.Run(() => da.Fill(dataTable));
            //                gridControlProducts.DataSource = dataTable;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    chStockDecreasingAmount.Visible = false;
            //    chShowStock.Dock = DockStyle.Left;
            //    chShowStock.Checked = true;
            //}
            //Cursor.Current = Cursors.Default;
        }



        private void BestSellingProductListCount(object sender, EventArgs e)
        {
            SimpleButton button = (SimpleButton)sender;
            BestsellingProducts(button.Text);
        }


        #region [..SETTINGS..]

        private void tabPane1_SelectedPageChanged(object sender, SelectedPageChangedEventArgs e)
        {
            if (e.Page == tabAllSettings)
            {
                ProductNegativeStatus();
                HotSalesShow();
            }
            else if (e.Page == tabLog)
            {
                dateLogStart.DateTime = DateTime.Now;
                dateLogFinish.DateTime = DateTime.Now;
                LogReport(Convert.ToDateTime(dateLogStart.Text), Convert.ToDateTime(dateLogFinish.Text));
            }
            else if (e.Page == tabLicence)
            {
                lLicenceKey.Text = LicenseService.Instance.GetLicenceKey();
            }
            else if (e.Page == tabModul)
            {

            }
        }

        private void bBackupDownload_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.Backups)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            DbHelpers.DatabaseBackup();
            BackupHistory();
        }

        private void accordionControlElement57_Click(object sender, EventArgs e)
        {
            navigationFrame1.SelectedPage = pageSettings;
            BackupHistory();
        }

        private void BackupHistory()
        {
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Mpos\Backup", "History", null) == null)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("Backup").SetValue("History", "Yoxdur");
            }
            else
            {
                lBackupHistory.Text = Registry.CurrentUser.OpenSubKey("Mpos").OpenSubKey("Backup").GetValue("History").ToString();
            }
        }

        private void bLogExport_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControlLogs, "Arxiv");
        }

        private void bLogSearch_Click(object sender, EventArgs e)
        {
            LogReport(Convert.ToDateTime(dateLogStart.Text), Convert.ToDateTime(dateLogFinish.Text));
        }

        private void LogReport(DateTime start, DateTime end)
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("LogReport", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StartDate", start);
                    cmd.Parameters.AddWithValue("@EndDate", end);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (System.Data.DataTable dataTable = new System.Data.DataTable())
                        {
                            da.Fill(dataTable);
                            gridControlLogs.DataSource = dataTable;
                            //gridView1.Columns["Saat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            //gridView1.Columns["Saat"].DisplayFormat.FormatString = "HH:mm:ss";
                        }
                    }
                }
            }
        }

        private void bLogDelete_Click(object sender, EventArgs e)
        {
            fAdminPassword f = new fAdminPassword();
            if (f.ShowDialog() is DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                    using (SqlCommand cmd = new SqlCommand("TRUNCATE TABLE dbo.Logs", connection))
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        FormHelpers.Alert("Arxiv uğurla təmizləndi", Enums.MessageType.Success);
                        Log("Arxiv məlumatları silindi");
                        LogReport(Convert.ToDateTime(dateLogStart.Text), Convert.ToDateTime(dateLogFinish.Text));
                    }
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void chDeactive_Click(object sender, EventArgs e)
        {
            UpdateProductNegativeStatus(false, "Anbar qalığının mənfiyə doğru azalması deaktiv edildi");
        }

        private void chActive_Click(object sender, EventArgs e)
        {
            UpdateProductNegativeStatus(true, "Anbar qalığının mənfiyə doğru azalması aktiv edildi");
        }

        private void UpdateProductNegativeStatus(bool status, string message)
        {
            int result = DbProsedures.ProductNegativeStatus(status);
            if (result > 0)
            {
                FormHelpers.Log(message);
            }
            else
            {
                FormHelpers.Alert("XƏTA BAŞ VERDİ", Enums.MessageType.Error);
            }
            ProductNegativeStatus();
        }

        private void ProductNegativeStatus()
        {
            var result = DbProsedures.ConvertToDataTable("SELECT STATUS FROM MENFI_AC_BAGLA", CommandType.Text);
            int data = result.Rows[0].Field<int>("STATUS");
            if (data > 0)
            {
                lProductNegativeStatus.Text = "Anbar qalığının mənfiyə doğru azalması aktiv edildi";
                chActive.Checked = true;
            }
            else
            {
                lProductNegativeStatus.Text = "Anbar qalığının mənfiyə doğru azalması deaktiv edildi";
                chDeactive.Checked = true;
            }
            return;
        }

        private void HotSalesShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("HotSalesShow").ToString());
            if (control)
            {
                chHotSales.Checked = true;
            }
            else
            {
                chHotSales.Checked = false;
            }
        }

        private void SendToKassaShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("SendToKassa").ToString());
            if (control)
            {
                chSendToKassa.Checked = true;
                chIsReceipt.Enabled = true;
            }
            else
            {
                chSendToKassa.Checked = false;
                chIsReceipt.Enabled = false;
            }
        }

        private void XPrinterReceiptPrintShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("IsReceipt").ToString());
            if (control)
            {
                chIsReceipt.Checked = true;
            }
            else
            {
                chIsReceipt.Checked = false;
            }
        }

        private void TerminalReceiptPrintShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("TerminalCashierPrint").ToString());
            if (control)
            {
                chTerminalPrintReceipt.Checked = true;
            }
            else
            {
                chTerminalPrintReceipt.Checked = false;
            }
        }

        private void OtherPayShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("OtherPay").ToString());
            if (control)
            {
                chOtherPay.Checked = true;
            }
            else
            {
                chOtherPay.Checked = false;
            }
        }

        private void ClinicModuleShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("ClinicModule").ToString());
            if (control)
            {
                chClinicModul.Checked = true;
                accordionControlElement49.Visible = true;
                accordionControlElement49.VisibleInFooter = true;
            }
            else
            {
                chClinicModul.Checked = false;
                accordionControlElement49.Visible = false;
                accordionControlElement49.VisibleInFooter = false;
            }
        }

        private void SuccessMessageVisibleShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("SuccessMessageVisible").ToString());
            if (control)
            {
                chPosSalesMessage.Checked = true;
            }
            else
            {
                chPosSalesMessage.Checked = false;
            }
        }

        private void chHotSales_CheckedChanged(object sender, EventArgs e)
        {
            if (chHotSales.Checked)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("HotSalesShow", true);
            }
            else
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("HotSalesShow", false);
            }
        }

        private void bKassaAdd_Click(object sender, EventArgs e)
        {
            fKassalar f = new fKassalar();
            f.ShowDialog();
        }

        private void bKassaPing_Click(object sender, EventArgs e)
        {
            var data = GetIpModel();
            Uri uri = new Uri(data.Ip);
            FormHelpers.PingHostAsync(uri.Host);
        }

        private void bTereziAdd_Click(object sender, EventArgs e)
        {
            fTereziler f = new fTereziler();
            f.ShowDialog();
        }

        private void bTereziPing_Click(object sender, EventArgs e)
        {
            var data = GetIpModel();
            Uri uri = new Uri(data.Ip);
            FormHelpers.PingHostAsync(uri.Host);
        }

        private void chSendToKassa_CheckedChanged(object sender, EventArgs e)
        {
            if (chSendToKassa.Checked)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("SendToKassa", true);
                chIsReceipt.Enabled = true;
            }
            else
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("SendToKassa", false);
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("IsReceipt", false);
                chIsReceipt.Enabled = false;
            }
        }

        private async void chStockAmount_CheckedChanged(object sender, EventArgs e)
        {
            await StockProductsList();
            //if (chStockAmount.Checked)
            //{
            //    Registry.CurrentUser.CreateSubKey("Mpos").SetValue("DecreasingAmount", true);

            //}
            //else
            //{
            //    Registry.CurrentUser.CreateSubKey("Mpos").SetValue("DecreasingAmount", false);

            //}
        }

        private void Get_StockDecreasingAmountShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("DecreasingAmount").ToString());
            if (control)
            {
                chStockAmount.Checked = true;
            }
            else
            {
                chStockAmount.Checked = false;
            }

        }

        private void chPosSalesMessage_CheckedChanged(object sender, EventArgs e)
        {
            if (chPosSalesMessage.Checked)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("SuccessMessageVisible", true);
            }
            else
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("SuccessMessageVisible", false);
            }
        }

        private void accordionControlElement60_Click(object sender, EventArgs e)
        {
            OpenForm<MEHSUL_MALIYET_HESABAT>();
        }

        private void chOtherPay_CheckedChanged(object sender, EventArgs e)
        {
            if (chOtherPay.Checked)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("OtherPay", true);
            }
            else
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("OtherPay", false);
            }
        }

        private void accordionControlElement61_Click(object sender, EventArgs e)
        {
            OpenForm<fAvansReport>();
        }

        private void accordionControlElement29_Click(object sender, EventArgs e)
        {
            fAddIncomeAndExpenses f = new fAddIncomeAndExpenses(SelectedDataType.Income, null);
            if (f.ShowDialog() is DialogResult.OK)
            {
                ExpensesDataLoad();
            }
        }

        private void accordionControlElement62_Click(object sender, EventArgs e)
        {
            fAddIncomeAndExpenses f = new fAddIncomeAndExpenses(SelectedDataType.Expense, null);
            if (f.ShowDialog() is DialogResult.OK)
            {
                ExpensesDataLoad();
            }
        }

        private void accordionControlElement63_Click(object sender, EventArgs e)
        {
            OpenForm<fIncomeAndExpensesReport>();
        }

        private void gridExpenses_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button is MouseButtons.Left && e.Clicks is 2)
            {
                if (gridExpenses != null && gridExpenses.FocusedRowHandle >= 0)
                {
                    string Name = gridExpenses.GetFocusedRowCellValue("Header").ToString();
                    fAddIncomeAndExpenses f = new fAddIncomeAndExpenses(SelectedDataType.Expense, Name);
                    if (f.ShowDialog() is DialogResult.OK)
                    {
                        ExpensesDataLoad();
                    };
                }
            }
        }

        private void accordionControlElement65_Click(object sender, EventArgs e)
        {
            OpenForm<fQuickAddProduct>("");
        }

        private void MAINSCRRENS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode is Keys.F12)
            {
                if (!UserCacheService.User.UserRole.ProductAdd)
                {
                    FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                    return;
                }
                OpenForm<fQuickAddProduct>("");
            }
        }

        private void MAINSCRRENS_FormClosed(object sender, FormClosedEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                case CloseReason.TaskManagerClosing:
                case CloseReason.FormOwnerClosing:
                case CloseReason.ApplicationExitCall:
                    FormHelpers.Log("Sistemdən çıxış etdi");
                    foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
                    {
                        form.Close();
                    }

                    Application.Exit();
                    break;
            }
        }

        private void gridProducts_DoubleClick(object sender, EventArgs e)
        {
            if (gridProducts.GetFocusedDataRow() != null)
            {
                string barcode = gridProducts.GetFocusedRowCellValue("MƏHSUL BARKOD").ToString();
                OpenForm<fQuickAddProduct>(barcode);
            }
        }

        private void accordionControlElement66_Click(object sender, EventArgs e)
        {
            OpenForm<fPrinterSettings>();
        }

        private void chIsReceipt_CheckedChanged(object sender, EventArgs e)
        {
            if (chIsReceipt.Checked)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("IsReceipt", true);
            }
            else
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("IsReceipt", false);

            }
        }

        private void navigationFrame1_SelectedPageChanged(object sender, SelectedPageChangedEventArgs e)
        {
            if (navigationFrame1.SelectedPage != pageBranch)
            {
                foreach (var btn in tablePanel2.Controls.OfType<CheckButton>())
                {
                    RemoteDbManager.ClearConnection();
                    btn.GroupIndex = -1;
                    btn.Checked = false;
                    btn.GroupIndex = 1;
                    groupControl5.Text = null;
                    xtraTabControl1.Visible = false;
                }

            }
        }

        private void accordionControlElement68_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.ProductDiscount)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<fDiscountProduct>();

        }

        private void accordionControlElement71_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.Suppliers)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<fAddSupplier>();
        }

        private void accordionControlElement69_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.Suppliers)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<fAddSupplierDebt>();
        }

        private void accordionControlElement70_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.Suppliers)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<bank_odenisleri>(Properties.Settings.Default.UserID);
        }

        private void accordionControlElement72_Click(object sender, EventArgs e)
        {
            OpenForm<fCreditRefund>();
        }

        private void accordionControlElement73_Click(object sender, EventArgs e)
        {
            OpenForm<fCreditRefundReport>();
        }

        private void chTerminalPrintReceipt_CheckedChanged(object sender, EventArgs e)
        {
            if (chTerminalPrintReceipt.Checked)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("TerminalCashierPrint", true);
            }
            else
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("TerminalCashierPrint", false);
            }
        }

        private void chClinicModul_CheckedChanged(object sender, EventArgs e)
        {
            if (chClinicModul.Checked)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("ClinicModule", true);
            }
            else
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("ClinicModule", false);
            }
        }

        #endregion [..SETTINGS..]


        #region [..BRANCHES..]
        private List<BranchesRoot.Branch> _branches;
        private class BranchesRoot
        {
            public List<Branch> Branches { get; set; }

            public class Branch
            {
                public string Name { get; set; }
                public string ConnectionString { get; set; }
            }
        }

        private void LoadBranches()
        {
            string filePath = Path.Combine(Application.StartupPath, "LocalFiles", "Branches.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var root = JsonConvert.DeserializeObject<BranchesRoot>(json);
                _branches = root?.Branches ?? new List<BranchesRoot.Branch>();
            }
            else
                FormHelpers.Alert("Fliallar tapılmadı !", MessageType.Error);
        }

        private async void CheckedBranches(object sender, EventArgs e)
        {
            var check = (CheckButton)sender;
            if (check == null || !check.Checked) return;


            foreach (var c in tablePanel2.Controls.OfType<CheckButton>().Where(x => !x.Checked))
                c.Enabled = false;


            xtraTabControl1.Visible = false;
            xtraTabControl1.SelectedTabPage = xtraTabPage1;

            var branch = _branches.FirstOrDefault(x => x.Name.Equals(check.Text, StringComparison.OrdinalIgnoreCase));
            if (branch != null)
            {
                string con = branch.ConnectionString;
                RemoteDbManager.SetConnectionString(con);

                groupControl5.Text = $"Flial: <b><color=#FF8C00>{branch.Name}</color></b>  -  Status: <b><color=#FF8C00>Qoşulur...</color></b>";

                bool result = await RemoteDbManager.ServerConnectionAsync();
                if (result)
                {
                    groupControl5.Text = $"Flial: <b><color=#FF8C00>{branch.Name}</color></b>  -  Status: <b><color=#018574>Uğurlu</color></b>";
                    xtraTabControl1.Visible = true;
                    //MessageBox.Show($"Seçilen Branch: {branch.Name}\nConnectionString: {con}");
                }
                else
                {
                    groupControl5.Text = $"Flial: <b><color=#FF8C00>{branch.Name}</color></b>  -  Status: <b><color=#E74856>Uğursuz</color></b>";
                    xtraTabControl1.Visible = false;
                }
            }

            foreach (var c in tablePanel2.Controls.OfType<CheckButton>())
                c.Enabled = true;
        }

        private void bBranchAddProduct_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            fAddProduct f = new fAddProduct();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchExcelImport_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            EXCELL_IMPORT f = new EXCELL_IMPORT();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchRefundProduct_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            MEHSUL_GAYTARMA_LAYOUT f = new MEHSUL_GAYTARMA_LAYOUT();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchBankSale_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            GAIME_SATISI_LAYOUT f = new GAIME_SATISI_LAYOUT();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchBankRefund_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            QAIME_SATISI_QAYTARMA_LAYOUT f = new QAIME_SATISI_QAYTARMA_LAYOUT();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchStock_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            ANBAR_GALIGI f = new ANBAR_GALIGI();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchAlisHesabat_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            MEHSUL_ALIS_HESABATI f = new MEHSUL_ALIS_HESABATI();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchAlisQaytarma_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            MEHSUL_GAYTARMA_HESABAT f = new MEHSUL_GAYTARMA_HESABAT();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchUmumiSatis_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            UMUMI_SATIS_HESABATI f = new UMUMI_SATIS_HESABATI();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchSatisNov_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            BANK_NEGD_HESABAT f = new BANK_NEGD_HESABAT();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchIzahliMehsulSatisi_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            IZAHLI_MEHSUL_SATISI f = new IZAHLI_MEHSUL_SATISI();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchIzahliMehsulQaytarma_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            izahli_mehsul_gaytarma f = new izahli_mehsul_gaytarma();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchAvansHesabati_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            fAvansReport f = new fAvansReport();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchCreditSale_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            KREDITHESABATI f = new KREDITHESABATI();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchCreditPay_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            KREDITODENISHESABAT1 f = new KREDITODENISHESABAT1();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchCreditSaleRefund_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            fCreditRefundReport f = new fCreditRefundReport();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchSuppliers_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            fAddSupplier f = new fAddSupplier();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchUsers_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            fUser f = new fUser();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void bBranchTerminal_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            fKassalar f = new fKassalar();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
            f.ShowDialog();
        }

        private void MAINSCRRENS_Shown(object sender, EventArgs e)
        {
            DbHelpers.UseLocalConnection();
        }

        private void bBranchLog_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);

            //f.Show();
            //f.FormClosed += (s, args) =>
            //{
            //    DbHelpers.UseLocalConnection();
            //};
        }

        private void bBranchMinusCountControl_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);

            //f.Show();
            //f.FormClosed += (s, args) =>
            //{
            //    DbHelpers.UseLocalConnection();
            //};
        }
        #endregion [..BRANCHES..]

    }
}