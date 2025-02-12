﻿using ComponentFactory.Krypton.Toolkit;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;
using Method = RestSharp.Method;

namespace WindowsFormsApp2.NKA
{
    public static class Sunmi
    {
        private static readonly POS_LAYOUT_NEW posPage;
        private static readonly bool MessageVisible = FormHelpers.SuccessMessageVisible();

        private static ResponseData RequestPOST(string ipAddress, string json)
        {
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


            ResponseData responseData = System.Text.Json.JsonSerializer.Deserialize<ResponseData>(response.Content);
            return responseData;
        }

        public static GetInfoResponse GetInfo(string ipAddress)
        {
            var root = new
            {
                operation = "getInfo",
                username = "username",
                password = "password",
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

                if (weatherForecast.message != "Successful operation" && weatherForecast.message != "Success operation")
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

        public static void OpenShift(string ipAddress, string cashier)
        {
            RootObject root = new RootObject
            {
                cashierName = cashier,
                operation = "openShift"
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json);

            if (response != null)
            {
                if ($"{response.message}" == "Success operation" || $"{response.message}" == "Successful operation")
                {
                    ReadyMessages.SUCCESS_OPEN_SHIFT_MESSAGE();
                    FormHelpers.Log(CommonData.SUCCESS_OPEN_SHIFT);
                }
                else if (response.message is "Növbə artıq açıqdır")
                {
                    MessageBox.Show(response.message, nameof(HeaderMessage.Mesaj), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ReadyMessages.ERROR_OPENSHIFT_MESSAGE(response.message);
                    FormHelpers.Log($"{ErrorMessages.ERROR_OPENSHIFT} Xəta mesajı: {response.message}");
                }
            }
        }

        public static void GetShiftStatus(string ipAddress, string cashier)
        {
            RootObject root = new RootObject
            {
                cashierName = cashier,
                operation = "getShiftStatus",
                data = null
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json);

            if (response != null)
            {
                if ($"{response.message}" == "Success operation" || $"{response.message}" == "Successful operation")
                {
                    if (response.data.shift_open)
                    {
                        string open_time = Convert.ToDateTime(response.data.shift_open_time).ToString("dd.MM.yyyy HH:mm:ss");
                        ReadyMessages.SUCCESS_SHIFT_STATUS_MESSAGE(open_time);
                    }
                    else
                    {
                        OpenShift(ipAddress, cashier);
                    }
                }
                else
                {
                    ReadyMessages.ERROR_OPENSHIFT_MESSAGE(response.message);
                }
            }
        }

        public static void CloseShift(string ipAddress, string cashier)
        {
            RootObject root = new RootObject
            {
                cashierName = cashier,
                operation = "closeShift"
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json);

            if (response != null)
            {
                if ($"{response.message}" == "Success operation" || $"{response.message}" == "Successful operation")
                {
                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_CLOSE_SHIFT_MESSAGE();
                    }

                    FormHelpers.Log(CommonData.SUCCESS_CLOSE_SHIFT);
                }
                else
                {
                    ReadyMessages.ERROR_DEFAULT_MESSAGE(response.message);
                    FormHelpers.Log($"Xəta mesajı: {response.message}");
                }
            }

        }

        public static void LastDocument(string ipAddress)
        {
            RootObject root = new RootObject
            {
                operation = "printLastCheque",
                cashierName = null
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json);

            if (response.message == "Success operation" || response.message == "Successful operation")
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

        public static void X_Report(string ipAddress, string cashier)
        {
            RootObject root = new RootObject
            {
                operation = "getXReport",
                cashierName = cashier,
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json);

            if (response != null)
            {
                if (response.message == "Success operation" || response.message == "Successful operation")
                {
                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_X_REPORT_MESSAGE();
                    }

                    FormHelpers.Log("GÜNLÜK (X) HESABATI UĞURLA ÇIXARILDI");
                }
                else
                {
                    ReadyMessages.ERROR_X_REPORT_MESSAGE(response.message);
                    FormHelpers.Log($"{ErrorMessages.ERROR_X_REPORT}  Xəta mesajı: {response.message}");
                }
            }
        }

        public static bool Sales(string ipAddress, string proccessNo, decimal cash, decimal card, decimal total, string cashier, Customer customer, Doctor doctor, string rrn = "")
        {
            List<Item> items = new List<Item>();

            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.SqlCon;
            conn.Open();
            string query = $@"SELECT 
                              name,
                              --Item.item_id,
                              code,
                              salePrice,
                              quantity,
                              vatType,
                              quantityType,
                              salePrice*quantity as ssum
                              FROM dbo.item WHERE user_id = {Properties.Settings.Default.UserID};";

            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string name = dr["name"].ToString();
                string code = dr["code"].ToString();
                decimal salePrice = Convert.ToDecimal(dr["salePrice"]);
                double quantity = Convert.ToDouble(dr["quantity"]);
                int vatType = Convert.ToInt32(dr["vatType"]);
                int quantityType = Convert.ToInt32(dr["quantityType"]);
                double ssum = Convert.ToDouble(dr["ssum"]);
                salePrice = Math.Round(salePrice, 2);

                Item itemProduct = new Item
                {
                    name = name,
                    code = code,
                    salePrice = salePrice,
                    quantity = quantity,
                    vatType = vatType,
                    quantityType = quantityType,
                    discountAmount = 0
                };
                items.Add(itemProduct);
            }

            Data data = new Data
            {
                documentUUID = Guid.NewGuid().ToString(),
                cashPayment = cash,
                cardPayment = card,
                bonusPayment = 0,
                items = items,
                cashierName = cashier,
                clientName = customer == null ? null : $"{customer.Name} {customer.Surname} {customer.FatherName}",
                rrn = rrn,
                moneyBackType = null
            };

            RootObject rootObject = new RootObject
            {
                data = data,
                operation = "sale",
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json);

            if (response != null)
            {
                if (response.message is "Success operation" || response.message is "Successful operation")
                {
                    DbProsedures.InsertPosSales(new PosSales
                    {
                        posNomre = response.data.number,
                        longFiskalId = response.data.document_id,
                        proccessNo = proccessNo,
                        cash = cash,
                        card = card,
                        total = total,
                        json = json,
                        shortFiskalId = response.data.short_document_id,
                        rrn = response.data.rrn,
                        customerId = customer?.CustomerID,
                        doctorId = doctor?.Id,
                    });

                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_SALES_MESSAGE();
                    }

                    FormHelpers.Log($"Pos satışı uğurla edildi. Qəbz No: {response.data.number}");
                    return true;
                }
                else if (response.message is "document: invalid shift duration")
                {
                    XtraMessageBox.Show("GÜN SONU (Z) HESABATI ÇIXARILMAYIB !\n\nZəhmət olmasa pos bağla düyməsinə vuraraq günü sonlandırın.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
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

        public static bool ReturnPos(string url, string cashier, string proccesNo, string _bankrrn = "")
        {
            string fiskallID = "";
            decimal cash = default;
            decimal card = default;

            SqlConnection conn2 = new SqlConnection();
            SqlCommand cmd2 = new SqlCommand();
            conn2.ConnectionString = Properties.Settings.Default.SqlCon;
            conn2.Open();
            string query2 = "SELECT  [pos_satis_check_main_id],[pos_nomre],[fiscal_id],[date_] ,[user_id_] ," +
                "[emeliyyat_nomre],[NEGD_],[KART_],[UMUMI_MEBLEG] ,[json_] ,[fiscalNum],[documentID]" +
                "  FROM [pos_satis_check_main] WHERE[pos_satis_check_main_id] IN(SELECT[pos_satis_check_main_id]  " +
                " FROM [pos_gaytarma_manual] where [pos_gaytarma_manual_id] =(select max([pos_gaytarma_manual_id]) " +
                "from [pos_gaytarma_manual])); ";



            cmd2.Connection = conn2;
            cmd2.CommandText = query2;

            SqlDataReader dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {
                decimal cash1 = Convert.ToDecimal(dr2["NEGD_"].ToString());
                decimal card1 = Convert.ToDecimal(dr2["KART_"].ToString());


                string fiscal_id = dr2["fiscal_id"].ToString();
                //string fiscalNum = dr2["fiscalNum"].ToString();


                fiskallID = fiscal_id;
                cash = cash1;
                card = card1;
            }


            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.SqlCon;
            conn.Open();


            string query = $@"(SELECT md.MEHSUL_ADI AS name,
                       p.item_id AS code,
                       pl.say AS say,
                       p.satis_giymet AS satis_giymet,
					    pl.say * p.satis_giymet as tutar,
                       p.quantity_type AS quantity_type,
                       md.VERGI_DERECESI AS vtypes
              FROM pos_satis_check_details p
                       INNER JOIN MAL_ALISI_DETAILS md ON p.mal_alisi_details_id = md.MAL_ALISI_DETAILS_ID
                       INNER JOIN pos_gaytarma_manual pl ON p.pos_satis_check_details_id = pl.pos_satis_check_details
              WHERE pl.emeliyyat_nomre = '{proccesNo}')";




            cmd.Connection = conn;
            cmd.CommandText = query;
            List<Item> items = new List<Item>();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string name = dr["name"].ToString();
                string code = dr["code"].ToString();
                decimal salePrice = Convert.ToDecimal(dr["satis_giymet"]);
                double quantity = Convert.ToDouble(dr["say"]);
                int vatType = Convert.ToInt32(dr["vtypes"]);
                int quantityType = Convert.ToInt32(dr["quantity_type"]);
                double ssum = Convert.ToDouble(dr["tutar"]);

                Item itemProduct = new Item
                {
                    name = name,
                    code = code,
                    salePrice = salePrice,
                    quantity = quantity,
                    codeType = 1,
                    vatType = vatType,
                    quantityType = quantityType,
                    discountAmount = 0
                };
                items.Add(itemProduct);
            }

            Data data = new Data
            {
                parentDocumentId = fiskallID,
                documentUUID = Guid.NewGuid().ToString(),
                cashPayment = cash,
                cardPayment = card,
                items = items,
                moneyBackType = 0,
                cashierName = cashier,
                rrn = _bankrrn,
                isManual = true
            };

            RootObject rootObject = new RootObject
            {
                data = data,
                operation = "moneyBack",
            };

            string json = JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });


            RestClient rest = new RestClient();
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(json, DataFormat.Json);
            RestResponse response = rest.Execute(request);

            ResponseData responseData = System.Text.Json.JsonSerializer.Deserialize<ResponseData>(response.Content);
            if ($"{responseData.message}" == "Success operation" || $"{responseData.message}" == "Successful operation")
            {
                if (MessageVisible)
                {
                    ReadyMessages.SUCCESS_RETURN_SALES_MESSAGE();
                }

                FormHelpers.Log($"Qəbz geri qaytarması edildi. Qəbz №: {responseData.data.number}");
                return true;
            }
            else
            {
                FormHelpers.Log($"Pos satış qaytarma xətası. Xəta mesajı: {responseData.message}");
                ReadyMessages.ERROR_RETURN_SALES_MESSAGE(responseData.message);
                return false;
            }
        }

        public static string CreditPay(RootObject rootObject)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return json;
        }

        public static string CreditSale(RootObject rootObject)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return json;
        }

        public static void PeriodicReport(DateTime _start, DateTime _end, string ipAddress)
        {
            string start = _start.ToString("dd.MM.yyyy HH:mm:ss");
            string end = _end.ToString("dd.MM.yyyy HH:mm:ss");

            Data data = new Data
            {
                startDate = start,
                endDate = end,
                clientName = null,
                currency = null
            };

            RootObject root = new RootObject
            {
                operation = "getPeriodicZReport",
                data = data
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json);

            if (response != null)
            {
                if ($"{response.message}" == "Success operation" || $"{response.message}" == "Successful operation")
                {
                    ReadyMessages.SUCCES_PERİODİC_Z_REPORT_MESSAGE();
                    FormHelpers.Log(CommonData.SUCCES_PERİODİC_Z_REPORT);
                }
                else
                {
                    ReadyMessages.ERROR_DEFAULT_MESSAGE(response.message);
                    FormHelpers.Log($"Dövrü hesabat çap edilərkən xəta yarandı. Xəta mesajı: {response.message}");
                }
            }
        }



        #region [..REQUEST CLASS..]
        public class Item
        {
            public string name { get; set; }
            public string code { get; set; }
            public double quantity { get; set; }
            public decimal salePrice { get; set; }
            public double? realPrice { get; set; } = null;
            public decimal? purchasePrice { get; set; } = null;
            public int? codeType { get; set; } = null;
            public int quantityType { get; set; }
            public int vatType { get; set; }
            public double? discountAmount { get; set; } = null;
        }

        public class Data
        {
            public bool? isManual { get; set; } = null;
            public string startDate { get; set; } = null;
            public string endDate { get; set; } = null;
            public string parentDocumentId { get; set; } = null;
            public string documentUUID { get; set; } = null;
            public decimal? cashPayment { get; set; } = null;
            public decimal? creditPayment { get; set; } = null;
            public decimal? depositPayment { get; set; } = null;
            public decimal? cardPayment { get; set; } = null;
            public decimal? bonusPayment { get; set; } = null;
            public List<Item> items { get; set; } = null;
            public int? moneyBackType { get; set; } = null;
            public string clientName { get; set; } = null;
            public double? clientTotalBonus { get; set; } = null;
            public double? clientEarnedBonus { get; set; } = null;
            public string clientBonusCardNumber { get; set; } = null;
            public string cashierName { get; set; } = null;
            public string rrn { get; set; } = null;
            public string currency { get; set; } = "AZN";
            public string creditPayer { get; set; } = null;
            public double? residue { get; set; } = null;
            public string creditContract { get; set; } = null;
            public string paymentNumber { get; set; } = null;
            public string note { get; set; } = null;
        }

        public class RootObject
        {
            public Data data { get; set; }
            public string operation { get; set; }
            public string username { get; set; } = "username";
            public string password { get; set; } = "password";
            public string cashierName { get; set; }
        }
        #endregion [..REQUEST CLASS..]


        #region [..RESPONSE CLASS..]
        public class ResponseDocumentData
        {
            public string document_id { get; set; }
            public int document_number { get; set; }
            public string number { get; set; }
            public int shift_document_number { get; set; }
            public string short_document_id { get; set; }
            public decimal totalSum { get; set; }
            public bool shift_open { get; set; }
            public string shift_open_time { get; set; }
            public string rrn { get; set; }
        }

        public class ResponseData
        {
            public ResponseDocumentData data { get; set; }
            public string code { get; set; }
            public string message { get; set; }
            public string document_number { get; set; }

        }

        public class GetInfoResponse
        {
            public Data data { get; set; }
            public string code { get; set; }
            public string message { get; set; }

            public class Data
            {
                public string application_version { get; set; }
                public string cashregister_model { get; set; }
                public string cashbox_tax_number { get; set; }
                public string cashbox_factory_number { get; set; }
                public string cashregister_factory_number { get; set; }
                public string cashbox_serial_number { get; set; }
                public string company_name { get; set; }
                public string company_tax_number { get; set; }
                public float last_doc_number { get; set; }
                public string last_online_time { get; set; }
                public string not_after { get; set; }
                public string not_before { get; set; }
                public string object_address { get; set; }
                public string object_name { get; set; }
                public string object_tax_number { get; set; }
                public string pks_driver_version { get; set; }
                public string qr_code_url { get; set; }
                public string state { get; set; }
                public string token_version { get; set; }
            }
        }

        #endregion [..RESPONSE CLASS..]
    }
}