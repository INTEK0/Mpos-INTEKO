using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.NKA;
using static WindowsFormsApp2.NKA.NBA;

namespace WindowsFormsApp2.Forms
{
    public partial class fKassaReport : DevExpress.XtraEditors.XtraForm
    {
        private Root nbaResponse { get; set; }

        public fKassaReport()
        {
            InitializeComponent();
        }

        private void bReport_Click(object sender, EventArgs e)
        {
            var kassa = FormHelpers.GetIpModel();

            if (dateEdit1.EditValue == null || dateEdit1.DateTime == DateTime.MinValue)
            {
                ReadyMessages.ERROR_DATETIME_MESSAGE($"BAŞLANĞIC TARİXİ: {dateEdit1.DateTime.ToShortDateString()}");
                return;
            }
            else if (dateEdit2.EditValue == null || dateEdit2.DateTime == DateTime.MinValue)
            {
                ReadyMessages.ERROR_DATETIME_MESSAGE($"BİTİŞ TARİXİ: {dateEdit2.DateTime.ToShortDateString()}");
                return;
            }

            dateEdit1.DateTime = new DateTime(dateEdit1.DateTime.Year, dateEdit1.DateTime.Month, dateEdit1.DateTime.Day, 00, 00, 0);
            dateEdit2.DateTime = new DateTime(dateEdit2.DateTime.Year, dateEdit2.DateTime.Month, dateEdit2.DateTime.Day, 23, 59, 0);

            switch (kassa.Model)
            {
                case "1":
                    Sunmi.PeriodicReport(dateEdit1.DateTime, dateEdit2.DateTime, kassa.Ip);
                    break;
                case "2":
                    //Test et
                    AzSmart.PeriodicReport(dateEdit1.DateTime,dateEdit2.DateTime, kassa.Ip, kassa.MerchantId);
                    break;
                case "3":
                    Omnitech.PeriodicReport(dateEdit1.DateTime, dateEdit2.DateTime, kassa.Ip);
                    break;
                case "5":

                    break;
                case "6":

                    if (dateEdit2.DateTime < dateEdit1.DateTime)
                    {
                        FormHelpers.Alert("Qeyd edilən tarix aralığı səhvdir", Enums.MessageType.Warning);
                        return;
                    }
                    else
                    {
                        nbaResponse = NBA.PeriodicReport(dateEdit1.DateTime, dateEdit2.DateTime, kassa.Ip);
                        NBA_Response_Report(nbaResponse);
                       
                    }
                    break;
            }
            Close();
        }

        private void NBA_Response_Report(Root response)
        {
            if (response != null)
            {
                PrintDocument pd = new PrintDocument();
                PrinterSettings settings = new PrinterSettings();
                PageSettings pageSettings = new PageSettings(settings);
                PaperSize paperSize = settings.DefaultPageSettings.PaperSize;

                pd.DefaultPageSettings = new PageSettings();
                pd.DefaultPageSettings.PaperSize = paperSize;
                pd.PrintPage += new PrintPageEventHandler(NBA_Periodic_Report);
                //pagesCount = 2;

                PrintDialog PrintDialog1 = new PrintDialog();
                PrintDialog1.Document = pd;
                pd.Print();
            }
        }

        private void NBA_Periodic_Report(object sender, PrintPageEventArgs e)
        {
            Font font = new System.Drawing.Font("Times New Roman", 7f);
            Font font2 = new System.Drawing.Font("Times New Roman", 8.75f, FontStyle.Bold);
            Font font40 = new System.Drawing.Font("Times New Roman", 10f, FontStyle.Bold);
            Font font4 = new System.Drawing.Font("Times New Roman", 8f, FontStyle.Bold);
            Font font3 = new System.Drawing.Font("Times New Roman", 8.75f);
            Font f8 = new System.Drawing.Font("Times New Roman", 7f);
            Font f9 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Bold);
            Font fonta = new System.Drawing.Font("Times New Roman", 8.5f);


            string start = dateEdit1.DateTime.ToString("dd-MM-yyyy HH:mm");
            string end = dateEdit2.DateTime.ToString("dd-MM-yyyy HH:mm");

            int offset2 = 10;

            //Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            //var qrcodeimg = barcode.Draw("https://www.e-kassa.gov.az/", 0, scale: 3);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            string ipAddress = FormHelpers.GetIpModel().Ip;
            var info = GetInfo(ipAddress);


            string header = $"TS Adı :{info.data.object_name}\r\n" +
                            $"TS Ünvanı :{info.data.object_address}\r\n\r\n" +
                            $"VÖ Adı :{info.data.company_name}\r\n" +
                            $"VÖEN - Obyekt kodu: {info.data.object_tax_number}";

            #region [..Header..]
            e.Graphics.DrawString(header, fonta, Brushes.Black, new RectangleF(40F, 12F, 220F, 110F), sf);
            e.Graphics.DrawString("Dövrü hesabat", font40, Brushes.Black, new Point(95, offset2 + 110));
            e.Graphics.DrawString("Çek nömrəsi No:" + nbaResponse.data.reportNumber, font2, Brushes.Black, new Point(90, offset2 + 125));
            e.Graphics.DrawString("Tarix: " + DateTime.Now.ToString("dd.MM.yyyy"), font, Brushes.Black, new Point(210, offset2 + 132));
            e.Graphics.DrawString("Saat: " + DateTime.Now.ToString("HH:mm"), font, Brushes.Black, new Point(210, offset2 + 142));
            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 150));
            e.Graphics.DrawString("Hesab Dövrü:", font, Brushes.Black, 5, offset2 + 165);
            e.Graphics.DrawString($"{start}\n{end}", font, Brushes.Black, 210, offset2 + 160);
            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 180));
            e.Graphics.DrawString("Kassa Çekləri", font4, Brushes.Black, new Point(110, offset2 + 190));
            e.Graphics.DrawString("Birinci kassa çekinin No:", f8, Brushes.Black, 5, offset2 + 200);
            e.Graphics.DrawString($"{nbaResponse.data.firstDocNumber}", f8, Brushes.Black, 250, offset2 + 200);
            e.Graphics.DrawString("Sonuncu kassa çekinin No:", f8, Brushes.Black, 5, offset2 + 210);
            e.Graphics.DrawString($"{nbaResponse.data.lastDocNumber}", f8, Brushes.Black, 250, offset2 + 210);
            #endregion [..Header..]



            #region [..Satış..]
            e.Graphics.DrawString("Satışlar", font4, Brushes.Black, new Point(125, offset2 + 230));
            e.Graphics.DrawString("Satış sayı", f8, Brushes.Black, 5, offset2 + 240);
            e.Graphics.DrawString($"{nbaResponse.data.currencies[0].saleCount}", f8, Brushes.Black, 250, offset2 + 240);
            e.Graphics.DrawString("Satış məbləği", f8, Brushes.Black, 5, offset2 + 250);
            e.Graphics.DrawString($"{nbaResponse.data.currencies[0].saleSum}", f8, Brushes.Black, 250, offset2 + 250);
            e.Graphics.DrawString("* Nağd satışın məbləği", f8, Brushes.Black, 5, offset2 + 260);
            e.Graphics.DrawString($"{nbaResponse.data.currencies[0].saleCashSum}", f8, Brushes.Black, 250, offset2 + 260);
            e.Graphics.DrawString("* Nağdsız satışın məbləği", f8, Brushes.Black, 5, offset2 + 270);
            e.Graphics.DrawString($"{nbaResponse.data.currencies[0].saleCashlessSum}", f8, Brushes.Black, 250, offset2 + 270);
            e.Graphics.DrawString("* Bonus satışın məbləği", f8, Brushes.Black, 5, offset2 + 280);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 280);
            e.Graphics.DrawString("* Avans satışın məbləği", f8, Brushes.Black, 5, offset2 + 290);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 290);
            e.Graphics.DrawString("Vergi məbləğinin cəmi", f8, Brushes.Black, 5, offset2 + 300);
            e.Graphics.DrawString($"{nbaResponse.data.currencies[0].saleVatAmounts[0].vatSum}", f8, Brushes.Black, 250, offset2 + 300);
            #endregion [..Satış..]



            #region [..Qaytarma..]
            e.Graphics.DrawString("Geri qaytarmalar", font4, Brushes.Black, new Point(100, offset2 + 320));
            e.Graphics.DrawString("Geri qaytarmaların sayı", f8, Brushes.Black, 5, offset2 + 330);
            e.Graphics.DrawString($"{nbaResponse.data.currencies[0].moneyBackCount}", f8, Brushes.Black, 250, offset2 + 330);
            e.Graphics.DrawString("Geri qaytarmaların cəmi", f8, Brushes.Black, 5, offset2 + 340);
            e.Graphics.DrawString($"{nbaResponse.data.currencies[0].moneyBackSum}", f8, Brushes.Black, 250, offset2 + 340);
            e.Graphics.DrawString("* Nağd geri qaytarmanın məbləği", f8, Brushes.Black, 5, offset2 + 350);
            e.Graphics.DrawString($"{nbaResponse.data.currencies[0].moneyBackCashSum}", f8, Brushes.Black, 250, offset2 + 350);
            e.Graphics.DrawString("* Nağdsız geri qaytarmanın məbləği", f8, Brushes.Black, 5, offset2 + 360);
            e.Graphics.DrawString($"{nbaResponse.data.currencies[0].moneyBackCashlessSum}", f8, Brushes.Black, 250, offset2 + 360);
            #endregion [..Qaytarma..]



            #region [..Ləğv etmə..]
            e.Graphics.DrawString("Ləğv edilmiş sənədlər", font4, Brushes.Black, new Point(95, offset2 + 390));
            e.Graphics.DrawString("Ləğv etmənin sayı", f8, Brushes.Black, 5, offset2 + 400);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 400);
            e.Graphics.DrawString("Ləğv etmənin məbləği", f8, Brushes.Black, 5, offset2 + 410);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 410);
            e.Graphics.DrawString("* Nağd ləğv etmə məbləği", f8, Brushes.Black, 5, offset2 + 420);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 420);
            e.Graphics.DrawString("* Nağdsız ləğv etmə məbləği", f8, Brushes.Black, 5, offset2 + 430);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 430);
            #endregion [..Ləğv etmə..]



            #region [..Bərpa..]
            e.Graphics.DrawString("Bərpa çeki", font4, Brushes.Black, new Point(120, offset2 + 450));
            e.Graphics.DrawString("Bərpa sayı", f8, Brushes.Black, 5, offset2 + 460);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 460);
            e.Graphics.DrawString("Bərpa məbləği", f8, Brushes.Black, 5, offset2 + 470);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 470);
            e.Graphics.DrawString("* Nağd bərpa məbləği", f8, Brushes.Black, 5, offset2 + 480);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 480);
            e.Graphics.DrawString("* Nağdsız bərpa məbləği", f8, Brushes.Black, 5, offset2 + 490);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 490);
            e.Graphics.DrawString("* Avans bərpa məbləği", f8, Brushes.Black, 5, offset2 + 500);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 500);
            e.Graphics.DrawString("* Kredit bərpa məbləği", f8, Brushes.Black, 5, offset2 + 510);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 510);
            e.Graphics.DrawString("* Bonus bərpa məbləği", f8, Brushes.Black, 5, offset2 + 520);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 520);
            #endregion [..Bərpa..]



            #region [..Avans..]
            e.Graphics.DrawString("Avans", font4, Brushes.Black, new Point(125, offset2 + 540));
            e.Graphics.DrawString("Avans sayı", f8, Brushes.Black, 5, offset2 + 550);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 550);
            e.Graphics.DrawString("Avans məbləği", f8, Brushes.Black, 5, offset2 + 560);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 560);
            e.Graphics.DrawString("* Nağd avans məbləği", f8, Brushes.Black, 5, offset2 + 570);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 570);
            e.Graphics.DrawString("* Nağdsız avans məbləği", f8, Brushes.Black, 5, offset2 + 580);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 580);
            e.Graphics.DrawString("* Bonus avans məbləği", f8, Brushes.Black, 5, offset2 + 590);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 590);
            #endregion [..Avans..]



            #region [..Kredit..]
            e.Graphics.DrawString("Kredit", font4, Brushes.Black, new Point(125, offset2 + 610));
            e.Graphics.DrawString("Kredit sayı", f8, Brushes.Black, 5, offset2 + 620);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 620);
            e.Graphics.DrawString("Kredit məbləği", f8, Brushes.Black, 5, offset2 + 630);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 630);
            e.Graphics.DrawString("* Nağd kredit məbləği", f8, Brushes.Black, 5, offset2 + 640);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 640);
            e.Graphics.DrawString("* Nağdsız kredit məbləği", f8, Brushes.Black, 5, offset2 + 650);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 650);
            e.Graphics.DrawString("* Bonus kredit məbləği", f8, Brushes.Black, 5, offset2 + 660);
            e.Graphics.DrawString("0", f8, Brushes.Black, 250, offset2 + 660);
            #endregion [..Kredit..]



            #region [..Footer..]
            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset2 + 670));
            e.Graphics.DrawString("NKA-nın modeli: " + info.data.cashregister_model, font, Brushes.Black, 5, offset2 + 690);
            e.Graphics.DrawString("NKA-nın zavod nömrəsi: " + info.data.cashregister_factory_number, font, Brushes.Black, 5, offset2 + 700);
            e.Graphics.DrawString("NMQ-nın qeydiyyat nömrəsi: " + info.data.cashbox_tax_number, font, Brushes.Black, 5, offset2 + 710);

            //e.Graphics.DrawString("www.e-kassa.gov.az", font, Brushes.Black, 100, offset2 + 725);

            //e.Graphics.DrawImage(qrcodeimg, 120, offset2 + 740, width: 50, height: 50);
            #endregion [..Footer..]
        }
    }
}