using DevExpress.Pdf.Native.BouncyCastle.Utilities.Net;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Web.UI;
using System.Windows.Forms;
using WindowsFormsApp2.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.NKA;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.Helpers.FormHelpers;
using static WindowsFormsApp2.POS_LAYOUT_NEW;

namespace WindowsFormsApp2
{
    public partial class POS_GAYTARMA_LAYOUT : DevExpress.XtraEditors.XtraForm
    {
        private readonly bool MessageVisible = SuccessMessageVisible();
        private int pagesCount;
        private int za;
        public static string keys_;
        public string customer, customervoen, obyektkod, obyetname, obyektadres, nkamodel, nkanumber, nkarnumber, returnid, bankttnmd, bankttnminputdata, gunfissayi, fissayi;
        List<string> bankdizi = new List<string>();
        List<string> bankdizic = new List<string>();
        public double tarihsoncontrol;
        public string body2a;
        string rrnCode = string.Empty;
        private readonly string Cashier;
        private PayType _payType;

        public POS_GAYTARMA_LAYOUT(string keysa_, string _cashier)
        {
            InitializeComponent();
            keys_ = keysa_;
            Cashier = _cashier;
            GridLocalizer.Active = new MyGridLocalizer();
            GridPanelText(gridView1);
        }


        private void POS_GAYTARMA_LAYOUT_Load(object sender, EventArgs e)
        {
            GetIpModel();
            textEdit1.Text = DbProsedures.GET_RefundProccessNo();


            DateTime dateTime = DateTime.UtcNow.Date;
            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit4.Text = dateTime.ToShortDateString();

            textEdit1.Enabled = false;
            if (lModel.Text == "1")
            {
                layoutControlItem4.AllowHide = true;
            }
            else if (lModel.Text == "6")
            {
                //NBA_GetInfo();
                layoutControlItem4.AllowHide = false;
            }
            else
            {
                layoutControlItem4.AllowHide = false;
            }
            if (lModel.Text is "1" || lModel.Text is "3" || lModel.Text is "5" || lModel.Text is "2" || lModel.Text is "6" || lModel.Text is "7")
            {
                layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            bankttnmcontrols();
        }

        public void GetIpModel()
        {
            var kassa = FormHelpers.GetIpModel();
            lModel.Text = kassa.Model;
            lIpAddress.Text = kassa.Ip;
            lMerchantId.Text = kassa.MerchantId;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dateEdit1.Text) || !string.IsNullOrEmpty(dateEdit4.Text))
            {
                if (lModel.Text == "1") //model
                {
                    get_date(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit4.Text));
                }
                else if (lModel.Text == "4") //model
                {
                    get_date(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit4.Text));
                }
                else if (lModel.Text == "3") //model
                {
                    get_date(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit4.Text));
                }
                else if (lModel.Text == "5") //model
                {
                    get_date(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit4.Text));
                }
                else if (lModel.Text == "6") //model
                {
                    get_date(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit4.Text));
                }
                else if (lModel.Text == "2") //model
                {
                    get_date_aZSMART(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit4.Text));
                }
                else
                {
                    get_date(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit4.Text));
                }
            }
        }

        public void get_date_aZSMART(DateTime d1, DateTime d2)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

                string queryString = "select * from  dbo.fn_pos_gaytarma_date_load (@pricePoint ,@pricePoint1) ";


                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", d1);
                command.Parameters.AddWithValue("@pricePoint1", d2);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                gridControl1.DataSource = dt1;


                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                if (lModel.Text is "2")
                {
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns[1].Visible = false;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[7].Visible = true;
                    gridView1.Columns[8].Visible = true;
                }
                else
                {
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns[7].Visible = false;
                    gridView1.Columns[8].Visible = false;
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }

        public void get_date(DateTime d1, DateTime d2)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

                string queryString = " select * from [dbo].[fn_pos_gaytarma_date_load] (@pricePoint ,@pricePoint1) ";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", d1);
                command.Parameters.AddWithValue("@pricePoint1", d2);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                gridControl1.DataSource = dt1;


                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Visible = false;
                gridView1.Columns[5].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Xəta!\n" + e);
            }
        }

        satis_json s = new satis_json();

        public void bankttnmcontrols()
        {
            if (File.Exists(Application.StartupPath + @"\BankTTNM.txt"))
            {
                string fileName = (Application.StartupPath + @"\BankTTNM.txt");

                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader sw = new StreamReader(fs);
                string data_ = sw.ReadLine();

                bankttnmd = data_;

                sw.Close();
                fs.Close();
            }
        }

        public class WeatherForecastomnitech
        {
            public string status { get; set; }
            public int code { get; set; }
            public string message { get; set; }

            public string access_token { get; set; }

            public string long_id { get; set; }
            public int document_number { get; set; }
            public string short_id { get; set; }

        }

        public class Data2
        {
            public string cashregister_factory_number { get; set; }
            public string company_tax_number { get; set; }
            public int document_number { get; set; }
            public string company_name { get; set; }
            public string object_tax_number { get; set; }
            public string object_name { get; set; }
            public string object_address { get; set; }
            public string cashbox_factory_number { get; set; }
            public string cashregister_model { get; set; }
            public string access_token { get; set; }
            public string document_id { get; set; }
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

        public class nbaroot
        {
            public Data2 data { get; set; }
            public string message { get; set; }
        }

        private bool NBA_GetInfo()
        {
            var response = NBA.GetInfo(lIpAddress.Text);
            if (response == null) { return false; }
            else
            {
                if (response.message is "Successful operation")
                {
                    customer = response.data.company_name;
                    customervoen = response.data.company_tax_number;
                    obyektkod = response.data.object_tax_number;
                    obyetname = response.data.object_name;
                    obyektadres = response.data.object_address;
                    nkamodel = response.data.cashregister_model;
                    nkanumber = response.data.cashregister_factory_number;
                    nkarnumber = response.data.cashbox_tax_number;
                    textBox4.Text = response.data.cashregister_factory_number;
                    return true;
                }
                else
                {
                    ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE($"Xəta mesajı: {response.message}");
                    return false;
                }
            }
        }

        public void POST(string Url, string Data/*, int m_id*/)
        {
            var client = new RestClient();
            var request = new RestRequest(Url, Method.Post);
            request.AddParameter("text/plain", Data, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            var data = response.Content.ToString();

            satis_return_Azsmart_gaytarma weatherForecast = System.Text.Json.JsonSerializer.Deserialize<satis_return_Azsmart_gaytarma>(data);


            if (weatherForecast.status is "success")
            {
                if (MessageVisible)
                {
                    ReadyMessages.SUCCESS_RETURN_SALES_MESSAGE();
                }

                FormHelpers.Log($"Qəbz geri qaytarması edildi Qəbz №: {weatherForecast.fiscalNum}");
                gridControl1.DataSource = null;
            }
            else
            {
                XtraMessageBox.Show($"Pos satış qaytarma xətası Xəta mesajı: {weatherForecast.message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FormHelpers.Log($"Pos satış qaytarma xətası - Xəta mesajı: {weatherForecast.message}");
            }
            textEdit1.Text = DbProsedures.GET_RefundProccessNo();
        }

        public class satis_return_Azsmart_gaytarma
        {
            public string status { get; set; }
            public string message { get; set; }
            public string fiscalNum { get; set; }
        }

        public static string sha1(string input)
        {
            using (SHA1 sha1Hash = SHA1.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                return hash.ToLower();
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public void omnitech_gaytarma(string _url, PayType type)
        {
            string json = Omnitech.Refund(_url, textBox1.Text, type, Cashier, textEdit1.Text);


            var url = _url.Replace("\n", "");

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";


            string dataheadersa4 = "";
            string p = "";
            string vatkonts2 = "";
            string edvlikisimsa = "";
            string tutkontrol = "";
            string productsa = "\"items\":[";
            string p2 = "],";

            string alldata = "";
            var dataheader = @" 
 
{" + '"' + "requestData" + '"' + ": {" +
'\u0022' + "access_token" + '\"' + ": " + '\u0022' + textBox1.Text + '\u0022' + "," +

      '\u0022' + "tokenData" + '\u0022' + ": {" +
        " \"operationId\" :  \"createDocument\"," +
              '\u0022' + "parameters" + '"' + ": {" +

             '\u0022' + "data" + '\u0022' + ": {";



            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
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


                    string cash = dr2["NEGD_"].ToString();
                    string card = dr2["KART_"].ToString();


                    string fiscal_id = dr2["fiscal_id"].ToString();
                    string fiscalNum = dr2["fiscalNum"].ToString();




                    dataheadersa4 = "\"lastOperationAtUtc\":\"\", " +
                        "\"parentDocument\":\"" + fiscal_id + "\", " +
                       " \"prepaymentSum\":0.0," +
"\"refund_document_number\":\"1\", \"refund_short_document_id\":\"" + fiscalNum + "\", ";


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
              WHERE pl.emeliyyat_nomre = '{textEdit1.Text}')";




                cmd.Connection = conn;
                cmd.CommandText = query;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr["name"].ToString();
                    string code = dr["code"].ToString();
                    string sprice = dr["satis_giymet"].ToString();
                    string qty = dr["say"].ToString();
                    string vat = dr["vtypes"].ToString();
                    string qunit = dr["quantity_type"].ToString();
                    string ssum = dr["tutar"].ToString();

                    p = p + "{\"itemName\":\"" + name + "\",\"itemCodeType\":0,\"itemCode\":\"" + code + "\",\"itemQuantityType\":" + qunit + ",\"itemQuantity\":" + qty.Replace(",", ".") + ",\"itemPrice\":" + sprice.Replace(",", ".") + ",\"itemSum\":" + ssum.Replace(",", ".") + ",\"itemVatPercent\":" + vat.Replace(",", ".") + ",\"discount\":0.0" + "},";
                    vatkonts2 = vat;
                }
                string pnew = p.Substring(0, p.Length - 1);
                SqlConnection conn4 = new SqlConnection();
                SqlCommand cmd4 = new SqlCommand();
                conn4.ConnectionString = Properties.Settings.Default.SqlCon;
                conn4.Open();
                //  string query4 = "SELECT SUM(tutar) as tut,sum(edv) as edvs from (SELECT    SUM( (M.say)*[satis_giymet]) as tutar, SUM((M.say)*[satis_giymet])*0.18 as edv FROM  [pos_satis_check_details] AS T,[pos_gaytarma_manual] AS M  WHERE T.[pos_satis_check_details_id]=M.[pos_satis_check_details] and  m.[pos_gaytarma_manual_id] =(select max([pos_gaytarma_manual_id]) from [pos_gaytarma_manual])) z";// position column from position table
                string query4 = "SELECT SUM(tutar) as tut,sum(edv) as edvs from  " +
                    "(SELECT    SUM((M.say) *[satis_giymet]) as tutar, SUM((M.say) *[satis_giymet]) * 0.18 as edv " +
                    " FROM [pos_satis_check_details] AS T,[pos_gaytarma_manual] AS M " +
                     " WHERE T.[pos_satis_check_details_id] = M.[pos_satis_check_details] and m.[pos_gaytarma_manual_id] IN(" +
                     " SELECT [pos_gaytarma_manual_id] FROM [pos_gaytarma_manual] WHERE  convert(varchar, DATE_, 20) = (" +
                     " SELECT convert(varchar, DATE_, 20)  FROM [pos_gaytarma_manual] WHERE[pos_gaytarma_manual_id] = (" +
                     " SELECT  MAX([pos_gaytarma_manual_id]) FROM [pos_gaytarma_manual])))) Z";

                cmd4.Connection = conn4;
                cmd4.CommandText = query4;

                SqlDataReader dr4 = cmd4.ExecuteReader();

                if (type is PayType.Cash)
                {
                    while (dr4.Read())
                    {


                        string tutara = dr4["tut"].ToString();
                        string edvsa = dr4["edvs"].ToString();

                        tutkontrol = $@"
                        ""bonusSum"": 0.0,
                        ""cashSum"": {tutara.Replace(",", ".")},
                        ""cashier"": Kassir,
                        ""cashlessSum"": 0.0,
                        ""creditSum"": 0.0,
                        ""currency"": ""AZN"",
                        ""firstOperationAtUtc"": """",";

                        edvlikisimsa = $@"
                        ""sum"": {tutara.Replace(",", ".")},
                        ""vatAmounts"": [
                            {{
                                ""vatPercent"": {vatkonts2},
                                ""vatSum"": {tutara.Replace(",", ".")}
                            }}
                        ]";
                    }
                }
                else if (type is PayType.Card)
                {
                    while (dr4.Read())
                    {
                        string tutara = dr4["tut"].ToString();
                        string edvsa = dr4["edvs"].ToString();

                        tutkontrol = $@"
                        ""bonusSum"": 0.0,
                        ""cashSum"": 0.0,
                        ""cashier"": Kassir,
                        ""cashlessSum"": {tutara.Replace(",", ".")},
                        ""creditSum"": 0.0,
                        ""currency"": ""AZN"",
                        ""firstOperationAtUtc"": """",";

                        edvlikisimsa = $@"
                        ""sum"": {tutara.Replace(",", ".")},
                        ""vatAmounts"": [
                            {{
                                ""vatPercent"": {vatkonts2},
                                ""vatSum"": {tutara.Replace(",", ".")}
                            }}
                        ]";

                    }
                }

                //            else if (type is PayType.CashCard)
                //            {
                //                while (dr4.Read())
                //                {


                //                    string tutara = dr4["tut"].ToString();
                //                    string edvsa = dr4["edvs"].ToString();

                //                    tutkontrol = "" +
                //                        "\"bonusSum\":0.0, " +
                //"\"cashSum\":" + tutara.Replace(",", ".") + ", " +
                //"\"cashier\":" + Cashier + ", \"cashlessSum\":0.0, \"creditSum\":0.0," +
                //"\"currency\":\"AZN\", \"firstOperationAtUtc\":\"\",";

                //                    edvlikisimsa = "\"sum\":" + tutara.Replace(",", ".") + "," +
                //                    "\"vatAmounts\":[" +
                //                    "{ \"vatPercent\":" + vatkonts2 + ", \"vatSum\": " + tutara.Replace(",", ".") + " }]},";

                //                }
                //            }


                string footernews =
            '\u0022' + "doc_type" + '\u0022' + ": " + '\u0022' + "money_back" + '\u0022' + "" +

            "}," +
             " \"version\"  : 1" +
              "}," +
            " \"checkData\" : { " +

                " \"check_type\" : 100 " +
            "}" +
 "}" +
 "}";
                alldata = dataheader + tutkontrol + productsa + pnew + p2 + dataheadersa4 + edvlikisimsa + footernews;
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json;charset=utf-8";
                httpRequest.ContentType = "application/json;charset=utf-8";


                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    WeatherForecastomnitech weatherForecast =
                     System.Text.Json.JsonSerializer.Deserialize<WeatherForecastomnitech>(result);

                    //MessageBox.Show($"{weatherForecast.message}");

                    if ($"{weatherForecast.message}" == "Successful operation" || $"{weatherForecast.message}" == "Successful operation")
                    {
                        if (MessageVisible)
                        {
                            ReadyMessages.SUCCESS_RETURN_SALES_MESSAGE();
                        }

                        FormHelpers.Log($"Qəbz geri qaytarması edildi Qəbz №: {weatherForecast.document_number}");
                        textEdit1.Text = DbProsedures.GET_RefundProccessNo();
                        gridControl1.RefreshDataSource();
                        /* textEdit3.Text = a; */

                    }
                    else
                    {
                        XtraMessageBox.Show(weatherForecast.message);
                        FormHelpers.Log($"Pos satış qaytarma xətası. Xəta mesajı: {weatherForecast.message}");
                    }
                }



            }
            catch (Exception e)
            {
                throw e;
            }

        }




        public void ekasam_gaytarma(string _url, PayType type)
        {
            string json = EKASAM.Refund(_url, textBox1.Text, type, Cashier, textEdit1.Text);


            var url = _url.Replace("\n", "");

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";


            string dataheadersa4 = "";
            string p = "";
            string vatkonts2 = "";
            string edvlikisimsa = "";
            string tutkontrol = "";
            string productsa = "\"items\":[";
            string p2 = "],";

            string alldata = "";
            var dataheader = @"{";



            //   try
            //   {
            //       SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            //       SqlConnection conn2 = new SqlConnection();
            //       SqlCommand cmd2 = new SqlCommand();
            //       conn2.ConnectionString = Properties.Settings.Default.SqlCon;
            //       conn2.Open();
            //       string query2 = "SELECT  [pos_satis_check_main_id],[pos_nomre],[fiscal_id],[date_] ,[user_id_] ," +
            //           "[emeliyyat_nomre],[NEGD_],[KART_],[UMUMI_MEBLEG] ,[json_] ,[fiscalNum],[documentID]" +
            //           "  FROM [pos_satis_check_main] WHERE[pos_satis_check_main_id] IN(SELECT[pos_satis_check_main_id]  " +
            //           " FROM [pos_gaytarma_manual] where [pos_gaytarma_manual_id] =(select max([pos_gaytarma_manual_id]) " +
            //           "from [pos_gaytarma_manual])); ";





            //       cmd2.Connection = conn2;
            //       cmd2.CommandText = query2;

            //       SqlDataReader dr2 = cmd2.ExecuteReader();

            //       while (dr2.Read())
            //       {


            //           string cash = dr2["NEGD_"].ToString();
            //           string card = dr2["KART_"].ToString();


            //           string fiscal_id = dr2["fiscal_id"].ToString();
            //           string fiscalNum = dr2["fiscalNum"].ToString();




            //           dataheadersa4 = "\"parentDocument\":\"" + fiscal_id + "\", ";



            //       }


            //       SqlConnection conn = new SqlConnection();
            //       SqlCommand cmd = new SqlCommand();
            //       conn.ConnectionString = Properties.Settings.Default.SqlCon;
            //       conn.Open();


            //       string query = $@"(SELECT md.MEHSUL_ADI AS name,
            //              p.item_id AS code,
            //              pl.say AS say,
            //              p.satis_giymet AS satis_giymet,
            //pl.say * p.satis_giymet as tutar,
            //              p.quantity_type AS quantity_type,
            //              md.VERGI_DERECESI AS vtypes
            //     FROM pos_satis_check_details p
            //              INNER JOIN MAL_ALISI_DETAILS md ON p.mal_alisi_details_id = md.MAL_ALISI_DETAILS_ID
            //              INNER JOIN pos_gaytarma_manual pl ON p.pos_satis_check_details_id = pl.pos_satis_check_details
            //     WHERE pl.emeliyyat_nomre = '{textEdit1.Text}')";




            //       cmd.Connection = conn;
            //       cmd.CommandText = query;

            //       SqlDataReader dr = cmd.ExecuteReader();
            //       while (dr.Read())
            //       {
            //           string name = dr["name"].ToString();
            //           string code = dr["code"].ToString();
            //           string sprice = dr["satis_giymet"].ToString();
            //           string qty = dr["say"].ToString();
            //           string vat = dr["vtypes"].ToString();
            //           string qunit = dr["quantity_type"].ToString();
            //           string ssum = dr["tutar"].ToString();

            //           p = p + "{\"itemName\":\"" + name + "\",\"itemCodeType\":0,\"itemCode\":\"" + code + "\",\"itemQuantityType\":" + qunit + ",\"itemQuantity\":" + qty.Replace(",", ".") + ",\"itemPrice\":" + sprice.Replace(",", ".") + ",\"itemSum\":" + ssum.Replace(",", ".") + ",\"itemVatPercent\":" + vat.Replace(",", ".") + ",\"discount\":0.0" + "},";
            //           vatkonts2 = vat;
            //       }
            //       string pnew = p.Substring(0, p.Length - 1);
            //       SqlConnection conn4 = new SqlConnection();
            //       SqlCommand cmd4 = new SqlCommand();
            //       conn4.ConnectionString = Properties.Settings.Default.SqlCon;
            //       conn4.Open();
            //       //  string query4 = "SELECT SUM(tutar) as tut,sum(edv) as edvs from (SELECT    SUM( (M.say)*[satis_giymet]) as tutar, SUM((M.say)*[satis_giymet])*0.18 as edv FROM  [pos_satis_check_details] AS T,[pos_gaytarma_manual] AS M  WHERE T.[pos_satis_check_details_id]=M.[pos_satis_check_details] and  m.[pos_gaytarma_manual_id] =(select max([pos_gaytarma_manual_id]) from [pos_gaytarma_manual])) z";// position column from position table
            //       string query4 = "SELECT SUM(tutar) as tut,sum(edv) as edvs from  " +
            //           "(SELECT    SUM((M.say) *[satis_giymet]) as tutar, SUM((M.say) *[satis_giymet]) * 0.18 as edv " +
            //           " FROM [pos_satis_check_details] AS T,[pos_gaytarma_manual] AS M " +
            //            " WHERE T.[pos_satis_check_details_id] = M.[pos_satis_check_details] and m.[pos_gaytarma_manual_id] IN(" +
            //            " SELECT [pos_gaytarma_manual_id] FROM [pos_gaytarma_manual] WHERE  convert(varchar, DATE_, 20) = (" +
            //            " SELECT convert(varchar, DATE_, 20)  FROM [pos_gaytarma_manual] WHERE[pos_gaytarma_manual_id] = (" +
            //            " SELECT  MAX([pos_gaytarma_manual_id]) FROM [pos_gaytarma_manual])))) Z";

            //       cmd4.Connection = conn4;
            //       cmd4.CommandText = query4;

            //       SqlDataReader dr4 = cmd4.ExecuteReader();

            //       if (type is PayType.Cash)
            //       {
            //           while (dr4.Read())
            //           {


            //               string tutara = dr4["tut"].ToString();
            //               string edvsa = dr4["edvs"].ToString();

            //               tutkontrol = $@"
            //               ""bonusSum"": 0.0,
            //               ""cashSum"": {tutara.Replace(",", ".")},
            //               ""cashier"": Kassir,
            //               ""cashlessSum"": 0.0,
            //               ""creditSum"": 0.0,
            //               ""currency"": ""AZN"",";


            //               edvlikisimsa = $@"
            //               ""sum"": {tutara.Replace(",", ".")},
            //               ""vatAmounts"": [
            //                   {{
            //                       ""vatPercent"": {vatkonts2},
            //                       ""vatSum"": {tutara.Replace(",", ".")}
            //                   }}
            //               ]";
            //           }
            //       }
            //       else if (type is PayType.Card)
            //       {
            //           while (dr4.Read())
            //           {
            //               string tutara = dr4["tut"].ToString();
            //               string edvsa = dr4["edvs"].ToString();

            //               tutkontrol = $@"
            //               ""bonusSum"": 0.0,
            //               ""cashSum"": 0.0,
            //               ""cashier"": Kassir,
            //               ""cashlessSum"": {tutara.Replace(",", ".")},
            //               ""creditSum"": 0.0,
            //               ""currency"": ""AZN"",";


            //               edvlikisimsa = $@"
            //               ""sum"": {tutara.Replace(",", ".")},
            //               ""vatAmounts"": [
            //                   {{
            //                       ""vatPercent"": {vatkonts2},
            //                       ""vatSum"": {tutara.Replace(",", ".")}
            //                   }}
            //               ]";

            //           }
            //       }

            //       //            else if (type is PayType.CashCard)
            //       //            {
            //       //                while (dr4.Read())
            //       //                {


            //       //                    string tutara = dr4["tut"].ToString();
            //       //                    string edvsa = dr4["edvs"].ToString();

            //       //                    tutkontrol = "" +
            //       //                        "\"bonusSum\":0.0, " +
            //       //"\"cashSum\":" + tutara.Replace(",", ".") + ", " +
            //       //"\"cashier\":" + Cashier + ", \"cashlessSum\":0.0, \"creditSum\":0.0," +
            //       //"\"currency\":\"AZN\", \"firstOperationAtUtc\":\"\",";

            //       //                    edvlikisimsa = "\"sum\":" + tutara.Replace(",", ".") + "," +
            //       //                    "\"vatAmounts\":[" +
            //       //                    "{ \"vatPercent\":" + vatkonts2 + ", \"vatSum\": " + tutara.Replace(",", ".") + " }]},";

            //       //                }
            //       //            }


            //       string footernews = "}";
            //       alldata = dataheader + tutkontrol + productsa + pnew + p2 + dataheadersa4 + edvlikisimsa + footernews;
            //       httpRequest.Method = "POST";

            //       string dt = DateTime.Now.ToString("yyyyMMddHHmmss");
            //       string nonce = Guid.NewGuid().ToString("n").Substring(0, 8);
            //       string token = EKASAM.ComputeSha256Hash(EKASAM.ComputeSha256Hash(dt) + ":" + nonce + ":" + "MPOS");


            //       var options = new RestClientOptions(_url)
            //       {
            //           MaxTimeout = -1,
            //       };
            //       var client = new RestClient(options);
            //       var request = new RestRequest("/kas_moneyback", Method.Post);
            //       request.AddHeader("dt", dt);
            //       request.AddHeader("nonce", nonce);
            //       request.AddHeader("token", token);
            //       request.AddStringBody(json, DataFormat.Json);
            //       RestResponse response = client.Execute(request);



            //       using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            //       {
            //           streamWriter.Write(json);
            //       }

            //       var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            //       using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //       {
            //           var result = streamReader.ReadToEnd();

            //           WeatherForecastomnitech weatherForecast =
            //            System.Text.Json.JsonSerializer.Deserialize<WeatherForecastomnitech>(result);

            //           //MessageBox.Show($"{weatherForecast.message}");

            //           if ($"{weatherForecast.message}" == "Success operation" || $"{weatherForecast.message}" == "Successful operation")
            //           {
            //               if (MessageVisible)
            //               {
            //                   ReadyMessages.SUCCESS_RETURN_SALES_MESSAGE();
            //               }

            //               FormHelpers.Log($"Qəbz geri qaytarması edildi Qəbz №: {weatherForecast.document_number}");
            //               textEdit1.Text = DbProsedures.GET_RefundProccessNo();
            //               gridControl1.RefreshDataSource();
            //               /* textEdit3.Text = a; */

            //           }
            //           else
            //           {
            //               XtraMessageBox.Show(weatherForecast.message);
            //               FormHelpers.Log($"Pos satış qaytarma xətası. Xəta mesajı: {weatherForecast.message}");
            //           }
            //       }



            //   }
            //   catch (Exception e)
            //   {
            //       throw e;
            //   }

        }

        string ErrorJsonSend = null;
        public void nba_gaytarma(string _url, PayType type)
        {
            rrnCode = null;
            body2a = null;
            ErrorJsonSend = null;
            var url = _url;
            if (string.IsNullOrWhiteSpace(keys_))
            {
                keys_ = NBA.Login(lIpAddress.Text, TerminalTokenData.NkaSerialNumber);
            }

            string p = "";

            string parameters = "{\"parameters\":{\"access_token\":\"" + keys_ + "\",\"doc_type\":\"money_back\",\"data\":{\"cashier\":\"" + Cashier + "\",\"currency\":\"AZN\",";
            string productsa = "\"items\":[";
            string p2 = "],";
            string dataheadersa = " ";
            string alldata = "";
            string parameters2 = "";
            string vatkonts2 = "";
            string edvlikisimsa = "";
            string tutkontrol = "";
            string tarihcontrol;
            DateTime thisDay = DateTime.Today;
            DateTime tarihcontrol2;
            TimeSpan span;

            string tarixcontrol = null;


            try
            {
                SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString);
                SqlConnection conn2 = new SqlConnection(DbHelpers.DbConnectionString);
                SqlCommand cmd2 = new SqlCommand();
                conn2.Open();
                string query2 = $@"SELECT  [pos_satis_check_main_id],
[pos_nomre],
[fiscal_id],
[date_],
[user_id_],
[emeliyyat_nomre],
[NEGD_],
[KART_],
[UMUMI_MEBLEG],
[json_],
[fiscalNum],
[documentID],
bankttnm as rrn
FROM [pos_satis_check_main] WHERE[pos_satis_check_main_id] IN(SELECT[pos_satis_check_main_id] 
FROM [pos_gaytarma_manual] where [pos_gaytarma_manual_id] =(select max([pos_gaytarma_manual_id]) 
FROM [pos_gaytarma_manual] where user_id_ = '{Properties.Settings.Default.UserID}'));";





                cmd2.Connection = conn2;
                cmd2.CommandText = query2;

                SqlDataReader dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {


                    string cash = dr2["NEGD_"].ToString();
                    string card = dr2["KART_"].ToString();


                    string fiscal_id = dr2["fiscal_id"].ToString();
                    string fiscalNum = dr2["fiscalNum"].ToString();

                    rrnCode = dr2["rrn"].ToString();


                    parameters2 = "\"parentDocument\":\"" + fiscal_id + "\", " +
                       " \"moneyBackType\":0,";

                    var datecontrol = Convert.ToDateTime(dr2["date_"]);
                    tarixcontrol = datecontrol.ToString("dd.MM.yyyy");



                    //tarihcontrol = dr2["date_"].ToString();
                    //tarihcontrol2 = Convert.ToDateTime(tarihcontrol);
                    //span = thisDay.Subtract(tarihcontrol2);
                    //tarihsoncontrol = Math.Round(Convert.ToDouble(span.TotalDays), 0);
                }


                SqlConnection conn = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                conn.ConnectionString = DbHelpers.DbConnectionString;
                conn.Open();

                //string query = $@"SELECT
                //(SELECT [MEHSUL_ADI] 
                //FROM[dbo].[MAL_ALISI_DETAILS] WHERE [MAL_ALISI_DETAILS_ID] = T.[mal_alisi_details_id]) AS name,
                //[quantity_type],[satis_giymet],[Code], M.say, (M.say) * [satis_giymet] as tutar,
                //(M.say) *[satis_giymet] * 
                //(SELECT CASE vatType 
                //WHEN 1 THEN 0.18 
                //WHEN 3 THEN 0 
                //WHEN 4 THEN 0.02
                //WHEN 5 THEN 0.08
                //ELSE 0 END 
                //FROM ITEM WHERE [mal_alisi_details_id] = T.[mal_alisi_details_id] AND user_id = {Properties.Settings.Default.UserID}) as edv,
                //(SELECT CASE vatType 
                //WHEN 1 THEN 18
                //WHEN 3 THEN 0 
                //WHEN 4 THEN 2 
                //WHEN 5 THEN 8 
                //ELSE 0 END 
                //FROM ITEM WHERE [mal_alisi_details_id] = T.[mal_alisi_details_id] AND user_id = {Properties.Settings.Default.UserID}) as vtypes FROM[pos_satis_check_details] AS T,
                //[pos_gaytarma_manual] AS M WHERE T.[pos_satis_check_details_id]= M.[pos_satis_check_details] and m.[pos_gaytarma_manual_id] IN 
                //(SELECT[pos_gaytarma_manual_id] FROM[pos_gaytarma_manual] 
                //WHERE  convert(varchar, DATE_, 20) = (SELECT convert(varchar, DATE_, 20) FROM[pos_gaytarma_manual] WHERE[pos_gaytarma_manual_id] = (SELECT  MAX([pos_gaytarma_manual_id]) FROM[pos_gaytarma_manual] WHERE user_id_ = {Properties.Settings.Default.UserID})))";
                string query = $@"(SELECT md.MEHSUL_ADI AS name,
                                p.item_id AS Code,
                                pl.say AS say,
                                md.ALIS_GIYMETI as alis_giymet,
                                p.satis_giymet AS satis_giymet,
                                pl.say * p.satis_giymet as tutar,
                                p.quantity_type AS quantity_type,
                               CASE md.VERGI_DERECESI 
								WHEN 1 THEN 0.18 
								WHEN 3 THEN 0 
								WHEN 4 THEN 0.02
								WHEN 5 THEN 0.08
								ELSE 0 END edv,
                                 case md.VERGI_DERECESI 
                                 when 1 then '18' 
                                 when 2 then '18' 
                                 when 3 then '0' 
                                 when 4 then '2' 
                                 when 5 then '8' 
                                 end as vtypes
                                FROM pos_satis_check_details p
                                INNER JOIN MAL_ALISI_DETAILS md ON p.mal_alisi_details_id = md.MAL_ALISI_DETAILS_ID
                                INNER JOIN pos_gaytarma_manual pl ON p.pos_satis_check_details_id = pl.pos_satis_check_details
                                WHERE pl.emeliyyat_nomre = '{textEdit1.Text}' and pl.user_id_ = '{Properties.Settings.Default.UserID}')";

                cmd.Connection = conn;
                cmd.CommandText = query;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {


                    string name = dr["name"].ToString();
                    string code = dr["Code"].ToString();
                    string sprice = dr["satis_giymet"].ToString();
                    string qty = dr["say"].ToString();
                    string vat = dr["vtypes"].ToString();
                    string qunit = dr["quantity_type"].ToString();
                    string ssum = dr["tutar"].ToString();

                    p = p + "{\"itemName\":\"" + name + "\",\"itemCodeType\":0,\"itemCode\":\"" + code + "\",\"itemQuantityType\":" + qunit + ",\"itemQuantity\":" + qty.Replace(",", ".") + ",\"itemPrice\":" + sprice.Replace(",", ".") + ",\"itemSum\":" + ssum.Replace(",", ".") + ",\"itemVatPercent\":" + vat.Replace(",", ".") + "},";
                    vatkonts2 = vat;
                }
                string pnew = p.Substring(0, p.Length - 1);
                string vatkonts = vatkonts2;
                SqlConnection conn4 = new SqlConnection();
                SqlCommand cmd4 = new SqlCommand();
                conn4.ConnectionString = Properties.Settings.Default.SqlCon;
                conn4.Open();
                string query4 = @"SELECT 
                SUM(tutar) as tut,
                SUM(edv) as edvs 
                FROM (SELECT SUM((M.say) *[satis_giymet]) as tutar, SUM((M.say) *[satis_giymet]) * 0.18 as edv 
                FROM [pos_satis_check_details] AS T,[pos_gaytarma_manual] AS M 
                WHERE T.[pos_satis_check_details_id] = M.[pos_satis_check_details] and m.[pos_gaytarma_manual_id] IN
                (SELECT [pos_gaytarma_manual_id] FROM [pos_gaytarma_manual] WHERE  convert(varchar, DATE_, 20) = 
                (SELECT convert(varchar, DATE_, 20)  FROM [pos_gaytarma_manual] WHERE[pos_gaytarma_manual_id] = 
                (SELECT  MAX([pos_gaytarma_manual_id]) FROM [pos_gaytarma_manual])))) Z";

                cmd4.Connection = conn4;
                cmd4.CommandText = query4;

                SqlDataReader dr4 = cmd4.ExecuteReader();

                if (type is PayType.Cash)
                {
                    while (dr4.Read())
                    {


                        string tutara = dr4["tut"].ToString();
                        string edvsa = dr4["edvs"].ToString();



                        tutkontrol = "\"sum\"  : " + tutara.Replace(",", ".") + "," +
                           "" +
                           " \"cashSum\" : " + tutara.Replace(",", ".") + "," +
                           "\"cashlessSum\": 0, " +
                           " \"prepaymentSum\" : 0.0," +
                           " \"creditSum\" : 0.0," +
                           "\"bonusSum\" : 0.0, " +
                           " \"vatAmounts\" : [" +
                              " {" +
                           " \"vatSum\" : " + tutara.Replace(",", ".") + "," +
                                   "\"vatPercent\": " + vatkonts + "" +
                              " } ] } },\"operationId\":\"createDocument\",\"version\":1 }";

                    }
                }
                else if (type is PayType.Card)
                {

                    string cardTotal = null;

                    while (dr4.Read())
                    {

                        cardTotal = dr4["tut"].ToString();
                        string edvsa = dr4["edvs"].ToString();


                        tutkontrol = "\"sum\"  : " + cardTotal.Replace(",", ".") + "," +
                           "" +
                           " \"cashSum\" :  0, " +
                           "\"cashlessSum\": " + cardTotal.Replace(",", ".") + "," +
                           " \"prepaymentSum\" : 0.0," +
                           " \"creditSum\" : 0.0," +
                           "\"bonusSum\" : 0.0, " +
                           " \"vatAmounts\" : [" +
                              " {" +
                           " \"vatSum\" : " + cardTotal.Replace(",", ".") + "," +
                                   "\"vatPercent\": " + vatkonts + "" +
                              " } ] } },\"operationId\":\"createDocument\",\"version\":1 }";

                    }



                    var client2 = new RestClient();
                    var client3 = new RestClient();

                    string url2 = url.Replace($"{NBA.NBA_FISCAL_SERVICE_PORT}/api/v1", $"{NBA.NBA_BANK_SERVICE_PORT}/transaction/start");
                    string urlbankcontrol = url.Replace($"{NBA.NBA_FISCAL_SERVICE_PORT}/api/v1", $"{NBA.NBA_BANK_SERVICE_PORT}/transaction/status");

                    var requestSend = new RestRequest(url2, Method.Post);
                    var requestbankdetail = new RestRequest(urlbankcontrol, Method.Post);

                    var requestsend = new RestRequest(url2, Method.Post);
                    string bankid;

                    requestsend.AddHeader("Accept", "application/json;charset=utf-8");
                    requestsend.AddHeader("apikey", "87903e62-9643-4e46-bb6f-3920be587332");

                    requestbankdetail.AddHeader("Accept", "application/json;charset=utf-8");
                    requestbankdetail.AddHeader("apikey", "87903e62-9643-4e46-bb6f-3920be587332");

                    //    TimeSpan span = thisDay.Subtract(tarihcontrol2);
                    string bugun = DateTime.Now.ToString("dd.MM.yyyy");
                    if (tarixcontrol == bugun)
                    {
                        var body2 = JsonConvert.SerializeObject(new
                        {
                            type = "void",
                            amount = Math.Round(Convert.ToDouble(cardTotal), 2).ToString().Replace(",", ""),
                            rrn = rrnCode,
                            dontredirecttosale = false
                        });


                        body2a = body2;

                    }
                    else
                    {

                        var body2 = JsonConvert.SerializeObject(new
                        {
                            type = "refund",
                            amount = Math.Round(Convert.ToDouble(cardTotal), 2).ToString().Replace(",", ""),
                            rrn = rrnCode,
                            dontredirecttosale = false
                        });
                        body2a = body2;
                    }
                    requestsend.AddStringBody(body2a, DataFormat.Json);

                    RestResponse response2 = client2.Execute(requestsend);

                banksalessend:
                    if (response2.Content == "ERROR!!! Please Check Banking APP")
                    {
                        XtraMessageBox.Show("ERROR!!! Please Check Banking APP", "Error");
                        goto banksalessend;
                    }
                    else
                    {
                        nbarootbank weatherForecastbank = System.Text.Json.JsonSerializer.Deserialize<nbarootbank>(response2.Content);

                        bankid = $"{weatherForecastbank.trnid}";
                    }
                    Thread.Sleep(3000);


                bankstart:
                    var bodybankdetail = "{\"trnid\":\"" + bankid + "\",\"installmentindex\":-1}";
                    requestbankdetail.AddStringBody(bodybankdetail, DataFormat.Json);
                    RestResponse responsebankdetail = client3.Execute(requestbankdetail);


                    string dataccontrolsa = responsebankdetail.Content;
                    string data2 = System.Text.RegularExpressions.Regex.Unescape(dataccontrolsa);

                    data2 = CleanJson(data2);

                    nbarootbankdetail weatherForecastbankdetail = System.Text.Json.JsonSerializer.Deserialize<nbarootbankdetail>(data2);


                    int i = 0, j = 0;

                    string statusa = $"{weatherForecastbankdetail.status}";
                    if (statusa == "approved")
                    {

                        bankdizi.Clear();
                        foreach (var item in weatherForecastbankdetail.merchantreceipt)
                        {
                            bankdizi.Add(item.line);

                            //if (item.line.Contains("RRN") || item.line.Contains("rrn"))
                            //{
                            //    rrnCode = item.line.Split(':').Last().Trim();
                            //}
                        }

                        bankdizic.Clear();
                        foreach (var item in weatherForecastbankdetail.customerreceipt)
                        {
                            bankdizic.Add(item.line);
                        }

                        #region [..XƏZİNƏDAR QƏBZİ..]

                        PrintDocument pd = new PrintDocument();
                        pd.DefaultPageSettings = new PageSettings
                        {
                            PaperSize = new PrinterSettings().DefaultPageSettings.PaperSize
                        };
                        pd.DocumentName = "Xəzinədar qəbzi";
                        pd.PrintPage += new PrintPageEventHandler(nba_bankprint);

                        pagesCount = 1;


                        PrintDialog PrintDialog1 = new PrintDialog
                        {

                            Document = pd
                        };

                        pd.Print();

                        #endregion [..XƏZİNƏDAR QƏBZİ..]


                        #region [..MÜŞTƏRİ QƏBZİ..]

                        PrintDocument pd2 = new PrintDocument();
                        pd2.DefaultPageSettings = new PageSettings
                        {
                            PaperSize = new PrinterSettings().DefaultPageSettings.PaperSize
                        };
                        pd.DocumentName = "Müştəri qəbzi";

                        pd2.PrintPage += new PrintPageEventHandler(nba_bankprintc);

                        pagesCount = 1;

                        PrintDialog printDialog2 = new PrintDialog
                        {
                            Document = pd2
                        };

                        pd2.Print();

                        #endregion [..MÜŞTƏRİ QƏBZİ..]

                    }
                    else if (statusa == "not approved")
                    {
                        #region [..XƏZİNƏDAR QƏBZİ..]

                        if (weatherForecastbankdetail.errorreceipt != null)
                        {
                            foreach (var item in weatherForecastbankdetail.errorreceipt)
                            {
                                bankdizi.Add(item.line);
                            }
                        }
                        ReadyMessages.ERROR_BANK_MESSAGE(weatherForecastbankdetail.responsecodeText);

                        PrintDocument pd = new PrintDocument();
                        pd.DefaultPageSettings = new PageSettings
                        {
                            PaperSize = new PrinterSettings().DefaultPageSettings.PaperSize
                        };

                        pd.PrintPage += new PrintPageEventHandler(nba_bankprint);

                        pagesCount = 1;


                        PrintDialog PrintDialog1 = new PrintDialog
                        {
                            Document = pd
                        };

                        pd.Print();

                        #endregion [..XƏZİNƏDAR QƏBZİ..]
                    }
                    else
                    {
                        goto bankstart;
                    }
                }




                alldata = parameters + parameters2 + productsa + pnew + p2 + tutkontrol;
                ErrorJsonSend = alldata;
                _payType = type;


                var client = new RestClient();
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                var body = alldata;
                request.AddStringBody(body, DataFormat.Json);
                RestResponse response = client.Execute(request);


                nbaroot weatherForecast = System.Text.Json.JsonSerializer.Deserialize<nbaroot>(response.Content);

                if (weatherForecast.message is "usb write error")
                {
                    ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE(weatherForecast.message);
                    return;
                }
                else if ($"{weatherForecast.message}" == "Successful operation")
                {
                    if (MessageVisible)
                    {
                        ReadyMessages.SUCCESS_RETURN_SALES_MESSAGE();
                    }
                    gunfissayi = $"{weatherForecast.data.shift_document_number}".ToString();
                    fissayi = $"{weatherForecast.data.document_number}";
                    FormHelpers.Log($"Qəbz geri qaytarması edildi");
                    textEdit1.Text = DbProsedures.GET_RefundProccessNo();
                    gridControl1.DataSource = null;
                    returnid = $"{weatherForecast.data.short_document_id}";



                    PrintDocument pd = new PrintDocument();
                    PrinterSettings settings = new PrinterSettings();
                    PageSettings pageSettings = new PageSettings(settings);
                    PaperSize paperSize = settings.DefaultPageSettings.PaperSize;

                    pd.DefaultPageSettings = new PageSettings();
                    pd.DefaultPageSettings.PaperSize = paperSize;
                    pd.PrintPage += new PrintPageEventHandler(nbaqaytarmaprint);

                    pagesCount = 1;



                    PrintDialog PrintDialog1 = new PrintDialog();


                    PrintDialog1.Document = pd;

                    pd.Print();
                }
                else
                {
                    XtraMessageBox.Show(weatherForecast.message);
                    FormHelpers.Log($"Pos satış qaytarma xətası Xəta mesajı: {weatherForecast.message}");
                }




            }
            catch (Exception e)
            {
                FormHelpers.Log($"Qaytarma zamanı göndərilən json:\n {ErrorJsonSend}");
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }

        }

        private void nba_bankprintc(object sender, PrintPageEventArgs e)
        {
            Font font4 = new System.Drawing.Font("Times New Roman", 8f, FontStyle.Regular);
            int offsetY = 10;
            int lineSpacing = 15;

            float pageWidth = e.PageBounds.Width;

            foreach (string line in bankdizic)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    if (!line.Contains(":")) // ":" simvolu olmayanları sətirlari mərkəzdə yaz
                    {
                        SizeF textSize = e.Graphics.MeasureString(line, font4);
                        float centerX = (pageWidth - textSize.Width) / 2;
                        e.Graphics.DrawString(line, font4, Brushes.Black, new PointF(centerX, offsetY));
                    }
                    else // ":" simvolu olan sətirlari solda vəya sağda yaz
                    {
                        string leftPart = line;
                        string rightPart = "";

                        // ":" simvoluna görə ayır
                        int colonIndex = line.IndexOf(':');
                        if (colonIndex != -1)
                        {
                            leftPart = line.Substring(0, colonIndex + 1).Trim(); // ":" daxil olmaqla solda yazılan
                            rightPart = line.Substring(colonIndex + 1).Trim();  // ":" sonrası sağ tərəfdə yazılan 
                        }

                        // Sol hissəni sola yazdırır
                        e.Graphics.DrawString(leftPart, font4, Brushes.Black, new PointF(10, offsetY));

                        // Sağ hissəni sağa yazdırır
                        if (!string.IsNullOrEmpty(rightPart))
                        {
                            SizeF textSize = e.Graphics.MeasureString(rightPart, font4);
                            float rightX = pageWidth - textSize.Width - 10; // Sağ tərəfdən 10 piksel boşluq üçün
                            e.Graphics.DrawString(rightPart, font4, Brushes.Black, new PointF(rightX, offsetY));
                        }
                    }

                    offsetY += lineSpacing;
                }
            }
        }

        private void nba_bankprint(object sender, PrintPageEventArgs e)
        {
            Font font4 = new System.Drawing.Font("Times New Roman", 8f, FontStyle.Regular);
            int offsetY = 10;
            int lineSpacing = 15;

            float pageWidth = e.PageBounds.Width;

            foreach (string line in bankdizi)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    if (!line.Contains(":")) // ":" simvolu olmayanları sətirlari mərkəzdə yaz
                    {
                        SizeF textSize = e.Graphics.MeasureString(line, font4);
                        float centerX = (pageWidth - textSize.Width) / 2;
                        e.Graphics.DrawString(line, font4, Brushes.Black, new PointF(centerX, offsetY));
                    }
                    else // ":" simvolu olan sətirlari solda vəya sağda yaz
                    {
                        string leftPart = line;
                        string rightPart = "";

                        // ":" simvoluna görə ayır
                        int colonIndex = line.IndexOf(':');
                        if (colonIndex != -1)
                        {
                            leftPart = line.Substring(0, colonIndex + 1).Trim(); // ":" daxil olmaqla solda yazılan
                            rightPart = line.Substring(colonIndex + 1).Trim();  // ":" sonrası sağ tərəfdə yazılan 
                        }

                        // Sol hissəni sola yazdırır
                        e.Graphics.DrawString(leftPart, font4, Brushes.Black, new PointF(10, offsetY));

                        // Sağ hissəni sağa yazdırır
                        if (!string.IsNullOrEmpty(rightPart))
                        {
                            SizeF textSize = e.Graphics.MeasureString(rightPart, font4);
                            float rightX = pageWidth - textSize.Width - 10; // Sağ tərəfdən 10 piksel boşluq üçün
                            e.Graphics.DrawString(rightPart, font4, Brushes.Black, new PointF(rightX, offsetY));
                        }
                    }

                    offsetY += lineSpacing;
                }
            }
        }

        public void datapay_gaytarma(string _url, PayType type)
        {
            //var url = _url;
            var url = _url + "/api/salereturn/create";


            string dataheadersa4 = "";
            string p = "";
            string vatkonts2 = "";
            string edvlikisimsa = "";
            string tutkontrol = "";
            string productsa = "\"items\":[";
            string p2 = "],";

            string alldata = "";
            var dataheader = @" 
 
{" + '"' + "requestData" + '"' + ": {" +
'\u0022' + "access_token" + '\"' + ": " + '\u0022' + textBox1.Text + '\u0022' + "," +

      '\u0022' + "tokenData" + '\u0022' + ": {" +
        " \"operationId\" :  \"createDocument\"," +
              '\u0022' + "parameters" + '"' + ": {" +

             '\u0022' + "data" + '\u0022' + ": {";



            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                SqlConnection conn2 = new SqlConnection();
                SqlCommand cmd2 = new SqlCommand();
                conn2.ConnectionString = Properties.Settings.Default.SqlCon;
                conn2.Open();
                string query2 = "SELECT  [pos_satis_check_main_id],[pos_nomre],[fiscal_id],[date_] ,[user_id_] ," +
                    "[emeliyyat_nomre],[NEGD_],[KART_],[UMUMI_MEBLEG] ,[json_] ,[fiscalNum],[documentID]" +
                    "  FROM [pos_satis_check_main] WHERE[pos_satis_check_main_id] IN(SELECT[pos_satis_check_main_id]  " +
                    " FROM [pos_gaytarma_manual] where [pos_gaytarma_manual_id] =(select max([pos_gaytarma_manual_id]) " +
                    "from [pos_gaytarma_manual])); ";// position column from position table





                cmd2.Connection = conn2;
                cmd2.CommandText = query2;

                SqlDataReader dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {


                    string cash = dr2["NEGD_"].ToString();
                    string card = dr2["KART_"].ToString();


                    string fiscal_id = dr2["fiscal_id"].ToString();
                    string fiscalNum = dr2["fiscalNum"].ToString();




                    dataheadersa4 = " {\"parentDocumentId\":\"" + fiscal_id + "\", ";



                }


                SqlConnection conn = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                conn.ConnectionString = Properties.Settings.Default.SqlCon;
                conn.Open();
                //  string query = "SELECT     (SELECT   [name]    " +
                //      "  FROM [dbo].[Item] where [code]=T.[code]) AS name,  [quantity_type]    ,[satis_giymet],[Code], M.say," +
                //     " (M.say)*[satis_giymet] as tutar, (M.say)*[satis_giymet]*" +
                //     "(SELECT CASE vatType WHEN 1 THEN 0.18  WHEN 3 THEN 0 WHEN 4 THEN 0.02 WHEN 5 THEN 0.08 ELSE 0 END FROM ITEM where code=T.Code)" +
                //     " as edv,(SELECT CASE vatType WHEN 1 THEN 18  WHEN 3 THEN 0 WHEN 4 THEN 2 WHEN 5 THEN 8 ELSE 0 END FROM ITEM where code=T.Code) " +
                //    "as vtypes FROM  [pos_satis_check_details] AS T,[pos_gaytarma_manual] AS M  WHERE T.[pos_satis_check_details_id]=M.[pos_satis_check_details]" +
                //    " and m.[pos_gaytarma_manual_id] =(select max([pos_gaytarma_manual_id]) from [pos_gaytarma_manual]);";// position column from position table


                string query = "SELECT(SELECT [MEHSUL_ADI]      FROM[dbo].[MAL_ALISI_DETAILS] where [MAL_ALISI_DETAILS_ID] = T.[mal_alisi_details_id]) AS name," +
  "[quantity_type],[satis_giymet],[Code], M.say  , (M.say) *[satis_giymet] as tutar,(SELECT vatType  FROM ITEM where [mal_alisi_details_id] = T.[mal_alisi_details_id]) as v2," +
  "(M.say) *[satis_giymet] * " +
  "(SELECT CASE vatType WHEN 1 THEN 0.18  WHEN 3 THEN 0 WHEN 4 THEN 0.02 WHEN 5 THEN 0.08 ELSE 0 END FROM ITEM where [mal_alisi_details_id] = T.[mal_alisi_details_id]) as edv," +
  "(SELECT CASE vatType WHEN 1 THEN 18  WHEN 3 THEN 0 WHEN 4 THEN 2 WHEN 5 THEN 8 ELSE 0 END FROM ITEM where [mal_alisi_details_id] = T.[mal_alisi_details_id]) as vtypes FROM[pos_satis_check_details] AS T," +
  "[pos_gaytarma_manual] AS M WHERE T.[pos_satis_check_details_id]= M.[pos_satis_check_details] and m.[pos_gaytarma_manual_id] IN(SELECT[pos_gaytarma_manual_id] FROM[pos_gaytarma_manual] WHERE  convert(varchar, DATE_, 20) = " +
  "(SELECT convert(varchar, DATE_, 20)  FROM[pos_gaytarma_manual] WHERE[pos_gaytarma_manual_id] = (SELECT  MAX([pos_gaytarma_manual_id]) FROM[pos_gaytarma_manual])))";


                cmd.Connection = conn;
                cmd.CommandText = query;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {


                    string name = dr["name"].ToString();
                    string code = dr["Code"].ToString();
                    string sprice = dr["satis_giymet"].ToString();
                    string qty = dr["say"].ToString();
                    string vat = dr["vtypes"].ToString();
                    string qunit = dr["quantity_type"].ToString();
                    string ssum = dr["tutar"].ToString();
                    string vsum = dr["edv"].ToString();
                    string vat2 = dr["v2"].ToString();

                    //  p = p + "{\"itemName\":\"" + name + "\",\"itemCodeType\":0,\"itemCode\":\"" + code + "\",\"itemQuantityType\":" + qunit + ",\"itemQuantity\":" + qty.Replace(",", ".") + ",\"itemPrice\":" + sprice.Replace(",", ".") + ",\"itemSum\":" + ssum.Replace(",", ".") + ",\"itemVatPercent\":" + vat.Replace(",", ".") + ",\"discount\":0.0" + "},";
                    vatkonts2 = vat;

                    p = p + "{\"name\":\"" + name + "\",\"code\":\"" + code + "\",\"quantity\":" + qty.Replace(",", ".") + ",\"salePrice\":" + sprice.Replace(",", ".") + ",\"purchasePrice\":" + sprice.Replace(",", ".") + ",\"sum\":" + ssum.Replace(",", ".") + ",\"vatSum\":" + ssum.Replace(",", ".") + ",\"vatType\":" + vat2 + ",\"unitType\":" + qunit + ",\"codeType\":1" + "},";

                }
                string pnew = p.Substring(0, p.Length - 1);
                string vatkonts = vatkonts2;
                SqlConnection conn4 = new SqlConnection();
                SqlCommand cmd4 = new SqlCommand();
                conn4.ConnectionString = Properties.Settings.Default.SqlCon;
                conn4.Open();
                //  string query4 = "SELECT SUM(tutar) as tut,sum(edv) as edvs from (SELECT    SUM( (M.say)*[satis_giymet]) as tutar, SUM((M.say)*[satis_giymet])*0.18 as edv FROM  [pos_satis_check_details] AS T,[pos_gaytarma_manual] AS M  WHERE T.[pos_satis_check_details_id]=M.[pos_satis_check_details] and  m.[pos_gaytarma_manual_id] =(select max([pos_gaytarma_manual_id]) from [pos_gaytarma_manual])) z";// position column from position table
                string query4 = "SELECT SUM(tutar) as tut,sum(edv) as edvs from  " +
                    "(SELECT    SUM((M.say) *[satis_giymet]) as tutar, SUM((M.say) *[satis_giymet]) * 0.18 as edv " +
                    " FROM [pos_satis_check_details] AS T,[pos_gaytarma_manual] AS M " +
                     " WHERE T.[pos_satis_check_details_id] = M.[pos_satis_check_details] and m.[pos_gaytarma_manual_id] IN(" +
                     " SELECT [pos_gaytarma_manual_id] FROM [pos_gaytarma_manual] WHERE  convert(varchar, DATE_, 20) = (" +
                     " SELECT convert(varchar, DATE_, 20)  FROM [pos_gaytarma_manual] WHERE[pos_gaytarma_manual_id] = (" +
                     " SELECT  MAX([pos_gaytarma_manual_id]) FROM [pos_gaytarma_manual])))) Z";

                cmd4.Connection = conn4;
                cmd4.CommandText = query4;
                string productsa2 = "\"items\":[";
                string dataheadersa = "";
                SqlDataReader dr4 = cmd4.ExecuteReader();


                if (type is PayType.Cash)
                {
                    while (dr4.Read())
                    {


                        string tutara = dr4["tut"].ToString();
                        string edvsa = dr4["edvs"].ToString();


                        dataheadersa = "\"sum\"  : " + tutara.Replace(",", ".") + "," +
                           '\u0022' + "cashier" + '\u0022' + ": " + '\u0022' + Cashier + '\u0022' + "," +
                           " \"cashSum\" : " + tutara.Replace(",", ".") + "," +
                           "\"cashlessSum\": 0," +
                            " \"cardSum\" : 0.0," +
                           " \"prepaymentSum\" : 0.0," +
                            "\"bonusSum\" : 0.0," +
                           " \"creditSum\" : 0.0," +
                            " \"vatSum\" : " + tutara.Replace(",", ".") + "," +
                            " \"vatFreeSum\" : 0.0," +
                             " \"vat\" : 2," +
                           "  \"paidSum\"  : " + tutara.Replace(",", ".") + "," +
                           " \"restSum\" : 0,";
                    }
                }
                else if (type is PayType.Card)
                {
                    while (dr4.Read())
                    {


                        string tutara = dr4["tut"].ToString();
                        string edvsa = dr4["edvs"].ToString();


                        dataheadersa = "\"sum\"  : " + tutara.Replace(",", ".") + "," +
                           '\u0022' + "cashier" + '\u0022' + ": " + '\u0022' + Cashier + '\u0022' + "," +
                           " \"cashSum\" : 0," +
                           "\"cashlessSum\": " + tutara.Replace(",", ".") + "," +
                            " \"cardSum\" : " + tutara.Replace(",", ".") + "," +
                           " \"prepaymentSum\" : 0.0," +
                            "\"bonusSum\" : 0.0," +
                           " \"creditSum\" : 0.0," +
                            " \"vatSum\" : " + tutara.Replace(",", ".") + "," +
                            " \"vatFreeSum\" : 0.0," +
                             " \"vat\" : 2," +
                           "  \"paidSum\"  : " + tutara.Replace(",", ".") + "," +
                           " \"restSum\" : 0,";
                    }
                }

                string footernews = " ] }";

                alldata = dataheadersa4 + dataheadersa + productsa2 + pnew + footernews;

                var clients = new RestClient();


                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Authorization", keys_);
                var body = alldata;

                request.AddParameter("text/plain", body, ParameterType.RequestBody);

                RestResponse response = clients.Execute(request);
                var data = response.Content.ToString();

                WeatherForecastomnitech weatherForecast =
                 System.Text.Json.JsonSerializer.Deserialize<WeatherForecastomnitech>(data);

                if (MessageVisible)
                {
                    ReadyMessages.SUCCESS_RETURN_SALES_MESSAGE();
                }


                FormHelpers.Log($"Qəbz geri qaytarması edildi Qəbz №: {weatherForecast.document_number}");

                textEdit1.Text = DbProsedures.GET_RefundProccessNo();
                gridControl1.DataSource = null;
            }
            catch (Exception e)
            {
                XtraMessageBox.Show("Xəta!\n" + e);
                FormHelpers.Log($"Pos satış qaytarma xətası\n Xəta mesajı: {e.Message}");
            }

        }

        public void xprinter_gaytarma()
        {
            PrintTest();
            textEdit1.Text = DbProsedures.GET_RefundProccessNo();
            gridControl1.RefreshDataSource();
        }

        private void PrintTest()
        {
            PrintDocument pd = new PrintDocument();
            PrinterSettings settings = new PrinterSettings();
            PageSettings pageSettings = new PageSettings(settings);
            PaperSize paperSize = settings.DefaultPageSettings.PaperSize;


            //pd.PrinterSettings = settings;
            pd.DefaultPageSettings = new PageSettings();
            pd.DefaultPageSettings.PaperSize = paperSize;
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage2);

            pagesCount = 1;



            PrintDialog PrintDialog1 = new PrintDialog();


            PrintDialog1.Document = pd;

            DialogResult result = PrintDialog1.ShowDialog();
            pd.Print();
        }

        void pd_PrintPage2(object sender, PrintPageEventArgs e)
        {
            Font font = new System.Drawing.Font("Times New Roman", 7f);
            Font font2 = new System.Drawing.Font("Times New Roman", 8.75f, FontStyle.Bold);
            Font font4 = new System.Drawing.Font("Times New Roman", 10f, FontStyle.Bold);
            Font font3 = new System.Drawing.Font("Times New Roman", 8.75f);
            Font company = new System.Drawing.Font("Times New Roman", 10f);
            Font f8 = new System.Drawing.Font("Times New Roman", 7.5f, FontStyle.Bold);
            Font f9 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Bold);
            string tutar = "";
            string nagd = "";
            string kart = "";


            int offset = 170;
            int offset2 = 10;
            int m = 0;
            int n = 20;


            string ssuma46 = "";
            SqlConnection conn4 = new SqlConnection();
            SqlCommand cmd4 = new SqlCommand();
            conn4.ConnectionString = Properties.Settings.Default.SqlCon;
            conn4.Open();
            string query4 = "SELECT round(SUM(tutar),2) as tut262  from (SELECT    SUM( (M.say)*[satis_giymet]) as tutar, SUM((M.say)*[satis_giymet])*0.18 as edv FROM  [pos_satis_check_details] AS T,[pos_gaytarma_manual] AS M  WHERE T.[pos_satis_check_details_id]=M.[pos_satis_check_details] and  m.[pos_gaytarma_manual_id] > " + textBox2.Text + ") z";

            cmd4.Connection = conn4;
            cmd4.CommandText = query4;

            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {

                string ssuma2 = dr4["tut262"].ToString();
                ssuma46 = ssuma2;
            }

            conn4.Close();


            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlConnection conn2 = new SqlConnection();
            SqlCommand cmd2 = new SqlCommand();
            conn2.ConnectionString = Properties.Settings.Default.SqlCon;
            conn2.Open();
            string query2 = "SELECT  [pos_satis_check_main_id],[pos_nomre],[fiscal_id],[date_] ,[user_id_] ,[emeliyyat_nomre],[NEGD_] as nagd,[KART_] as kart,[UMUMI_MEBLEG] as odeme ,[json_] ,[fiscalNum],[documentID],(SELECT TOP 1   [COMPANY_NAME]    FROM [COMPANY].[COMPANY]) AS sirket,(SELECT TOP 1   [SIRKET_VOEN]    FROM [COMPANY].[COMPANY]) as voen  FROM [pos_satis_check_main] WHERE[pos_satis_check_main_id] IN(SELECT[pos_satis_check_main_id]   FROM [pos_gaytarma_manual] where [pos_gaytarma_manual_id] =(select max([pos_gaytarma_manual_id]) from [pos_gaytarma_manual]))";// position column from position table

            cmd2.Connection = conn2;
            cmd2.CommandText = query2;

            SqlDataReader dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {


                string cash = dr2["nagd"].ToString();
                string card = dr2["kart"].ToString();

                string tot = dr2["odeme"].ToString();
                string sirket = dr2["sirket"].ToString();
                string voen = dr2["voen"].ToString();



                e.Graphics.DrawString("  " + sirket, company, Brushes.Black, new Point(90, offset2));
                e.Graphics.DrawString("VÖEN :" + voen, font3, Brushes.Black, new Point(85, offset2 + 15));

                e.Graphics.DrawString("Geri Qaytarma Çeki", font4, Brushes.Black, new Point(95, offset2 + 40));
                e.Graphics.DrawString("Çek nömrəsi No:" + textEdit1.Text, font2, Brushes.Black, new Point(70, offset2 + 55));
                e.Graphics.DrawString("Kassir: " + Cashier, font, Brushes.Black, new Point(5, offset2 + 80));
                e.Graphics.DrawString("Tarix: " + DateTime.Now.ToString("dd MM yyyy"), font, Brushes.Black, new Point(190, offset2 + 80));
                e.Graphics.DrawString("Saat: " + DateTime.Now.ToString("HH:mm"), font, Brushes.Black, new Point(190, offset2 + 95));

                e.Graphics.DrawString("*****************************************************", font2, Brushes.Black, new Point(5, offset2 + 110));
                e.Graphics.DrawString("Malın adı", f8, Brushes.Black, 5, offset2 + 120);
                e.Graphics.DrawString("Miqdar", f8, Brushes.Black, 130, offset2 + 120);
                e.Graphics.DrawString("Qiymət", f8, Brushes.Black, 190, offset2 + 120);
                e.Graphics.DrawString("Toplam", f8, Brushes.Black, 240, offset2 + 120);
                e.Graphics.DrawString("_____________________________________________________", font2, Brushes.Black, new Point(5, offset2 + 130));

                tutar = tot;
                kart = card;
                nagd = cash;

            }





            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.SqlCon;
            conn.Open();
            string query = "SELECT    (SELECT    MEHSUL_ADI      FROM [dbo].[MAL_ALISI_DETAILS] where MAL_ALISI_DETAILS_ID=T.mal_alisi_details_id) AS name,  [quantity_type]    ,[satis_giymet] as spr,[Code]    ,M.say  , (M.say)*[satis_giymet] as tutara" +
                "" +
                " FROM  [pos_satis_check_details] AS T,[pos_gaytarma_manual] AS M  WHERE [pos_gaytarma_manual_id]>" + textBox2.Text + "  and   T.[pos_satis_check_details_id]=M.[pos_satis_check_details] ";// position column from position table

            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {


                string name = dr["name"].ToString();

                string sprice = dr["spr"].ToString();
                string qty = dr["say"].ToString();

                string ssum = dr["tutara"].ToString();



                e.Graphics.DrawString(name, font, Brushes.Black, new Point(5, offset + m));
                e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(qty)), font, Brushes.Black, new Point(130, offset + n));
                e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(sprice)), font, Brushes.Black, new Point(190, offset + n));
                e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(ssum)), font, Brushes.Black, new Point(240, offset + n));

                offset += 20;
                m += 20;
                n += 20;

            }




            za++;
            e.HasMorePages = za < pagesCount;

            e.Graphics.DrawString("_____________________________________________________", font2, Brushes.Black, new Point(5, offset + m));
            e.Graphics.DrawString("YEKUN MƏBLƏĞ:", f9, Brushes.Black, 5, offset + m + 20);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(ssuma46)), f9, Brushes.Black, new Point(240, offset + m + 20));
            e.Graphics.DrawString("_____________________________________________________", font2, Brushes.Black, new Point(5, offset + m + 30));
            e.Graphics.DrawString("*****************************************************", font2, Brushes.Black, new Point(5, offset + m + 50));
            e.Graphics.DrawString("Ödəniş növü", f8, Brushes.Black, 5, offset + m + 65);
            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 5, offset + m + 80);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(kart)), f8, Brushes.Black, 240, offset + m + 80);
            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 5, offset + m + 90);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(ssuma46)), f8, Brushes.Black, 240, offset + m + 90);
            e.Graphics.DrawString("Ödənilib nağd:", f8, Brushes.Black, 5, offset + m + 100);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(ssuma46)), f8, Brushes.Black, 240, offset + m + 100);
            e.Graphics.DrawString("Qalıq qaytarılıb nağd:", f8, Brushes.Black, 5, offset + m + 110);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(0.00)), f8, Brushes.Black, 240, offset + m + 110);
        }

        void nbaqaytarmaprint(object sender, PrintPageEventArgs e)
        {
            Font font = new System.Drawing.Font("Times New Roman", 7f);
            Font font5f = new System.Drawing.Font("Times New Roman", 5f);
            Font font2 = new System.Drawing.Font("Times New Roman", 8.75f, FontStyle.Bold);
            Font font4 = new System.Drawing.Font("Times New Roman", 10f, FontStyle.Bold);
            Font font3 = new System.Drawing.Font("Times New Roman", 8.75f);
            Font f8 = new System.Drawing.Font("Times New Roman", 7.5f, FontStyle.Bold);
            Font f9 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Bold);
            Font fonta = new System.Drawing.Font("Times New Roman", 8.5f);
            string tutar = "";
            string nagd = "";
            string kart = "";
            string snos = "";
            int offset = 230;
            int offset2 = 10;
            int m = 0;
            int n = 20;




            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            Rectangle rect2 = new Rectangle(70, 10, 100, 122);

            string ustbaslik = "TS Adı :" + obyetname + "\r\n" + "TS Ünvanı :" + obyektadres + "\r\n" + "\r\n" + "VÖ Adı :" + customer + "\r\n" + "VÖEN :" + customervoen + "\r\n" + "Obyektin kodu :" + obyektkod;



            string ssuma46 = "";
            SqlConnection conn4 = new SqlConnection();
            SqlCommand cmd4 = new SqlCommand();
            conn4.ConnectionString = Properties.Settings.Default.SqlCon;
            conn4.Open();
            string query4 = "SELECT round(SUM(tutar),2) as tut262  from (SELECT    SUM( (M.say)*[satis_giymet]) as tutar, SUM((M.say)*[satis_giymet])*0.18 as edv FROM  [pos_satis_check_details] AS T,[pos_gaytarma_manual] AS M  WHERE T.[pos_satis_check_details_id]=M.[pos_satis_check_details] and  m.[pos_gaytarma_manual_id] >= " + textBox2.Text + ") z";

            cmd4.Connection = conn4;
            cmd4.CommandText = query4;

            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {

                string ssuma2 = dr4["tut262"].ToString();
                ssuma46 = ssuma2;
            }

            conn4.Close();


            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlConnection conn2 = new SqlConnection();
            SqlCommand cmd2 = new SqlCommand();
            conn2.ConnectionString = Properties.Settings.Default.SqlCon;
            conn2.Open();
            string query2 = "SELECT  [pos_satis_check_main_id],[pos_nomre],[fiscal_id],[date_] ,[user_id_] ,[emeliyyat_nomre],[NEGD_] as nagd,[KART_] as kart,[UMUMI_MEBLEG] as odeme ,[json_] ,[fiscalNum],[documentID],(SELECT TOP 1   [COMPANY_NAME]    FROM [COMPANY].[COMPANY]) AS sirket,(SELECT TOP 1   [SIRKET_VOEN]    FROM [COMPANY].[COMPANY]) as voen  FROM [pos_satis_check_main] WHERE[pos_satis_check_main_id] IN(SELECT[pos_satis_check_main_id]   FROM [pos_gaytarma_manual] where [pos_gaytarma_manual_id] =(select max([pos_gaytarma_manual_id]) from [pos_gaytarma_manual]))";

            cmd2.Connection = conn2;
            cmd2.CommandText = query2;

            SqlDataReader dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {


                string cash = dr2["nagd"].ToString();
                string card = dr2["kart"].ToString();

                string tot = dr2["odeme"].ToString();
                string sirket = dr2["sirket"].ToString();
                string voen = dr2["voen"].ToString();
                string fisida = dr2["fiscal_id"].ToString();
                snos = fisida;

                e.Graphics.DrawString(ustbaslik, fonta, Brushes.Black, new RectangleF(50F, 10F, 200F, 90F), sf);

                e.Graphics.DrawString("Geri Qaytarma Çeki", font4, Brushes.Black, new Point(90, offset2 + 105));

                e.Graphics.DrawString("Çek nömrəsi No:" + fissayi, font2, Brushes.Black, new Point(80, offset2 + 120));
                e.Graphics.DrawString($"Kassir: {Cashier}", font, Brushes.Black, new Point(5, offset2 + 135));
                e.Graphics.DrawString("Tarix: " + DateTime.Now.ToString("dd.MM.yyyy"), font, Brushes.Black, new Point(190, offset2 + 135));
                e.Graphics.DrawString("Saat: " + DateTime.Now.ToString("HH:mm"), font, Brushes.Black, new Point(190, offset2 + 150));

                e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 165));
                e.Graphics.DrawString("Satış Çekinin Fiskal ID:" + fisida, font5f, Brushes.Black, new Point(5, offset2 + 175));

                e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 190));
                e.Graphics.DrawString("Malın adı", f8, Brushes.Black, 5, offset2 + 200);
                e.Graphics.DrawString("Miqdar", f8, Brushes.Black, 130, offset2 + 200);
                e.Graphics.DrawString("Qiymət", f8, Brushes.Black, 190, offset2 + 200);
                e.Graphics.DrawString("Toplam", f8, Brushes.Black, 240, offset2 + 200);
                e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset2 + 206));

                tutar = tot;
                kart = card;
                nagd = cash;
            }




            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            var qrcodeimg = barcode.Draw("https://monitoring.e-kassa.gov.az/#/index?doc=" + returnid, 0, scale: 3);
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.SqlCon;
            conn.Open();
            string query = "SELECT    (SELECT    MEHSUL_ADI      FROM [dbo].[MAL_ALISI_DETAILS] where MAL_ALISI_DETAILS_ID=T.mal_alisi_details_id) AS name,  [quantity_type]    ,[satis_giymet] as spr,[Code]    ,M.say  , (M.say)*[satis_giymet] as tutara" +
                "" +
                " FROM  [pos_satis_check_details] AS T,[pos_gaytarma_manual] AS M  WHERE [pos_gaytarma_manual_id]>=" + textBox2.Text + "  and   T.[pos_satis_check_details_id]=M.[pos_satis_check_details] ";// position column from position table

            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {


                string name = dr["name"].ToString();

                string sprice = dr["spr"].ToString();
                string qty = dr["say"].ToString();

                string ssum = dr["tutara"].ToString();



                e.Graphics.DrawString(name, font, Brushes.Black, new Point(5, offset + m));
                e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(qty)), font, Brushes.Black, new Point(130, offset + n));
                e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(sprice)), font, Brushes.Black, new Point(190, offset + n));
                e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(ssum)), font, Brushes.Black, new Point(240, offset + n));

                offset += 20;
                m += 20;
                n += 20;

            }




            za++;
            e.HasMorePages = za < pagesCount;



            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset + m));
            e.Graphics.DrawString("YEKUN MƏBLƏĞ", f9, Brushes.Black, 5, offset + m + 20);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(ssuma46)), f9, Brushes.Black, new Point(240, offset + m + 20));
            e.Graphics.DrawString("YEKUN ƏDV", f9, Brushes.Black, 5, offset + m + 30);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(ssuma46) * 18 / 118), f9, Brushes.Black, new Point(240, offset + m + 30));

            e.Graphics.DrawString("ƏDV-dən azad", font, Brushes.Black, 5, offset + m + 40);
            e.Graphics.DrawString("0", font, Brushes.Black, new Point(240, offset + m + 40));

            e.Graphics.DrawString("ƏDV %18 =" + String.Format("{0:0.00}", Convert.ToDouble(ssuma46) * 18 / 118), font, Brushes.Black, 5, offset + m + 50);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(ssuma46)), font, Brushes.Black, new Point(240, offset + m + 50));


            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset + m + 60));
            e.Graphics.DrawString("Ödəniş üsulu", f8, Brushes.Black, 5, offset + m + 70);
            if (_payType is PayType.Cash)
            {
                e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 5, offset + m + 80);
                e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(ssuma46)), f8, Brushes.Black, 240, offset + m + 80);
                e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 5, offset + m + 90);
                e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset + m + 90);
            }
            else if (_payType is PayType.Card)
            {
                e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 5, offset + m + 80);
                e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset + m + 80);
                e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 5, offset + m + 90);
                e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(ssuma46)), f8, Brushes.Black, 240, offset + m + 90);
            }
            //e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 5, offset + m + 80);
            //e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(ssuma46)), f8, Brushes.Black, 240, offset + m + 80);
            //e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 5, offset + m + 90);
            //e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset + m + 90);
            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 5, offset + m + 100);
            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset + m + 100);
            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset + m + 110));

            e.Graphics.DrawString("Növbə ərzində vurulmuş çek sayı:" + gunfissayi, font, Brushes.Black, 5, offset + m + 125);
            e.Graphics.DrawString("NKA-nın modeli:" + nkamodel, font, Brushes.Black, 5, offset + m + 135);
            e.Graphics.DrawString("NKA-nın zavod nömrəsi:" + nkanumber, font, Brushes.Black, 5, offset + m + 150);
            e.Graphics.DrawString("NMQ-nın qeydiyyat nömrəsi:" + nkarnumber, font, Brushes.Black, 5, offset + m + 170);
            e.Graphics.DrawString("Fiskal İD:" + returnid, font, Brushes.Black, 5, offset + m + 180);

            e.Graphics.DrawString("www.e-kassa.gov.az", font, Brushes.Black, 90, offset + m + 200);

            e.Graphics.DrawImage(qrcodeimg, 90, offset + m + 215, width: 85, height: 85);
        }

        void nbaqaytarmaprint2(object sender, PrintPageEventArgs e)
        {
            Font font = new System.Drawing.Font("Times New Roman", 7f);
            Font font2 = new System.Drawing.Font("Times New Roman", 8.75f, FontStyle.Bold);
            Font font4 = new System.Drawing.Font("Times New Roman", 10f, FontStyle.Bold);
            Font font3 = new System.Drawing.Font("Times New Roman", 8.75f);
            Font f8 = new System.Drawing.Font("Times New Roman", 7.5f, FontStyle.Bold);
            Font f9 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Bold);
            Font fonta = new System.Drawing.Font("Times New Roman", 8.5f);
            string tutar = "";
            string nagd = "";
            string kart = "";
            string snos = "";
            int offset = 210;
            int offset2 = 10;
            int m = 0;
            int n = 20;




            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            Rectangle rect2 = new Rectangle(70, 10, 100, 122);

            string ustbaslik = "TS Adı :" + obyetname + "\r\n" + "TS Unvanı :" + obyektadres + "\r\n" + "\r\n" + "VÖ Adı :" + customer + "\r\n" + "VÖEN :" + customervoen + "\r\n" + "Obyektin kodu :" + obyektkod;



            string ssuma46 = "";
            SqlConnection conn4 = new SqlConnection();
            SqlCommand cmd4 = new SqlCommand();
            conn4.ConnectionString = Properties.Settings.Default.SqlCon;
            conn4.Open();
            string query4 = "SELECT round(SUM(tutar),2) as tut262  from (SELECT    SUM( (M.say)*[satis_giymet]) as tutar, SUM((M.say)*[satis_giymet])*0.18 as edv FROM  [pos_satis_check_details] AS T,[pos_gaytarma_manual] AS M  WHERE T.[pos_satis_check_details_id]=M.[pos_satis_check_details] and  m.[pos_gaytarma_manual_id] > " + textBox2.Text + ") z";// position column from position table

            cmd4.Connection = conn4;
            cmd4.CommandText = query4;

            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {

                string ssuma2 = dr4["tut262"].ToString();
                ssuma46 = ssuma2;
            }

            conn4.Close();


            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlConnection conn2 = new SqlConnection();
            SqlCommand cmd2 = new SqlCommand();
            conn2.ConnectionString = Properties.Settings.Default.SqlCon;
            conn2.Open();
            string query2 = "SELECT  [pos_satis_check_main_id],[pos_nomre],[fiscal_id],[date_] ,[user_id_] ,[emeliyyat_nomre],[NEGD_] as nagd,[KART_] as kart,[UMUMI_MEBLEG] as odeme ,[json_] ,[fiscalNum],[documentID],(SELECT TOP 1   [COMPANY_NAME]    FROM [COMPANY].[COMPANY]) AS sirket,(SELECT TOP 1   [SIRKET_VOEN]    FROM [COMPANY].[COMPANY]) as voen  FROM [pos_satis_check_main] WHERE[pos_satis_check_main_id] IN(SELECT[pos_satis_check_main_id]   FROM [pos_gaytarma_manual] where [pos_gaytarma_manual_id] =(select max([pos_gaytarma_manual_id]) from [pos_gaytarma_manual]))";// position column from position table

            cmd2.Connection = conn2;
            cmd2.CommandText = query2;

            SqlDataReader dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {


                string cash = dr2["nagd"].ToString();
                string card = dr2["kart"].ToString();

                string tot = dr2["odeme"].ToString();

                string fisida = dr2["fiscalNum"].ToString();
                snos = fisida;



                tutar = tot;
                kart = card;
                nagd = cash;

            }
            e.Graphics.DrawString(ustbaslik, fonta, Brushes.Black, new RectangleF(50F, 10F, 200F, 90F), sf);

            e.Graphics.DrawString("Satış Qatarma Çeki", font4, Brushes.Black, new Point(90, offset2 + 105));

            e.Graphics.DrawString("Çek nömrəsi No:", font2, Brushes.Black, new Point(70, offset2 + 120));
            e.Graphics.DrawString("Kassir: kassir", font, Brushes.Black, new Point(5, offset2 + 135));
            e.Graphics.DrawString("Tarix: " + DateTime.Now.ToString("dd.MM.yyyy"), font, Brushes.Black, new Point(190, offset2 + 135));
            e.Graphics.DrawString("Saat: " + DateTime.Now.ToString("HH:mm"), font, Brushes.Black, new Point(190, offset2 + 150));

            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 165));
            e.Graphics.DrawString("Satış Çekinin Fiskal ID:", font2, Brushes.Black, new Point(70, offset2 + 170));
        }

        private void ReturnSales(Enums.PayType type)
        {
            Cursor.Current = Cursors.WaitCursor;
            int z = count_grid();
            int a = check_say();
            decimal fr;
            if (z == 1)
            {
                XtraMessageBox.Show("MİQDAR 0 OLA BİLMƏZ");
            }
            else
            {
                if (a > 0)
                {
                    XtraMessageBox.Show("QAYTARILACAQ MİQDAR SAY DAN KİÇİK OLA BİLMƏZ");
                }
                else
                {
                    if (string.IsNullOrEmpty(textEdit1.Text))
                    {
                        MessageBox.Show("Emeliyyat nömrəsi boşdur");
                    }
                    else
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        foreach (int i in gridView1.GetSelectedRows())
                        {
                            DataRow row = gridView1.GetDataRow(i);
                            fr = Convert.ToDecimal(row["QAYTARILACAQ MİQDAR"]);
                            DbProsedures.InsertPosRefund(new DatabaseClasses.PosRefund
                            {
                                proccessNo = textEdit1.Text,
                                pos_satis_check_main_id = Convert.ToInt32(row[0]),
                                pos_satis_check_details_id = Convert.ToInt32(row[1]),
                                quantity = fr,
                                comment = memoEdit1.Text
                            });
                        }
                        bool isSuccess = false;
                        switch (lModel.Text)
                        {
                            case "1":
                                isSuccess = Sunmi.Refund(new DTOs.RefundDto
                                {
                                    IpAddress = lIpAddress.Text,
                                    Cashier = Cashier,
                                    ProccessNo = textEdit1.Text
                                });

                                if (isSuccess)
                                {
                                    textEdit1.Text = DbProsedures.GET_RefundProccessNo();
                                    gridControl1.DataSource = null;
                                }
                                break; /*SUNMI*/
                            case "2":
                                isSuccess = AzSmart.Refund(lIpAddress.Text, lMerchantId.Text, Cashier, textEdit1.Text);
                                if (isSuccess)
                                {
                                    textEdit1.Text = DbProsedures.GET_RefundProccessNo();
                                    gridControl1.DataSource = null;
                                }
                                break; /*AZSMART*/
                            case "3":
                                textBox1.Text = Omnitech.Login(lIpAddress.Text); //AccessToken
                                switch (type)
                                {
                                    case PayType.Cash:
                                        omnitech_gaytarma(lIpAddress.Text, PayType.Cash);
                                        break;
                                    case PayType.Card:
                                        omnitech_gaytarma(lIpAddress.Text, PayType.Card);
                                        break;
                                    case PayType.CashCard:
                                        //  omnitech_gaytarma(labelControl1.Text.ToString(), PayType.CashCard);
                                        break;
                                    default:
                                        break;
                                }
                                break; /*OMNITECH*/
                            case "4":
                                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                                {
                                    connection.Open();
                                    string query = $"SELECT MAX([pos_gaytarma_manual_id]) as ids4 FROM [pos_gaytarma_manual] WHERE user_id_ = {Properties.Settings.Default.UserID}";
                                    using (SqlCommand cmd = new SqlCommand(query, connection))
                                    {
                                        using (SqlDataReader dr = cmd.ExecuteReader())
                                        {
                                            while (dr.Read())
                                            {
                                                string datakontrol = dr["ids4"].ToString();
                                                textBox2.Text = datakontrol;
                                            }
                                        }
                                    }
                                }

                                xprinter_gaytarma();
                                break; /*XPRINTER*/
                            case "5":
                                switch (type)
                                {
                                    case PayType.Cash:
                                        datapay_gaytarma(lIpAddress.Text, PayType.Cash);
                                        break;
                                    case PayType.Card:
                                        datapay_gaytarma(lIpAddress.Text, PayType.Card);
                                        break;
                                    case PayType.CashCard:
                                        break;
                                    default:
                                        break;
                                }
                                break; /*DATAPAY*/
                            case "6":
                                if (NBA_GetInfo())
                                {
                                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                                    {
                                        connection.Open();
                                        string query = $"SELECT MAX([pos_gaytarma_manual_id]) as ids4 FROM [pos_gaytarma_manual] WHERE user_id_ = {Properties.Settings.Default.UserID}";
                                        using (SqlCommand cmd = new SqlCommand(query, connection))
                                        {
                                            using (SqlDataReader dr = cmd.ExecuteReader())
                                            {
                                                while (dr.Read())
                                                {
                                                    string datakontrol = dr["ids4"].ToString();
                                                    textBox2.Text = datakontrol;
                                                }
                                            }
                                        }
                                    }

                                    switch (type)
                                    {
                                        case PayType.Cash:
                                            nba_gaytarma(lIpAddress.Text, PayType.Cash);
                                            break;
                                        case PayType.Card:
                                            nba_gaytarma(lIpAddress.Text, PayType.Card);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                break; /*NBA*/


                            case "7":
                                textBox1.Text = EKASAM.Login(lIpAddress.Text); //AccessToken
                                switch (type)
                                {
                                    case PayType.Cash:
                                        ekasam_gaytarma(lIpAddress.Text, PayType.Cash);
                                        break;
                                    case PayType.Card:
                                        ekasam_gaytarma(lIpAddress.Text, PayType.Card);
                                        break;
                                    case PayType.CashCard:
                                        //  omnitech_gaytarma(labelControl1.Text.ToString(), PayType.CashCard);
                                        break;
                                    default:
                                        break;
                                }
                                break; /*EKASSAM*/
                        }
                        Cursor.Current = Cursors.Default;



                        #region BEFORE CODE


                        //if (lModel.Text == "1")
                        //{
                        //    foreach (int i in gridView1.GetSelectedRows())
                        //    {
                        //        DataRow row = gridView1.GetDataRow(i);
                        //        fr = Convert.ToDecimal(row["QAYTARILACAQ MİQDAR"]);
                        //        DbProsedures.InsertPosRefund(new DatabaseClasses.PosRefund
                        //        {
                        //            proccessNo = textEdit1.Text,
                        //            pos_satis_check_main_id = Convert.ToInt32(row[0]),
                        //            pos_satis_check_details_id = Convert.ToInt32(row[1]),
                        //            quantity = fr,
                        //            comment = memoEdit1.Text
                        //        });

                        //    }

                        //    //if (type is PayType.Card && bankttnmd == "1")
                        //    //{
                        //    //    Bankttnminput bt = new Bankttnminput(null, this);
                        //    //    bt.ShowDialog();

                        //    //}
                        //    //else
                        //    //{
                        //    //    bankttnminputdata = "";
                        //    //}


                        //    Sunmi.ReturnPos(lIpAddress.Text, Cashier, textEdit1.Text);
                        //    gridControl1.DataSource = null;
                        //}
                        //else if (lModel.Text == "3")
                        //{
                        //    foreach (int i in gridView1.GetSelectedRows())
                        //    {
                        //        DataRow row = gridView1.GetDataRow(i);

                        //        fr = Convert.ToDecimal(row["QAYTARILACAQ MİQDAR"]);

                        //        DbProsedures.InsertPosRefund(new DatabaseClasses.PosRefund
                        //        {
                        //            proccessNo = textEdit1.Text,
                        //            pos_satis_check_main_id = Convert.ToInt32(row[0]),
                        //            pos_satis_check_details_id = Convert.ToInt32(row[1]),
                        //            quantity = fr,
                        //            comment = memoEdit1.Text
                        //        });
                        //    }

                        //    textBox1.Text = Omnitech.Login(lIpAddress.Text); //AccessToken
                        //    switch (type)
                        //    {
                        //        case PayType.Cash:
                        //            omnitech_gaytarma(lIpAddress.Text, PayType.Cash);
                        //            break;
                        //        case PayType.Card:
                        //            omnitech_gaytarma(lIpAddress.Text, PayType.Card);
                        //            break;
                        //        case PayType.CashCard:
                        //            //  omnitech_gaytarma(labelControl1.Text.ToString(), PayType.CashCard);
                        //            break;
                        //        default:
                        //            break;
                        //    }
                        //}
                        //else if (lModel.Text == "5")
                        //{

                        //    foreach (int i in gridView1.GetSelectedRows())
                        //    {
                        //        DataRow row = gridView1.GetDataRow(i);
                        //        fr = Convert.ToDecimal(row["QAYTARILACAQ MİQDAR"]);
                        //        DbProsedures.InsertPosRefund(new DatabaseClasses.PosRefund
                        //        {
                        //            proccessNo = textEdit1.Text,
                        //            pos_satis_check_main_id = Convert.ToInt32(row[0]),
                        //            pos_satis_check_details_id = Convert.ToInt32(row[1]),
                        //            quantity = fr,
                        //            comment = memoEdit1.Text
                        //        });
                        //    }

                        //    switch (type)
                        //    {
                        //        case PayType.Cash:
                        //            datapay_gaytarma(lIpAddress.Text, PayType.Cash);
                        //            break;
                        //        case PayType.Card:
                        //            datapay_gaytarma(lIpAddress.Text, PayType.Card);
                        //            break;
                        //        case PayType.CashCard:
                        //            break;
                        //        default:
                        //            break;
                        //    }


                        //}
                        //else if (lModel.Text == "6")
                        //{
                        //    try
                        //    {
                        //        var response = NBA.GetInfo(lIpAddress.Text);
                        //        if (response == null) { return; }
                        //        else
                        //        {
                        //            if (response.message is "Successful operation")
                        //            {
                        //                customer = response.data.company_name;
                        //                customervoen = response.data.company_tax_number;
                        //                obyektkod = response.data.object_tax_number;
                        //                obyetname = response.data.object_name;
                        //                obyektadres = response.data.object_address;
                        //                nkamodel = response.data.cashregister_model;
                        //                nkanumber = response.data.cashregister_factory_number;
                        //                nkarnumber = response.data.cashbox_tax_number;
                        //                textBox4.Text = response.data.cashregister_factory_number;

                        //                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                        //                {
                        //                    connection.Open();
                        //                    string query = $"SELECT MAX([pos_gaytarma_manual_id]) as ids4 FROM [pos_gaytarma_manual] WHERE user_id_ = {Properties.Settings.Default.UserID}";
                        //                    using (SqlCommand cmd = new SqlCommand(query, connection))
                        //                    {
                        //                        using (SqlDataReader dr = cmd.ExecuteReader())
                        //                        {
                        //                            while (dr.Read())
                        //                            {
                        //                                string datakontrol = dr["ids4"].ToString();
                        //                                textBox2.Text = datakontrol;
                        //                            }
                        //                        }
                        //                    }
                        //                }

                        //                foreach (int i in gridView1.GetSelectedRows())
                        //                {
                        //                    DataRow row = gridView1.GetDataRow(i);

                        //                    fr = Convert.ToDecimal(row["QAYTARILACAQ MİQDAR"]);

                        //                    DbProsedures.InsertPosRefund(new DatabaseClasses.PosRefund
                        //                    {
                        //                        proccessNo = textEdit1.Text,
                        //                        pos_satis_check_main_id = Convert.ToInt32(row[0]),
                        //                        pos_satis_check_details_id = Convert.ToInt32(row[1]),
                        //                        quantity = fr,
                        //                        comment = memoEdit1.Text
                        //                    });
                        //                }

                        //                switch (type)
                        //                {
                        //                    case PayType.Cash:
                        //                        nba_gaytarma(lIpAddress.Text, PayType.Cash);
                        //                        break;
                        //                    case PayType.Card:
                        //                        nba_gaytarma(lIpAddress.Text, PayType.Card);
                        //                        break;
                        //                    default:
                        //                        break;
                        //                }
                        //            }
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE(ex.Message);
                        //    }

                        //}
                        //else if (lModel.Text == "4")
                        //{
                        //    SqlConnection conn4 = new SqlConnection();
                        //    SqlCommand cmd4 = new SqlCommand();
                        //    conn4.ConnectionString = Properties.Settings.Default.SqlCon;
                        //    conn4.Open();
                        //    string query4 = "SELECT MAX([pos_gaytarma_manual_id]) as ids4  FROM  [pos_gaytarma_manual] ";

                        //    cmd4.Connection = conn4;
                        //    cmd4.CommandText = query4;

                        //    SqlDataReader dr4 = cmd4.ExecuteReader();

                        //    while (dr4.Read())
                        //    {
                        //        string datakontrol = dr4["ids4"].ToString();
                        //        textBox2.Text = datakontrol;
                        //    }
                        //    foreach (int i in gridView1.GetSelectedRows())
                        //    {
                        //        DataRow row = gridView1.GetDataRow(i);

                        //        fr = Convert.ToDecimal(row["QAYTARILACAQ MİQDAR"]);

                        //        DbProsedures.InsertPosRefund(new DatabaseClasses.PosRefund
                        //        {
                        //            proccessNo = textEdit1.Text,
                        //            pos_satis_check_main_id = Convert.ToInt32(row[0]),
                        //            pos_satis_check_details_id = Convert.ToInt32(row[1]),
                        //            quantity = fr,
                        //            comment = memoEdit1.Text
                        //        });
                        //    }


                        //    xprinter_gaytarma();
                        //}
                        //else if (lModel.Text == "2") //azsmart
                        //{
                        //    foreach (int i in gridView1.GetSelectedRows())
                        //    {
                        //        DataRow row = gridView1.GetDataRow(i);

                        //        fr = Convert.ToDecimal(row["QAYTARILACAQ MİQDAR"]);

                        //        DbProsedures.InsertPosRefund(new DatabaseClasses.PosRefund
                        //        {
                        //            proccessNo = textEdit1.Text,
                        //            pos_satis_check_main_id = Convert.ToInt32(row[0]),
                        //            pos_satis_check_details_id = Convert.ToInt32(row[1]),
                        //            quantity = fr,
                        //            comment = memoEdit1.Text
                        //        });
                        //    }
                        //    bool isSuccess = AzSmart.Refund(lIpAddress.Text, lMerchantId.Text, Cashier, textEdit1.Text);
                        //    if (isSuccess)
                        //    {
                        //        GETKOD();
                        //        gridControl1.DataSource = null;
                        //    }
                        //    //AZSMART_POS_AC(lIpAddress.Text);
                        //}

                        #endregion BEFORE CODE
                    }
                }
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Satış qəbzinin ləğv edilməsi
        /// </summary>
        private void bRollback_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            int z = count_grid();
            int a = check_say();
            decimal fr;
            if (z == 1)
            {
                XtraMessageBox.Show("MİQDAR 0 OLA BİLMƏZ");
            }
            else
            {
                if (a > 0)
                {
                    XtraMessageBox.Show("QAYTARILACAQ MİQDAR SAY DAN KİÇİK OLA BİLMƏZ");
                }
                else
                {


                    if (string.IsNullOrEmpty(textEdit1.Text))
                    {
                        MessageBox.Show("Emeliyyat Nomre bosdur");
                    }
                    else
                    {


                        if (lModel.Text == "2") //azsmart
                        {
                            foreach (int i in gridView1.GetSelectedRows())
                            {
                                DataRow row = gridView1.GetDataRow(i);
                                AzsmartRollback(lIpAddress.Text, Convert.ToInt32(row[0]));
                            }
                        }
                    }
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void AzsmartRollback(string ip_azsmart, int _min_id)
        {
            var url = ip_azsmart;
            url = url + "/rollback";
            var data = AzsmartRollbackJson(_min_id);

            POST(url, data/*, _min_id*/);
        }

        private string AzsmartRollbackJson(int m_id)
        {
            var p = s.AzsmartRollback(m_id);

            string base_64 = Base64Encode(p);
            var data_ = base_64;
            var convert_sign1 = data_ + lMerchantId.Text.ToString();//"85b10352cc424152ae995fa6757ee6d4";
            var conver_sign2sha = sha1(convert_sign1);
            var convert_sign3 = Base64Encode(conver_sign2sha);
            var string_post = "data=" + data_.Replace("=", "%3D") + "&" + "sign=" + convert_sign3.Replace("=", "%3D");
            return string_post;
        }

        private void bCardReturn_Click(object sender, EventArgs e)
        {
            ReturnSales(Enums.PayType.Card);
        }

        private void bCashCardReturn_Click(object sender, EventArgs e)
        {
            ReturnSales(Enums.PayType.CashCard);
        }

        private void bCashReturn_Click(object sender, EventArgs e)
        {
            ReturnSales(Enums.PayType.Cash);
        }

        public int count_grid()
        {
            int x = 0;
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);
                var y = Convert.ToDecimal(row["QAYTARILACAQ MİQDAR"]);
                if (y <= 0)
                {
                    x = 1;
                }

            }
            return x;
        }

        public int check_say()
        {
            int ch = 0;
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);
                var y = Convert.ToDecimal(row["SAY"]);
                var z = Convert.ToDecimal(row["QAYTARILACAQ MİQDAR"]);
                if (y < z)
                {
                    ch = 1;
                }

            }
            return ch;
        }

        private void textEdit6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                //MessageBox.Show("ENTER has been pressed!");
                get(textEdit6.Text.ToString());
                //textEdit5.Text = string.Empty;
                textEdit6.Text = string.Empty;
            }


            else if (e.KeyChar == (char)27)
            {
                this.Close();
            }
        }

        public void get(string paramValue)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

                // Provide the query string with a parameter placeholder.
                //string queryString = "select mal_alisi_details_id,barkod,mehsul_adi,satis_qiymeti,count(*) as say" +
                //    ",satis_qiymeti*count(*) as mebleg " +
                //    "from (select  TOP 100 PERCENT * from calculation order by id desc) " +
                //    "  t  where  emeliyyat_nomre=@pricePoint group by mal_alisi_details_id,barkod,mehsul_adi,satis_qiymeti ";

                string queryString = " select * from dbo.fn_pos_gaytarma_pos_nomre_load (@pricePoint) ";


                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", paramValue);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                gridControl1.DataSource = dt1;


                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Visible = false;
                gridView1.Columns[5].Visible = false;

            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            fPOSRefundReport f = new fPOSRefundReport();
            f.ShowDialog();
        }
    }
}