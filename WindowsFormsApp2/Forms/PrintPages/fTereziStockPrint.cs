using System;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Forms.PrintPages
{
    public partial class fTereziStockPrint : DevExpress.XtraEditors.XtraForm
    {
        public fTereziStockPrint()
        {
            InitializeComponent();
        }

        private void fTereziStockPrint_Load(object sender, EventArgs e)
        {
            GridDataLoad();
            barSubItem2.Caption = bPdf.Caption;
            barSubItem2.ImageOptions.SvgImage = bPdf.ImageOptions.SvgImage;
        }

        private void GridDataLoad()
        {
            string query = @"SELECT 
[MAL_ALISI_DETAILS_ID] AS Code,
[MƏHSUL ADI] AS ProductName,
[SATIŞ QİYMƏTİ] AS SalePrice 
FROM [terazimalzeme]";
            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;

            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = gridControl1;
            link.CreateDocument();
            link.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;

            documentViewer1.DocumentSource = link;
        }

        
        private void bClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void CheckedPrintType(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //barSubItem2.Caption = e.Item.Caption;
            //barSubItem2.ImageOptions.SvgImage = e.Item.ImageOptions.SvgImage;
        }

        private void bPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SendKeys.Send("^p");
        }
    }
}