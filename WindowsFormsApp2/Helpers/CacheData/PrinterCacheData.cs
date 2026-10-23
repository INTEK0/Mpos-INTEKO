using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using Zen.Barcode;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using WindowsFormsApp2.Forms;

namespace WindowsFormsApp2.Helpers.CacheData
{
    public class PrinterCacheData
    {
        public static string ReplaceChars(string input)
        {
            if (input == null)
                return string.Empty;

            return input
                .Replace("ç", "c").Replace("Ç", "C")
                .Replace("ğ", "g").Replace("Ğ", "G")
                .Replace("ı", "i").Replace("İ", "I")
                .Replace("ö", "o").Replace("Ö", "O")
                .Replace("ş", "s").Replace("Ş", "S")
                .Replace("ü", "u").Replace("Ü", "U")
                .Replace("ə", "e").Replace("Ə", "E");
        }

        public static void PrintLabel45x25(string _company, string _product, string _price, string _barcode, string printerName)
        {
            string companyName = ReplaceChars(_company);
            string productName = ReplaceChars(_product);

            double priceY = 140;
            double aznY = priceY + 25;
            double productY = 35;
            int lineSpacing = 25;

            List<string> productLines = SplitProductName(productName, 12);
            if (productLines.Count > 3)
                productLines = productLines.Take(3).ToList();

            string tsplCommand = $@"
SIZE 45 mm, 25 mm
GAP 2 mm, 0
DENSITY 10
SPEED 4
DIRECTION 1
CLS

REM === Company Name: Centered at top ===
TEXT 110,10,""3"",0,1,1,""{companyName}"" 

REM === Draw line under company name ===
BAR 0,30,420,2
";

            for (int i = 0; i < productLines.Count; i++)
            {
                tsplCommand += $@"
REM === Product name, line {i + 1} ===
TEXT 10,{productY + (i * lineSpacing)},""3"",0,1,1,""{productLines[i]}""
";
            }

            tsplCommand += $@"
REM === Price ===
TEXT 10,{priceY},""3"",0,1,1,""{_price}""

REM === AZN ===
TEXT 10,{aznY},""3"",0,1,1,""AZN""

REM === Barcode ===
BARCODE  160,130,""128"",60,1,0,2,2,""{_barcode}""

PRINT 1,1
";

            bool result = RawPrinterHelper.SendStringToPrinter(printerName, tsplCommand);
        }

        public static void PrintLabel60x40(string _company, string _product, string _price, string _barcode, string printerName)
        {
            string companyName = ReplaceChars(_company);
            string productName = ReplaceChars(_product);

            double priceY = 220;
            double aznY = priceY + 40;
            //double productY = 55;
            //if (productName.Length > 19)
            //{
            //    productY += 25;
            //}      

            #region [..MƏHSUL ADINDA LİMİT OLMAYAN KOD (Uzun olduqda alt sətirə keçmir)..]

            //            string tsplCommand = $@"
            //SIZE 60 mm, 40 mm
            //GAP 2 mm, 0
            //DENSITY 10
            //SPEED 4
            //DIRECTION 1
            //CLS

            //REM === Company Name: Centered at top ===
            //TEXT 150,15,""3"",0,1,1,""{companyName}""

            //REM === Draw line under company name ===
            //BAR 0,45,580,2

            //REM === Product name under the line, left aligned, large area ===
            //TEXT 10,55,""3"",0,1,1,""{productName}""

            //REM === Price at bottom-left ===
            //TEXT 15,{priceY},""4"",0,1,1,""{_price}""


            //REM === Price at bottom-left ===
            //TEXT 15,{aznY},""4"",0,1,1,""AZN""

            //REM === Barcode at bottom-right ===
            //BARCODE  180,200,""128"",80,1,0,2,2,""{_barcode}""

            //PRINT 1,1
            //";

            #endregion [..MƏHSUL ADINDA LİMİT OLMAYAN KOD (Uzun olduqda alt sətirə keçmir)..]


            List<string> productLines = SplitProductName(productName, 16);
            if (productLines.Count > 3)
                productLines = productLines.Take(3).ToList();

            double productY = 55;
            int lineSpacing = 40;

            string tsplCommand = $@"
SIZE 60 mm, 40 mm
GAP 2 mm, 0
DENSITY 10
SPEED 4
DIRECTION 1
CLS

REM === Company Name: Centered at top ===
TEXT 150,15,""3"",0,1,1,""{companyName}"" 

REM === Draw line under company name ===
BAR 0,45,580,2
";

            // Məhsulun adını hər sətir üçün yazdırılır. (Maksimum 4 sətir)
            for (int i = 0; i < productLines.Count; i++)
            {
                tsplCommand += $@"
REM === Product name, line {i + 1}, left aligned ===
TEXT 10,{productY + (i * lineSpacing)},""4"",0,1,1,""{productLines[i]}""
";
            }

            // Qiyməti və AZN yazdırılır
            tsplCommand += $@"
REM === Price at bottom-left ===
TEXT 15,{priceY},""4"",0,1,1,""{_price}""

REM === AZN text ===
TEXT 15,{aznY},""4"",0,1,1,""AZN""

REM === Barcode at bottom-right ===
BARCODE  180,200,""128"",80,1,0,2,2,""{_barcode}""
PRINT 1,1
";


            bool result = RawPrinterHelper.SendStringToPrinter(printerName, tsplCommand);
        }

        public static void PrintLabel30x20(string _product, string _price, string _barcode, string printerName, Enums.BarcodeType barcodeType)
        {
            string productName = ReplaceChars(_product);

            //double productY = 10;
            //if (productName.Length > 16)
            //{
            //    productY += 15;
            //}

            List<string> productLines = SplitProductName(productName, 16);
            if (productLines.Count > 2)
                productLines = productLines.Take(2).ToList();

            double productY = 10;
            int lineSpacing = 22;

            string tsplCommand = $@"
SIZE 30 mm, 20 mm
GAP 2 mm, 0
DENSITY 10
SPEED 4
DIRECTION 1
CLS";
            // Məhsulun adını hər sətir üçün yazdırılır. (Maksimum 2 sətir)
            for (int i = 0; i < productLines.Count; i++)
            {
                tsplCommand += $@"
REM === Product name, line {i + 1}, left aligned ===
TEXT 5,{productY + (i * lineSpacing)},""2"",0,1,1,""{productLines[i]}""
";
            }

            // Qiyməti və AZN yazdırılır
            tsplCommand += $@"
REM === Price ===
TEXT 58,135,""2"",0,1,1,""{_price} AZN""

REM === Barcode ===
BARCODE  7,50,""{Enums.GetEnumDescription(barcodeType)}"",40,2,0,2,2,""{_barcode}""

PRINT 1,1
";


            bool result = RawPrinterHelper.SendStringToPrinter(printerName, tsplCommand);
        }

        private static List<string> SplitProductName(string productName, int maxLength)
        {
            List<string> lines = new List<string>();

            for (int i = 0; i < productName.Length; i += maxLength)
                lines.Add(productName.Substring(i, Math.Min(maxLength, productName.Length - i)));

            return lines;
        }

        public static void PrintLabel60x40Dress(string _company, string _product, string _price, string _barcode, string printerName, string discountPrice = "")
        {
            string companyName = ReplaceChars(_company ?? "").Replace("\"", "'").Trim();
            string productName = ReplaceChars(_product ?? "").Replace("\"", "'").Trim();
            string barcode = (_barcode ?? "").Trim();

            // 203 DPI:
            // Fiziki etiket: 60x40 mm = 480x320 dot
            // Saquli gorunus: 40x60 mm = 320x480 dot

            const int labelWidth = 480;
            const int portraitWidth = 320;

            if (string.IsNullOrWhiteSpace(barcode) || barcode.Any(c => c < 32 || c > 126 || c == '"'))
                throw new ArgumentException("Barkod duzgun deyil.");

            decimal normalPrice = ParseLabelPrice60x40(_price);

            decimal newPrice = string.IsNullOrWhiteSpace(discountPrice) ? 0 : ParseLabelPrice60x40(discountPrice);

            bool hasDiscount = newPrice > 0 && newPrice < normalPrice;

            // === SIRKET ADI ===

            int companyFont;
            int companyCharWidth;

            if (companyName.Length * 32 <= 280)
            {
                companyFont = 5;
                companyCharWidth = 32;
            }
            else if (companyName.Length * 24 <= 280)
            {
                companyFont = 4;
                companyCharWidth = 24;
            }
            else if (companyName.Length * 16 <= 280)
            {
                companyFont = 3;
                companyCharWidth = 16;
            }
            else
            {
                companyFont = 2;
                companyCharWidth = 12;
            }

            if (companyName.Length * companyCharWidth > 280)
                throw new ArgumentException("Sirket adi etikete sigmir.");

            int companyX = labelWidth - 40;
            int companyY = (portraitWidth - companyName.Length * companyCharWidth) / 2;

            // === MEHSUL ADI ===

            List<string> productLines = SplitProductName(productName, 16);

            int productFont = 3;
            int productCharWidth = 16;
            int lineSpacing = 29;

            if (productLines.Count > 3)
            {
                productLines = SplitProductName(productName, 23);
                productFont = 2;
                productCharWidth = 12;
                lineSpacing = 25;
            }

            if (productLines.Count > 3)
                throw new ArgumentException("Mehsul adi 3 setre sigmir.");

            // === QIYMET ===

            decimal finalPrice = hasDiscount ? newPrice : normalPrice;

            string priceText = FormatLabelPrice60x40(finalPrice);

            int priceFont = 5;
            int priceCharWidth = 32;

            int priceWidth = priceText.Length * priceCharWidth;
            int aznWidth = 3 * 16;

            int totalPriceWidth = priceWidth + 12 + aznWidth;

            if (totalPriceWidth > 280)
            {
                priceFont = 4;
                priceCharWidth = 24;
                priceWidth = priceText.Length * priceCharWidth;
                totalPriceWidth = priceWidth + 12 + aznWidth;
            }

            if (totalPriceWidth > 280)
                throw new ArgumentException("Qiymet etikete sigmir.");

            int priceStartY = (portraitWidth - totalPriceWidth) / 2;

            int priceX = hasDiscount
                ? labelWidth - 145
                : labelWidth - 124;

            int aznY = priceStartY + priceWidth + 12;

            // === BARKOD ENI ===

            bool numeric = barcode.All(char.IsDigit);

            int barcodeSymbols = numeric ? (barcode.Length % 2 == 0
                    ? barcode.Length / 2 + 2
                    : (barcode.Length - 1) / 2 + 4)
                : barcode.Length + 2;

            int barcodeWidth = (barcodeSymbols * 11 + 13) * 2;

            if (barcodeWidth > portraitWidth - 40)
                throw new ArgumentException("Barkod 40 mm enli dizayna sigmir.");

            int barcodeY = (portraitWidth - barcodeWidth) / 2;

            int barcodeTextFont = barcode.Length * 16 <= 280 ? 3 : 2;
            int barcodeTextCharWidth = barcodeTextFont == 3 ? 16 : 12;

            int barcodeTextWidth = barcode.Length * barcodeTextCharWidth;

            if (barcodeTextWidth > 280)
                throw new ArgumentException("Barkod nomresi etikete sigmir.");

            int barcodeTextY = (portraitWidth - barcodeTextWidth) / 2;

            // === TSPL ===

            string tsplCommand = $@"
SIZE 60 mm,40 mm
GAP 2 mm,0
DENSITY 10
SPEED 4
DIRECTION 1
REFERENCE 0,0
CODEPAGE 1254
CLS

REM === SIRKET ADI ===
TEXT {companyX},{companyY},""{companyFont}"",90,1,1,""{companyName}""

REM === SIRKET ADININ ALT XETTI ===
BAR 397,20,2,280
";

            // === ENDIRIMLI QIYMET ===

            if (hasDiscount)
            {
                string oldPriceText = FormatLabelPrice60x40(normalPrice) + " AZN";

                int oldWidth = oldPriceText.Length * 12;

                if (oldWidth > 280)
                    throw new ArgumentException("Kohne qiymet sigmir.");

                int oldY = (portraitWidth - oldWidth) / 2;

                tsplCommand += $@"
REM === KOHNE QIYMET ===
TEXT 368,{oldY},""2"",90,1,1,""{oldPriceText}""

REM === KOHNE QIYMETIN USTUNDEN XETT ===
BAR 358,{oldY},2,{oldWidth}
";
            }

            tsplCommand += $@"
REM === BOYUK QIYMET ===
TEXT {priceX},{priceStartY},""{priceFont}"",90,1,1,""{priceText}""

REM === AZN ===
TEXT {priceX - 12},{aznY},""3"",90,1,1,""AZN""
";

            // === MEHSUL ADI ===

            int productStartX = productLines.Count == 1 ? labelWidth - 250
                : productLines.Count == 2
                    ? labelWidth - 234
                    : labelWidth - 222;

            for (int i = 0; i < productLines.Count; i++)
            {
                string line = productLines[i];

                int textWidth = line.Length * productCharWidth;

                if (textWidth > 280)
                    throw new ArgumentException("Mehsul setri etikete sigmir.");

                int textY = (portraitWidth - textWidth) / 2;
                int textX = productStartX - i * lineSpacing;

                tsplCommand += $@"
REM === MEHSUL ADI {i + 1} ===
TEXT {textX},{textY},""{productFont}"",90,1,1,""{line}""
";
            }

            // === BARKOD ===

            tsplCommand += $@"
REM === CODE128 BARKOD ===
BARCODE 153,{barcodeY},""128"",74,0,90,2,2,""{barcode}""

REM === BARKOD NOMRESI ===
TEXT 67,{barcodeTextY},""{barcodeTextFont}"",90,1,1,""{barcode}""

PRINT 1,1
";

            RawPrinterHelper.SendStringToPrinter(printerName, tsplCommand);
        }

        private static decimal ParseLabelPrice60x40(string value)
        {
            string text = (value ?? "")
                .Replace("₼", "")
                .Replace("AZN", "")
                .Replace(" ", "")
                .Replace(",", ".")
                .Trim();

            return decimal.Parse(text, NumberStyles.AllowDecimalPoint |NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
        }

        private static string FormatLabelPrice60x40(decimal value)
        {
            return value.ToString("0.00", CultureInfo.InvariantCulture).Replace(".", ",");
        }
    }
}
