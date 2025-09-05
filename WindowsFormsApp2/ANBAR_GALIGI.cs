using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using WindowsFormsApp2.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class ANBAR_GALIGI : BaseForm
    {
        private readonly string filePath = $@"{Application.StartupPath}\LocalFiles\GridColumnsSettings.json";
        public ANBAR_GALIGI()
        {
            InitializeComponent();
            GridPanelText(gridView1);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Anbar Qalığı");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dateEdit4.Text))
                FormHelpers.Alert("Tarix seçimi edilməyib", Enums.MessageType.Warning);
            else
                getall(Convert.ToDateTime(dateEdit4.Text));
        }

        private void getall(DateTime D2_)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (D2_.Date == DateTime.Now.Date)
                D2_ = D2_.AddDays(1);

            string queryString = "gaime_Satis_mal_load_tarixle @d1 = @pricepoint1";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(queryString, con))
            {
                cmd.CommandTimeout = 300;
                cmd.Parameters.AddWithValue("@pricepoint1", D2_);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                using (DataTable dt = new DataTable())
                {
                    da.Fill(dt);

                    gridControl1.DataSource = dt;
                    gridView1.Columns["ANBAR QALIĞI"].Summary.Clear();
                    gridView1.Columns["ALIŞ QİYMƏTİ"].Summary.Clear();
                    gridView1.Columns["SATIŞ QİYMƏTİ"].Summary.Clear();
                    GridColumnSummaryItem stockSum = new GridColumnSummaryItem
                    {
                        FieldName = "ANBAR QALIĞI",
                        SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        DisplayFormat = "{0:N2}"
                    };
                    GridColumnSummaryItem PuchaseSum = new GridColumnSummaryItem
                    {
                        FieldName = "ALIŞ QİYMƏTİ",
                        SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        DisplayFormat = "{0:N2}",

                    };
                    GridColumnSummaryItem SaleSum = new GridColumnSummaryItem
                    {
                        FieldName = "SATIŞ QİYMƏTİ",
                        SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        DisplayFormat = "{0:N2}",

                    };
                    gridView1.Columns["ANBAR QALIĞI"].Summary.Add(stockSum);
                    gridView1.Columns["ALIŞ QİYMƏTİ"].Summary.Add(PuchaseSum);
                    gridView1.Columns["SATIŞ QİYMƏTİ"].Summary.Add(SaleSum);
                }
            }
        }

        private void ANBAR_GALIGI_Load(object sender, EventArgs e)
        {
            dateEdit4.DateTime = DateTime.Now;
        }

        private async void gridView1_DoubleClick(object sender, EventArgs e)
        {
            await GetProduct();
        }

        private async Task GetProduct()
        {
            string supplierName = gridView1.GetFocusedRowCellValue("TƏCHİZATÇI")?.ToString() ?? "Yoxdur";
            string name = gridView1.GetFocusedRowCellValue("MƏHSUL ADI")?.ToString() ?? "Yoxdur";
            string code = gridView1.GetFocusedRowCellValue("MƏHSUL KODU")?.ToString() ?? "Yoxdur";
            string barcode = gridView1.GetFocusedRowCellValue("MƏHSUL BARKOD")?.ToString() ?? "Yoxdur";
            string unit = gridView1.GetFocusedRowCellValue("VAHİD")?.ToString() ?? "Yoxdur";
            string tax = gridView1.GetFocusedRowCellValue("EDV")?.ToString() ?? "Yoxdur";
            decimal purchasePrice = Convert.ToDecimal(gridView1.GetFocusedRowCellValue("ALIŞ QİYMƏTİ")?.ToString() ?? "0");
            decimal salePrice = Convert.ToDecimal(gridView1.GetFocusedRowCellValue("SATIŞ QİYMƏTİ")?.ToString() ?? "0");
            decimal quantity = Convert.ToDecimal(gridView1.GetFocusedRowCellValue("ANBAR QALIĞI")?.ToString() ?? "0");
            ProductDetail _detail = new ProductDetail()
            {
                SupplierName = supplierName,
                ProductName = name,
                ProductCode = code,
                Barcode = barcode,
                UnitName = unit,
                TaxName = tax,
                PurchasePrice = purchasePrice,
                SalePrice = salePrice,
                StockAmount = quantity

            };
            fProductDetail detail = new fProductDetail(_detail);
            detail.ShowDialog();
        }

        private void gridView1_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            if (e.Column.SummaryItem.SummaryType == DevExpress.Data.SummaryItemType.Sum)
            {
                e.Handled = true;
                e.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
                e.Appearance.ForeColor = Color.AliceBlue;
                e.Appearance.DrawBackground(e.Cache, e.Bounds);
                e.Appearance.DrawString(e.Cache, e.Info.DisplayText, e.Bounds);
            }
        }

        private void bShowColumns_Click(object sender, EventArgs e)
        {
            fColumnSettings f = new fColumnSettings("Stock");
            f.ShowDialog();
        }
    }
}