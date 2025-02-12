using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

using System.Net;
using System.Security.Cryptography;

using System.Drawing.Printing;
using System.Management;
using System.Printing;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.NKA;

namespace WindowsFormsApp2
{
    public partial class KREDITSATISLAYOUTSA : DevExpress.XtraEditors.XtraForm
    {
        PaperSize paperSize = new PaperSize("A5 (148 x 210 mm)", 584, 827);//set the paper size
        int totalnumber = 0;//this is for total number of items of the list or array
        int itemperpage = 0;//this is for no of item per page 
        private int za;


        private PrintPreviewDialog previewDlg = null;
        private readonly MAINSCRRENS frm1;
        public static string radio = "";
        public static string longids1 = "";
        public static string shortids1 = "";
        public static string mal_alisi_details_id = "0";
        public static string edv = "";
        public static string anbargalig = "0";
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        private const String CATEGORIES_TABLE = "Categories";
        // field name constants
        private const String CATEGORYID_FIELD = "CategoryID";
        private const string TARİX = "TARIX";
        private const string bank_nagd = "ÖDƏMƏ TİPİ";
        private const string GAIME_NOM = "QAİMƏ №";
        private const string ODENILEN_MEBLEG = "ÖDƏNİLƏN MƏBLƏĞ";
        private const string EMELIYYAT_NOMRESI = "EMELIYYAT NOMRESI";
        private const string techicatci_adi = "TƏCHİZATÇI ADI";
        private const String MEHSUL_ADI = "MƏHSUL ADI";
        private const String MEHSUL_KODU = "MƏHSUL KODU";
        private const string musteri_adi = "MÜŞTƏRİ ADI";
        private const string ANBAR = "ANBAR";
        private const string GEYD = "GEYD";

        private const string VERGI_DERECESI = "VEGRI DƏRƏCƏSİ";
        private const string MIGDAR = "MIGDAR";
        private const string VAHID = "VAHID";

        private const string SATIS_GIYMETI = "SATIŞ QİYMƏTİ";
        private const string ENDIRIM_FAIZ = "ENDIRIM FAIZ";
        private const string ENDIRIM_AZN = "ENDIRIM_AZN";
        private const string ENDIRIM_MEBLEGI = "ENDIRIM MƏBLƏĞİ";
        private const string YEKUN_MEBLEG = "YEKUN MƏBLƏĞ";

        private DataTable dt;
        private SqlDataAdapter da;

        SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
        public static int _g_user_id;
        public KREDITSATISLAYOUTSA(int _userid, MAINSCRRENS frm_)
        {
            InitializeComponent();
            _g_user_id = _userid;
            frm1 = frm_;
        }
        public void techizatci_axtar(string techizatci_adi, string Mehsul_ad, string satis_giymeti,
            int mal_Details, string anbar_galig, string _edv_)
        {
            textEdit2.Text = techizatci_adi.ToString();
            textEdit9.Text = Mehsul_ad.ToString();
            tSalePrice.Text = satis_giymeti.ToString();
            //    lookUpEdit8GEtData_yeni(Convert.ToInt32(mal_Details));
            mal_alisi_details_id = mal_Details.ToString();
            anbargalig = anbar_galig.ToString();
            edv = _edv_;

            evdkontrol(_edv_);


        }
        public void MUSTERI(string ID, string MUSTERI_AD)
        {

            labelControl9.Text = ID;
            textEdit3.Text = MUSTERI_AD;
        }

        public void ZAMIN(string ID, string ZAMIN_AD)
        {
            label3.Text = ID;
            tZamin.Text = ZAMIN_AD;
        }
        private string qeryString = "EXEC   dbo.KREDIT_SATISI_EMELIYYAT_NOMRE";
        private string qeryString4 = "EXEC   dbo.USERNAME";

        public void GETKOD()
        {

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
            {

                SqlCommand command = new SqlCommand(qeryString, connection);


                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //XtraMessageBox.Show(reader[0].ToString());

                        textEdit5.Text = reader[0].ToString();


                    }
                    reader.Close();


                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    XtraMessageBox.Show(ex.Message);
                }
            }

        }
        public void usernames()
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                    SqlCommand cmd = new SqlCommand("SELECT AD FROM [userParol]  where id=" + _g_user_id + "   ", connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //XtraMessageBox.Show(reader[0].ToString());

                        simpleLabelItem1.Text = reader[0].ToString();


                    }
                    reader.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        public void evdkontrol(string edvs)
        {
            //   MessageBox.Show(edvs);
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                    SqlCommand cmd = new SqlCommand("SELECT  [EDV_ID] FROM  [VERGI_DERECESI] where [EDV]=N'" + edvs + "'   ", connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //XtraMessageBox.Show(reader[0].ToString());

                        label9.Text = reader[0].ToString();



                    }
                    reader.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void KREDITSATISLAYOUTSA_Load(object sender, EventArgs e)
        {
            //labelControl1.Visible = false;
            InitLookUpEdit_();

            DateTime dateTime = DateTime.UtcNow.Date;
            //dateEdit1.Text = dateTime.ToString();

            dateEdit1.Text = dateTime.ToShortDateString();

            textEdit5.Enabled = false;
            GETKOD();
            usernames();
            //  lookUpEdit8GEtData_yeni();
            //ANBAR_LOAD();
            searchlookupedit();
            //dt = new DataTable(CATEGORIES_TABLE);

            listView1.Visible = false;
            Random random = new Random();
            int number = random.Next(15000, 99500);
            get_ip_model();
            //SUMMI_POS_AC(label5.Text.ToString());

            // returns last index of 'c'




        }


        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }





        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        public void searchlookupedit()
        {

            //DataTable dt = null;

            //using (SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon))

            //{
            //    con.Open();
            //    string strCmd = "SELECT * FROM GAIME_SATIS_SEARCH()";
            //    using (SqlCommand cmd = new SqlCommand(strCmd, con))
            //    {

            //        SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
            //        dt = new DataTable("TName");
            //        da.Fill(dt);

            //    }
            //}
            //searchLookUpEdit1.Properties.DisplayMember = "TƏCHİZATÇI";
            //searchLookUpEdit1.Properties.ValueMember = "TECHIZATCI_ID";
            //searchLookUpEdit1.Properties.DataSource = dt;
            //searchLookUpEdit1.Properties.NullText = "--Seçin--";


        }
        public static string mehsul_kod;


        private void searchLookUpEdit1_TextChanged_1(object sender, EventArgs e)
        {
            searchlookupedit();

            //string marketId = this.searchLookUpEdit1.Properties.View.GetFocusedRowCellValue("MƏHSUL ADI").ToString();
            //string mehsukod = this.searchLookUpEdit1.Properties.View.GetFocusedRowCellValue("MƏHSUL KODU").ToString();
            //   mal_alisi_details_id = this.searchLookUpEdit1.Properties.View.GetFocusedRowCellValue("MAL_ALISI_DETAILS_ID").ToString();
            //  textEdit9.Text = marketId; //mehsul adi
            //    mehsul_kod = mehsukod;
            //  lookUpEdit8GEtData_yeni(Convert.ToInt32(mal_alisi_details_id));
            //  textEdit6.Text= this.searchLookUpEdit1.Properties.View.GetFocusedRowCellValue("SATIS_GIYMETI").ToString();

            //textEdit9.Text = this.searchLookUpEdit1.Properties.View.GetFocusedRowCellValue("MƏHSUL ADI").ToString();
            //  XtraMessageBox.Show(this.searchLookUpEdit1.Properties.View.GetFocusedRowCellValue("MƏHSUL ADI").ToString());

            // label2.Text = mal_alisi_details_id.ToString();

            //SearchLookUpEdit editor = searchLookUpEdit1;
            //DataRowView row = editor.Properties.GetRowByKeyValue(editor.EditValue) as DataRowView;
            //if (row["MƏHSUL ADI"].ToString() != null)
            //{
            //    textEdit9.Text = row["MƏHSUL ADI"].ToString();
            //}

            //mal_alisi_details_id= row["MAL_ALISI_DETAILS_ID"].ToString();
            //textEdit6.Text = row["SATIS_GIYMETI"].ToString();
            //string marketId = row["MƏHSUL ADI"].ToString();
            //string mehsukod = row["MƏHSUL KODU"].ToString();
            //mehsul_kod = mehsukod;
        }




        public void lookUpEdit8GEtData_yeni(int id)
        {
            //int id = Convert.ToInt32(lookUpEdit7.EditValue.ToString());

            if (id > 0)
            {


                string strQuery = "select * from  [dbo].[GAIME_SATIS_magaza_load] (@IDD)";
                //string strQuery = "select GRUPLAR_ID No,GRUP as N'Qrup' " +
                //   " From GRUPLAR  where IXTISAS_ID=@IDD";

                SqlCommand cmd = new SqlCommand(strQuery);

                cmd.Parameters.AddWithValue("@IDD", id);

                DataTable dt = GetData(cmd);




                //lookUpEdit1.Properties.DisplayMember = "MAĞAZA";
                //lookUpEdit1.Properties.ValueMember = "STOREID";
                //lookUpEdit1.Properties.DataSource = dt;
                //lookUpEdit1.Properties.NullText = "--Seçin--";
                //lookUpEdit1.Properties.PopulateColumns();
                //lookUpEdit1.Properties.Columns[0].Visible = false;
                //lookUpEdit1.Properties.Columns[2].Visible = false;

            }


        }


        //public string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";




        private DataTable GetData(SqlCommand cmd)

        {

            DataTable dt = new DataTable();


            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);

            SqlDataAdapter sda = new SqlDataAdapter();

            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

            try

            {

                con.Open();

                sda.SelectCommand = cmd;

                sda.Fill(dt);

                return dt;

            }

            catch (Exception)

            {



                return null;

            }

            finally

            {

                con.Close();

                sda.Dispose();

                con.Dispose();

            }

        }
        private void searchLookUpEdit1_QueryProcessKey(object sender, DevExpress.XtraEditors.Controls.QueryProcessKeyEventArgs e)
        {

        }

        private void searchLookUpEdit1_QueryPopUp(object sender, CancelEventArgs e)
        {


            //searchLookUpEdit1.Properties.View.Columns["TECHIZATCI_ID"].Visible = false;
            //searchLookUpEdit1.Properties.View.Columns["MAL_ALISI_DETAILS_ID"].Visible = false;

        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        public void getmebleg(string paramValue, string paramValue1, string paramValue2, string paramValue3)
        {
            //YEKUN_MEBLEG 

            string queryString = " exec yekun_mebleg_calc @migdar =@pricePoint,@alis_giymet =@pricePoint1,@endirim_faiz =@pricePoint2,@endirim_azn =@pricePoint3";
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.AddWithValue("@pricePoint", paramValue);
            command.Parameters.AddWithValue("@pricePoint1", paramValue1);
            command.Parameters.AddWithValue("@pricePoint2", paramValue2);
            command.Parameters.AddWithValue("@pricePoint3", paramValue3);
            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {

                textEdit10.Text = dr["endirim_meblegi"].ToString();
                tYekunMebleg.Text = dr["yekun_mebleg"].ToString();
            }
            connection.Close();
        }

        private void textEdit7_TextChanged_1(object sender, EventArgs e)
        {
            textEdit13.Text = "0.00";
            getmebleg(tQuantity.Text, tSalePrice.Text, textEdit7.Text, textEdit13.Text);
        }

        private void textEdit13_TextChanged_1(object sender, EventArgs e)
        {

            getmebleg(tQuantity.Text, tSalePrice.Text, textEdit7.Text, textEdit13.Text);
        }

        private void textEdit8_TextChanged_1(object sender, EventArgs e)
        {
            getmebleg(tQuantity.Text, tSalePrice.Text, textEdit7.Text, textEdit13.Text);
        }

        private void textEdit6_TextChanged_1(object sender, EventArgs e)
        {
            getmebleg(tQuantity.Text, tSalePrice.Text, textEdit7.Text, textEdit13.Text);
        }
        CRUD_GAIME_SATISI cgs = new CRUD_GAIME_SATISI();
        int b;










        private void searchLookUpEdit1_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {

        }
        public void getmebleg_yekun_galig(string emeliyyat_nomre_)
        {
            //YEKUN_MEBLEG 

            string queryString = " select SUM(CAST(YEKUN_MEBLEG AS decimal(9, 2))) AS YEKUN_MEBLEG, " +
              " SUM(CAST(ODENILEN_MEBLEG AS decimal(9, 2)))+sum(CAST(ODENILEN_MEBLEG_EDV AS decimal(9, 2))) ODENILEN_MEBLEG , " +
               " (SUM(CAST(YEKUN_MEBLEG AS decimal(9, 2))) - SUM(CAST(ODENILEN_MEBLEG AS decimal(9, 2))) - SUM(CAST(ODENILEN_MEBLEG_EDV AS decimal(9, 2)))) as GALIG " +
                  " from GAIME_SATISI_DETAILS where EMMELIYYAT_NOMRE = @pricePoint ";
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.AddWithValue("@pricePoint", emeliyyat_nomre_);

            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {


            }
            connection.Close();
        }


        public void clear()
        {
            //textEdit5.Text = "";
            textEdit9.Text = "";
            tYekunMebleg.Text = "";
            textEdit2.Text = "";
            tZamin.Text = "";
            textEdit3.Text = "";

            tSalePrice.Text = "";
            textEdit7.Text = "";
            textEdit13.Text = "";
            textEdit10.Text = "";
            textEdit17.Text = "";
            textEdit1.Text = "";
            tIllikFaiz.Text = "";
            tKomissiyaMeblegi.Text = "0";
            tIlkinOdenis.Text = "";
            tAyliqOdenis.Text = "";
            tTotal.Text = "";
            textEdit24.Text = "";
            textEdit25.Text = "";
            tIlkinOdenisdenSonraQaliq.Text = "";
            tMuddetAy.Text = "";
            tIllikFaiz.Text = "";
            tAyliqOdenis.Text = "";
            tTotal.Text = "";
            textEdit1.Text = "";
            textEdit11.Text = "";
            textEdit12.Text = "";
            textEdit5.Text = "";
            memoEdit1.Text = "";
            listView1.Columns.Clear();
            listView1.Items.Clear();
            //InitLookUpEdit_();

            GETKOD();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            MUSTERIAXTARKREDIT M = new MUSTERIAXTARKREDIT(this);
            M.Show();
        }


        public void ksatis()
        {

            if (string.IsNullOrEmpty(textEdit3.Text) ||
                string.IsNullOrEmpty(textEdit9.Text))
            {
                XtraMessageBox.Show("MƏLUMATLAR TAM OLARAQ DOLDURULMALIDIR");
            }
            else
            {
                int y = cgs.test_proc_(textEdit5.Text.ToString(), textEdit3.Text.ToString());

                if (y > 0)
                {
                    XtraMessageBox.Show("MÜŞTƏRİ ADI DÜZ DEYİL");
                }
                else
                {

                    if (string.IsNullOrEmpty(textEdit3.Text))
                    {
                        XtraMessageBox.Show("MÜŞTƏRİ ADI DAXİL EDİLMƏMİŞDİR");
                    }
                    else
                    {
                        try
                        {
                            string kno = textEdit5.Text;
                            string kno2 = textEdit12.Text;
                            string fizidl6 = longids1;
                            string fizidl7 = label7.Text;

                            //  double ilkodenis = Convert.ToDouble(tIlkinOdenis.Text.Replace(",", ".")); //Sayıları düzgün əlavə etmir deyə yığışdırdım -Həsən
                            double ilkodenis = Math.Round(Convert.ToDouble(tIlkinOdenis.Text), 2);
                            string tarix = dateEdit1.Text;
                            string musteri_id = labelControl9.Text;
                            string musteri = textEdit3.Text;
                            string zamin_id = label3.Text;
                            string zamin = tZamin.Text;
                            int userid = _g_user_id;
                            string personel = simpleLabelItem1.Text;
                            string techizatci = textEdit2.Text;


                            string ak = mal_alisi_details_id;

                            string Mehsul_ad = textEdit9.Text;
                            string miktar = tQuantity.Text.Replace(",", ".");
                            string satis_giymeti = (tSalePrice.Text.Replace(",", "."));
                            string inidirimoran = textEdit7.Text.Replace(",", ".");
                            string inidirimamnt = textEdit10.Text.Replace(",", ".");
                            string ilfaiz = tIllikFaiz.Text.Replace(",", ".");
                            string kkomisyon = tKomissiyaMeblegi.Text.Replace(",", ".");
                            string ttaksit = tMuddetAy.Text.Replace(",", ".");
                            double esas = Convert.ToDouble(textEdit1.Text.Replace(",", "."));
                            double toplammeblag2 = Convert.ToDouble(textEdit1.Text) + Convert.ToDouble(tIlkinOdenis.Text);
                            string toplammeblag = Convert.ToString(Convert.ToDouble(tTotal.Text)).Replace(",", ".");
                            string ayliktutar = tAyliqOdenis.Text.Replace(",", ".");
                            string aciklama = memoEdit1.Text;
                            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
                            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[KREDIT_SATISI_MAIN] ([EMELIIYYAT_NOMRE],GAIME_NOMRE,[ODENILEN_MEBLEG],[ODEME_TIPI],[musteri_id],[MUSTERI],[zamin_id],[ZAMIN],[_user_id],[personel],techizat,[product_id]," +
                                "[product_name] ,[prd_qty],[prd_price],[prd_discount],[prd_discountamnt],[illikfaiz],[komisyon],[taksit],[yekun],[ilkinodenis],[ayliktutar],[aciklama], [longids],[shortids]) VALUES ('" + kno + "','" + kno2 + "'," + toplammeblag + ",1," + musteri_id + ",N'" + musteri + "'," + zamin_id + ",N'" + zamin + "'," + _g_user_id + ",N'" + personel + "',N'" + techizatci + "'," + ak + "," +
                                "N'" + Mehsul_ad + "'," + miktar + "," + satis_giymeti + ",'" + inidirimoran + "'," + inidirimamnt + ",'" + ilfaiz + "','" + kkomisyon + "','" + ttaksit + "'," + toplammeblag + "," + ilkodenis + "," + ayliktutar + ",'" + aciklama + "','" + fizidl6 + "','" + fizidl7 + "') ", con);





                            con.Open();
                            cmd.CommandType = CommandType.Text;

                            SqlDataReader dr416 = cmd.ExecuteReader();
                            dr416.Close();

                            int indexa = textEdit5.Text.LastIndexOf('-');
                            int indexb = textEdit5.Text.Length;
                            string idnos = (textEdit5.Text.Substring(indexa + 1, indexb - indexa - 1));

                            int countay2 = Convert.ToInt32(tMuddetAy.Text);
                            for (int counti2 = 1; counti2 <= countay2; counti2++)
                            {
                                int j = 30 * counti2;
                                SqlCommand cmdodeme = new SqlCommand("INSERT INTO [dbo].[KREDIT_SATISI_AYLIKODEME] ([kredit_id],[taksitno],[DATEODEMEGUNU_],[ODENILECEK_MEBLEG],longidsana) VALUES (" + idnos + "," + counti2 + ",'" + DateTime.Today.AddDays(j).ToString("yyyy-MM-dd") + "'," + tAyliqOdenis.Text.Replace(",", ".") + ",'" + label6.Text + "')", con);


                                cmdodeme.CommandType = CommandType.Text;

                                SqlDataReader dr4168 = cmdodeme.ExecuteReader();
                                dr4168.Close();
                            }


                            FormHelpers.Log($"{textEdit9.Text} məhsulu {textEdit12.Text} müqavilə nömrəsinə əsasən kredit satışı ilə satıldı.");

                            con.Close();
                            krediprint();

                            clear();
                            textEdit9.Text = "";
                            //  lookUpEdit1.EditValue = null;
                            textEdit3.Text = "";
                            GETKOD();

                            textEdit2.Text = "";

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message.ToString());
                        }




                        textEdit2.Text = "";

                    }

                }
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            clear();

            //searchLookUpEdit1.EditValue = null;
            textEdit9.Text = "";
            //lookUpEdit1.EditValue = null;
            textEdit3.Text = "";

            tSalePrice.Text = "";
            textEdit2.Text = "";
            GETKOD();

            //InitLookUpEdit_();
        }



        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            MUSTERIAXTARKREDIT M = new MUSTERIAXTARKREDIT(this);
            M.ShowDialog();
        }


        private void simpleButton6_Click(object sender, EventArgs e)
        {
            TECHIZATCIKREDIT T = new TECHIZATCIKREDIT(this);
            T.ShowDialog();
        }


        private List<Account> datasource_;
        private void InitLookUpEdit_()
        {
            datasource_ = new List<Account>();
            Random random = new Random();
            datasource_.Add(new Account("MƏRKƏZ OBYEKT") { ID = 1002 });
            //  datasource.Add(new Account("S"){ ID = random.Next(100)});
            lookUpEdit1.Properties.DataSource = datasource_;
            lookUpEdit1.Properties.DisplayMember = "MƏRKƏZ OBYEKT";
            lookUpEdit1.Properties.ValueMember = "4";
        }

        private void KREDITSATISLAYOUTSA_Shown(object sender, EventArgs e)
        {
            if (datasource_.Count == 1)
                lookUpEdit1.EditValue = datasource_[0].ID;
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //rowcellclick

        }

        private void KREDITSATISLAYOUTSA_FormClosing(object sender, FormClosingEventArgs e)
        {
            // frm1.getall();
        }

        private void textEdit4_TextChanged(object sender, EventArgs e)
        {
            //yekun mebleg
            if (string.IsNullOrEmpty(tYekunMebleg.Text)) { }
            else
            {
                getmebleg_(tYekunMebleg.Text.ToString(), edv);
            }


        }
        public void getmebleg_(string paramValue, string paramValue1)
        {

            string queryString = " exec mehsul_alisi_edv @yekun_mebleg_=@pricePoint,@vergi_derece =@pricePoint1";
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.AddWithValue("@pricePoint", paramValue);
            command.Parameters.AddWithValue("@pricePoint1", paramValue1);

            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {

                textEdit1.Text = dr["vergisiz"].ToString();
                textEdit11.Text = dr["vergi"].ToString();
            }
            connection.Close();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            ZAMINKREDIT M = new ZAMINKREDIT(this);
            M.ShowDialog();
        }

        private void textEdit8_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit20_EditValueChanged(object sender, EventArgs e)
        {

            try
            {


                double komisya = Math.Round(((Convert.ToDouble(tYekunMebleg.Text) - Convert.ToDouble(tIlkinOdenis.Text)) * (Convert.ToDouble(tKomissiyaMeblegi.Text) / 100)), 2);
                double faizh = Math.Round(((Convert.ToDouble(tIlkinOdenisdenSonraQaliq.Text) / 100) * (Convert.ToDouble(tIllikFaiz.Text))) * Convert.ToInt32(tMuddetAy.Text), 2);
                textEdit17.Text = Math.Round(faizh, 2).ToString();
                double kreditmebl = Convert.ToDouble(tIlkinOdenisdenSonraQaliq.Text);
                double tota = Math.Round(faizh, 2) + kreditmebl;
                double aylikfaizsiz = kreditmebl / Convert.ToInt32(tMuddetAy.Text);

                double yilfaiz = Math.Round((Convert.ToDouble(tYekunMebleg.Text) + komisya - Convert.ToDouble(tIlkinOdenis.Text)) * (Convert.ToDouble(tIllikFaiz.Text) / 100) / 12, 2);
                double krediteverilenmeblag = Convert.ToDouble(tYekunMebleg.Text) - Convert.ToDouble(tIlkinOdenis.Text) + komisya + (yilfaiz * Convert.ToInt32(tMuddetAy.Text));

                tAyliqOdenis.Text = (Math.Round((tota / Convert.ToInt32(tMuddetAy.Text)), 2)).ToString();
                tTotal.Text = Math.Round(tota, 2).ToString();
                double edvderece = Math.Round(Convert.ToDouble(textEdit11.Text) / Convert.ToDouble(textEdit1.Text), 2);

                double yekuna = tota;

                textEdit1.Text = tota.ToString();
                textEdit11.Text = (tota * 0.18).ToString();
                listView1.Visible = true;
                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.View = View.Details;

                //ListView'e ızgaralı görünüm kazandırır. (Excel tablosu gibi.)
                listView1.GridLines = true;
                listView1.Columns.Add("NO", 50);
                listView1.Columns.Add("QRAFİK ÜZRƏ ÖDƏNİŞ TARİXİ", 100);
                listView1.Columns.Add("ƏSAS MƏBLƏĞDƏN QALIQ", 100);
                //listView1.Columns.Add("AYLIQ ƏSAS MƏBLƏĞ", 100);
                //listView1.Columns.Add("AYLIQ FAİZ", 100);
                listView1.Columns.Add("AYLIQ CƏM ÖDƏNİŞ", 100);
                listView1.Columns.Add("QALIQ MƏBLƏĞ", 100);
                double aylikfaiztut = faizh / Convert.ToInt32(tMuddetAy.Text);
                int countay = Convert.ToInt32(tMuddetAy.Text);
                DateTime bugun = DateTime.Today;
                for (int counti = 1; counti <= countay; counti++)
                {
                    int j = 30 * counti;
                    double hesap1 = kreditmebl - ((counti - 1) * Math.Round(aylikfaizsiz, 2));

                    double hesap2 = Convert.ToDouble(tTotal.Text) - (Convert.ToDouble(tAyliqOdenis.Text) * counti);


                    listView1.Items.Add(counti.ToString());
                    listView1.Items[counti - 1].SubItems.Add(DateTime.Today.AddMonths(counti).ToString("dd.MM.yyyy"));
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(hesap1, 2).ToString());
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(aylikfaizsiz, 2).ToString());
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(aylikfaiztut, 2).ToString());
                    listView1.Items[counti - 1].SubItems.Add(tAyliqOdenis.Text);
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(hesap2, 2).ToString());



                }

                GETKOD();
                listView1.Visible = true;
            }

            catch
            {

            }


        }

        private void textEdit21_EditValueChanged(object sender, EventArgs e)
        {

            try
            {

                if (tIlkinOdenis.Text == "")
                {
                    tIlkinOdenis.Text = "0";

                    tIlkinOdenisdenSonraQaliq.Text = textEdit25.Text;
                }
                else if (Convert.ToDouble(tIlkinOdenis.Text) < 1)
                {
                    tIlkinOdenis.Text = "0";
                    double netsa = Convert.ToDouble(textEdit25.Text);
                    double ilksa = Convert.ToDouble(tIlkinOdenis.Text);

                    double totala = netsa;
                    tIlkinOdenisdenSonraQaliq.Text = totala.ToString();
                }
                else
                {
                    if (Convert.ToDecimal(tIlkinOdenis.Text) >= Convert.ToDecimal(tYekunMebleg.Text))
                    {
                        tIlkinOdenis.Text = "0";
                        double netsa = Convert.ToDouble(textEdit25.Text);
                        double ilksa = Convert.ToDouble(tIlkinOdenis.Text);

                        double totala = netsa - ilksa;
                        tIlkinOdenisdenSonraQaliq.Text = totala.ToString();

                    }

                    else
                    {

                        double netsa = Convert.ToDouble(textEdit25.Text);
                        double ilksa = Convert.ToDouble(tIlkinOdenis.Text);

                        double totala = netsa - ilksa;
                        tIlkinOdenisdenSonraQaliq.Text = totala.ToString();
                    }
                }


            }

            catch
            {
                Console.WriteLine("Xəta!\n" + e);
            }

        }

        private void textEdit5_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit7_EditValueChanged(object sender, EventArgs e)
        {

        }

        public static int model_ = 0;
        public static string ip_ = "";
        public void get_ip_model()
        {
            var kassa = FormHelpers.GetIpModel();
            label5.Text = kassa.Ip;
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


                    label5.Text = kassa.Ip;

                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }

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
                string query = "SELECT  [VAHID]      FROM  [MAL_ALISI_DETAILS] where [MAL_ALISI_DETAILS_ID]=" + mal_alisi_details_id;


                cmd.Connection = conn;
                cmd.CommandText = query;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ///grid load                              
                    //model_ = Convert.ToInt32( dr["model"].ToString());
                    // ip_ = dr["ip_"].ToString();


                    label8.Text = dr["VAHID"].ToString();

                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }

        }

        public void SUMMI_POS_AC(string _ip_)
        {
            //pos ac

            // record Person(string Name, string Occupation);
            try
            {
                var url = "http://" + _ip_ + ":5544";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json;charset=utf-8";
                httpRequest.ContentType = "application/json;charset=utf-8";

                var data = @"{
                   ""operation"": ""openShift"",
                   ""cashierName"": ""Yeni Kassir"",
                   ""username"":""admin"",
                   ""password"": ""admin""
                          }";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    //MessageBox.Show(result.ToString());

                    WeatherForecast weatherForecast =
                     System.Text.Json.JsonSerializer.Deserialize<WeatherForecast>(result);

                    //MessageBox.Show($"{weatherForecast.message}");

                    if ($"{weatherForecast.message}" == "Success operation" || $"{weatherForecast.message}" == "Successful operation")
                    {
                        XtraMessageBox.Show("UĞURLA AÇILDI");
                    }
                    else
                    {
                        XtraMessageBox.Show(weatherForecast.message);
                    }
                }
            }
            catch
            {
                XtraMessageBox.Show("Kassa ilə əlaqə yoxdur");
                //this.Close();
            }
            //MessageBox.Show(httpResponse.StatusCode.ToString());
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

        private void textEdit22_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit19_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double kommisyah = Math.Round(Convert.ToDouble(tYekunMebleg.Text) * Convert.ToDouble(tKomissiyaMeblegi.Text) / 100, 2);
                textEdit24.Text = kommisyah.ToString();
                double totalkom = kommisyah + Math.Round(Convert.ToDouble(tYekunMebleg.Text), 2);
                textEdit25.Text = totalkom.ToString();

                double netsa = Convert.ToDouble(textEdit25.Text);
                double ilksa = Convert.ToDouble(tIlkinOdenis.Text);

                double totala = netsa - ilksa;
                tIlkinOdenisdenSonraQaliq.Text = totala.ToString();
            }

            catch
            {
                Console.WriteLine("Xəta!\n" + e);
            }
        }





        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {

        }

        private void Hesapla_Click(object sender, EventArgs e)
        {
            try
            {
                double kommisyah = Math.Round(Convert.ToDouble(tYekunMebleg.Text) * Convert.ToDouble(tKomissiyaMeblegi.Text) / 100, 2);
                textEdit24.Text = kommisyah.ToString();
                double totalkom = kommisyah + Math.Round(Convert.ToDouble(tYekunMebleg.Text), 2);
                textEdit25.Text = totalkom.ToString();

                double netsa = Convert.ToDouble(textEdit25.Text);
                double ilksa = Convert.ToDouble(tIlkinOdenis.Text);

                double totala = netsa - ilksa;
                tIlkinOdenisdenSonraQaliq.Text = totala.ToString();

                if (tIlkinOdenis.Text == "")
                {
                    tIlkinOdenis.Text = "0";

                    tIlkinOdenisdenSonraQaliq.Text = textEdit25.Text;
                }
                else if (Convert.ToDouble(tIlkinOdenis.Text) < 1)
                {
                    tIlkinOdenis.Text = "0";
                    double netsa2 = Convert.ToDouble(textEdit25.Text);
                    double ilksa2 = Convert.ToDouble(tIlkinOdenis.Text);

                    double totala2 = netsa2;
                    tIlkinOdenisdenSonraQaliq.Text = totala.ToString();
                }
                else
                {
                    if (Convert.ToDecimal(tIlkinOdenis.Text) >= Convert.ToDecimal(tYekunMebleg.Text))
                    {
                        tIlkinOdenis.Text = "0";
                        double netsa2 = Convert.ToDouble(textEdit25.Text);
                        double ilksa2 = Convert.ToDouble(tIlkinOdenis.Text);

                        double totala2 = netsa2 - ilksa2;
                        tIlkinOdenisdenSonraQaliq.Text = totala2.ToString();

                    }

                    else
                    {

                        double netsa2 = Convert.ToDouble(textEdit25.Text);
                        double ilksa2 = Convert.ToDouble(tIlkinOdenis.Text);

                        double totala2 = netsa2 - ilksa2;
                        tIlkinOdenisdenSonraQaliq.Text = totala2.ToString();
                    }
                }

                double komisya = Math.Round(((Convert.ToDouble(tYekunMebleg.Text) - Convert.ToDouble(tIlkinOdenis.Text)) * (Convert.ToDouble(tKomissiyaMeblegi.Text) / 100)), 2);
                double faizh = Math.Round(((Convert.ToDouble(tIlkinOdenisdenSonraQaliq.Text) / 100) * (Convert.ToDouble(tIllikFaiz.Text))) * Convert.ToInt32(tMuddetAy.Text), 2);
                textEdit17.Text = Math.Round(faizh, 2).ToString();
                double kreditmebl = Convert.ToDouble(tIlkinOdenisdenSonraQaliq.Text);
                double tota = Math.Round(faizh, 2) + kreditmebl;
                double aylikfaizsiz = kreditmebl / Convert.ToInt32(tMuddetAy.Text);

                double yilfaiz = Math.Round(((Convert.ToDouble(tYekunMebleg.Text) + komisya - Convert.ToDouble(tIlkinOdenis.Text)) * (Convert.ToDouble(tIllikFaiz.Text) / 100)) / 1, 2);
                double krediteverilenmeblag = Convert.ToDouble(tYekunMebleg.Text) - Convert.ToDouble(tIlkinOdenis.Text) + komisya + (yilfaiz * Convert.ToInt32(tMuddetAy.Text));

                tAyliqOdenis.Text = (Math.Round((tota / Convert.ToInt32(tMuddetAy.Text)), 2)).ToString();
                tTotal.Text = Math.Round(tota, 2).ToString();
                double edvderece = Math.Round(Convert.ToDouble(textEdit11.Text) / Convert.ToDouble(textEdit1.Text), 2);

                double yekuna = tota;

                textEdit1.Text = tota.ToString();
                textEdit11.Text = (tota * 0.18).ToString();
                listView1.Visible = true;
                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.View = View.Details;

                //ListView'e ızgaralı görünüm kazandırır. (Excel tablosu gibi.)
                listView1.GridLines = true;
                listView1.Columns.Add("NO", 50);
                listView1.Columns.Add("QRAFİK ÜZRƏ ÖDƏNİŞ TARİXİ", 100);
                listView1.Columns.Add("ƏSAS MƏBLƏĞDƏN QALIQ", 100);
                //listView1.Columns.Add("AYLIQ ƏSAS MƏBLƏĞ", 100);
                //listView1.Columns.Add("AYLIQ FAİZ", 100);
                listView1.Columns.Add("AYLIQ CƏM ÖDƏNİŞ", 100);
                listView1.Columns.Add("QALIQ MƏBLƏĞ", 100);
                double aylikfaiztut = faizh / Convert.ToInt32(tMuddetAy.Text);
                int countay = Convert.ToInt32(tMuddetAy.Text);
                DateTime bugun = DateTime.Today;
                for (int counti = 1; counti <= countay; counti++)
                {
                    int j = 30 * counti;
                    double hesap1 = kreditmebl - ((counti - 1) * Math.Round(aylikfaizsiz, 2));

                    double hesap2 = Convert.ToDouble(tTotal.Text) - (Convert.ToDouble(tAyliqOdenis.Text) * counti);


                    listView1.Items.Add(counti.ToString());
                    listView1.Items[counti - 1].SubItems.Add(DateTime.Today.AddMonths(counti).ToString("dd.MM.yyyy"));
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(hesap1, 2).ToString());
                    //listView1.Items[counti - 1].SubItems.Add(Math.Round(aylikfaizsiz, 2).ToString());
                    //listView1.Items[counti - 1].SubItems.Add(Math.Round(aylikfaiztut,2).ToString());
                    listView1.Items[counti - 1].SubItems.Add(tAyliqOdenis.Text);
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(hesap2, 2).ToString());



                }

                GETKOD();

                listView1.Visible = true;
            }

            catch
            {

            }
        }

        private void textEdit4_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double kommisyah = Math.Round(Convert.ToDouble(tYekunMebleg.Text) * Convert.ToDouble(tKomissiyaMeblegi.Text) / 100, 2);
                textEdit24.Text = kommisyah.ToString();
                double totalkom = kommisyah + Math.Round(Convert.ToDouble(tYekunMebleg.Text), 2);
                textEdit25.Text = totalkom.ToString();

                double netsa = Convert.ToDouble(textEdit25.Text);
                double ilksa = Convert.ToDouble(tIlkinOdenis.Text);

                double totala = netsa - ilksa;
                tIlkinOdenisdenSonraQaliq.Text = totala.ToString();
            }

            catch
            {
                Console.WriteLine("Xəta!\n" + e);
            }
        }

        private void textEdit26_EditValueChanged(object sender, EventArgs e)
        {

            try
            {


                double komisya = Math.Round(((Convert.ToDouble(tYekunMebleg.Text) - Convert.ToDouble(tIlkinOdenis.Text)) * (Convert.ToDouble(tKomissiyaMeblegi.Text) / 100)), 2);
                double faizh = Math.Round(((Convert.ToDouble(tIlkinOdenisdenSonraQaliq.Text) / 100) * (Convert.ToDouble(tIllikFaiz.Text))) * Convert.ToInt32(tMuddetAy.Text), 2);
                textEdit17.Text = Math.Round(faizh, 2).ToString();
                double kreditmebl = Convert.ToDouble(tIlkinOdenisdenSonraQaliq.Text);
                double tota = Math.Round(faizh, 2) + kreditmebl;
                double aylikfaizsiz = kreditmebl / Convert.ToInt32(tMuddetAy.Text);

                double yilfaiz = Math.Round(((Convert.ToDouble(tYekunMebleg.Text) + komisya - Convert.ToDouble(tIlkinOdenis.Text)) * (Convert.ToDouble(tIllikFaiz.Text) / 100)) / 12, 2);
                double krediteverilenmeblag = Convert.ToDouble(tYekunMebleg.Text) - Convert.ToDouble(tIlkinOdenis.Text) + komisya + (yilfaiz * Convert.ToInt32(tMuddetAy.Text));

                tAyliqOdenis.Text = (Math.Round((tota / Convert.ToInt32(tMuddetAy.Text)), 2)).ToString();
                tTotal.Text = Math.Round(tota, 2).ToString();
                double edvderece = Math.Round(Convert.ToDouble(textEdit11.Text) / Convert.ToDouble(textEdit1.Text), 2);

                double yekuna = tota;

                textEdit1.Text = tota.ToString();
                textEdit11.Text = (tota * 0.18).ToString();
                listView1.Visible = true;
                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.View = View.Details;

                //ListView'e ızgaralı görünüm kazandırır. (Excel tablosu gibi.)
                listView1.GridLines = true;
                listView1.Columns.Add("NO", 50);
                listView1.Columns.Add("QRAFİK ÜZRƏ ÖDƏNİŞ TARİXİ", 100);
                listView1.Columns.Add("ƏSAS MƏBLƏĞDƏN QALIQ", 100);
                //listView1.Columns.Add("AYLIQ ƏSAS MƏBLƏĞ", 100);
                //listView1.Columns.Add("AYLIQ FAİZ", 100);
                listView1.Columns.Add("AYLIQ CƏM ÖDƏNİŞ", 100);
                listView1.Columns.Add("QALIQ MƏBLƏĞ", 100);
                double aylikfaiztut = faizh / Convert.ToInt32(tMuddetAy.Text);
                int countay = Convert.ToInt32(tMuddetAy.Text);
                DateTime bugun = DateTime.Today;
                for (int counti = 1; counti <= countay; counti++)
                {
                    int j = 30 * counti;
                    double hesap1 = kreditmebl - ((counti - 1) * Math.Round(aylikfaizsiz, 2));

                    double hesap2 = Convert.ToDouble(tTotal.Text) - (Convert.ToDouble(tAyliqOdenis.Text) * counti);


                    listView1.Items.Add(counti.ToString());
                    listView1.Items[counti - 1].SubItems.Add(DateTime.Today.AddMonths(counti).ToString("dd.MM.yyyy"));
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(hesap1, 2).ToString());
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(aylikfaizsiz, 2).ToString());
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(aylikfaiztut, 2).ToString());
                    listView1.Items[counti - 1].SubItems.Add(tAyliqOdenis.Text);
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(hesap2, 2).ToString());



                }

                GETKOD();
                listView1.Visible = true;
            }

            catch
            {

            }
        }

        private void textEdit6_EditValueChanged(object sender, EventArgs e)
        {

        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tMuddetAy.EditValue = comboBox1.SelectedItem.ToString();
        }



        private void simpleButton5_Click(object sender, EventArgs e)
        {
            string uuid = Guid.NewGuid().ToString();



            if (string.IsNullOrEmpty(textEdit3.Text) ||
             string.IsNullOrEmpty(textEdit9.Text)
             ||
             string.IsNullOrEmpty(textEdit12.Text)
             //(Convert.ToInt32(textEdit8.Text)==0)
             //string.IsNullOrEmpty(textEdit6.Text)
             )
            {
                XtraMessageBox.Show("MƏLUMATLAR TAM OLARAQ DOLDURULMALIDIR");
            }
            else
            {


                int y = cgs.test_proc_(textEdit5.Text.ToString(), textEdit3.Text.ToString());

                if (y > 0)
                {
                    XtraMessageBox.Show("MÜŞTƏRİ ADI DÜZ DEYİL");
                }
                else
                {

                    if (string.IsNullOrEmpty(textEdit3.Text))
                    {
                        XtraMessageBox.Show("MÜŞTƏRİ SEÇİMİ EDİLMƏMİŞDİR", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(tZamin.Text))
                    {
                        XtraMessageBox.Show("ZAMİN SEÇİMİ EDİLMƏMİŞDİR", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    else
                    {

                        try
                        {

                            get_vahid();
                            string vahid = label8.Text;
                            int indexa = textEdit5.Text.LastIndexOf('-');
                            int indexb = textEdit5.Text.Length;
                            string idnos = (textEdit5.Text.Substring(indexa + 1, indexb - indexa - 1));
                            double price = Convert.ToDouble(textEdit1.Text) + Convert.ToDouble(tIlkinOdenis.Text);
                            string testprice = Convert.ToString(price);
                            double cashab = Math.Round(Convert.ToDouble(tIlkinOdenis.Text), 2);
                            string personel = simpleLabelItem1.Text;
                            double control1 = Convert.ToDouble(idnos) + 115000;
                            //            var dataheader =

                            //          "  {\"data\": {" +

                            //"\"documentUUID\": \" " + uuid + "\"," +
                            //"\"cashPayment\":0.0," +
                            //"\"cardPayment\":" + tIlkinOdenis.Text.Replace(",", ".") + "," +
                            //"\"creditPayment\":" + tTotal.Text.Replace(",", ".") + "," +
                            //"\"bonusPayment\":0.0," +
                            //"\"depositPayment\":0.0," +
                            //"\"creditContract\":\"" + textEdit12.Text.Replace(",", ".") + "\"," +
                            //"\"creditPayer\":\"" + textEdit3.Text.Replace(",", ".") + "\"," +
                            //"\"clientName\":\"" + textEdit3.Text.Replace(",", ".") + "\"," +
                            //"\"items\":[{\"name\":\"" + textEdit9.Text + "\",\"code\":\"" + mal_alisi_details_id + "\",\"quantity\":" + tQuantity.Text.Replace(",", ".") + ",\"salePrice\":" + testprice.Replace(",", ".") + ",\"vatType\":" + tQuantity.Text.Replace(",", ".") + ",\"quantityType\":" + vahid + "}]," +
                            //"\"clientTotalBonus\":0.0,\"clientEarnedBonus\":0.0," +
                            //"\"clientBonusCardNumber\":\"\"," +
                            //"\"cashierName\":\"" + personel + "\"," +
                            //"\"currency\":\"AZN\"}," +
                            //"\"operation\":\"sale\"," +
                            //"\"username\":\"username\"," +
                            //"\"password\":\"password\"} "
                            //                ;

                            double quantity = Convert.ToDouble(tQuantity.Text);
                            decimal saleprice = Convert.ToDecimal(price);

                            int vatType = Convert.ToInt32(label9.Text);
                            int quantityType = Convert.ToInt32(vahid);

                            Sunmi.Item item = new Sunmi.Item()
                            {
                                name = textEdit9.Text,
                                code = mal_alisi_details_id,
                                quantity = quantity,
                                salePrice = saleprice,
                                vatType = vatType,
                                quantityType = quantityType
                            };

                            decimal card = Convert.ToDecimal(cashab);
                            decimal creditPayment = Convert.ToDecimal(tTotal.Text);
                            Sunmi.Data data = new Sunmi.Data()
                            {
                                documentUUID = uuid,
                                cashPayment = 0,
                                cardPayment = card,
                                cashierName = personel,
                                creditContract = textEdit12.Text,
                                creditPayment = creditPayment,
                                creditPayer = textEdit3.Text,
                                clientName = textEdit3.Text,
                                items = new List<Sunmi.Item> { item }
                            };

                            Sunmi.RootObject root = new Sunmi.RootObject
                            {
                                data = data,
                                operation = "sale"
                            };

                            string json = Sunmi.CreditSale(root);

                            var url =  label5.Text;


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

                                WeatherForecast weatherForecast =
                                 System.Text.Json.JsonSerializer.Deserialize<WeatherForecast>(result);


                                if ($"{weatherForecast.message}" == "Successful operation" || $"{weatherForecast.message}" == "Successfull operation")
                                {

                                    ReadyMessages.SUCCES_CREDIT_SALES_MESSAGE();


                                    string a = weatherForecast.data.document_id;
                                    string b = weatherForecast.data.short_document_id;
                                    longids1 = a;
                                    label6.Text = a;
                                    label7.Text = b;
                                    ksatis();


                                    /*   st.insert_chec_pos_main(result, p_id, textEdit1.Text.ToString(), cash_, card_, umumi_mebleg_); */

                                }
                                else
                                {

                                    XtraMessageBox.Show(weatherForecast.message + weatherForecast.code);
                                }
                            }

                            //  ksatis();

                        }

                        catch (Exception ex)
                        {

                            XtraMessageBox.Show("ƏMƏLİYYATDA SƏHV \n\n" + ex.Message);
                        }






                    }

                }
            }
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            string uuid = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(textEdit3.Text) ||
             string.IsNullOrEmpty(textEdit9.Text) ||
             string.IsNullOrEmpty(textEdit12.Text))
            {
                XtraMessageBox.Show("MƏLUMATLAR TAM OLARAQ DOLDURULMALIDIR");
            }
            else
            {
                int y = cgs.test_proc_(textEdit5.Text, textEdit3.Text);

                if (y > 0)
                {
                    XtraMessageBox.Show("MÜŞTƏRİ ADI DÜZ DEYİL");
                }
                else
                {

                    if (string.IsNullOrEmpty(textEdit3.Text))
                    {
                        XtraMessageBox.Show("MÜŞTƏRİ SEÇİMİ EDİLMƏMİŞDİR", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(tZamin.Text))
                    {
                        XtraMessageBox.Show("ZAMİN SEÇİMİ EDİLMƏMİŞDİR", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    else
                    {
                        try
                        {
                            get_vahid();
                            string vahid = label8.Text;
                            int indexa = textEdit5.Text.LastIndexOf('-');
                            int indexb = textEdit5.Text.Length;
                            string idnos = (textEdit5.Text.Substring(indexa + 1, indexb - indexa - 1));
                            double price = Convert.ToDouble(tSalePrice.Text);
                            string testprice = Convert.ToString(price);
                            double cashab = Math.Round(Convert.ToDouble(tIlkinOdenis.Text), 2);
                            string personel = simpleLabelItem1.Text;
                            double control1 = Convert.ToDouble(idnos) + 115000;
                            


                            double quantity = Convert.ToDouble(tQuantity.Text);
                            decimal saleprice = Convert.ToDecimal(price);

                            int vatType = Convert.ToInt32(label9.Text);
                            int quantityType = Convert.ToInt32(vahid);

                            Sunmi.Item item = new Sunmi.Item()
                            {
                                name = textEdit9.Text,
                                code = mal_alisi_details_id,
                                quantity = quantity,
                                salePrice = saleprice,
                                vatType = vatType,
                                quantityType = quantityType
                            };

                            decimal cash = Convert.ToDecimal(cashab);
                            decimal creditPayment = Convert.ToDecimal(tTotal.Text);
                            Sunmi.Data data = new Sunmi.Data()
                            {
                                documentUUID = uuid,
                                cashPayment = cash,
                                cardPayment = 0,
                                cashierName = personel,
                                creditContract = textEdit12.Text,
                                creditPayment = creditPayment,
                                creditPayer = textEdit3.Text,
                                clientName = textEdit3.Text,
                                items = new List<Sunmi.Item> { item }
                            };

                            Sunmi.RootObject root = new Sunmi.RootObject
                            {
                                data = data,
                                operation = "sale"
                            };

                            string json = Sunmi.CreditSale(root);


                            var url =  label5.Text;


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




                                if ($"{weatherForecast.message}" == "Successful operation" || $"{weatherForecast.message}" == "Successful operation")
                                {

                                    ReadyMessages.SUCCES_CREDIT_SALES_MESSAGE();

                                    string a = weatherForecast.data.document_id;
                                    string b = weatherForecast.data.short_document_id;
                                    /* textEdit3.Text = a; */
                                    //MessageBox.Show(b);
                                    longids1 = weatherForecast.data.document_id;
                                    label6.Text = a;
                                    label7.Text = b;
                                    //  MessageBox.Show(weatherForecast.data.document_id + weatherForecast.code+ weatherForecast.message);
                                    ksatis();


                                    /*   st.insert_chec_pos_main(result, p_id, textEdit1.Text.ToString(), cash_, card_, umumi_mebleg_); */

                                }
                                else
                                {

                                    XtraMessageBox.Show(weatherForecast.message + weatherForecast.code);
                                }
                            }

                            //  ksatis();

                        }

                        catch (Exception)
                        {
                            XtraMessageBox.Show("Kassa ilə əlaqə yoxdur");

                        }
                    }

                }
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            decimal f = Convert.ToDecimal(tIlkinOdenis.Text);

            nagdkardkredit nk = new nagdkardkredit(f, this);
            nk.Show();
        }

        public void krediprint()
        {
            DialogResult pdr = printDialog1.ShowDialog();
            if (pdr == DialogResult.OK)
            {
                printDocument1.Print();
            }

        }
        public void gelen_data_negd_pos(decimal cash_, decimal card_, decimal umumi_mebleg_)
        {
            string casha = cash_.ToString();
            string card = card_.ToString();
            string umumi = umumi_mebleg_.ToString();

            string uuid = Guid.NewGuid().ToString();



            if (string.IsNullOrEmpty(textEdit3.Text) ||
             string.IsNullOrEmpty(textEdit9.Text) ||
             string.IsNullOrEmpty(textEdit12.Text)
             //(Convert.ToInt32(textEdit8.Text)==0)
             //string.IsNullOrEmpty(textEdit6.Text)
             )
            {
                XtraMessageBox.Show("MƏLUMATLAR TAM OLARAQ DOLDURULMALIDIR");
            }
            else
            {


                int y = cgs.test_proc_(textEdit5.Text.ToString(), textEdit3.Text.ToString());

                if (y > 0)
                {
                    XtraMessageBox.Show("MÜŞTƏRİ ADI DÜZ DEYİL");
                }
                else
                {

                    if (string.IsNullOrEmpty(textEdit3.Text))
                    {
                        XtraMessageBox.Show("MÜŞTƏRİ SEÇİMİ EDİLMƏMİŞDİR", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(tZamin.Text))
                    {
                        XtraMessageBox.Show("ZAMİN SEÇİMİ EDİLMƏMİŞDİR", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    else
                    {

                        try
                        {

                            get_vahid();
                            string vahid = label8.Text;
                            int indexa = textEdit5.Text.LastIndexOf('-');
                            int indexb = textEdit5.Text.Length;
                            string idnos = (textEdit5.Text.Substring(indexa + 1, indexb - indexa - 1));
                            double price = Convert.ToDouble(textEdit1.Text) + Convert.ToDouble(tIlkinOdenis.Text);
                            string testprice = Convert.ToString(price);
                            string personel = simpleLabelItem1.Text;
                            double control1 = Convert.ToDouble(idnos) + 115000;
                            var dataheader =

                          "  {\"data\": {" +

                "\"documentUUID\": \" " + uuid + "\"," +
                "\"cashPayment\":" + casha.Replace(",", ".") + "," +
                "\"cardPayment\":" + card.Replace(",", ".") + "," +
                "\"creditPayment\":" + tTotal.Text.Replace(",", ".") + "," +
                "\"bonusPayment\":0.0," +
                "\"depositPayment\":0.0," +
                "\"creditContract\":\"" + textEdit12.Text.Replace(",", ".") + "\"," +
                "\"creditPayer\":\"" + textEdit3.Text.Replace(",", ".") + "\"," +
                "\"clientName\":\"" + textEdit3.Text.Replace(",", ".") + "\"," +
                "\"items\":[{\"name\":\"" + textEdit9.Text + "\",\"code\":\"" + mal_alisi_details_id + "\",\"quantity\":" + tQuantity.Text.Replace(",", ".") + ",\"salePrice\":" + testprice.Replace(",", ".") + ",\"vatType\":" + label9.Text.Replace(",", ".") + ",\"quantityType\":" + vahid + "}]," +
                "\"clientTotalBonus\":0.0,\"clientEarnedBonus\":0.0," +
                "\"clientBonusCardNumber\":\"\"," +
                "\"cashierName\":\"" + personel + "\"," +
                "\"currency\":\"AZN\"}," +
                "\"operation\":\"sale\"," +
                "\"username\":\"username\"," +
                "\"password\":\"password\"} "




                                ;



                            var url =  label5.Text;


                            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                            httpRequest.Method = "POST";

                            httpRequest.Accept = "application/json;charset=utf-8";
                            httpRequest.ContentType = "application/json;charset=utf-8";


                            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                            {
                                streamWriter.Write(dataheader);
                            }

                            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                var result = streamReader.ReadToEnd();
                                //MessageBox.Show(result.ToString());

                                WeatherForecast weatherForecast =
                                 System.Text.Json.JsonSerializer.Deserialize<WeatherForecast>(result);

                                //MessageBox.Show($"{weatherForecast.message}");

                                if ($"{weatherForecast.message}" == "Successful operation" || $"{weatherForecast.message}" == "Successful operation")
                                {

                                    ReadyMessages.SUCCESS_SALES_MESSAGE();

                                    string a = weatherForecast.data.document_id;
                                    string b = weatherForecast.data.short_document_id;
                                    /* textEdit3.Text = a; */
                                    longids1 = a;
                                    label6.Text = a;
                                    label7.Text = b;
                                    ksatis();


                                    /*   st.insert_chec_pos_main(result, p_id, textEdit1.Text.ToString(), cash_, card_, umumi_mebleg_); */

                                }
                                else
                                {

                                    XtraMessageBox.Show(weatherForecast.message + weatherForecast.code);
                                }
                            }

                            //  ksatis();

                        }

                        catch (Exception)
                        {
                            XtraMessageBox.Show("Kassa ilə əlaqə yoxdur");

                        }






                    }

                }
            }




        }
        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            double totalmeblag = Convert.ToDouble(textEdit1.Text) + Convert.ToDouble(tIlkinOdenis.Text);
            Font myFont = new Font("Calibri", 18);
            Font myFont2 = new Font("Calibri", 12);
            Font myFont3 = new Font("Calibri", 10);
            SolidBrush sbrush = new SolidBrush(Color.Black);
            Pen myPen = new Pen(Color.Black);

            //Bu kısımda sipariş formu yazısını ve çizgileri yazdırıyorum
            e.Graphics.DrawString("Tarix:" + DateTime.Now.ToString("dd/MM/yyyy"), myFont3, sbrush, 600, 80);

            e.Graphics.DrawLine(myPen, 50, 100, 750, 100);
            e.Graphics.DrawLine(myPen, 50, 160, 750, 160);

            e.Graphics.DrawString("KREDİT SATIŞI", myFont, sbrush, 270, 120);
            e.Graphics.DrawString("KASSİR:  " + simpleLabelItem1.Text, myFont3, sbrush, 50, 180);
            e.Graphics.DrawString("Müqavilə №:  " + textEdit12.Text, myFont3, sbrush, 50, 200);
            e.Graphics.DrawString("MÜŞTƏRİ ADI:  " + textEdit3.Text, myFont3, sbrush, 250, 200);
            e.Graphics.DrawString("MƏHSUL ADI:  " + textEdit9.Text, myFont3, sbrush, 50, 220);
            e.Graphics.DrawString("KREDİT MÜDDƏTİ(AY):  " + tMuddetAy.Text, myFont3, sbrush, 50, 240);
            e.Graphics.DrawString("İLKİN ÖDƏNİŞ:  " + Convert.ToDouble(tIlkinOdenis.Text).ToString(), myFont3, sbrush, 50, 260);
            e.Graphics.DrawString("KREDİT MƏBLƏĞ:  " + textEdit1.Text, myFont3, sbrush, 50, 280);
            e.Graphics.DrawString("TOPLAM MƏBLƏĞ:  " + totalmeblag.ToString(), myFont3, sbrush, 50, 300);



            e.Graphics.DrawLine(myPen, 50, 320, 750, 320);


            myFont = new Font("Calibri", 8, FontStyle.Bold);
            e.Graphics.DrawString("NO", myFont, sbrush, 50, 328);
            e.Graphics.DrawString("QRAFİK ÜZRƏ ÖDƏNİŞ TARİXİ", myFont, sbrush, 80, 328);
            e.Graphics.DrawString("ƏSAS MƏBLƏĞDƏN QALIQ", myFont, sbrush, 250, 328);
            //e.Graphics.DrawString("AYLIQ ƏSAS MƏBLƏĞ", myFont, sbrush, 400, 328);
            //e.Graphics.DrawString("AYLIQ FAİZ", myFont, sbrush, 530, 328);
            e.Graphics.DrawString("AYLIQ CƏM ÖDƏNİŞ", myFont, sbrush, 610, 328);

            e.Graphics.DrawLine(myPen, 50, 348, 750, 348);




            int y = 360;

            StringFormat myStringFormat = new StringFormat();
            myStringFormat.Alignment = StringAlignment.Far;

            decimal gTotal = 0;

            foreach (ListViewItem lvi in listView1.Items)
            {
                e.Graphics.DrawString(lvi.SubItems[0].Text, myFont, sbrush, 50, y);
                e.Graphics.DrawString(lvi.SubItems[1].Text, myFont, sbrush, 80, y);
                e.Graphics.DrawString(lvi.SubItems[2].Text, myFont, sbrush, 250, y);
                e.Graphics.DrawString(lvi.SubItems[3].Text, myFont, sbrush, 400, y);
                e.Graphics.DrawString(lvi.SubItems[4].Text, myFont, sbrush, 530, y);
                e.Graphics.DrawString(lvi.SubItems[5].Text, myFont, sbrush, 610, y);
                //  e.Graphics.DrawString(lvi.Text, myFont, sbrush, 220, y);
                ////decimal bFiyat = Convert.ToDecimal(lvi.SubItems[2].Text);
                ////decimal fiyat = Convert.ToDecimal(lvi.SubItems[1].Text) * Convert.ToDecimal(lvi.SubItems[2].Text);
                ////gTotal += fiyat;
                //  e.Graphics.DrawString(bFiyat.ToString("c"), myFont, sbrush, 530, y, myStringFormat);
                //  e.Graphics.DrawString(fiyat.ToString("c"), myFont, sbrush, 700, y, myStringFormat);

                y += 20;

            }

            e.Graphics.DrawLine(myPen, 50, y, 750, y);
            //  e.Graphics.DrawString(gTotal.ToString("c"), myFont, sbrush, 700, y + 10, myStringFormat);

        }

        private void textEdit9_EditValueChanged(object sender, EventArgs e)
        {

        }
    }

}

