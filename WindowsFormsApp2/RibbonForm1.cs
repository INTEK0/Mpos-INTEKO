using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting.BarCode;
using Zen.Barcode;
using WindowsFormsApp2.Helpers;
using DevExpress.XtraGrid.Localization;
using static WindowsFormsApp2.Helpers.FormHelpers;
using System.Drawing.Printing;
using WindowsFormsApp2.Forms;
using System.Drawing.Imaging;

namespace WindowsFormsApp2
{
    public partial class RibbonForm1 : DevExpress.XtraEditors.DirectXForm
    {

        public static int u_id;
        private bool isClosingHandled { get; set; }
        public string productname, productprice, barcodesa;
        string marka, fiyat, tedarikci, urunad, discountPrice, oldPrice, urunkod, barkodsa;
        public RibbonForm1(int xuser, int user_id)
        {
            InitializeComponent();
            u_id = user_id;
            if (xuser < 1)
            {
                barButtonItem44.Enabled = false;
                barSubItem7.Enabled = false;
                barSubItem6.Enabled = false;
                barButtonItem24.Enabled = false;
                barSubItem5.Enabled = false;
                barSubItem4.Enabled = false;
                barSubItem2.Enabled = false;
                barSubItem1.Enabled = false;
                barButtonItem14.Enabled = false;
                barButtonItem4.Enabled = false;
                barButtonItem3.Enabled = false;
                barButtonItem2.Enabled = false;
                barButtonItem1.Enabled = false;
            }
            FormHelpers.GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }


        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        MEHSUL_ALISI_LAYOUT MA;

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["MEHSUL_ALISI_LAYOUT"] != null)
            {
                var Main = Application.OpenForms["MEHSUL_ALISI_LAYOUT"] as MEHSUL_ALİSİ;
                if (Main != null)
                {
                    //
                }
                // Main.Close();
            }
            else
            {
                MA = new MEHSUL_ALISI_LAYOUT(u_id);
                MA.Show();
            }
        }

        MEHSUL_GAYTARMA_LAYOUT MG;
        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["MEHSUL_GAYTARMA_LAYOUT"] != null)
            {
                var Main = Application.OpenForms["MEHSUL_GAYTARMA_LAYOUT"] as MEHSUL_GAYTARMA_LAYOUT;
                if (Main != null)
                {

                }
                //  Main.Close();
            }
            else
            {
                MG = new MEHSUL_GAYTARMA_LAYOUT(u_id);
                MG.Show();

            }
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {


        }

        QAIME_SATISI_QAYTARMA_LAYOUT GST;
        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["QAIME_SATISI_QAYTARMA_LAYOUT"] != null)
            {
                var Main = Application.OpenForms["QAIME_SATISI_QAYTARMA_LAYOUT"] as QAIME_SATISI_QAYTARMA_LAYOUT;
                if (Main != null)
                {

                }
                //  Main.Close();
            }
            else
            {
                GST = new QAIME_SATISI_QAYTARMA_LAYOUT(u_id);
                GST.Show();

            }


        }

        //  MÜŞTƏRİLƏR MS;
        MUSTERILER_LAYOUT MS;
        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["MUSTERILER_LAYOUT"] != null)
            {
                var Main = Application.OpenForms["MUSTERILER_LAYOUT"] as MUSTERILER_LAYOUT;
                if (Main != null)
                {

                }
                // Main.Close();
            }
            else
            {
                MS = new MUSTERILER_LAYOUT();
                MS.Show();
            }
        }

        ANBARDAN_ANBARA AB;
        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMessageBox.Show("BU MODUL DEAKTİV EDİLMİŞDİR ,SERVİS XİDMƏTİNƏ MÜRACİƏT EDİN");

        }

        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMessageBox.Show("BU MODUL DEAKTİV EDİLMİŞDİR ,SERVİS XİDMƏTİNƏ MÜRACİƏT EDİN");


        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMessageBox.Show("BU MODUL DEAKTİV EDİLMİŞDİR ,SERVİS XİDMƏTİNƏ MÜRACİƏT EDİN");

        }

        bank_odenisleri TO;
        private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["bank_odenisleri"] != null)
            {
                var Main = Application.OpenForms["bank_odenisleri"] as bank_odenisleri;
                if (Main != null)
                {

                }
                // Main.Close();
            }
            else
            {
                TO = new bank_odenisleri(u_id);
                TO.Show();

            }
        }

        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMessageBox.Show("Sistemdə bankla inteqrasiya mövcud deyildir.(Servis xidmətinə müraciət edin)");

        }

        private void barButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        ANBAR_GALIGI A;
        private void barButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (Application.OpenForms["ANBAR_GALIGI"] != null)
            {
                var Main = Application.OpenForms["ANBAR_GALIGI"] as ANBAR_GALIGI;
                if (Main != null)
                {

                }
                //    Main.Close();
            }
            else
            {
                A = new ANBAR_GALIGI();
                A.Show();

            }
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void RibbonForm1_Load(object sender, EventArgs e)
        {
            getall();
            gridControl1.RefreshDataSource();

        }
        public void getall()
        {
            try
            {
                gridView1.ClearSelection();
                gridControl1.RefreshDataSource();
                gridControl1.DataSource = null;


                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                // Provide the query string with a parameter placeholder.
                string queryString =
                     // " exec  dbo.chart_report ";

                     " exec dbo.gaime_Satis_mal_load ";

                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@pricepoint", D1_);
                //command.Parameters.AddWithValue("@pricepoint1", D2_);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //dataGridView1.DataSource= dt;
                //dataGridView1.Columns[2].Visible = false;
                //dataGridView1.Columns[0].Visible = false;

                gridControl1.DataSource = dt;
                gridView1.Columns["TECHIZATCI_ID"].Visible = false;
                gridView1.Columns["MAL_ALISI_DETAILS_ID"].Visible = false;

                //gridView1.Columns[0].Visible = false;
                gridView1.Columns["EDV"].Visible = false;
                //gridView1.Columns["VAHİD"].Visible = false;


                if (gridView1.Columns.ColumnByFieldName("Barkod Cap") != null)
                {

                }
                else
                {
                    AddUnboundColumn();
                    AddRepository();

                }

                footerProductCount.Caption = $"Məhsul sayısı: {gridView1.DataRowCount}";
                //gridView1.OptionsSelection.MultiSelect = true;
                //gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;


            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }
        }

        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        private void vahid_main()
        {
            string strQuery = "select VAHIDLER_ID, VAHIDLER_NAME as N'VAHIDLƏR' from VAHIDLER";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            lookVahidler.Properties.DisplayMember = "VAHIDLƏR";
            lookVahidler.Properties.ValueMember = "VAHIDLER_ID";
            lookVahidler.Properties.DataSource = dt;
            lookVahidler.Properties.NullText = "VAHIDLƏR";
            lookVahidler.Properties.PopulateColumns();
            lookVahidler.Properties.Columns[0].Visible = false;
        }

        private void AddRepository()
        {
            RepositoryItemButtonEdit edit = new RepositoryItemButtonEdit();
            edit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            edit.ButtonClick += edit_ButtonClick;
            edit.Buttons[0].Caption = "Barkod";
            edit.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            gridView1.Columns["Barkod Cap"].ColumnEdit = edit;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();

            foreach (var rowHandle in selectedRows)
            {
                string barcode = gridView1.GetRowCellValue(rowHandle, "MƏHSUL BARKOD").ToString();
                string name = gridView1.GetRowCellValue(rowHandle, "MƏHSUL ADI").ToString();
                string salesPrice = gridView1.GetRowCellValue(rowHandle, "SATIŞ QİYMƏTİ").ToString();

                productname = name;
                productprice = salesPrice;
                barcodesa = barcode;
                barkodc(barcode, rowHandle, name, salesPrice);

                System.Drawing.Printing.PrintDocument myPrintDocument1 = new System.Drawing.Printing.PrintDocument();
                PrintDialog myPrinDialog1 = new PrintDialog();

                myPrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printbarkod);
                myPrintDocument1.DocumentName = $"Barkod-{barcode}";
                // Varsayılan yazıcıyı ayarla
                myPrintDocument1.PrinterSettings.PrinterName = new System.Drawing.Printing.PrinterSettings().PrinterName;
                myPrinDialog1.Document = myPrintDocument1;
                myPrintDocument1.DocumentName = $"Barkod-{barcode}";
                myPrintDocument1.PrintController = new System.Drawing.Printing.StandardPrintController();
                myPrintDocument1.Print();
            }
        }

        //void edit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    int index = gridView1.FocusedRowHandle;
        //    string deger1 = gridView1.GetRowCellValue(index, "MƏHSUL BARKOD").ToString();
        //    string deger2 = gridView1.GetRowCellValue(index, "MƏHSUL ADI").ToString();
        //    string deger3 = gridView1.GetRowCellValue(index, "SATIŞ QİYMƏTİ").ToString();

        //    productname = deger2;
        //    productprice = deger3;
        //    barcodesa = deger1;
        //    barkodc(deger1, index, deger2, deger3);
        //    System.Drawing.Printing.PrintDocument myPrintDocument1 = new System.Drawing.Printing.PrintDocument();




        //    PrintDialog myPrinDialog1 = new PrintDialog();

        //    myPrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printbarkod);



        //    myPrinDialog1.Document = myPrintDocument1;



        //    if (myPrinDialog1.ShowDialog() == DialogResult.OK)
        //    {

        //        myPrintDocument1.Print();
        //    }
        //}


        void edit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            #region SofiStyle

            //int index = gridView1.FocusedRowHandle;
            //string deger1 = gridView1.GetRowCellValue(index, "MƏHSUL BARKOD").ToString();
            //string deger2 = gridView1.GetRowCellValue(index, "MƏHSUL ADI").ToString();
            //string deger3 = gridView1.GetRowCellValue(index, "SATIŞ QİYMƏTİ").ToString();
            //string deger4 = gridView1.GetRowCellValue(index, "TƏCHİZATÇI").ToString();
            //string deger5 = gridView1.GetRowCellValue(index, "MƏHSUL KODU").ToString();
            ////string deger6 = gridView1.GetRowCellValue(index, "ENDİRİMLİ QİYMƏTİ").ToString();

            //urunad = deger2;
            //fiyat = deger3;
            //barkodsa = deger1;
            //tedarikci = deger4;
            //urunkod = deger5;

            ////discountPrice = deger6; //HASAN
            //oldPrice = deger3; //HASAN



            //barkodc(deger1, index, deger2, deger3);
            //System.Drawing.Printing.PrintDocument myPrintDocument1 = new System.Drawing.Printing.PrintDocument();


            //PrintDialog myPrinDialog1 = new PrintDialog();

            //myPrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printbarkod);



            //myPrinDialog1.Document = myPrintDocument1;



            //if (myPrinDialog1.ShowDialog() == DialogResult.OK)
            //{

            //    myPrintDocument1.Print();
            //}

            #endregion SofiStyle


            #region MAYISOĞLU
            //int index = gridView1.FocusedRowHandle;
            //string deger1 = gridView1.GetRowCellValue(index, "MƏHSUL BARKOD").ToString();
            //string deger2 = gridView1.GetRowCellValue(index, "MƏHSUL ADI").ToString();
            //string deger3 = gridView1.GetRowCellValue(index, "SATIŞ QİYMƏTİ").ToString();

            //productname = deger2;
            //productprice = deger3;
            //barcodesa = deger1;
            //barkodc(deger1, index, deger2, deger3);
            //System.Drawing.Printing.PrintDocument myPrintDocument1 = new System.Drawing.Printing.PrintDocument();




            //PrintDialog myPrinDialog1 = new PrintDialog();

            //myPrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printbarkod);


            //myPrinDialog1.Document = myPrintDocument1;


            //PrintDocument pd = new PrintDocument();
            //PrinterSettings settings = new PrinterSettings();
            //PageSettings pageSettings = new PageSettings(settings);
            //PaperSize paperSize = settings.DefaultPageSettings.PaperSize;

            //pd.PrinterSettings = settings;
            //pd.DefaultPageSettings = new PageSettings();
            //pd.DefaultPageSettings.PaperSize = paperSize;
            //pd.PrintPage += new PrintPageEventHandler(printbarkod);
            //myPrinDialog1.Document = pd;

            //pd.Print();

            #endregion MAYISOĞLU

            int focusedRowHandle = this.gridView1.FocusedRowHandle;
            string data = this.gridView1.GetRowCellValue(focusedRowHandle, "MƏHSUL BARKOD").ToString();
            string urundata = this.gridView1.GetRowCellValue(focusedRowHandle, "MƏHSUL ADI").ToString();
            string data3 = this.gridView1.GetRowCellValue(focusedRowHandle, "SATIŞ QİYMƏTİ").ToString();
            this.productname = urundata;
            this.productprice = data3;
            this.barcodesa = data;
            this.barkodc(data, focusedRowHandle, urundata, data3);
            PrintDocument printDocument = new PrintDocument();
            PrintDialog printDialog = new PrintDialog();
            printDocument.PrintPage += new PrintPageEventHandler(this.printbarkod);
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() != DialogResult.OK)
                return;
            printDocument.Print();

        }

        void printbarkod(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region    SofiStyle Barkod çap
            //Font font = new System.Drawing.Font("Times New Roman", 7f);
            //Font font2 = new System.Drawing.Font("Times New Roman", 8.75f, FontStyle.Bold);
            //Font font40 = new System.Drawing.Font("Times New Roman", 10f, FontStyle.Bold);
            //Font font4 = new System.Drawing.Font("Times New Roman", 8f, FontStyle.Bold);
            //Font font3 = new System.Drawing.Font("Times New Roman", 8.75f);
            //Font f8 = new System.Drawing.Font("Times New Roman", 9f);
            //Font f80 = new System.Drawing.Font("Times New Roman", 7f);
            //Font f9 = new System.Drawing.Font("Times New Roman", 10f, FontStyle.Bold);
            //Font fonta = new System.Drawing.Font("Times New Roman", 8f, FontStyle.Bold);
            //Font price = new Font("Times New Roman", 12, FontStyle.Bold);
            //Font oldPriceFont = new Font("Times New Roman", 8, FontStyle.Strikeout);

            //int offset = 185;
            //int offset2 = 10;
            //int m = 0;
            //int n = 20;

            //StringFormat sf = new StringFormat();
            //sf.LineAlignment = StringAlignment.Center;
            //sf.Alignment = StringAlignment.Center;


            //Rectangle rect2 = new Rectangle(20, 10, 80, 40);
            ////Bitmap myBitmap1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            ////pictureBox1.DrawToBitmap(myBitmap1, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            //Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            //var barcodeImage = barcode.Draw(barkodsa, 70, 2);

            ////e.Graphics.DrawImage(myBitmap1, 0, 0);
            //Image instagramLogo = Properties.Resources.instagram_logo__1_;

            //float logoHeight = fonta.Height;
            //float logoWidth = logoHeight; // Kare formunda olmasını sağlamak için genişliği de aynı yapıyoruz




            ////myBitmap1.Dispose();
            //e.Graphics.DrawString(tedarikci, f9, Brushes.Black, 50, 10);
            //if (discountPrice != "0,000" && discountPrice != "")
            //{
            //    e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(oldPrice)) + " ₼", oldPriceFont, Brushes.Black, 60, 55);
            //    e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(discountPrice)) + " ₼", price, Brushes.Red, 55, 70);
            //}
            //else
            //{
            //    e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(fiyat)) + " ₼", price, Brushes.Black, 55, 55);
            //}
            //e.Graphics.DrawString($"{urunad} - {urunkod}", f8, Brushes.Black, new RectangleF(30F, 100f, 115f, 40f), sf);
            //e.Graphics.DrawImage(instagramLogo, 30, 150, logoWidth, logoHeight); // Logonun boyutlarını ve konumunu ayarlayın
            //e.Graphics.DrawString("sofi_style_boutique", fonta, Brushes.Black, 45, 150);
            //e.Graphics.DrawImage(barcodeImage, 45, 165, width: 100, height: 40);
            //e.Graphics.DrawString(barkodsa, f80, Brushes.Black, 45, 210);
            //e.Graphics.RotateTransform(90);
            #endregion    SofiStyle Barkod çap


            //Font font = new System.Drawing.Font("Arial 4", 6f);
            //Font priceFont = new Font("Arial 8", 7F, FontStyle.Bold);
            //Font font2 = new System.Drawing.Font("Arial 8", 6f);
            //Bitmap myBitmap1 = new Bitmap(pictureBox1.Width, pictureBox1.Height + 10);
            //double price = double.Parse(productprice);
            //var img = new Bitmap(pictureBox1.Width, pictureBox1.Height, System.Drawing.Imaging.PixelFormat.Format48bppRgb);
            //img.MakeTransparent();


            //using (var g = Graphics.FromImage(img))
            //{
            //    // send that graphics object to the rendering code
            //    RenderBarcodeInfoToGraphics(g, barcodesa, productname + "-" + price.ToString("C2"),
            //        new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            //}



            //StringFormat sf = new StringFormat();
            //sf.LineAlignment = StringAlignment.Near;
            //sf.Alignment = StringAlignment.Near;


            ////MƏHSUL ADI
            //float product = e.Graphics.MeasureString(productname, font).Width;
            //e.Graphics.DrawString(productname, font, Brushes.Black, new RectangleF(.5F, .5F, 200, product), sf);


            ////SATIŞ QİYMƏTİ
            //float priceWidth = e.Graphics.MeasureString(price.ToString("C2"), priceFont).Width;
            //e.Graphics.DrawString(price.ToString("C2"), priceFont, Brushes.Black, new RectangleF(40F, 14F, priceWidth + 25F, 30F), sf);

            //float logoWidth = img.Width - 20F;
            //float logoHeight = 26F;
            //float logoX = 2F; // Logo X location
            //float logoY = 24F; // Logo Y location

            ////BARKOD IMAGE
            //e.Graphics.DrawImage(img, logoX, logoY, logoWidth, logoHeight);



            ////BARKOD
            //float barcodeWidth = e.Graphics.MeasureString(barcodesa, font2).Width;
            //e.Graphics.DrawString(barcodesa, font2, Brushes.Black, new RectangleF(45F, 50F, barcodeWidth, 15F), sf);


            //myBitmap1.Dispose();










            Font font1 = new Font("Arial 12", 10f);
            Font font2 = new Font("Arial 12", 16f, FontStyle.Bold);
            Font font3 = new Font("Arial 12", 6f);
            Bitmap bitmap1 = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height + 30);
            double num = double.Parse(this.productprice);
            Bitmap bitmap2 = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height, PixelFormat.Format48bppRgb);
            bitmap2.MakeTransparent();
            using (Graphics g = Graphics.FromImage((Image)bitmap2))
                RibbonForm1.RenderBarcodeInfoToGraphics(g, this.barcodesa, this.productname + "-" + num.ToString("C2"), new Rectangle(0, 0, this.pictureBox1.Width, this.pictureBox1.Height));
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Near;
            format.Alignment = StringAlignment.Near;
            float width1 = e.Graphics.MeasureString(this.productname, font1).Width;
            e.Graphics.DrawString(this.productname, font1, Brushes.Black, new RectangleF(10f, 5f, 200f, width1), format);
            SizeF sizeF = e.Graphics.MeasureString(num.ToString("C2"), font2);
            float width2 = sizeF.Width;
            e.Graphics.DrawString(num.ToString("C2"), font2, Brushes.Black, new RectangleF(100f, 53f, width2 + 25f, 30f), format);
            float width3 = (float)bitmap2.Width - 20f;
            float height = 60f;
            float x = 20f;
            float y = 75f;
            e.Graphics.DrawImage((Image)bitmap2, x, y, width3, height);
            sizeF = e.Graphics.MeasureString(this.barcodesa, font3);
            float width4 = sizeF.Width;
            e.Graphics.DrawString(this.barcodesa, font3, Brushes.Black, new RectangleF(100f, 120f, width4, 15f), format);
            bitmap1.Dispose();
















        }

        private static void RenderBarcodeInfoToGraphics(Graphics g, string code, string info, Rectangle rect)
        {
            // Constants to make numbers a little less magical
            const int barcodeHeight = 70;
            const int marginTop = 10;
            const string codeFontFamilyName = "Courier New";
            const int codeFontEmSize = 8;
            const int marginCodeFromCode = 10;
            const string infoFontFamilyName = "Arial";
            const int infoFontEmSize = 12;
            const int marginInfoFromCode = 10;

            // white background
            g.Clear(Color.White);

            // We want to make sure that when it draws, the renderer doesn't compensate
            // for images scaling larger by blurring the image. This will leave your
            // bars crisp and clean no matter how high the DPI is
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

            // generate barcode
            using (var img = BarcodeDrawFactory.Code128WithChecksum.Draw(code, barcodeHeight))
            {
                // daw the barcode image
                g.DrawImage(img,
                    new Point(rect.X + (rect.Width / 2 - img.Width / 2), rect.Y + marginTop));
            }

            // now draw the code under the bar code
            using (var br = new SolidBrush(Color.Black))
            {
                // calculate starting position of text from the top
                var yPos = rect.Y + marginTop + barcodeHeight + marginCodeFromCode;

                // align text to top center of area
                var sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near
                };

                // draw the code, saving the height of the code text
                var codeTextHeight = 0;
                using (var font =
                    new Font(codeFontFamilyName, codeFontEmSize, FontStyle.Regular))
                {
                    codeTextHeight = (int)Math.Round(g.MeasureString(code, font).Height);


                }

                // draw the info below the code
                using (var font =
                    new Font(infoFontFamilyName, infoFontEmSize, FontStyle.Regular))
                {
                    g.DrawString(info, font, br,
                        new Rectangle(rect.X,
                            yPos + codeTextHeight + marginInfoFromCode, rect.Width, 0), sf);
                }
            }
        }

        void barkodc(string data, int data2, string urundata, string data3)
        {
            this.pictureBox1.Image?.Dispose();
            Bitmap bitmap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height, PixelFormat.Format48bppRgb);
            string code = data;
            if (!(code != ""))
                return;
            using (Graphics g = Graphics.FromImage((Image)bitmap))
                RibbonForm1.RenderBarcodeInfoToGraphics(g, code, this.productname + "-" + this.productprice + " AZN", new Rectangle(0, 0, this.pictureBox1.Width, this.pictureBox1.Height));
            this.pictureBox1.Image = (Image)bitmap;


            //// if there was a previous image in the picture box, dispose of it now
            //pictureBox1.Image?.Dispose();

            //// create a 24 bit image that is the size of your picture box
            //var img = new Bitmap(pictureBox1.Width, pictureBox1.Height, System.Drawing.Imaging.PixelFormat.Format48bppRgb);
            //// wrap it in a graphics object


            //string deger1 = data;
            //string urunadi = urundata;
            //string urunfiyat = data3;
            //int index = data2;
            //if (deger1 != "")
            //{

            //    using (var g = Graphics.FromImage(img))
            //    {
            //        // send that graphics object to the rendering code
            //        RenderBarcodeInfoToGraphics(g, deger1, productname + "-" + productprice + " AZN",
            //            new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            //    }

            //    // set the new image in the picture box
            //    pictureBox1.Image = img;
            //}
        }

        void barkodc2(string data, int data2, string data3, string data4, string data5)
        {
            string deger1 = data;
            string techizatci = data3;
            string qiymet = data4;
            string mad = data5;
            int index = data2;
            if (deger1 != "")
            {
                Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                var barcodeImage = barcode.Draw(deger1, 70, 2);

                var resultImage = new Bitmap(barcodeImage.Width + 5, barcodeImage.Height + 25); // 20 is bottom padding, adjust to your text

                using (var graphics = Graphics.FromImage(resultImage))
                using (var font = new Font("Consolas", 10))
                using (var font2 = new Font("Consolas", 12))
                using (var brush = new SolidBrush(Color.Black))
                using (var format = new StringFormat()
                {
                    FormatFlags = StringFormatFlags.DirectionVertical,
                    Alignment = StringAlignment.Center, // Also, horizontally centered text, as in your example of the expected output
                    LineAlignment = StringAlignment.Far
                })
                {
                    graphics.Clear(Color.White);
                    graphics.DrawString(techizatci, font2, brush, resultImage.Width / 2, resultImage.Height, format);
                    graphics.DrawString(qiymet + " ₼", font2, brush, resultImage.Width / 2, resultImage.Height, format);
                    graphics.DrawString(mad, font, brush, resultImage.Width / 2, resultImage.Height, format);
                    graphics.DrawImage(barcodeImage, 5, 5);
                    graphics.DrawString(deger1, font, brush, resultImage.Width / 2, resultImage.Height, format);
                }

                pictureBox1.Image = resultImage;



                /*    MessageBox.Show("The button from the " + gridView1.GetRowCellValue(index, "MƏHSUL BARKOD").ToString() + " row has been clicked!"); */





            }

        }

        private void AddUnboundColumn()
        {
            GridColumn unbColumn = gridView1.Columns.AddField("Barkod Cap");
            unbColumn.VisibleIndex = gridView1.Columns.Count;
            unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
        }

        POS_LAYOUT_NEW Pp;

        private void barButtonItem45_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["POS_LAYOUT_NEW"] != null)
            {
                var Main = Application.OpenForms["POS_LAYOUT_NEW"] as POS_LAYOUT_NEW;
                if (Main != null)
                {

                }
                //    Main.Close();
            }
            else
            {
                Pp = new POS_LAYOUT_NEW();
                Pp.Show();

            }
        }

        private void barButtonItem32_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem40_ItemClick(object sender, ItemClickEventArgs e)
        {
            ANBAR_MENFEET AMF = new ANBAR_MENFEET();
            AMF.Show();
        }

        private void barButtonItem44_ItemClick(object sender, ItemClickEventArgs e)
        {
            USERQEYDIYYAT_LAYOUT ug = new USERQEYDIYYAT_LAYOUT();
            ug.Show();
            //UserQeydiyyat ug = new UserQeydiyyat();
            //ug.Show();
        }

        private void barButtonItem37_ItemClick(object sender, ItemClickEventArgs e)
        {
            //alis hesabati
            MEHSUL_ALIS_HESABATI MH = new MEHSUL_ALIS_HESABATI();
            MH.Show();
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMessageBox.Show("BU MODUL DEAKTİV EDİLMİŞDİR ,SERVİS XİDMƏTİNƏ MÜRACİƏT EDİN");
        }


        private void barButtonItem47_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm<fKassalar>();
        }

        private void barButtonItem48_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem49_ItemClick(object sender, ItemClickEventArgs e)
        {
            //UMUMI SATIS HESABATI

            UMUMI_SATIS_HESABATI U = new UMUMI_SATIS_HESABATI();
            U.Show();
        }

        private void barButtonItem50_ItemClick(object sender, ItemClickEventArgs e)
        {
            BANK_NEGD_HESABAT BH = new BANK_NEGD_HESABAT();
            BH.Show();
        }

        private void barButtonItem52_ItemClick(object sender, ItemClickEventArgs e)
        {
            //EXCELL IMPORT 
            EXCELL_IMPORT e_im = new EXCELL_IMPORT();
            e_im.Show();

        }

        private void barButtonItem54_ItemClick(object sender, ItemClickEventArgs e)
        {
            //MEHSUL ALISI
        }

        private void barButtonItem55_ItemClick(object sender, ItemClickEventArgs e)
        {
            //MEHSUL GAYTARMA HESABATI
            MEHSUL_GAYTARMA_HESABAT MGS = new MEHSUL_GAYTARMA_HESABAT();
            MGS.Show();
        }

        private void barButtonItem56_ItemClick(object sender, ItemClickEventArgs e)
        {
            //TECHIZATCI ODENISI HESABATI
            TECHIZATCI_ODENISI_HESABATI TOB = new TECHIZATCI_ODENISI_HESABATI();
            TOB.Show();
        }

        private void barButtonItem57_ItemClick(object sender, ItemClickEventArgs e)
        {
            IZAHLI_MEHSUL_SATISI IM = new IZAHLI_MEHSUL_SATISI();
            IM.Show();
        }

        private void barButtonItem58_ItemClick(object sender, ItemClickEventArgs e)
        {
            izahli_mehsul_gaytarma igm = new izahli_mehsul_gaytarma();
            igm.Show();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //  EXCELL
            try
            {
                string path = "output.xlsx";
                gridControl1.ExportToXlsx(path);
                // Open the created XLSX file with the default application. 
                Process.Start(path);
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            getall();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            //  EXCELL
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Excel faylı|*.xlsx";
                saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveFile.OverwritePrompt = true; //varsa soruşmadan üstünə yazması üçün false olaraq qalmalıdır
                saveFile.FileName = $"Anbar Qalığı_{DateTime.Now.DayOfYear}.xlsx";
                if (saveFile.ShowDialog() is DialogResult.OK)
                {
                    gridControl1.ExportToXlsx(saveFile.FileName);
                    FormHelpers.Log("Anbar qalığı export edildi");
                    Process.Start(saveFile.FileName);
                }



                //string path = "output.xlsx";
                //gridControl1.ExportToXlsx(path);
                //Process.Start(path);
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }
        MUSTERI_ODENISLERI MOS;
        private void barButtonItem59_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["MUSTERI_ODENISLERI"] != null)
            {
                var Main = Application.OpenForms["MUSTERI_ODENISLERI"] as MUSTERI_ODENISLERI;
                if (Main != null)
                {

                }
                // Main.Close();
            }
            else
            {
                MOS = new MUSTERI_ODENISLERI(u_id);
                MOS.Show();

            }
        }

        MUSTERI_ODENIS_HESABAT MOH;
        private void barButtonItem60_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["MUSTERI_ODENIS_HESABAT"] != null)
            {
                var Main = Application.OpenForms["MUSTERI_ODENIS_HESABAT"] as MUSTERI_ODENIS_HESABAT;
                if (Main != null)
                {

                }
                // Main.Close();
            }
            else
            {
                MOH = new MUSTERI_ODENIS_HESABAT();
                MOH.Show();

            }
        }
        techizatci_odenisleri_hesabar TH;
        private void barButtonItem61_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["techizatci_odenisleri_hesabar"] != null)
            {
                var Main = Application.OpenForms["techizatci_odenisleri_hesabar"] as techizatci_odenisleri_hesabar;
                if (Main != null)
                {

                }
                // Main.Close();
            }
            else
            {
                TH = new techizatci_odenisleri_hesabar();
                TH.Show();

            }
        }

        private void barButtonItem62_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["MUSTERILER_LAYOUT"] != null)
            {
                var Main = Application.OpenForms["MUSTERILER_LAYOUT"] as MUSTERILER_LAYOUT;
                if (Main != null)
                {

                }
                // Main.Close();
            }
            else
            {
                MS = new MUSTERILER_LAYOUT();
                MS.Show();
            }
        }

        private void barButtonItem63_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        KREDITSATISLAYOUTSA gsa;
        private void barButtonItem64_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["KREDITSATISLAYOUTSA"] != null)
            {
                var Main = Application.OpenForms["KREDITSATISLAYOUTSA"] as KREDITSATISLAYOUTSA;
                if (Main != null)
                {

                }
                // Main.Close();
            }
            else
            {
                //    gsa = new KREDITSATISLAYOUTSA(u_id, this);
                //   gsa.Show();
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void ribbonStatusBar_Click(object sender, EventArgs e)
        {

        }

        KREDITHESABATI khsp;

        private void barButtonItem65_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["KREDITHESABATI"] != null)
            {
                var Main = Application.OpenForms["KREDITHESABATI"] as KREDITHESABATI;
                if (Main != null)
                {

                }
                // Main.Close();
            }
            else
            {
                khsp = new KREDITHESABATI();
                khsp.Show();
            }
        }
        SearchKrediOdeme_LAYOUT sodeme;
        private void barButtonItem66_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["SearchKrediOdeme_LAYOUT"] != null)
            {
                var Main = Application.OpenForms["SearchKrediOdeme_LAYOUT"] as SearchKrediOdeme_LAYOUT;
                if (Main != null)
                {

                }
                // Main.Close();
            }
            else
            {
                //  sodeme = new SearchKrediOdeme_LAYOUT(u_id, this);
                //   sodeme.Show();
            }
        }


        KREDITODENISHESABAT1 khsp2;


        private void barButtonItem67_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["KREDITODENISHESABAT1"] != null)
            {
                var Main = Application.OpenForms["KREDITODENISHESABAT1"] as KREDITODENISHESABAT1;
                if (Main != null)
                {

                }
                // Main.Close();
            }
            else
            {
                khsp2 = new KREDITODENISHESABAT1();
                khsp2.Show();
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem42_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem43_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        fTereziler TR;

        private void barButtonItem70_ItemClick(object sender, ItemClickEventArgs e)
        {
            BankTTNM bt = new BankTTNM();
            bt.Show();
        }

        private void bLogs_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem68_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["fTereziler"] != null)
            {
                var Main = Application.OpenForms["fTereziler"] as fTereziler;
                if (Main != null)
                {

                }
            }
            else
            {
                TR = new fTereziler();
                TR.Show();
            }
        }

        private void barButtonItem69_ItemClick(object sender, ItemClickEventArgs e)
        {
            SqlConnection connection34 = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlConnection conn234 = new SqlConnection();
            SqlCommand cmd234 = new SqlCommand();
            conn234.ConnectionString = Properties.Settings.Default.SqlCon;
            conn234.Open();

            string message = "Excel faylına istəyə görə bütün məhsulları vəya KQ olan məhsulları yazdıra bilərsiniz.\n\n" +
                "Yes/Да - Bütün məhsulları yazdır\n" +
                "No/Нет - Vahidi KQ olan məhsulları yazdır\n" +
                "Cancel/Отмена - Ləğv et";

            DialogResult result = MessageBox.Show(message, "Mesaj", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            string query = string.Empty;
            if (result is DialogResult.Cancel)
            {
                return;
            }
            else if (result is DialogResult.Yes)
            {
                query = "exec InsertIntoTerazimalzemeAllProducts";
            }
            else if (result is DialogResult.No)
            {
                query = "exec InsertIntoTerazimalzemeFilteredByVahid";
            }

            cmd234.Connection = conn234;
            cmd234.CommandText = query;
            cmd234.CommandTimeout = 60;
            cmd234.ExecuteNonQuery();
            conn234.Close();

            gridView2.ClearSelection();
            gridControl2.RefreshDataSource();
            gridControl2.DataSource = null;


            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


            // Provide the query string with a parameter placeholder.
            string queryString =
                 // " exec  dbo.chart_report ";

                 " SELECT  ROW_NUMBER() OVER(ORDER BY [MƏHSUL ADI]) AS Hotkey,REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE([MƏHSUL ADI],N'Ə','E'),N'ə','e'),N'ı','i'),N'ü','u'),N'ğ','g'),N'Ğ','G' ),N'Ü','U'),N'Ş','S'),N'ş','s'),N'Ç','C'),N'ç','c')  as Name  ,[MAL_ALISI_DETAILS_ID] as LFCode, [MAL_ALISI_DETAILS_ID] as Code ,7 AS [Barcode Type],[SATIŞ QİYMƏTİ] AS [Unit Price],4 AS [Unit Weight],0 AS [Unit Amount] ,21 AS [Department],0 AS [PT Weight],15 AS [Shelf Time],0 AS [Pack Type],0 AS [Tare],	0 AS [Error(%)],	0 AS [Message1],	0 AS [Message2],	0 AS [Label],	0 AS [Discount/Table],	0 AS [Account],	0 AS [sPluFieldTitle20],	0 AS [Account],	0 AS [Recommend days],	0 AS [nutrition],	0 AS [Ice(%)] FROM[terazimalzeme] ";

            SqlCommand command = new SqlCommand(queryString, connection);
            //command.Parameters.AddWithValue("@pricepoint", D1_);
            //command.Parameters.AddWithValue("@pricepoint1", D2_);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //dataGridView1.DataSource= dt;
            //gridView1.Columns[0].Visible = false;
            //dataGridView1.Columns[2].Visible = false;
            //dataGridView1.Columns[0].Visible = false;

            gridControl2.DataSource = dt;


            //gridView2.ExportToText("terazidata.xls", new DevExpress.XtraPrinting.TextExportOptions { Encoding = System.Text.Encoding.UTF8 });

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Excel faylı|*.xls";
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFile.OverwritePrompt = true; //varsa soruşmadan üstünə yazması üçün false olaraq qalmalıdır
            saveFile.FileName = "terazidata.xls";
            if (saveFile.ShowDialog() is DialogResult.OK)
            {
                gridView2.ExportToCsv(saveFile.FileName, new DevExpress.XtraPrinting.CsvExportOptions { Separator = "\t" });
            }
        }

        void barkodc(string data, int data2, string fiyat, string tedarikci, string urunad)
        {
            string deger1 = data;
            int index = data2;
            if (deger1 != "")
            {
                Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                var barcodeImage = barcode.Draw(deger1, 70, 2);

                var resultImage = new Bitmap(barcodeImage.Width + 5, barcodeImage.Height + 25); // 20 is bottom padding, adjust to your text

                using (var graphics = Graphics.FromImage(resultImage))
                using (var font = new Font("Consolas", 12))
                using (var brush = new SolidBrush(Color.Black))
                using (var format = new StringFormat()
                {
                    Alignment = StringAlignment.Center, // Also, horizontally centered text, as in your example of the expected output
                    LineAlignment = StringAlignment.Far
                })
                {
                    graphics.Clear(Color.White);
                    graphics.DrawString(tedarikci, font, brush, resultImage.Width / 2, 5, format);
                    graphics.DrawImage(barcodeImage, 5, 10);
                    graphics.DrawString(deger1, font, brush, resultImage.Width / 2, resultImage.Height, format);
                }

                pictureBox1.Image = resultImage;



                /*    MessageBox.Show("The button from the " + gridView1.GetRowCellValue(index, "MƏHSUL BARKOD").ToString() + " row has been clicked!"); */





            }

        }




    }
}