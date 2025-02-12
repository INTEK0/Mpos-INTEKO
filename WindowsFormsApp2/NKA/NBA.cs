using DevExpress.Xpo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json;
using System.Windows.Forms;
using WindowsFormsApp2.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.Messages;

namespace WindowsFormsApp2.NKA
{
    public class NBA
    {
        public static readonly string NBA_FISCAL_SERVICE_PORT = "9847"; //9898 prod port - 9847 test port
        public static readonly string NBA_BANK_SERVICE_PORT = "9944"; //9999 prod port - 9944 test port


        public static (string json, string edvhesap1, string edvhesap2, string edvdenazad2, string odenen, string qaliq) Sales(decimal _total, decimal _cash, decimal _card, decimal _incomingSum, string _cashier, string _accessToken, string rrn = default)
        {
            List<Item> items = new List<Item>();
            List<VatAmount> vatAmounts = new List<VatAmount>();


            decimal _edvlitoplam = default;
            decimal _edvlitoplam2 = default;
            decimal _odenen = default;
            decimal _qaliq = default;
            decimal _edvsiz = default;
            string _vatType = string.Empty;
            decimal qaliq = default;


            if (_card == 0)
            {
                qaliq = _incomingSum - _total;
            }
            //else if (_card > 0 && _cash > 0)
            //{
            //    qaliq = _incomingSum - _total;
            //}

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.SqlCon;

                using (SqlCommand cmd = new SqlCommand())
                {
                    conn.Open();

                    string query = @"
   select 
    t.name,
    t.item_id,
    t.salePrice,
    t.quantity,   
	case t.vatType 
        when 1 then '18' 
        when 4 then '2' 
        when 5 then '8' 
        else '0' 
    end as vatType, 
    t.quantityType,
    t.salePrice * t.quantity as total, 
    ROUND(
        case t.vatType 
            when 1 then t.salePrice * t.quantity * 0.18 
            when 3 then 0 
            when 4 then t.salePrice * t.quantity * 0.02 
            else 0 
        end, 
    2) as ssumvat,
    SUM(t.salePrice * t.quantity) OVER (PARTITION BY t.vatType) as EdvliToplam,
    SUM(
        ROUND(
            case t.vatType 
                when 1 then t.salePrice * t.quantity * 0.18 
                when 3 then 0 
                when 4 then t.salePrice * t.quantity * 0.02 
                else 0 
            end, 
        2)
    ) OVER (PARTITION BY t.vatType) as EdvsizToplam
from dbo.item as t;
";

                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string name = dr["name"].ToString();
                            string code = dr["item_id"].ToString();
                            decimal salePrice = Convert.ToDecimal(dr["salePrice"]);
                            decimal quantity = Convert.ToDecimal(dr["quantity"]);
                            int quantityType = Convert.ToInt32(dr["quantityType"]);
                            decimal total = Convert.ToDecimal(dr["total"]);
                            //string taxName = dr["TaxName"].ToString();
                            int TaxPrc = Convert.ToInt32(dr["vatType"]);

                            int miqdar = Convert.ToInt32(quantity);
                            Item itemProduct = new Item
                            {
                                itemName = name,
                                itemCode = code,
                                itemCodeType = 0,
                                itemQuantityType = quantityType,
                                itemQuantity = miqdar,
                                itemPrice = salePrice,
                                itemSum = total,
                                itemVatPercent = TaxPrc
                            };
                            items.Add(itemProduct);
                        }
                    }
                }
            }


            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Properties.Settings.Default.SqlCon;
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query2 = @"
select t.vatType,sum(t.ssum) as ssum,sum(t.ssumvat) as ssumvat 
from( select  case  vatType when 1 then '18' when 3 then '0' when 4 then '2' when 5 then '8' else 'bos' end as vatType,
salePrice* quantity as ssum,ROUND(case vatType when 1 then (salePrice* quantity)*18/118 when 3 then 0 when 4 then salePrice* quantity*0.02 else 0 end,2)
as ssumvat  from dbo.item  ) as t group by t.vatType";

                    cmd.Connection = conn;
                    cmd.CommandText = query2;
                    conn.Open();
                    using (SqlDataReader dr2 = cmd.ExecuteReader())
                    {
                        while (dr2.Read())
                        {
                            string vatType = dr2["vatType"].ToString();
                            if (vatType == "18")
                            {
                                _edvlitoplam = Convert.ToDecimal(dr2["ssum"].ToString());
                                _edvlitoplam2 = Convert.ToDecimal(dr2["ssumvat"].ToString());
                            }
                            else
                            {
                                _edvsiz = Convert.ToDecimal(dr2["ssum"].ToString());
                            }

                            VatAmount vatAmount = new VatAmount
                            {
                                vatPercent = vatType,
                                vatSum = _edvlitoplam
                            };
                            vatAmounts.Add(vatAmount);
                        }
                    }
                }
            }

            _odenen = _incomingSum;
            _qaliq = qaliq;


            Data data = new Data()
            {
                sum = _total,
                cashSum = _cash,
                cashlessSum = _card,
                incomingSum = _incomingSum,
                cashier = _cashier,
                items = items,
                vatAmounts = vatAmounts,
                changeSum = qaliq

            };

            Parameters parameters = new Parameters
            {
                access_token = _accessToken,
                doc_type = "sale",
                data = data
            };


            RootObject rootObject = new RootObject
            {
                parameters = parameters
            };


            string json = JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            return (json, _edvlitoplam.ToString(), _edvlitoplam2.ToString(), _edvsiz.ToString(), _odenen.ToString(), _qaliq.ToString());
        }

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
                pin = "23264544",
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



        #region [..Request Classes..]

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
