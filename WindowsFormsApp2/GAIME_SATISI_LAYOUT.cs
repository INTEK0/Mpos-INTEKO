using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using WindowsFormsApp2;
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
using WindowsFormsApp2.Helpers;
using DevExpress.XtraGrid.Localization;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class GAIME_SATISI_LAYOUT : DevExpress.XtraEditors.XtraForm
    {
        private readonly MAINSCRRENS frm1;
        public static string radio = "";
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
        public GAIME_SATISI_LAYOUT(int _userid, MAINSCRRENS frm_)
        {
            InitializeComponent();
            _g_user_id = _userid;
            frm1 = frm_;
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }
        public void techizatci_axtar(string techizatci_adi, string Mehsul_ad, string satis_giymeti,
            int mal_Details, string anbar_galig, string _edv_)
        {
            textEdit2.Text = techizatci_adi.ToString();
            textEdit9.Text = Mehsul_ad.ToString();
            textEdit6.Text = satis_giymeti.ToString();
            //    lookUpEdit8GEtData_yeni(Convert.ToInt32(mal_Details));
            mal_alisi_details_id = mal_Details.ToString();
            anbargalig = anbar_galig.ToString();
            edv = _edv_;

        }
        public void MUSTERI(string ID, string MUSTERI_AD)
        {
            labelControl9.Text = ID;
            textEdit3.Text = MUSTERI_AD;
        }
        private string qeryString = "EXEC   dbo.GAIME_SATISI_EMELIYYAT_NOMRE";


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
        private void GAIME_SATISI_LAYOUT_Load(object sender, EventArgs e)
        {
            clear();
            //labelControl1.Visible = false;
            InitLookUpEdit_();
            radioButton1.Checked = true;
            DateTime dateTime = DateTime.UtcNow.Date;
            //dateEdit1.Text = dateTime.ToString();

            dateEdit1.Text = dateTime.ToShortDateString();
            gridControl1.TabStop = false;
            textEdit5.Enabled = false;
            GETKOD();

            //  lookUpEdit8GEtData_yeni();
            //ANBAR_LOAD();
            searchlookupedit();
            //dt = new DataTable(CATEGORIES_TABLE);


        }


        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            radio = radioButton1.Text.ToString();

            if (radioButton1.Checked == true)
            {
                textEdit17.Enabled = true;
                textEdit1.Enabled = true;
            }
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            radio = radioButton3.Text.ToString();
            if (radioButton3.Checked == true)
            {
                textEdit17.Enabled = false;
                textEdit1.Enabled = false;
            }
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

            catch (Exception ex)

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
                textEdit4.Text = dr["yekun_mebleg"].ToString();
            }
            connection.Close();
        }

        private void textEdit7_TextChanged_1(object sender, EventArgs e)
        {
            textEdit13.Text = "0.00";
            getmebleg(textEdit8.Text, textEdit6.Text, textEdit7.Text, textEdit13.Text);
        }

        private void textEdit13_TextChanged_1(object sender, EventArgs e)
        {
            textEdit7.Text = "0.00";
            getmebleg(textEdit8.Text, textEdit6.Text, textEdit7.Text, textEdit13.Text);
        }

        private void textEdit8_TextChanged_1(object sender, EventArgs e)
        {
            getmebleg(textEdit8.Text, textEdit6.Text, textEdit7.Text, textEdit13.Text);
        }

        private void textEdit6_TextChanged_1(object sender, EventArgs e)
        {
            getmebleg(textEdit8.Text, textEdit6.Text, textEdit7.Text, textEdit13.Text);
        }
        CRUD_GAIME_SATISI cgs = new CRUD_GAIME_SATISI();
        int b;





        private void gridView1_FocusedRowChanged_2(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {


                int paramValue = Convert.ToInt32(dr[0]);

                //labelControl1.Text = dr[0].ToString();

                //XtraMessageBox.Show(paramValue.ToString());
                string queryString =
                   " select  D.GAIME_SATISI_DETAILS_ID, CT.SIRKET_ADI,m.MUSTERI,MD.MEHSUL_ADI,MD.MEHSUL_KODU,d.MIGDARI" +
                   ",d.SATIS_GIYMETI, " +
                   " CAST(replace(D.MIGDARI,',','.') " +
                   "AS DECIMAL(9,2))*CAST(replace(D.SATIS_GIYMETI,',','.') AS DECIMAL(9,2)) " +
                   " AS YEKUN_MEBLEG,d.ENDIRIM_AZN,d.ENDIRIM_FAIZ,d.ENDIRIM_MEBLEGI,d.GEYD,cs.STORE_NAME " + " from GAIME_SATISI_MAIN m inner join GAIME_SATISI_DETAILS d " +
                   " on m.GAIME_SATISI_MAIN_ID = d.GAIME_SATISI_MAIN_ID " + " inner JOIN MAL_ALISI_DETAILS MD ON MD.MAL_ALISI_DETAILS_ID=D.MAL_DETAILS_ID " +
                   " INNER JOIN MAL_ALISI_MAIN MM ON MM.MAL_ALISI_MAIN_ID=MD.MAL_ALISI_MAIN_ID " +
                   " INNER JOIN COMPANY.TECHIZATCI CT ON CT.TECHIZATCI_ID=MM.TECHIZATCI_ID " +
                   " inner join COMPANY.STORE cs on cs.STOREID=d.MAGAZA " +
                " WHERE D.GAIME_SATISI_DETAILS_ID=@pricepoint ";


                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@pricePoint", paramValue);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            label1.Text = dr[0].ToString();
                            textEdit3.Text = reader[2].ToString();
                            textEdit9.Text = reader[3].ToString();
                            mehsul_kod = reader[4].ToString();
                            textEdit8.Text = reader[5].ToString();
                            textEdit6.Text = reader[6].ToString();
                            textEdit4.Text = reader[7].ToString();
                            textEdit13.Text = reader[8].ToString();
                            textEdit7.Text = reader[9].ToString();
                            textEdit10.Text = reader[10].ToString();
                            memoEdit1.Text = reader[11].ToString();
                            lookUpEdit1.Text = reader[12].ToString();
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                    }
                }
            }

        }





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

                textEdit14.Text = dr["YEKUN_MEBLEG"].ToString();
                textEdit1.Text = dr["ODENILEN_MEBLEG"].ToString();
                textEdit12.Text = dr["GALIG"].ToString();
            }
            connection.Close();
        }

        private void lookUpEdit8GEtData_yeni()
        {
            //int id = Convert.ToInt32(lookUpEdit7.EditValue.ToString());



            string strQuery = "select WAREHOUSE_ID,WAREHOUSE_NAME as 'Anbar' from COMPANY.WAREHOUSE";
            //string strQuery = "select GRUPLAR_ID No,GRUP as N'Qrup' " +
            //   " From GRUPLAR  where IXTISAS_ID=@IDD";

            SqlCommand cmd = new SqlCommand(strQuery);

            //cmd.Parameters.AddWithValue("@IDD", a);

            DataTable dt = GetData(cmd);




            //lookUpEdit7.Properties.DisplayMember = "Anbar";
            //lookUpEdit7.Properties.ValueMember = "WAREHOUSE_ID";
            //lookUpEdit7.Properties.DataSource = dt;
            //lookUpEdit7.Properties.NullText = "--Seçin--";
            //lookUpEdit7.Properties.PopulateColumns();
            //lookUpEdit7.Properties.Columns[0].Visible = false;



        }



        private void magazaLoad(string a)
        {
            //int id = Convert.ToInt32(lookUpEdit7.EditValue.ToString());




            string strQuery = "SELECT distinct( STOREID), OBYEKT FROM[dbo].[fn_MAGAZA_ANBAR_LOAD]('') where ANBAR = @IDD";
            //string strQuery = "select GRUPLAR_ID No,GRUP as N'Qrup' " +
            //   " From GRUPLAR  where IXTISAS_ID=@IDD";

            SqlCommand cmd = new SqlCommand(strQuery);

            cmd.Parameters.AddWithValue("@IDD", a);

            DataTable dt = GetData(cmd);




            lookUpEdit1.Properties.DisplayMember = "OBYEKT";
            lookUpEdit1.Properties.ValueMember = "STOREID";
            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.NullText = "--Seçin--";
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns[0].Visible = false;



        }

        private void gridclear()
        {
            gridControl1.DataSource = null;
        }

        public void clear()
        {
            textEdit9.Text = "";
            textEdit8.Text = "";
            textEdit6.Text = "";
            textEdit7.Text = "";
            textEdit13.Text = "";
            textEdit10.Text = "";
            textEdit17.Text = "";
            textEdit1.Text = "";
            memoEdit1.Text = "";
        }

        public void clear_details()
        {
            //textEdit5.Text = "";
            textEdit9.Text = "";
            textEdit8.Text = "";
            textEdit6.Text = "";
            textEdit7.Text = "";
            textEdit13.Text = "";
            textEdit10.Text = "";
            textEdit17.Text = "";
            textEdit1.Text = "";
            memoEdit1.Text = "";
            //InitLookUpEdit_();
            textEdit3.Text = "";
            //textEdit9.Enabled = false;
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {

            gridView1.ShowPrintPreview();

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            MUSTERI_AXTAR M = new MUSTERI_AXTAR(this);
            M.Show();
        }






        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //   gridView1.ShowPrintPreview();
            // cap et
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Müştəri adı və məhsul adı kontrolu
            if (string.IsNullOrEmpty(textEdit3.Text) || string.IsNullOrEmpty(textEdit9.Text))
            {
                XtraMessageBox.Show("MƏLUMATLAR TAM OLARAQ DOLDURULMALIDIR");
            }
            else
            {

                if (string.IsNullOrEmpty(textEdit3.Text))
                {
                    XtraMessageBox.Show("MÜŞTƏRİ ADI DAXİL EDİLMƏMİŞDİR");
                }
                else
                {
                    gridView1.MoveFirst();

                    try
                    {
                        string gaime_;
                        string mebleg_;
                        if (string.IsNullOrEmpty(textEdit17.Text))
                        {
                            gaime_ = "a";
                        }

                        else
                        {
                            gaime_ = textEdit17.Text;
                        }

                        if (string.IsNullOrEmpty(textEdit1.Text)) //əsas məbləğ
                        {
                            mebleg_ = "0.0";
                        }
                        else
                        {
                            mebleg_ = textEdit1.Text;
                        }

                        string edv_siz = "0.0";
                        string ed = "0.0";

                        if (string.IsNullOrEmpty(textEdit1.Text))
                        {
                            edv_siz = "0.0";
                        }
                        if (string.IsNullOrEmpty(textEdit11.Text))
                        {
                            ed = "0.0";
                        }

                        int ret = cgs.GAIME_SATISI_MAIN(textEdit5.Text,
                            gaime_,
                            mebleg_,
                            Convert.ToDateTime(dateEdit1.Text),
                            radio.Trim(),
                            textEdit3.Text,
                            _g_user_id,
                            edv_siz,
                            ed,
                            Convert.ToInt32(labelControl9.Text));



                        if (ret > 0)
                        {

                            string gaime_nomre = gaime_;


                            string an_galig = anbargalig.Replace('.', ',');
                            decimal anbar_ga = Convert.ToDecimal(an_galig);

                            string mig = textEdit8.Text;
                            string mig_d = mig.Replace('.', ',');

                            decimal migdar_ = Convert.ToDecimal(mig_d);

                            
                            int status_menfi_ = getmenfi_status();
                            if (status_menfi_ > 0)
                            {
                                //icaze ver menfi aciqdir 

                                int xx = cgs.GAIME_SATISI_DETAILS(ret,
                                    Convert.ToDateTime(dateEdit1.Text),
                                    textEdit5.Text,
                                    radio,
                                    gaime_nomre,
                                    textEdit1.Text,
                                    Convert.ToInt32(mal_alisi_details_id),
                                    Convert.ToInt32(lookUpEdit1.EditValue.ToString()),
                                    0,
                                    textEdit8.Text,
                                    textEdit6.Text,
                                    textEdit7.EditValue.ToString(),
                                    textEdit13.Text,
                                    textEdit10.Text,
                                    textEdit4.Text,
                                    memoEdit1.Text,
                                    Convert.ToDateTime(dateEdit1.Text),
                                    textEdit1.Text,
                                    textEdit11.Text);


                                FormHelpers.Log($"{textEdit8.Text} ədəd {textEdit9.Text} məhsulu qaimə satışı ilə satıldı");

                                if (xx > 4)
                                {
                                    XtraMessageBox.Show("ƏMƏLİYYAT DAHA ÖNCƏ İCRA EDİLMİŞDİR");

                                }

                                getmebleg_yekun_galig(textEdit5.Text);
                            }
                            else
                            {

                                if (migdar_ > anbar_ga)
                                {
                                    XtraMessageBox.Show("SATILAN MƏHSULUN MİQDARI ANBAR QALIĞINDAN BÖYÜK OLA BİLMƏZ");
                                }
                                else
                                {
                                    //satis
                                    int xx = cgs.GAIME_SATISI_DETAILS(ret,
                             Convert.ToDateTime(dateEdit1.Text),
                             textEdit5.Text,
                             radio,
                             gaime_nomre,
                             textEdit1.Text.ToString(),//mebleg
                             Convert.ToInt32(mal_alisi_details_id),
                             Convert.ToInt32(lookUpEdit1.EditValue.ToString()),
                             0, 
                             textEdit8.Text, 
                             textEdit6.Text, 
                             textEdit7.Text,
                             textEdit13.Text,
                             textEdit10.Text,
                             textEdit4.Text,
                             memoEdit1.Text, 
                             Convert.ToDateTime(dateEdit1.Text),
                             textEdit1.Text,
                             textEdit11.Text
                             );
                                    FormHelpers.Log($"{textEdit8.Text} ədəd {textEdit9.Text} məhsulu qaimə satışı ilə satıldı");

                                    if (xx > 4)
                                    {
                                        XtraMessageBox.Show("ƏMƏLİYYAT DAHA ÖNCƏ İCRA EDİLMİŞDİR");

                                    }

                                    getmebleg_yekun_galig(textEdit5.Text);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        //MessageBox.Show(ex.Message);
                    }
                    GetallData(textEdit5.Text);

                    gridView1.MoveFirst();
                    clear();
                    textEdit2.Text = "";

                }
            }

        }

        public int getmenfi_status()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                string queryString = "SELECT STATUS FROM MENFI_AC_BAGLA ";
                SqlCommand command = new SqlCommand(queryString, connection);


                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                int number = dt.Rows[0].Field<int>("STATUS");


                return number;

            }
            catch (Exception e)
            {

                XtraMessageBox.Show("Xəta!\n" + e);
                return -100;
            }
        }
        
        private void button21_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();


            foreach (int item in selectedRows)
            {
                var row = gridView1.GetDataRow(item);


                int B = Convert.ToInt32(row[0].ToString());
                if (B > 0)
                {
                    cgs.GAIME_SATISI_DELETE(B);
                    FormHelpers.Log($"{row[2]} Müştərisinin {row[3]} məhsulu qaimə satış tarixçəsindən silindi");
                }
            }


            GetallData(textEdit5.Text);
            textEdit9.Text = null;
            getmebleg_yekun_galig(textEdit5.Text);
        }
        public void GetallData_id(string id)
        {

            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                string queryString = "select  D.GAIME_SATISI_DETAILS_ID, " +
                    "CT.SIRKET_ADI,m.MUSTERI,MD.MEHSUL_ADI,MD.MEHSUL_KODU,d.MIGDARI,d.SATIS_GIYMETI, " +
                   " CAST(D.MIGDARI AS DECIMAL(9, 2)) * CAST(D.SATIS_GIYMETI AS DECIMAL(9, 2)) AS YEKUN_MEBLEG " +
                   " from GAIME_SATISI_MAIN m inner " +
                   " join GAIME_SATISI_DETAILS d " +
                   " on m.GAIME_SATISI_MAIN_ID = d.GAIME_SATISI_MAIN_ID " +
                   " inner JOIN MAL_ALISI_DETAILS MD ON MD.MAL_ALISI_DETAILS_ID = D.MAL_DETAILS_ID " +
                   " INNER JOIN MAL_ALISI_MAIN MM ON MM.MAL_ALISI_MAIN_ID = MD.MAL_ALISI_MAIN_ID " +
                   " INNER JOIN COMPANY.TECHIZATCI CT ON CT.TECHIZATCI_ID = MM.TECHIZATCI_ID " +
                   " where GAIME_SATISI_DETAILS_ID =@pricepoint " +
                   " union all select 0,'','','','','','',0 ";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricepoint", id);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }
        }
        public void get_em(string em_)
        {
            textEdit5.Text = em_.ToString();
        }
        public void GetallData(string emeliyyat_nomre)
        {

            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                string queryString = "SELECT * FROM dbo.fn_GAIME_SATISI_LOAD(@pricepoint) ";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricepoint", emeliyyat_nomre);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textEdit5.Text))
            {

            }
            else
            {
                int x = cgs.GAIME_SATISI_check_status(textEdit5.Text);
                if (x > 0)
                {
                    gridclear();
                    clear();
                    //searchLookUpEdit1.EditValue = null;
                    textEdit9.Text = "";
                    //  lookUpEdit1.EditValue = null;
                    textEdit3.Text = "";
                    GETKOD();
                    textEdit12.Text = "";
                    textEdit14.Text = "";
                    textEdit2.Text = "";
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
            gridControl1.DataSource = null;
            textEdit6.Text = "";
            textEdit2.Text = "";
            GETKOD();
            textEdit14.Text = "";
            textEdit12.Text = "";
            //InitLookUpEdit_();
        }



        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            MUSTERI_AXTAR M = new MUSTERI_AXTAR(this);
            M.ShowDialog();
        }

        //MUSTERI_SIYAHISI M;
        //private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    //MUSTERI_SIYAHISI M = new MUSTERI_SIYAHISI();
        //    //M.Show();
        //    if (Application.OpenForms["MUSTERI_SIYAHISI"] != null)
        //    {
        //        var Main = Application.OpenForms["MUSTERI_SIYAHISI"] as MUSTERI_SIYAHISI;
        //        if (Main != null)
        //        {

        //        }
        //        // Main.Close();
        //    }
        //    else
        //    {
        //        M = new MUSTERI_SIYAHISI();
        //        M.Show();
        //    }
        //}
        GAIME_SATIS_DETAILS_LAYOUT gs;
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //GAIME_SATIS_DETAILS gs = new GAIME_SATIS_DETAILS(this);
            //gs.Show();
            if (Application.OpenForms["GAIME_SATIS_DETAILS_LAYOUT"] != null)
            {
                var Main = Application.OpenForms["GAIME_SATIS_DETAILS_LAYOUT"] as GAIME_SATIS_DETAILS_LAYOUT;
                if (Main != null)
                {

                }
                // Main.Close();
            }
            else
            {
                gs = new GAIME_SATIS_DETAILS_LAYOUT(this);
                gs.Show();
            }
        }

        private void lookUpEdit7_TextChanged_1(object sender, EventArgs e)
        {
            //magazaLoad(lookUpEdit7.Text.ToString());
        }

        private void textEdit10_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            radio = radioButton4.Text.ToString();
            if (radioButton4.Checked == true)
            {
                textEdit17.Enabled = true;
                textEdit1.Enabled = true;
            }
        }

        private void gridView1_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //update focus row 
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {


                int paramValue = Convert.ToInt32(dr[0]);
                label1.Text = paramValue.ToString();

                //update_mal_details_id = Convert.ToInt32(dr[0]);
                //XtraMessageBox.Show(paramValue.ToString());
                string queryString =
                    " select  md.MEHSUL_ADI,gd.MAL_DETAILS_ID,GAIME_NOM,gd.MIGDARI,gd.SATIS_GIYMETI,gd.ENDIRIM_FAIZ,gd.ENDIRIM_AZN," +
                     " gd.ENDIRIM_MEBLEGI,gd.YEKUN_MEBLEG,gd.GEYD,cs.STORE_NAME,gm.MUSTERI " +
                   " from GAIME_SATISI_DETAILS gd " +
                  " inner join MAL_ALISI_DETAILS md on md.MAL_ALISI_DETAILS_ID = gd.MAL_DETAILS_ID " +
                    " inner join COMPANY.STORE cs on cs.STOREID = gd.MAGAZA " +
                  " inner join GAIME_SATISI_MAIN gm on gm.GAIME_SATISI_MAIN_ID = gd.GAIME_SATISI_MAIN_ID " +
                  " where gd.GAIME_SATISI_DETAILS_ID = @pricePoint ";

                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@pricePoint", paramValue);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            //            
                            //textBox1.Text = reader[1].ToString();
                            textEdit9.Text = reader[0].ToString();
                            mal_alisi_details_id = reader[1].ToString();
                            textEdit17.Text = reader[2].ToString();
                            textEdit8.Text = reader[3].ToString();
                            textEdit6.Text = reader[4].ToString();
                            textEdit7.Text = reader[5].ToString();
                            textEdit13.Text = reader[6].ToString();
                            textEdit10.Text = reader[7].ToString();
                            textEdit4.Text = reader[8].ToString();
                            memoEdit1.Text = reader[9].ToString();
                            lookUpEdit1.Text = reader[10].ToString();
                            textEdit3.Text = reader[11].ToString();


                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                    }

                }

            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            TECHIZATCI_SEC T = new TECHIZATCI_SEC(this);
            T.ShowDialog();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //update focus row 
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {


                int paramValue = Convert.ToInt32(dr[0]);
                label1.Text = paramValue.ToString();

                //update_mal_details_id = Convert.ToInt32(dr[0]);
                //XtraMessageBox.Show(paramValue.ToString());
                string queryString =
                    " select  md.MEHSUL_ADI,gd.MAL_DETAILS_ID,GAIME_NOM,gd.MIGDARI,gd.SATIS_GIYMETI,gd.ENDIRIM_FAIZ,gd.ENDIRIM_AZN," +
                     " gd.ENDIRIM_MEBLEGI,gd.YEKUN_MEBLEG,gd.GEYD,cs.STORE_NAME,gm.MUSTERI " +
                   " from GAIME_SATISI_DETAILS gd " +
                  " inner join MAL_ALISI_DETAILS md on md.MAL_ALISI_DETAILS_ID = gd.MAL_DETAILS_ID " +
                    " inner join COMPANY.STORE cs on cs.STOREID = gd.MAGAZA " +
                  " inner join GAIME_SATISI_MAIN gm on gm.GAIME_SATISI_MAIN_ID = gd.GAIME_SATISI_MAIN_ID " +
                  " where gd.GAIME_SATISI_DETAILS_ID = @pricePoint ";

                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@pricePoint", paramValue);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            //            
                            //textBox1.Text = reader[1].ToString();
                            textEdit9.Text = reader[0].ToString();
                            mal_alisi_details_id = reader[1].ToString();
                            textEdit17.Text = reader[2].ToString();
                            textEdit8.Text = reader[3].ToString();
                            textEdit6.Text = reader[4].ToString();
                            textEdit7.Text = reader[5].ToString();
                            textEdit13.Text = reader[6].ToString();
                            textEdit10.Text = reader[7].ToString();
                            textEdit4.Text = reader[8].ToString();
                            memoEdit1.Text = reader[9].ToString();
                            lookUpEdit1.Text = reader[10].ToString();
                            textEdit3.Text = reader[11].ToString();


                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                    }

                }

            }
        }
        private List<Account> datasource_;
        private void InitLookUpEdit_()
        {
            datasource_ = new List<Account>();
            Random random = new Random();
            datasource_.Add(new Account("MƏRKƏZ OBYEKT") { ID = 1002 });
            //  datasource.Add(new Account("S"){ ID = random.Next(100)});
            lookUpEdit1.Properties.DataSource = datasource_;
            lookUpEdit1.Properties.DisplayMember = "Name";
            lookUpEdit1.Properties.ValueMember = "ID";
        }

        private void GAIME_SATISI_LAYOUT_Shown(object sender, EventArgs e)
        {
            if (datasource_.Count == 1)
                lookUpEdit1.EditValue = datasource_[0].ID;
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //rowcellclick

        }

        private void GAIME_SATISI_LAYOUT_FormClosing(object sender, FormClosingEventArgs e)
        {
            // frm1.getall();
        }

        private void textEdit4_TextChanged(object sender, EventArgs e)
        {
            //yekun mebleg
            if (string.IsNullOrEmpty(textEdit4.Text)) { }
            else
            {
                getmebleg_(textEdit4.Text.ToString(), edv);
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

    }

}

