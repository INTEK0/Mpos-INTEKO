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

namespace WindowsFormsApp2.NKA
{
    public class Xprinter
    {
        private static readonly bool MessageVisible = FormHelpers.SuccessMessageVisible();

        public static void PrepaymentPay(SalesDto salesData)
        {
            List<PrepaymentRequest.Item> items = new List<PrepaymentRequest.Item>();
            int _vatType = 0;
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                con.Open();
                string query = $@"SELECT 
                              name,
                              --Item.item_id,
                              code,
                              salePrice,
                              quantity,
                              discount,
                              vatType,
                              quantityType,
                              salePrice*quantity as ssum
                              FROM dbo.item WHERE user_id = {Properties.Settings.Default.UserID};";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string name = dr["name"].ToString();
                            string code = dr["code"].ToString();
                            decimal salePrice = Convert.ToDecimal(dr["salePrice"]);
                            double quantity = Convert.ToDouble(dr["quantity"]);
                            int vatType = Convert.ToInt32(dr["vatType"]);
                            int quantityType = Convert.ToInt32(dr["quantityType"]);
                            decimal discount = Convert.ToDecimal(dr["discount"]);
                            salePrice = Math.Round(salePrice, 2);
                            _vatType = vatType;
                            PrepaymentRequest.Item itemProduct = new PrepaymentRequest.Item
                            {
                                name = name,
                                code = code,
                                salePrice = salePrice,
                                quantity = quantity,
                                vatType = vatType,
                                quantityType = quantityType
                            };
                            items.Add(itemProduct);
                        }
                    }
                }
            }

            PrepaymentRequest.Data data = new PrepaymentRequest.Data
            {
                sum = salesData.PrepaymentPay,
                vatType = _vatType,
                documentUUID = Guid.NewGuid().ToString(),
                cashPayment = salesData.IncomingSum,
                cardPayment = salesData.Card,
                bonusPayment = 0,
                items = items,
                cashierName = salesData.Cashier,
                clientName = salesData.Customer == null ? null : $"{salesData.Customer?.Name} {salesData.Customer?.Surname} {salesData.Customer?.FatherName}",
                rrn = salesData.Rrn,
                moneyBackType = null
            };

            PrepaymentRequest.Root rootObject = new PrepaymentRequest.Root
            {
                data = data,
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            //var response = RequestPOST(salesData.IpAddress, json);

            //if (response != null)
            //{
            //    if (response.message is "Success operation" || response.message is "Successful operation")
            //    {
            //        DbProsedures.InsertPosSales(new PosSales
            //        {
            //            posNomre = response.data.number,
            //            longFiskalId = response.data.document_id,
            //            proccessNo = salesData.ProccessNo,
            //            total = salesData.Total,
            //            Prepayment = salesData.PrepaymentPay,
            //            cash = salesData.Cash,
            //            card = salesData.Card,
            //            json = json,
            //            shortFiskalId = response.data.short_document_id,
            //            rrn = response.data.rrn,
            //            customerId = salesData.Customer?.CustomerID,
            //            doctorId = salesData.Doctor?.Id,
            //        });

            //        if (salesData.Customer != null)
            //        {
            //            decimal debt = salesData.Total - salesData.PrepaymentPay; //Qalan borcu
            //            DbProsedures.InsertCustomerDebt(CustomerDebtType.AvansPay, DateTime.Now, salesData.Customer.CustomerID, debt);
            //        }


            //        if (MessageVisible)
            //        {
            //            ReadyMessages.SUCCESS_SALES_MESSAGE();
            //        }

            //        FormHelpers.Log($"Avans ödənişi uğurla edildi. Qəbz No: {response.data.number}");
            //        return true;
            //    }
            //    else if (response.message is "document: invalid shift duration")
            //    {
            //        XtraMessageBox.Show("GÜN SONU (Z) HESABATI ÇIXARILMAYIB !\n\nZəhmət olmasa pos bağla düyməsinə vuraraq günü sonlandırın.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return false;
            //    }
            //    else
            //    {
            //        ReadyMessages.ERROR_SALES_MESSAGE(response.message);
            //        FormHelpers.Log($"Avans ödənişi xətası - Xəta mesajı: {response.message}");
            //        return false;
            //    }
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}
