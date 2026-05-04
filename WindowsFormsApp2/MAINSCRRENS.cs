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
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using Licence.Services;
using Microsoft.Win32;
using Newtonsoft.Json;
using WindowsFormsApp2.App.Application;
using WindowsFormsApp2.Forms;
using WindowsFormsApp2.Forms.PrintPages;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.CacheData;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.App.Helpers.Enums;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.Helpers.FormHelpers;
using DbHelpers = WindowsFormsApp2.Helpers.DB.DbHelpers;
using Enums = WindowsFormsApp2.Helpers.Enums;

namespace WindowsFormsApp2
{
    public partial class MAINSCRRENS : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public string productname, productprice, barcodesa;
        private string RrnFilePath => Path.Combine(Application.StartupPath, "BankTTNM.txt");
        public MAINSCRRENS(int xuser)
        {
            InitializeComponent();
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
            OpenForm<GAIME_SATISI_LAYOUT>();
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
            //OpenForm<ANBAR_MENFEET>();
            OpenForm<fSaleProfitReport>();
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

        private void accordionControlElement51_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.ScalesProductDownload)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }

            try
            {
                string message = "Tərəziyə istəyə görə bütün məhsulları vəya Çəki məhsullarını yazdıra bilərsiniz.\n\n" +
             "Bəli - Bütün məhsulları yazdır\n" +
             "Xeyr - Çəki məhsullarını yazdır\n";

                MessageBoxManager.Register();
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
                MessageBoxManager.Unregister();


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
            ProductNegativeStatus();
            HotSalesShow();
            SendToKassaShow();
            TerminalReceiptPrintShow();
            XPrinterReceiptPrintShow();
            OtherPayShow();
            SuccessMessageVisibleShow();
            CloudAppShow();
            BranchShow();
            ClinicModuleShow();
            SysAdminControl();
            await LicenceCheck();
            if (UserCacheService.User.Id == 0)
                return;
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
            //lLicenceExpireDate.Text = "-";
            //lLicenceExpireDate.ForeColor = Color.Black;
            //return;

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
                lExpireDate.Text = expireDate.ToString("dd.MM.yyyy");

                int daysRemaining = (expireDate - DateTime.Today).Days;

                if (daysRemaining <= 2)
                    lLicenceExpireDate.ForeColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
                else if (daysRemaining <= 5)
                    lLicenceExpireDate.ForeColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
                else
                    lLicenceExpireDate.ForeColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;

                lLicenceExpireDate.ToolTip = $"Lisenziyanın bitmə müddətinə {daysRemaining} gün qalıb";
                lExpireDate.ToolTip = $"Lisenziyanın bitmə müddətinə {daysRemaining} gün qalıb";



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

                BestsellingProducts(); //Ən çox satılan məhsullar
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
                await TotalSalesInformation(); //Cari satış hesabatı
                await TotalRefundInformation(); //Cari qaytarma hesabatı
                await TotalPurchaseInformation(); //Cari alış hesabatı
                await MonthEarningLoadAsync(); //Aylıq satış qrafikası
                await SalesTypeLoadAsync();//Cari satış növ qrafikası
                await StockCountAsync();//Cari məhsul sayı

                BestsellingProducts(); //Ən çox satılan məhsullar
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DATALOAD_MESSAGE(ex.Message);
            }
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

    
    UNION ALL

    -- KREDIT_SATIS_MAIN
    SELECT 
        ISNULL(SUM(CAST(k.prd_qty * k.prd_price AS DECIMAL(18, 5))), 0) AS TotalSalePrice,
        ISNULL(COUNT(DISTINCT k.KREDIT_SATISI_MAIN_ID), 0) AS SalesCount
    FROM [dbo].KREDIT_SATISI_MAIN k
    WHERE CAST(k.TARIX AS DATE) = CAST(GETDATE() AS DATE)
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
            string query = $@"SELECT 
                ISNULL(SUM(t.TotalPruchasePrice),0) AS TotalPurchasePrice,
                ISNULL(SUM(t.PurchaseCount),0) AS TotalPurchaseCount
                FROM (
                -- MAL_ALISI_MAIN cədvəlindəki datalar
                SELECT
                ISNULL(SUM(CAST(md.MIGDARI AS DECIMAL(18, 2)) * CAST(md.ALIS_GIYMETI AS DECIMAL(18, 2))),0) AS TotalPruchasePrice,
                COUNT(md.MAL_ALISI_DETAILS_ID) AS PurchaseCount
                FROM [dbo].MAL_ALISI_MAIN ma
                JOIN [dbo].MAL_ALISI_DETAILS md ON ma.MAL_ALISI_MAIN_ID = md.MAL_ALISI_MAIN_ID
                WHERE CAST(ma.date_ AS DATE) = CAST(GETDATE() AS DATE)) t;";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                await con.OpenAsync();

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

        private void accordionControlElement54_Click_1(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.Users)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            OpenForm<fUser>();
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
            DbBackupSettingsLoad();
            RrnLoadStatus();
        }

        private void BackupHistory()
        {
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Mpos\Backup", "History", null) == null)
                Registry.CurrentUser.CreateSubKey("Mpos")?.CreateSubKey("Backup")?.SetValue("History", "Yoxdur");
            else
                lBackupHistory.Text = Registry.CurrentUser.OpenSubKey("Mpos")?.OpenSubKey("Backup")?.GetValue("History").ToString();
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
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("LogReport", con))
            {
                con.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", start);
                cmd.Parameters.AddWithValue("@EndDate", end);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                using (DataTable dataTable = new DataTable())
                {
                    da.Fill(dataTable);
                    gridControlLogs.DataSource = dataTable;
                    //gridView1.Columns["Saat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    //gridView1.Columns["Saat"].DisplayFormat.FormatString = "HH:mm:ss";
                }
            }
        }

        private void bLogDelete_Click(object sender, EventArgs e)
        {
            fAdminPassword f = new fAdminPassword();
            if (f.ShowDialog() is DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
                using (SqlCommand cmd = new SqlCommand("TRUNCATE TABLE dbo.Logs", connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    FormHelpers.Alert("Arxiv uğurla təmizləndi", Enums.MessageType.Success);
                    Log("Arxiv məlumatları silindi");
                    LogReport(Convert.ToDateTime(dateLogStart.Text), Convert.ToDateTime(dateLogFinish.Text));
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
                FormHelpers.Log(message);
            else
                FormHelpers.Alert("XƏTA BAŞ VERDİ", Enums.MessageType.Error);

            ProductNegativeStatus();
        }

        private void ProductNegativeStatus()
        {
            var result = DbProsedures.ConvertToDataTable("SELECT STATUS FROM MENFI_AC_BAGLA");
            int data = result.Rows[0].Field<int>("STATUS");
            if (data > 0)
                chActive.Checked = true;
            else
                chDeactive.Checked = true;
        }

        private void HotSalesShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos")?.GetValue("HotSalesShow").ToString());
            if (control)
                chHotSales.Checked = true;
            else
                chHotSales.Checked = false;
        }

        private void SendToKassaShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos")?.GetValue("SendToKassa").ToString());
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
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos")?.GetValue("IsReceipt").ToString());

            if (control)
                chIsReceipt.Checked = true;
            else
                chIsReceipt.Checked = false;
        }

        private void TerminalReceiptPrintShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos")?.GetValue("TerminalCashierPrint").ToString());
            if (control)
                chTerminalPrintReceipt.Checked = true;
            else
                chTerminalPrintReceipt.Checked = false;
        }

        private void OtherPayShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos")?.GetValue("OtherPay").ToString());
            if (control)
                chOtherPay.Checked = true;
            else
                chOtherPay.Checked = false;
        }

        private void ClinicModuleShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos")?.GetValue("ClinicModule").ToString());
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

        private void CloudAppShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos")?.GetValue("CloudApp").ToString());
            if (control)
            {
                var data = Enum.GetValues(typeof(ApiOperation))
                    .Cast<ApiOperation>()
                    .Select(x => new
                    {
                        Value = GetEnumDescription(x)
                    })
                    .ToList();

                lookCloudReport.Enabled = true;
                lookCloudReport.Properties.DataSource = data;
                lookCloudReport.Properties.DisplayMember = "Value";
                lookCloudReport.Properties.ForceInitialize();
                chCloud.Checked = true;
            }
            else
            {
                chCloud.Checked = false;
                lookCloudReport.Enabled = false;
                lookCloudReport.EditValue = null;
            }
            chCloud.Refresh();
        }

        private void BranchShow()
        {
            bool isBranch = false;

            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos")?.GetValue("Branch") ?? false);

            if (control)
                isBranch = true;

            accordionControlElement3.Visible = isBranch;
            chBranch.Checked = isBranch;

            chBranch.Refresh();
        }

        private void SuccessMessageVisibleShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("SuccessMessageVisible").ToString());
            if (control)
                chPosSalesMessage.Checked = true;
            else
                chPosSalesMessage.Checked = false;
        }

        private void chHotSales_CheckedChanged(object sender, EventArgs e)
        {
            if (chHotSales.Checked)
                Registry.CurrentUser.CreateSubKey("Mpos")?.SetValue("HotSalesShow", true);
            else
                Registry.CurrentUser.CreateSubKey("Mpos")?.SetValue("HotSalesShow", false);
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

        private void chPosSalesMessage_CheckedChanged(object sender, EventArgs e)
        {
            if (chPosSalesMessage.Checked)
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("SuccessMessageVisible", true);
            else
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("SuccessMessageVisible", false);
        }

        private void accordionControlElement60_Click(object sender, EventArgs e)
        {
            OpenForm<MEHSUL_MALIYET_HESABAT>();
        }

        private void chOtherPay_CheckedChanged(object sender, EventArgs e)
        {
            if (chOtherPay.Checked)
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("OtherPay", true);
            else
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("OtherPay", false);
        }

        private void accordionControlElement61_Click(object sender, EventArgs e)
        {
            OpenForm<fAvansReport>();
        }

        private void accordionControlElement29_Click(object sender, EventArgs e)
        {
            fAddIncomeAndExpenses f = new fAddIncomeAndExpenses(SelectedDataType.Income, null);
            f.ShowDialog();
        }

        private void accordionControlElement62_Click(object sender, EventArgs e)
        {
            fAddIncomeAndExpenses f = new fAddIncomeAndExpenses(SelectedDataType.Expense, null);
            f.ShowDialog();
        }

        private void accordionControlElement63_Click(object sender, EventArgs e)
        {
            OpenForm<fIncomeAndExpensesReport>();
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

        private void accordionControlElement66_Click(object sender, EventArgs e)
        {
            OpenForm<fPrinterSettings>();
        }

        private void chIsReceipt_CheckedChanged(object sender, EventArgs e)
        {
            if (chIsReceipt.Checked)
                Registry.CurrentUser.CreateSubKey("Mpos")?.SetValue("IsReceipt", true);
            else
                Registry.CurrentUser.CreateSubKey("Mpos")?.SetValue("IsReceipt", false);
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
                Registry.CurrentUser.CreateSubKey("Mpos")?.SetValue("TerminalCashierPrint", true);
            else
                Registry.CurrentUser.CreateSubKey("Mpos")?.SetValue("TerminalCashierPrint", false);
        }

        private void chClinicModul_CheckedChanged(object sender, EventArgs e)
        {
            if (chClinicModul.Checked)
                Registry.CurrentUser.CreateSubKey("Mpos")?.SetValue("ClinicModule", true);
            else
                Registry.CurrentUser.CreateSubKey("Mpos")?.SetValue("ClinicModule", false);
        }


        #region [..CLOUD..]

        private void chCloud_Click(object sender, EventArgs e)
        {
            if (!chCloud.Checked)
            {
                fAdminPassword f = new fAdminPassword();
                if (f.ShowDialog() is DialogResult.OK)
                    Registry.CurrentUser.CreateSubKey("Mpos")?.SetValue("CloudApp", true);
                else
                {
                    chCloud.Checked = false;
                    Registry.CurrentUser.CreateSubKey("Mpos")?.SetValue("CloudApp", false);
                    lookCloudReport.Enabled = false;
                    lookCloudReport.EditValue = null;
                }
            }
            else
            {
                chCloud.Checked = false;
                Registry.CurrentUser.CreateSubKey("Mpos")?.SetValue("CloudApp", false);
                lookCloudReport.Enabled = false;
                lookCloudReport.EditValue = null;
            }
            CloudAppShow();
        }

        private void lookCloudReport_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button?.Tag?.ToString() is "CloudImport" && !string.IsNullOrWhiteSpace(lookCloudReport.Text))
            {
                var facade = new SyncFacade();
                Task.Run(async () =>
                {
                    switch (lookCloudReport.Text)
                    {
                        case "Məhsul alışı hesabatı":
                            var resultInvoice = await facade.SendInvoicesAsync(true);
                            if (resultInvoice.Ok)
                                XtraMessageBox.Show($"{lookCloudReport.Text} clouda əlavə edildi", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                XtraMessageBox.Show(resultInvoice.Error, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "Anbar qalığı hesabatı":
                            var resultStock = await facade.SendStockAsync();
                            if (resultStock.Ok)
                                XtraMessageBox.Show($"{lookCloudReport.Text} clouda əlavə edildi", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                XtraMessageBox.Show(resultStock.Error, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error); break;
                        case "Mənfəət hesabatı":
                            var resultProfit = await facade.SendProfitAsync(true);
                            if (resultProfit.Ok)
                                XtraMessageBox.Show($"{lookCloudReport.Text} clouda əlavə edildi", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                XtraMessageBox.Show(resultProfit.Error, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "Satış hesabatı":
                            var resultSales = await facade.SendSalesAsync(true);
                            if (resultSales.Ok)
                                XtraMessageBox.Show($"{lookCloudReport.Text} clouda əlavə edildi", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                XtraMessageBox.Show(resultSales.Error, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "Satış qaytarma hesabatı":
                            var resultRefund = await facade.SendSaleRefundAsync(true);
                            if (resultRefund.Ok)
                                XtraMessageBox.Show($"{lookCloudReport.Text} clouda əlavə edildi", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                XtraMessageBox.Show(resultRefund.Error, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "Ödəniş növü hesabatı":
                            var resultPaymentType = await facade.SendPaymentsAsync(true);
                            if (resultPaymentType.Ok)
                                XtraMessageBox.Show($"{lookCloudReport.Text} clouda əlavə edildi", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                XtraMessageBox.Show(resultPaymentType.Error, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                });

            }
        }

        #endregion [..CLOUD..]


        #region [..BACKUP..]

        private void spinBackupRemoveDay_Leave(object sender, EventArgs e)
        {
            var value = spinBackupRemoveDay.Value;
            string query = $"UPDATE DbBackupSettings set IsDailyDeleted = @value WHERE UserId = @userId";

            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@value", (byte)value);
                cmd.Parameters.AddWithValue("@userId", UserCacheService.User.Id);
                cmd.ExecuteNonQuery();
            }
        }

        private void tBackupSendEmail_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() is "Save")
            {
                string query = $"UPDATE DbBackupSettings set Email = @email WHERE UserId = @userId";

                using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@email", tBackupSendEmail.Text.TrimStart().Trim());
                    cmd.Parameters.AddWithValue("@userId", UserCacheService.User.Id);
                    cmd.ExecuteNonQuery();
                }
                tBackupSendEmail.Properties.Buttons[0].Visible = true;
                tBackupSendEmail.Properties.Buttons[1].Visible = false;
                tBackupSendEmail.ReadOnly = true;

            }
            else if (e.Button.Tag.ToString() is "Edit")
            {
                tBackupSendEmail.Properties.Buttons[0].Visible = false;
                tBackupSendEmail.Properties.Buttons[1].Visible = true;
                tBackupSendEmail.ReadOnly = false;
            }
        }

        private void DbBackupSettingsLoad()
        {
            var data = DbBackupService.dbBackupSetting;
            if (data != null)
            {
                spinBackupRemoveDay.Value = data.IsDailyDeleted;
                tBackupSendEmail.Text = data.Email;
                chBackupAuto.Checked = data.DailyBackup;

                if (string.IsNullOrWhiteSpace(tBackupSendEmail.Text))
                {
                    tBackupSendEmail.Properties.Buttons[0].Visible = false;
                    tBackupSendEmail.Properties.Buttons[1].Visible = true;
                    tBackupSendEmail.ReadOnly = false;
                }
                else
                {
                    tBackupSendEmail.Properties.Buttons[0].Visible = true;
                    tBackupSendEmail.Properties.Buttons[1].Visible = false;
                    tBackupSendEmail.ReadOnly = true;
                }
            }
        }

        private void chBackupAuto_EditValueChanged(object sender, EventArgs e)
        {
            string query = $"UPDATE DbBackupSettings set DailyBackup = @value WHERE UserId = @userId";

            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@value", chBackupAuto.Checked);
                cmd.Parameters.AddWithValue("@userId", UserCacheService.User.Id);
                cmd.ExecuteNonQuery();
            }
        }

        #endregion  [..BACKUP..]


        #region [..RRN MODULE..]

        private void RrnSaveStatus(bool status)
        {
            File.WriteAllText(RrnFilePath, status ? "1" : "0");
        }

        private void RrnLoadStatus()
        {
            if (!File.Exists(RrnFilePath))
            {
                chRrn.Checked = false;
                return;
            }

            var text = File.ReadAllText(RrnFilePath);
            if (text is "1")
                chRrn.Checked = true;
            else
                chRrn.Checked = false;
        }

        private void chRrn_CheckedChanged(object sender, EventArgs e)
        {
            RrnSaveStatus(chRrn.Checked);
        }

        #endregion



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

        #region [..PRODUCTS..]

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

        #endregion [..PRODUCTS..]


        #region [..BANK..]

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

        #endregion [..BANK..]


        #region [..REPORT..]

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

        #endregion [..REPORT..]


        #region [..CREDİT..]

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

        #endregion [..CREDİT..]


        #region [..SUPPLİERS..]

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

        #endregion [..SUPPLİERS..]


        #region [..USERS..]

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

        #endregion [..USERS..]


        #region [..SETTINGS..]

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

        private void bBranchLog_Click(object sender, EventArgs e)
        {
            DbHelpers.UseRemoteConnection(RemoteDbManager.BranchConnectionString);
            fLogs f = new fLogs();
            f.Show();
            f.FormClosed += (s, args) =>
            {
                DbHelpers.UseLocalConnection();
            };
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

        #endregion [..SETTINGS..]


        #endregion [..BRANCHES..]


        private async void MAINSCRRENS_Shown(object sender, EventArgs e)
        {
            DbHelpers.UseLocalConnection();
            await MonthEarningLoadAsync();
            await SalesTypeLoadAsync();
            await StockCountAsync();
        }

        private async Task MonthEarningLoadAsync()
        {
            int year = DateTime.Today.Year;
            DateTime startMonth = new DateTime(year, 1, 1);

            var list = new List<DashboardStatisticsDto>();

            // Öncə bütün ayları 0 ilə doldur (SQL’də boş aylar olmayabilər)
            for (int i = 0; i < 12; i++)
            {
                var monthDate = startMonth.AddMonths(i);
                var name = (Enums.Month)monthDate.Month;

                list.Add(new DashboardStatisticsDto
                {
                    Day = Enums.GetEnumDescription(name),
                    Date = monthDate,
                    TotalGain = 0
                });
            }

            const string query = @"SELECT 
    SaleMonth,
    SUM(TotalGain) AS TotalGain
FROM (
    -- POS
    SELECT 
        MONTH(psm.date_) AS SaleMonth, 
        ISNULL(SUM(CAST(psm.UMUMI_MEBLEG AS DECIMAL(18,5))), 0) AS TotalGain
    FROM pos_satis_check_main psm
    WHERE YEAR(psm.date_) = @Year
    GROUP BY MONTH(psm.date_)

    UNION ALL

    -- GAIME
    SELECT 
        MONTH(g.TARIX) AS SaleMonth, 
        ISNULL(SUM(CAST(g.YEKUN_MEBLEG AS DECIMAL(18,5))), 0) AS TotalGain
    FROM GAIME_SATISI_DETAILS g
    WHERE YEAR(g.TARIX) = @Year
    GROUP BY MONTH(g.TARIX)

    UNION ALL

    -- KREDİT
    SELECT 
        MONTH(k.TARIX) AS SaleMonth, 
        ISNULL(SUM(CAST(k.prd_qty * k.prd_price AS DECIMAL(18,5))), 0) AS TotalGain
    FROM KREDIT_SATISI_MAIN k
    WHERE YEAR(k.TARIX) = @Year
    GROUP BY MONTH(k.TARIX)
) t
GROUP BY SaleMonth
ORDER BY SaleMonth;";
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;
                cmd.CommandTimeout = 300;
                await connection.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int month = reader.GetInt32(0);
                        decimal total = reader.GetDecimal(1);

                        var dto = list.First(x => x.Date.Month == month);
                        dto.TotalGain = total;
                    }
                }
            }

            var series = chartWeek.Series[0];
            series.DataSource = list;
            series.ArgumentDataMember = "Day";
            series.ValueDataMembers.Clear();
            series.ValueDataMembers.AddRange(new[] { "TotalGain" });
        }

        private async Task SalesTypeLoadAsync()
        {
            var currentDate = DateTime.Now.Date;

            var list = new List<DashboardSaleTypeDto>();

            const string query = @"SELECT 
    date_ as N'Date',
    CAST(SUM(NEGD) AS decimal(18,3)) as N'Cash',
    CAST(SUM(BANK_) AS decimal(18,3)) as N'Bank',
    CAST(SUM(KART_) AS decimal(18,3)) as N'Card',
    CAST(SUM(NISYE) AS decimal(18,3)) as N'Credit'
FROM (

    -- QAİMƏ SATIŞI
    SELECT 
        cast(gm.DATE_ as date) date_,
        0 AS NEGD,
        SUM(ISNULL( gm.ODENILEN_MEBLEG, 0)) AS BANK_,
        0 AS KART_,
        0 AS NISYE
    FROM GAIME_SATISI_MAIN gm 
    inner join GAIME_SATISI_DETAILS gsd 
        on gm.GAIME_SATISI_MAIN_ID = gsd.GAIME_SATISI_MAIN_ID
    left join gaime_satis_gaytarma gsg 
        on gsg.gaime_satis_details_id=gsd.GAIME_SATISI_DETAILS_ID 
    CROSS APPLY (
        SELECT 
        (gsd.MIGDARI - isnull(gsg.migdar,0.0)) *
        (cast(replace(gsd.SATIS_GIYMETI,',','.') as decimal(18,3)) -
         cast(replace(gsd.ENDIRIM_MEBLEGI,',','.') as decimal(18,3))) as mebleg
    ) calc
    where cast(gm.DATE_ as date) = @date 
    GROUP BY cast(gm.DATE_ as date)

    UNION ALL

    -- POS SATIŞI
    SELECT 
        cast(date_ as date),
        SUM(CASE WHEN NEGD_>UMUMI_MEBLEG THEN UMUMI_MEBLEG ELSE NEGD_ END),
        0,
        SUM(KART_),
        0
    FROM pos_satis_check_main
    where cast(date_ as date) = @date 
    GROUP BY cast(date_ as date)

    UNION ALL

    -- KREDIT SATIŞI
    SELECT 
        cast(TARIX as date),
        0,
        0,
        0,
        SUM(yekun)
    FROM KREDIT_SATISI_MAIN
    where cast(TARIX as date) = @date 
    GROUP BY cast(TARIX as date)

) X
GROUP BY date_;";
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.Add("@date", SqlDbType.Date).Value = currentDate;
                cmd.CommandTimeout = 300;
                await connection.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        decimal cash = reader.GetDecimal(1);
                        decimal bank = reader.GetDecimal(2);
                        decimal card = reader.GetDecimal(3);
                        decimal credit = reader.GetDecimal(4);

                        list.Add(new DashboardSaleTypeDto { PaymentName = "Nağd", Amount = cash });
                        list.Add(new DashboardSaleTypeDto { PaymentName = "Kart", Amount = card });
                        list.Add(new DashboardSaleTypeDto { PaymentName = "Bank", Amount = bank });
                        list.Add(new DashboardSaleTypeDto { PaymentName = "Nisyə", Amount = credit });
                    }
                }
            }

            var series = chartSalesType.Series[0];
            series.DataSource = list;
            series.ArgumentDataMember = "PaymentName";
            series.ValueDataMembers.Clear();
            series.ValueDataMembers.AddRange(new[] { "Amount" });

            series.Label.TextPattern = "{A}: {V:C2}";
            series.LegendTextPattern = "{A}";
        }

        private async Task StockCountAsync()
        {
            const string query = "SELECT COUNT(*) FROM [VW_WAREHOUSE_STOCK]";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                await con.OpenAsync();
                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        var data = dr[0].ToString();
                        lStockCount.Text = data;
                    }
                }
            }
        }

        private void accordionControlElement36_Click(object sender, EventArgs e)
        {
            OpenForm<fBankSaleReport>();
        }

        private void chBranch_Click(object sender, EventArgs e)
        {
            bool isBranch = false;

            if (!chBranch.Checked)
            {
                using (var f = new fAdminPassword())
                    isBranch = f.ShowDialog() == DialogResult.OK;
            }

            chBranch.Checked = isBranch;
            Registry.CurrentUser.CreateSubKey("Mpos")?.SetValue("Branch", isBranch);
            BranchShow();
        }

        private void accordionControlElement74_Click(object sender, EventArgs e)
        {
            fInfo f = new fInfo();
            f.ShowDialog();
        }

        private class DashboardStatisticsDto
        {
            public string Day { get; set; }
            public DateTime Date { get; set; }
            public decimal TotalGain { get; set; }
        }

        private class DashboardSaleTypeDto
        {
            public string PaymentName { get; set; }
            public decimal Amount { get; set; }
        }
    }
}