using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;
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
        public const string FiskalPort = "8008"; //prod port: 8008 - test port: 10155
        private static readonly RestClient _restClient = new RestClient();

        private static readonly bool MessageVisible = FormHelpers.SuccessMessageVisible();
        private static BaseRequestResponse<T> RequestPOST<T>(string ipAddress, string merchantID, string json)
        {
            string data = JsonConvertBase64(json, merchantID);

            var request = new RestRequest(ipAddress, Method.Post);
            request.AddParameter("text/plain", data, ParameterType.RequestBody);
            RestResponse response = _restClient.Execute(request);
            if (string.IsNullOrWhiteSpace(response?.Content))
            {
                return new BaseRequestResponse<T>
                {
                    code = 506,
                    message = response.ErrorMessage,
                    requestJson = json
                };
            }

            var responseData = System.Text.Json.JsonSerializer.Deserialize<T>(response.Content);
            return new BaseRequestResponse<T>
            {
                requestJson = json,
                responseJson = response.Content,
                data = responseData
            };
        }

        public static void OpenShift(string ipAdress, string merchantId, string cashier)
        {
            var root = new
            {
                employeeName = cashier
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST<BaseResponse>($"{ipAdress}/open_shift", merchantId, json);

            if (response.code != 506)
            {
                if (response.data.status is "success")
                {
                    ReadyMessages.SUCCESS_OPEN_SHIFT_MESSAGE();
                    FormHelpers.Log(CommonData.SUCCESS_OPEN_SHIFT);
                }
                else
                {
                    ReadyMessages.ERROR_OPENSHIFT_MESSAGE(response.data.message);
                    FormHelpers.Log($"{ErrorMessages.ERROR_OPENSHIFT} Xəta mesajı: {response.data.message}");
                }
            }
            else
            {
                ReadyMessages.ERROR_OPENSHIFT_MESSAGE($"Kassa ilə əlaqə zamanı xəta yarandı\n{response.message}");
            }
        }

        public static void GetShiftStatus(string ipAdress, string merchantId, string cashier)
        {
            var root = new
            {
                employeeName = cashier,
            };


            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST<ResponseCheckShift>($"{ipAdress}/check_shift", merchantId, json);
            if (response.code != 506)
            {
                if (response.data.isShiftOpen is "true")
                {
                    string open_time = Convert.ToDateTime(response.data.shiftOpenAt).ToString("dd.MM.yyyy HH:mm:ss");
                    ReadyMessages.SUCCESS_SHIFT_STATUS_MESSAGE(open_time);
                }
                else
                {
                    OpenShift(ipAdress, merchantId, cashier);
                }
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE($"Kassa ilə əlaqə zamanı xəta yarandı\n{response.message}");
            }
        }

        public static void CloseShift(string ipAdress, string merchantId, string cashier)
        {
            var root = new
            {
                employeeName = cashier
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST<BaseResponse>($"{ipAdress}/close_shift", merchantId, json);
            if (response.code != 506)
            {
                if (response.data.status is "success")
                {
                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_CLOSE_SHIFT_MESSAGE();
                    }

                    FormHelpers.Log(CommonData.SUCCESS_CLOSE_SHIFT);
                }
                else
                {
                    ReadyMessages.WARNING_DEFAULT_MESSAGE(response.data.message);
                    FormHelpers.OperationLog(new OperationLogs
                    {
                        Message = "Z REPORT ERROR",
                        OperationId = 0,
                        OperationType = OperationType.ZReport,
                        RequestCode = response.requestJson,
                        ResponseCode = response.responseJson
                    });
                    FormHelpers.Log($"Z REPORT ERROR - Xəta mesajı: {response.data.message}");
                }
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE($"Kassa ilə əlaqə zamanı xəta yarandı\n{response.message}");
            }
        }

        public static async Task<bool> Sales(DTOs.SalesDto salesData)
        {
            string requestJson = null;
            string responseJson = null;
            int posSalesId = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                List<RequestSale.Item> items = new List<RequestSale.Item>();
                string query = "GetItems_AzSmart";

                SqlConnection conn = new SqlConnection(DbHelpers.DbConnectionString);
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", Properties.Settings.Default.UserID);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr["name"].ToString();
                    string Id = dr["item_id"].ToString();
                    string barcode = dr["code"].ToString();
                    decimal salePrice = Convert.ToDecimal(dr["salePrice"]);
                    decimal _purchasePrice = Convert.ToDecimal(dr["purchasePrice"]);
                    decimal quantity = Convert.ToDecimal(dr["quantity"]);
                    string taxName = dr["TaxName"].ToString();
                    int TaxCode = Convert.ToInt32(dr["TaxCode"]);
                    int calcType = Convert.ToInt32(dr["calcType"]);
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

                    var taxs = new List<RequestSale.itemTaxes>
                    {
                        new RequestSale.itemTaxes
                        {
                            fullName = taxName,
                            taxName = taxName,
                            taxPrc = TaxPrc,
                            calcType = calcType,
                            //taxCode = 0
                        }
                    };

                    RequestSale.Item itemProduct = new RequestSale.Item
                    {
                        itemId = Id,
                        itemName = name,
                        itemBarcode = barcode,
                        itemQty = miqdar,
                        itemAmount = price,
                        itemMarginPrice = purchasePrice,
                        itemMarginSum = purchasePriceSum,
                        itemTaxes = taxs
                    };
                    items.Add(itemProduct);
                }

                int cash = Convert.ToInt32(salesData.IncomingSum * 100);
                int card = Convert.ToInt32(salesData.Card * 100);
                int total = Convert.ToInt32(salesData.Total * 100);

                RequestSale.Payments payments = new RequestSale.Payments
                {
                    cashAmount = cash,
                    cashlessAmount = card,
                    rrn = string.IsNullOrWhiteSpace(salesData.Rrn) ? null : salesData.Rrn,
                };

                string docnumber = await ReturnHeaderId();

                RequestSale.Root root = new RequestSale.Root
                {
                    employeeName = salesData.Cashier,
                    items = items,
                    payments = payments,
                    amount = total,
                    docNumber = docnumber
                };

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });


                var response = RequestPOST<ResponseSale>($"{salesData.IpAddress}/sale", salesData.MerchantId, json);
                requestJson = response.requestJson;
                responseJson = response.responseJson;

                if (response.code != 506)
                {
                    if (response.data.status == "success")
                    {
                        posSalesId = DbProsedures.InsertPosSales(new PosSales
                        {
                            posNomre = response.data.fiscalNum,
                            longFiskalId = response.data.fiscalID,
                            proccessNo = salesData.ProccessNo,
                            cash = cash,
                            card = card,
                            total = total,
                            json = json,
                            shortFiskalId = null,
                            rrn = response.data.rrn,
                        });

                        if (MessageVisible)
                        {
                            ReadyMessages.SUCCESS_SALES_MESSAGE();
                        }

                        FormHelpers.Log($"Pos satışı uğurla edildi. Qəbz №: {response.data.fiscalNum}");
                        return true;
                    }
                    else
                    {
                        ReadyMessages.ERROR_SALES_MESSAGE(response.data.message);
                        FormHelpers.Log($"Pos satışı xətası - Xəta mesajı: {response.data.message}");
                        return false;
                    }
                }
                else
                {
                    ReadyMessages.ERROR_SALES_MESSAGE($"Kassa ilə əlaqə zamanı xəta yarandı\n{response.message}");
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
                    OperationId = posSalesId,
                    Message = posSalesId == 0 ? "Error" : "Success",
                    RequestCode = requestJson,
                    ResponseCode = responseJson,
                });
                Cursor.Current = Cursors.Default;
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

            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
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
                connection.Open();
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


            var response = RequestPOST<ResponseRefund>($"{ipAddress}/refund", merchantID, json);

            if (response.code != 506)
            {
                if (response.data.status == "success")
                {
                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_RETURN_SALES_MESSAGE();
                    }

                    FormHelpers.Log($"Qəbz geri qaytarması edildi Qəbz №: {response.data.fiscalNum}");
                    return true;
                }
                else
                {
                    FormHelpers.Log($"Pos satış qaytarma xətası. Xəta mesajı: {response.data.message}");
                    ReadyMessages.ERROR_RETURN_SALES_MESSAGE(response.data.message);
                    return false;
                }
            }
            else
            {
                ReadyMessages.ERROR_RETURN_SALES_MESSAGE($"Kassa ilə əlaqə zamanı xəta yarandı\n{response.message}");
                return false;
            }
        }

        public static void LastReceiptCopy(string ipAddress, string merchantId, string cashier)
        {
            string fiskalID = string.Empty;
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(DbHelpers.LastDocumentFiskalId, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            fiskalID = dr[0].ToString();
                        }
                    }
                }
            }

            var root = new
            {
                documentID = fiskalID
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST<BaseResponse>($"{ipAddress}/check_copy", merchantId, json);

            if (response.code != 506)
            {
                if (response.data.status is "success")
                {
                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_LAST_DOCUMENT_MESSAGE();
                    }
                    FormHelpers.Log(CommonData.SUCCESS_LAST_DOCUMENT);
                }
                else
                {
                    ReadyMessages.ERROR_LAST_DOCUMENT_MESSAGE(response.data.message);
                    FormHelpers.Log($"Təkrar qəbz çap olunarkən xəta yarandı. Xəta mesajı: {response.data.message}");
                }
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE($"Kassa ilə əlaqə zamanı xəta yarandı\n{response.message}");
            }
        }

        public static async Task<bool> InstallmentSales(DTOs.SalesDto salesData)
        {
            string requestJson = null;
            string responseJson = null;
            int posSalesId = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                List<RequestSale.Item> items = new List<RequestSale.Item>();
                string query = "GetItems_AzSmart";

                SqlConnection conn = new SqlConnection(DbHelpers.DbConnectionString);
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", Properties.Settings.Default.UserID);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr["name"].ToString();
                    string Id = dr["item_id"].ToString();
                    string barcode = dr["code"].ToString();
                    decimal salePrice = Convert.ToDecimal(dr["salePrice"]);
                    decimal _purchasePrice = Convert.ToDecimal(dr["purchasePrice"]);
                    decimal quantity = Convert.ToDecimal(dr["quantity"]);
                    string taxName = dr["TaxName"].ToString();
                    int TaxCode = Convert.ToInt32(dr["TaxCode"]);
                    int calcType = Convert.ToInt32(dr["calcType"]);
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

                    var taxs = new List<RequestSale.itemTaxes>
                    {
                        new RequestSale.itemTaxes
                        {
                            fullName = taxName,
                            taxName = taxName,
                            taxPrc = TaxPrc,
                            calcType = calcType,
                        }
                    };

                    RequestSale.Item itemProduct = new RequestSale.Item
                    {
                        itemId = Id,
                        itemName = name,
                        itemBarcode = barcode,
                        itemQty = miqdar,
                        itemAmount = price,
                        itemMarginPrice = purchasePrice,
                        itemMarginSum = purchasePriceSum,
                        itemTaxes = taxs
                    };
                    items.Add(itemProduct);
                }

                int cash = Convert.ToInt32(salesData.IncomingSum * 100);
                int card = Convert.ToInt32(salesData.Card * 100);
                int total = Convert.ToInt32(salesData.Total * 100);

                RequestSale.Payments payments = new RequestSale.Payments
                {
                    cashAmount = 0,
                    cashlessAmount = 0,
                    installmentAmount = total,
                    rrn = string.IsNullOrWhiteSpace(salesData.Rrn) ? null : salesData.Rrn,
                };

                string docnumber = await ReturnHeaderId();

                RequestSale.Root root = new RequestSale.Root
                {
                    employeeName = salesData.Cashier,
                    items = items,
                    payments = payments,
                    amount = total,
                    docNumber = docnumber
                };

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                var response = RequestPOST<ResponseSale>($"{salesData.IpAddress}/sale", salesData.MerchantId, json);
                requestJson = response.requestJson;
                responseJson = response.responseJson;

                if (response.code != 506)
                {
                    if (response.data.status == "success")
                    {
                        posSalesId = DbProsedures.InsertPosSales(new PosSales
                        {
                            posNomre = response.data.fiscalNum,
                            longFiskalId = response.data.fiscalID.ToString(),
                            proccessNo = salesData.ProccessNo,
                            cash = cash,
                            card = total,
                            total = total,
                            json = json,
                            shortFiskalId = null,
                            rrn = response.data.rrn
                        });

                        if (MessageVisible)
                        {
                            ReadyMessages.SUCCESS_SALES_MESSAGE();
                        }

                        FormHelpers.Log($"Pos satışı uğurla edildi. Qəbz №: {response.data.fiscalNum}");
                        return true;
                    }
                    else
                    {
                        ReadyMessages.ERROR_SALES_MESSAGE(response.data.message);
                        FormHelpers.Log($"Pos satışı xətası - Xəta mesajı: {response.data.message}");
                        return false;
                    }
                }
                else
                {
                    ReadyMessages.ERROR_SALES_MESSAGE($"Kassa ilə əlaqə zamanı xəta yarandı\n{response.message}");
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
                    OperationId = posSalesId,
                    Message = posSalesId == 0 ? "Error" : "Success",
                    RequestCode = requestJson,
                    ResponseCode = responseJson,
                });
                Cursor.Current = Cursors.Default;
            }




            /*
            List<Item> items = new List<Item>();
            SqlConnection conn = new SqlConnection(DbHelpers.DbConnectionString);
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            string query = $@"select name,
            Item.item_id,
            salePrice,
            quantity,
            case vatType 
            when 1 then '1800' 
            when 4 then '200'
            when 5 then '800'
            else 0 end as TaxPrc, 
            case vatType 
            when 1 then N'ƏDV 18%' 
            when 4 then 'SV-2%' 
            when 5 then 'SV-8%' 
            when 3 then N'ƏDV-dən azad' end as TaxName,
            quantityType,
            salePrice*quantity as ssum
            FROM  dbo.item where user_id = {Properties.Settings.Default.UserID};";

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

            string docnumber = await ReturnHeaderId();

            RootObject rootObject = new RootObject
            {
                employeeName = cashier,
                rrn = rrn,
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
            */
        }

        public static void XReport(string ipAddress, string merchantId, string cashier)
        {
            var root = new
            {
                departmentName = string.Empty
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST<BaseResponse>($"{ipAddress}/x_report", merchantId, json);

            if (response.code != 506)
            {
                if (response.data.status is "success")
                {
                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_X_REPORT_MESSAGE();
                    }
                    FormHelpers.Log(CommonData.SUCCESS_X_REPORT);
                }
                else
                {
                    ReadyMessages.WARNING_DEFAULT_MESSAGE(response.data.message);
                }
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE($"Kassa ilə əlaqə zamanı xəta yarandı\n{response.message}");
            }
        }

        public static void PeriodicReport(DateTime _start, DateTime _end, string ipAddress, string merchantId)
        {
            string start = _start.ToString("yyyy-MM-dd");
            string end = _end.ToString("yyyy-MM-dd");

            RequestPeriodicReport root = new RequestPeriodicReport
            {
                employeeName = "Kassir",
                dateFrom = start,
                dateFor = end,
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST<BaseResponse>($"{ipAddress}/dates_report", merchantId, json);
            if (response.code != 506)
            {
                if (response.data.status == "success")
                {
                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCES_PERİODİC_Z_REPORT_MESSAGE();
                    }
                    FormHelpers.Log(CommonData.SUCCES_PERİODİC_Z_REPORT);
                }
                else
                {
                    ReadyMessages.ERROR_DEFAULT_MESSAGE(response.data.message);
                    FormHelpers.Log($"Dövrü hesabat çap edilərkən xəta yarandı. Xəta mesajı: {response.data.message}");
                }
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE($"Kassa ilə əlaqə zamanı xəta yarandı\n{response.message}");
            }
        }

        private async static Task<string> ReturnHeaderId()
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = $"SELECT header_id FROM dbo.header WHERE userId = {Properties.Settings.Default.UserID}";
                    await con.OpenAsync();
                    var result = await cmd.ExecuteScalarAsync();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private static string JsonConvertBase64(string json, string merchantId)
        {
            string base64Data = Base64Encode(json);
            string sha1Hash = sha1(base64Data + merchantId);
            string base64Sign = Base64Encode(sha1Hash);
            return $"data={base64Data.Replace("=", "%3D")}&sign={base64Sign.Replace("=", "%3D")}";
        }


        #region [..Request Classes..]

        private class RequestSale
        {
            public class ExtraPayment
            {
                public string code { get; set; }
                public int amount { get; set; }
                public string trxParams { get; set; }
            }

            public class Item
            {
                public string itemId { get; set; }
                public string itemCodeType { get; set; } = null;
                public string itemName { get; set; }
                public int? itemAttr { get; set; } = null;
                public string itemQRCode { get; set; } = null;
                public string itemCode { get; set; } = null;
                public string itemUnitCode { get; set; } = null;
                public string itemUnit { get; set; } = null;
                public string itemBarcode { get; set; } = null;
                public int itemQty { get; set; }
                public int itemAmount { get; set; }
                public int discount { get; set; }
                public int? discountPrc { get; set; } = null;
                public string extraData { get; set; } = null;
                public string textToPrint { get; set; } = null;
                public List<itemTaxes> itemTaxes { get; set; }
                public int? itemMarginSum { get; set; } = null;
                public int? itemMarginPrice { get; set; } = null;
            }

            public class itemTaxes
            {
                public string taxName { get; set; }
                public string fullName { get; set; }
                public int taxPrc { get; set; }
                public int? taxCode { get; set; } = null;
                public int calcType { get; set; } = 1;
            }

            public class Payments
            {
                public int cashAmount { get; set; }
                public int cashlessAmount { get; set; }
                public int creditAmount { get; set; }
                public int bonusesAmount { get; set; }
                public int prepaymentAmount { get; set; }
                public int prepaymentCashlessAmount { get; set; }
                public int installmentAmount { get; set; }
                public int invoiceAmount { get; set; }
                public string rrn { get; set; } = null;
                public string cardNumber { get; set; } = null;
                public string bankName { get; set; } = null;
            }

            public class Root
            {
                public string documentID { get; set; } = null;
                public int? documentExtID { get; set; } = null;
                public string docTime { get; set; } = null;
                public string docNumber { get; set; }
                public string wsName { get; set; } = null;
                public string departmentName { get; set; } = null;
                public string departmentCode { get; set; } = null;
                public string employeeName { get; set; }
                public int amount { get; set; }
                public string currency { get; set; } = "AZN";
                public List<Item> items { get; set; }
                public Payments payments { get; set; }
                // public List<ExtraPayment> extraPayments { get; set; } = null;
                public string fiscalID { get; set; } = null;
                public string printFooter { get; set; } = null;
                public string creditContract { get; set; } = null;
                public string prepayDocID { get; set; } = null;
                public string prepayDocNum { get; set; } = null;
                public string clientPhone { get; set; } = null;
                public string clientName { get; set; } = null;
                public int? tips { get; set; } = null;
                public int? cashback { get; set; } = null;
                public bool? skipReceiptPrint { get; set; } = null;
            }
        }

        private class RequestPeriodicReport
        {
            public string employeeName { get; set; }
            public string dateFrom { get; set; }
            public string dateFor { get; set; }
        }

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
            public string rrn { get; set; } = null;
        }

        #endregion [..Request Classes..]



        #region [..Response Classes..]

        public class BaseRequestResponse<T>
        {
            public int code { get; set; }
            public string message { get; set; }
            public string requestJson { get; set; }
            public string responseJson { get; set; }
            public T data { get; set; }
        }

        public class BaseResponse
        {
            public string status { get; set; }
            public int code { get; set; }
            public string message { get; set; }
        }

        private class ResponseCheckShift : BaseResponse
        {
            public string isShiftOpen { get; set; }
            public string shiftOpenAt { get; set; }
        }

        private class ResponseSale : BaseResponse
        {
            public string auth { get; set; }
            public string card_num { get; set; }
            public string checkNum { get; set; }
            public int docStatus { get; set; }
            public int documentExtID { get; set; }
            public int documentID { get; set; }
            public string fiscalID { get; set; }
            public string fiscalNum { get; set; }
            public string preview_data { get; set; }
            public int printError { get; set; }
            public string printTime { get; set; }
            public string rrn { get; set; }
            public int shiftOrdersCnt { get; set; }
            public List<TotalPayment> totalPayments { get; set; }
            public class TotalPayment
            {
                public int amount { get; set; }
                public string auth { get; set; }
                public string card_num { get; set; }
                public int change { get; set; }
                public string checkNum { get; set; }
                public string extraParams { get; set; }
                public int id { get; set; }
                public string name { get; set; }
                public string notes { get; set; }
                public string rrn { get; set; }
                public string type { get; set; }
            }
        }

        private class ResponseRefund : BaseResponse
        {
            public string rrn { get; set; }
            public string fiscalNum { get; set; }
            public object fiscalID { get; set; }
        }


        #endregion [..Response Classes..]
    }
}
