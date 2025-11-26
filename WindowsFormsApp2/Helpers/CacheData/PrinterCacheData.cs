using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;

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
TEXT 55,135,""2"",0,1,1,""{_price} AZN""

REM === Barcode ===
BARCODE  7,60,""{Enums.GetEnumDescription(barcodeType)}"",40,1,0,2,2,""{_barcode}""

PRINT 1,1
";


            bool result = RawPrinterHelper.SendStringToPrinter(printerName, tsplCommand);
        }

        private static List<string> SplitProductName(string productName, int maxLength)
        {
            List<string> lines = new List<string>();

            for (int i = 0; i < productName.Length; i += maxLength)
            {
                lines.Add(productName.Substring(i, Math.Min(maxLength, productName.Length - i)));
            }

            return lines;
        }
    }
}
