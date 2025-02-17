using DevExpress.DashboardCommon;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Text.Json;
using System.Windows.Forms;
using WindowsFormsApp2.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static DTOs;
using static WindowsFormsApp2.POS_LAYOUT_NEW;

namespace WindowsFormsApp2.NKA
{
    public class NBA
    {
        public static readonly string NBA_FISCAL_SERVICE_PORT = "9847"; //9898 prod port - 9847 test port
        public static readonly string NBA_BANK_SERVICE_PORT = "9944"; //9999 prod port - 9944 test port

        /* return olunacaq json, edvHesap1, edvHesap2, edvdenazad2,odenen,qaliq */


        public static GetInfoResponse GetInfo(string ipAddress)
        {
            RootObject root = new RootObject
            {
                parameters = null,
                operationId = "getInfo"
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            RestClient rest = new RestClient();
            RestRequest request = new RestRequest(ipAddress, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(json, DataFormat.Json);
            RestResponse response = rest.Execute(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE();
                FormHelpers.Log($"Kassa ilə əlaqə zamanı xəta yarandı\n\n {response.ErrorMessage}");
                return null;
            }
            else
            {
                GetInfoResponse weatherForecast = System.Text.Json.JsonSerializer.Deserialize<GetInfoResponse>(response.Content);

                if (weatherForecast.message != "Successful operation")
                {
                    ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE($"Xəta mesajı: {weatherForecast.message}");
                    return null;
                }
                else
                {
                    return weatherForecast;
                }
            }
        }

        public static string Login(string ipAddress, string cashregister_factory_number)
        {
            /* pin prod da '12348765' bu şəkildə qeyd olunmalıdır.
             * Test də isə '23264544' bu şəkildə */
            Parameters parameters = new Parameters
            {
                cashregister_factory_number = cashregister_factory_number,
                pin = "12348765",
                role = "user",
            };

            RootObject root = new RootObject
            {
                parameters = parameters,
                operationId = "toLogin"
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            RestClient rest = new RestClient();
            RestRequest request = new RestRequest(ipAddress, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(json, DataFormat.Json);
            RestResponse response = rest.Execute(request);

            NbaResponse weatherForecast = System.Text.Json.JsonSerializer.Deserialize<NbaResponse>(response.Content);

            if (weatherForecast.message != "Successful operation")
            {
                ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE(weatherForecast.message);
                return null;
            }

            string token = weatherForecast.data.access_token;

            return token;
        }

        private static void OpenShift(string ipAddress, string accessToken)
        {
            Parameters parameters = new Parameters
            {
                access_token = accessToken,
            };

            RootObject root = new RootObject
            {
                parameters = parameters,
                operationId = "openShift"
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            RestClient rest = new RestClient();
            RestRequest request = new RestRequest(ipAddress, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(json, DataFormat.Json);
            RestResponse response = rest.Execute(request);

            NbaResponse weatherForecast = System.Text.Json.JsonSerializer.Deserialize<NbaResponse>(response.Content);

            if (weatherForecast.message != "Successful operation")
            {
                ReadyMessages.ERROR_OPENSHIFT_MESSAGE(weatherForecast.message);
                return;
            }
            else
            {
                ReadyMessages.SUCCESS_OPEN_SHIFT_MESSAGE();
                FormHelpers.Log(CommonData.SUCCESS_OPEN_SHIFT);
                //      fDeposit f = new fDeposit();
                //      if (f.ShowDialog() is DialogResult.OK)
                //     {
                //          decimal depositAmount = f.depositAmount;
                //         Deposit(ipAddress, accessToken, depositAmount, "Kassir");
                //   }
            }
        }

        public static Root CloseShift(string ipAddress, string accessToken)
        {
            Parameters parameters = new Parameters
            {
                access_token = accessToken,
            };

            RootObject root = new RootObject
            {
                parameters = parameters,
                operationId = "closeShift"
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            RestClient rest = new RestClient();
            RestRequest request = new RestRequest(ipAddress, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(json, DataFormat.Json);
            RestResponse response = rest.Execute(request);

            Root weatherForecast = System.Text.Json.JsonSerializer.Deserialize<Root>(response.Content);

            if (weatherForecast.message != "Successful operation")
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE($"Xəta mesajı: {weatherForecast.message}");
                return null;
            }
            else
            {
                return weatherForecast;
            }
        }

        public static void GetShiftStatus(string ipAddress, string accessToken)
        {
            Parameters parameters = new Parameters
            {
                access_token = accessToken,
            };

            RootObject root = new RootObject
            {
                parameters = parameters,
                operationId = "getShiftStatus"
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            RestClient rest = new RestClient();
            RestRequest request = new RestRequest(ipAddress, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(json, DataFormat.Json);
            RestResponse response = rest.Execute(request);

            NbaResponse weatherForecast = System.Text.Json.JsonSerializer.Deserialize<NbaResponse>(response.Content);

            if (weatherForecast.message is "Successful operation")
            {
                if (weatherForecast.data.shift_open)
                {
                    string open_time = Convert.ToDateTime(weatherForecast.data.shift_open_time).ToString("dd.MM.yyyy HH:mm:ss");
                    ReadyMessages.SUCCESS_SHIFT_STATUS_MESSAGE(open_time);
                }
                else
                {
                    OpenShift(ipAddress, accessToken);
                }
            }
            else
            {
                ReadyMessages.ERROR_OPENSHIFT_MESSAGE(weatherForecast.message);
            }
        }

        public static void Deposit(string ipAddress, string accessToken, decimal amount, string cashier)
        {
            var data = new
            {
                cashier = cashier,
                currency = "AZN",
                sum = amount,
            };

            var parameters = new
            {
                access_token = accessToken,
                doc_type = "deposit",
                data = data
            };

            var root = new
            {
                parameters = parameters,
                operationId = "createDocument",
                version = 1
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            RestClient rest = new RestClient();
            RestRequest request = new RestRequest(ipAddress, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(json, DataFormat.Json);
            RestResponse response = rest.Execute(request);

            if (response.IsSuccessful)
            {
                NbaResponse weatherForecast = System.Text.Json.JsonSerializer.Deserialize<NbaResponse>(response.Content);

                if (weatherForecast.message is "Successful operation")
                {
                    string message = $"Kassaya {amount} AZN uğurla mədaxil edildi";
                    FormHelpers.Alert(message, Enums.MessageType.Success);
                    FormHelpers.Log(message);
                }
                else
                {
                    ReadyMessages.ERROR_DEFAULT_MESSAGE($"Xəta kodu: {weatherForecast.code}\n\nMesaj: {weatherForecast.message}");
                }
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE($"Xəta kodu: {response.StatusCode}\nMesaj: {response.ErrorMessage}");
                FormHelpers.Log($"Mədaxil zamanı xəta yarandı. Xəta kodu: {response.StatusCode} - Mesaj: {response.ErrorMessage}");
            }
        }

        public static LastDocumentResponse LastDocument(string ipAddress)
        {
            var root = new
            {
                operationId = "getLastDocument",
                version = 1
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            RestClient rest = new RestClient();
            RestRequest request = new RestRequest(ipAddress, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(json, DataFormat.Json);
            RestResponse response = rest.Execute(request);

            LastDocumentResponse weatherForecast = System.Text.Json.JsonSerializer.Deserialize<LastDocumentResponse>(response.Content);

            if (weatherForecast.message != "Successful operation")
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(weatherForecast.message);
                return null;
            }
            else
            {
                return weatherForecast;
            }
        }

        public static Root PeriodicReport(DateTime _start, DateTime _end, string ipAddress)
        {
            string start = _start.ToString("yyyy-MM-ddTHH:mm:ssZ");
            string end = _end.ToString("yyyy-MM-ddTHH:mm:ssZ");

            string cashregister_factory_number = GetInfo(ipAddress).data.cashregister_factory_number;
            var accessToken = Login(ipAddress, cashregister_factory_number);

            Parameters parameters = new Parameters
            {
                access_token = accessToken,
                from = start,
                to = end,
            };

            RootObject root = new RootObject
            {
                parameters = parameters,
                operationId = "getPeriodicZReport",
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            RestClient rest = new RestClient();
            RestRequest request = new RestRequest(ipAddress, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(json, DataFormat.Json);
            RestResponse response = rest.Execute(request);

            Root weatherForecast = System.Text.Json.JsonSerializer.Deserialize<Root>(response.Content);

            if (weatherForecast.message is "Successful operation")
            {
                return weatherForecast;
            }

            return null;
        }

        public static void Sales(SalesDto salesData)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(salesData.AccessToken))
                {
                    salesData.AccessToken = Login(salesData.IpAddress, TerminalTokenData.NkaSerialNumber);
                }


                List<SalesRequest.Item> items = new List<SalesRequest.Item>();
                List<SalesRequest.VatAmount> vatAmounts = new List<SalesRequest.VatAmount>();

                using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    string query = $@"
select 
    t.name,
    t.item_id,
    t.salePrice,
	t.purchasePrice,
    t.quantity,   
	case t.vatType 
        when 1 then '18' 
        when 2 then '18' 
        when 3 then '0'
        when 4 then '2' 
        when 5 then '8'
    end as vatType, 
		case t.vatType 
        when 1 then '18%' 
		when 2 then N'TİCARƏT ƏLAVƏSİ 18%'
        when 3 then N'ƏDV-SİZ' 
        when 4 then 'SV-2%' 
        when 5 then '8%' 
    end as vatTypeName,
    t.quantityType,
    t.salePrice * t.quantity as ssum
from dbo.item as t
WHERE user_id = {Properties.Settings.Default.UserID}";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            decimal vatSumFor18Percent = 0;
                            decimal vatSumFor2Percent = 0;
                            decimal vatSumFor0Percent = 0;
                            decimal vatSumFor8Percent = 0;

                            while (dr.Read())
                            {
                                string name = dr["name"].ToString();
                                string code = dr["item_id"].ToString();
                                decimal salePrice = Convert.ToDecimal(dr["salePrice"]);
                                decimal? purchasePrice = Convert.ToDecimal(dr["purchasePrice"]);
                                double quantity = Convert.ToDouble(dr["quantity"]);
                                int vatType = Convert.ToInt32(dr["vatType"]);
                                string vatTypeName = dr["vatTypeName"].ToString();
                                int quantityType = Convert.ToInt32(dr["quantityType"]);
                                decimal ssum = Convert.ToDecimal(dr["ssum"]);

                                decimal? marginSum = purchasePrice * (decimal)quantity;

                                if (vatTypeName != "TİCARƏT ƏLAVƏSİ 18%")
                                {
                                    marginSum = null;
                                    purchasePrice = null;
                                }

                                SalesRequest.Item item = new SalesRequest.Item
                                {
                                    itemName = name,
                                    itemCode = code,
                                    itemQuantityType = quantityType,
                                    itemQuantity = quantity,
                                    itemPrice = salePrice,
                                    itemSum = ssum,
                                    itemVatPercent = vatType,
                                    itemMarginPrice = purchasePrice,
                                    itemMarginSum = marginSum,
                                    discount = 0,
                                };
                                items.Add(item);

                                if (vatType == 18)
                                {
                                    vatSumFor18Percent += ssum;
                                }
                                else if (vatType == 2)
                                {
                                    vatSumFor2Percent += ssum;
                                }
                                else if (vatType == 0)
                                {
                                    vatSumFor0Percent += ssum;
                                }
                                else if (vatType == 8)
                                {
                                    vatSumFor8Percent += ssum;
                                }
                            }
                            if (vatSumFor18Percent > 0)
                            {
                                vatAmounts.Add(new SalesRequest.VatAmount
                                {
                                    vatPercent = 18,
                                    vatSum = vatSumFor18Percent
                                });
                            }

                            if (vatSumFor2Percent > 0)
                            {
                                vatAmounts.Add(new SalesRequest.VatAmount
                                {
                                    vatPercent = 2,
                                    vatSum = vatSumFor0Percent
                                });
                            }

                            if (vatSumFor0Percent > 0)
                            {
                                vatAmounts.Add(new SalesRequest.VatAmount
                                {
                                    vatPercent = 0,
                                    vatSum = vatSumFor0Percent
                                });
                            }

                            if (vatSumFor8Percent > 0)
                            {
                                vatAmounts.Add(new SalesRequest.VatAmount
                                {
                                    vatPercent = 8,
                                    vatSum = vatSumFor8Percent
                                });
                            }

                            SalesRequest.Data data = new SalesRequest.Data
                            {
                                sum = salesData.Total,
                                cashSum = salesData.Cash,
                                cashlessSum = salesData.Card,
                                incomingSum = salesData.IncomingSum,
                                cashier = salesData.Cashier,
                                items = items,
                                vatAmounts = vatAmounts,
                            };

                            SalesRequest.Parameters parameters = new SalesRequest.Parameters
                            {
                                access_token = salesData.AccessToken,
                                data = data,
                            };

                            string json = Newtonsoft.Json.JsonConvert.SerializeObject(parameters, new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

                            var client = new RestClient();
                            var request = new RestRequest(salesData.IpAddress, Method.Post);
                            request.AddHeader("Content-Type", "application/json;charset=utf-8");
                            request.AddStringBody(json, DataFormat.Json);
                            RestResponse response = client.Execute(request);

                            nbaroot weatherForecast = System.Text.Json.JsonSerializer.Deserialize<nbaroot>(response.Content);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region [..Request Classes..]

        public class SalesRequest
        {
            public class Item
            {
                public string itemName { get; set; }
                public int? itemCodeType { get; set; } = null;
                public string itemCode { get; set; }
                public int itemQuantityType { get; set; }
                public double itemQuantity { get; set; }
                public decimal itemPrice { get; set; }
                public decimal? discount { get; set; }
                public decimal? itemMarginPrice { get; set; }
                public decimal? itemMarginSum { get; set; }
                public decimal itemSum { get; set; }
                public int itemVatPercent { get; set; }
            }
            public class VatAmount
            {
                public decimal vatSum { get; set; }
                public int vatPercent { get; set; }
            }
            public class Data
            {
                public string cashier { get; set; }
                public string currency { get; set; } = "AZN";
                public List<Item> items { get; set; }
                public decimal sum { get; set; }
                public decimal cashSum { get; set; }
                public decimal cashlessSum { get; set; }
                public decimal prepaymentSum { get; set; } = 0;
                public decimal creditSum { get; set; } = 0;
                public decimal bonusSum { get; set; } = 0;
                public decimal incomingSum { get; set; }
                public decimal changeSum { get; set; } = 0;
                public List<VatAmount> vatAmounts { get; set; }
            }
            public class Parameters
            {
                public string access_token { get; set; }
                public string doc_type { get; set; } = "sale";
                public string operationId { get; set; } = "createDocument";
                public int version { get; set; } = 1;
                public Data data { get; set; }
            }
        }

        public class Item
        {
            public string itemName { get; set; }
            public int? itemCodeType { get; set; } = null;
            public string itemCode { get; set; }
            public int itemQuantityType { get; set; }
            public int itemQuantity { get; set; }
            public decimal itemPrice { get; set; }
            public decimal itemSum { get; set; }
            public int itemVatPercent { get; set; }
        }

        public class VatAmount
        {
            public decimal vatSum { get; set; }
            public string vatPercent { get; set; }
        }

        public class Data
        {
            public string cashier { get; set; }
            public string currency { get; set; } = "AZN";
            public List<Item> items { get; set; }
            public decimal sum { get; set; }
            public decimal cashSum { get; set; }
            public decimal cashlessSum { get; set; }
            public decimal prepaymentSum { get; set; } = 0;
            public decimal creditSum { get; set; } = 0;
            public decimal bonusSum { get; set; } = 0;
            public decimal incomingSum { get; set; }
            public decimal changeSum { get; set; } = 0;
            public List<VatAmount> vatAmounts { get; set; }
        }

        public class Parameters
        {
            public string from { get; set; }
            public string to { get; set; }
            public string access_token { get; set; }
            public string doc_type { get; set; }
            public int? prev_doc_number { get; set; } = null;
            public string pin { get; set; } = null;
            public string role { get; set; } = null;
            public string cashregister_factory_number { get; set; } = null;
            public Data data { get; set; }
        }

        public class RootObject
        {
            public Parameters parameters { get; set; }
            public string operationId { get; set; } = "createDocument";
            public int version { get; set; } = 1;
        }


        #endregion [..Request Classes..]



        #region [..Response Classes..]


        public class Currency
        {
            public string currency { get; set; }
            public int saleCount { get; set; }
            public double saleSum { get; set; }
            public double saleCashSum { get; set; }
            public double saleCashlessSum { get; set; }
            public double salePrepaymentSum { get; set; }
            public double saleCreditSum { get; set; }
            public double saleBonusSum { get; set; }
            public List<SaleVatAmount> saleVatAmounts { get; set; }
            public int depositCount { get; set; }
            public double depositSum { get; set; }
            public int withdrawCount { get; set; }
            public double withdrawSum { get; set; }
            public int moneyBackCount { get; set; }
            public double moneyBackSum { get; set; }
            public double moneyBackCashSum { get; set; }
            public double moneyBackCashlessSum { get; set; }
            public double moneyBackPrepaymentSum { get; set; }
            public double moneyBackCreditSum { get; set; }
            public double moneyBackBonusSum { get; set; }
            public List<MoneyBackVatAmount> moneyBackVatAmounts { get; set; }
            public int rollbackCount { get; set; }
            public double rollbackSum { get; set; }
            public double rollbackCashSum { get; set; }
            public double rollbackCashlessSum { get; set; }
            public double rollbackPrepaymentSum { get; set; }
            public double rollbackCreditSum { get; set; }
            public double rollbackBonusSum { get; set; }
            public List<object> rollbackVatAmounts { get; set; }
            public int correctionCount { get; set; }
            public double correctionSum { get; set; }
            public double correctionCashSum { get; set; }
            public double correctionCashlessSum { get; set; }
            public double correctionPrepaymentSum { get; set; }
            public double correctionCreditSum { get; set; }
            public double correctionBonusSum { get; set; }
            public List<object> correctionVatAmounts { get; set; }
            public int prepayCount { get; set; }
            public double prepaySum { get; set; }
            public double prepayCashSum { get; set; }
            public double prepayCashlessSum { get; set; }
            public double prepayPrepaymentSum { get; set; }
            public double prepayCreditSum { get; set; }
            public double prepayBonusSum { get; set; }
            public List<object> prepayVatAmounts { get; set; }
            public int creditpayCount { get; set; }
            public double creditpaySum { get; set; }
            public double creditpayCashSum { get; set; }
            public double creditpayCashlessSum { get; set; }
            public double creditpayPrepaymentSum { get; set; }
            public double creditpayCreditSum { get; set; }
            public double creditpayBonusSum { get; set; }
            public List<object> creditpayVatAmounts { get; set; }
        }

        public class DataResponse
        {
            public DateTime shiftOpenAtUtc { get; set; }
            public object shiftCloseAtUtc { get; set; }
            public DateTime createdAtUtc { get; set; }
            public int reportNumber { get; set; }
            public int firstDocNumber { get; set; }
            public int lastDocNumber { get; set; }
            public int docCountToSend { get; set; }
            public List<Currency> currencies { get; set; }
            public string document_id { get; set; }
        }

        public class MoneyBackVatAmount
        {
            public double vatSum { get; set; }
            public double vatPercent { get; set; }
        }

        public class Root
        {
            public DataResponse data { get; set; }
            public int code { get; set; }
            public string message { get; set; }
        }

        public class SaleVatAmount
        {
            public double vatSum { get; set; }
            public double vatPercent { get; set; }
        }

        public class NbaResponse
        {
            public Data2 data { get; set; }
            public string message { get; set; }
            public int code { get; set; }
        }

        public class Data2
        {
            public bool shift_open { get; set; }
            public DateTime? shift_open_time { get; set; }
            public string cashregister_factory_number { get; set; }
            public string cashbox_tax_number { get; set; }
            public string company_tax_number { get; set; }
            public string company_name { get; set; }
            public string object_tax_number { get; set; }
            public string object_name { get; set; }
            public string object_address { get; set; }
            public string cashbox_factory_number { get; set; }
            public string cashregister_model { get; set; }
            public string access_token { get; set; }
            public string document_id { get; set; }
            public int document_number { get; set; }
            public int shift_document_number { get; set; }
            public string short_document_id { get; set; }
            public string shiftOpenAtUtc { get; set; }
            public string shiftCloseAtUtc { get; set; }
            public int firstDocNumber { get; set; }
            public int lastDocNumber { get; set; }
            public int reportNumber { get; set; }
            public Data3[] currencies { get; set; }
        }

        public class Data3
        {
            public int saleCount { get; set; }
            public double saleSum { get; set; }
            public double saleCashSum { get; set; }
            public double saleCashlessSum { get; set; }
            public double salePrepaymentSum { get; set; }
            public double saleCreditSum { get; set; }
            public double saleBonusSum { get; set; }
            public int depositCount { get; set; }

            public int depositSum { get; set; }
            public int moneyBackCount { get; set; }
            public double moneyBackSum { get; set; }
            public double moneyBackCashSum { get; set; }
            public double moneyBackCashlessSum { get; set; }
            public Data4[] saleVatAmounts { get; set; }
            public Data5[] moneyBackVatAmounts { get; set; }
        }

        public class Data4
        {
            public double vatPercent { get; set; }
            public double vatSum { get; set; }
        }

        public class Data5
        {
            public double vatPercent { get; set; }
            public double vatSum { get; set; }
        }

        #endregion [..Response Classes..]


        public class NbaSalesReturnData
        {

        }

        public class GetInfoResponse
        {
            public Data data { get; set; }
            public int code { get; set; }
            public string message { get; set; }

            public class Data
            {
                public string company_tax_number { get; set; }
                public string company_name { get; set; }
                public string object_tax_number { get; set; }
                public string object_name { get; set; }
                public string object_address { get; set; }
                public string cashbox_tax_number { get; set; }
                public string cashbox_factory_number { get; set; }
                public string firmware_version { get; set; }
                public string cashregister_factory_number { get; set; }
                public string cashregister_model { get; set; }
                public string qr_code_url { get; set; }
                public DateTime not_before { get; set; }
                public DateTime not_after { get; set; }
                public string state { get; set; }
                public DateTime last_online_time { get; set; }
                public object oldest_document_time { get; set; }
                public int last_doc_number { get; set; }
                public string production_uuid { get; set; }
            }
        }

        public class LastDocumentResponse
        {
            public Data data { get; set; }
            public int code { get; set; }
            public string message { get; set; }

            public class Data
            {
                public string document_id { get; set; }
                public string docType { get; set; }
                public Doc doc { get; set; }
            }

            public class Doc
            {
                public string cashier { get; set; }
                public string currency { get; set; }
                public int docNumber { get; set; }
                public int positionInShift { get; set; }
                public DateTime createdAtUtc { get; set; }
                public object rrn { get; set; }
                public object bonusCardNumber { get; set; }
                public object creditContract { get; set; }
                public object creditPayer { get; set; }
                public object parents { get; set; }
                public List<Item> items { get; set; }
                public double sum { get; set; }
                public double cashSum { get; set; }
                public double cashlessSum { get; set; }
                public double prepaymentSum { get; set; }
                public double creditSum { get; set; }
                public double bonusSum { get; set; }
                public double changeSum { get; set; }
                public double incomingSum { get; set; }
                public List<VatAmount> vatAmounts { get; set; }
                public object guestsCount { get; set; }
                public object carNumber { get; set; }
                public object gasStationInfo { get; set; }
                public object committentInfo { get; set; }
            }

            public class Item
            {
                public string itemName { get; set; }
                public int itemCodeType { get; set; }
                public string itemCode { get; set; }
                public object itemMarkingCode { get; set; }
                public int itemQuantityType { get; set; }
                public double itemQuantity { get; set; }
                public double itemPrice { get; set; }
                public double itemSum { get; set; }
                public double itemVatPercent { get; set; }
                public object itemMarginPrice { get; set; }
                public object itemMarginSum { get; set; }
                public object itemAgentCommission { get; set; }
            }

            public class VatAmount
            {
                public double vatSum { get; set; }
                public double vatPercent { get; set; }
            }
        }
    }
}
