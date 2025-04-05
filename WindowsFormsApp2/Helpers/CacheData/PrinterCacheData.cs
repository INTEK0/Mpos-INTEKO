using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp2.Helpers.CacheData
{
    public class PrinterCacheData
    {
        public static string ReplaceChars(string input)
        {
            return input
                .Replace("ç", "c").Replace("Ç", "C")
                .Replace("ğ", "g").Replace("Ğ", "G")
                .Replace("ı", "i").Replace("İ", "I")
                .Replace("ö", "o").Replace("Ö", "O")
                .Replace("ş", "s").Replace("Ş", "S")
                .Replace("ü", "u").Replace("Ü", "U")
                .Replace("ə", "e").Replace("Ə", "E");
        }

        public static void PrintLabel(string _company, string _product, string _price, string _barcode)
        {
            string companyName = ReplaceChars(_company);
            string productName = ReplaceChars(_product);
            string price = _price;
            string barcode = _barcode;
            double priceY = 220;
            double aznY = priceY + 40; //220 - Satış qiymətinin Y dəyəri
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

REM === Product name under the line, left aligned, large area ===
TEXT 10,55,""3"",0,1,1,""{productName}""

REM === Price at bottom-left ===
TEXT 15,{priceY},""4"",0,1,1,""{price}""


REM === Price at bottom-left ===
TEXT 15,{aznY},""4"",0,1,1,""AZN""

REM === Barcode at bottom-right ===
BARCODE  180,200,""128"",80,1,0,2,2,""{barcode}""

PRINT 1,1
";

            string printerName = "Xprinter XP-350B";

            bool result = RawPrinterHelper.SendStringToPrinter(printerName, tsplCommand);
        }
    }
}
