using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWin.Design;
using DevExpress.Data.Helpers;
using DevExpress.DataAccess.Native.Web;
using DevExpress.XtraEditors;
using DevExpress.XtraMap.Native;
using Newtonsoft.Json;
using RestSharp;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.CacheData;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static DTOs;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;
using Method = RestSharp.Method;

namespace WindowsFormsApp2.NKA
{
    public static class Sunmi
    {
        private static readonly bool MessageVisible = FormHelpers.SuccessMessageVisible();
        private static readonly RestClient _restClient = new RestClient();

        private static ResponseData RequestPOST(string url, string json)
        {
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(json, DataFormat.Json);
            RestResponse response = _restClient.Execute(request);
            if (string.IsNullOrWhiteSpace(response?.Content))
            {
                return new ResponseData
                {
                    code = "506",
                    message = "Error",
                    requestJson = json
                };
            }

            ResponseData responseData = System.Text.Json.JsonSerializer.Deserialize<ResponseData>(response.Content);
            responseData.requestJson = json;
            responseData.responseJson = response.Content;
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

        public static void Deposit(DepositDto _data)
        {
            DepositRequest.Data data = new DepositRequest.Data
            {
                cashierName = _data.Cashier,
                sum = _data.Sum,
            };

            DepositRequest.Root root = new DepositRequest.Root
            {
                data = data
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            RestClient rest = new RestClient();
            RestRequest request = new RestRequest(_data.IpAddress, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(json, DataFormat.Json);
            RestResponse response = rest.Execute(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE();
                FormHelpers.Log($"Kassa ilə əlaqə zamanı xəta yarandı\n\n {response.ErrorMessage}");
                return;
            }

            DepositResponse.Root responseData = System.Text.Json.JsonSerializer.Deserialize<DepositResponse.Root>(response.Content);

            if (responseData.message is "Successoperation" ||
                responseData.message is "Success operation" ||
                responseData.message is "Successful operation")
            {
                string message = $"Kassaya {_data.Sum} AZN uğurla mədaxil edildi";
                ReadyMessages.SUCCESS_DEFAULT_MESSAGE(message);
                FormHelpers.Log(message);
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE($"Xəta kodu: {responseData.code}\n\nMesaj: {responseData.message}");
            }
        }

        public static void Withdraw(WithdrawDto _data)
        {
            WithdrawRequest.Data data = new WithdrawRequest.Data
            {
                cashierName = _data.Cashier,
                sum = _data.Sum,
            };

            WithdrawRequest.Root root = new WithdrawRequest.Root
            {
                data = data
            };

            string json = JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            RestClient rest = new RestClient();
            RestRequest request = new RestRequest(_data.IpAddress, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(json, DataFormat.Json);
            RestResponse response = rest.Execute(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE();
                FormHelpers.Log($"Kassa ilə əlaqə zamanı xəta yarandı\n\n {response.ErrorMessage}");
                return;
            }

            WithdrawResponse.Root responseData = System.Text.Json.JsonSerializer.Deserialize<WithdrawResponse.Root>(response.Content);

            if (responseData.message is "Successoperation" ||
                responseData.message is "Success operation" ||
                responseData.message is "Successful operation")
            {
                string message = $"Kassadan {_data.Sum} AZN uğurla məxaric edildi";
                ReadyMessages.SUCCESS_DEFAULT_MESSAGE(message);
                FormHelpers.Log(message);
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE($"Xəta kodu: {responseData.code}\n\nMesaj: {responseData.message}");
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
            bool IsBank = true;
            if (!string.IsNullOrWhiteSpace(UserCacheService.Terminal.BankName) && UserCacheService.Terminal?.BankName != "PAX A35")
            {
                IsBank = false;
                BankRequest bank = new BankRequest()
                {
                    data = null,
                    operation = "transactionTapXphoneCloseDay"
                };

                string Bankjson = JsonConvert.SerializeObject(bank, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                var Bankresponse = RequestPOST(ipAddress, Bankjson);

                //if (//response success olaraq gələrsə)
                //{
                //    IsBank = true;
                //}else
                //{
                //    IsBank = false;
                //    //Xəta mesajı
                //}
                //IsBank = true;
            }

            if (IsBank)
            {
                CloseShiftRequest request = new CloseShiftRequest()
                {
                    data = new CloseShiftRequest.Data()
                    {
                        cashierName = cashier,
                        documentUUID = Guid.NewGuid().ToString()
                    }
                };



                string json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                var response = RequestPOST(ipAddress, json);

                if (response != null)
                {
                    if (response.message is "Success operation" || response.message is "Successful operation")
                    {
                        if (MessageVisible)
                            ReadyMessages.SUCCESS_CLOSE_SHIFT_MESSAGE();

                        FormHelpers.Log(CommonData.SUCCESS_CLOSE_SHIFT);
                    }
                    else
                    {
                        ReadyMessages.ERROR_DEFAULT_MESSAGE(response.message);
                        FormHelpers.Log($"Xəta mesajı: {response.message}");
                    }
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

        public static bool Sales(SalesDto salesData)
        {
            int posSaleId = 0;
            string _requestJson = null;
            string _responseJson = null;
            try
            {
                List<Item> items = new List<Item>();

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
                using (SqlConnection conn = new SqlConnection(DbHelpers.CurrentConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string name = dr["name"].ToString();
                            string code = dr["code"].ToString();
                            decimal salePrice = Convert.ToDecimal(dr["salePrice"]);
                            decimal quantity = Convert.ToDecimal(dr["quantity"]);
                            int vatType = Convert.ToInt32(dr["vatType"]);
                            int quantityType = Convert.ToInt32(dr["quantityType"]);
                            decimal discount = Convert.ToDecimal(dr["discount"]);
                            salePrice = Math.Round(salePrice, 2);

                            Item itemProduct = new Item
                            {
                                name = name,
                                code = code,
                                salePrice = salePrice,
                                quantity = quantity,
                                vatType = vatType,
                                quantityType = quantityType,
                                discountAmount = discount
                            };
                            items.Add(itemProduct);
                        }
                    }
                }

                Data data = new Data
                {
                    documentUUID = salesData.DocumentUUID,
                    cashPayment = salesData.Cash,
                    cardPayment = salesData.Card,
                    bonusPayment = 0,
                    items = items,
                    cashierName = salesData.Cashier,
                    clientName = salesData.Customer == null ? null : $"{salesData.Customer.Name} {salesData.Customer.Surname} {salesData.Customer.FatherName}",
                    rrn = string.IsNullOrWhiteSpace(salesData.Rrn) ? null : salesData.Rrn,
                    moneyBackType = null
                };

                if (salesData.PayType is PayType.Card &&
                    !string.IsNullOrWhiteSpace(UserCacheService.Terminal.BankName) &&
                    UserCacheService.Terminal.BankName != "PAX A35")
                {
                    if (SaleBank(salesData))
                    {
                        var responseBank = BankCheckStatus(salesData.IpAddress, salesData.DocumentUUID);

                        if (responseBank is null || string.IsNullOrWhiteSpace(responseBank?.data?.rrn))
                            return false;  // Bank uğursuzdursa, satış dayansın

                        data.rrn = responseBank.data.rrn;
                    }
                    else
                        return false;
                }


                RootObject rootObject = new RootObject
                {
                    data = data,
                    operation = "sale",
                };

                decimal totalSum = (decimal)items.Sum(x => (x.salePrice * x.quantity) - x.discountAmount);

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                var response = RequestPOST(salesData.IpAddress, json);

                _requestJson = json;
                _responseJson = response.responseJson;

                if (response.message != "error" && response.code != "506")
                {
                    switch (response.message)
                    {
                        case "Success operation":
                        case "Successful operation":
                            posSaleId = DbProsedures.InsertPosSales(new PosSales
                            {
                                posNomre = response.data.number,
                                longFiskalId = response.data.document_id,
                                proccessNo = salesData.ProccessNo,
                                cash = salesData.Cash,
                                card = salesData.Card,
                                total = totalSum,
                                json = json,
                                shortFiskalId = response.data.short_document_id,
                                rrn = !string.IsNullOrWhiteSpace(response.data.rrn)
                                    ? response.data.rrn
                                    : (!string.IsNullOrWhiteSpace(salesData.Rrn) ? salesData.Rrn : null),
                                BankTransactionId = !string.IsNullOrWhiteSpace(response.data.transaction_id) ? response.data.transaction_id : null,
                                BankTransactionNumber = !string.IsNullOrWhiteSpace(response.data.transaction_number) ? response.data.transaction_number : null,
                                BankApprovalCode = !string.IsNullOrWhiteSpace(response.data.approval_code) ? response.data.approval_code : null,
                                customerId = salesData.Customer?.CustomerID,
                                doctorId = salesData.Doctor?.Id,
                            });

                            if (MessageVisible)
                            {
                                ReadyMessages.SUCCESS_SALES_MESSAGE();
                            }

                            FormHelpers.Log($"Pos satışı uğurla edildi. Qəbz No: {response.data.number}");
                            return true;
                        case "document: invalid shift duration":
                            XtraMessageBox.Show("GÜN SONU (Z) HESABATI ÇIXARILMAYIB !\n\nZəhmət olmasa pos bağla düyməsinə vuraraq günü sonlandırın.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        default:
                            ReadyMessages.ERROR_SALES_MESSAGE(response.message);
                            FormHelpers.Log($"Pos satışı xətası - Xəta mesajı: {response.message}");
                            return false;
                    }
                }
                else
                {
                    ReadyMessages.ERROR_SALES_MESSAGE("Kassa ilə əlaqə zamanı xəta yarandı");
                    return false;
                }
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_SALES_MESSAGE(ex.Message);
                return false;
            }
            finally
            {
                FormHelpers.OperationLog(new OperationLogs
                {
                    OperationType = OperationType.PosSales,
                    OperationId = posSaleId,
                    Message = posSaleId == 0 ? "Error" : "Success",
                    RequestCode = _requestJson,
                    ResponseCode = string.IsNullOrWhiteSpace(_responseJson) ? "Əlaqə zamanı xəta yarandı" : _responseJson
                });
            }
        }

        private static bool SaleBank(SalesDto salesData)
        {
            if (salesData.Card > 0 && !string.IsNullOrWhiteSpace(UserCacheService.Terminal.BankName))
            {
                BankRequest bank = new BankRequest()
                {
                    data = new BankRequest.Data()
                    {
                        documentUUID = salesData.DocumentUUID,
                        operationType = 0,
                        amount = salesData.Card
                    }
                };

                string Bankjson = Newtonsoft.Json.JsonConvert.SerializeObject(bank, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                RestRequest request = new RestRequest(salesData.IpAddress, Method.Post);
                request.AddHeader("Content-Type", "application/json;charset=utf-8");
                request.AddStringBody(Bankjson, DataFormat.Json);
                RestResponse Restresponse = _restClient.Execute(request);
                if (string.IsNullOrWhiteSpace(Restresponse?.Content))
                {
                    ReadyMessages.ERROR_SALES_MESSAGE("Terminal ilə əlaqə zamanı xəta yarandı");
                    return false;
                }

                var response = System.Text.Json.JsonSerializer.Deserialize<BankResponse>(Restresponse.Content);

                if (response != null)
                {
                    switch (response.message)
                    {
                        case "İcra olunur":
                            Task.Delay(3500);
                            return true;

                        default:
                            ReadyMessages.ERROR_SALES_MESSAGE(response.message);
                            FormHelpers.Log($"Bank satış xətası - Xəta mesajı: {response.message}");
                            return false;
                    }
                }
                else
                {
                    ReadyMessages.ERROR_SALES_MESSAGE("Terminal ilə əlaqə zamanı xəta yarandı");
                    return false;
                }
            }
            return false;
        }

        public static bool RefundBank(RefundDto refundData)
        {
            if (refundData.Card > 0 && !string.IsNullOrWhiteSpace(UserCacheService.Terminal.BankName))
            {
                BankRequest bank = new BankRequest()
                {
                    data = new BankRequest.Data()
                    {
                        documentUUID = refundData.DocumentUUID,
                        operationType = 1,
                        amount = refundData.Card,
                        rrn = refundData.Rrn,
                    }
                };

                string Bankjson = Newtonsoft.Json.JsonConvert.SerializeObject(bank, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                RestRequest request = new RestRequest(refundData.IpAddress, Method.Post);
                request.AddHeader("Content-Type", "application/json;charset=utf-8");
                request.AddStringBody(Bankjson, DataFormat.Json);
                RestResponse Restresponse = _restClient.Execute(request);
                if (string.IsNullOrWhiteSpace(Restresponse?.Content))
                {
                    ReadyMessages.ERROR_SALES_MESSAGE("Terminal ilə əlaqə zamanı xəta yarandı");
                    return false;
                }

                var response = System.Text.Json.JsonSerializer.Deserialize<BankResponse>(Restresponse.Content);

                if (response.message != "error" && response.code != "506")
                {
                    switch (response.message)
                    {
                        case "İcra olunur":
                            Task.Delay(3000);
                            return true;
                        //return BankCheckStatus(salesData.IpAddress, salesData.DocumentUUID);

                        default:
                            ReadyMessages.ERROR_SALES_MESSAGE(response.message);
                            FormHelpers.Log($"Bank qaytarma xətası - Xəta mesajı: {response.message}");
                            return false;
                    }
                }
                else
                {
                    ReadyMessages.ERROR_SALES_MESSAGE("Terminal ilə əlaqə zamanı xəta yarandı");
                    return false;
                }
            }
            return false;
        }

        public static BankResponse BankCheckStatus(string IpAdress, string uuid)
        {
            BankRequest bank = new BankRequest()
            {
                data = new BankRequest.Data()
                {
                    documentUUID = uuid
                },
                operation = "transactionTapXphoneCheck"
            };

            string Bankjson = Newtonsoft.Json.JsonConvert.SerializeObject(bank, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        start:
            RestRequest request = new RestRequest(IpAdress, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(Bankjson, DataFormat.Json);
            RestResponse Restresponse = _restClient.Execute(request);
            if (string.IsNullOrWhiteSpace(Restresponse?.Content))
            {
                ReadyMessages.ERROR_SALES_MESSAGE("Terminal ilə əlaqə zamanı xəta yarandı");
                return null;
            }

            var response = System.Text.Json.JsonSerializer.Deserialize<BankResponse>(Restresponse.Content);

            if (response != null)
            {
                switch (response.message)
                {
                    case "TƏSDİQLƏNDİ":
                    case "Success operation":
                        if (string.IsNullOrWhiteSpace(response?.data.rrn))
                            goto start;

                        return response;
                    default:
                        ReadyMessages.ERROR_SALES_MESSAGE(response.message);
                        FormHelpers.Log($"Bank xətası - Xəta mesajı: {response.message}");
                        return null;
                }
            }
            else
            {
                ReadyMessages.ERROR_SALES_MESSAGE("Terminal ilə əlaqə zamanı xəta yarandı");
                return null;
            }
        }

        public static bool Refund(RefundDto refundData)
        {
            string fiskallID = "";
            List<Item> items = new List<Item>();
            decimal cash = default;
            decimal card = default;
            string rrn = null;
            string transactionId = null;
            string transactionNumber = null;
            string approvalCode = null;
            string saleDate = null;
            string query = $@"SELECT 
  [pos_satis_check_main_id], 
  [pos_nomre], 
  [fiscal_id], 
  [date_], 
  [bankttnm], 
  [user_id_], 
  [emeliyyat_nomre], 
  [NEGD_], 
  [KART_], 
  [UMUMI_MEBLEG], 
  [json_], 
  [fiscalNum],
  [BankTransactionId],
  [BankTransactionNumber],
  [BankApprovalCode],
  [documentID] 
FROM 
  [pos_satis_check_main] WHERE[pos_satis_check_main_id] IN(
    SELECT[pos_satis_check_main_id] 
    FROM 
      [pos_gaytarma_manual] 
    where 
      [pos_gaytarma_manual_id] =(
        select 
          max([pos_gaytarma_manual_id]) 
        from 
          [pos_gaytarma_manual] WHERE user_id_ = {UserCacheService.User.Id}));";

            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        decimal cash1 = Convert.ToDecimal(dr["NEGD_"].ToString());
                        decimal card1 = Convert.ToDecimal(dr["KART_"].ToString());
                        string fiscal_id = dr["fiscal_id"].ToString();
                        string rrn1 = dr["bankttnm"].ToString();
                        string bankTransactionId = dr["BankTransactionId"].ToString();
                        string bankTransactionNumber = dr["BankTransactionNumber"].ToString();
                        string bankApprovalCode = dr["BankApprovalCode"].ToString();
                        var tarix = Convert.ToDateTime(dr["date_"].ToString());


                        fiskallID = fiscal_id;
                        cash = cash1;
                        card = card1;
                        rrn = rrn1;
                        transactionId = bankTransactionId;
                        transactionNumber = bankTransactionNumber;
                        approvalCode = bankApprovalCode;
                        saleDate = tarix.ToString("dd.MM.yyyy");
                    }
                    refundData.Rrn = string.IsNullOrWhiteSpace(rrn) ? refundData.Rrn : rrn;
                    refundData.BankTransactionId = transactionId;
                    refundData.BankTransactionNumber = transactionNumber;
                    refundData.BankApprovalCode = approvalCode;
                }

            }

            string query2 = $@"(SELECT md.MEHSUL_ADI AS name,
                       p.item_id AS code,
                       pl.say AS say,
                       p.satis_giymet AS satis_giymet,
					    pl.say * p.satis_giymet as tutar,
                       p.quantity_type AS quantity_type,
                       md.VERGI_DERECESI AS vtypes
              FROM pos_satis_check_details p
                       INNER JOIN MAL_ALISI_DETAILS md ON p.mal_alisi_details_id = md.MAL_ALISI_DETAILS_ID
                       INNER JOIN pos_gaytarma_manual pl ON p.pos_satis_check_details_id = pl.pos_satis_check_details
              WHERE pl.emeliyyat_nomre = '{refundData.ProccessNo}')";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query2, con))
            {
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string name = dr["name"].ToString();
                        string code = dr["code"].ToString();
                        decimal salePrice = Convert.ToDecimal(dr["satis_giymet"]);
                        decimal quantity = Convert.ToDecimal(dr["say"]);
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
                }

            }

            Data data = new Data
            {
                parentDocumentId = fiskallID,
                documentUUID = UUIDGenerateService.UUID,
                cashPayment = cash,
                cardPayment = card,
                items = items,
                moneyBackType = 0,
                cashierName = refundData.Cashier,
                rrn = refundData.Rrn,
                isManual = true
            };

            if (card > 0 && UserCacheService.Terminal?.BankName == "PAX A35")
            {
                data.sum = card + cash;
                data.isSendCardPayment = true;
                data.rrn = refundData.Rrn;
                data.transactionId = refundData.BankTransactionId;
                data.transactionNumber = refundData.BankTransactionNumber;
                data.approvalCode = refundData.BankApprovalCode;
            }


            if (refundData.PayType is PayType.Card &&
                !string.IsNullOrWhiteSpace(UserCacheService.Terminal?.BankName) &&
                UserCacheService.Terminal?.BankName != "PAX A35")

            {
                string today = DateTime.Now.ToString("dd.MM.yyyy");

                var responseBank = RefundBank(new RefundDto
                {
                    IpAddress = refundData.IpAddress,
                    Card = card,
                    Rrn = saleDate == today ? refundData.BankTransactionId : refundData.Rrn,
                    DocumentUUID = refundData.DocumentUUID,
                });

                Sunmi.BankResponse responseCheck = new Sunmi.BankResponse();
                if (responseBank)
                {
                    responseCheck = Sunmi.BankCheckStatus(refundData.IpAddress, refundData.DocumentUUID);
                }

                if (responseCheck is null || string.IsNullOrWhiteSpace(responseCheck?.data?.rrn))
                {
                    // Bank uğursuzdursa, satış dayansın
                    return false;
                }

                rrn = responseCheck.data.rrn;
                //data.isSendCardPayment = true;
                data.rrn = refundData.Rrn;
            }


            RootObject rootObject = new RootObject
            {
                data = data,
                operation = "moneyBack",
            };

            string json = JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(refundData.IpAddress, json);

            try
            {
                if (response.message != "error" && response.code != "506")
                {
                    if (response.message == "Success operation" || response.message == "Successful operation")
                    {
                        if (MessageVisible)
                        {
                            ReadyMessages.SUCCESS_RETURN_SALES_MESSAGE();
                        }

                        FormHelpers.Log($"Qəbz geri qaytarması edildi. Qəbz №: {response.data.number}");
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
                    return false;
            }
            catch (Exception ex)
            {
                FormHelpers.Log($"Pos satış qaytarma xətası. Xəta mesajı: {ex.Message}");
                ReadyMessages.ERROR_RETURN_SALES_MESSAGE(ex.Message);
                return false;
            }
            finally
            {
                FormHelpers.OperationLog(new OperationLogs
                {
                    OperationType = OperationType.RefundPosSales,
                    OperationId = (int)OperationType.RefundPosSales,
                    RequestCode = response.requestJson,
                    ResponseCode = response.responseJson,
                });
            }


            return false;
        }

        public static bool CloseShiftBank(string IpAdress)
        {
            BankRequest bank = new BankRequest()
            {
                data = null,
                operation = "transactionTapXphoneCloseDay"
            };

            string Bankjson = Newtonsoft.Json.JsonConvert.SerializeObject(bank, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            RestRequest request = new RestRequest(IpAdress, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(Bankjson, DataFormat.Json);
            RestResponse Restresponse = _restClient.Execute(request);
            if (string.IsNullOrWhiteSpace(Restresponse?.Content))
            {
                ReadyMessages.ERROR_SALES_MESSAGE("Terminal ilə əlaqə zamanı xəta yarandı");
                return false;
            }

            var response = System.Text.Json.JsonSerializer.Deserialize<BankResponse>(Restresponse.Content);

            if (response.message != "error" && response.code != "506")
            {
                switch (response.message)
                {

                    case "Success operation":
                    case "Uğurlu":
                        return true;
                    default:
                        ReadyMessages.ERROR_SALES_MESSAGE(response.message);
                        FormHelpers.Log($"Bank Z hesabat xətası - Xəta mesajı: {response.message}");
                        return false;
                }
            }
            else
            {
                ReadyMessages.ERROR_SALES_MESSAGE("Terminal ilə əlaqə zamanı xəta yarandı");
                return false;
            }
        }

        public static bool CreditPay(CreditPayDto creditData)
        {
            CreditPayRequest.Item item = new CreditPayRequest.Item()
            {
                name = creditData.item.Name,
                code = creditData.item.Code,
                quantity = creditData.item.Quantity,
                salePrice = creditData.Total,
                realPrice = creditData.item.SalePrice,
                vatType = creditData.item.VatType,
                quantityType = creditData.item.quantityType
            };

            CreditPayRequest.Data data = new CreditPayRequest.Data()
            {
                documentUUID = creditData.documentUUID,
                parentDocumentId = creditData.ParenDocumentId,
                cashPayment = creditData.IncomingSum,
                cardPayment = creditData.CardPayment,
                cashierName = creditData.CashierName,
                residue = creditData.Residue,
                paymentNumber = creditData.paymentNumber.ToString(),
                creditContract = creditData.CreditContract,
                creditPayer = creditData.CustomerName,
                clientName = creditData.CustomerName,
                items = new List<CreditPayRequest.Item> { item }
            };


            CreditPayRequest root = new CreditPayRequest
            {
                data = data
            };


            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            RestClient rest = new RestClient();
            RestRequest request = new RestRequest(creditData.Url, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(json, DataFormat.Json);
            RestResponse response = rest.Execute(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE();
                FormHelpers.Log($"Kassa ilə əlaqə zamanı xəta yarandı\n\n {response.ErrorMessage}");
                return false;
            }
            else
            {
                CreditPayResponse payResponse = System.Text.Json.JsonSerializer.Deserialize<CreditPayResponse>(response.Content);

                if (payResponse.message != "Successful operation" && payResponse.message != "Success operation")
                {
                    ReadyMessages.ERROR_DEFAULT_MESSAGE(payResponse.message);
                    FormHelpers.Log($"Xəta mesajı: {payResponse.message}");
                    return false;
                }
                else
                {
                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCES_CREDIT_PAYMENT_MESSAGE();
                    }

                    short paymentType = (short)(creditData.IncomingSum > 0 ? 1 : 2);
                    DbProsedures.UPDATE_CreditPay(payResponse.data.short_document_id,
                        payResponse.data.document_id,
                        payResponse.data.document_number.ToString(),
                        paymentType,
                        creditData.CreditMonthId);

                    FormHelpers.Log($"{data.creditContract} nömrəli kredit müqaviləsinin ödənişi edildi. Qəbz No: {payResponse.data.document_number}");

                    return true;
                }
            }
        }

        public static Tuple<bool, string, string, string> CreditSale(CreditSaleDto dataDto)
        {
            List<CreditSaleRequest.Item> items = new List<CreditSaleRequest.Item>();
            CreditSaleRequest.Item item = new CreditSaleRequest.Item()
            {
                name = dataDto.item.ProductName,
                code = dataDto.item.ProductCode,
                quantity = dataDto.item.Quantity,
                quantityType = dataDto.item.QuantityType,
                vatType = dataDto.item.VatType,
                salePrice = dataDto.item.SalePrice
            };
            items.Add(item);

            CreditSaleRequest.Data data = new CreditSaleRequest.Data()
            {
                cashPayment = dataDto.CashPayment,
                cardPayment = dataDto.CardPayment,
                cashierName = dataDto.Cashier,
                creditPayer = dataDto.CustomerName,
                creditContract = dataDto.CreditContract,
                creditPayment = dataDto.creditPayment,
                documentUUID = dataDto.DocumentUUID,
                note = dataDto.Note,
                items = items,
            };

            CreditSaleRequest request = new CreditSaleRequest()
            {
                data = data
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(dataDto.Url, json);

            if (response.message != "error" && response.code != "506")
            {

                switch (response.message)
                {
                    case "Success operation":
                    case "Successful operation":
                        if (MessageVisible)
                        {
                            ReadyMessages.SUCCESS_CREDIT_SALES_MESSAGE();
                        }
                        FormHelpers.Log($"Kredit satışı uğurla edildi. Qəbz No: {response.data.number}");
                        return new Tuple<bool, string, string, string>(true,
                            response.data.document_id,
                            response.data.short_document_id,
                            response.data.number);
                    case "document: invalid shift duration":
                        XtraMessageBox.Show("GÜN SONU (Z) HESABATI ÇIXARILMAYIB !\n\nPos Satış səhifəsindən daxil olaraq günü sonlandırın", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return new Tuple<bool, string, string, string>(false, null, null, null);
                    default:
                        ReadyMessages.ERROR_CREDIT_SALES_MESSAGE(response.message);
                        FormHelpers.Log($"Kredit satışı xətası - Xəta mesajı: {response.message}");
                        FormHelpers.OperationLog(new OperationLogs()
                        {
                            Message = response?.message,
                            RequestCode = response?.requestJson,
                            ResponseCode = response?.responseJson,
                            OperationType = OperationType.CreditSale,
                            OperationId = 506
                        });
                        return new Tuple<bool, string, string, string>(false, null, null, null);
                }
            }
            else
            {
                ReadyMessages.ERROR_CREDIT_SALES_MESSAGE("Kassa ilə əlaqə zamanı xəta yarandı");
                return new Tuple<bool, string, string, string>(false, null, null, null);
            }
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

        public static bool Prepayment(SalesDto salesData)
        {
            List<PrepaymentRequest.Item> items = new List<PrepaymentRequest.Item>();
            int _vatType = 0;
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
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

            var response = RequestPOST(salesData.IpAddress, json);

            if (response != null)
            {
                if (response.message is "Success operation" || response.message is "Successful operation")
                {
                    DbProsedures.InsertPosSales(new PosSales
                    {
                        posNomre = response.data.number,
                        longFiskalId = response.data.document_id,
                        proccessNo = salesData.ProccessNo,
                        total = salesData.Total,
                        Prepayment = salesData.PrepaymentPay,
                        cash = salesData.Cash,
                        card = salesData.Card,
                        json = json,
                        shortFiskalId = response.data.short_document_id,
                        rrn = response.data.rrn,
                        customerId = salesData.Customer?.CustomerID,
                        doctorId = salesData.Doctor?.Id,
                    });

                    if (salesData.Customer != null)
                    {
                        decimal debt = salesData.Total - salesData.PrepaymentPay; //Qalan borcu
                        DbProsedures.InsertCustomerDebt(CustomerDebtType.AvansPay, DateTime.Now, salesData.Customer.CustomerID, debt);
                    }


                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_SALES_MESSAGE();
                    }

                    FormHelpers.Log($"Avans ödənişi uğurla edildi. Qəbz No: {response.data.number}");
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
                    FormHelpers.Log($"Avans ödənişi xətası - Xəta mesajı: {response.message}");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool PrepaymentSale(SalesDto salesData, decimal pos_satis_main_id)
        {
            List<PrepaymentSaleRequest.Item> items = new List<PrepaymentSaleRequest.Item>();
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                con.Open();
                string query = $@"select 
       mad.MEHSUL_ADI as productName,
       mad.BARKOD as barcode,
	   mad.VERGI_DERECESI as taxType,
       psd.count_,
       psd.quantity_type as unitType,
       psd.satis_giymet as salePrice,
       psd.satis_giymet * psd.count_ as total
from [pos_satis_check_details] psd
inner join MAL_ALISI_DETAILS mad ON mad.MAL_ALISI_DETAILS_ID = psd.mal_alisi_details_id
inner join MAL_ALISI_MAIN man on man.MAL_ALISI_MAIN_ID = mad.MAL_ALISI_MAIN_ID
inner join pos_satis_check_main psm ON psm.pos_satis_check_main_id = {pos_satis_main_id}
WHERE psd.pos_satis_check_main_id = {pos_satis_main_id} AND psm.user_id_ = {Properties.Settings.Default.UserID}";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string name = dr["productName"].ToString();
                            string code = dr["barcode"].ToString();
                            decimal salePrice = Convert.ToDecimal(dr["salePrice"]);
                            double quantity = Convert.ToDouble(dr["count_"]);
                            int vatType = Convert.ToInt32(dr["taxType"]);
                            int quantityType = Convert.ToInt32(dr["unitType"]);
                            PrepaymentSaleRequest.Item itemProduct = new PrepaymentSaleRequest.Item
                            {
                                name = name,
                                code = code,
                                salePrice = salePrice,
                                quantity = quantity,
                                vatType = vatType,
                                codeType = 1,
                                quantityType = quantityType
                            };
                            items.Add(itemProduct);
                        }
                    }
                }
            }

            PrepaymentSaleRequest.Data data = new PrepaymentSaleRequest.Data
            {
                documentUUID = Guid.NewGuid().ToString(),
                prepaymentDocumentId = salesData.FiscalId,
                cashPayment = salesData.Cash,
                cardPayment = salesData.Card,
                depositPayment = salesData.PrepaymentPay,
                bonusPayment = 0,
                items = items,
                cashierName = salesData.Cashier,
                clientName = salesData.CustomerNameManual,
                rrn = salesData.Rrn,
                moneyBackType = null
            };

            PrepaymentSaleRequest.Root rootObject = new PrepaymentSaleRequest.Root
            {
                data = data,
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(salesData.IpAddress, json);

            if (response != null)
            {
                if (response.message is "Success operation" || response.message is "Successful operation")
                {
                    using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
                    {
                        string query = $"UPDATE [dbo].[pos_satis_check_main] SET NEGD_={salesData.Cash.ToString("N2").Replace(',', '.')}+NEGD_,KART_={salesData.Card.ToString("N2").Replace(',', '.')}+KART_, [PREdate_]=getdate(),PREfiscal_id='{response.data.short_document_id}' where pos_satis_check_main_id={pos_satis_main_id}";
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

                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_ADVANCE_SALES_MESSAGE();
                    }

                    FormHelpers.Log($"Avans satışı uğurla edildi. Qəbz No: {response.data.number}");
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
                    FormHelpers.Log($"Avans satışı xətası - Xəta mesajı: {response.message}");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static Tuple<bool, string, string> CreditRefund(CreditSaleRefundDto refundDto)
        {
            int vatType = 18;
            switch (refundDto.item.VatType)
            {
                case 1:
                case 2:
                    vatType = 18;
                    break;
                case 3: vatType = 0; break;
                case 4: vatType = 2; break;
                case 6: vatType = 2; break;
                case 5: vatType = 8; break;
            }


            var items = new List<CreditRefundRequest.Item>
            {
                new CreditRefundRequest.Item()
                {
                    name = refundDto.item.ProductName,
                    code = refundDto.item.ProductCode,
                    quantityType = refundDto.item.QuantityType,
                    quantity = refundDto.item.Quantity,
                    salePrice = refundDto.item.SalePrice,
                    vatType = vatType,
                }
            };


            CreditRefundRequest.Data data = new CreditRefundRequest.Data
            {
                documentUUID = Guid.NewGuid().ToString(),
                parentDocumentId = refundDto.ParentLongFiscalId,
                cashPayment = refundDto.IncomingSum,
                cardPayment = refundDto.CardPayment,
                items = items,
                moneyBackType = 0,
                cashierName = refundDto.Cashier,
                rrn = refundDto.Rrn,
                clientName = refundDto.CustomerName
            };

            CreditRefundRequest.Root rootObject = new CreditRefundRequest.Root
            {
                data = data,
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            Clipboard.SetText(json);
            return new Tuple<bool, string, string>(false, null, null);
            var response = RequestPOST(refundDto.Url, json);

            if (response != null)
            {
                if (response.message == "Successful operation")
                {
                    if (MessageVisible)
                        ReadyMessages.SUCCESS_RETURN_SALES_MESSAGE();

                    FormHelpers.Log($"Kredit satışı uğurla geri qaytarıldı. Qəbz No: {response.data.document_number.ToString()}");
                    return new Tuple<bool, string, string>(true, response.data.document_id, response.data.document_number.ToString());
                }
                else if (response.message == "document: invalid shift duration")
                {
                    XtraMessageBox.Show("GÜN SONU (Z) HESABATI ÇIXARILMAYIB !\n\nZəhmət olmasa pos bağla düyməsinə vuraraq günü sonlandırın.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return new Tuple<bool, string, string>(false, null, null);
                }
                else if (response.message == "document: invalid shift status")
                {
                    XtraMessageBox.Show("NÖVBƏ AÇILMAYIB !\n\nZəhmət olmasa pos aç düyməsinə vuraraq növbəni açın.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return new Tuple<bool, string, string>(false, null, null);
                }
                else
                {
                    ReadyMessages.ERROR_SALES_MESSAGE(response.message);
                    FormHelpers.Log($"Kredit geri qaytarma xətası - Xəta mesajı: {response.message}");
                    return new Tuple<bool, string, string>(false, null, null);
                }
            }
            else
            {
                return new Tuple<bool, string, string>(false, null, null);
            }
        }


        #region [..REQUEST CLASS..]

        public class Item
        {
            public string name { get; set; }
            public string code { get; set; }
            public decimal quantity { get; set; }
            public decimal salePrice { get; set; }
            public double? realPrice { get; set; } = null;
            public decimal? purchasePrice { get; set; } = null;
            public int? codeType { get; set; } = null;
            public int quantityType { get; set; }
            public int vatType { get; set; }
            public decimal? discountAmount { get; set; } = null;
        }

        public class Data
        {
            public bool? isSendCardPayment { get; set; } = null;
            public decimal? sum { get; set; } = null;
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
            public string transactionId { get; set; } = null;
            public string transactionNumber { get; set; } = null;
            public string approvalCode { get; set; } = null;
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

        private class CloseShiftRequest
        {
            public class Data
            {
                public string documentUUID { get; set; }
                public string cashierName { get; set; }
            }
            public Data data { get; set; }
            public string operation { get; set; } = "closeShift";
            public string username { get; set; } = "username";
            public string password { get; set; } = "password";
        }

        public class PrepaymentRequest
        {
            public class Data
            {
                public string documentUUID { get; set; } = null;
                public decimal? sum { get; set; } = null;
                public int vatType { get; set; }
                public decimal? cashPayment { get; set; } = null;
                public decimal? creditPayment { get; set; } = null;
                public decimal? depositPayment { get; set; } = null;
                public decimal? cardPayment { get; set; } = null;
                public decimal? bonusPayment { get; set; } = null;
                public List<Item> items { get; set; }
                public string clientName { get; set; } = null;
                public decimal? clientTotalBonus { get; set; } = null;
                public decimal? clientEarnedBonus { get; set; } = null;
                public string clientBonusCardNumber { get; set; } = null;
                public string cashierName { get; set; }
                public int? moneyBackType { get; set; } = null;
                public string rrn { get; set; } = null;
                public string currency { get; set; } = "AZN";
                public string note { get; set; } = null;
            }

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
            }

            public class Root
            {
                public Data data { get; set; }
                public string operation { get; set; } = "prepaymentProducts";
                public string username { get; set; } = "username";
                public string password { get; set; } = "password";
            }
        }

        public class PrepaymentSaleRequest
        {
            public class Data
            {
                public string documentUUID { get; set; }
                public string prepaymentDocumentId { get; set; }
                public decimal? cashPayment { get; set; } = null;
                public decimal? depositPayment { get; set; } = null;
                public decimal? cardPayment { get; set; } = null;
                public decimal? bonusPayment { get; set; } = null;
                public List<Item> items { get; set; }
                public string clientName { get; set; } = null;
                public decimal? clientTotalBonus { get; set; } = null;
                public decimal? clientEarnedBonus { get; set; } = null;
                public string clientBonusCardNumber { get; set; } = null;
                public string cashierName { get; set; }
                public int? moneyBackType { get; set; } = null;
                public string rrn { get; set; } = null;
                public string currency { get; set; } = "AZN";
                public string note { get; set; } = null;
            }

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
                public decimal? discountAmount { get; set; } = null;
            }

            public class Root
            {
                public Data data { get; set; }
                public string operation { get; set; } = "sale";
                public string username { get; set; } = "username";
                public string password { get; set; } = "password";
            }
        }

        public class DepositRequest
        {
            public class Data
            {
                public string documentUUID { get; set; } = Guid.NewGuid().ToString();
                public decimal sum { get; set; }
                public string cashierName { get; set; }
                public string currency { get; set; } = "AZN";
            }

            public class Root
            {
                public Data data { get; set; }
                public string operation { get; set; } = "deposit";
                public string username { get; set; } = "username";
                public string password { get; set; } = "password";
            }
        }

        public class WithdrawRequest
        {
            public class Data
            {
                public string documentUUID { get; set; } = Guid.NewGuid().ToString();
                public decimal sum { get; set; }
                public string cashierName { get; set; }
                public string currency { get; set; } = "AZN";
            }

            public class Root
            {
                public Data data { get; set; }
                public string operation { get; set; } = "deposit";
                public string username { get; set; } = "username";
                public string password { get; set; } = "password";
            }
        }

        public class CreditSaleRequest
        {
            public class Item
            {
                public string name { get; set; }
                public string code { get; set; }
                public decimal quantity { get; set; }
                public decimal salePrice { get; set; }
                public double? realPrice { get; set; } = null;
                public decimal? purchasePrice { get; set; } = null;
                public int? codeType { get; set; } = null;
                public int quantityType { get; set; }
                public int vatType { get; set; }
                public decimal? discountAmount { get; set; } = null;
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

            public Data data { get; set; }
            public string operation { get; set; } = "sale";
        }

        public class CreditPayRequest
        {
            public class Item
            {
                public string name { get; set; }
                public string code { get; set; }
                public decimal quantity { get; set; }
                public decimal salePrice { get; set; }
                public decimal? realPrice { get; set; } = null;
                public decimal? purchasePrice { get; set; } = null;
                public int? codeType { get; set; } = null;
                public int quantityType { get; set; }
                public int vatType { get; set; }
                public decimal? discountAmount { get; set; } = null;
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
                public decimal? residue { get; set; } = null;
                public string creditContract { get; set; } = null;
                public string paymentNumber { get; set; } = null;
                public string note { get; set; } = null;
            }

            public Data data { get; set; }
            public string operation { get; set; } = "credit";
        }

        public class CreditRefundRequest
        {
            public class Data
            {
                public string parentDocumentId { get; set; }
                public string documentUUID { get; set; }
                public decimal cashPayment { get; set; }
                public decimal creditPayment { get; set; }
                public decimal depositPayment { get; set; }
                public decimal cardPayment { get; set; }
                public decimal bonusPayment { get; set; }
                public List<Item> items { get; set; }
                public bool isManual { get; set; } = true;
                public int moneyBackType { get; set; }
                public string clientName { get; set; }
                public decimal clientTotalBonus { get; set; }
                public int clientEarnedBonus { get; set; }
                public string clientBonusCardNumber { get; set; }
                public string cashierName { get; set; }
                public string currency { get; set; } = "AZN";
                public string rrn { get; set; }
                public string note { get; set; }
            }

            public class Item
            {
                public string name { get; set; }
                public string code { get; set; }
                public decimal quantity { get; set; }
                public decimal salePrice { get; set; }
                public double? realPrice { get; set; } = null;
                public decimal? purchasePrice { get; set; } = null;
                public int? codeType { get; set; } = null;
                public int quantityType { get; set; }
                public int vatType { get; set; }
                public decimal? discountAmount { get; set; } = null;
            }

            public class Root
            {
                public Data data { get; set; }
                public string operation { get; set; } = "moneyBack";
                public string username { get; set; } = "username";
                public string password { get; set; } = "password";
            }
        }

        private class BankRequest
        {
            public class Data
            {
                public string documentUUID { get; set; }
                public int? operationType { get; set; } = null;
                public decimal? amount { get; set; } = null;
                public string rrn { get; set; } = null;
            }

            public Data data { get; set; }
            public string operation { get; set; } = "transactionTapXPhone";
            public int version { get; set; } = 1;
        }

        #endregion [..REQUEST CLASS..]


        #region [..RESPONSE CLASS..]

        public abstract class BaseResponse
        {
            public string requestJson { get; set; }
            public string responseJson { get; set; }
            public string code { get; set; }
            public string message { get; set; }
        }

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
            public string transaction_id { get; set; }
            public string transaction_number { get; set; }
            public string approval_code { get; set; }
        }

        public class ResponseData : BaseResponse
        {
            public ResponseDocumentData data { get; set; }
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

        public class DepositResponse
        {
            public class Data
            {
                public string document_id { get; set; }
                public string document_number { get; set; }
                public string shift_document_number { get; set; }
                public string short_document_id { get; set; }
                public decimal totalSum { get; set; }
            }

            public class Root : BaseResponse
            {
                public Data data { get; set; }
            }
        }

        public class WithdrawResponse
        {
            public class Data
            {
                public string document_id { get; set; }
                public string document_number { get; set; }
                public string shift_document_number { get; set; }
                public string short_document_id { get; set; }
                public decimal totalSum { get; set; }
            }

            public class Root : BaseResponse
            {
                public Data data { get; set; }
            }
        }

        public class CreditPayResponse : BaseResponse
        {
            public class Data
            {
                public string approval_code { get; set; }
                public string document_id { get; set; }
                public int document_number { get; set; }
                public string number { get; set; }
                public string rrn { get; set; }
                public int shift_document_number { get; set; }
                public string short_document_id { get; set; }
                public double totalSum { get; set; }
                public string transaction_id { get; set; }
                public string transaction_number { get; set; }
            }
            public Data data { get; set; }
        }

        public class BankResponse
        {
            public class Data
            {
                public string aid { get; set; }
                public string amt { get; set; }
                public string applbl { get; set; }
                public string auth_code { get; set; }
                public string bank_owner { get; set; }
                public string batch { get; set; }
                public string card_mask { get; set; }
                public string cur_code { get; set; }
                public string date_time { get; set; }
                public string host_resp_code { get; set; }
                public string intentResult { get; set; }
                public string pmt_dest { get; set; }
                public string pmt_name { get; set; }
                public string pmt_terminal { get; set; }
                public string rrn { get; set; }
                public string stan { get; set; }
                public string status { get; set; }
                public string terminal { get; set; }
                public string trxid { get; set; }
                public string tvr { get; set; }
                public string unp { get; set; }
            }
            public Data data { get; set; }
            public string code { get; set; }
            public string message { get; set; }
            public bool? IsSuccess { get; set; } = false;
        }

        #endregion [..RESPONSE CLASS..]
    }
}