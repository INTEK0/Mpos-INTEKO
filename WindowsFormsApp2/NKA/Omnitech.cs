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
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;

namespace WindowsFormsApp2.NKA
{
    public static class Omnitech
    {
        private static readonly bool MessageVisible = FormHelpers.SuccessMessageVisible();
        private static OmnitechResponse RequestPOST(string ipAddress, string json)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(ipAddress, Method.Post);
                request.AddHeader("Content-Type", "application/json;charset=utf-8");
                request.AddStringBody(json, DataFormat.Json);
                RestResponse response = client.Execute(request);

                if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE();
                    FormHelpers.Log($"Kassa ilə əlaqə zamanı xəta yarandı\n\n {response.ErrorMessage}");
                    return null;
                }
                else
                {
                    OmnitechResponse weatherForecast = System.Text.Json.JsonSerializer.Deserialize<OmnitechResponse>(response.Content);
                    return weatherForecast;
                }
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
                return null;
            }

        }

        public static string Login(string ipAddress)
        {
            var requestData = new RequestData
            {
                int_ref = null,
                checkData = new CheckData
                {
                    payment_change = null,
                    check_type = 40
                },
                name = "Api",
                password = "1"
            };

            var root = new RootObject
            {
                requestData = requestData
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json);

            if (response != null)
            {
                if (response.message == "login success")
                {
                    return response.access_token;
                }

                ReadyMessages.ERROR_LOGIN_MESSAGE(response.message);
                FormHelpers.Log($"{ErrorMessages.ERROR_LOGIN} Xəta mesajı: {response.message}");
                return null;

            }
            else
            {
                return null;
            }
        }

        public static bool GetShiftStatus(string ipAddress, string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                accessToken = Login(ipAddress);
            }

            var requestData = new RequestData
            {
                access_token = accessToken,
                int_ref = null,
                checkData = new CheckData
                {
                    payment_change = null,
                    check_type = 14
                }
            };

            var root = new RootObject
            {
                requestData = requestData
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json);

            if (response.message is "Successful operation")
            {
                if (response.shiftStatus)
                {
                    string open_time = Convert.ToDateTime(response.shift_open_time).ToString("dd.MM.yyyy HH:mm:ss");
                    ReadyMessages.SUCCESS_SHIFT_STATUS_MESSAGE(open_time);
                    return true;
                }
                else
                {
                    OpenShift(ipAddress, accessToken);
                    return false;
                }
            }
            else
            {
                ReadyMessages.ERROR_OPENSHIFT_MESSAGE(response.message);
                return false;
            }
        }

        private static void OpenShift(string ipAddress, string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                accessToken = Login(ipAddress);
            }

            var requestData = new RequestData
            {
                int_ref = null,
                checkData = new CheckData
                {
                    check_type = 15
                },
                access_token = accessToken
            };

            var root = new RootObject
            {
                requestData = requestData
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json);

            if (response.message == "Successful operation")
            {
                ReadyMessages.SUCCESS_OPEN_SHIFT_MESSAGE();
                FormHelpers.Log(CommonData.SUCCESS_OPEN_SHIFT);
            }
            else if (response.message == "document: invalid shift status")
            {
                ReadyMessages.WARNING_DEFAULT_MESSAGE(response.message);
                FormHelpers.Log($"{ErrorMessages.ERROR_OPENSHIFT} Xəta mesajı: {response.message}");
            }
            else
            {
                ReadyMessages.ERROR_OPENSHIFT_MESSAGE(response.message);
                FormHelpers.Log($"{ErrorMessages.ERROR_OPENSHIFT} Xəta mesajı: {response.message}");
            }
        }

        public static void CloseShift(string ipAddress, string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                accessToken = Login(ipAddress);
            }

            var requestData = new RequestData
            {
                int_ref = null,
                checkData = new CheckData
                {
                    payment_change = null,
                    check_type = 13
                },
                access_token = accessToken
            };

            var root = new RootObject
            {
                requestData = requestData
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json);

            if (response.message is "Successful operation")
            {
                if (MessageVisible)
                {
                    ReadyMessages.SUCCESS_CLOSE_SHIFT_MESSAGE();
                }
                FormHelpers.Log($"{CommonData.SUCCESS_CLOSE_SHIFT}\n" +
                                $"Növbənin açılma vaxtı: {response.data.shiftOpenAtUtc}\n" +
                                $"Növbənin bağlanma vaxtı: {response.data.createdAtUtc}");
            }
        }

        public static void XReport(string ipAddress, string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                accessToken = Login(ipAddress);
            }

            var requestData = new RequestData
            {
                int_ref = null,
                checkData = new CheckData
                {
                    check_type = 12
                },
                access_token = accessToken
            };

            var root = new RootObject
            {
                requestData = requestData
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json);

            if (response.message == "Successful operation")
            {
                if (MessageVisible)
                {
                    ReadyMessages.SUCCESS_X_REPORT_MESSAGE();
                }

                FormHelpers.Log(CommonData.SUCCESS_X_REPORT);
            }
            else
            {
                ReadyMessages.ERROR_X_REPORT_MESSAGE(response.message);
                FormHelpers.Log($"{ErrorMessages.ERROR_X_REPORT}  Xəta mesajı: {response.message}");
            }
        }

        public static bool Sales(string ipAddress, string token, string proccessNo, decimal total, decimal cash, decimal card, decimal incomingSum, string cashier, Customer customer, Doctor doctor, string rrn = "")
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                token = Login(ipAddress);
            }

            List<Item> items = new List<Item>();
            List<VatAmount> vatAmounts = new List<VatAmount>();

            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = DbHelpers.DbConnectionString;
            conn.Open();
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

            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();

            decimal vatSumFor18Percent = 0;
            decimal vatSumFor2Percent = 0;
            decimal vatSumFor0Percent = 0;

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


                Item item = new Item
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
            }

            if (vatSumFor18Percent > 0)
            {
                vatAmounts.Add(new VatAmount
                {
                    vatPercent = 18,
                    vatSum = vatSumFor18Percent
                });
            }

            if (vatSumFor2Percent > 0)
            {
                vatAmounts.Add(new VatAmount
                {
                    vatPercent = 2,
                    vatSum = vatSumFor0Percent
                });
            }

            if (vatSumFor0Percent > 0)
            {
                vatAmounts.Add(new VatAmount
                {
                    vatPercent = 0,
                    vatSum = vatSumFor0Percent
                });
            }

            Data data = new Data
            {
                sum = total,
                cashSum = cash,
                cashlessSum = card,
                incomingSum = incomingSum,
                cashier = cashier,
                items = items,
                vatAmounts = vatAmounts,
            };



            Parameters parameters = new Parameters
            {
                doc_type = "sale",
                data = data
            };

            //List<ReceiptDetail> receiptDetails = new List<ReceiptDetail>()
            //{
            //new ReceiptDetail { v = $"Müştəri: {customer.Name} {customer.Surname} {customer.FatherName}" },
            ////new ReceiptDetail { t = 1, k = "Number", v = "new number" },
            ////new ReceiptDetail { t = 2, k = "Number", v = "5258645" }
            //};

            TokenData tokenData = new TokenData
            {
                parameters = parameters
            };

            CheckData checkData = new CheckData
            {
                payment_change = null,
                check_type = 1
            };


            RequestData requestData = new RequestData
            {
                access_token = token,
                tokenData = tokenData,
                checkData = checkData
            };

            RootObject rootObject = new RootObject
            {
                requestData = requestData,
                receiptDetails = null
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json);

            if (response != null)
            {
                if (response.message == "Successful operation")
                {
                    DbProsedures.InsertPosSales(new PosSales
                    {
                        posNomre = response.document_number.ToString(),
                        longFiskalId = response.long_id,
                        proccessNo = proccessNo,
                        cash = cash,
                        card = card,
                        total = total,
                        json = json,
                        shortFiskalId = response.short_id,
                        customerId = customer?.CustomerID,
                        doctorId = doctor?.Id,
                    });

                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_SALES_MESSAGE();
                    }

                    FormHelpers.Log($"Pos satışı uğurla edildi. Qəbz №: {response.document_number}");
                    return true;
                }
                else if (response.message == "document: invalid shift duration")
                {
                    XtraMessageBox.Show("GÜN SONU (Z) HESABATI ÇIXARILMAYIB !\n\nZəhmət olmasa pos bağla düyməsinə vuraraq günü sonlandırın.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else if (response.message == "document: invalid shift status")
                {
                    XtraMessageBox.Show("NÖVBƏ AÇILMAYIB !\n\nZəhmət olmasa pos aç düyməsinə vuraraq növbəni açın.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        public static bool SalesPre(string ipAddress, string token, string proccessNo, decimal total, decimal cash, decimal card, decimal incomingSum, string cashier, Customer customer, Doctor doctor, string rrn = "")
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                token = Login(ipAddress);
            }

            List<Item> items = new List<Item>();
            List<VatAmount> vatAmounts = new List<VatAmount>();

            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.SqlCon;
            conn.Open();
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

            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();

            decimal vatSumFor18Percent = 0;
            decimal vatSumFor2Percent = 0;
            decimal vatSumFor0Percent = 0;

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


                Item item = new Item
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
            }

            if (vatSumFor18Percent > 0)
            {
                vatAmounts.Add(new VatAmount
                {
                    vatPercent = 18,
                    vatSum = vatSumFor18Percent
                });
            }

            if (vatSumFor2Percent > 0)
            {
                vatAmounts.Add(new VatAmount
                {
                    vatPercent = 2,
                    vatSum = vatSumFor0Percent
                });
            }

            if (vatSumFor0Percent > 0)
            {
                vatAmounts.Add(new VatAmount
                {
                    vatPercent = 0,
                    vatSum = vatSumFor0Percent
                });
            }

            Data data = new Data
            {
                sum = total,
                cashSum = cash,
                cashlessSum = card,
                incomingSum = incomingSum,
                cashier = cashier,
                items = items,
                vatAmounts = vatAmounts,
                prepaymentSum = cash + card,
            };



            Parameters parameters = new Parameters
            {
                doc_type = "prepay",
                data = data
            };



            TokenData tokenData = new TokenData
            {
                parameters = parameters
            };

            CheckData checkData = new CheckData
            {
                payment_change = null,
                check_type = 34
            };


            RequestData requestData = new RequestData
            {
                access_token = token,
                tokenData = tokenData,
                checkData = checkData
            };

            RootObject rootObject = new RootObject
            {
                requestData = requestData,
                receiptDetails = null
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });


            var response = RequestPOST(ipAddress, json);

            if (response != null)
            {
                if (response.message == "Successful operation")
                {
                    DbProsedures.InsertPosSales(new PosSales
                    {
                        posNomre = response.document_number.ToString(),
                        longFiskalId = response.long_id,
                        proccessNo = proccessNo,
                        cash = cash,
                        card = card,
                        total = total,
                        json = json,
                        shortFiskalId = response.short_id,
                        customerId = customer?.CustomerID,
                        doctorId = doctor?.Id,
                    });

                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_SALES_MESSAGE();
                    }

                    FormHelpers.Log($"Pos satışı uğurla edildi. Qəbz №: {response.document_number}");
                    return true;
                }
                else if (response.message == "document: invalid shift duration")
                {
                    XtraMessageBox.Show("GÜN SONU (Z) HESABATI ÇIXARILMAYIB !\n\nZəhmət olmasa pos bağla düyməsinə vuraraq günü sonlandırın.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else if (response.message == "document: invalid shift status")
                {
                    XtraMessageBox.Show("NÖVBƏ AÇILMAYIB !\n\nZəhmət olmasa pos aç düyməsinə vuraraq növbəni açın.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
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





        public static bool PreSaleFinish(string ipAddress, string token, decimal id, string username, string fisids)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                token = Login(ipAddress);
            }

            List<Item> items = new List<Item>();
            List<VatAmount> vatAmounts = new List<VatAmount>();

            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.SqlCon;
            conn.Open();
            string query = $@"
SELECT 
A.[MEHSUL_ADI] as name,d.item_id,D.satis_giymet as salePrice,D.satis_giymet as purchasePrice,d.count_ as quantity,



case A.VERGI_DERECESI 
        when 1 then '18' 
        when 2 then '18' 
        when 3 then '0'
        when 4 then '2' 
        when 5 then '8'
    end as vatType,

	case  A.VERGI_DERECESI 
        when 1 then '18%' 
		when 2 then N'TİCARƏT ƏLAVƏSİ 18%'
        when 3 then N'ƏDV-SİZ' 
        when 4 then 'SV-2%' 
        when 5 then '8%' 
    end as vatTypeName,

	 D.quantity_type as quantityType,
    d.count_ * d.satis_giymet as ssum,pos.fiscal_id,pos.[Prepayment]
 
  FROM [dbo].[pos_satis_check_details] as D,[dbo].[MAL_ALISI_DETAILS] AS A,[pos_satis_check_main] as pos

  where 
  POS.pos_satis_check_main_id=D.pos_satis_check_main_id AND 
  A.[mal_alisi_details_id]=D.[mal_alisi_details_id]
  
  AND D.[pos_satis_check_main_id]=" + id;

            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();

            decimal vatSumFor18Percent = 0;
            decimal vatSumFor2Percent = 0;
            decimal vatSumFor0Percent = 0;
            decimal total = 0;
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
                total = total + ssum;
                if (vatTypeName != "TİCARƏT ƏLAVƏSİ 18%")
                {
                    marginSum = null;
                    purchasePrice = null;
                }


                Item item = new Item
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
            }

            if (vatSumFor18Percent > 0)
            {
                vatAmounts.Add(new VatAmount
                {
                    vatPercent = 18,
                    vatSum = vatSumFor18Percent
                });
            }

            if (vatSumFor2Percent > 0)
            {
                vatAmounts.Add(new VatAmount
                {
                    vatPercent = 2,
                    vatSum = vatSumFor0Percent
                });
            }

            if (vatSumFor0Percent > 0)
            {
                vatAmounts.Add(new VatAmount
                {
                    vatPercent = 0,
                    vatSum = vatSumFor0Percent
                });
            }

            Data data = new Data
            {
                parents = fisids,
                prepaymentSum = total,
                sum = 0,
                cashSum = 0,
                cashlessSum = 0,
                incomingSum = 0,
                cashier = username,
                items = items,
                vatAmounts = vatAmounts,
            };



            Parameters parameters = new Parameters
            {
                doc_type = "sale",
                data = data
            };

            List<ReceiptDetail> receiptDetails = new List<ReceiptDetail>()
            {
            new ReceiptDetail { v = $"Müştəri: Yeni Müştəri" },
            //new ReceiptDetail { t = 1, k = "Number", v = "new number" },
            //new ReceiptDetail { t = 2, k = "Number", v = "5258645" }
            };

            TokenData tokenData = new TokenData
            {
                parameters = parameters
            };

            CheckData checkData = new CheckData
            {
                payment_change = null,
                check_type = 1
            };


            RequestData requestData = new RequestData
            {
                access_token = token,
                tokenData = tokenData,
                checkData = checkData
            };

            RootObject rootObject = new RootObject
            {
                requestData = requestData,
                receiptDetails = receiptDetails
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });


            var response = RequestPOST(ipAddress, json);

            if (response != null)
            {
                if (response.message == "Successful operation")
                {


                    SqlConnection connection4 = new SqlConnection(Properties.Settings.Default.SqlCon);
                    string queryStringk = $"UPDATE [dbo].[pos_satis_check_main] SET [PREdate_]=getdate(),PREfiscal_id='{response.document_number}' where pos_satis_check_main_id={id}  ";
                    connection4.Open();
                    SqlCommand command4 = new SqlCommand(queryStringk, connection4);

                    SqlDataReader dr4 = command4.ExecuteReader();



                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_SALES_MESSAGE();
                    }

                    FormHelpers.Log($"Pos satışı uğurla edildi. Qəbz №: {response.document_number}");
                    return true;
                }
                else if (response.message == "document: invalid shift duration")
                {
                    XtraMessageBox.Show("GÜN SONU (Z) HESABATI ÇIXARILMAYIB !\n\nZəhmət olmasa pos bağla düyməsinə vuraraq günü sonlandırın.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else if (response.message == "document: invalid shift status")
                {
                    XtraMessageBox.Show("NÖVBƏ AÇILMAYIB !\n\nZəhmət olmasa pos aç düyməsinə vuraraq növbəni açın.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        public static void PeriodicReport(DateTime _start, DateTime _end, string ipAddress)
        {
            string start = _start.ToString("yyyy-MM-dd hh:mm:ss");
            string end = _end.ToString("yyyy-MM-dd hh:mm:ss");

            string accessToken = Login(ipAddress);

            var requestData = new RequestData
            {
                date_start = start,
                date_end = end,
                int_ref = null,
                checkData = new CheckData
                {
                    payment_change = null,
                    check_type = 18
                },
                access_token = accessToken
            };

            var root = new RootObject
            {
                requestData = requestData
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json);

            if (response.message == "success")
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

        public static string Refund(string ipAddress, string accessToken, PayType payType, string cashier, string proccesNo)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                accessToken = Login(ipAddress);
            }

            string _fiskallID = "", _shortFiskallID = "", _checkNum = "";
            decimal _cash = default, _card = default, _total2 = default;

            using (SqlConnection conn2 = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                conn2.Open();
                string query2 = $@"SELECT [pos_satis_check_main_id],
                [pos_nomre],
                [fiscal_id],
                [NEGD_],
                [KART_],
                [UMUMI_MEBLEG], 
                [fiscalNum]
                FROM [pos_satis_check_main] WHERE [pos_satis_check_main_id] IN 
                (SELECT [pos_satis_check_main_id] FROM [pos_gaytarma_manual] WHERE [pos_gaytarma_manual_id] = 
                (SELECT MAX([pos_gaytarma_manual_id]) FROM [pos_gaytarma_manual] WHERE user_id_ = {Properties.Settings.Default.UserID}));";

                using (SqlCommand cmd2 = new SqlCommand(query2, conn2))
                using (SqlDataReader dr2 = cmd2.ExecuteReader())
                {
                    if (dr2.Read())
                    {
                        _fiskallID = dr2["fiscal_id"].ToString();
                        _shortFiskallID = dr2["fiscalNum"].ToString();
                        _checkNum = dr2["pos_nomre"].ToString();
                        _cash = Convert.ToDecimal(dr2["NEGD_"]);
                        _card = Convert.ToDecimal(dr2["KART_"]);
                        _total2 = Convert.ToDecimal(dr2["UMUMI_MEBLEG"]);
                    }
                }
            }


            List<Item> items = new List<Item>();
            decimal totalMarginSum = 0;  // Toplam itemMarginSum'u tutacak
            decimal totalItemSum = 0;    // Toplam itemSum'u tutacak

            using (SqlConnection conn = new SqlConnection(DbHelpers.DbConnectionString))
            {
                conn.Open();
                string query = $@"(SELECT md.MEHSUL_ADI AS name,
                                p.item_id AS code,
                                pl.say AS say,
                                md.ALIS_GIYMETI as alis_giymet,
                                p.satis_giymet AS satis_giymet,
                                pl.say * p.satis_giymet as tutar,
                                p.quantity_type AS quantity_type,
                                md.VERGI_DERECESI AS vtypes,
                                 case md.VERGI_DERECESI 
                                 when 1 then '18' 
                                 when 2 then '18' 
                                 when 3 then '0' 
                                 when 4 then '2' 
                                 when 5 then '8' 
                                 end as TaxPrc
                                FROM pos_satis_check_details p
                                INNER JOIN MAL_ALISI_DETAILS md ON p.mal_alisi_details_id = md.MAL_ALISI_DETAILS_ID
                                INNER JOIN pos_gaytarma_manual pl ON p.pos_satis_check_details_id = pl.pos_satis_check_details
                                WHERE pl.emeliyyat_nomre = '{proccesNo}')";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        decimal? purchasePrice = (Convert.ToInt32(dr["vtypes"]) == 2) ? Convert.ToDecimal(dr["alis_giymet"]) : (decimal?)null;
                        double qty = Convert.ToDouble(dr["say"]);
                        decimal ssum = Convert.ToDecimal(dr["tutar"]);
                        decimal? marginSum = purchasePrice * (decimal)qty;

                        // item bilgilerini oluşturma
                        Item item = new Item
                        {
                            itemName = dr["name"].ToString(),
                            itemCode = dr["code"].ToString(),
                            itemQuantityType = Convert.ToInt32(dr["quantity_type"]),
                            itemQuantity = qty,
                            itemPrice = Convert.ToDecimal(dr["satis_giymet"]),
                            itemSum = ssum,
                            itemVatPercent = Convert.ToDecimal(dr["TaxPrc"]),
                            itemMarginPrice = purchasePrice,
                            itemMarginSum = marginSum
                        };
                        items.Add(item);

                        // Toplam margin sum ve item sum ekleniyor
                        totalMarginSum += marginSum ?? 0;
                        totalItemSum += ssum;
                    }
                }
            }

            // KDV hesaplamaları için iki farklı toplam
            decimal vatSumFor18Percent = 0;
            decimal vatSumFor0Percent = 0;


            // VatPercent'e göre toplama işlemi
            foreach (var item in items)
            {
                if (item.itemVatPercent == 18)
                {
                    vatSumFor18Percent += item.itemSum;
                }
                else if (item.itemVatPercent == 0)
                {
                    vatSumFor0Percent += item.itemSum;
                }
            }



            List<VatAmount> vatAmounts = new List<VatAmount>();

            // Eğer vatSumFor18Percent değeri sıfırdan büyükse ekle
            if (vatSumFor18Percent > 0)
            {
                vatAmounts.Add(new VatAmount
                {
                    vatPercent = 18,
                    vatSum = vatSumFor18Percent
                });
            }

            // Eğer vatSumFor0Percent değeri sıfırdan büyükse ekle
            if (vatSumFor0Percent > 0)
            {
                vatAmounts.Add(new VatAmount
                {
                    vatPercent = 0,
                    vatSum = vatSumFor0Percent
                });
            }



            Data data = new Data
            {
                sum = _total2,
                cashSum = (payType == PayType.Cash) ? _total2 : 0,
                cashlessSum = (payType == PayType.Card) ? _card : 0,
                incomingSum = (payType == PayType.CashCard) ? _cash : 0,
                cashier = cashier,
                vatAmounts = vatAmounts,
                items = items,
                refund_short_document_id = _shortFiskallID,
                parentDocument = _fiskallID,
                refund_document_number = _checkNum
            };

            Parameters parameters = new Parameters { doc_type = "money_back", data = data };
            TokenData tokenData = new TokenData { parameters = parameters };
            CheckData checkData = new CheckData { check_type = 100 };

            RequestData requestData = new RequestData { access_token = accessToken, tokenData = tokenData, checkData = checkData };
            RootObject rootObject = new RootObject { requestData = requestData };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            return json;
        }

        public static void LastReceiptCopy(string ipAdress, string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                accessToken = Login(ipAdress);
            }

            string fiskalID = string.Empty;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(DbHelpers.LastDocumentFiskalId, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            fiskalID = dr[0].ToString();
                        }
                    }
                }
            }

            RequestData requestData = new RequestData
            {
                access_token = accessToken,
                int_ref = null,
                checkData = new CheckData
                {
                    check_type = 11,
                    payment_change = null
                },
                fiscalId = fiskalID,
            };

            RootObject root = new RootObject
            {
                requestData = requestData
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAdress, json);

            if (response.message == "Successful operation")
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


        #region [..Request Classes..]

        private class Item
        {
            public string itemName { get; set; }
            public int itemCodeType { get; set; } = 0;
            public string itemCode { get; set; }
            public int itemQuantityType { get; set; }
            public double itemQuantity { get; set; }
            public decimal itemPrice { get; set; }
            public decimal itemSum { get; set; }
            public decimal itemVatPercent { get; set; }
            public decimal discount { get; set; } = 0;
            public decimal? itemMarginPrice { get; set; } = null;
            public decimal? itemMarginSum { get; set; } = null;
        }

        private class VatAmount
        {
            public decimal vatSum { get; set; }
            public decimal? vatPercent { get; set; }
        }

        private class Data
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
            public string parentDocument { get; set; } = null;
            public string refund_short_document_id { get; set; } = null;
            public string refund_document_number { get; set; } = null;

            public string parents { get; set; } = null;
            public List<VatAmount> vatAmounts { get; set; }
        }

        private class Parameters
        {
            public string doc_type { get; set; }
            public Data data { get; set; }
        }

        private class TokenData
        {
            public Parameters parameters { get; set; }
            public string operationId { get; set; } = "createDocument";
            public int version { get; set; } = 1;
        }

        private class CheckData
        {
            public int? payment_change { get; set; } = 0;
            public int check_type { get; set; } = 1;
        }

        private class RequestData
        {
            public string date_start { get; set; } = null;
            public string date_end { get; set; } = null;
            public string name { get; set; } = null;
            public string password { get; set; } = null;
            public string access_token { get; set; } = null;
            public string int_ref { get; set; } = "123456";
            public string fiscalId { get; set; } = null;
            public TokenData tokenData { get; set; }
            public CheckData checkData { get; set; }
        }

        private class ReceiptDetail
        {
            public int? t { get; set; } = null;
            public string k { get; set; } = null;
            public string v { get; set; }
        }

        private class RootObject
        {
            public RequestData requestData { get; set; }
            public List<ReceiptDetail> receiptDetails { get; set; }
        }

        #endregion [..Request Classes..]


        #region [..Response Classes..]

        public class OmnitechResponse
        {
            public int code { get; set; }
            public string message { get; set; }
            public string access_token { get; set; }
            public string serial { get; set; }
            public bool shiftStatus { get; set; }
            public string desc { get; set; }
            public string shift_open_time { get; set; }
            public int document_number { get; set; }
            public string long_id { get; set; }
            public string short_id { get; set; }
            public int shift_document_number { get; set; }
            public OmnitechResponseData data { get; set; }
        }

        public class OmnitechResponseData
        {
            public string createdAtUtc { get; set; }
            public string shiftOpenAtUtc { get; set; }
            public string document_id { get; set; }
            public int firstDocNumber { get; set; }
            public int lastDocNumber { get; set; }
            public int reportNumber { get; set; }
        }

        #endregion [..Response Classes..]
    }
}