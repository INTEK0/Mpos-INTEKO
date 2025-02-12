using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.BarCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using Zen.Barcode;
using static DevExpress.Diagram.Core.Native.Either;
using static WindowsFormsApp2.Helpers.Enums;

namespace WindowsFormsApp2.Forms
{
    public partial class fPrintBarcode : DevExpress.XtraEditors.XtraForm
    {
        private string _productName,
                       _salePrice,
                       _barcode,
                       _supplierName;

        private Bitmap _barcodeImage;

        private PrintType _printType = PrintType.medium;

        public fPrintBarcode()
        {
            InitializeComponent();
            GridDataLoad();
            PrintTypeLoad();
        }

        private void PrintTypeLoad()
        {
            var data = Enum.GetValues(typeof(PrintType))
                       .Cast<PrintType>()
                       .Select(x => new
                       {
                           Key = (int)x,
                           Value = GetEnumDescription(x)
                       })
                       .ToList();


            lookPrintType.Properties.DataSource = data;
            lookPrintType.Properties.ValueMember = "Key";
            lookPrintType.Properties.DisplayMember = "Value";
            lookPrintType.Properties.ForceInitialize();
            lookPrintType.EditValue = _printType;
        }

        private void GridDataLoad()
        {
            var data = DbProsedures.ConvertToDataTable("exec dbo.gaime_Satis_mal_load");
            gridControlProducts.DataSource = data;
            gridProducts.GroupPanelText = $"Məhsul sayı: {data.Rows.Count}";
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            GridDataLoad();
        }

        private void bPrintToExcel_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControlProducts, "Anbar Qalığı");
        }

        private void bPrint_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridProducts.GetSelectedRows();

            foreach (var rowHandle in selectedRows)
            {
                string barcode = gridProducts.GetRowCellValue(rowHandle, colBarcode).ToString();
                string name = gridProducts.GetRowCellValue(rowHandle, colProductName).ToString();
                string salesPrice = gridProducts.GetRowCellValue(rowHandle, coLSalePrice).ToString();

                _productName = name;
                _salePrice = salesPrice;
                _barcode = barcode;

                _printType = (PrintType)lookPrintType.EditValue;
                _barcodeImage = GenerateBarcode(_barcode, 0, _productName, _salePrice);

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings = new PageSettings
                {
                    PaperSize = new PrinterSettings().DefaultPageSettings.PaperSize
                };
                pd.DocumentName = $"Name_{name} - Barkod_{barcode}";
                pd.PrintPage += new PrintPageEventHandler(printbarkod);
                pd.PrinterSettings.PrinterName = new System.Drawing.Printing.PrinterSettings().PrinterName;


                PrintDialog PrintDialog1 = new PrintDialog
                {
                    Document = pd
                };

                pd.Print();
            }
            this.Show();
        }

        private void bGridPrint_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int rowHandle = gridProducts.FocusedRowHandle;

            string barcode = gridProducts.GetRowCellValue(rowHandle, colBarcode).ToString();
            string name = gridProducts.GetRowCellValue(rowHandle, colProductName).ToString();
            string salesPrice = gridProducts.GetRowCellValue(rowHandle, coLSalePrice).ToString();

            _productName = name;
            _salePrice = salesPrice;
            _barcode = barcode;

            _printType = (PrintType)lookPrintType.EditValue;
            _barcodeImage = GenerateBarcode(_barcode, 0, _productName, _salePrice);

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings = new PageSettings
            {
                PaperSize = new PrinterSettings().DefaultPageSettings.PaperSize
            };
            pd.DocumentName = $"Name_{name} - Barkod_{barcode}";
            pd.PrintPage += new PrintPageEventHandler(printbarkod);
            pd.PrinterSettings.PrinterName = new System.Drawing.Printing.PrinterSettings().PrinterName;


            PrintDialog PrintDialog1 = new PrintDialog
            {
                Document = pd
            };

            pd.Print();
        }

        void printbarkod(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (_printType is PrintType.minimum)
            {
                #region MAYISOĞLU BARKOD ÇAP
                Font font = new System.Drawing.Font("Arial 4", 6f);
                Font priceFont = new Font("Arial 8", 7F, FontStyle.Bold);
                Font font2 = new System.Drawing.Font("Arial 8", 6f);
                Bitmap myBitmap1 = new Bitmap(_barcodeImage.Width, _barcodeImage.Height + 10);
                double price = double.Parse(_salePrice);
                var img = new Bitmap(_barcodeImage.Width, _barcodeImage.Height, System.Drawing.Imaging.PixelFormat.Format48bppRgb);
                img.MakeTransparent();


                using (var g = Graphics.FromImage(img))
                {
                    // send that graphics object to the rendering code
                    RenderBarcodeInfoToGraphics(g, _barcode, _productName + "-" + price.ToString("C2"),
                        new Rectangle(0, 0, _barcodeImage.Width, _barcodeImage.Height));
                }



                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Near;
                sf.Alignment = StringAlignment.Near;


                //MƏHSUL ADI
                float product = e.Graphics.MeasureString(_productName, font).Width;
                e.Graphics.DrawString(_productName, font, Brushes.Black, new RectangleF(1F, .5F, 200, product), sf);


                //SATIŞ QİYMƏTİ
                float priceWidth = e.Graphics.MeasureString(price.ToString("C2"), priceFont).Width;
                e.Graphics.DrawString(price.ToString("C2"), priceFont, Brushes.Black, new RectangleF(50F, 14F, priceWidth + 25F, 30F), sf);

                float logoWidth = img.Width - 20F;
                float logoHeight = 26F;
                float logoX = 2F; // Logo X location
                float logoY = 24F; // Logo Y location

                //BARKOD IMAGE
                e.Graphics.DrawImage(img, logoX, logoY, logoWidth, logoHeight);



                //BARKOD
                float barcodeWidth = e.Graphics.MeasureString(_barcode, font2).Width;
                e.Graphics.DrawString(_barcode, font2, Brushes.Black, new RectangleF(50F, 50F, barcodeWidth, 15F), sf);


                myBitmap1.Dispose();
                #endregion MAYISOĞLU BARKOD ÇAP
            }
            else if (_printType is PrintType.medium)
            {
                #region RASİM MARKET ÇAP
                Font font1 = new Font("Arial", 10f);
                Font font2 = new Font("Arial", 16f, FontStyle.Bold);
                Font font3 = new Font("Arial", 6f);

                Bitmap bitmap2 = new Bitmap(_barcodeImage.Width, _barcodeImage.Height, PixelFormat.Format48bppRgb);
                bitmap2.MakeTransparent();

                if (!double.TryParse(_salePrice, out double salePrice))
                {
                    FormHelpers.Alert("Satış qiyməti düzgün daxil edilməyib", Enums.MessageType.Error);
                    return;
                }

                using (Graphics g = Graphics.FromImage(bitmap2))
                {
                    RenderBarcodeInfoToGraphics(
                        g,
                        _barcode,
                        $"{_productName} - {salePrice:C2}",
                        new Rectangle(0, 0, _barcodeImage.Width, _barcodeImage.Height)
                    );
                }

                StringFormat format = new StringFormat
                {
                    LineAlignment = StringAlignment.Near,
                    Alignment = StringAlignment.Near
                };

                //Məhsulun adı
                float productNameWidth = e.Graphics.MeasureString(_productName, font1).Width;
                e.Graphics.DrawString(
                    _productName,
                    font1,
                    Brushes.Black,
                    new RectangleF(3f, 3f, 200f, productNameWidth),
                    format
                );

                //Satış qiyməti
                SizeF priceSize = e.Graphics.MeasureString(salePrice.ToString("C2"), font2);
                float priceWidth = priceSize.Width;
                e.Graphics.DrawString(
                    salePrice.ToString("C2"),
                    font2,
                    Brushes.Black,
                    new RectangleF(100f, 50f, priceWidth + 25f, 30f),
                    format
                );

                //Barkod şəkli
                float barcodeWidth = (float)bitmap2.Width - 20f;
                float barcodeHeight = 60f;
                float barcodeX = 20f;
                float barcodeY = 75f;
                e.Graphics.DrawImage(bitmap2, barcodeX, barcodeY, barcodeWidth, barcodeHeight);

                //barkod text
                SizeF barcodeTextSize = e.Graphics.MeasureString(_barcode, font3);
                float barcodeTextWidth = barcodeTextSize.Width;
                e.Graphics.DrawString(
                    _barcode,
                    font3,
                    Brushes.Black,
                    new RectangleF(100f, 125f, barcodeTextWidth, 15f),
                    format
                );

                #endregion RASİM MARKET ÇAP
            }
            else if (_printType is PrintType.maximum)
            {
                #region SOFİSTYLE BARKOD ÇAP
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
                //var barcodeImage = barcode.Draw(_barcode, 70, 2);

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
                //e.Graphics.DrawString(_barcode, f80, Brushes.Black, 45, 210);
                //e.Graphics.RotateTransform(90);

                #endregion SOFİSTYLE BARKOD ÇAP
            }
        }

        private Bitmap GenerateBarcode(string barcode, int data2, string productName, string salePrice)
        {
            Bitmap bitmap = new Bitmap(175, 100, PixelFormat.Format48bppRgb);
            if (!(barcode != ""))
                return null;
            using (Graphics g = Graphics.FromImage((Image)bitmap))
                RenderBarcodeInfoToGraphics(g, barcode, _productName + "-" + _salePrice + " AZN",
                    new Rectangle(0, 0, 175, 100));
            return bitmap;
        }

        private void RenderBarcodeInfoToGraphics(Graphics g, string code, string info, Rectangle rect)
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
    }
}