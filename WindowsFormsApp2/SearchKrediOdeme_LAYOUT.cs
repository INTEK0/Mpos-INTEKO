using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.NKA;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class SearchKrediOdeme_LAYOUT : DevExpress.XtraEditors.XtraForm
    {
        public static string deger2 = "";
        public static string deger3 = "";
        public static string deger4 = "";
        public static string deger5 = "";
        public static string deger6 = "";
        private readonly int _g_user_id;
        private readonly MAINSCRRENS frm1;
        public SearchKrediOdeme_LAYOUT(int _userid, MAINSCRRENS frm_)
        {
            InitializeComponent();
            _g_user_id = _userid;
            frm1 = frm_;
            GridPanelText(gridView1);
            GridPanelText(gridView2);
            GridLocalizer.Active = new MyGridLocalizer();
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Excel faylı|*.xlsx";
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFile.OverwritePrompt = true; //varsa soruşmadan üstünə yazması üçün false olaraq qalmalıdır
            saveFile.FileName = $"Kredit Ödənişləri_{DateTime.Now.ToShortDateString()}.xlsx";
            if (saveFile.ShowDialog() is DialogResult.OK)
            {
                gridControl2.ExportToXlsx(saveFile.FileName);
                FormHelpers.Log("Kredit ödəniş hesabatı excelə çap edildi");
            }
        }

        public void getall()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                string queryString = @"SELECT [KREDIT_SATISI_MAIN_ID] ID,
[GAIME_NOMRE] 'MÜQAVİLƏ NÖMRƏSİ',
[ODENILEN_MEBLEG] 'KREDİT MƏBLƏĞİ',
[MUSTERI] 'AD SOYAD ATA ADI',
[ZAMIN] 'ZAMIN AD SOYAD',
[personel] 'SATIŞ PERSONEL',
[product_name] 'MƏHSULUN ADI',
[taksit] 'KREDİT MÜDDƏTİ(AY)',
[prd_price] 'YEKUN MƏBLƏĞ',
[ayliktutar] 'QRAFİK ÜZRƏ ÖDƏNİŞ',
[DATE_] 'MÜQAVİLƏ TARİXİ' ,
(SELECT MAX([DATE2_]) FROM [KREDIT_SATISI_AYLIKODEME] WHERE kredit_id =[KREDIT_SATISI_MAIN_ID])
'SON ÖDƏNİŞ TARİXİ',
(SELECT SUM(ODENILEN_MEBLEG)  FROM [KREDIT_SATISI_AYLIKODEME] WHERE kredit_id =[KREDIT_SATISI_MAIN_ID]) 'CƏM ÖDƏNİLƏN MƏBLƏĞ',
(SELECT  ODENILEN_MEBLEG FROM [KREDIT_SATISI_AYLIKODEME] WHERE kredit_id =[KREDIT_SATISI_MAIN_ID] AND DATE2_ = 
(SELECT MAX([DATE2_])  FROM [KREDIT_SATISI_AYLIKODEME] WHERE kredit_id =[KREDIT_SATISI_MAIN_ID]) ) 'SON ÖDƏNİŞ MƏBLƏĞİ' ,
[product_id],
ilkinodenis,
prd_qty 'MİQDAR'
FROM [KREDIT_SATISI_MAIN]"; ;
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[14].Visible = false;
                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                getall();

            }
        }

        private void SearchTechizatci_LAYOUT_Load(object sender, EventArgs e)
        {

            textEdit2.TabIndex = 2;

            listView1.Visible = false;

            //labelControl1.Text = "0";
            getall();
            gridControl1.TabStop = false;
            get_ip_model();
            usernames();
            // SUMMI_POS_AC(label1.Text.ToString());

        }

        public void get_vahid()
        {


            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                SqlConnection conn = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                conn.ConnectionString = Properties.Settings.Default.SqlCon;
                conn.Open();
                string query = "SELECT  [VAHID] ,[VERGI_DERECESI]     FROM  [MAL_ALISI_DETAILS] where [MAL_ALISI_DETAILS_ID]=" + label3.Text;


                cmd.Connection = conn;
                cmd.CommandText = query;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ///grid load                              
                    //model_ = Convert.ToInt32( dr["model"].ToString());
                    // ip_ = dr["ip_"].ToString();

                    label4.Text = dr["VAHID"].ToString();

                    label5.Text = dr["VERGI_DERECESI"].ToString();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }

        }

        private void textEdit11_Click(object sender, EventArgs e)
        {
            listView1.Visible = true;
        }


        public void getallodeme()
        {
            try
            {
                gridView2.ClearSelection();
                gridControl2.DataSource = null;


                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                // Provide the query string with a parameter placeholder.
                string queryString =
                     // " exec  dbo.chart_report ";

                     "SELECT  [KREDIT_SATISI_AYLIK_ID] ,[kredit_id],[taksitno] AS \"KREDİT (AY)\",[DATEODEMEGUNU_] AS \"QRAFİK ÜZRƏ ÖDƏNİŞ TARİXİ\",[ODENILECEK_MEBLEG] AS \"AYLIQ ÖDƏNİŞ\" ,[ODENILEN_MEBLEG] \"ÖDƏNİŞ\",  case   WHEN [ODENILEN_MEBLEG]>=ODENILECEK_MEBLEG THEN 1 ELSE 0 END AS KONTROL,[longidsana]   FROM  [KREDIT_SATISI_AYLIKODEME] where kredit_id=" + labelControl4.Text;

                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gridControl2.DataSource = dt;
                gridView2.Columns[1].Visible = false;
                gridView2.Columns[0].Visible = false;
                gridView2.Columns[6].Visible = false;
                gridView2.Columns[7].Visible = false;


                if (gridView2.Columns.ColumnByFieldName("ÖDƏNİŞ ƏT") != null)
                {

                }
                else
                {
                    AddUnboundColumn();
                    AddRepository();

                }


                //gridView1.OptionsSelection.MultiSelect = true;
                //gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;


            }
            catch (Exception e)
            {
                MessageBox.Show("Xəta!\n" + e);
            }
        }

        private void AddUnboundColumn()
        {
            GridColumn unbColumn = gridView2.Columns.AddField("ÖDƏNİŞ ƏT");
            unbColumn.VisibleIndex = gridView2.Columns.Count;
            unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
        }


        private void AddRepository()
        {
            RepositoryItemButtonEdit edit = new RepositoryItemButtonEdit();
            edit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            edit.ButtonClick += edit_ButtonClick;
            edit.Buttons[0].Caption = "ÖDƏNİŞ ƏT";
            edit.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            gridView2.Columns["ÖDƏNİŞ ƏT"].ColumnEdit = edit;
        }
        public int index = 0;
        void edit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            int[] selectedRows = gridView2.GetSelectedRows();
            foreach (var rowHandle in selectedRows)
            {
                label6.Text = rowHandle.ToString();
                index = rowHandle;
                deger2 = gridView2.GetRowCellValue(rowHandle, "KONTROL").ToString();
                deger3 = gridView2.GetRowCellValue(rowHandle, "KREDIT_SATISI_AYLIK_ID").ToString();
                deger6 = gridView2.GetRowCellValue(rowHandle, "KREDİT (AY)").ToString();
                deger4 = gridView2.GetRowCellValue(rowHandle, "longidsana").ToString();
                deger5 = gridView2.GetRowCellValue(rowHandle, "AYLIQ ÖDƏNİŞ").ToString();
            }


            //label6.Text = index.ToString();
            //deger2 = gridView2.GetRowCellValue(index, "KONTROL").ToString();
            //deger3 = gridView2.GetRowCellValue(index, "KREDIT_SATISI_AYLIK_ID").ToString();
            //deger6 = gridView2.GetRowCellValue(index, "KREDİT (AY)").ToString();
            //deger4 = gridView2.GetRowCellValue(index, "longidsana").ToString();
            //deger5 = gridView2.GetRowCellValue(index, "AYLIQ ÖDƏNİŞ").ToString();
            string deger20 = "";



            if (index > 0)
            {
                deger20 = gridView2.GetRowCellValue(index - 1, "KONTROL").ToString();
            }
            else
            {
                deger20 = "Bos";
            }


            if (deger20 == "0")
            {

                XtraMessageBox.Show("Zəhmət olmasa, əvvəlki ayın ödənişini edin.");
            }
            else
            {

                if (deger2 == "1")
                {
                    XtraMessageBox.Show("Ödəniş əvvəllər edilib. Növbəti ödənişi edin");

                }

                else
                {

                    deger5 = gridView2.GetRowCellValue(Convert.ToInt32(label6.Text), "AYLIQ ÖDƏNİŞ").ToString();
                    double deger51 = Math.Round(Convert.ToDouble(deger5), 2);
                    decimal f = Convert.ToDecimal(deger51);

                    nagkardkredit nk = new nagkardkredit(f, this);
                    nk.ShowDialog();



                }
            }



        }




        public static int model_ = 0;
        public static string ip_ = "";
        public void get_ip_model()
        {


            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                SqlConnection conn = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                conn.ConnectionString = Properties.Settings.Default.SqlCon;
                conn.Open();
                string query = "select ki.IP_ADRESS " +
            " from KASSA_IP ki inner join KASSA_FIRMALAR kf " +
            " on ki.KASSA_FIRMA_IP = kf.KASSA_FIRMALAR_ID " +
            " inner join userParol u on u.id = ki.KASSIR_ID " +
            "where u.Id = " + _g_user_id;

                cmd.Connection = conn;
                cmd.CommandText = query;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ///grid load                              
                    //model_ = Convert.ToInt32( dr["model"].ToString());
                    // ip_ = dr["ip_"].ToString();


                    label1.Text = dr["IP_ADRESS"].ToString();

                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }

        }

        public class WeatherForecast
        {
            public string code { get; set; }
            public string message { get; set; }

            public sondata data { get; set; }


        }


        public class sondata
        {

            public string document_id { get; set; }
            public string short_document_id { get; set; }


        }

        public void usernames()
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                    SqlCommand cmd = new SqlCommand("SELECT AD FROM  [userParol]  where id=" + _g_user_id + "   ", connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //XtraMessageBox.Show(reader[0].ToString());

                        label2.Text = reader[0].ToString();


                    }
                    reader.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        public void gelen_data_negd_pos(decimal cash_, decimal card_, decimal umumi_mebleg_)
        {
            string casha = cash_.ToString();
            string card = card_.ToString();
            string umumi = umumi_mebleg_.ToString();

            decimal deger51 = Math.Round(Convert.ToDecimal(deger5), 2);
            double deger9 = Math.Round(Convert.ToDouble(deger6) * Convert.ToDouble(deger5), 2);

            double degerodenena = Math.Round(Convert.ToDouble(deger6), 2);

            double degeryek = Math.Round(Convert.ToDouble(textEdit13.Text), 2);
            double yekunodenens = Convert.ToDouble(textEdit11.Text) - deger9;
            double yekunodenens2 = 0;
            if (yekunodenens < 0)
            {
                yekunodenens2 = 0;
            }
            else
            {
                yekunodenens2 = Math.Round(yekunodenens, 2);
            }

            try
            {


                string fizid = "";
                string fizid2 = "";


                string ip = label1.Text;



                string uuid = Guid.NewGuid().ToString();

                //            var dataheader =




                //"{ \"data\":{ " +
                //"\"cashPayment\":" + casha.Replace(",", ".") + "," +
                //" \"cardPayment\":" + card.Replace(",", ".") + "," +

                // "\"bonusPayment\":0.0," +
                // "\"clientName\":\"" + textEdit5.Text.Replace(",", ".") + "\"," +
                // "\"items\":[{ \"name\":\"" + textEdit6.Text + "\",\"code\":\"" + label3.Text + "\",\"quantity\":1,\"salePrice\":" + deger51.ToString().Replace(",", ".") + ",\"realPrice\":" + degeryek.ToString().Replace(",", ".") + ",\"vatType\":" + label5.Text + ",\"quantityType\":" + label4.Text + "}]," +
                //"\"documentUUID\":\"" + uuid + "\"," +
                //"\"parentDocumentId\":\"" + deger4 + "\"," +
                //" \"paymentNumber\":\"" + deger6 + "\"," +
                //" \"residue\":" + yekunodenens2.ToString().Replace(",", ".") + "," +
                // "\"creditContract\":\"" + textEdit2.Text + "\"," +
                // " \"creditPayer\":\"" + textEdit5.Text + "\"," +
                //  "\"clientTotalBonus\":0.0," +
                //  " \"clientEarnedBonus\":0.0," +
                //  "\"clientBonusCardNumber\":\"\"," +
                // "\"cashierName\":\"" + label2.Text + "\"," +
                // " \"currency\":\"AZN\"}," +
                // "\"operation\":\"credit\"," +
                // "\"username\":\"username\"," +
                // "\"password\":\"password\"}"
                //                ;


                int vatType = Convert.ToInt16(label5.Text);
                int quantityType = Convert.ToInt16(label4.Text);
                decimal salePrice = Convert.ToDecimal(tSalePrice.Text);
                Sunmi.Item item = new Sunmi.Item()
                {
                    name = textEdit6.Text,
                    code = label3.Text,
                    quantity = 1,
                    salePrice = deger51,
                    realPrice = degeryek,
                    vatType = vatType,
                    quantityType = quantityType
                };



                Sunmi.Data data = new Sunmi.Data()
                {
                    documentUUID = uuid,
                    parentDocumentId = deger4,
                    cashPayment = cash_,
                    cardPayment = card_,
                    cashierName = label2.Text,
                    residue = yekunodenens2,
                    paymentNumber = deger6,
                    creditContract = textEdit2.Text,
                    creditPayer = textEdit5.Text,
                    clientName = textEdit5.Text,
                    items = new List<Sunmi.Item> { item }

                };

                Sunmi.RootObject root = new Sunmi.RootObject
                {
                    data = data,
                    operation = "credit"
                };



                string json = Sunmi.CreditPay(root);

                var url = "http://" + ip + ":5544";


                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
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

                    WeatherForecast weatherForecast = System.Text.Json.JsonSerializer.Deserialize<WeatherForecast>(result);

                    if ($"{weatherForecast.message}" == "Successful operation" || $"{weatherForecast.message}" == "Successfull operation")
                    {

                        ReadyMessages.SUCCES_CREDIT_PAYMENT_MESSAGE();

                        FormHelpers.Log($"{textEdit2.Text} nömrəli müqavilənin '{textEdit6.Text}' kredit ödənişi edildi.");


                        string a = weatherForecast.data.document_id;
                        string b = weatherForecast.data.short_document_id;

                        fizid = a;
                        fizid2 = b;

                        SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);

                        SqlCommand cmdodeme = new SqlCommand();
                        con.Open();
                        cmdodeme.CommandText = "UPDATE [dbo].[KREDIT_SATISI_AYLIKODEME] SET [DATE2_]=GETDATE(),[ODENILEN_MEBLEG]=[ODENILECEK_MEBLEG],[longids]=N'" + fizid + "',[shortids]=N'" + fizid2 + "'  WHERE KREDIT_SATISI_AYLIK_ID=" + deger3;
                        cmdodeme.Connection = con;
                        cmdodeme.CommandType = CommandType.Text;
                        cmdodeme.ExecuteNonQuery();
                        
        
                        con.Close();
                        getall();
                        getallodeme();
                        /*   st.insert_chec_pos_main(result, p_id, textEdit1.Text.ToString(), cash_, card_, umumi_mebleg_); */

                    }
                    else
                    {

                        XtraMessageBox.Show(weatherForecast.message);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            //Dinar Tekstil bazasındakı kodu götür
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                int paramValue = Convert.ToInt32(dr[0]);
                labelControl4.Text = dr[0].ToString();
                textEdit2.Text = dr[1].ToString();
                textEdit1.Text = dr[10].ToString();
                textEdit5.Text = dr[3].ToString();
                textEdit6.Text = dr[6].ToString();
                textEdit7.Text = dr[2].ToString();
                textEdit8.Text = dr[7].ToString();
                textEdit3.Text = dr[9].ToString();
                textEdit4.Text = dr[11].ToString();
                textEdit9.Text = dr[13].ToString();
                textEdit10.Text = dr[12].ToString();
                tIlkinOdenis.Text = dr[15].ToString();
                tSalePrice.Text = dr[8].ToString();
                tAmount.Text = dr["MİQDAR"].ToString();
                textEdit13.Text = dr[2].ToString();

                textEdit7.Text = (Convert.ToDouble(textEdit13.Text) - Convert.ToDouble(tIlkinOdenis.Text)).ToString();

                if (textEdit10.Text == "")
                {
                    textEdit11.Text = (Convert.ToDouble(textEdit7.Text)).ToString();
                }
                else
                {
                    textEdit11.Text = (Convert.ToDouble(textEdit7.Text) - Convert.ToDouble(textEdit10.Text)).ToString();
                }
                label3.Text = dr[14].ToString();

                get_vahid();
                getallodeme();
            }
        }
    }
}