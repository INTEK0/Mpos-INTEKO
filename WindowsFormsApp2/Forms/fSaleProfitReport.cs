using System;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraGrid;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.Services;

namespace WindowsFormsApp2.Forms
{
    public partial class fSaleProfitReport : DevExpress.XtraEditors.XtraForm
    {
        public fSaleProfitReport()
        {
            InitializeComponent();
        }

        private void fSaleProfitReport_Load(object sender, EventArgs e)
        {
            var date = DatetimeService.CurrentDateTime;
            dateEdit1.DateTime = date;
            dateEdit2.DateTime = date;
            gridView1.OptionsView.ShowColumnHeaders = false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GetReport(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text).AddDays(1));
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Satış üzrə mənfəət hesabatı");
        }

        private void GetReport(DateTime start, DateTime end)
        {
            try
            {
                string query = "EXEC [dbo].[ANBAR_MENFEET]  @d1=@start, @d2=@end";
                using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
                using (SqlCommand cmd = new SqlCommand(query,con))
                {
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);
                        gridView1.OptionsView.ShowColumnHeaders = true;
                        gridView1.OptionsView.ShowColumnHeaders = true;
                        gridControl1.DataSource = dt;
                        gridView1.BestFitColumns();

                        string SaleQuantity = "SaleQuantity";
                        string TotalPurchaseAmount = "TotalPurchaseAmount";
                        string TotalSaleAmount = "TotalSaleAmount";
                        string Profit = "Profit";

                        gridView1.Columns[SaleQuantity].Summary.Clear();
                        gridView1.Columns[TotalPurchaseAmount].Summary.Clear();
                        gridView1.Columns[TotalSaleAmount].Summary.Clear();
                        gridView1.Columns[Profit].Summary.Clear();

                        GridColumnSummaryItem quantitySum = new GridColumnSummaryItem
                        {
                            FieldName = SaleQuantity,
                            SummaryType = DevExpress.Data.SummaryItemType.Sum,
                            DisplayFormat = "{0:N2}"
                        };

                        GridColumnSummaryItem purchasekSum = new GridColumnSummaryItem
                        {
                            FieldName = TotalPurchaseAmount,
                            SummaryType = DevExpress.Data.SummaryItemType.Sum,
                            DisplayFormat = "{0:N2}"
                        };

                        GridColumnSummaryItem saleSum = new GridColumnSummaryItem
                        {
                            FieldName = TotalSaleAmount,
                            SummaryType = DevExpress.Data.SummaryItemType.Sum,
                            DisplayFormat = "{0:N2}"
                        };

                        GridColumnSummaryItem profitSum = new GridColumnSummaryItem
                        {
                            FieldName = Profit,
                            SummaryType = DevExpress.Data.SummaryItemType.Sum,
                            DisplayFormat = "{0:N2}"
                        };

                        gridView1.Columns[SaleQuantity].Summary.Add(quantitySum);
                        gridView1.Columns[TotalPurchaseAmount].Summary.Add(purchasekSum);
                        gridView1.Columns[TotalSaleAmount].Summary.Add(saleSum);
                        gridView1.Columns[Profit].Summary.Add(profitSum);
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            gridView1.GroupPanelText = $"Satış sayı: {gridView1.RowCount}";
        }
    }
}