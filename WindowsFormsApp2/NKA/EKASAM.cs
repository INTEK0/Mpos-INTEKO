using DevExpress.XtraEditors;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.Helpers;
using System.Security.Cryptography;
using DevExpress.DashboardCommon;

namespace WindowsFormsApp2.NKA
{
    public static class EKASAM
    {
        private static Random random = new Random();
        private static readonly bool MessageVisible = FormHelpers.SuccessMessageVisible();
        private static EKASAMResponse RequestPOST(string ipAddress, string json, string kontrol)
        {
            string dt = DateTime.Now.ToString("yyyyMMddHHmmss");
            string nonce = Guid.NewGuid().ToString("n").Substring(0, 8);
            string token = ComputeSha256Hash(ComputeSha256Hash(dt) + ":" + nonce + ":" + "MPOS");


            try
            {
                var options = new RestClientOptions(ipAddress)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(kontrol, Method.Post);
                request.AddHeader("dt", dt);
                request.AddHeader("nonce", nonce);
                request.AddHeader("token", token);
                request.AddStringBody(json, DataFormat.Json);
                RestResponse response = client.Execute(request);






                //var client = new RestClient();
                //var request = new RestRequest(ipAddress, Method.Post);
                //request.AddHeader("Content-Type", "application/json;charset=utf-8");
                //request.AddStringBody(json, DataFormat.Json);
                //RestResponse response = client.Execute(request);

                if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE();
                    FormHelpers.Log($"Kassa ilə əlaqə zamanı xəta yarandı\n\n {response.ErrorMessage}");
                    return null;
                }
                else
                {
                    EKASAMResponse weatherForecast = System.Text.Json.JsonSerializer.Deserialize<EKASAMResponse>(response.Content);
                    return weatherForecast;
                }
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
                return null;
            }

        }


        private static EKASAMResponse RequestGET(string ipAddress, string json, string urla)
        {
            string dt = DateTime.Now.ToString("yyyyMMddHHmmss");
            string nonce = Guid.NewGuid().ToString("n").Substring(0, 8);
            string token = ComputeSha256Hash(ComputeSha256Hash(dt) + ":" + nonce + ":" + "MPOS");

            try
            {
                var options = new RestClientOptions(ipAddress)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(urla, Method.Get);
                request.AddHeader("dt", dt);
                request.AddHeader("nonce", nonce);
                request.AddHeader("token", token);
                RestResponse response = client.Execute(request);
                EKASAMResponse weatherForecast = System.Text.Json.JsonSerializer.Deserialize<EKASAMResponse>(response.Content);
                //Console.WriteLine(response.Content);
                return weatherForecast;

            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
                return null;
            }
        }



        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static EKASAMResponse RequestGETLOGIN(string ipAddress, string json = null)
        {

            string dt = DateTime.Now.ToString("yyyyMMddHHmmss");
            string nonce = Guid.NewGuid().ToString("n").Substring(0, 8);
            string token = ComputeSha256Hash(ComputeSha256Hash(dt) + ":" + nonce + ":" + "MPOS");

            try
            {
                var options = new RestClientOptions(ipAddress)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("kas_login", Method.Get);
                request.AddHeader("dt", dt);
                request.AddHeader("nonce", nonce);
                request.AddHeader("token", token);
                RestResponse response = client.Execute(request);
                EKASAMResponse weatherForecast = System.Text.Json.JsonSerializer.Deserialize<EKASAMResponse>(response.Content);
                //Console.WriteLine(response.Content);
                return weatherForecast;
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
                return null;
            }

        }

        public static string Login(string ipAddress)
        {
            var response = RequestGETLOGIN(ipAddress);

            if (response != null)
            {
                if (response.message == "Success operation")
                {
                    return response.data.access_token;
                }

                //ReadyMessages.ERROR_LOGIN_MESSAGE(response.message);
                //FormHelpers.Log($"{ErrorMessages.ERROR_LOGIN} Xəta mesajı: {response.message}");
                return null;

            }
            else
            {
                return null;
            }
        }

        public static bool GetShiftStatus(string ipAddress, string accessToken)
        {
            accessToken = Login(ipAddress);
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                accessToken = Login(ipAddress);
            }
            // OpenShift(ipAddress,"");

            var response = RequestGET(ipAddress, "", "kas_shift");

            if (response.message == "Success operation")
            {
             //   MessageBox.Show(response.shift_open.ToString());


                if (response.shift_open == false)

                {
                    // ReadyMessages.SUCCESS_OPEN_SHIFT_MESSAGE();
                    FormHelpers.Log("Shift Status Bagli");

                    OpenShift(ipAddress, accessToken);
                    return false;


                }

                else
                {

                    ReadyMessages.SUCCESS_OPEN_SHIFT_MESSAGE();
                    FormHelpers.Log("Shift Status Acildi");
                    return true;
                }

            }
            else if (response.message == "document: invalid shift status")
            {
                //ReadyMessages.WARNING_DEFAULT_MESSAGE(response.message);
                //FormHelpers.Log($"{ErrorMessages.ERROR_OPENSHIFT} Xəta mesajı: {response.message}");
                return false;
            }
            else { return false; }




            //if (response.message is "Success operation")
            //{
            //    if (response.data.shift_open)
            //    {
            //        string open_time = Convert.ToDateTime(response.data.shift_open_time).ToString("dd.MM.yyyy HH:mm:ss");
            //        ReadyMessages.SUCCESS_SHIFT_STATUS_MESSAGE(open_time);
            //        return true;
            //    }
            //    else
            //    {
            //        OpenShift(ipAddress + "/kas_shift", accessToken);
            //        return false;
            //    }
            //}
            //else
            //{
            //    ReadyMessages.ERROR_OPENSHIFT_MESSAGE(response.message);
            //    return false;
            //}
        }

        private static void OpenShift(string ipAddress, string accessToken)
        {




            var response = RequestGET(ipAddress, "", "kas_openshift");

            if (response.message == "Success operation")
            {
                ReadyMessages.SUCCESS_OPEN_SHIFT_MESSAGE();
                FormHelpers.Log(CommonData.SUCCESS_OPEN_SHIFT);

            }
            else if (response.message == "document: invalid shift status")
            {
                ReadyMessages.WARNING_DEFAULT_MESSAGE(response.message);
                FormHelpers.Log($"{ErrorMessages.ERROR_OPENSHIFT} Xəta mesajı: {response.message}");

            }
            else { }
        }

        public static void CloseShift(string ipAddress, string accessToken)
        {
            //accessToken = Login(ipAddress);
            //if (string.IsNullOrWhiteSpace(accessToken))
            //{
            //    accessToken = Login(ipAddress);
            //}

            //var requestData = new RequestData
            //{

            //};

            //var root = new RootObject
            //{
            //    requestData = requestData
            //};

            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            //{
            //    NullValueHandling = NullValueHandling.Ignore
            //});

            //var response = RequestGET(ipAddress + "/kas_closeshift", json);

            //if (response.message is "Successful operation")
            //{
            //    if (MessageVisible)
            //    {
            //        ReadyMessages.SUCCESS_CLOSE_SHIFT_MESSAGE();
            //    }
            //    FormHelpers.Log($"{CommonData.SUCCESS_CLOSE_SHIFT}\n" +
            //                    $"Növbənin açılma vaxtı: {response.data.shiftOpenAtUtc}\n" +
            //                    $"Növbənin bağlanma vaxtı: {response.data.createdAtUtc}");
            //}


            var response = RequestGET(ipAddress, "", "kas_closeshift");

            if (response.message == "Success operation")
            {
                ReadyMessages.SUCCESS_CLOSE_SHIFT_MESSAGE();
                FormHelpers.Log($"{CommonData.SUCCESS_CLOSE_SHIFT}\n" +
                               $"Növbənin açılma vaxtı: {response.data.shiftOpenAtUtc}\n" +
                               $"Növbənin bağlanma vaxtı: {response.data.createdAtUtc}");

            }
            else if (response.message == "document: invalid shift status")
            {
                ReadyMessages.WARNING_DEFAULT_MESSAGE(response.message);
                FormHelpers.Log($"{ErrorMessages.ERROR_OPENSHIFT} Xəta mesajı: {response.message}");

            }
            else { }
        }

        public static void XReport(string ipAddress, string accessToken)
        {
            //accessToken = Login(ipAddress);
            //if (string.IsNullOrWhiteSpace(accessToken))
            //{
            //    accessToken = Login(ipAddress);
            //}

            //var requestData = new RequestData
            //{

            //};

            //var root = new RootObject
            //{
            //    requestData = requestData
            //};

            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            //{
            //    NullValueHandling = NullValueHandling.Ignore
            //});

            //var response = RequestGET(ipAddress + "/kas_xreport", json);

            //if (response.message == "Successful operation")
            //{
            //    if (MessageVisible)
            //    {
            //        ReadyMessages.SUCCESS_X_REPORT_MESSAGE();
            //    }

            //    FormHelpers.Log(CommonData.SUCCESS_X_REPORT);
            //}
            //else
            //{
            //    ReadyMessages.ERROR_X_REPORT_MESSAGE(response.message);
            //    FormHelpers.Log($"{ErrorMessages.ERROR_X_REPORT}  Xəta mesajı: {response.message}");
            //}


            var response = RequestGET(ipAddress, "", "kas_xreport");

            if (response.message == "Success operation")
            {
                ReadyMessages.SUCCESS_X_REPORT_MESSAGE();
                FormHelpers.Log($"{ErrorMessages.ERROR_X_REPORT}  Xəta mesajı: {response.message}");

            }
            else
            {
                ReadyMessages.WARNING_DEFAULT_MESSAGE(response.message);
                FormHelpers.Log($"{ErrorMessages.ERROR_X_REPORT} Xəta mesajı: {response.message}");

            }

        }

        public static bool Sales(string ipAddress, string token, string proccessNo, decimal total, decimal cash, decimal card, decimal incomingSum, string cashier, Customer customer, Doctor doctor, decimal qaliq, string rrn = "")
        {
            Login(ipAddress);
            if (string.IsNullOrWhiteSpace(token))
            {
                token = Login(ipAddress);
            }

            List<Item> items = new List<Item>();
            List<Itemticaretelave> items2 = new List<Itemticaretelave>();
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
            decimal vatSumFor8Percent = 0;

            while (dr.Read())
            {
                string name = dr["name"].ToString();
                string code = dr["item_id"].ToString();
                decimal salePrice = Convert.ToDecimal(dr["salePrice"]);
                decimal purchasePrice = Convert.ToDecimal(dr["purchasePrice"]);
                double quantity = Convert.ToDouble(dr["quantity"]);
                int vatType = Convert.ToInt32(dr["vatType"]);
                string vatTypeName = dr["vatTypeName"].ToString();
                int quantityType = Convert.ToInt32(dr["quantityType"]);
                decimal ssum = Convert.ToDecimal(dr["ssum"]);

                decimal marginSum = (salePrice- purchasePrice) * (decimal)quantity;

                if (vatTypeName != "TİCARƏT ƏLAVƏSİ 18%")
                {
                    marginSum = 0;
                    purchasePrice = 0;
                }


                //vatAmounts.Add(new VatAmount
                //{

                //    vatSum = total
                //});

                if (vatTypeName == "TİCARƏT ƏLAVƏSİ 18%")

                {
                    Item item = new Item
                    {
                        itemName = name,
                        itemCode = code,
                        itemCodeType = 1,
                        itemQuantityType = quantityType,
                        itemQuantity = quantity,
                        itemPrice = salePrice,
                        itemSum = ssum,
                        itemVatPercent = vatType,
                        itemMarginPrice = purchasePrice,
                        itemMarginSum = marginSum,

                    };
                    items.Add(item);

                    vatSumFor18Percent += marginSum;
                }

                else

                {

                    Item item = new Item
                    {
                        itemName = name,
                        itemCode = code,
                        itemCodeType = 1,
                        itemQuantityType = quantityType,
                        itemQuantity = quantity,
                        itemPrice = salePrice,
                        itemSum = ssum,
                        itemVatPercent = vatType,
                        //itemMarginPrice = purchasePrice,
                        //itemMarginSum = marginSum,

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
                    vatSum = vatSumFor2Percent
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

            if (vatSumFor8Percent > 0)
            {
                vatAmounts.Add(new VatAmount
                {
                    vatPercent = 8,
                    vatSum = vatSumFor8Percent
                });
            }

            Root rootObject = new Root
            {
                cashier = cashier,
                cashSum = cash,
                cashlessSum = card,
                currency = "AZN",
                items = items,
               
                incomingSum = incomingSum,
                sum = total,
                vatAmounts = vatAmounts,
                prepaymentSum = 0,
                bonusSum = 0,
                changeSum = qaliq,
                creditContract = null,
                creditSum = 0,
                parents = null,
            };


           
                   
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });


            var response = RequestPOST(ipAddress, json, "kas_sale");

            if (response != null)
            {
                if (response.message == "Success operation")
                {
                    DbProsedures.InsertPosSales(new PosSales
                    {
                        posNomre = response.data.document_number.ToString(),
                        longFiskalId = response.data.document_id,
                        proccessNo = proccessNo,
                        cash = cash,
                        card = card,
                        total = total,
                        json = json,
                        shortFiskalId = response.data.short_document_id,
                        customerId = customer?.CustomerID,
                        doctorId = doctor?.Id,
                    });

                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_SALES_MESSAGE();
                    }

                    FormHelpers.Log($"Pos satışı uğurla edildi. Qəbz №: {response.data.document_number}");
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
            Login(ipAddress);
            string start = _start.ToString("yyyy-MM-ddTHH:mm:ssZ");
            string end = _end.ToString("yyyy-MM-ddTHH:mm:ssZ");

            string accessToken = Login(ipAddress);

            var zreport = new zreport
            {
                from = start,
                to = end,


            };



            string json = Newtonsoft.Json.JsonConvert.SerializeObject(zreport, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAddress, json, "kas_periodic_z");

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

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.SqlCon))
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
                         //   itemMarginPrice = Convert.ToDecimal(dr["alis_giymet"]),
                         //   itemMarginSum = Convert.ToDecimal(marginSum)
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
            decimal vatSumFor2Percent = 0;

            decimal vatSumFor8Percent = 0;


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

                else if (item.itemVatPercent == 2)
                {
                    vatSumFor2Percent += item.itemSum;
                }

                else if (item.itemVatPercent == 8)
                {
                    vatSumFor8Percent += item.itemSum;
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


            if (vatSumFor2Percent > 0)
            {
                vatAmounts.Add(new VatAmount
                {
                    vatPercent = 2,
                    vatSum = vatSumFor2Percent
                });
            }



            var root = new RootRefund
            {
                sum = _total2,
                cashSum = (payType == PayType.Cash) ? _total2 : 0,
                cashlessSum = (payType == PayType.Card) ? _card : 0,
                incomingSum = (payType == PayType.CashCard) ? _cash : 0,
                cashier = cashier,
                vatAmounts = vatAmounts,
                items = items,

                parentDocument = _fiskallID,
                //  refund_document_number = _checkNum
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            var response = RequestPOST(ipAddress, json, "kas_moneyback");



            if (response != null)
            {
                if (response.message == "Success operation")
                {


                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_RETURN_SALES_MESSAGE();
                    }

                    FormHelpers.Log($"Pos satış qaytarma uğurla edildi. Qəbz №: {response.data.document_number}");
                    return json;
                }
                else if (response.message == "document: invalid shift duration")
                {
                    XtraMessageBox.Show("GÜN SONU (Z) HESABATI ÇIXARILMAYIB !\n\nZəhmət olmasa pos bağla düyməsinə vuraraq günü sonlandırın.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return json;
                }
                else if (response.message == "document: invalid shift status")
                {
                    XtraMessageBox.Show("NÖVBƏ AÇILMAYIB !\n\nZəhmət olmasa pos aç düyməsinə vuraraq növbəni açın.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return json;
                }
                else
                {
                    ReadyMessages.ERROR_RETURN_SALES_MESSAGE(response.message);
                    FormHelpers.Log($"Pos qaytarma xətası - Xəta mesajı: {response.message}");
                    return json;
                }
            }
            else
            {
                return json;
            }
        }

        public static void LastReceiptCopy(string ipAdress, string accessToken)
        {


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

            var copyprint = new copyprint
            {

                fiscalId = fiskalID,
            };

            //RootObject root = new RootObject
            //{
            //    requestData = requestData
            //};

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(copyprint, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var response = RequestPOST(ipAdress, json, " kas_receipt_copy");

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

        public class Item
        {
            public string itemName { get; set; }
            public int itemCodeType { get; set; }
            public string itemCode { get; set; }
            public string itemMarkingCode { get; set; }
            public List<string> itemMarkingCodes { get; set; }
            public int itemQuantityType { get; set; }
            public double itemQuantity { get; set; }
            public decimal itemPrice { get; set; }
            public decimal itemSum { get; set; }
            public decimal itemVatPercent { get; set; }
           public decimal itemMarginPrice { get; set; }
            public decimal itemMarginSum { get; set; }
        }

        public class Itemticaretelave
        {
            public string itemName { get; set; }
            public int itemCodeType { get; set; }
            public string itemCode { get; set; }
            public string itemMarkingCode { get; set; }
            public List<string> itemMarkingCodes { get; set; }
            public int itemQuantityType { get; set; }
            public double itemQuantity { get; set; }
            public decimal itemPrice { get; set; }
            public decimal itemSum { get; set; }
            public decimal itemVatPercent { get; set; }
               public decimal itemMarginPrice { get; set; }
               public decimal itemMarginSum { get; set; }
        }

        public class zreport
        {
            public string from { get; set; }

            public string to { get; set; }

        }

        public class copyprint
        {
            public string fiscalId { get; set; }



        }

        public class Root
        {
            public string cashier { get; set; }
            public string creditContract { get; set; }
            public List<string> parents { get; set; }
            public string currency { get; set; }

            public List<Item> items { get; set; }

        
            public decimal sum { get; set; }
            public decimal cashSum { get; set; }
            public decimal cashlessSum { get; set; }
            public decimal prepaymentSum { get; set; }
            public decimal creditSum { get; set; }
            public decimal bonusSum { get; set; }
            public decimal incomingSum { get; set; }
            public decimal changeSum { get; set; }
            public List<VatAmount> vatAmounts { get; set; }
        }

        public class RootRefund
        {
            public string cashier { get; set; }
            public string currency { get; set; } = "AZN";
            public int moneyBackType { get; set; } = 0;

            public string creditContract { get; set; }
            public List<string> parents { get; set; }
            public string parentDocument { get; set; }
            public List<Item> items { get; set; }
            public decimal sum { get; set; }
            public decimal cashSum { get; set; }
            public decimal cashlessSum { get; set; }
            public decimal prepaymentSum { get; set; }
            public decimal creditSum { get; set; }
            public decimal bonusSum { get; set; }
            public decimal incomingSum { get; set; }
            public decimal changeSum { get; set; }
            public List<VatAmount> vatAmounts { get; set; }
        }


        public class VatSum
        {
            public decimal vatSum { get; set; }
           
        }
        public class VatAmount
        {
            public decimal vatSum { get; set; }
            public int? vatPercent { get; set; }
        }

        #endregion [..Request Classes..]


        #region [..Response Classes..]

        public class EKASAMResponse
        {
            public int code { get; set; }
            public string message { get; set; }
            public string access_token { get; set; }
            public string serial { get; set; }
            public bool shiftStatus { get; set; }
            public string desc { get; set; }
            public bool shift_open { get; set; }
            public string shift_open_time { get; set; }
            public int document_number { get; set; }
            public string long_id { get; set; }
            public string short_id { get; set; }
            public int shift_document_number { get; set; }
            public EKASAMResponseData data { get; set; }
        }

        public class EKASAMResponseData
        {
            public string createdAtUtc { get; set; }
            public string shiftOpenAtUtc { get; set; }
            public string document_id { get; set; }
            public int firstDocNumber { get; set; }
            public int lastDocNumber { get; set; }
            public int reportNumber { get; set; }
            public string access_token { get; set; }
            public bool shift_open { get; set; }
            public string shift_open_time { get; set; }

            public int document_number { get; set; }

            public string short_document_id { get; set; }


        }

        #endregion [..Response Classes..]
    }
}

