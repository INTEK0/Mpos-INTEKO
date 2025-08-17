using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.NKA.Sunmi;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.Helpers;
using static DTOs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Drawing;
using System.Drawing.Printing;
using DevExpress.DataAccess.Native.Web;
using DevExpress.XtraMap.Native;

namespace WindowsFormsApp2.NKA
{
    public class Xprinter
    {
        private static readonly bool MessageVisible = FormHelpers.SuccessMessageVisible();

        public static void PrepaymentPay(SalesDto salesData)
        {
            var receiptNo = (Math.Abs(Guid.NewGuid().GetHashCode()) % 1000000000).ToString();

            DbProsedures.InsertPosSales(new PosSales
            {
                posNomre = receiptNo,
                longFiskalId = $"994{receiptNo}",
                proccessNo = salesData.ProccessNo,
                total = salesData.Total,
                Prepayment = salesData.PrepaymentPay,
                cash = salesData.Cash,
                card = salesData.Card,
                shortFiskalId = receiptNo,
                customerId = salesData.Customer?.CustomerID,
                doctorId = salesData.Doctor?.Id,
            });

            if (salesData.Customer != null)
            {
                decimal debt = salesData.Total - salesData.PrepaymentPay; //Qalan borcu
                DbProsedures.InsertCustomerDebt(CustomerDebtType.AvansPay, DateTime.Now, salesData.Customer.CustomerID, debt);
            }

            ReadyMessages.SUCCESS_SALES_MESSAGE();
            FormHelpers.Log($"Avans ödənişi uğurla edildi. Qəbz No: {receiptNo}");
        }

        public static void PrepaymentSale(SalesDto salesData, decimal pos_satis_main_id)
        {
            var receiptNo = (Math.Abs(Guid.NewGuid().GetHashCode()) % 1000000000).ToString();
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = $"UPDATE [dbo].[pos_satis_check_main] SET NEGD_={salesData.Cash.ToString("N2").Replace(',', '.')}+NEGD_,KART_={salesData.Card.ToString("N2").Replace(',', '.')}+KART_, [PREdate_]=getdate(),PREfiscal_id='{receiptNo}' where pos_satis_check_main_id={pos_satis_main_id}";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            if (salesData.CustomerId != null)
            {
                decimal debt = salesData.Total - salesData.PrepaymentPay;
                DbProsedures.InsertCustomerDebt(CustomerDebtType.AvansSale, DateTime.Now, (int)salesData.CustomerId, debt);
            }

            ReadyMessages.SUCCESS_ADVANCE_SALES_MESSAGE();
            FormHelpers.Log($"Avans satışı uğurla edildi. Qəbz No: {receiptNo}");
        }

        private static void PrepaymentPrint(object sender, PrintPageEventArgs e)
        {
            //            Font font = new System.Drawing.Font("Times New Roman", 7f);
            //            Font font2 = new System.Drawing.Font("Times New Roman", 8.75f, FontStyle.Bold);
            //            Font font4 = new System.Drawing.Font("Times New Roman", 10f, FontStyle.Bold);
            //            Font font3 = new System.Drawing.Font("Times New Roman", 8.75f);
            //            Font company = new System.Drawing.Font("Times New Roman", 10f);
            //            Font f8 = new System.Drawing.Font("Times New Roman", 7.5f, FontStyle.Bold);
            //            Font f9 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Bold);

            //            int offset = 170;
            //            int offset2 = 10;
            //            int m = 0;
            //            int n = 20;


            //            e.Graphics.DrawString("  " + TerminalTokenData.CompanyName, company, Brushes.Black, new Point(90, offset2));
            //            e.Graphics.DrawString("VÖEN :" + TerminalTokenData.Voen, font3, Brushes.Black, new Point(85, offset2 + 15));

            //            e.Graphics.DrawString("Avans(beh) ödənişi", font4, Brushes.Black, new Point(95, offset2 + 40));
            //            e.Graphics.DrawString("Çek nömrəsi No:" + data.ProccessNo, font2, Brushes.Black, new Point(70, offset2 + 55));
            //            e.Graphics.DrawString("Kassir: " + data.Cashier, font, Brushes.Black, new Point(5, offset2 + 80));
            //            e.Graphics.DrawString("Tarix: " + DateTime.Now.ToString("dd.MM.yyyy"), font, Brushes.Black, new Point(190, offset2 + 80));
            //            e.Graphics.DrawString("Saat: " + DateTime.Now.ToString("HH:mm"), font, Brushes.Black, new Point(190, offset2 + 95));

            //            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 110));
            //            e.Graphics.DrawString("Malın adı", f8, Brushes.Black, 5, offset2 + 120);
            //            e.Graphics.DrawString("Miqdar", f8, Brushes.Black, 130, offset2 + 120);
            //            e.Graphics.DrawString("Qiymət", f8, Brushes.Black, 190, offset2 + 120);
            //            e.Graphics.DrawString("Toplam", f8, Brushes.Black, 240, offset2 + 120);
            //            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset2 + 130));

            //            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            //            {
            //                string query = $@"SELECT [name],
            //code,
            //ROUND(salePrice,2) as salePrice,
            //ROUND(quantity,2) AS quantity,
            //case  vatType 
            //when 1 then '18'
            //when 3 then '0' 
            //when 4 then '2' 
            //when 5 then '8' 
            //else 0 end as vatType,
            //round(quantityType,2) as quantityType,
            //ROUND(salePrice*quantity,2) as ssum 
            //FROM  dbo.item WHERE user_id = {Properties.Settings.Default.UserID}";
            //                using (SqlCommand cmd = new SqlCommand(query, con))
            //                {
            //                    con.Open();
            //                    using (SqlDataReader dr = cmd.ExecuteReader())
            //                    {
            //                        while (dr.Read())
            //                        {
            //                            string name = dr["name"].ToString();
            //                            string code = dr["code"].ToString();
            //                            string sprice = dr["salePrice"].ToString();
            //                            string qty = dr["quantity"].ToString();
            //                            string vat = dr["vatType"].ToString();
            //                            string qunit = dr["quantityType"].ToString();
            //                            string ssum = dr["ssum"].ToString();

            //                            e.Graphics.DrawString(name, font, Brushes.Black, new Point(5, offset + m));
            //                            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(qty)), font, Brushes.Black, new Point(130, offset + n));
            //                            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(sprice)), font, Brushes.Black, new Point(190, offset + n));
            //                            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(ssum)), font, Brushes.Black, new Point(240, offset + n));

            //                            offset += 20;
            //                            m += 20;
            //                            n += 20;
            //                        }
            //                    }
            //                }
            //            }

            //            e.HasMorePages = false;
            //            double qalıq = Convert.ToDouble(data.IncomingSum) - Convert.ToDouble(data.Cash);
            //            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset + m));
            //            e.Graphics.DrawString("YEKUN MƏBLƏĞ:", f9, Brushes.Black, 5, offset + m + 20);
            //            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(data.Total)), f9, Brushes.Black, new Point(240, offset + m + 20));
            //            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset + m + 30));
            //            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset + m + 50));
            //            e.Graphics.DrawString("Ödəniş növü", f8, Brushes.Black, 5, offset + m + 65);
            //            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 5, offset + m + 80);
            //            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(data.Card)), f8, Brushes.Black, 240, offset + m + 80);
            //            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 5, offset + m + 90);
            //            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(data.Cash)), f8, Brushes.Black, 240, offset + m + 90);
            //            e.Graphics.DrawString("Ödənilib nağd:", f8, Brushes.Black, 5, offset + m + 100);
            //            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(data.IncomingSum)), f8, Brushes.Black, 240, offset + m + 100);
            //            e.Graphics.DrawString("Qalıq qaytarılıb nağd:", f8, Brushes.Black, 5, offset + m + 110);
            //            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(qalıq)), f8, Brushes.Black, 240, offset + m + 110);
        }
    }
}
