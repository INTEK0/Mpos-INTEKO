using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Forms;
using WindowsFormsApp2.Helpers;
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
            if (xuser < 1)
            {
                accordionControlElement5.Visible = false;
                accordionControlElement6.Visible = false;
                accordionControlElement58.Visible = false;
                accordionControlElement8.Visible = false;
                accordionControlElement9.Visible = false;
                accordionControlElement10.Visible = false;
                accordionControlElement17.Visible = false;
                accordionControlElement28.Visible = false;
                accordionControlElement32.Visible = false;
                accordionControlElement33.Visible = false;
                accordionControlElement35.Visible = false;
                accordionControlElement36.Visible = false;
                accordionControlElement43.Visible = false;
                accordionControlElement47.Visible = false;
                accordionControlElement48.Visible = false;
                accordionControlElement50.Visible = false;
                tabLog.Visible = false;
                tabDatabase.Visible = false;
                accordionControlElement54.Visible = false;
            }
            GridPanelText(gridProducts);
            GridPanelText(gridLogs);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void accordionControlElement13_Click(object sender, EventArgs e)
        {
            FormHelpers.Alert("Bu modul aktiv deyildir. Servis xidmətinə müraciət edin", Enums.MessageType.Warning);
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
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
            OpenForm<GAIME_SATISI_LAYOUT>(Properties.Settings.Default.UserID, this);
        }

        private void accordionControlElement12_Click(object sender, EventArgs e)
        {
            OpenForm<QAIME_SATISI_QAYTARMA_LAYOUT>(Properties.Settings.Default.UserID);
        }

        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
            OpenForm<fAddProduct>();
        }

        private void accordionControlElement9_Click(object sender, EventArgs e)
        {
            OpenForm<MEHSUL_GAYTARMA_LAYOUT>(Properties.Settings.Default.UserID);
        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
            //OpenForm<TECIZATCI_LAYOUT>();
            OpenForm<fAddSupplier>();
        }

        private void accordionControlElement41_Click(object sender, EventArgs e)
        {
            OpenForm<POS_LAYOUT_NEW>();
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


            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
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

            string queryString = @"SELECT  ROW_NUMBER() OVER(ORDER BY [MƏHSUL ADI]) AS Hotkey,
REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE([MƏHSUL ADI],N'Ə','E'),N'ə','e'),N'ı','i'),N'ü','u'),N'ğ','g'),N'Ğ','G' ),N'Ü','U'),N'Ş','S'),N'ş','s'),N'Ç','C'),N'ç','c')  as Name  ,
[MAL_ALISI_DETAILS_ID] as LFCode,
[MAL_ALISI_DETAILS_ID] as Code ,
7 AS [Barcode Type],
[SATIŞ QİYMƏTİ] AS [Unit Price],
4 AS [Unit Weight],
0 AS [Unit Amount] ,
21 AS [Department],
0 AS [PT Weight],
15 AS [Shelf Time],
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

            var data = DbProsedures.ConvertToDataTable(queryString);

            gridControl2.DataSource = data;

            using (SaveFileDialog saveFile = new SaveFileDialog
            {
                Filter = "Excel Faylı|*.xls",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                OverwritePrompt = true, //varsa soruşmadan üstünə yazması üçün false olaraq qalmalıdır
                FileName = "TərəziData.xls"
            })
            {
                if (saveFile.ShowDialog() is DialogResult.OK)
                {
                    gridView2.ExportToCsv(saveFile.FileName, new DevExpress.XtraPrinting.CsvExportOptions { Separator = "\t" });
                }
            }
        }

        private void accordionControlElement18_Click(object sender, EventArgs e)
        {
            OpenForm<bank_odenisleri>(Properties.Settings.Default.UserID);
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
            OpenForm<fAddCustomer>();
        }

        private void accordionControlElement53_Click(object sender, EventArgs e)
        {
            tabPaneSettings.SelectedPage = tabModul;
        }

        private void accordionControlElement23_Click(object sender, EventArgs e)
        {
            OpenForm<fAddGuarantor>();
            //OpenForm<ZAMINLER_LAYOUT>();
        }

        private void accordionControlElement24_Click(object sender, EventArgs e)
        {
            OpenForm<KREDITSATISLAYOUTSA>(Properties.Settings.Default.UserID, this);
        }

        private void accordionControlElement25_Click(object sender, EventArgs e)
        {
            OpenForm<SearchKrediOdeme_LAYOUT>(Properties.Settings.Default.UserID, this);
        }

        private void accordionControlElement26_Click(object sender, EventArgs e)
        {
            OpenForm<KREDITHESABATI>();
        }

        private void accordionControlElement27_Click(object sender, EventArgs e)
        {
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
            FormHelpers.Alert("Bu modul aktiv deyildir. Servis xidmətinə müraciət edin", Enums.MessageType.Warning);
            //OpenForm<Magaza>();
        }

        private void accordionControlElement55_Click(object sender, EventArgs e)
        {
            //OpenForm<RibbonForm1>(1, Properties.Settings.Default.UserID);
            OpenForm<fPrintBarcode>();
        }

        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                case CloseReason.TaskManagerClosing:
                case CloseReason.FormOwnerClosing:
                case CloseReason.ApplicationExitCall:
                    FormHelpers.Log("Sistemdən çıxış etdi");
                    Application.Exit();
                    break;
            }
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            lMposVersion.Text = Application.ProductVersion;
            lLicenceVersion.Text = "Yoxdur";
            BestsellingProducts();
            TotalSalesInformation();
            TotalRefundInformation();
            TotalPurchaseInformation();
            StockInformation();
            StockDecreasingAmountLoad();
            ProductNegativeStatus();
            HotSalesShow();
            SendToKassaShow();
            TerminalReceiptPrintShow();
            OtherPayShow();
            SuccessMessageVisibleShow();
            Get_StockDecreasingAmountShow();
            //ClinicModuleShow();
        }

        private void BestsellingProducts(string count = "5")
        {
            gridView1.ViewCaption = $"{DateTime.Now.ToString("MMMM")} ayında ən çox satılan {count} məhsul";
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
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
ORDER BY TotalAmount DESC;
";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        da.Fill(dataTable);
                        gridControl1.DataSource = dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// SATIŞ HESABATI
        /// </summary>
        private void TotalSalesInformation()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                con.Open();
                string query = $@"SELECT 
                ISNULL(SUM(t.TotalSalePrice), 0) AS TotalSalePrice,
                ISNULL(SUM(t.SalesCount), 0) AS TotalSalesCount
                FROM (
                -- pos_satis_check_details cədvəlindəki datalar
                SELECT 
                ISNULL(SUM(CAST(p.count_ AS DECIMAL(18, 2)) * CAST(p.satis_giymet AS DECIMAL(18, 2))),0) AS TotalSalePrice,
                ISNULL(SUM(CAST(p.count_ AS DECIMAL(18, 2))),0) AS SalesCount
                FROM [dbo].[pos_satis_check_details] p
                JOIN [dbo].[pos_satis_check_main] pm ON p.[pos_satis_check_main_id] = pm.[pos_satis_check_main_id]
                WHERE CAST(pm.date_ AS DATE) = CAST(GETDATE() AS DATE)

                UNION ALL

                -- GAIME_SATISI_DETAILS cədvəlindəki datalar
                SELECT 
                ISNULL(SUM(CAST(g.YEKUN_MEBLEG AS DECIMAL(18, 2))),0) AS TotalSalePrice,
                ISNULL(SUM(CAST(g.MIGDARI AS DECIMAL(18, 2))),0) AS SalesCount
                FROM [dbo].[GAIME_SATISI_DETAILS] g
                WHERE CAST(g.TARIX AS DATE) = CAST(GETDATE() AS DATE)) t;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lSalePriceTotal.Text = Convert.ToDecimal(dr["TotalSalePrice"]).ToString("C2");
                            lSalesCount.Text = dr["TotalSalesCount"].ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// QAYTARMA HESABATI
        /// </summary>
        private void TotalRefundInformation()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                con.Open();
                string query = $@"SELECT 
                ISNULL(SUM(t.TotalRefundPrice),0) AS TotalRefundPrice,
                ISNULL(SUM(t.RefundCount),0) AS TotalRefundCount
                FROM (
                -- pos_gaytarma_manual cədvəlindəki datalar
                SELECT 
                ISNULL(SUM(CAST(pg.say AS DECIMAL(18, 2)) * CAST(pd.satis_giymet AS DECIMAL(18, 2))),0) AS TotalRefundPrice,
                ISNULL(SUM(CAST(pg.say AS DECIMAL(18, 2))),0) AS RefundCount
                FROM [dbo].pos_gaytarma_manual pg
                JOIN [dbo].pos_satis_check_details pd ON pg.pos_satis_check_details = pd.pos_satis_check_details_id
                WHERE CAST(pg.date_ AS DATE) = CAST(GETDATE() AS DATE)

                UNION ALL

                -- gaime_satis_gaytarma cədvəlindəki datalar
                SELECT 
                ISNULL(SUM(CAST(gd.SATIS_GIYMETI AS DECIMAL(18, 2)) * CAST(g.migdar AS DECIMAL(18, 2))),0) AS TotalRefundPrice,
                ISNULL(SUM(CAST(g.migdar AS DECIMAL(18, 2))),0) AS RefundCount
                FROM [dbo].gaime_satis_gaytarma g
	            JOIN [dbo].GAIME_SATISI_DETAILS gd ON g.gaime_satis_details_id = gd.GAIME_SATISI_DETAILS_ID
                WHERE CAST(g.tarix_ AS DATE) = CAST(GETDATE() AS DATE)) t;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lRefuntPrice.Text = Convert.ToDecimal(dr["TotalRefundPrice"]).ToString("C2");
                            lRefundCount.Text = dr["TotalRefundCount"].ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ALIŞ HESABATI
        /// </summary>
        private void TotalPurchaseInformation()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                con.Open();
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
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lPurchaseTotalPrice.Text = Convert.ToDecimal(dr["TotalPurchasePrice"]).ToString("C2");
                            lPurchaseCount.Text = dr["TotalPurchaseCount"].ToString();
                        }
                    }
                }
            }
        }

        private void chShowStock_CheckedChanged(object sender, EventArgs e)
        {
            CheckButton checkEdit = (CheckButton)sender;
            if (checkEdit.Checked)
            {
                StockProductsList();
            }
        }

        private void chStockDecreasingAmount_CheckedChanged(object sender, EventArgs e)
        {
            CheckButton checkEdit = (CheckButton)sender;
            if (checkEdit.Checked)
            {
                StockDecreasingAmountLoad();
            }
        }

        /// <summary>
        /// STOK SAYI lStockCount-a yazdırılır
        /// </summary>
        private void StockInformation()
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                string query = $@"
                DECLARE @Result TABLE (
                TECHIZATCI_ID int,
                TECHIZATCI NVARCHAR(100),
                MAL_ALIS_DETAILS_ID int,
                PRODUCTNAME NVARCHAR(500),
                PRODUCTCODE NVARCHAR(100),
                SALEPRICE decimal(18, 3),
                STOCK decimal(9,2),
                BARCODE NVARCHAR(100),
                EDV NVARCHAR(50));
                INSERT INTO @Result
                EXEC dbo.gaime_Satis_mal_load;
                SELECT COUNT(*) AS StockCount
                FROM @Result;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lStockCount.Text = dr["StockCount"].ToString();
                        }
                    }
                }
            }
        }

        private void bGridExcelExport_Click(object sender, EventArgs e)
        {
            if (chShowStock.Checked)
            {
                FormHelpers.ExcelExport(gridControlProducts, "Anbar Qalığı");
            }
            else if (chStockDecreasingAmount.Checked)
            {
                FormHelpers.ExcelExport(gridControlProducts, "Miqdarı az olan məhsulların siyahısı");
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
        private async void StockProductsList()
        {
            Cursor.Current = Cursors.WaitCursor;
            gridProducts.ViewCaption = "Anbar qalığı";

            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                string query = "EXEC dbo.gaime_Satis_mal_load;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    await con.OpenAsync();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => da.Fill(dataTable));
                        gridControlProducts.DataSource = dataTable;
                        if (gridProducts.Columns["TECHIZATCI_ID"] != null)
                        {
                            gridProducts.Columns["TECHIZATCI_ID"].Visible = false;
                        }
                        if (gridProducts.Columns["MAL_ALISI_DETAILS_ID"] != null)
                        {
                            gridProducts.Columns["MAL_ALISI_DETAILS_ID"].Visible = false;
                        }
                        if (gridProducts.Columns["EDV"] != null)
                        {
                            gridProducts.Columns["EDV"].Visible = false;
                        }
                        gridProducts.RefreshData();
                    }
                }
            }
        }

        private void accordionControlElement54_Click_1(object sender, EventArgs e)
        {
            OpenForm<USERQEYDIYYAT_LAYOUT>();
        }

        /// <summary>
        /// Miqdarı az olan məhsulları göstərilməsi
        /// </summary>
        private async void StockDecreasingAmountLoad()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (chStockAmount.Checked)
            {
                gridProducts.ViewCaption = "Miqdarı az olan məhsullar";
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                    string query = $@"exec [StockDecreasingAmount]";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        await con.OpenAsync();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            await Task.Run(() => da.Fill(dataTable));
                            gridControlProducts.DataSource = dataTable;
                        }
                    }
                }
            }
            else
            {
                chStockDecreasingAmount.Visible = false;
                chShowStock.Dock = DockStyle.Left;
                chShowStock.Checked = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void lRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                BestsellingProducts();
                StockDecreasingAmountLoad();
                TotalSalesInformation();
                TotalRefundInformation();
                TotalPurchaseInformation();
                StockInformation();
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DATALOAD_MESSAGE(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
                chStockDecreasingAmount.Checked = true;
            }
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
                lLicenceKey.Text = LicenceKey();
            }
            else if (e.Page == tabModul)
            {

            }
        }

        private void bBackupDownload_Click(object sender, EventArgs e)
        {
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
                        using (DataTable dataTable = new DataTable())
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
            }
            else
            {
                chSendToKassa.Checked = false;
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

        //private void ClinicModuleShow()
        //{
        //    bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("ClinicModule").ToString());
        //    if (control)
        //    {
        //        chClinicModul.Checked = true;
        //        accordionControlElement49.Visible = true;
        //        accordionControlElement49.VisibleInFooter = true;
        //    }
        //    else
        //    {
        //        chClinicModul.Checked = false;
        //        accordionControlElement49.Visible = false;
        //        accordionControlElement49.VisibleInFooter = false;
        //    }
        //}

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
            }
            else
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("SendToKassa", false);
            }
        }

        private void chStockAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (chStockAmount.Checked)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("DecreasingAmount", true);
                chStockDecreasingAmount.Visible = true;
                chShowStock.Dock = DockStyle.None;
                chShowStock.Location = new System.Drawing.Point(210, 2);
            }
            else
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("DecreasingAmount", false);
                chStockDecreasingAmount.Visible = false;
                chShowStock.Dock = DockStyle.Left;
                StockProductsList();
            }
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
    }
}