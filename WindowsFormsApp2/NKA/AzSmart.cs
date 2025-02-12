using DevExpress.CodeParser;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.Sql;
using DevExpress.Pdf.Native.BouncyCastle.Utilities.Net;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.POS_GAYTARMA_LAYOUT;
using Method = RestSharp.Method;

namespace WindowsFormsApp2.NKA
{
    public static class AzSmart
    {
        private static readonly bool MessageVisible = FormHelpers.SuccessMessageVisible();
        private static AzSmartResponse RequestPOST(string ipAddress, string data)
        {
            var client = new RestClient();
            var request = new RestRequest(ipAddress, Method.Post);
            request.AddParameter("text/plain", data, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE();
                FormHelpers.Log($"Kassa ilə əlaqə zamanı xəta yarandı\n\n {response.ErrorMessage}");
                return null;
            }
            else
            {
                AzSmartResponse weatherForecast = System.Text.Json.JsonSerializer.Deserialize<AzSmartResponse>(response.Content);
                return weatherForecast;
            }
        }

        public static void OpenShift(string ipAdress, string merchantId, string cashier)
        {
            RootObject rootObject = new RootObject
            {
                employeeName = cashier,
                wsName = null,
                departmentName = null,
                currency = null,
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var data = JsonConvertBase64(json, merchantId);


            var client = new RestClient();
            var request = new RestRequest(ipAdress + "/open_shift", Method.Post);
            request.AddParameter("text/plain", data, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);

            //if (response.ResponseStatus != ResponseStatus.Completed)
            //{
            //    ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE();
            //    FormHelpers.Log($"Kassa ilə əlaqə zamanı xəta yarandı\n\n {response.ErrorMessage}");
            //    return;
            //}

            AzSmartResponseOpenShift weatherForecast = System.Text.Json.JsonSerializer.Deserialize<AzSmartResponseOpenShift>(response.Content);
            if (weatherForecast != null)
            {
                switch (weatherForecast.code)
                {
                    case 0:
                        ReadyMessages.SUCCESS_OPEN_SHIFT_MESSAGE();
                        FormHelpers.Log(CommonData.SUCCESS_OPEN_SHIFT);
                        break;
                    case 6:
                        ReadyMessages.SUCCESS_SHIFT_STATUS_MESSAGE();
                        break;
                    default:
                        XtraMessageBox.Show(weatherForecast.message);
                        break;
                }
            }
        }

        public static void CloseShift(string ipAdress, string merchantId, string cashier)
        {
            RootObject rootObject = new RootObject
            {
                employeeName = cashier,
                wsName = null,
                departmentName = null,
                currency = null,
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var data = JsonConvertBase64(json, merchantId);

            var response = RequestPOST(ipAdress + "/close_shift", data);

            if (response != null)
            {
                if (response.status is "success")
                {
                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_CLOSE_SHIFT_MESSAGE();
                    }

                    FormHelpers.Log(CommonData.SUCCESS_CLOSE_SHIFT);
                }
                else
                {
                    ReadyMessages.WARNING_DEFAULT_MESSAGE(response.message);
                }
            }
        }

        public static bool Sales(string ipAddress, string merchantID, string proccessNo, decimal _total, decimal _cash, decimal _card, string cashier, string rrn = default)
        {
            List<Item> items = new List<Item>();
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.SqlCon;
            conn.Open();
            string query = $@"
SELECT 
t.name,
t.item_id,
t.salePrice,
t.purchasePrice,
t.quantity,
case t.vatType 
        when 1 then '1800' 
        when 2 then '1800' 
        when 3 then '0'
        when 4 then '200' 
        when 5 then '800'
end as TaxPrc,
case t.vatType 
        when 1 then N'ƏDV 18%'
        when 2 then N'Ticarət əlavəsi 18%'
        when 3 then N'ƏDV-dən azad'
        when 4 then 'SV-2%'
        when 5 then 'SV-8%'
end as TaxName,
t.quantityType,
salePrice*quantity as ssum
FROM dbo.Item as t
WHERE user_id = {Properties.Settings.Default.UserID}";

            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string name = dr["name"].ToString();
                string code = dr["item_id"].ToString();
                decimal salePrice = Convert.ToDecimal(dr["salePrice"]);
                decimal _purchasePrice = Convert.ToDecimal(dr["purchasePrice"]);
                decimal quantity = Convert.ToDecimal(dr["quantity"]);
                string taxName = dr["TaxName"].ToString();
                int TaxPrc = Convert.ToInt32(dr["TaxPrc"]);


                int miqdar = Convert.ToInt32(quantity * 1000);
                int price = Convert.ToInt32(salePrice * quantity * 100);

                int? purchasePrice = Convert.ToInt32(_purchasePrice * 100);
                int? purchasePriceSum = Convert.ToInt32(_purchasePrice * quantity * 100);

                if (taxName != "Ticarət əlavəsi 18%")
                {
                    purchasePriceSum = null;
                    purchasePrice = null;
                }

                List<ItemTax> taxs = new List<ItemTax>();


                ItemTax tax = new ItemTax
                {
                    fullName = taxName,
                    taxName = taxName,
                    taxPrc = TaxPrc,
                };
                taxs.Add(tax);


                Item itemProduct = new Item
                {
                    itemName = name,
                    itemId = code,
                    itemQty = miqdar,
                    itemAmount = price,
                    itemMarginPrice = purchasePrice,
                    itemMarginSum = purchasePriceSum,
                    itemTaxes = taxs
                };
                items.Add(itemProduct);
            }

            int cash = Convert.ToInt32(_cash * 100);
            int card = Convert.ToInt32(_card * 100);
            int total = Convert.ToInt32(_total * 100);

            Payments payments = new Payments
            {
                cashAmount = cash,
                cashlessAmount = card,
            };

            string docnumber = ReturnHeaderId();

            RootObject rootObject = new RootObject
            {
                employeeName = cashier,
                //rrn = rrn,
                items = items,
                payments = payments,
                amount = total,
                docNumber = docnumber
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            string hashData = JsonConvertBase64(json, merchantID);

            var response = RequestPOST(ipAddress + "/sale", hashData);

            if (response != null)
            {
                if (response.status == "success")
                {
                    DbProsedures.InsertPosSales(new PosSales
                    {
                        posNomre = response.fiscalNum,
                        longFiskalId = response.fiscalID.ToString(),
                        proccessNo = proccessNo,
                        cash = cash,
                        card = card,
                        total = total,
                        json = json,
                        shortFiskalId = null
                    });

                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_SALES_MESSAGE();
                    }

                    FormHelpers.Log($"Pos satışı uğurla edildi. Qəbz №: {response.fiscalNum}");
                    return true;
                }
                else
                {
                    ReadyMessages.ERROR_SALES_MESSAGE(response.message);
                    FormHelpers.Log($"Pos satışı xətası - Xəta mesajı: {response.message}");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool Refund(string ipAddress, string merchantID, string cashier, string proccesNo, string _bankrrn = "")
        {
            string _fiskallID = "";
            string _checkNum = "";
            decimal _cash = default;
            decimal _card = default;
            decimal _total = default;
            decimal _total2 = default;
            List<Item> items = new List<Item>();

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(DbHelpers.GetPosGaytarmaManualQuery, connection))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            decimal cash1 = Convert.ToDecimal(dr["NEGD_"].ToString());
                            decimal card1 = Convert.ToDecimal(dr["KART_"].ToString());
                            decimal total1 = Convert.ToDecimal(dr["UMUMI_MEBLEG"].ToString());


                            string fiscal_id = dr["fiscal_id"].ToString();
                            string posNomre = dr["pos_nomre"].ToString();


                            _fiskallID = fiscal_id;
                            _cash = cash1;
                            _card = card1;
                            _checkNum = posNomre;
                            _total2 = total1;
                        }
                    }
                }
            }


            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                string query = $@"(SELECT md.MEHSUL_ADI AS name,
                       p.item_id AS code,
                       pl.say AS say,
                       p.satis_giymet AS satis_giymet,
                       case md.VERGI_DERECESI when 1 then '1800' when 4 then '200' when 5 then '800' else 0 end as TaxPrc,
					   case md.VERGI_DERECESI when 1 then N'ƏDV 18%' when 4 then 'SV-2%' when 5 then 'SV-8%'  when 3 then N'ƏDV-dən azad' end as TaxName
              FROM pos_satis_check_details p
                       INNER JOIN MAL_ALISI_DETAILS md ON p.mal_alisi_details_id = md.MAL_ALISI_DETAILS_ID
                       INNER JOIN pos_gaytarma_manual pl ON p.pos_satis_check_details_id = pl.pos_satis_check_details
              WHERE pl.emeliyyat_nomre = '{proccesNo}')";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string name = dr["name"].ToString();
                            string code = dr["code"].ToString();
                            decimal salePrice = Convert.ToDecimal(dr["satis_giymet"]);
                            decimal quantity = Convert.ToDecimal(dr["say"]);

                            string taxName = dr["TaxName"].ToString();
                            int TaxPrc = Convert.ToInt32(dr["TaxPrc"]);

                            _total += salePrice;


                            int miqdar = Convert.ToInt32(quantity * 1000);
                            int price = Convert.ToInt32(salePrice * quantity * 100);


                            List<ItemTax> taxs = new List<ItemTax>();


                            ItemTax tax = new ItemTax
                            {
                                fullName = taxName,
                                taxName = taxName,
                                taxPrc = TaxPrc,
                            };
                            taxs.Add(tax);



                            Item itemProduct = new Item
                            {
                                itemName = name,
                                itemId = code,
                                itemQty = miqdar,
                                itemAmount = price,
                                itemTaxes = taxs
                            };
                            items.Add(itemProduct);
                        }
                    }
                }
            }


            int cash = Convert.ToInt32(_cash * 100);
            int card = Convert.ToInt32(_card * 100);
            int total = Convert.ToInt32(_total2 * 100);
            int amount = Convert.ToInt32(_total * 100);
            Payments payments = new Payments();
            if (cash > 0)
            {
                payments.cashAmount = amount;
                payments.cashlessAmount = 0;
            }
            else if (card > 0)
            {
                payments.cashAmount = 0;
                payments.cashlessAmount = amount;
            }
            else if (cash > 0 && card > 0)
            {
                payments.cashAmount = amount;
                payments.cashlessAmount = 0;
            }

            string docnumber = Guid.NewGuid().ToString();

            RootObject rootObject = new RootObject
            {
                employeeName = cashier,
                //rrn = rrn,
                items = items,
                payments = payments,
                amount = amount,
                docNumber = docnumber,
                parentDocID = _fiskallID,
                checkNum = _checkNum,
                originAmount = total
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            string hashData = JsonConvertBase64(json, merchantID);

            var response = RequestPOST(ipAddress + "/refund", hashData);

            if (response != null)
            {
                if (response.status == "success")
                {
                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_RETURN_SALES_MESSAGE();
                    }

                    FormHelpers.Log($"Qəbz geri qaytarması edildi Qəbz №: {response.fiscalNum}");
                    return true;
                }
                else
                {
                    FormHelpers.Log($"Pos satış qaytarma xətası. Xəta mesajı: {response.message}");
                    ReadyMessages.ERROR_RETURN_SALES_MESSAGE(response.message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void LastReceiptCopy(string ipAddress, string merchantId, string cashier)
        {
            RootObject rootObject = new RootObject
            {
                employeeName = cashier,
                wsName = null,
                departmentName = null,
                currency = null,
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var data = JsonConvertBase64(json, merchantId);

            var response = RequestPOST(ipAddress + "/last_document", data);

            if (response != null)
            {
                if (response.status is "success")
                {
                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_LAST_DOCUMENT_MESSAGE();
                    }
                    FormHelpers.Log(CommonData.SUCCESS_LAST_DOCUMENT);
                }
                else
                {
                    ReadyMessages.ERROR_LAST_DOCUMENT_MESSAGE(response.message);
                    FormHelpers.Log($"Təkrar qəbz çap olunarkən xəta yarandı. Xəta mesajı: {response.message}");
                }
            }
        }

        public static bool InstallmentSales(string ipAddress, string merchantID, string processNo, decimal _total, string cashier)
        {
            List<Item> items = new List<Item>();
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.SqlCon;
            conn.Open();
            string query = "select name,Item.item_id,salePrice,quantity, case vatType when 1 then '1800' when 4 then '200' when 5 then '800' else 0 end " +
                "as TaxPrc, case vatType when 1 then N'ƏDV 18%' when 4 then 'SV-2%' when 5 then 'SV-8%'  when 3 then N'ƏDV-dən azad' end as TaxName,quantityType,salePrice*quantity as ssum from  dbo.item;";

            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string name = dr["name"].ToString();
                string code = dr["item_id"].ToString();
                decimal salePrice = Convert.ToDecimal(dr["salePrice"]);
                decimal quantity = Convert.ToDecimal(dr["quantity"]);
                string taxName = dr["TaxName"].ToString();
                int TaxPrc = Convert.ToInt32(dr["TaxPrc"]);
                //int vatType = Convert.ToInt32(dr["vatType"]);
                //int quantityType = Convert.ToInt32(dr["quantityType"]);
                //double ssum = Convert.ToDouble(dr["ssum"]);

                int price = Convert.ToInt32(salePrice * quantity) * 100;
                int miqdar = Convert.ToInt32(quantity * 1000);

                List<ItemTax> taxs = new List<ItemTax>();

                ItemTax tax = new ItemTax
                {
                    fullName = taxName,
                    taxName = taxName,
                    taxPrc = TaxPrc,
                };
                taxs.Add(tax);



                Item itemProduct = new Item
                {
                    itemName = name,
                    itemId = code,
                    itemQty = miqdar,
                    itemAmount = price,
                    itemTaxes = taxs
                };
                items.Add(itemProduct);
            }

            int total = Convert.ToInt32(_total) * 100;

            Payments payments = new Payments
            {
                cashAmount = 0,
                cashlessAmount = 0,
                installmentAmount = total
            };

            string docnumber = ReturnHeaderId();

            RootObject rootObject = new RootObject
            {
                employeeName = cashier,
                //rrn = rrn,
                items = items,
                payments = payments,
                amount = total,
                docNumber = docnumber
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            string hashData = JsonConvertBase64(json, merchantID);

            var response = RequestPOST(ipAddress + "/sale", hashData);

            if (response != null)
            {
                if (response.status is "success")
                {
                    DbProsedures.InsertPosSales(new PosSales
                    {
                        posNomre = response.fiscalNum,
                        longFiskalId = response.fiscalID.ToString(),
                        proccessNo = processNo,
                        cash = 0,
                        card = _total,
                        total = total,
                        json = json,
                        shortFiskalId = null
                    });

                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_SALES_MESSAGE();

                    }
                    FormHelpers.Log($"Birbank ilə taksit ödənişi uğurla edildi. Qəbz No: {response.fiscalNum}");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void XReport(string ipAddress, string merchantId, string cashier)
        {
            RootObject rootObject = new RootObject
            {
                employeeName = cashier,
                wsName = null,
                departmentName = null,
                currency = null,
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var data = JsonConvertBase64(json, merchantId);

            var response = RequestPOST(ipAddress + "/x_report", data);

            if (response != null)
            {
                if (response.status is "success")
                {
                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_X_REPORT_MESSAGE();
                    }
                    FormHelpers.Log(CommonData.SUCCESS_X_REPORT);
                }
                else
                {
                    ReadyMessages.WARNING_DEFAULT_MESSAGE(response.message);
                }
            }
        }

        public static void PeriodicReport(DateTime _start, DateTime _end, string ipAddress, string merchantId)
        {
            string start = _start.ToString("yyyy-MM-dd hh:mm:ss");
            string end = _end.ToString("yyyy-MM-dd hh:mm:ss");

            RootObject rootObject = new RootObject
            {
                wsName = null,
                departmentName = null,
                amount = null,
                currency = null,
                date_start = start,
                date_stop = end,
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var data = JsonConvertBase64(json, merchantId);

            var response = RequestPOST(ipAddress + "/dates_report", data);

            if (response.status == "success")
            {
                if (MessageVisible)
                {
                    ReadyMessages.SUCCES_PERİODİC_Z_REPORT_MESSAGE();
                }
                FormHelpers.Log(CommonData.SUCCES_PERİODİC_Z_REPORT);
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(response.message);
                FormHelpers.Log($"Dövrü hesabat çap edilərkən xəta yarandı. Xəta mesajı: {response.message}");
            }
        }

        private static string ReturnHeaderId()
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = Properties.Settings.Default.SqlCon;
            cmd.Connection = con;
            cmd.CommandText = "select header_id from  dbo.header;";
            con.Open();

            var result = cmd.ExecuteScalar();

            if (result != null)
            {
                return result.ToString();
            }
            else
            {
                return null;
            }
        }

        public static string JsonConvertBase64(string json, string merchantId)
        {
            string base_64 = Base64Encode(json);
            var data_ = base_64;
            var convert_sign1 = data_ + merchantId;//"8c12504097cd4effb70319f29d319cc5";
            var conver_sign2sha = sha1(convert_sign1);
            var convert_sign3 = Base64Encode(conver_sign2sha);
            var string_post = "data=" + data_.Replace("=", "%3D") + "&" + "sign=" + convert_sign3.Replace("=", "%3D");
            return string_post;
        }

        #region [..Request Classes..]

        private class ItemTax
        {
            public string fullName { get; set; }
            public string taxName { get; set; }
            public int taxPrc { get; set; }
            public int calcType { get; set; } = 1;
        }

        private class Item
        {
            public string itemId { get; set; }
            public string itemName { get; set; }
            public int? itemAttr { get; set; } = null;
            public int itemQty { get; set; }
            public int itemAmount { get; set; }
            public int? discount { get; set; } = null;
            public List<ItemTax> itemTaxes { get; set; }
            public int? itemMarginSum { get; set; } = null;
            public int? itemMarginPrice { get; set; } = null;
        }

        private class Payments
        {
            public int cashAmount { get; set; }
            public int cashlessAmount { get; set; }
            //public int invoiceAmount { get; set; }
            //public decimal? creditAmount { get; set; } = null;
            //public decimal? bonusesAmount { get; set; } = null;
            public decimal? prepaymentAmount { get; set; } = null;
            public decimal? installmentAmount { get; set; } = null;
        }

        private class RootObject
        {
            public string docTime { get; set; } = null;
            public string docNumber { get; set; } = null;
            public int? wsName { get; set; } = 15;
            public string departmentName { get; set; } = "department";
            public string employeeName { get; set; } = "Admin";
            public string date_start { get; set; } = null;
            public string date_stop { get; set; } = null;
            public int? amount { get; set; } = null;
            public string currency { get; set; } = "AZN";
            public List<Item> items { get; set; } = null;
            public Payments payments { get; set; } = null;
            public string fiscalID { get; set; } = null;
            public string printFooter { get; set; } = null;
            public string creditContract { get; set; } = null;
            public string prepayDocID { get; set; } = null;
            public string prepayDocNum { get; set; } = null;
            public string departmentCode { get; set; } = null;
            public string parentDocID { get; set; } = null;
            public string checkNum { get; set; } = null;
            public int? originAmount { get; set; } = null;
            //public string rrn { get; set; } = null;
        }

        #endregion [..Request Classes..]



        #region [..Response Classes..]

        public class AzSmartResponseOpenShift
        {
            public string status { get; set; }
            public int code { get; set; }
            public string message { get; set; }

        }

        private class AzSmartResponse
        {
            public int Cash { get; set; }
            public int CashRest { get; set; }
            public int CorrBonusesSum { get; set; }
            public int CorrCashSum { get; set; }
            public int CorrCashlessSum { get; set; }
            public int CorrCount { get; set; }
            public int CorrCreditSum { get; set; }
            public int CorrPrepaymentSum { get; set; }
            public int CorrSum { get; set; }
            public int CreditpayBonusesSum { get; set; }
            public int CreditpayCashSum { get; set; }
            public int CreditpayCashlessSum { get; set; }
            public int CreditpayCount { get; set; }
            public int CreditpayCreditSum { get; set; }
            public int CreditpayPrepaymentSum { get; set; }
            public int CreditpayRollbackBonusesSum { get; set; }
            public int CreditpayRollbackCashSum { get; set; }
            public int CreditpayRollbackCashlessSum { get; set; }
            public int CreditpayRollbackCount { get; set; }
            public int CreditpayRollbackCreditSum { get; set; }
            public int CreditpayRollbackPrepaymentSum { get; set; }
            public int CreditpayRollbackSum { get; set; }
            public int CreditpaySum { get; set; }
            public string DepartmentName { get; set; }
            public int DepositCount { get; set; }
            public int DepositSum { get; set; }
            public int DocCountToSend { get; set; }
            public string EmployeeName { get; set; }
            public string FirstDocNumber { get; set; }
            //public int FiscalID { get; set; }
            public string LastDocNumber { get; set; }
            public int MoneyBackBonusesSum { get; set; }
            public int MoneyBackCashSum { get; set; }
            public int MoneyBackCashlessSum { get; set; }
            public int MoneyBackCount { get; set; }
            public int MoneyBackCreditSum { get; set; }
            public int MoneyBackPrepaymentSum { get; set; }
            public int MoneyBackSum { get; set; }
            public int OpenOrdersCnt { get; set; }
            public int PrepayBonusesSum { get; set; }
            public int PrepayCashSum { get; set; }
            public int PrepayCashlessSum { get; set; }
            public int PrepayCount { get; set; }
            public int PrepayCreditSum { get; set; }
            public int PrepayPrepaymentSum { get; set; }
            public int PrepaySum { get; set; }
            public string ReportNumber { get; set; }
            public int RollbackBonusesSum { get; set; }
            public int RollbackCashSum { get; set; }
            public int RollbackCashlessSum { get; set; }
            public int RollbackCount { get; set; }
            public int RollbackCreditSum { get; set; }
            public int RollbackPrepaymentSum { get; set; }
            public int RollbackSum { get; set; }
            public int SaleBonusesSum { get; set; }
            public int SaleCashSum { get; set; }
            public int SaleCashlessSum { get; set; }
            public int SaleCount { get; set; }
            public int SaleCreditSum { get; set; }
            public int SalePrepaymentCashSum { get; set; }
            public int SalePrepaymentCashlessSum { get; set; }
            public int SalePrepaymentSum { get; set; }
            public int SaleSum { get; set; }
            public int ShiftID { get; set; }
            public string ShiftOpenAt { get; set; }
            public int ToPrint { get; set; }
            public int WithdrawCount { get; set; }
            public int WithdrawSum { get; set; }
            public string WsName { get; set; }


            public string status { get; set; }
            public int shiftID { get; set; }
            public int? code { get; set; }
            public string message { get; set; }
            public string error_description { get; set; } = null;
            public int? documentID { get; set; } = null;
            public object fiscalID { get; set; } = null;
            public string fiscalNum { get; set; } = null;
            public string rrn { get; set; } = null;
            public string auth { get; set; } = null;
            public string card_num { get; set; } = null;
            public string checkNum { get; set; } = null;
        }

        #endregion [..Response Classes..]
    }
}
