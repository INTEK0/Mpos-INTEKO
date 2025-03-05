using DevExpress.CodeParser;
using DevExpress.DashboardCommon.Viewer;
using DevExpress.Data.Linq.Helpers;
using DevExpress.Map.Native;
using DevExpress.Pdf.Native.BouncyCastle.Utilities.Net;
using DevExpress.PivotGrid.PivotQuery;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsApp2.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.NKA;
using WindowsFormsApp2.Reports;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.Helpers.FormHelpers;
using Method = RestSharp.Method;

namespace WindowsFormsApp2
{
    public partial class POS_LAYOUT_NEW : BaseForm
    {
        private readonly bool MessageVisible = FormHelpers.SuccessMessageVisible();

        private int pagesCount = 1;
        private int za;
        private DataTable dt;
        private SqlDataAdapter da;

        public string customer, customervoen, obyektkod, obyetname, obyektadres, nkamodel, nkanumber, nkarnumber, barkodsa, bankttnmd, bankttnminputdata;
        public string YekunMebleg, zdocument, nacilma, ndoc, firstDocNumber, lastDocNumber, saleCount, saleSum, saleCashSum, saleCashlessSum, salePrepaymentSum, saleCreditSum, saleBonusSum, saleVatAmounts, depositCount, moneyBackCount, moneyBackSum, moneyBackCashSum, moneyBackCashlessSum, moneyBackVatAmounts, vatPercent, vatPercentm, vatSuma, vatSumma, rno, sdocumentid, fissayi, gunfissayi, odenen, qaliq, edvdenazada1, edvhesap1, edvdenazada2, edvhesap2, deposita, withdrawa, nhtarix, depositSum;


        AutoCompleteStringCollection coll_ = new AutoCompleteStringCollection();
        public string casha2, tota2;
        public int clmns, height;

        List<string> bankdizi = new List<string>();
        List<string> bankdizic = new List<string>();
        List<string> bankdizis = new List<string>();
        private Customer _customer;
        private Doctor _doctor;

        public POS_LAYOUT_NEW()
        {
            InitializeComponent();
            GridLocalizer.Active = new MyGridLocalizer();
        }



        private void POS_LAYOUT_NEW_Load(object sender, EventArgs e)
        {


            textBox5.Text = "MƏHSUL ADI";

            textBox5.ForeColor = Color.LightGray;
            textBox5.Font = new Font("Tahoma", 16, FontStyle.Bold);
            //labelControl21.Text = "ALIŞ";
            lModel.Visible = true;
            Auto();
            gridControl1.TabStop = true;
            tUsername.Text = DbProsedures.GetUser().NameSurname;
            textEdit2.Text = DateTime.Now.ToShortDateString();

            //st.del_tr();
            textEdit1.Text = DbProsedures.GET_SalesProcessNo();
            textEdit11.Text = DbProsedures.GET_TotalSalesCount();
            get_ip_model();
            bankttnmWrite();

            textEdit11.Enabled = false;
            textEdit1.Enabled = false;
            CalculationDelete();
            tileproduct();
            BasketDataControl();
            PrintKassaOrPrinterShow();
            //ClinicModule();
            tBarcode.Focus();
        }

        private void bankttnmWrite()
        {
            string fileName = (Application.StartupPath + @"\BankTTNM.txt");

            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            string data_ = sw.ReadLine();

            bankttnmd = data_;

            sw.Close();
            fs.Close();
        }

        satis_json st = new satis_json();

        private void PrintKassaOrPrinterShow()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("SendToKassa").ToString());
            if (control)
            {
                chSendToPrinter.Visible = true;
                chSendToKassa.Visible = true;
                layoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                chSendToKassa.Checked = true;
            }
            else
            {
                chSendToPrinter.Visible = false;
                chSendToKassa.Visible = false;
                layoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        //private void ClinicModule()
        //{
        //    bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("ClinicModule").ToString());
        //    if (control)
        //    {
        //        layoutControlItem56.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        //    }
        //    else
        //    {
        //        layoutControlItem56.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //    }
        //}

        private void CalculationDelete()
        {
            using (SqlConnection conn = new SqlConnection(DbHelpers.DbConnectionString))
            {
                conn.Open();
                string deleteQuery = $"DELETE FROM calculation WHERE userId = {Properties.Settings.Default.UserID}";

                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Auto()
        {
            da = new SqlDataAdapter("select * from dbo.POS_autocomplete_search_mehsul_Adi_distinct()", DbHelpers.DbConnectionString);

            DataTable dt = new DataTable();

            da.Fill(dt);
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    coll_.Add(dt.Rows[i]["MEHSUL_ADI"].ToString());
                }
            }

            textBox5.AutoCompleteCustomSource = coll_;
            textBox5.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox5.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void tBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode is Keys.Enter)
            {
                string kontrol = tBarcode.Text;
                string kod;
                string kg;
                string gr;
                string barkod;

                if (kontrol.Substring(0, 1) == "0" && kontrol.Count() is 12)
                {
                    if (kontrol.Substring(0, 1) == "0")
                    {
                        if (kontrol.Count() is 12)
                        {
                            kontrol = "0" + kontrol;
                        }
                        kod = kontrol.Substring(2, 5);
                        kg = kontrol.Substring(7, 2);
                        gr = kontrol.Substring(9, 3);


                        SqlConnection conn = new SqlConnection();
                        SqlCommand cmd = new SqlCommand();
                        conn.ConnectionString = Properties.Settings.Default.SqlCon;
                        conn.Open();

                        string query = $@"select BARKOD from [MAL_ALISI_DETAILS] where [MAL_ALISI_DETAILS_ID]={kod}";
                        cmd.Connection = conn;
                        cmd.CommandText = query;

                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            barkodsa = dr["BARKOD"].ToString();
                            barkod = dr["BARKOD"].ToString();
                            textEdit10.Text = kg + "," + gr;
                            getall(barkod);
                            get(textEdit1.Text);

                            get_say_birmal(barkod, textEdit1.Text);
                        }


                        int rowHandle = gridView1.LocateByValue("MAL_ALISI_DETAILS_ID", Int32.Parse(kod));
                        if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                            gridView1.FocusedRowHandle = rowHandle;
                        gridView1.SelectRow(rowHandle);

                        tBarcode.Text = string.Empty;
                        //deyisilmis
                        textEdit10.Text = kg + "," + gr;
                        get_cem(textEdit1.Text.ToString());

                        int kayitsayisi = gridView1.RowCount;
                        //  gridView1.MoveLast();


                        string productId = gridView1.GetRowCellValue(rowHandle, "MAL_ALISI_DETAILS_ID").ToString();

                        st.del_migdarnewsa_calculation(productId,
                                                       textEdit10.Text,
                                                       textEdit1.Text);


                        get(textEdit1.Text);
                        get_say_birmal(tBarcode.Text.ToString(), textEdit1.Text);
                        tBarcode.Text = string.Empty;
                        //deyisilmis

                        get_cem(textEdit1.Text.ToString());



                        textEdit9.Text = "";
                        textEdit10.Text = "";
                        textEdit12.Text = "";
                        textEdit13.Text = "";


                    }
                }


                else
                {
                    getall(tBarcode.Text);
                    get(textEdit1.Text);
                    get_say_birmal(tBarcode.Text, textEdit1.Text);
                    get_cem(textEdit1.Text);
                }


                textBox5.Text = string.Empty;
                tCustomer.Text = string.Empty;
                tBarcode.Text = string.Empty;
            }
        }

        private void textEdit4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //string kontrol = tBarcode.Text;
            //string kod;
            //string kg;
            //string gr;
            //string barkod;


            //if (e.KeyChar == (char)13)
            //{
            //    if (kontrol.Substring(0, 1) == "0")
            //    {
            //        if (kontrol.Count() is 12)
            //        {
            //            kontrol = "0" + kontrol;
            //        }
            //        kod = kontrol.Substring(2, 5);
            //        kg = kontrol.Substring(7, 2);
            //        gr = kontrol.Substring(9, 3);


            //        SqlConnection conn = new SqlConnection();
            //        SqlCommand cmd = new SqlCommand();
            //        conn.ConnectionString = Properties.Settings.Default.SqlCon;
            //        conn.Open();

            //        string query = $@"select BARKOD from [MAL_ALISI_DETAILS] where [MAL_ALISI_DETAILS_ID]={ kod}";
            //        cmd.Connection = conn;
            //        cmd.CommandText = query;

            //        SqlDataReader dr = cmd.ExecuteReader();
            //        while (dr.Read())
            //        {
            //            barkodsa = dr["BARKOD"].ToString();
            //            barkod = dr["BARKOD"].ToString();
            //            textEdit10.Text = kg + "," + gr;
            //            getall(1, barkod);
            //            get(textEdit1.Text.ToString());

            //            get_say_birmal(barkod, textEdit1.Text.ToString());
            //        }


            //        int rowHandle = gridView1.LocateByValue("MAL_ALISI_DETAILS_ID", Int32.Parse(kod));
            //        if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            //            gridView1.FocusedRowHandle = rowHandle;
            //        gridView1.SelectRow(rowHandle);

            //        tBarcode.Text = string.Empty;
            //        //deyisilmis
            //        textEdit10.Text = kg + "," + gr;
            //        get_cem(textEdit1.Text.ToString());

            //        int kayitsayisi = gridView1.RowCount;
            //        //  gridView1.MoveLast();



            //        st.del_migdarnewsa_calculation(gridView1.GetRowCellValue(rowHandle, "MAL_ALISI_DETAILS_ID").ToString(), textEdit10.Text.ToString(),
            //          textEdit1.Text.ToString());


            //        get(textEdit1.Text.ToString());
            //        get_say_birmal(tBarcode.Text.ToString(), textEdit1.Text.ToString());
            //        tBarcode.Text = string.Empty;
            //        //deyisilmis

            //        get_cem(textEdit1.Text.ToString());



            //        textEdit9.Text = "";
            //        textEdit10.Text = "";
            //        textEdit12.Text = "";
            //        textEdit13.Text = "";


            //    }
            //    else
            //    {
            //        //MessageBox.Show("ENTER has been pressed!");
            //        getall(1, tBarcode.Text.ToString());
            //        get(textEdit1.Text.ToString());
            //        get_say_birmal(tBarcode.Text.ToString(), textEdit1.Text.ToString());
            //        tBarcode.Text = string.Empty;
            //        //deyisilmis

            //        get_cem(textEdit1.Text.ToString());
            //        //tCustomer.Text = string.Empty;
            //    }
            //}


            //else if (e.KeyChar == (char)27)
            //{
            //    this.Close();
            //}
            //tCustomer.Text = string.Empty;
            //tBarcode.Text = string.Empty;


            #region OLD CODE
            //string kontrol = tBarcode.Text;
            //string kod;
            //string kg;
            //string gr;
            //string barkod;
            //if (e.KeyChar == (char)13)
            //{



            //    if (kontrol.Substring(0, 1) == "0")
            //    {
            //        kod = kontrol.Substring(2, 4);
            //        kg = kontrol.Substring(6, 2);
            //        gr = kontrol.Substring(8, 3);
            //        // MessageBox.Show(kod);
            //        //  MessageBox.Show(kg);

            //        SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

            //        SqlConnection conn = new SqlConnection();
            //        SqlCommand cmd = new SqlCommand();
            //        conn.ConnectionString = Properties.Settings.Default.SqlCon;
            //        conn.Open();




            //        string query = $@"select BARKOD from [MAL_ALISI_DETAILS] where [MAL_ALISI_DETAILS_ID]={ kod}";


            //        cmd.Connection = conn;
            //        cmd.CommandText = query;
            //        SqlDataReader dr = cmd.ExecuteReader();
            //        while (dr.Read())
            //        {
            //            ///grid load                              
            //            //model_ = Convert.ToInt32( dr["model"].ToString());
            //            // ip_ = dr["ip_"].ToString();

            //            barkod = dr["BARKOD"].ToString();
            //            textEdit10.Text = kg + "," + gr;
            //            getall(1, barkod);
            //            get(textEdit1.Text.ToString());

            //            get_say_birmal(barkod, textEdit1.Text.ToString());

            //            int rowHandle = gridView1.LocateByValue("Barkod", barkod);
            //            if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            //                gridView1.FocusedRowHandle = rowHandle;

            //        }

            //        gridView1.SelectRows(0, 4);

            //        //textEdit10.Text = kg;
            //        tBarcode.Text = string.Empty;
            //        //deyisilmis
            //        textEdit10.Text = kg + "," + gr;
            //        get_cem(textEdit1.Text.ToString());

            //        int kayitsayisi = gridView1.RowCount;
            //        //  gridView1.MoveLast();



            //        st.del_migdarnewsa_calculation(dele_migdar_mal_id.ToString(), textEdit10.Text.ToString(),
            //          textEdit1.Text.ToString());
            //        //}
            //        //   getall(1, textEdit4.Text.ToString());

            //        get(textEdit1.Text.ToString());
            //        get_say_birmal(tBarcode.Text.ToString(), textEdit1.Text.ToString());
            //        tBarcode.Text = string.Empty;
            //        //deyisilmis

            //        get_cem(textEdit1.Text.ToString());



            //        textEdit9.Text = "";
            //        textEdit10.Text = "";
            //        textEdit12.Text = "";
            //        textEdit13.Text = "";


            //    }
            //    else
            //    {
            //        //MessageBox.Show("ENTER has been pressed!");
            //        getall(1, tBarcode.Text.ToString());
            //        get(textEdit1.Text.ToString());
            //        get_say_birmal(tBarcode.Text.ToString(), textEdit1.Text.ToString());
            //        tBarcode.Text = string.Empty;
            //        //deyisilmis

            //        get_cem(textEdit1.Text.ToString());
            //        //tCustomer.Text = string.Empty;
            //    }
            //}


            //else if (e.KeyChar == (char)27)
            //{
            //    this.Close();
            //}
            //tCustomer.Text = string.Empty;
            #endregion
        }

        public void get_cem(string emeliyyat_n)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                    connection.Open();
                    string query = @"
                    select cast ((sum(s - (s*ISNULL(pg.endirim_faiz,0.00))/100.00 - ISNULL(pg.endiriz_azn,0.00))) as decimal(9,2)) as cem 
                    from (select mal_alisi_details_id, satis_qiymeti * count(*)*(case when sum(isnull(kg_,0.00))!=0.00 then sum(isnull(kg_,0.00)) else 1 end ) s 
                    from calculation where  emeliyyat_nomre =  @pricePoint AND userId = @userID
                    group by satis_qiymeti  ,mal_alisi_details_id) tx 
                    left join pos_guzest pg on pg.mal_details_id = tx.mal_alisi_details_id and pg.emeliyyat_nomre = @pricePoint";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@pricePoint", emeliyyat_n);
                        cmd.Parameters.AddWithValue("@userID", Properties.Settings.Default.UserID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (dr["cem"].ToString() != "0.000")
                                {
                                    textEdit6.Text = dr["cem"].ToString();
                                }
                            }
                            gridView1.GroupPanelText = $"Məhsul sayı: {gridView1.RowCount}";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE("Xəta!\n" + e.Message);
            }
        }

        private void get(string proccessNo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                    string queryString = " SELECT * FROM  dbo.fn_POS_SATIS_LOAD(@EMELIYYAT_NOMRE,@userID)";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@EMELIYYAT_NOMRE", proccessNo);
                        command.Parameters.AddWithValue("@userID", Properties.Settings.Default.UserID);
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                gridControl1.DataSource = dt;
                                gridView1.OptionsSelection.MultiSelect = true;
                                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

                                gridView1.Columns[0].Visible = false;
                                gridView1.Columns[8].Visible = false;
                                gridView1.Columns[9].Visible = false;
                                gridView1.Columns["ALIŞ QİYMƏTİ"].Visible = false;
                                gridView1.Columns["BARKOD"].Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE("Xəta!\n" + e.Message);
            }
        }

        private void get_say_birmal(string barkod, string em_nomre)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                    connection.Open();
                    string query = "exec CALC_SAY_CALCULATION @barkod=@pricepoint ,@emeliyyat_nomre=@pricepoint1,@userID=@userId";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@pricepoint", barkod);
                        cmd.Parameters.AddWithValue("@pricepoint1", em_nomre);
                        cmd.Parameters.AddWithValue("@userId", Properties.Settings.Default.UserID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                textEdit10.Text = dr["SAY"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE("Xəta!\n" + e.Message);
            }


        }

        /// <summary>
        /// Məhsulun alış qiymətini sağ üst küncə göstərilməsi üçündür. Deaktiv edilmə səbəbi odur ki gridView-ə məhsul gəldiyində ən sağ tərəfdə məhsulun alış qiyməti zatən qeyd olunur.
        /// Aktiv etmədən öncə layoutControlItem13 visibiliy true vəya always edin
        /// </summary>
        /// <param name="mal_id">MAL_ALIS_DETAİLS cədvəlindən id göndərilir</param>
        public void son_alis_giymeti(int mal_id)
        {
            //int paramValue = mal_id;

            //try
            //{
            //    SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


            //    SqlConnection conn = new SqlConnection();
            //    SqlCommand cmd = new SqlCommand();
            //    conn.ConnectionString = Properties.Settings.Default.SqlCon;
            //    conn.Open();
            //    string query = "exec alis_giymeti_pos_miveggeti @pricePoint";
            //    cmd.Parameters.AddWithValue("@pricePoint", paramValue);
            //    cmd.Connection = conn;
            //    cmd.CommandText = query;

            //    SqlDataReader dr = cmd.ExecuteReader();
            //    while (dr.Read())
            //    {



            //        textEdit8.Text = dr[0].ToString();



            //    }


            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("Xəta!\n" + e);
            //}

        }

        public void getall(string barcode)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                    connection.Open();
                    string query = "SELECT * FROM  dbo.POS_SATIS(1,@pricePoint);";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@pricePoint", barcode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (string.IsNullOrEmpty(tBarcode.Text))
                                {
                                    tBarcode.Text = dr["BARKOD"].ToString();
                                }

                                textBox5.Text = dr["MƏHSUL ADI"].ToString();

                                for (int i = 0; i < Convert.ToInt32(dr["say"]); i++)
                                {
                                    int productID = Convert.ToInt32(dr["mal_details_id"].ToString());
                                    decimal salePrice = Convert.ToDecimal(dr["SATIŞ QİYMƏTİ"].ToString());
                                    decimal purchasePrice = Convert.ToDecimal(dr["ALIŞ QİYMƏTİ"].ToString());

                                    DbProsedures.InsertCalculation(new Calculation
                                    {
                                        proccessNo = textEdit1.Text,
                                        ProductID = productID,
                                        Barcode = dr["BARKOD"].ToString(),
                                        ProductName = dr["MƏHSUL ADI"].ToString(),
                                        SalePrice = salePrice,
                                        PurchasePrice = purchasePrice
                                    });
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE("Xəta!\n" + e.Message);
            }

        }

        public static int dele_migdar_mal_id;

        private void textEdit10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(textEdit10.Text))
                {
                    textEdit10.Text = "0";
                }
                else
                {
                    st.del_migdar_calculation(dele_migdar_mal_id.ToString(), textEdit10.Text, textEdit1.Text);
                    get(textEdit1.Text);
                    get_say_birmal(tBarcode.Text, textEdit1.Text);
                    tBarcode.Text = string.Empty;
                    //deyisilmis

                    get_cem(textEdit1.Text);

                    textEdit9.Text = "";
                    textEdit10.Text = "";
                    textEdit12.Text = "";
                    textEdit13.Text = "";

                }
            }


            else if (e.KeyChar == (char)27)
            {
                this.Close();
            }

        }

        private void textEdit9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(textEdit9.Text))
                {
                    textEdit9.Text = "0";
                }
                else
                {
                    //int a_ = Convert.ToInt32(textEdit10.Text.ToString());
                    //if (a_ > 0 && dele_migdar_mal_id > 0)
                    //{
                    // st.del_migdar_calculation(dele_migdar_mal_id, Convert.ToInt32(textEdit10.Text.ToString()));
                    st.update_satis_giymeti_(dele_migdar_mal_id, Convert.ToDecimal(textEdit9.Text.ToString()));
                    //}
                    //   getall(1, textEdit4.Text.ToString());
                    get(textEdit1.Text.ToString());
                    get_say_birmal(tBarcode.Text.ToString(), textEdit1.Text.ToString());
                    tBarcode.Text = string.Empty;
                    //deyisilmis

                    get_cem(textEdit1.Text.ToString());


                    textEdit9.Text = "";
                    textEdit10.Text = "";
                    textEdit12.Text = "";
                    textEdit13.Text = "";
                }

            }


            else if (e.KeyChar == (char)27)
            {
                this.Close();
            }

        }

        private void textEdit12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(textEdit12.Text))
                {

                }
                else
                {
                    try
                    {


                        //   int a_ = Convert.ToInt32(textEdit10.Text.ToString());
                        //if (a_ > 0 && dele_migdar_mal_id > 0)
                        //{
                        // st.del_migdar_calculation(dele_migdar_mal_id, Convert.ToInt32(textEdit10.Text.ToString()));
                        //if (string.IsNullOrEmpty(textEdit12.Text) || string.IsNullOrEmpty(textEdit13.Text))
                        st.pos_guzest_insert_(textEdit1.Text.ToString(),
                            dele_migdar_mal_id, textEdit12.Text.ToString(), textEdit13.Text.ToString());
                        //}
                        //   getall(1, textEdit4.Text.ToString());
                        get(textEdit1.Text.ToString());
                        get_say_birmal(tBarcode.Text.ToString(), textEdit1.Text.ToString());
                        tBarcode.Text = string.Empty;
                        //deyisilmis

                        get_cem(textEdit1.Text.ToString());


                        textEdit9.Text = "";
                        textEdit10.Text = "";
                        textEdit12.Text = "0";
                        textEdit13.Text = "0";
                    }
                    catch
                    {
                        XtraMessageBox.Show("ƏMƏLİYYATDA SƏHV- FAİZ MƏBLƏĞİNİ DÜZGÜN DAXİL EDİN ");
                    }
                }

            }


            else if (e.KeyChar == (char)27)
            {
                this.Close();
            }
        }

        private void textEdit13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(textEdit13.Text))
                {

                }
                else
                {
                    try
                    {


                        //    int a_ = Convert.ToInt32(textEdit10.Text.ToString());
                        //if (a_ > 0 && dele_migdar_mal_id > 0)
                        //{
                        // st.del_migdar_calculation(dele_migdar_mal_id, Convert.ToInt32(textEdit10.Text.ToString()));
                        //if (string.IsNullOrEmpty(textEdit12.Text) || string.IsNullOrEmpty(textEdit13.Text))
                        st.pos_guzest_insert_(textEdit1.Text.ToString(),
                            dele_migdar_mal_id, textEdit12.Text.ToString(), textEdit13.Text.ToString());
                        //}
                        //   getall(1, textEdit4.Text.ToString());
                        get(textEdit1.Text.ToString());
                        get_say_birmal(tBarcode.Text.ToString(), textEdit1.Text.ToString());
                        tBarcode.Text = string.Empty;
                        //deyisilmis

                        get_cem(textEdit1.Text.ToString());


                        textEdit9.Text = "";
                        textEdit10.Text = "";
                        textEdit12.Text = "0";
                        textEdit13.Text = "0";
                    }

                    catch
                    {
                        XtraMessageBox.Show("ƏMƏLİYYATDA SƏHV- MƏBLƏĞİ DÜZGÜN DAXİL EDİN ");
                    }
                }

            }


            else if (e.KeyChar == (char)27)
            {
                this.Close();
            }
        }

        static TextEdit textboxname;

        private void textEdit9_Click(object sender, EventArgs e)
        {
            textboxname = textEdit9;
        }

        private void textEdit10_Click(object sender, EventArgs e)
        {
            textboxname = textEdit10;
        }

        private void textEdit12_Click(object sender, EventArgs e)
        {
            textboxname = textEdit12;
        }

        private void textEdit13_Click(object sender, EventArgs e)
        {
            textboxname = textEdit13;
        }

        private void textEdit7_Click(object sender, EventArgs e)
        {
            textboxname = textEdit7;
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {

            if (textboxname != null)
            {
                textboxname.Text = textboxname.Text + "1";
            }

        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            if (textboxname != null)
            {
                textboxname.Text = textboxname.Text + "2";
            }

        }

        private void simpleButton171_Click(object sender, EventArgs e)
        {
            if (textboxname != null)
            {
                textboxname.Text = textboxname.Text + "3";
            }

        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            if (textboxname != null)
            {
                textboxname.Text = textboxname.Text + "4";
            }

        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            if (textboxname != null)
            {
                textboxname.Text = textboxname.Text + "5";
            }

        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            if (textboxname != null)
            {
                textboxname.Text = textboxname.Text + "6";
            }

        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            if (textboxname != null)
            {
                textboxname.Text = textboxname.Text + "7";
            }

        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (textboxname != null)
            {
                textboxname.Text = textboxname.Text + "8";
            }

        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            if (textboxname != null)
            {
                textboxname.Text = textboxname.Text + "9";
            }

        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            if (textboxname != null)
            {
                textboxname.Text = textboxname.Text + "0";
            }

        }

        private void simpleButton20_Click(object sender, EventArgs e)
        {
            if (textboxname != null)
            {
                textboxname.Text = textboxname.Text + ".";
            }

        }

        private void DeleteButton()
        {
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);

                int B = Convert.ToInt32(row[0].ToString());
                if (B > 0)
                {
                    st_.del_grid_data(B, textEdit1.Text);
                }
            }

            get(textEdit1.Text);
            get_say_birmal(tBarcode.Text, textEdit1.Text);
            tBarcode.Text = string.Empty;

            get_cem(textEdit1.Text);

            textEdit9.Text = "";
            textEdit10.Text = "";
            textEdit12.Text = "";
            textEdit13.Text = "";
            tCustomer.Text = "";
            textBox5.Text = "";
            tBarcode.Focus();
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            DeleteButton();
        }

        private void simpleButton18_Click(object sender, EventArgs e)
        {
            textboxname = null;
        }

        void Payment(Enums.PayType type)
        {
            if (type is Enums.PayType.Cash)
            {
                //CASH
                if (!string.IsNullOrEmpty(textEdit6.Text))
                {
                    SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                    string queryString = "SELECT STATUS FROM MENFI_AC_BAGLA ";
                    SqlCommand command = new SqlCommand(queryString, connection);


                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    decimal saysa24 = 0;
                    int number = dt.Rows[0].Field<int>("STATUS");
                    int numberkontrol = 0;


                    if (number == 0)
                    {

                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(i);

                            SqlConnection connection4 = new SqlConnection(Properties.Settings.Default.SqlCon);
                            string queryStringk = "SELECT sum(   migdar_ ) as miktar   FROM dbo.GAIME_SATIS_SEARCH_menfi_ACIG() where [MƏHSUL ADI]=N'" + row["MƏHSUL ADI"].ToString() + "' and [MƏHSUL KODU]=(select [MEHSUL_KODU]from [MAL_ALISI_DETAILS] where [MAL_ALISI_DETAILS_ID]=" + Convert.ToInt32(row["MAL_ALISI_DETAILS_ID"]) + " ) group by TECHIZATCI_ID ,[TƏCHİZATÇI] ,[MƏHSUL ADI],  [MƏHSUL KODU],BARKOD  ";
                            connection4.Open();
                            SqlCommand command4 = new SqlCommand(queryStringk, connection4);
                            decimal saysa = Convert.ToDecimal(row["SAY"]);
                            SqlDataReader dr4 = command4.ExecuteReader();
                            while (dr4.Read())
                            {
                                saysa24 = Convert.ToDecimal(dr4["miktar"].ToString());
                            }
                            if ((saysa24 - saysa) < 0)
                            {
                                numberkontrol = numberkontrol + 1;
                                XtraMessageBox.Show($"Satılan məhsul sayı anbardakı qalıqdan çoxdur \nMəhsul adı: {row["MƏHSUL ADI"].ToString()}\nAnbar qalığı: {saysa24.ToString("N2")}", "Bildiriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                numberkontrol = numberkontrol + 0;
                            }
                        }
                        if (numberkontrol == 0)
                        {
                            decimal f = Convert.ToDecimal(textEdit6.Text);

                            bank n = new bank(f, this);

                            n.ShowDialog();
                        }
                    }
                    else
                    {
                        decimal f = Convert.ToDecimal(textEdit6.Text);

                        bank n = new bank(f, this);

                        n.ShowDialog();
                    }
                }
            }
            else if (type is Enums.PayType.Card)
            {
                Cursor.Current = Cursors.WaitCursor;
                //kart 
                if (string.IsNullOrEmpty(textEdit6.Text))
                {

                }
                else
                {
                    SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                    string queryString = "SELECT STATUS FROM MENFI_AC_BAGLA ";
                    SqlCommand command = new SqlCommand(queryString, connection);


                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    decimal saysa24 = 0;
                    int number = dt.Rows[0].Field<int>("STATUS");
                    int numberkontrol = 0;


                    if (number == 0)
                    {

                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(i);

                            SqlConnection connection4 = new SqlConnection(Properties.Settings.Default.SqlCon);
                            string queryStringk = "SELECT sum(   migdar_ ) as miktar   FROM dbo.GAIME_SATIS_SEARCH_menfi_ACIG() where [MƏHSUL ADI]=N'" + row["MƏHSUL ADI"].ToString() + "' and [MƏHSUL KODU]=(select [MEHSUL_KODU]from [MAL_ALISI_DETAILS] where [MAL_ALISI_DETAILS_ID]=" + Convert.ToInt32(row["MAL_ALISI_DETAILS_ID"]) + " ) group by TECHIZATCI_ID ,[TƏCHİZATÇI] ,[MƏHSUL ADI],  [MƏHSUL KODU],BARKOD  ";
                            connection4.Open();
                            SqlCommand command4 = new SqlCommand(queryStringk, connection4);
                            decimal saysa = Convert.ToDecimal(row["SAY"]);
                            SqlDataReader dr4 = command4.ExecuteReader();
                            while (dr4.Read())
                            {
                                saysa24 = Convert.ToDecimal(dr4["miktar"].ToString());
                            }
                            if ((saysa24 - saysa) < 0)

                            {
                                numberkontrol = numberkontrol + 1;
                                XtraMessageBox.Show($"Satılan məhsul sayı anbardakı qalıqdan çoxdur \nMəhsul adı: {row["MƏHSUL ADI"].ToString()}\nAnbar qalığı: {saysa24.ToString("N2")}", "Bildiriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                numberkontrol = numberkontrol + 0;

                            }
                        }
                        if (numberkontrol == 0)
                        {

                            decimal f = Convert.ToDecimal(textEdit6.Text);
                            //bool clinic = false;
                            //DialogResult result = XtraMessageBox.Show("A4 sənədi çap edilsin ?", nameof(HeaderMessage.Mesaj), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            //if (result is DialogResult.Yes)
                            //{
                            //    clinic = true;
                            //}

                            gelen_data_negd_pos(0, f, f, 0, 0, false);
                        }
                    }
                    else
                    {
                        decimal f = Convert.ToDecimal(textEdit6.Text);

                        //bool clinic = false;
                        //DialogResult result = XtraMessageBox.Show("A4 sənədi çap edilsin ?", nameof(HeaderMessage.Mesaj), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //if (result is DialogResult.Yes)
                        //{
                        //    clinic = true;
                        //}
                        gelen_data_negd_pos(0, f, f, 0, 0, false);
                    }
                }

                Cursor.Current = Cursors.Default;
            }
            else if (type is Enums.PayType.CashCard)
            {
                if (!string.IsNullOrEmpty(textEdit6.Text))
                {
                    SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                    string queryString = "SELECT STATUS FROM MENFI_AC_BAGLA ";
                    SqlCommand command = new SqlCommand(queryString, connection);

                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    decimal saysa24 = 0;
                    int number = dt.Rows[0].Field<int>("STATUS");
                    int numberkontrol = 0;
                    if (number == 0)
                    {
                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(i);

                            SqlConnection connection4 = new SqlConnection(Properties.Settings.Default.SqlCon);
                            string queryStringk = "SELECT sum(   migdar_ ) as miktar   FROM dbo.GAIME_SATIS_SEARCH_menfi_ACIG() where [MƏHSUL ADI]=N'" + row["MƏHSUL ADI"].ToString() + "' and [MƏHSUL KODU]=(select [MEHSUL_KODU]from [MAL_ALISI_DETAILS] where [MAL_ALISI_DETAILS_ID]=" + Convert.ToInt32(row["MAL_ALISI_DETAILS_ID"]) + " ) group by TECHIZATCI_ID ,[TƏCHİZATÇI] ,[MƏHSUL ADI],  [MƏHSUL KODU],BARKOD  ";
                            connection4.Open();
                            SqlCommand command4 = new SqlCommand(queryStringk, connection4);
                            decimal saysa = Convert.ToDecimal(row["SAY"]);
                            SqlDataReader dr4 = command4.ExecuteReader();
                            while (dr4.Read())
                            {
                                saysa24 = Convert.ToDecimal(dr4["miktar"].ToString());
                            }
                            if ((saysa24 - saysa) < 0)
                            {
                                numberkontrol = numberkontrol + 1;
                                XtraMessageBox.Show($"Satılan məhsul sayı anbardakı qalıqdan çoxdur \nMəhsul adı: {row["MƏHSUL ADI"].ToString()}\nAnbar qalığı: {saysa24.ToString("N2")}", "Bildiriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                numberkontrol = numberkontrol + 0;

                            }
                        }
                        if (numberkontrol == 0)
                        {
                            decimal f = Convert.ToDecimal(textEdit6.Text);

                            nagd_kart nk = new nagd_kart(f, this);
                            nk.ShowDialog();
                        }
                    }
                    else
                    {
                        decimal f = Convert.ToDecimal(textEdit6.Text);

                        nagd_kart nk = new nagd_kart(f, this);
                        nk.ShowDialog();
                    }
                }
            }
            else if (type is Enums.PayType.Prepayment)
            {
                if (!string.IsNullOrEmpty(textEdit6.Text))
                {
                    SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                    string queryString = "SELECT STATUS FROM MENFI_AC_BAGLA ";
                    SqlCommand command = new SqlCommand(queryString, connection);

                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    decimal saysa24 = 0;
                    int number = dt.Rows[0].Field<int>("STATUS");
                    int numberkontrol = 0;
                    if (number == 0)
                    {
                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(i);

                            SqlConnection connection4 = new SqlConnection(Properties.Settings.Default.SqlCon);
                            string queryStringk = "SELECT sum(   migdar_ ) as miktar   FROM dbo.GAIME_SATIS_SEARCH_menfi_ACIG() where [MƏHSUL ADI]=N'" + row["MƏHSUL ADI"].ToString() + "' and [MƏHSUL KODU]=(select [MEHSUL_KODU]from [MAL_ALISI_DETAILS] where [MAL_ALISI_DETAILS_ID]=" + Convert.ToInt32(row["MAL_ALISI_DETAILS_ID"]) + " ) group by TECHIZATCI_ID ,[TƏCHİZATÇI] ,[MƏHSUL ADI],  [MƏHSUL KODU],BARKOD  ";
                            connection4.Open();
                            SqlCommand command4 = new SqlCommand(queryStringk, connection4);
                            decimal saysa = Convert.ToDecimal(row["SAY"]);
                            SqlDataReader dr4 = command4.ExecuteReader();
                            while (dr4.Read())
                            {
                                saysa24 = Convert.ToDecimal(dr4["miktar"].ToString());
                            }
                            if ((saysa24 - saysa) < 0)
                            {
                                numberkontrol = numberkontrol + 1;
                                XtraMessageBox.Show($"Satılan məhsul sayı anbardakı qalıqdan çoxdur \nMəhsul adı: {row["MƏHSUL ADI"].ToString()}\nAnbar qalığı: {saysa24.ToString("N2")}", "Bildiriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                numberkontrol = numberkontrol + 0;

                            }
                        }
                        if (numberkontrol == 0)
                        {
                            decimal f = Convert.ToDecimal(textEdit6.Text);

                            prenagdkart nk = new prenagdkart(f, this);
                            nk.ShowDialog();
                        }
                    }
                    else
                    {
                        decimal f = Convert.ToDecimal(textEdit6.Text);

                        prenagdkart nk = new prenagdkart(f, this);
                        nk.ShowDialog();
                    }
                }
            }
            else if (type is PayType.OtherPay)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(textEdit6.Text))
                {
                    SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString);
                    string queryString = "SELECT STATUS FROM MENFI_AC_BAGLA ";
                    SqlCommand command = new SqlCommand(queryString, connection);


                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    decimal saysa24 = 0;
                    int number = dt.Rows[0].Field<int>("STATUS");
                    int numberkontrol = 0;

                    if (number == 0)
                    {

                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(i);

                            SqlConnection connection4 = new SqlConnection(Properties.Settings.Default.SqlCon);
                            string queryStringk = "SELECT sum(   migdar_ ) as miktar   FROM dbo.GAIME_SATIS_SEARCH_menfi_ACIG() where [MƏHSUL ADI]=N'" + row["MƏHSUL ADI"].ToString() + "' and [MƏHSUL KODU]=(select [MEHSUL_KODU]from [MAL_ALISI_DETAILS] where [MAL_ALISI_DETAILS_ID]=" + Convert.ToInt32(row["MAL_ALISI_DETAILS_ID"]) + " ) group by TECHIZATCI_ID ,[TƏCHİZATÇI] ,[MƏHSUL ADI],  [MƏHSUL KODU],BARKOD  ";
                            connection4.Open();
                            SqlCommand command4 = new SqlCommand(queryStringk, connection4);
                            decimal saysa = Convert.ToDecimal(row["SAY"]);
                            SqlDataReader dr4 = command4.ExecuteReader();
                            while (dr4.Read())
                            {
                                saysa24 = Convert.ToDecimal(dr4["miktar"].ToString());
                            }
                            if ((saysa24 - saysa) < 0)

                            {
                                numberkontrol = numberkontrol + 1;
                                XtraMessageBox.Show($"Satılan məhsul sayı anbardakı qalıqdan çoxdur \nMəhsul adı: {row["MƏHSUL ADI"].ToString()}\nAnbar qalığı: {saysa24.ToString("N2")}", "Bildiriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                numberkontrol = numberkontrol + 0;

                            }
                        }
                        if (numberkontrol == 0)
                        {
                            decimal f = Convert.ToDecimal(textEdit6.Text);

                            gelen_data_negd_pos(0, f, f, 0, 0, false, Enums.PayType.OtherPay);
                        }
                    }
                    else
                    {
                        decimal f = Convert.ToDecimal(textEdit6.Text);
                        gelen_data_negd_pos(0, f, f, 0, 0, false, Enums.PayType.OtherPay);
                    }
                }

                Cursor.Current = Cursors.Default;
            }
            else if (type is PayType.Installment)
            {
                Cursor.Current = Cursors.WaitCursor;
                //kart 
                if (string.IsNullOrEmpty(textEdit6.Text))
                {

                }
                else

                {
                    SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                    string queryString = "SELECT STATUS FROM MENFI_AC_BAGLA ";
                    SqlCommand command = new SqlCommand(queryString, connection);


                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    decimal saysa24 = 0;
                    int number = dt.Rows[0].Field<int>("STATUS");
                    int numberkontrol = 0;
                    //    XtraMessageBox.Show(number.ToString());


                    if (number == 0)
                    {

                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(i);

                            SqlConnection connection4 = new SqlConnection(Properties.Settings.Default.SqlCon);
                            string queryStringk = "SELECT sum(   migdar_ ) as miktar   FROM dbo.GAIME_SATIS_SEARCH_menfi_ACIG() where [MƏHSUL ADI]=N'" + row["MƏHSUL ADI"].ToString() + "' and [MƏHSUL KODU]=(select [MEHSUL_KODU]from [MAL_ALISI_DETAILS] where [MAL_ALISI_DETAILS_ID]=" + Convert.ToInt32(row["MAL_ALISI_DETAILS_ID"]) + " ) group by TECHIZATCI_ID ,[TƏCHİZATÇI] ,[MƏHSUL ADI],  [MƏHSUL KODU],BARKOD  ";
                            connection4.Open();
                            SqlCommand command4 = new SqlCommand(queryStringk, connection4);
                            decimal saysa = Convert.ToDecimal(row["SAY"]);
                            SqlDataReader dr4 = command4.ExecuteReader();
                            while (dr4.Read())
                            {
                                saysa24 = Convert.ToDecimal(dr4["miktar"].ToString());
                            }
                            if ((saysa24 - saysa) < 0)

                            {
                                numberkontrol = numberkontrol + 1;
                                XtraMessageBox.Show($"Satılan məhsul sayı anbardakı qalıqdan çoxdur \nMəhsul adı: {row["MƏHSUL ADI"].ToString()}\nAnbar qalığı: {saysa24.ToString("N2")}", "Bildiriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                numberkontrol = numberkontrol + 0;

                            }
                        }
                        if (numberkontrol == 0)
                        {

                            decimal f = Convert.ToDecimal(textEdit6.Text);

                            AzSmartInstallmentSales(lIpAdress.Text, tUsername.Text, f);
                        }
                    }
                    else
                    {
                        decimal f = Convert.ToDecimal(textEdit6.Text);

                        AzSmartInstallmentSales(lIpAdress.Text, tUsername.Text, f);
                    }
                }

                Cursor.Current = Cursors.Default;
            }
            tBarcode.Focus();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Payment(Enums.PayType.Cash);
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("OtherPay").ToString());
            if (control)
            {
                fCardAndOtherPay f = new fCardAndOtherPay();
                var result = f.ShowDialog();
                if (result is DialogResult.Yes)
                {
                    Payment(Enums.PayType.OtherPay);
                }
                else if (result is DialogResult.No)
                {
                    Payment(Enums.PayType.Card);
                }
            }
            else
            {
                Payment(Enums.PayType.Card);
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            Payment(Enums.PayType.CashCard);
        }

        /// <summary>
        /// POS AÇ - NÖVBƏNİN AÇILMASI
        /// </summary>
        private void simpleButton22_Click(object sender, EventArgs e)
        {

            // MessageBox.Show(lModel.Text);
            Cursor.Current = Cursors.WaitCursor;
            switch (lModel.Text)
            {
                case "1":
                    Sunmi.GetShiftStatus(lIpAdress.Text, tUsername.Text);
                    break; /*SUNMI*/
                case "2":
                    AzSmart.OpenShift(lIpAdress.Text, lMerchantId.Text, tUsername.Text);
                    break; /*AZSMART*/
                case "3":
                    textBox1.Text = Omnitech.Login(lIpAdress.Text); //Access Token alır
                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        Omnitech.GetShiftStatus(lIpAdress.Text, textBox1.Text); //Açıq olub olmadığını yoxlayır. Bağlıdırsa yenidən açır
                    }
                    break; /*OMNITECH*/
                case "4":
                    FormHelpers.Alert("Xprinter istifadəçiləri üçün funksiya müvəqqəti olaraq deaktiv edilmiştir", MessageType.Info);
                    break; /*XPRINTER*/
                case "5":
                    textBox1.Text = DataPay.Login(lIpAdress.Text);
                    //datapay_POS_Login(label1.Text);
                    DataPay.OpenShift(lIpAdress.Text, textBox1.Text);
                    break; /*DATAPAY*/
                case "6":
                    if (NBA_GetInfo())
                    {
                        textBox1.Text = NBA.Login(lIpAdress.Text, textBox4.Text); //Access Token alır
                        NBA.GetShiftStatus(lIpAdress.Text, textBox1.Text); //Açıq olub olmadığını yoxlayır. Bağlıdırsa yenidən açır
                    }
                    break; /*NBA*/

                case "7":
                    textBox1.Text = EKASAM.Login(lIpAdress.Text); //Access Token alır
                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        EKASAM.GetShiftStatus(lIpAdress.Text, textBox1.Text); //Açıq olub olmadığını yoxlayır. Bağlıdırsa yenidən açır
                    }
                    break; /*EKASSAM*/
            }
            Cursor.Current = Cursors.Default;
        }


        /// <summary>
        /// POS BAĞLA - NÖVBƏNİN BAĞLANMASI
        /// </summary>
        private void simpleButton24_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            switch (lModel.Text)
            {
                case "1":
                    Sunmi.CloseShift(lIpAdress.Text, tUsername.Text);
                    break; /*SUNMI*/
                case "2":
                    AzSmart.CloseShift(lIpAdress.Text, lMerchantId.Text, tUsername.Text);
                    break; /*AZSMART*/
                case "3":
                    Omnitech.CloseShift(lIpAdress.Text.Replace("\n", ""), textBox1.Text);
                    break; /*OMNITECH*/
                case "4":
                    FormHelpers.Alert("Xprinter istifadəçiləri üçün funksiya müvəqqəti olaraq deaktiv edilmiştir", MessageType.Info);
                    break; /*XPRINTER*/
                case "5":
                    DataPay.CloseShift(lIpAdress.Text, textBox1.Text);
                    break; /*DATAPAY*/
                case "6":
                    textBox1.Text = NBA.Login(lIpAdress.Text, textBox4.Text); //Access Token alır
                    NBA_CloseShift();
                    break; /*NBA*/

                case "7":
                    EKASAM.CloseShift(lIpAdress.Text.Replace("\n", ""), textBox1.Text);
                    break; /*EKASSAM*/
            }
            Cursor.Current = Cursors.Default;
        }

        public class WeatherForecastDataPay
        {
            public string status { get; set; }
            public int code { get; set; }
            public string message { get; set; }

            public string access_token { get; set; }

            public string long_id { get; set; }
            public int document_number { get; set; }
            public string short_id { get; set; }
            public int documentNumber { get; set; }

            public string documentId { get; set; }
            public string shortDocumentId { get; set; }
        }

        public class Data2
        {
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

            public string createdAtUtc { get; set; }
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

            public double depositSum { get; set; }
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

        public class nbarootbank
        {
            public string trnid { get; set; }
        }

        public class nbarootbankdetail
        {
            public string status { get; set; }
            public string responsecodeText { get; set; }
            public List<Databanksa> merchantreceipt { get; set; }
            public List<Databanksacustomer> customerreceipt { get; set; }

            public List<Databanksasettlementreceipt> settlementreceipt { get; set; }
            public List<Databanksa> errorreceipt { get; set; }

        }

        public class Databanksa
        {
            public string trnid { get; set; }
            public string status { get; set; }
            public string pan { get; set; }
            public string rrn { get; set; }
            public string terminalid { get; set; }
            public string state { get; set; }
            public string line { get; set; }
        }

        public class Databanksacustomer
        {
            public string line { get; set; }
        }

        public class Databanksasettlementreceipt
        {
            public string line { get; set; }
        }

        private bool NBA_GetInfo()
        {
            var response = NBA.GetInfo(lIpAdress.Text);
            if (response == null) { return false; }
            else
            {
                if (response.message is "Successful operation")
                {
                    if (TerminalTokenData.NkaSerialNumber == response.data.cashregister_factory_number)
                    {
                        customer = TerminalTokenData.CompanyName;
                        customervoen = TerminalTokenData.Voen;
                        obyektkod = TerminalTokenData.ObjectTaxNumber;
                        obyetname = TerminalTokenData.TsName;
                        obyektadres = TerminalTokenData.Address;
                        nkamodel = TerminalTokenData.NkaModel;
                        nkanumber = TerminalTokenData.NkaSerialNumber;
                        nkarnumber = TerminalTokenData.NMQRegistrationNumber;
                        textBox4.Text = TerminalTokenData.NkaSerialNumber;
                        //customer = response.data.company_name;
                        //customervoen = response.data.company_tax_number;
                        //obyektkod = response.data.object_tax_number;
                        //obyetname = response.data.object_name;
                        //obyektadres = response.data.object_address;
                        //nkamodel = response.data.cashregister_model;
                        //nkanumber = response.data.cashregister_factory_number;
                        //nkarnumber = response.data.cashbox_tax_number;
                        //textBox4.Text = response.data.cashregister_factory_number;
                        return true;
                    }
                    else
                    {
                        FormHelpers.Alert("Fərqli kassa avadanlığı ilə istifadəyə icazə yoxdur", MessageType.Info);
                        return false;
                    }
                }
                else
                {
                    ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE($"Xəta mesajı: {response.message}");
                    return false;
                }
            }
        }

        public void NBA_CloseShift()
        {
            string SendJson = null;
            string ResponseJson = null;
            try
            {
                var responseData = NBA.CloseShift(lIpAdress.Text, textBox1.Text);
                SendJson = responseData.RequestJson;
                ResponseJson = responseData.ResponseJson;
                if (responseData == null) { return; }
                else
                {
                    if (responseData.message is "Successful operation")
                    {
                        nacilma = responseData.data.shiftOpenAtUtc.ToString();
                        ndoc = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                        firstDocNumber = responseData.data.firstDocNumber.ToString();
                        lastDocNumber = responseData.data.lastDocNumber.ToString();
                        zdocument = responseData.data.document_id;

                        saleCount = responseData.data.currencies[0].saleCount.ToString();
                        saleSum = responseData.data.currencies[0].saleSum.ToString();
                        saleCashSum = responseData.data.currencies[0].saleCashSum.ToString();
                        saleCashlessSum = responseData.data.currencies[0].saleCashlessSum.ToString();
                        salePrepaymentSum = responseData.data.currencies[0].salePrepaymentSum.ToString();
                        saleCreditSum = responseData.data.currencies[0].saleCreditSum.ToString();
                        saleBonusSum = responseData.data.currencies[0].saleBonusSum.ToString();

                        depositCount = responseData.data.currencies[0].depositCount.ToString();
                        depositSum = responseData.data.currencies[0].depositSum.ToString();
                        moneyBackCount = responseData.data.currencies[0].moneyBackCount.ToString();
                        moneyBackSum = responseData.data.currencies[0].moneyBackSum.ToString();
                        moneyBackCashSum = responseData.data.currencies[0].moneyBackCashSum.ToString();
                        moneyBackCashlessSum = responseData.data.currencies[0].moneyBackCashlessSum.ToString();

                        vatSuma = responseData.data.currencies[0].saleVatAmounts[0].vatSum.ToString();

                        if (!($"{responseData.data.currencies[0].moneyBackVatAmounts}".Length > 0))
                        {
                            vatSumma = $"{responseData.data.currencies[0].moneyBackVatAmounts[0].vatSum}".ToString();
                        }

                        if (MessageVisible)
                        {
                            ReadyMessages.SUCCESS_CLOSE_SHIFT_MESSAGE();
                        }


                        Log(CommonData.SUCCESS_CLOSE_SHIFT);

                        PrintDocument pd = new PrintDocument();
                        PrinterSettings settings = new PrinterSettings();
                        PageSettings pageSettings = new PageSettings(settings);
                        PaperSize paperSize = settings.DefaultPageSettings.PaperSize;

                        pd.DefaultPageSettings = new PageSettings();
                        pd.DefaultPageSettings.PaperSize = paperSize;
                        pd.PrintPage += new PrintPageEventHandler(nka_zhesabat);

                        pagesCount = 2;


                        PrintDialog PrintDialog1 = new PrintDialog();

                        PrintDialog1.Document = pd;

                        pd.Print();
                    }
                }


                var url = lIpAdress.Text;
                var client2 = new RestClient();
                var client3 = new RestClient();


                string urlbankcontrol = url.Replace($"{NBA.NBA_FISCAL_SERVICE_PORT}/api/v1", $"{NBA.NBA_BANK_SERVICE_PORT}/settlement/start");


                var requestbankdetail = new RestRequest(urlbankcontrol, Method.Post);


                string bankid;



                requestbankdetail.AddHeader("Accept", "application/json");
                requestbankdetail.AddHeader("apikey", "87903e62-9643-4e46-bb6f-3920be587332");

                var body2 = "{}";
                requestbankdetail.AddStringBody(body2, DataFormat.Json);

                RestResponse response2 = client2.Execute(requestbankdetail);





                string dataccontrolsa = response2.Content;
                string data2 = System.Text.RegularExpressions.Regex.Unescape(dataccontrolsa);

                data2 = CleanJson(data2);

                nbarootbankdetail weatherForecastbankdetail = System.Text.Json.JsonSerializer.Deserialize<nbarootbankdetail>(data2);


                int i = 0, j = 0;

                string statusa = $"{weatherForecastbankdetail.status}";
                if (statusa == "approved")
                {

                    foreach (var item in weatherForecastbankdetail.settlementreceipt)
                    {
                        bankdizis.Add(item.line);
                    }


                    PrintDocument pd = new PrintDocument();
                    pd.DefaultPageSettings = new PageSettings
                    {
                        PaperSize = new PrinterSettings().DefaultPageSettings.PaperSize
                    };

                    pd.PrintPage += new PrintPageEventHandler(nba_bankprints);

                    pagesCount = 1;


                    PrintDialog PrintDialog1 = new PrintDialog
                    {
                        Document = pd
                    };

                    pd.Print();
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE(e.Message);
            }
            finally
            {
                FormHelpers.OperationLog(new OperationLogs
                {
                    OperationType = Enums.OperationType.ZReport,
                    OperationId = 0,
                    Message = "Z Report request/response json",
                    RequestCode = SendJson,
                    ResponseCode = ResponseJson,
                });
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            fDeposit n = new fDeposit(this);
            n.ShowDialog();
        }

        public void deposit(decimal deposit)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    ReadyMessages.WARNING_DEFAULT_MESSAGE("NÖVBƏ AÇILMAYIB !\nPOS AÇ DÜYMƏSİNƏ VURARAQ NÖVBƏNİ AÇIN");
                    return;
                }

                deposita = deposit.ToString();
                var url = lIpAdress.Text;
                string parameters = "{\"parameters\":{\"access_token\":\"" + textBox1.Text + "\",\"doc_type\":\"deposit\",\"data\":{\"cashier\":\"" + tUsername.Text + "\",\"currency\":\"AZN\",\"sum\":\"" + deposit + "\"}},\"operationId\":\"createDocument\",\"version\":1}";


                var client = new RestClient();
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json;charset=utf-8");
                request.AddStringBody(parameters, DataFormat.Json);
                RestResponse response = client.Execute(request);

                nbaroot weatherForecast = System.Text.Json.JsonSerializer.Deserialize<nbaroot>(response.Content);


                sdocumentid = $"{weatherForecast.data.short_document_id}";
                YekunMebleg = deposit.ToString();

                fissayi = $"{weatherForecast.data.document_number}";

                gunfissayi = $"{weatherForecast.data.shift_document_number}".ToString();


                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings = new PageSettings
                {
                    PaperSize = new PrinterSettings().DefaultPageSettings.PaperSize
                };

                pd.PrintPage += new PrintPageEventHandler(nba_deposit);

                pagesCount = 1;


                PrintDialog PrintDialog1 = new PrintDialog
                {
                    Document = pd
                };

                pd.Print();

            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
            }
        }

        public void nba_X_Report(string _ip_, string keys)
        {
            try
            {
                string url = _ip_;

                var client = new RestClient();
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");

                var body = @" { ""parameters"":{ ""access_token"":""" + textBox1.Text + " \"},\"operationId\":\"getXReport\",\"version\":1}";
                request.AddStringBody(body, DataFormat.Json);
                RestResponse response = client.Execute(request);

                nbaroot weatherForecast = JsonSerializer.Deserialize<nbaroot>(response.Content);

                string a1 = $"{weatherForecast.message}";

                nacilma = $"{weatherForecast.data.shiftOpenAtUtc}";
                nhtarix = $"{weatherForecast.data.createdAtUtc}";
                rno = $"{weatherForecast.data.reportNumber}";
                ndoc = $"{weatherForecast.data.shiftCloseAtUtc}";
                firstDocNumber = $"{weatherForecast.data.firstDocNumber}".ToString();
                lastDocNumber = $"{weatherForecast.data.lastDocNumber}".ToString();
                zdocument = $"{weatherForecast.data.document_id}";

                saleCount = $"{weatherForecast.data.currencies[0].saleCount}".ToString();
                saleSum = $"{weatherForecast.data.currencies[0].saleSum}".ToString();
                saleCashSum = $"{weatherForecast.data.currencies[0].saleCashSum}".ToString();
                saleCashlessSum = $"{weatherForecast.data.currencies[0].saleCashlessSum}".ToString();
                salePrepaymentSum = $"{weatherForecast.data.currencies[0].salePrepaymentSum}".ToString();
                saleCreditSum = $"{weatherForecast.data.currencies[0].saleCreditSum}".ToString();
                saleBonusSum = $"{weatherForecast.data.currencies[0].saleBonusSum}".ToString();

                depositCount = $"{weatherForecast.data.currencies[0].depositCount}".ToString();
                depositSum = $"{weatherForecast.data.currencies[0].depositSum}".ToString();
                moneyBackCount = $"{weatherForecast.data.currencies[0].moneyBackCount}".ToString();
                moneyBackSum = $"{weatherForecast.data.currencies[0].moneyBackSum}".ToString();
                moneyBackCashSum = $"{weatherForecast.data.currencies[0].moneyBackCashSum}".ToString();
                moneyBackCashlessSum = $"{weatherForecast.data.currencies[0].moneyBackCashlessSum}".ToString();

                vatSuma = $"{weatherForecast.data.currencies[0].saleVatAmounts[0].vatSum}".ToString();

                if (!($"{weatherForecast.data.currencies[0].moneyBackVatAmounts}"?.Length > 0))
                {
                    vatSumma = $"{weatherForecast.data.currencies[0].moneyBackVatAmounts[0].vatSum}".ToString();
                }

                if (MessageVisible)
                {
                    ReadyMessages.SUCCESS_X_REPORT_MESSAGE();
                }

                FormHelpers.Log(CommonData.SUCCESS_X_REPORT);


                PrintDocument pd = new PrintDocument();
                PrinterSettings settings = new PrinterSettings();
                PageSettings pageSettings = new PageSettings(settings);
                PaperSize paperSize = settings.DefaultPageSettings.PaperSize;

                pd.DefaultPageSettings = new PageSettings();
                pd.DefaultPageSettings.PaperSize = paperSize;
                pd.PrintPage += new PrintPageEventHandler(nka_xhesabat);

                pagesCount = 2;

                PrintDialog PrintDialog1 = new PrintDialog();

                PrintDialog1.Document = pd;

                pd.Print();

            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE(e.Message);
            }
        }

        private void tDoctor_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            fSelectedData<POS_LAYOUT_NEW> doctor = new fSelectedData<POS_LAYOUT_NEW>(this, SelectedDataType.Doctor);
            doctor.ShowDialog();
        }

        private void bZakazPrint_Click(object sender, EventArgs e)
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("ClinicModule").ToString());
            if (control)
            {
                if (gridView1.RowCount > 0)
                {
                    if (string.IsNullOrWhiteSpace(tCustomer.Text))
                    {
                        FormHelpers.Alert("Müştəri seçimi edilmədi", MessageType.Warning);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(tDoctor.Text))
                    {
                        FormHelpers.Alert("Həkim seçimi edilmədi", MessageType.Warning);
                        return;
                    }
                    DbProsedures.Insert_ClinicData(tCustomer.Text, tDoctor.Text);

                    printClinic zakaz = new printClinic();
                    zakaz.Print();

                    CalculationDelete();
                    clear();
                }
            }
        }

        private void tCustomer_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            fSelectedData<POS_LAYOUT_NEW> customers = new fSelectedData<POS_LAYOUT_NEW>(this, SelectedDataType.Customer);
            customers.ShowDialog();
        }

        private bool XezerClinicPrint()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("ClinicModule").ToString());
            if (control)
            {
                if (gridView1.RowCount > 0)
                {
                    if (string.IsNullOrWhiteSpace(tCustomer.Text))
                    {
                        FormHelpers.Alert("Müştəri seçimi edilmədi", MessageType.Warning);
                        return false;
                    }

                    if (string.IsNullOrWhiteSpace(tDoctor.Text))
                    {
                        FormHelpers.Alert("Həkim seçimi edilmədi", MessageType.Warning);
                        return false;
                    }

                    DbProsedures.Insert_ClinicData(tCustomer.Text, tDoctor.Text);

                    printXezerClinic zakaz = new printXezerClinic();
                    zakaz.Print();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                FormHelpers.Alert("Klinika modulu aktiv edilməyib", MessageType.Warning);
                return false;
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            fPrepayment f = new fPrepayment();
            f.ShowDialog();
            //prepaymentsales bt = new prepaymentsales(this);
            //if (bt.ShowDialog() is DialogResult.Cancel)
            //{
            //    return;
            //}

        }

        private void bClinicPrint_Click(object sender, EventArgs e)
        {
            if (XezerClinicPrint())
            {
                CalculationDelete();
                clear();
            }
        }

        public void prepaymentsalesfinish(string fisid, Enums.PayType type)
        {
            string query = $@"SELECT pos_satis_check_main_id,
                            Prepayment,
                            fiscal_id,
                            UMUMI_MEBLEG
                            FROM [pos_satis_check_main]
                            where Prepayment>0
                            and fiscalNum= '{fisid}'";

            var data = DbProsedures.ConvertToDataTable(query);
            int number = data.Rows[0].Field<int>("pos_satis_check_main_id");
            decimal prepay = data.Rows[0].Field<decimal>("Prepayment");
            string fiskalid = data.Rows[0].Field<string>("fiscal_id");
            decimal total = data.Rows[0].Field<decimal>("UMUMI_MEBLEG");


            if (number > 0)
            {
                switch (lModel.Text)
                {
                    case "1":
                        Sunmi.PrepaymentSale(new DTOs.SalesDto
                        {
                            IpAddress = lIpAdress.Text,
                            Cashier = tUsername.Text,
                            Total = total,
                            FiscalId = fiskalid,
                            PrepaymentPay = prepay,
                            PayType = type
                        }, number);
                        break; /*SUNMI*/
                    case "3":
                        Omnitech.PrepaymentSale(lIpAdress.Text, textBox1.Text, number, tUsername.Text, fiskalid, prepay, type);
                        break; /*OMNITECH*/
                }
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE("Fiskal id nömrəsi düzgün daxil edilmədi");
            }
        }

        private void simpleButton15_Click_1(object sender, EventArgs e)
        {
            Payment(Enums.PayType.Prepayment);
        }

        private void bDeposit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fDeposit n = new fDeposit(this);
            n.ShowDialog();
        }

        private void bWithdraw_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fWithdraw n = new fWithdraw(this);
            n.ShowDialog();
        }

        private void bPrintClinic_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XezerClinicPrint())
            {
                CalculationDelete();
                clear();
            }
        }

        private void bBarShotcurt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fShortcuts f = new fShortcuts();
            f.ShowDialog();
        }

        private void simpleButton26_Click(object sender, EventArgs e)
        {
            Payment(PayType.Installment);
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            fWithdraw n = new fWithdraw(this);

            n.ShowDialog();
        }

        public void nba_X_Reportaylik(string _ip_)
        {
            try
            {
                string url = _ip_;
                DateTime dt_Ay = DateTime.Now;
                DateTime dt_Ay_ilkGun = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // Ay ilk günü
                DateTime dt_Ay_sonGun = dt_Ay_ilkGun.AddMonths(1).AddDays(-1);// Ay son günü

                var client = new RestClient();
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");

                var body = @" { ""parameters"":{ ""access_token"":""" + textBox1.Text + " \",\"from\":\"" + dt_Ay_ilkGun.ToString("yyyy-MM-dd") + "T00:00:56Z\",\"to\":\"" + dt_Ay_sonGun.ToString("yyyy-MM-dd") + "T23:59:56Z\"},\"operationId\":\"getPeriodicZReport\",\"version\":1}";
                request.AddStringBody(body, DataFormat.Json);
                RestResponse response = client.Execute(request);

                nbaroot weatherForecast = System.Text.Json.JsonSerializer.Deserialize<nbaroot>(response.Content);

                string a1 = $"{weatherForecast.message}";


                nacilma = $"{weatherForecast.data.shiftOpenAtUtc}";
                rno = $"{weatherForecast.data.reportNumber}";
                ndoc = $"{weatherForecast.data.shiftCloseAtUtc}";
                firstDocNumber = $"{weatherForecast.data.firstDocNumber}".ToString();
                lastDocNumber = $"{weatherForecast.data.lastDocNumber}".ToString();
                zdocument = $"{weatherForecast.data.document_id}";

                saleCount = $"{weatherForecast.data.currencies[0].saleCount}".ToString();
                saleSum = $"{weatherForecast.data.currencies[0].saleSum}".ToString();
                saleCashSum = $"{weatherForecast.data.currencies[0].saleCashSum}".ToString();
                saleCashlessSum = $"{weatherForecast.data.currencies[0].saleCashlessSum}".ToString();
                salePrepaymentSum = $"{weatherForecast.data.currencies[0].salePrepaymentSum}".ToString();
                saleCreditSum = $"{weatherForecast.data.currencies[0].saleCreditSum}".ToString();
                saleBonusSum = $"{weatherForecast.data.currencies[0].saleBonusSum}".ToString();

                depositCount = $"{weatherForecast.data.currencies[0].depositCount}".ToString();
                moneyBackCount = $"{weatherForecast.data.currencies[0].moneyBackCount}".ToString();
                moneyBackSum = $"{weatherForecast.data.currencies[0].moneyBackSum}".ToString();
                moneyBackCashSum = $"{weatherForecast.data.currencies[0].moneyBackCashSum}".ToString();
                moneyBackCashlessSum = $"{weatherForecast.data.currencies[0].moneyBackCashlessSum}".ToString();

                vatSuma = $"{weatherForecast.data.currencies[0].saleVatAmounts[0].vatSum}".ToString();

                if (!($"{weatherForecast.data.currencies[0].moneyBackVatAmounts}"?.Length > 0))
                {
                    vatSumma = $"{weatherForecast.data.currencies[0].moneyBackVatAmounts[0].vatSum}".ToString();
                }

                XtraMessageBox.Show("AYLIQ Z HESABATI UĞURLA ÇIXARILDI");



                PrintDocument pd = new PrintDocument();
                PrinterSettings settings = new PrinterSettings();
                PageSettings pageSettings = new PageSettings(settings);
                PaperSize paperSize = settings.DefaultPageSettings.PaperSize;

                pd.DefaultPageSettings = new PageSettings();
                pd.DefaultPageSettings.PaperSize = paperSize;
                pd.PrintPage += new PrintPageEventHandler(nka_xhesabataylik);

                pagesCount = 2;

                PrintDialog PrintDialog1 = new PrintDialog();

                PrintDialog1.Document = pd;

                pd.Print();

            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE(e.Message);
            }
        }

        satis_json st_ = new satis_json();

        private void AzSmartInstallmentSales(string ip, string cashier, decimal total)
        {
            st.update_calculation_tr();
            DbProsedures.DeleteItem();

            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);

                DbProsedures.InsertItem(new DatabaseClasses.Item
                {
                    Name = row["MƏHSUL ADI"].ToString(),
                    Code = "Code2",
                    Quantity = Convert.ToDecimal(row["SAY"]),
                    SalePrice = Convert.ToDecimal(row["SATIŞ QİYMƏTİ"]),
                    PurchasePrice = Convert.ToDecimal(row["ALIŞ QİYMƏTİ"]),
                    vatType = Convert.ToInt32(row["EDV_ID"]),
                    QuantityType = Convert.ToInt32(row["VAHIDLER_ID"]),
                    ProductId = Convert.ToInt32(row["MAL_ALISI_DETAILS_ID"])
                });
            }

            DbProsedures.DeleteHeader();
            DbProsedures.InsertHeader(new Header
            {
                cash = 0,
                card = total,
                CustomerName = "YENİ MÜŞTƏRİ",
                paidPayment = 0
            });

            bool IsSuccess = AzSmart.InstallmentSales(lIpAdress.Text, lMerchantId.Text, textEdit1.Text, total, cashier);

            if (IsSuccess)
            {
                clear();
                textEdit1.Text = DbProsedures.GET_SalesProcessNo();
                textEdit11.Text = DbProsedures.GET_TotalSalesCount();
                CalculationDelete();
            }
        }

        /// <summary>
        /// X HESABAT
        /// </summary>
        private void simpleButton21_Click(object sender, EventArgs e)
        {
            switch (lModel.Text)
            {
                case "1":
                    Sunmi.X_Report(lIpAdress.Text, tUsername.Text);
                    break; /*SUNMI*/
                case "2":
                    AzSmart.XReport(lIpAdress.Text, lMerchantId.Text, tUsername.Text);
                    break; /*AZSMART*/
                case "3":
                    Omnitech.XReport(lIpAdress.Text, textBox1.Text);
                    break; /*OMNITECH*/
                case "4":
                    FormHelpers.Alert("Xprinter istifadəçiləri üçün funksiya müvəqqəti olaraq deaktiv edilmiştir", MessageType.Info);
                    break; /*XPRINTER*/
                case "5":
                    DataPay.X_Report(lIpAdress.Text, textBox1.Text);
                    break; /*DATAPAY*/
                case "6":
                    nba_X_Report(lIpAdress.Text, textBox1.Text);
                    break; /*NBA*/

                case "7":
                    EKASAM.XReport(lIpAdress.Text, textBox1.Text);
                    break; /*EKASSAM*/
            }
        }

        /// <summary>
        /// NBA AYLIQ HESABAT
        /// </summary>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (lModel.Text == "6")
            {
                nba_X_Reportaylik(lIpAdress.Text);
            }
            else
            {
                FormHelpers.Alert("Sadəcə NBA kassalarında istifadə olunabilər", MessageType.Info);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            getbarkod_mehsuladi(textBox5.Text);
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getall(tBarcode.Text);
                get(textEdit1.Text);
                get_say_birmal(tBarcode.Text, textEdit1.Text);
                tBarcode.Text = string.Empty;

                get_cem(textEdit1.Text);
                textBox5.Text = "";
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "MƏHSUL ADI";
                textBox5.ForeColor = Color.Black;
                textBox5.Font = new Font("Tahoma", 16, FontStyle.Bold);
            }

        }

        private void bInstallment_Click(object sender, EventArgs e)
        {
            switch (lModel.Text)
            {
                case "2":
                    Payment(PayType.Installment);
                    break;
                default:
                    Alert("İstifadə etdiyiniz NKA taksit ödənişləri üçün nəzərdə tutulmayıb", MessageType.Warning);
                    break;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "MƏHSUL ADI")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.Black;
                textBox5.Font = new Font("Tahoma", 12, FontStyle.Regular);
            }
        }

        private void POS_LAYOUT_NEW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers is Keys.Control && e.KeyCode is Keys.N)
            {
                gridView1.SelectAll();
                DeleteButton();
            }

            switch (e.KeyCode)
            {
                case Keys.F1:
                    tBarcode.Focus();
                    break;
                case Keys.F2:
                    simpleButton22_Click(null, null);
                    break;
                case Keys.F3:
                    simpleButton24_Click(null, null);
                    break;
                case Keys.F5:
                    Payment(Enums.PayType.Cash);
                    break;
                case Keys.F6:
                    Payment(Enums.PayType.Card);
                    break;
                case Keys.F7:
                    Payment(Enums.PayType.CashCard);
                    break;
                case Keys.F8:
                    simpleButton23_Click(null, null);
                    break;
                case Keys.F9:
                    simpleButton25_Click(null, null);
                    break;
            }
        }

        private void bShortcut_Click(object sender, EventArgs e)
        {
            fShortcuts f = new fShortcuts();
            f.ShowDialog();
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {

                int paramValue = Convert.ToInt32(dr[0]);
                dele_migdar_mal_id = Convert.ToInt32(dr[0]);

                textEdit10.Text = dr["say"].ToString();
                textEdit9.Text = dr[4].ToString();
                string queryString = "exec CALC_SAY_CALCULATION @MAL_ALISI_DETAILS_ID=@pricePoint ";

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
                            textEdit10.Text = reader[0].ToString();
                        }

                        reader.Close();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void get_ip_model()
        {
            var data = FormHelpers.GetIpModel();

            lModel.Text = data.Model;
            lIpAdress.Text = data.Ip;
            lMerchantId.Text = data.MerchantId;
        }

        private void simpleButton25_Click(object sender, EventArgs e)
        {
            FormHelpers.OpenForm<POS_GAYTARMA_LAYOUT>(textBox1.Text, tUsername.Text);
        }

        public void gelen_data_negd_pos(decimal cash_, decimal card_, decimal umumi_mebleg_, decimal incomingSum = default, decimal _qaliq = default, bool clinic = false, PayType payType = PayType.Empty)
        {
            try
            {
                st.update_calculation_tr();
                DbProsedures.DeleteItem();
                decimal _discount = 0;
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    DataRow row = gridView1.GetDataRow(i);

                    decimal say = Convert.ToDecimal(row["SAY"]);
                    decimal salePrice = Convert.ToDecimal(row["SATIŞ QİYMƏTİ"]);
                    int vatType = Convert.ToInt32(row["EDV_ID"]);
                    int quantityType = Convert.ToInt32(row["VAHIDLER_ID"]);
                    int productId = Convert.ToInt32(row["MAL_ALISI_DETAILS_ID"]);
                    _discount = Convert.ToDecimal(row["GÜZƏŞT"]);
                    decimal purchasePrice = 0;
                    string barcode = "Code2";
                    if (row.Table.Columns.Contains("ALIŞ QİYMƏTİ") && !row.IsNull("ALIŞ QİYMƏTİ"))
                    {
                        purchasePrice = Convert.ToDecimal(row["ALIŞ QİYMƏTİ"]);
                    }
                    if (row.Table.Columns.Contains("BARKOD") && !row.IsNull("BARKOD"))
                    {
                        barcode = row["BARKOD"].ToString();
                    }


                    DbProsedures.InsertItem(new DatabaseClasses.Item
                    {
                        Name = row["MƏHSUL ADI"].ToString(),
                        Code = barcode,
                        Quantity = say,
                        SalePrice = salePrice,
                        PurchasePrice = purchasePrice,
                        vatType = vatType,
                        QuantityType = quantityType,
                        ProductId = productId
                    });
                }


                DbProsedures.DeleteHeader();
                DbProsedures.InsertHeader(new Header
                {
                    cash = cash_,
                    card = card_,
                    CustomerName = string.IsNullOrWhiteSpace(tCustomer.Text) ? "YENİ MÜŞTƏRİ" : tCustomer.Text,
                    paidPayment = incomingSum
                });

                //RRN KODU MANUAL OLARAQ YAZMAQ ÜÇÜN
                if (card_ > 0 && bankttnmd == "1")
                {
                    Bankttnminput bt = new Bankttnminput(this);
                    if (bt.ShowDialog() is DialogResult.Cancel)
                    {
                        return;
                    }

                }
                else
                {
                    bankttnminputdata = "";
                }


                if (clinic)
                {
                    if (!XezerClinicPrint())
                        return;
                }

                bool IsSuccess = false;

                //Satışı Xprinterə yönləndirmək üçün
                if (chSendToPrinter.Visible is true && chSendToPrinter.Checked is true)
                {
                    xprintersales(cash_, card_, umumi_mebleg_, incomingSum);
                    return;
                }



                switch (lModel.Text)
                {
                    case "1":
                        IsSuccess = Sunmi.Sales(new DTOs.SalesDto
                        {
                            IpAddress = lIpAdress.Text,
                            ProccessNo = textEdit1.Text,
                            Cash = incomingSum,
                            Card = card_,
                            Total = umumi_mebleg_,
                            Cashier = tUsername.Text,
                            Customer = null,
                            Doctor = null,
                            Rrn = bankttnminputdata
                        });

                        if (IsSuccess)
                        {
                            clear();
                            textEdit11.Text = DbProsedures.GET_TotalSalesCount();
                            CalculationDelete();
                        }

                        if (IsSuccess)
                        {
                            clear();
                            textEdit11.Text = DbProsedures.GET_TotalSalesCount();
                            CalculationDelete();
                        }
                        break; /*SUNMI*/
                    case "2":
                        IsSuccess = AzSmart.Sales(new DTOs.SalesDto
                        {
                            IpAddress = lIpAdress.Text,
                            MerchantId = lMerchantId.Text,
                            ProccessNo = textEdit1.Text,
                            Total = umumi_mebleg_,
                            Cash = cash_,
                            Card = card_,
                            IncomingSum = incomingSum,
                            Cashier = tUsername.Text,
                            Rrn = bankttnminputdata
                        });

                        if (IsSuccess)
                        {
                            clear();
                            textEdit11.Text = DbProsedures.GET_TotalSalesCount();
                            CalculationDelete();
                        }
                        break; /*AZSMART*/
                    case "3":
                        IsSuccess = Omnitech.Sales(lIpAdress.Text.Replace("\n", ""),
                            textBox1.Text,
                            textEdit1.Text,
                            umumi_mebleg_,
                            cash_,
                            card_,
                            incomingSum,
                            tUsername.Text,
                            _customer,
                            _doctor);

                        if (IsSuccess)
                        {
                            clear();
                            textEdit11.Text = DbProsedures.GET_TotalSalesCount();
                            CalculationDelete();
                        }
                        break; /*OMNITECH*/
                    case "4":
                        xprintersales(cash_, card_, umumi_mebleg_, incomingSum);
                        break; /*XPRINTER*/
                    case "5":
                        paysales(cash_, card_, umumi_mebleg_);
                        break; /*DATAPAY*/
                    case "6":
                        //NBA.Sales(new DTOs.SalesDto
                        //{
                        //    Cash = cash_,
                        //    Card = card_,
                        //    Total = umumi_mebleg_,
                        //    IncomingSum = incomingSum,
                        //    Balance = _qaliq,
                        //    PayType = payType,
                        //    IpAddress = lIpAdress.Text,
                        //    AccessToken = textBox4.Text
                        //});

                        nbasales(new DTOs.SalesDto
                        {
                            Cash = cash_,
                            Card = card_,
                            Total = umumi_mebleg_,
                            IncomingSum = incomingSum,
                            Balance = _qaliq,
                            PayType = payType
                        });
                        break; /*NBA*/
                    case "7":
                        IsSuccess = EKASAM.Sales(new DTOs.SalesDto
                        {
                            IpAddress = lIpAdress.Text.Replace("\n", ""),
                            AccessToken = textBox1.Text,
                            ProccessNo = textEdit1.Text,
                            Total = umumi_mebleg_,
                            Cash = cash_,
                            Card = card_,
                            IncomingSum = incomingSum,
                            Cashier = tUsername.Text,
                            Customer = _customer,
                            Doctor = _doctor,
                            Balance = incomingSum - cash_
                        });

                        if (IsSuccess)
                        {
                            clear();
                            textEdit11.Text = DbProsedures.GET_TotalSalesCount();
                            CalculationDelete();
                        }
                        break; /*EKASSAM*/
                }
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
            }
            finally
            {
                textEdit1.Text = DbProsedures.GET_SalesProcessNo();
            }

            tBarcode.Focus();
        }

        public void gelen_data_negd_pos_pre(decimal cash_, decimal card_, decimal umumi_mebleg_, decimal incomingSum = default, decimal _qaliq = default, bool clinic = false)
        {
            try
            {
                st.update_calculation_tr();
                DbProsedures.DeleteItem();
                decimal _discount = 0;
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    DataRow row = gridView1.GetDataRow(i);

                    decimal say = Convert.ToDecimal(row["SAY"]);
                    decimal salePrice = Convert.ToDecimal(row["SATIŞ QİYMƏTİ"]);
                    int vatType = Convert.ToInt32(row["EDV_ID"]);
                    int quantityType = Convert.ToInt32(row["VAHIDLER_ID"]);
                    int productId = Convert.ToInt32(row["MAL_ALISI_DETAILS_ID"]);
                    _discount = Convert.ToDecimal(row["GÜZƏŞT"]);
                    decimal purchasePrice = 0;
                    string barcode = "Code2";
                    if (row.Table.Columns.Contains("ALIŞ QİYMƏTİ") && !row.IsNull("ALIŞ QİYMƏTİ"))
                    {
                        purchasePrice = Convert.ToDecimal(row["ALIŞ QİYMƏTİ"]);
                    }
                    if (row.Table.Columns.Contains("BARKOD") && !row.IsNull("BARKOD"))
                    {
                        barcode = row["BARKOD"].ToString();
                    }


                    DbProsedures.InsertItem(new DatabaseClasses.Item
                    {
                        Name = row["MƏHSUL ADI"].ToString(),
                        Code = barcode,
                        Quantity = say,
                        SalePrice = salePrice,
                        PurchasePrice = purchasePrice,
                        vatType = vatType,
                        QuantityType = quantityType,
                        ProductId = productId
                    });
                }


                DbProsedures.DeleteHeader();
                DbProsedures.InsertHeader(new Header
                {
                    cash = cash_,
                    card = card_,
                    CustomerName = string.IsNullOrWhiteSpace(tCustomer.Text) ? "YENİ MÜŞTƏRİ" : tCustomer.Text,
                    paidPayment = incomingSum,
                    PayType = PayType.Prepayment
                });

                //RRN KODU MANUAL OLARAQ YAZMAQ ÜÇÜN
                if (card_ > 0 && bankttnmd == "1")
                {
                    Bankttnminput bt = new Bankttnminput(this);
                    if (bt.ShowDialog() is DialogResult.Cancel)
                    {
                        return;
                    }

                }
                else
                {
                    bankttnminputdata = "";
                }


                if (clinic)
                {
                    if (!XezerClinicPrint())
                        return;
                }

                bool IsSuccess = false;

                //Satışı Xprinterə yönləndirmək üçün
                if (chSendToPrinter.Visible is true && chSendToPrinter.Checked is true)
                {
                    ReadyMessages.WARNING_DEFAULT_MESSAGE("Avans satışları yalnız NKA Kassaları üçün nəzərdə tutulmuşdur");
                    return;
                }



                switch (lModel.Text)
                {
                    case "1":
                        IsSuccess = Sunmi.Prepayment(new DTOs.SalesDto
                        {
                            IpAddress = lIpAdress.Text,
                            ProccessNo = textEdit1.Text,
                            IncomingSum = incomingSum,
                            Cash = cash_,
                            Card = card_,
                            Total = umumi_mebleg_,
                            PrepaymentPay = cash_,
                            Cashier = tUsername.Text,
                            Customer = _customer,
                            Doctor = _doctor,
                            Rrn = bankttnminputdata
                        });

                        if (IsSuccess)
                        {
                            clear();
                            textEdit11.Text = DbProsedures.GET_TotalSalesCount();
                            CalculationDelete();
                        }
                        break; /*SUNMI*/
                    case "3":
                        IsSuccess = Omnitech.Prepayment(new DTOs.SalesDto
                        {
                            IpAddress = lIpAdress.Text.Replace("\n", ""),
                            AccessToken = textBox1.Text,
                            ProccessNo = textEdit1.Text,
                            Total = umumi_mebleg_,
                            Cash = cash_,
                            Card = card_,
                            IncomingSum = incomingSum,
                            Cashier = tUsername.Text,
                            Customer = _customer,
                            Doctor = _doctor
                        });

                        if (IsSuccess)
                        {
                            clear();
                            textEdit11.Text = DbProsedures.GET_TotalSalesCount();
                            CalculationDelete();
                        }
                        break; /*OMNITECH*/
                }
            }
            catch (WebException ex) when (ex.Status is WebExceptionStatus.ConnectFailure)
            {
                //throw;
                ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE(ex.Message);
            }
            catch (WebException ex)
            {
                //throw;
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
            }
            finally
            {
                textEdit1.Text = DbProsedures.GET_SalesProcessNo();
            }

            tBarcode.Focus();
        }


        public override void ReceiveData<T>(T data)
        {
            if (data is Customer customer)
            {
                _customer = customer;
                tCustomer.Text = $"{customer.Name} {customer.Surname} {customer.FatherName}";
            }
            else if (data is Doctor doctor)
            {
                _doctor = doctor;
                tDoctor.Text = doctor.NameSurname;
            }
        }

        private void clear()
        {
            gridControl1.DataSource = null;
            textEdit9.Text = "0";
            textEdit10.Text = "0";
            textEdit12.Text = "0";
            textEdit13.Text = "0";
            textEdit6.Text = "";
            textEdit7.Text = "";
            tCustomer.Text = "";
            textBox5.Text = "";
            tCustomer.Text = null;
            tDoctor.Text = null;
            gridView1.GroupPanelText = $"Məhsul sayı: {gridView1.RowCount}";
            _customer = null;
        }

        public static string CleanJson(string jsonString)
        {
            JObject json = JObject.Parse(jsonString);

            foreach (JProperty property in json.Properties())
            {
                if (property.Value.Type == JTokenType.Array)
                {
                    JArray array = (JArray)property.Value;
                    foreach (JObject item in array)
                    {
                        foreach (JProperty innerProperty in item.Properties())
                        {
                            if (innerProperty.Value.Type == JTokenType.String)
                            {
                                innerProperty.Value = innerProperty.Value.ToString().Trim();
                            }
                        }
                    }
                }
                else if (property.Value.Type == JTokenType.String)
                {
                    property.Value = property.Value.ToString().Trim();
                }
            }
            return json.ToString();
        }

        public void getbarkod_mehsuladi(string mehsul_adi)
        {
            string paramValue = mehsul_adi;
            try
            {
                using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    connection.Open();
                    string query = "select * from  dbo.POS_autocomplete_search_mehsul_Adi(@pricePoint);";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@pricePoint", paramValue);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                tBarcode.Text = dr["BARKOD"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormHelpers.OpenForm<POS_MEHSUL_AXTARIS>();
        }

        /// <summary>
        /// TƏKRAR QƏBZ
        /// </summary>
        private void simpleButton23_Click(object sender, EventArgs e)
        {
            switch (lModel.Text)
            {
                case "1":
                    Sunmi.LastDocument(lIpAdress.Text);
                    break; /*SUNMI*/
                case "2":
                    AzSmart.LastReceiptCopy(lIpAdress.Text, lMerchantId.Text, tUsername.Text);
                    break; /*AZSMART*/
                case "3":
                    Omnitech.LastReceiptCopy(lIpAdress.Text, textBox1.Text);
                    break; /*OMNITECH*/
                case "4":
                    XprinterTekrar();
                    break; /*XPRINTER*/
                case "5":
                    break; /*DATAPAY*/
                case "6":
                    nbatekrar();
                    break; /*NBA*/

                case "7":
                    EKASAM.LastReceiptCopy(lIpAdress.Text, textBox1.Text);
                    break; /*EKASSAM*/
            }
        }

        private void XprinterTekrar()
        {
            PrintDocument pd = new PrintDocument();
            PrinterSettings settings = new PrinterSettings();
            PageSettings pageSettings = new PageSettings(settings);
            PaperSize paperSize = settings.DefaultPageSettings.PaperSize;

            pd.DefaultPageSettings = new PageSettings();
            pd.DefaultPageSettings.PaperSize = paperSize;
            pd.PrintPage += new PrintPageEventHandler(xprinterTekrarQebz);

            pagesCount = 1;

            PrintDialog PrintDialog1 = new PrintDialog();

            PrintDialog1.Document = pd;

            pd.Print();
            if (MessageVisible)
            {
                ReadyMessages.SUCCESS_LAST_DOCUMENT_MESSAGE();
            }

            FormHelpers.Log(CommonData.SUCCESS_LAST_DOCUMENT);
        }

        private void xprinterTekrarQebz(object sender, PrintPageEventArgs e)
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
            string _incomingSum = "";
            string kart = "";

            int offset = 170;
            int offset2 = 10;
            int m = 0;
            int n = 20;

            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlConnection conn2 = new SqlConnection();
            SqlCommand cmd2 = new SqlCommand();
            conn2.ConnectionString = Properties.Settings.Default.SqlCon;
            conn2.Open();
            string query2 = "select round(cashPayment,2) as cashPayment ,round(cardPayment,2) as cardPayment,paidPayment,bonusPayment ,clientName ,header_id,cashPayment +cardPayment as tot,(SELECT TOP 1   [COMPANY_NAME]    FROM [COMPANY].[COMPANY]) AS sirket,(SELECT TOP 1   [SIRKET_VOEN]    FROM [COMPANY].[COMPANY]) as voen from  dbo.header;";

            cmd2.Connection = conn2;
            cmd2.CommandText = query2;

            SqlDataReader dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {
                string cash = dr2["cashPayment"].ToString();
                string card = dr2["cardPayment"].ToString();
                string bonus = dr2["bonusPayment"].ToString();
                string client = dr2["clientName"].ToString();
                string tot = dr2["tot"].ToString();
                string sirket = dr2["sirket"].ToString();
                string incomingSum = dr2["paidPayment"].ToString();
                string voen = dr2["voen"].ToString();


                e.Graphics.DrawString("  " + sirket, company, Brushes.Black, new Point(90, offset2));
                e.Graphics.DrawString("VÖEN :" + voen, font3, Brushes.Black, new Point(85, offset2 + 15));
                e.Graphics.DrawString("Satış Çeki(Təkrar Nüsxə)", font4, Brushes.Black, new Point(60, offset2 + 40));
                e.Graphics.DrawString("Çek nömrəsi No:" + textEdit1.Text, font2, Brushes.Black, new Point(70, offset2 + 55));
                e.Graphics.DrawString("Kassir: " + tUsername.Text, font, Brushes.Black, new Point(5, offset2 + 80));
                e.Graphics.DrawString("Tarix: " + DateTime.Now.ToString("dd.MM.yyyy"), font, Brushes.Black, new Point(190, offset2 + 80));
                e.Graphics.DrawString("Saat: " + DateTime.Now.ToString("HH:mm"), font, Brushes.Black, new Point(190, offset2 + 95));

                e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 110));
                e.Graphics.DrawString("Malın adı", f8, Brushes.Black, 5, offset2 + 120);
                e.Graphics.DrawString("Miqdar", f8, Brushes.Black, 130, offset2 + 120);
                e.Graphics.DrawString("Qiymət", f8, Brushes.Black, 190, offset2 + 120);
                e.Graphics.DrawString("Toplam", f8, Brushes.Black, 240, offset2 + 120);
                e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset2 + 130));

                tutar = tot;
                kart = card;
                nagd = cash;
                _incomingSum = incomingSum;
            }

            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.SqlCon;
            conn.Open();
            string query = $"select name,code,ROUND(salePrice,2) as salePrice,ROUND(quantity,2) AS quantity,case  vatType when 1 then '18' when 3 then '0' when 4 then '2' when 5 then '8' else 0 end as vatType,round(quantityType,2) as quantityType,ROUND(salePrice*quantity,2) as ssum from  dbo.item WHERE user_id = {Properties.Settings.Default.UserID};";

            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string name = dr["name"].ToString();
                string code = dr["code"].ToString();
                string sprice = dr["salePrice"].ToString();
                string qty = dr["quantity"].ToString();
                string vat = dr["vatType"].ToString();
                string qunit = dr["quantityType"].ToString();
                string ssum = dr["ssum"].ToString();

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
            double qalıq = Convert.ToDouble(_incomingSum) - Convert.ToDouble(nagd);
            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset + m));
            e.Graphics.DrawString("YEKUN MƏBLƏĞ:", f9, Brushes.Black, 5, offset + m + 20);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(tutar)), f9, Brushes.Black, new Point(240, offset + m + 20));
            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset + m + 30));
            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset + m + 50));
            e.Graphics.DrawString("Ödəniş növü", f8, Brushes.Black, 5, offset + m + 70);
            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 5, offset + m + 80);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(kart)), f8, Brushes.Black, 240, offset + m + 80);
            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 5, offset + m + 90);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(nagd)), f8, Brushes.Black, 240, offset + m + 90);
            e.Graphics.DrawString("Ödənilib nağd:", f8, Brushes.Black, 5, offset + m + 100);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(_incomingSum)), f8, Brushes.Black, 240, offset + m + 100);
            e.Graphics.DrawString("Qalıq qaytarılıb nağd:", f8, Brushes.Black, 5, offset + m + 110);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(qalıq)), f8, Brushes.Black, 240, offset + m + 110);
        }

        private NBA.LastDocumentResponse nbaLastDocumentResponse;
        private void nbatekrar()
        {
            nbaLastDocumentResponse = NBA.LastDocument(lIpAdress.Text);
            if (nbaLastDocumentResponse != null)
            {
                PrintDocument pd = new PrintDocument();
                PrinterSettings settings = new PrinterSettings();
                PageSettings pageSettings = new PageSettings(settings);
                PaperSize paperSize = settings.DefaultPageSettings.PaperSize;

                pd.DefaultPageSettings = new PageSettings();
                pd.DefaultPageSettings.PaperSize = paperSize;
                pd.PrintPage += new PrintPageEventHandler(nba_saleprinttekrar);

                PrintDialog PrintDialog1 = new PrintDialog();


                PrintDialog1.Document = pd;

                pd.Print();

                if (MessageVisible)
                {
                    ReadyMessages.SUCCESS_LAST_DOCUMENT_MESSAGE();
                }

                Log(CommonData.SUCCESS_LAST_DOCUMENT);
            }
        }




        public void withdraw(decimal deposit)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    ReadyMessages.WARNING_DEFAULT_MESSAGE("NÖVBƏ AÇILMAYIB !\nPOS AÇ DÜYMƏSİNƏ VURARAQ NÖVBƏNİ AÇIN");
                    return;
                }

                withdrawa = deposit.ToString();
                var url = lIpAdress.Text;
                string parameters = "{\"parameters\":{\"access_token\":\"" + textBox1.Text + "\",\"doc_type\":\"withdraw\",\"data\":{\"cashier\":\"" + tUsername.Text + "\",\"currency\":\"AZN\",\"sum\":\"" + deposit + "\"}},\"operationId\":\"createDocument\",\"version\":1}";


                var client = new RestClient();
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json;charset=utf-8");
                request.AddStringBody(parameters, DataFormat.Json);
                RestResponse response = client.Execute(request);

                nbaroot weatherForecast = System.Text.Json.JsonSerializer.Deserialize<nbaroot>(response.Content);


                sdocumentid = $"{weatherForecast.data.short_document_id}";
                YekunMebleg = deposit.ToString();

                fissayi = $"{weatherForecast.data.document_number}";

                gunfissayi = $"{weatherForecast.data.shift_document_number}".ToString();



                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings = new PageSettings
                {
                    PaperSize = new PrinterSettings().DefaultPageSettings.PaperSize
                };

                pd.PrintPage += new PrintPageEventHandler(nba_withdraw);

                pagesCount = 1;


                PrintDialog PrintDialog1 = new PrintDialog
                {
                    Document = pd
                };

                pd.Print();



            }

            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
            }


        }


        private void nbasales(DTOs.SalesDto salesData/*decimal cash_, decimal card_, decimal umumi_mebleg_, decimal _incomingSum = default, decimal _qaliq = default*/)
        {
            string RequestSendJson = string.Empty;
            string ResponseSendJson = string.Empty;
            try
            {

                Cursor.Current = Cursors.WaitCursor;
                //if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
                //{
                //    ReadyMessages.WARNING_DEFAULT_MESSAGE("NÖVBƏ AÇILMAYIB !\nPOS AÇ DÜYMƏSİNƏ VURARAQ NÖVBƏNİ AÇIN");
                //    return;
                //}

                string cash = "";
                string tot = "";
                string p = "";
                string vatsa = "";
                string parameters = "{\"parameters\":{\"access_token\":\"" + textBox1.Text + "\",\"doc_type\":\"sale\",\"data\":{\"cashier\":\"" + tUsername.Text + "\",\"currency\":\"AZN\",";
                string productsa = "\"items\":[";
                string p2 = "],";
                string dataheadersa = " ";
                // string alldata = "";
                int satirsayi = 0;

                SqlConnection conn2 = new SqlConnection();
                SqlCommand cmd2 = new SqlCommand();
                conn2.ConnectionString = DbHelpers.DbConnectionString;
                conn2.Open();
                string query2 = DbHelpers.GetHeaderDataQuery;

                cmd2.Connection = conn2;
                cmd2.CommandText = query2;

                SqlDataReader dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {
                    if (decimal.Parse(dr2["paidPayment"].ToString()) > salesData.Cash)
                    {
                        cash = salesData.Total.ToString();
                        tot = salesData.Total.ToString();
                        odenen = dr2["paidPayment"].ToString();
                        qaliq = (decimal.Parse(dr2["paidPayment"].ToString()) - salesData.Cash).ToString();
                    }
                    else
                    {
                        cash = dr2["cashPayment"].ToString();
                        tot = dr2["tot"].ToString();
                        odenen = dr2["paidPayment"].ToString();
                        qaliq = "0";
                    }


                    SqlConnection connvat = new SqlConnection();
                    SqlCommand cmdvat = new SqlCommand();
                    connvat.ConnectionString = DbHelpers.DbConnectionString;
                    connvat.Open();
                    string queryvat = $@"select t.vatType,sum(t.ssum) as ssum,sum(t.ssumvat) as ssumvat from
                (select  case  vatType when 1 then '18.0' when 2 then '18.0' when 3 then '0.0' when 4 then '2.0' when 5 then '8.0' else 'bos' end as vatType,
                salePrice* quantity as ssum,
                ROUND(case vatType when 1 then (salePrice* quantity)*18/118 when 3 then 0 when 4 then salePrice* quantity*0.02 else 0 end,2) as ssumvat
                FROM dbo.item WHERE user_id = {Properties.Settings.Default.UserID}) as t group by t.vatType";

                    cmdvat.Connection = connvat;
                    cmdvat.CommandText = queryvat;


                    SqlDataReader drvat = cmdvat.ExecuteReader();
                    while (drvat.Read())
                    {

                        string vatType = drvat["vatType"].ToString();
                        string ssumvat = drvat["ssum"].ToString();


                        if (vatType == "18.0")
                        {
                            edvhesap1 = drvat["ssum"].ToString();
                            edvhesap2 = drvat["ssumvat"].ToString();
                        }
                        else
                        {

                            edvdenazada1 = drvat["ssum"].ToString();
                            edvdenazada2 = drvat["ssumvat"].ToString();
                        }

                        vatsa = vatsa + "{\"vatSum\" : " + ssumvat.Replace(",", ".") + "," + "\"vatPercent\":" + vatType + "},";

                    }

                    string vatsanew = vatsa.Substring(0, vatsa.Length - 1);




                    casha2 = cash;
                    tota2 = tot;
                    //string incomingSum = _incomingSum.ToString();
                    string card = dr2["cardPayment"].ToString();
                    string bonus = dr2["bonusPayment"].ToString();
                    string client86 = dr2["clientName"].ToString();


                    dataheadersa = "\"sum\"  : " + tot.Replace(",", ".") + "," +
                        "" +
                        " \"cashSum\" : " + salesData.Cash.ToString().Replace(",", ".") + "," +
                        "\"cashlessSum\": " + card.Replace(",", ".") + "," +
                        " \"prepaymentSum\" : 0.0," +
                        " \"creditSum\" : 0.0," +
                        "\"bonusSum\" : 0.0, " +
                        "\"incomingSum\" : " + salesData.IncomingSum.ToString().Replace(",", ".") + "," +
                        "\"changeSum\" : " + salesData.Balance.ToString().Replace(",", ".") + "," +
                        " \"vatAmounts\" : [" + vatsanew +

                           "  ] } },\"operationId\":\"createDocument\",\"version\":1 }";

                }
                clmns = 0;

                SqlConnection conn = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                conn.ConnectionString = DbHelpers.DbConnectionString;
                conn.Open();
                string query = $@"select name,Item.item_id,salePrice,quantity,
case  vatType 
when 1 then '18'
when 2 then '18'
when 3 then '0' 
when 4 then '2' 
when 5 then '8' 
else 0 end as vatType,
quantityType,
salePrice*quantity as ssum
from  dbo.item where user_id = {Properties.Settings.Default.UserID}";

                cmd.Connection = conn;
                cmd.CommandText = query;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr["name"].ToString();
                    string code = dr["item_id"].ToString();
                    string sprice = dr["salePrice"].ToString();
                    string qty = dr["quantity"].ToString();
                    string vat = dr["vatType"].ToString();
                    string qunit = dr["quantityType"].ToString();
                    string ssum = dr["ssum"].ToString();
                    satirsayi = satirsayi + 1;
                    p = p + "{\"itemName\":\"" + name + "\",\"itemCodeType\":0,\"itemCode\":\"" + code + "\",\"itemQuantityType\":" + qunit + ",\"itemQuantity\":" + qty.Replace(",", ".") + ",\"itemPrice\":" + sprice.Replace(",", ".") + ",\"itemSum\":" + ssum.Replace(",", ".") + ",\"itemVatPercent\":" + vat.Replace(",", ".") + " " + "},";
                    clmns = clmns + 1;
                }
                string pnew = p.Substring(0, p.Length - 1);

                string rrnCode = string.Empty;

                var url = lIpAdress.Text;
                //var url3 = "http://81.21.87.10:9942/transaction/start";


                if (salesData.PayType is PayType.OtherPay)
                {
                    goto otherPay;
                }
                else
                {
                    if (salesData.Card > 0)
                    {
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

                        var body2 = "{ \"type\":\"sale\",\"amount\":\"" + salesData.Card.ToString().Replace(",", "") + "\",\"currency\":\"AZN\",\"applicationid\":\"xxxxxxxx\", \"dontredirecttosale\":false}";
                        requestsend.AddStringBody(body2, DataFormat.Json);

                        RestResponse response2 = client2.Execute(requestsend);

                    banksalessend:
                        if (response2.Content == "ERROR!!! Please Check Banking APP")
                        {
                            XtraMessageBox.Show("ERROR!!! Please Check Banking APP", "Error");
                            goto banksalessend;
                        }
                        else
                        {
                            nbarootbank weatherForecastbank = JsonSerializer.Deserialize<nbarootbank>(response2.Content);

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

                                if (item.line.Contains("RRN") || item.line.Contains("rrn"))
                                {
                                    rrnCode = item.line.Split(':').Last().Trim();
                                }
                            }

                            bankdizic.Clear();
                            if (weatherForecastbankdetail.customerreceipt != null)
                            {
                                foreach (var item in weatherForecastbankdetail.customerreceipt)
                                {
                                    bankdizic.Add(item.line);
                                }
                            }


                            #region [..XƏZİNƏDAR QƏBZİ..]

                            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("TerminalCashierPrint").ToString());
                            if (control)
                            {
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
                            }

                            #endregion [..XƏZİNƏDAR QƏBZİ..]


                            #region [..MÜŞTƏRİ QƏBZİ..]

                            PrintDocument pd2 = new PrintDocument();
                            pd2.DefaultPageSettings = new PageSettings
                            {
                                PaperSize = new PrinterSettings().DefaultPageSettings.PaperSize
                            };
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
                            bankdizi.Clear();
                            bankdizic.Clear();

                            if (weatherForecastbankdetail.errorreceipt != null)
                            {
                                foreach (var item in weatherForecastbankdetail.errorreceipt)
                                {
                                    bankdizi.Add(item.line);
                                }
                            }
                            ReadyMessages.ERROR_BANK_MESSAGE(weatherForecastbankdetail.responsecodeText);


                            #region [..XƏZİNƏDAR QƏBZİ..]


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

                            return;
                        }
                        else
                        {
                            goto bankstart;
                        }
                        parameters = "{\"parameters\":{\"access_token\":\"" + textBox1.Text + "\",\"doc_type\":\"sale\",\"data\":{\"cashier\":\"" + tUsername.Text + "\",\"currency\":\"AZN\",\"rrn\":\"" + rrnCode + "\",";
                    }
                }

            otherPay:

                string json = NBA.Sales(new DTOs.SalesDto
                {
                    Cash = salesData.Cash,
                    Card = salesData.Card,
                    Total = salesData.Total,
                    IncomingSum = salesData.IncomingSum,
                    Balance = salesData.Balance,
                    PayType = salesData.PayType,
                    Rrn = rrnCode,
                    Cashier = tUsername.Text,
                    IpAddress = lIpAdress.Text,
                    AccessToken = textBox1.Text
                });


                // alldata = parameters + productsa + pnew + p2 + dataheadersa;
                RequestSendJson = json;

                var client = new RestClient();
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json;charset=utf-8");
                request.AddStringBody(json, DataFormat.Json);
                RestResponse response = client.Execute(request);
                ResponseSendJson = response.Content;

                nbaroot weatherForecast = System.Text.Json.JsonSerializer.Deserialize<nbaroot>(response.Content);


                if (weatherForecast.message == "Successful operation")
                {
                    if (MessageVisible)
                    {
                        FormHelpers.Alert(CommonData.SUCCESS_SALES, MessageType.Success);
                        //ReadyMessages.SUCCESS_SALES_MESSAGE();
                    }

                    sdocumentid = $"{weatherForecast.data.short_document_id}";
                    YekunMebleg = salesData.Total.ToString();

                    fissayi = $"{weatherForecast.data.document_number}";

                    gunfissayi = $"{weatherForecast.data.shift_document_number}".ToString();

                    textBox3.Text = weatherForecast.data.document_id;
                    clear();
                    textEdit1.Text = DbProsedures.GET_SalesProcessNo();
                    textEdit11.Text = DbProsedures.GET_TotalSalesCount();

                    Log($"Pos satışı uğurla edildi. Qəbz №: {weatherForecast.data.document_number}");

                    DbProsedures.InsertPosSales(new PosSales
                    {
                        posNomre = weatherForecast.data.document_number.ToString(),
                        longFiskalId = weatherForecast.data.document_id,
                        proccessNo = textEdit1.Text,
                        cash = salesData.Cash,
                        card = salesData.Card,
                        total = salesData.Total,
                        json = json,
                        shortFiskalId = weatherForecast.data.short_document_id,
                        rrn = rrnCode
                    });


                    PrintDocument pd = new PrintDocument();
                    pd.DefaultPageSettings = new PageSettings
                    {
                        PaperSize = new PrinterSettings().DefaultPageSettings.PaperSize
                    };

                    pd.PrintPage += new PrintPageEventHandler(nba_saleprint);

                    pagesCount = 1;


                    PrintDialog PrintDialog1 = new PrintDialog
                    {
                        Document = pd
                    };

                    pd.Print();
                    CalculationDelete();
                }
                else
                {
                    FormHelpers.Log($"Pos satış xətası - {weatherForecast.message}\nJson: {RequestSendJson}");
                    if (salesData.PayType == PayType.OtherPay)
                    {
                        goto Finish;
                    }
                    if (salesData.Card > 0)
                    {
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

                        var body2 = "{ \"type\":\"void\",\"amount\":\"" + salesData.Card.ToString().Replace(",", "") + "\",\"rrn\":\"" + rrnCode + "\", \"dontredirecttosale\":false}";
                        requestsend.AddStringBody(body2, DataFormat.Json);

                        RestResponse response2 = client2.Execute(requestsend);

                        nbarootbank weatherForecastbank = JsonSerializer.Deserialize<nbarootbank>(response2.Content);

                        bankid = $"{weatherForecastbank.trnid}";

                    bankstart2:
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
                            bankdizi.Clear();
                            bankdizic.Clear();

                            if (weatherForecastbankdetail.errorreceipt != null)
                            {
                                foreach (var item in weatherForecastbankdetail.errorreceipt)
                                {
                                    bankdizi.Add(item.line);
                                }
                            }
                            ReadyMessages.ERROR_BANK_MESSAGE(weatherForecastbankdetail.responsecodeText);


                            #region [..XƏZİNƏDAR QƏBZİ..]


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

                            ReadyMessages.ERROR_SALES_MESSAGE(weatherForecastbankdetail.responsecodeText);
                            FormHelpers.Log($"Pos satışı xətası: {weatherForecastbankdetail.responsecodeText}");
                            return;
                        }
                        else
                        {
                            goto bankstart2;
                        }


                    }

                Finish:
                    ReadyMessages.ERROR_SALES_MESSAGE(weatherForecast.message);
                    FormHelpers.Log($"Pos satışı xətası: {weatherForecast.message}");
                    return;
                }
            }
            catch (Exception ex)
            {
                FormHelpers.Log($"Satış zamanı göndərilən json:\n {RequestSendJson}");

                throw ex;
                //ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
            }
            finally
            {
                FormHelpers.OperationLog(new OperationLogs
                {
                    OperationType = OperationType.PosSales,
                    OperationId = 0,
                    Message = "Kassa satış",
                    RequestCode = RequestSendJson,
                    ResponseCode = ResponseSendJson,
                });


                //FormHelpers.OperationLog(new OperationLogs
                //{
                //    OperationType = OperationType.PosSales,
                //    OperationId = 0,
                //    Message = "Bank satış",
                //    RequestCode = RequestSendJson,
                //    ResponseCode = ResponseSendJson,
                //});

                Cursor.Current = Cursors.Default;
            }

        }

        private void paysales(decimal cash_, decimal card_, decimal umumi_mebleg_)
        {
            string p = "";
            string productsa = "\"items\":[";
            string dataheadersa = " ";
            string alldata = "";

            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                SqlConnection conn2 = new SqlConnection();
                SqlCommand cmd2 = new SqlCommand();
                conn2.ConnectionString = Properties.Settings.Default.SqlCon;
                conn2.Open();
                string query2 = DbHelpers.GetHeaderDataQuery;

                cmd2.Connection = conn2;
                cmd2.CommandText = query2;

                SqlDataReader dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {
                    string cash = dr2["cashPayment"].ToString();
                    string card = dr2["cardPayment"].ToString();
                    string bonus = dr2["bonusPayment"].ToString();
                    string client = dr2["clientName"].ToString();
                    string tot = dr2["tot"].ToString();

                    dataheadersa = "{\"sum\"  : " + tot.Replace(",", ".") + "," +
                        '\u0022' + "cashier" + '\u0022' + ": " + '\u0022' + "Kassir" + '\u0022' + "," +
                        " \"cashSum\" : " + cash.Replace(",", ".") + "," +
                        "\"cashlessSum\":" + card.Replace(",", ".") + "," +
                         " \"cardSum\" : 0.0," +
                        " \"prepaymentSum\" : 0.0," +
                         "\"bonusSum\" : 0.0," +
                        " \"creditSum\" : 0.0," +
                         " \"vatSum\" : " + tot.Replace(",", ".") + "," +
                         " \"vatFreeSum\" : 0.0," +
                          " \"vat\" : 2," +
                        "  \"paidSum\"  : " + tot.Replace(",", ".") + "," +
                        " \"restSum\" : 0,";
                }




                SqlConnection conn = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                conn.ConnectionString = Properties.Settings.Default.SqlCon;
                conn.Open();
                string query = $"select name,code,salePrice,quantity,vatType as V2,case  vatType when 1 then '18' when 3 then '0' when 4 then '2' when 5 then '8' else 0 end as vatType,quantityType,salePrice*quantity as ssum, salePrice*quantity*((case  vatType when 1 then '18' when 3 then '0' when 4 then '2' when 5 then '8' else 0 end)/100 ) as vatsums from  dbo.item WHERE user_id = {Properties.Settings.Default.UserID};";

                cmd.Connection = conn;
                cmd.CommandText = query;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr["name"].ToString();
                    string code = dr["code"].ToString();
                    string sprice = dr["salePrice"].ToString();
                    string qty = dr["quantity"].ToString();
                    string vat = dr["vatType"].ToString();
                    string vat2 = dr["v2"].ToString();
                    string qunit = dr["quantityType"].ToString();
                    string ssum = dr["ssum"].ToString();
                    string vsum = dr["vatsums"].ToString();

                    p = p + "{\"name\":\"" + name + "\",\"code\":\"" + code + "\",\"quantity\":" + qty.Replace(",", ".") + ",\"salePrice\":" + sprice.Replace(",", ".") + ",\"purchasePrice\":" + sprice.Replace(",", ".") + ",\"sum\":" + ssum.Replace(",", ".") + ",\"vatSum\":" + vsum.Replace(",", ".") + ",\"vatType\":" + vat2 + ",\"unitType\":" + qunit + ",\"codeType\":1" + "},";
                }
                string pnew = p.Substring(0, p.Length - 1);

                string footernews = " ] }";

                alldata = dataheadersa + productsa + pnew + footernews;

                string url = $"{lIpAdress.Text}/api/sale/create";

                var clients = new RestClient();

                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Authorization", textBox1.Text);
                var body = alldata;

                request.AddParameter("text/plain", body, ParameterType.RequestBody);

                RestResponse response = clients.Execute(request);
                var data = response.Content.ToString();

                WeatherForecastDataPay weatherForecast = System.Text.Json.JsonSerializer.Deserialize<WeatherForecastDataPay>(data);

                if (MessageVisible)
                {
                    ReadyMessages.SUCCESS_SALES_MESSAGE();
                }

                Log($"Pos satışı uğurla edildi. Qəbz №: {weatherForecast.document_number}");

                string a = $"{weatherForecast.long_id}";
                /* textEdit3.Text = a; */
                textBox3.Text = a;
                clear();
                textEdit1.Text = DbProsedures.GET_SalesProcessNo();
                textEdit11.Text = DbProsedures.GET_TotalSalesCount();

                //     st_.azmart_sale_insert_(weatherForecast.documentId, weatherForecast.documentNumber.ToString(),
                //weatherForecast.shortDocumentId, response.Content.ToString(), p_id, textEdit1.Text.ToString(), cash_, card_, umumi_mebleg_);

                DbProsedures.InsertPosSales(new PosSales
                {
                    posNomre = weatherForecast.documentNumber.ToString(),
                    longFiskalId = weatherForecast.documentId,
                    proccessNo = textEdit1.Text,
                    cash = cash_,
                    card = card_,
                    total = umumi_mebleg_,
                    json = alldata,
                    shortFiskalId = weatherForecast.shortDocumentId
                });

            }

            catch (Exception e)
            {
                XtraMessageBox.Show("Xəta!\n" + e);
                FormHelpers.Log($"Pos satışı xətası\n Xəta mesajı: {e.Message}");
            }
        }

        private void xprintersales(decimal cash_, decimal card_, decimal umumi_mebleg_, decimal incomingSum = default)
        {
            try
            {
                SqlConnection conn2 = new SqlConnection(Properties.Settings.Default.SqlCon);
                SqlCommand cmd2 = new SqlCommand();
                conn2.Open();
                string query2 = DbHelpers.GetHeaderDataQuery;

                cmd2.Connection = conn2;
                cmd2.CommandText = query2;

                SqlDataReader dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {
                    string cash = dr2["cashPayment"].ToString();
                    string card = dr2["cardPayment"].ToString();
                    string bonus = dr2["bonusPayment"].ToString();
                    string client = dr2["clientName"].ToString();
                    string paidPayment = dr2["paidPayment"].ToString();
                    string tot = dr2["tot"].ToString();
                }


                SqlConnection conn = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                conn.ConnectionString = Properties.Settings.Default.SqlCon;
                conn.Open();
                string query = $"select name,code,salePrice,quantity,case  vatType when 1 then '18' when 3 then '0' when 4 then '2' when 5 then '8' else 0 end as vatType,quantityType,salePrice*quantity as ssum from  dbo.item WHERE user_id = {Properties.Settings.Default.UserID};";

                cmd.Connection = conn;
                cmd.CommandText = query;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr["name"].ToString();
                    string code = dr["code"].ToString();
                    string sprice = dr["salePrice"].ToString();
                    string qty = dr["quantity"].ToString();
                    string vat = dr["vatType"].ToString();
                    string qunit = dr["quantityType"].ToString();
                    string ssum = dr["ssum"].ToString();
                }

                if (MessageVisible)
                {
                    FormHelpers.Alert(CommonData.SUCCESS_SALES, MessageType.Success);
                }

                Random rastgele = new Random();
                int sayi2 = rastgele.Next(1001, 999999999);
                int sayi4 = rastgele.Next(856, 45465);
                int sayi3 = rastgele.Next(856321, 7856321);
                string a2 = sayi2.ToString();

                DbProsedures.InsertPosSales(new PosSales
                {
                    posNomre = Convert.ToString(sayi2 + sayi4 - sayi3),
                    longFiskalId = "994" + a2,
                    proccessNo = textEdit1.Text,
                    cash = cash_,
                    card = card_,
                    total = umumi_mebleg_,
                    json = null,
                    shortFiskalId = null
                });

                clear();
                textEdit11.Text = DbProsedures.GET_TotalSalesCount();
                CalculationDelete();
                PrintTest();
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
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
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);

            pagesCount = 1;



            PrintDialog PrintDialog1 = new PrintDialog();


            PrintDialog1.Document = pd;

            // DialogResult result = PrintDialog1.ShowDialog();
            pd.Print();
        }

        void pd_PrintPage(object sender, PrintPageEventArgs e)
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
            string _incomingSum = "";
            string kart = "";

            int offset = 170;
            int offset2 = 10;
            int m = 0;
            int n = 20;

            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlConnection conn2 = new SqlConnection();
            SqlCommand cmd2 = new SqlCommand();
            conn2.ConnectionString = Properties.Settings.Default.SqlCon;
            conn2.Open();
            string query2 = "select round(cashPayment,2) as cashPayment ,round(cardPayment,2) as cardPayment,paidPayment,bonusPayment ,clientName ,header_id,cashPayment +cardPayment as tot,(SELECT TOP 1 [COMPANY_NAME] FROM [COMPANY].[COMPANY]) AS sirket,(SELECT TOP 1   [SIRKET_VOEN]    FROM [COMPANY].[COMPANY]) as voen from  dbo.header;";

            cmd2.Connection = conn2;
            cmd2.CommandText = query2;

            SqlDataReader dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {


                string cash = dr2["cashPayment"].ToString();
                string card = dr2["cardPayment"].ToString();
                string bonus = dr2["bonusPayment"].ToString();
                string client = dr2["clientName"].ToString();
                string tot = dr2["tot"].ToString();
                string sirket = dr2["sirket"].ToString();
                string incomingSum = dr2["paidPayment"].ToString();
                string voen = dr2["voen"].ToString();



                e.Graphics.DrawString("  " + sirket, company, Brushes.Black, new Point(90, offset2));
                e.Graphics.DrawString("VÖEN :" + voen, font3, Brushes.Black, new Point(85, offset2 + 15));

                e.Graphics.DrawString("Satış Çeki", font4, Brushes.Black, new Point(95, offset2 + 40));
                e.Graphics.DrawString("Çek nömrəsi No:" + textEdit1.Text, font2, Brushes.Black, new Point(70, offset2 + 55));
                e.Graphics.DrawString("Kassir: " + tUsername.Text, font, Brushes.Black, new Point(5, offset2 + 80));
                e.Graphics.DrawString("Tarix: " + DateTime.Now.ToString("dd.MM.yyyy"), font, Brushes.Black, new Point(190, offset2 + 80));
                e.Graphics.DrawString("Saat: " + DateTime.Now.ToString("HH:mm"), font, Brushes.Black, new Point(190, offset2 + 95));

                e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 110));
                e.Graphics.DrawString("Malın adı", f8, Brushes.Black, 5, offset2 + 120);
                e.Graphics.DrawString("Miqdar", f8, Brushes.Black, 130, offset2 + 120);
                e.Graphics.DrawString("Qiymət", f8, Brushes.Black, 190, offset2 + 120);
                e.Graphics.DrawString("Toplam", f8, Brushes.Black, 240, offset2 + 120);
                e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset2 + 130));

                tutar = tot;
                kart = card;
                nagd = cash;
                _incomingSum = incomingSum;
            }

            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.SqlCon;
            conn.Open();
            string query = $"select name,code,ROUND(salePrice,2) as salePrice,ROUND(quantity,2) AS quantity,case  vatType when 1 then '18' when 3 then '0' when 4 then '2' when 5 then '8' else 0 end as vatType,round(quantityType,2) as quantityType,ROUND(salePrice*quantity,2) as ssum from  dbo.item WHERE user_id = {Properties.Settings.Default.UserID};";

            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string name = dr["name"].ToString();
                string code = dr["code"].ToString();
                string sprice = dr["salePrice"].ToString();
                string qty = dr["quantity"].ToString();
                string vat = dr["vatType"].ToString();
                string qunit = dr["quantityType"].ToString();
                string ssum = dr["ssum"].ToString();

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
            double qalıq = Convert.ToDouble(_incomingSum) - Convert.ToDouble(nagd);
            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset + m));
            e.Graphics.DrawString("YEKUN MƏBLƏĞ:", f9, Brushes.Black, 5, offset + m + 20);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(tutar)), f9, Brushes.Black, new Point(240, offset + m + 20));
            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset + m + 30));
            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset + m + 50));
            e.Graphics.DrawString("Ödəniş növü", f8, Brushes.Black, 5, offset + m + 65);
            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 5, offset + m + 80);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(kart)), f8, Brushes.Black, 240, offset + m + 80);
            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 5, offset + m + 90);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(nagd)), f8, Brushes.Black, 240, offset + m + 90);
            e.Graphics.DrawString("Ödənilib nağd:", f8, Brushes.Black, 5, offset + m + 100);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(_incomingSum)), f8, Brushes.Black, 240, offset + m + 100);
            e.Graphics.DrawString("Qalıq qaytarılıb nağd:", f8, Brushes.Black, 5, offset + m + 110);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(qalıq)), f8, Brushes.Black, 240, offset + m + 110);

            CalculationDelete();
        }

        void nka_zhesabat(object sender, PrintPageEventArgs e)
        {
            Font font = new System.Drawing.Font("Times New Roman", 7f);
            Font font2 = new System.Drawing.Font("Times New Roman", 8.75f, FontStyle.Bold);
            Font font40 = new System.Drawing.Font("Times New Roman", 10f, FontStyle.Bold);
            Font font4 = new System.Drawing.Font("Times New Roman", 8f, FontStyle.Bold);
            Font font3 = new System.Drawing.Font("Times New Roman", 8.75f);
            Font f8 = new System.Drawing.Font("Times New Roman", 7f);
            Font f9 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Bold);
            Font fonta = new System.Drawing.Font("Times New Roman", 8.5f);

            int offset2 = 10;
            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            var qrcodeimg = barcode.Draw("https://www.e-kassa.gov.az/", 0, scale: 3);


            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            Rectangle rect2 = new Rectangle(70, 10, 100, 122);

            double moneybackvatsa = Math.Round(Convert.ToDouble(moneyBackSum) * 18 / 118, 2);
            double saleSumvatsa = Math.Round(Convert.ToDouble(saleSum) * 18 / 118);

            double dovriyyesa = Math.Round(Convert.ToDouble(saleSum), 2) - Math.Round(Convert.ToDouble(moneyBackSum), 2);

            double dovriyyesavergi = Math.Round(dovriyyesa * 18 / 118, 2);

            string ustbaslik = "TS Adı :" + obyetname + "\r\n" + "TS Ünvanı :" + obyektadres + "\r\n" + "\r\n" + "VÖ Adı :" + customer + "\r\n" + "VÖEN :" + customervoen + "\r\n" + "Obyektin kodu :" + obyektkod;


            e.Graphics.DrawString(ustbaslik, fonta, Brushes.Black, new RectangleF(50F, 10F, 200F, 90F), sf);



            e.Graphics.DrawString("Z-hesabat (Növbənin bağlanması)", font40, Brushes.Black, new Point(60, offset2 + 105));
            e.Graphics.DrawString("Çek nömrəsi No: 1", font2, Brushes.Black, new Point(90, offset2 + 120));
            e.Graphics.DrawString("Tarix: " + DateTime.Now.ToString("dd.MM.yyyy"), font, Brushes.Black, new Point(190, offset2 + 135));
            e.Graphics.DrawString("Saat: " + DateTime.Now.ToString("HH:mm"), font, Brushes.Black, new Point(190, offset2 + 150));

            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 160));
            e.Graphics.DrawString("Növbənin açılma vaxtı:", f8, Brushes.Black, 5, offset2 + 175);

            e.Graphics.DrawString(nacilma, f8, Brushes.Black, 200, offset2 + 175);

            e.Graphics.DrawString("Hesabatın alınma vaxtı:", f8, Brushes.Black, 5, offset2 + 190);

            e.Graphics.DrawString(nhtarix, f8, Brushes.Black, 200, offset2 + 190);
            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 200));
            e.Graphics.DrawString("Kassa Çekləri", font4, Brushes.Black, new Point(90, offset2 + 210));

            e.Graphics.DrawString("Birinci kassa çekinin No:", f8, Brushes.Black, 5, offset2 + 220);

            e.Graphics.DrawString(firstDocNumber, f8, Brushes.Black, 240, offset2 + 220);

            e.Graphics.DrawString("Sonuncu kassa çekinin No:", f8, Brushes.Black, 5, offset2 + 230);

            e.Graphics.DrawString(lastDocNumber, f8, Brushes.Black, 240, offset2 + 230);


            e.Graphics.DrawString("Dövriyyə:", f8, Brushes.Black, 5, offset2 + 250);

            e.Graphics.DrawString(dovriyyesa.ToString(), f8, Brushes.Black, 240, offset2 + 250);

            e.Graphics.DrawString("Dövriyyə üzrə vergi:", f8, Brushes.Black, 5, offset2 + 260);

            e.Graphics.DrawString(dovriyyesavergi.ToString(), f8, Brushes.Black, 240, offset2 + 260);


            e.Graphics.DrawString("Satış", font4, Brushes.Black, new Point(90, offset2 + 270));

            e.Graphics.DrawString("Satış çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 280);

            e.Graphics.DrawString(saleCount, f8, Brushes.Black, 240, offset2 + 280);

            e.Graphics.DrawString("Satış məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 290);

            e.Graphics.DrawString(saleSum, f8, Brushes.Black, 240, offset2 + 290);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 300);

            e.Graphics.DrawString(saleCashSum, f8, Brushes.Black, 240, offset2 + 300);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 310);

            e.Graphics.DrawString(saleCashlessSum.ToString(), f8, Brushes.Black, 240, offset2 + 310);


            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 320);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 320);
            e.Graphics.DrawString("Avans (beh) məbləği :", f8, Brushes.Black, 10, offset2 + 330);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 330);

            e.Graphics.DrawString("Nisyə (kredit) alınmış məbləği:", f8, Brushes.Black, 10, offset2 + 340);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 340);

            e.Graphics.DrawString("Vergi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 350);

            e.Graphics.DrawString(dovriyyesavergi.ToString(), f8, Brushes.Black, 240, offset2 + 350);



            e.Graphics.DrawString("Geri Qaytarma", font4, Brushes.Black, new Point(90, offset2 + 370));

            e.Graphics.DrawString("Geri qaytarma çeklərinin sayı:", f8, Brushes.Black, 5, offset2 + 380);

            e.Graphics.DrawString(moneyBackCount, f8, Brushes.Black, 240, offset2 + 380);

            e.Graphics.DrawString("Geri qaytarma məbləğinin cəmi:", f8, Brushes.Black, 5, offset2 + 390);

            e.Graphics.DrawString(moneyBackSum, f8, Brushes.Black, 240, offset2 + 390);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 400);

            e.Graphics.DrawString(moneyBackCashSum, f8, Brushes.Black, 240, offset2 + 400);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 410);

            e.Graphics.DrawString(moneyBackCashlessSum, f8, Brushes.Black, 240, offset2 + 410);


            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 420);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 420);
            e.Graphics.DrawString("Avans (beh) məbləği :", f8, Brushes.Black, 10, offset2 + 430);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 430);

            e.Graphics.DrawString("Nisyə (kredit) alınmış məbləği:", f8, Brushes.Black, 10, offset2 + 440);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 440);

            e.Graphics.DrawString("Vergi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 450);

            e.Graphics.DrawString(moneybackvatsa.ToString(), f8, Brushes.Black, 240, offset2 + 450);



            e.Graphics.DrawString("Ləğv Etmə ", font4, Brushes.Black, new Point(90, offset2 + 470));

            e.Graphics.DrawString("Ləğv Etmə çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 480);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 480);

            e.Graphics.DrawString("Ləğv Etmə məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 490);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 490);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 500);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 500);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 510);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 510);


            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 520);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 520);
            e.Graphics.DrawString("Avans (beh) məbləği :", f8, Brushes.Black, 10, offset2 + 530);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 530);

            e.Graphics.DrawString("Nisyə (kredit) alınmış məbləği:", f8, Brushes.Black, 10, offset2 + 540);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 540);

            e.Graphics.DrawString("Vergi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 550);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 550);


            e.Graphics.DrawString("Mədaxil ", font4, Brushes.Black, new Point(90, offset2 + 560));

            e.Graphics.DrawString("Mədaxil çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 570);

            e.Graphics.DrawString(depositCount, f8, Brushes.Black, 240, offset2 + 570);

            e.Graphics.DrawString("Mədaxil məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 580);

            e.Graphics.DrawString(depositSum, f8, Brushes.Black, 240, offset2 + 580);


            e.Graphics.DrawString("Məxaric ", font4, Brushes.Black, new Point(90, offset2 + 590));

            e.Graphics.DrawString("Məxaric çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 600);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 600);

            e.Graphics.DrawString("Məxaric məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 610);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 610);



            e.Graphics.DrawString("Korreksiya ", font4, Brushes.Black, new Point(90, offset2 + 620));

            e.Graphics.DrawString("Korreksiya çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 630);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 630);

            e.Graphics.DrawString("Korreksiya məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 640);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 640);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 650);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 650);

            e.Graphics.DrawString("Nağdsiz:", f8, Brushes.Black, 15, offset2 + 660);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 660);

            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 670);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 670);


            e.Graphics.DrawString("Vergi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 6780);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 680);



            e.Graphics.DrawString("Avans (beh) Ödənişi ", font4, Brushes.Black, new Point(90, offset2 + 690));

            e.Graphics.DrawString("Avans (beh) Ödənişi çeklərin sayı:", f8, Brushes.Black, 5, offset2 + 700);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 700);

            e.Graphics.DrawString("Avans (beh) ödənişi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 710);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 710);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 720);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 720);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 730);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 730);

            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 740);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 740);


            e.Graphics.DrawString("Vergi məbləğinin cəmi:", f8, Brushes.Black, 5, offset2 + 750);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 750);



            e.Graphics.DrawString("Kredit (nisyə) ödənişi ", font4, Brushes.Black, new Point(90, offset2 + 760));

            e.Graphics.DrawString("Kredit (nisyə) ödənişi çeklərin sayı:", f8, Brushes.Black, 5, offset2 + 770);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 770);

            e.Graphics.DrawString("Kredit (nisyə) ödənişi məbləğinin cəmi:", f8, Brushes.Black, 5, offset2 + 780);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 780);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 790);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 790);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 800);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 800);

            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 810);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 820);


            e.Graphics.DrawString("Vergi məbləğinin cəmi:", f8, Brushes.Black, 5, offset2 + 830);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 830);


            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset2 + 837));

            e.Graphics.DrawString("DVX göndəriləyən çeklərin sayı: 0", font, Brushes.Black, 5, offset2 + 850);
            e.Graphics.DrawString("NKA-nın modeli:" + TerminalTokenData.NkaModel, font, Brushes.Black, 5, offset2 + 860);
            e.Graphics.DrawString("NKA-nın zavod nömrəsi:" + TerminalTokenData.NkaSerialNumber, font, Brushes.Black, 5, offset2 + 870);
            e.Graphics.DrawString("NMQ-nın qeydiyyat nömrəsi:" + TerminalTokenData.NMQRegistrationNumber, font, Brushes.Black, 5, offset2 + 880);



            //e.Graphics.DrawString("www.e-kassa.gov.az", font, Brushes.Black, 60, offset2 + 840);

            //e.Graphics.DrawImage(qrcodeimg, 90, offset2 + 860, width: 50, height: 50);

        }

        void nka_xhesabat(object sender, PrintPageEventArgs e)
        {
            Font font = new System.Drawing.Font("Times New Roman", 7f);
            Font font2 = new System.Drawing.Font("Times New Roman", 8.75f, FontStyle.Bold);
            Font font40 = new System.Drawing.Font("Times New Roman", 10f, FontStyle.Bold);
            Font font4 = new System.Drawing.Font("Times New Roman", 8f, FontStyle.Bold);
            Font font3 = new System.Drawing.Font("Times New Roman", 8.75f);
            Font f8 = new System.Drawing.Font("Times New Roman", 7f);
            Font f9 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Bold);
            Font fonta = new System.Drawing.Font("Times New Roman", 8.5f);

            int offset2 = 10;
            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            var qrcodeimg = barcode.Draw("https://www.e-kassa.gov.az/", 0, scale: 3);


            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            Rectangle rect2 = new Rectangle(70, 10, 100, 122);

            string ustbaslik = "TS Adı :" + obyetname + "\r\n" + "TS Ünvanı :" + obyektadres + "\r\n" + "\r\n" + "VÖ Adı :" + customer + "\r\n" + "VÖEN :" + customervoen + "\r\n" + "Obyektin kodu :" + obyektkod;

            double moneybackvatsa = Math.Round(Convert.ToDouble(moneyBackSum) * 18 / 118, 2);
            double saleSumvatsa = Math.Round(Convert.ToDouble(saleSum) * 18 / 118);

            double dovriyyesa = Math.Round(Convert.ToDouble(saleSum), 2) - Math.Round(Convert.ToDouble(moneyBackSum), 2);

            double dovriyyesavergi = Math.Round(dovriyyesa * 18 / 118, 2);
            e.Graphics.DrawString(ustbaslik, fonta, Brushes.Black, new RectangleF(50F, 10F, 200F, 90F), sf);

            TimeZoneInfo utc4 = TimeZoneInfo.CreateCustomTimeZone("UTC+4", TimeSpan.FromHours(4), "UTC+4", "UTC+4");

            DateTime utcTime = DateTime.Parse(nacilma);
            //DateTime openShiftTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, utc4);


            DateTime utcTime2 = DateTime.Parse(nhtarix);
            //DateTime createReportTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime2, utc4);


            e.Graphics.DrawString("X-hesabat", font40, Brushes.Black, new Point(110, offset2 + 105));
            e.Graphics.DrawString("Çek nömrəsi No:" + rno, font2, Brushes.Black, new Point(90, offset2 + 120));
            e.Graphics.DrawString("Tarix: " + DateTime.Now.ToString("dd.MM.yyyy"), font, Brushes.Black, new Point(190, offset2 + 135));
            e.Graphics.DrawString("Saat: " + DateTime.Now.ToString("HH:mm"), font, Brushes.Black, new Point(190, offset2 + 150));

            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 160));
            e.Graphics.DrawString("Növbənin açılma vaxtı:", f8, Brushes.Black, 5, offset2 + 175);

            e.Graphics.DrawString(utcTime.ToString("dd.MM.yyyy HH:mm:ss"), f8, Brushes.Black, 200, offset2 + 175);

            e.Graphics.DrawString("Hesabatın alınma vaxtı:", f8, Brushes.Black, 5, offset2 + 190);

            e.Graphics.DrawString(utcTime2.ToString("dd.MM.yyyy HH:mm:ss"), f8, Brushes.Black, 200, offset2 + 190);
            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 200));
            e.Graphics.DrawString("Kassa Çekləri", font4, Brushes.Black, new Point(90, offset2 + 210));

            e.Graphics.DrawString("Birinci kassa çekinin No:", f8, Brushes.Black, 5, offset2 + 220);

            e.Graphics.DrawString(firstDocNumber, f8, Brushes.Black, 240, offset2 + 220);

            e.Graphics.DrawString("Sonuncu kassa çekinin No:", f8, Brushes.Black, 5, offset2 + 230);

            e.Graphics.DrawString(lastDocNumber, f8, Brushes.Black, 240, offset2 + 230);


            e.Graphics.DrawString("Dövriyyə:", f8, Brushes.Black, 5, offset2 + 250);

            e.Graphics.DrawString(dovriyyesa.ToString(), f8, Brushes.Black, 240, offset2 + 250);

            e.Graphics.DrawString("Dövriyyə üzrə vergi:", f8, Brushes.Black, 5, offset2 + 260);

            e.Graphics.DrawString(dovriyyesavergi.ToString(), f8, Brushes.Black, 240, offset2 + 260);


            e.Graphics.DrawString("Satış", font4, Brushes.Black, new Point(90, offset2 + 270));

            e.Graphics.DrawString("Satış çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 280);

            e.Graphics.DrawString(saleCount, f8, Brushes.Black, 240, offset2 + 280);

            e.Graphics.DrawString("Satış məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 290);

            e.Graphics.DrawString(saleSum, f8, Brushes.Black, 240, offset2 + 290);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 300);

            e.Graphics.DrawString(saleCashSum, f8, Brushes.Black, 240, offset2 + 300);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 310);

            e.Graphics.DrawString(saleCashlessSum.ToString(), f8, Brushes.Black, 240, offset2 + 310);


            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 320);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 320);
            e.Graphics.DrawString("Avans (beh) məbləği :", f8, Brushes.Black, 10, offset2 + 330);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 330);

            e.Graphics.DrawString("Nisyə (kredit) alınmış məbləği:", f8, Brushes.Black, 10, offset2 + 340);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 340);

            e.Graphics.DrawString("Vergi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 350);

            e.Graphics.DrawString(dovriyyesavergi.ToString(), f8, Brushes.Black, 240, offset2 + 350);



            e.Graphics.DrawString("Geri Qaytarma", font4, Brushes.Black, new Point(90, offset2 + 370));

            e.Graphics.DrawString("Geri qaytarma çeklərinin sayı:", f8, Brushes.Black, 5, offset2 + 380);

            e.Graphics.DrawString(moneyBackCount, f8, Brushes.Black, 240, offset2 + 380);

            e.Graphics.DrawString("Geri qaytarma məbləğinin cəmi:", f8, Brushes.Black, 5, offset2 + 390);

            e.Graphics.DrawString(moneyBackSum, f8, Brushes.Black, 240, offset2 + 390);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 400);

            e.Graphics.DrawString(moneyBackCashSum, f8, Brushes.Black, 240, offset2 + 400);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 410);

            e.Graphics.DrawString(moneyBackCashlessSum, f8, Brushes.Black, 240, offset2 + 410);


            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 420);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 420);
            e.Graphics.DrawString("Avans (beh) məbləği :", f8, Brushes.Black, 10, offset2 + 430);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 430);

            e.Graphics.DrawString("Nisyə (kredit) alınmış məbləği:", f8, Brushes.Black, 10, offset2 + 440);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 440);

            e.Graphics.DrawString("Vergi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 450);

            e.Graphics.DrawString(moneybackvatsa.ToString(), f8, Brushes.Black, 240, offset2 + 450);



            e.Graphics.DrawString("Ləğv Etmə ", font4, Brushes.Black, new Point(90, offset2 + 470));

            e.Graphics.DrawString("Ləğv Etmə çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 480);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 480);

            e.Graphics.DrawString("Ləğv Etmə məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 490);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 490);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 500);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 500);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 510);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 510);


            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 520);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 520);
            e.Graphics.DrawString("Avans (beh) məbləği :", f8, Brushes.Black, 10, offset2 + 530);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 530);

            e.Graphics.DrawString("Nisyə (kredit) alınmış məbləği:", f8, Brushes.Black, 10, offset2 + 540);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 540);

            e.Graphics.DrawString("Vergi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 550);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 550);


            e.Graphics.DrawString("Mədaxil ", font4, Brushes.Black, new Point(90, offset2 + 560));

            e.Graphics.DrawString("Mədaxil çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 570);

            e.Graphics.DrawString(depositCount, f8, Brushes.Black, 240, offset2 + 570);

            e.Graphics.DrawString("Mədaxil məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 580);

            e.Graphics.DrawString(depositSum, f8, Brushes.Black, 240, offset2 + 580);


            e.Graphics.DrawString("Məxaric ", font4, Brushes.Black, new Point(90, offset2 + 590));

            e.Graphics.DrawString("Məxaric çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 600);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 600);

            e.Graphics.DrawString("Məxaric məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 610);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 610);



            e.Graphics.DrawString("Korreksiya ", font4, Brushes.Black, new Point(90, offset2 + 620));

            e.Graphics.DrawString("Korreksiya çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 630);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 630);

            e.Graphics.DrawString("Korreksiya məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 640);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 640);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 650);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 650);

            e.Graphics.DrawString("Nağdsiz:", f8, Brushes.Black, 15, offset2 + 660);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 660);

            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 670);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 670);


            e.Graphics.DrawString("Vergi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 6780);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 680);



            e.Graphics.DrawString("Avans (beh) Ödənişi ", font4, Brushes.Black, new Point(90, offset2 + 690));

            e.Graphics.DrawString("Avans (beh) Ödənişi çeklərin sayı:", f8, Brushes.Black, 5, offset2 + 700);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 700);

            e.Graphics.DrawString("Avans (beh) ödənişi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 710);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 710);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 720);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 720);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 730);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 730);

            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 740);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 740);


            e.Graphics.DrawString("Vergi məbləğinin cəmi:", f8, Brushes.Black, 5, offset2 + 750);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 750);



            e.Graphics.DrawString("Kredit (nisyə) ödənişi ", font4, Brushes.Black, new Point(90, offset2 + 760));

            e.Graphics.DrawString("Kredit (nisyə) ödənişi çeklərin sayı:", f8, Brushes.Black, 5, offset2 + 770);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 770);

            e.Graphics.DrawString("Kredit (nisyə) ödənişi məbləğinin cəmi:", f8, Brushes.Black, 5, offset2 + 780);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 780);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 790);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 790);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 800);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 800);

            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 810);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 820);


            e.Graphics.DrawString("Vergi məbləğinin cəmi:", f8, Brushes.Black, 5, offset2 + 830);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 830);


            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset2 + 837));

            e.Graphics.DrawString("DVX göndəriləyən çeklərin sayı: 0", font, Brushes.Black, 5, offset2 + 850);
            e.Graphics.DrawString("NKA-nın modeli:" + TerminalTokenData.NkaModel, font, Brushes.Black, 5, offset2 + 860);
            e.Graphics.DrawString("NKA-nın zavod nömrəsi:" + TerminalTokenData.NkaSerialNumber, font, Brushes.Black, 5, offset2 + 870);
            e.Graphics.DrawString("NMQ-nın qeydiyyat nömrəsi:" + TerminalTokenData.NMQRegistrationNumber, font, Brushes.Black, 5, offset2 + 880);


            //   e.Graphics.DrawString("www.e-kassa.gov.az", font, Brushes.Black, 60, offset2 + 840);

            //e.Graphics.DrawImage(qrcodeimg, 90, offset2 + 860, width: 80, height: 80);

        }

        void nka_xhesabataylik(object sender, PrintPageEventArgs e)
        {
            Font font = new System.Drawing.Font("Times New Roman", 7f);
            Font font2 = new System.Drawing.Font("Times New Roman", 8.75f, FontStyle.Bold);
            Font font40 = new System.Drawing.Font("Times New Roman", 10f, FontStyle.Bold);
            Font font4 = new System.Drawing.Font("Times New Roman", 8f, FontStyle.Bold);
            Font font3 = new System.Drawing.Font("Times New Roman", 8.75f);
            Font f8 = new System.Drawing.Font("Times New Roman", 7f);
            Font f9 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Bold);
            Font fonta = new System.Drawing.Font("Times New Roman", 8.5f);
            DateTime dt_Ay_ilkGun = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // Ay ilk günü
            DateTime dt_Ay_sonGun = dt_Ay_ilkGun.AddMonths(1).AddDays(-1);// Ay son günü

            int offset2 = 10;
            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            var qrcodeimg = barcode.Draw("https://www.e-kassa.gov.az/", 0, scale: 3);


            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            Rectangle rect2 = new Rectangle(70, 10, 100, 122);

            string ustbaslik = "TS Adı :" + obyetname + "\r\n" + "TS Unvanı :" + obyektadres + "\r\n" + "\r\n" + "VÖ Adı :" + customer + "\r\n" + "VÖEN :" + customervoen + "\r\n" + "Obyektin kodu :" + obyektkod;

            double moneybackvatsa = Math.Round(Convert.ToDouble(moneyBackSum) * 18 / 118, 2);
            double saleSumvatsa = Math.Round(Convert.ToDouble(saleSum) * 18 / 118);

            double dovriyyesa = Math.Round(Convert.ToDouble(saleSum), 2) - Math.Round(Convert.ToDouble(moneyBackSum), 2);

            double dovriyyesavergi = Math.Round(dovriyyesa * 18 / 118, 2);
            e.Graphics.DrawString(ustbaslik, fonta, Brushes.Black, new RectangleF(50F, 10F, 200F, 90F), sf);



            e.Graphics.DrawString("Z-hesabat dövürlü", font40, Brushes.Black, new Point(75, offset2 + 105));
            e.Graphics.DrawString("Çek nömrəsi No:" + rno, font2, Brushes.Black, new Point(90, offset2 + 120));
            e.Graphics.DrawString("Tarix: " + DateTime.Now.ToString("dd MM yyyy"), font, Brushes.Black, new Point(190, offset2 + 132));
            e.Graphics.DrawString("Saat: " + DateTime.Now.ToString("HH:mm"), font, Brushes.Black, new Point(190, offset2 + 142));

            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 150));
            e.Graphics.DrawString("Hesab Dövrü:", font, Brushes.Black, 5, offset2 + 160);

            e.Graphics.DrawString(dt_Ay_ilkGun.ToString("yyyy-MM-dd") + "/" + dt_Ay_sonGun.ToString("yyyy-MM-dd"), font, Brushes.Black, 200, offset2 + 160);


            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 169));

            e.Graphics.DrawString("Növbənin açılma vaxtı:", font, Brushes.Black, 5, offset2 + 175);

            e.Graphics.DrawString(nacilma, font, Brushes.Black, 200, offset2 + 175);

            e.Graphics.DrawString("Sonuncu Z hesabat alınma vaxtı:", font, Brushes.Black, 5, offset2 + 190);

            e.Graphics.DrawString(ndoc, font, Brushes.Black, 200, offset2 + 190);
            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 200));
            e.Graphics.DrawString("Kassa Çekləri", font4, Brushes.Black, new Point(90, offset2 + 210));

            e.Graphics.DrawString("Birinci kassa çekinin No:", f8, Brushes.Black, 5, offset2 + 220);

            e.Graphics.DrawString(firstDocNumber, f8, Brushes.Black, 240, offset2 + 220);

            e.Graphics.DrawString("Sonuncu kassa çekinin No:", f8, Brushes.Black, 5, offset2 + 230);

            e.Graphics.DrawString(lastDocNumber, f8, Brushes.Black, 240, offset2 + 230);


            e.Graphics.DrawString("Dövriyyə:", f8, Brushes.Black, 5, offset2 + 250);

            e.Graphics.DrawString(dovriyyesa.ToString(), f8, Brushes.Black, 240, offset2 + 250);

            e.Graphics.DrawString("Dövriyyə uzre vergi:", f8, Brushes.Black, 5, offset2 + 260);

            e.Graphics.DrawString(dovriyyesavergi.ToString(), f8, Brushes.Black, 240, offset2 + 260);


            e.Graphics.DrawString("Satış", font4, Brushes.Black, new Point(90, offset2 + 270));

            e.Graphics.DrawString("Satış çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 280);

            e.Graphics.DrawString(saleCount, f8, Brushes.Black, 240, offset2 + 280);

            e.Graphics.DrawString("Satış məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 290);

            e.Graphics.DrawString(saleSum, f8, Brushes.Black, 240, offset2 + 290);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 300);

            e.Graphics.DrawString(saleCashSum, f8, Brushes.Black, 240, offset2 + 300);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 310);

            e.Graphics.DrawString(saleSumvatsa.ToString(), f8, Brushes.Black, 240, offset2 + 310);


            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 320);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 320);
            e.Graphics.DrawString("Avans (beh) məbləği :", f8, Brushes.Black, 10, offset2 + 330);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 330);

            e.Graphics.DrawString("Nisyə (kredit) alınmış məbləği:", f8, Brushes.Black, 10, offset2 + 340);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 340);

            e.Graphics.DrawString("Vergi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 350);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 350);



            e.Graphics.DrawString("Geri Qaytarma", font4, Brushes.Black, new Point(90, offset2 + 370));

            e.Graphics.DrawString("Geri qaytarma çeklərinin Sayı:", f8, Brushes.Black, 5, offset2 + 380);

            e.Graphics.DrawString(moneyBackCount, f8, Brushes.Black, 240, offset2 + 380);

            e.Graphics.DrawString("Geri qaytarma məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 390);

            e.Graphics.DrawString(moneyBackSum, f8, Brushes.Black, 240, offset2 + 390);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 400);

            e.Graphics.DrawString(moneyBackCashSum, f8, Brushes.Black, 240, offset2 + 400);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 410);

            e.Graphics.DrawString(moneyBackCashlessSum, f8, Brushes.Black, 240, offset2 + 410);


            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 420);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 420);
            e.Graphics.DrawString("Avans (beh) məbləği :", f8, Brushes.Black, 10, offset2 + 430);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 430);

            e.Graphics.DrawString("Nisyə (kredit) alınmış məbləği:", f8, Brushes.Black, 10, offset2 + 440);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 440);

            e.Graphics.DrawString("Vergi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 450);

            e.Graphics.DrawString(moneybackvatsa.ToString(), f8, Brushes.Black, 240, offset2 + 450);



            e.Graphics.DrawString("Ləğv Etmə ", font4, Brushes.Black, new Point(90, offset2 + 470));

            e.Graphics.DrawString("Ləğv Etmə çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 480);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 480);

            e.Graphics.DrawString("Ləğv Etmə məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 490);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 490);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 500);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 500);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 510);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 510);


            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 520);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 520);
            e.Graphics.DrawString("Avans (beh) məbləği :", f8, Brushes.Black, 10, offset2 + 530);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 530);

            e.Graphics.DrawString("Nisyə (kredit) alınmış məbləği:", f8, Brushes.Black, 10, offset2 + 540);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 540);

            e.Graphics.DrawString("Vergi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 550);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 550);


            e.Graphics.DrawString("Mədaxil ", font4, Brushes.Black, new Point(90, offset2 + 560));

            e.Graphics.DrawString("Mədaxil çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 570);

            e.Graphics.DrawString(depositCount, f8, Brushes.Black, 240, offset2 + 570);

            e.Graphics.DrawString("Mədaxil məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 580);

            e.Graphics.DrawString(depositSum, f8, Brushes.Black, 240, offset2 + 580);


            e.Graphics.DrawString("Məxaric ", font4, Brushes.Black, new Point(90, offset2 + 590));

            e.Graphics.DrawString("Məxaric çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 600);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 600);

            e.Graphics.DrawString("Məxaric məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 610);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 610);



            e.Graphics.DrawString("Korreksiya ", font4, Brushes.Black, new Point(90, offset2 + 620));

            e.Graphics.DrawString("Korreksiya çeklərin Sayı:", f8, Brushes.Black, 5, offset2 + 630);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 630);

            e.Graphics.DrawString("Korreksiya məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 640);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 640);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 650);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 650);

            e.Graphics.DrawString("Nağdsiz:", f8, Brushes.Black, 15, offset2 + 660);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 660);

            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 670);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 670);


            e.Graphics.DrawString("Vergi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 6780);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 680);



            e.Graphics.DrawString("Avans (beh) Ödənişi ", font4, Brushes.Black, new Point(90, offset2 + 690));

            e.Graphics.DrawString("Avans (beh) Ödənişi çeklərin sayı:", f8, Brushes.Black, 5, offset2 + 700);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 700);

            e.Graphics.DrawString("Avans (beh) ödənişi məbləğinin Cəmi:", f8, Brushes.Black, 5, offset2 + 710);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 710);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 720);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 720);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 730);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 730);

            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 740);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 740);


            e.Graphics.DrawString("Vergi məbləğinin cəmi:", f8, Brushes.Black, 5, offset2 + 750);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 750);



            e.Graphics.DrawString("Kredit (nisyə) ödənişi ", font4, Brushes.Black, new Point(90, offset2 + 760));

            e.Graphics.DrawString("Kredit (nisyə) ödənişi çeklərin sayı:", f8, Brushes.Black, 5, offset2 + 770);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 770);

            e.Graphics.DrawString("Kredit (nisyə) ödənişi məbləğinin cəmi:", f8, Brushes.Black, 5, offset2 + 780);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 780);

            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 15, offset2 + 790);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 790);

            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 15, offset2 + 800);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 800);

            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 15, offset2 + 810);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 820);


            e.Graphics.DrawString("Vergi məbləğinin cəmi:", f8, Brushes.Black, 5, offset2 + 830);

            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset2 + 830);


            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset2 + 840));

            e.Graphics.DrawString("DVX gonderilmeyen ceklerin sayi:0", font, Brushes.Black, 5, offset2 + 850);
            e.Graphics.DrawString("NKA-nın modeli:" + TerminalTokenData.NkaModel, font, Brushes.Black, 5, offset2 + 860);
            e.Graphics.DrawString("NKA-nın zavod nömrəsi:" + TerminalTokenData.NkaSerialNumber, font, Brushes.Black, 5, offset2 + 870);
            e.Graphics.DrawString("NMQ-nın qeydiyyat nömrəsi:" + TerminalTokenData.NMQRegistrationNumber, font, Brushes.Black, 5, offset2 + 880);


            // e.Graphics.DrawString("www.e-kassa.gov.az", font, Brushes.Black, 60, offset2 + 840);

            // e.Graphics.DrawImage(qrcodeimg, 90, offset2 + 860, width: 50, height: 50);
        }

        void nba_saleprint(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Times New Roman", 8f, FontStyle.Regular);
            Font font2 = new Font("Times New Roman", 8.75f, FontStyle.Bold);
            Font font4 = new Font("Times New Roman", 10f, FontStyle.Bold);
            Font f8 = new Font("Calibri", 7.8f, FontStyle.Bold);
            Font f9 = new Font("Times New Roman", 9f, FontStyle.Bold);
            Font fonta = new Font("Times New Roman", 8.5f, FontStyle.Regular);

            string tutar = "";
            string nagd = "";
            string kart = "";

            int offset = 220;
            int offset2 = 10;
            int m = 0;
            int n = 20;

            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            var qrcodeimg = barcode.Draw("https://monitoring.e-kassa.gov.az/#/index?doc=" + sdocumentid, 0, scale: 3);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            Rectangle rect2 = new Rectangle(70, 10, 100, 122);

            string ustbaslik = "TS Adı :" + TerminalTokenData.TsName + "\r\n" + "TS Ünvanı :" + TerminalTokenData.Address + "\r\n" + "\r\n" + "VÖ Adı :" + TerminalTokenData.CompanyName + "\r\n" + "VÖEN - Obyekt kodu :" + TerminalTokenData.ObjectTaxNumber /*+ "\r\n" + "Obyektin kodu :" + obyektkod*/;

            SqlConnection conn2 = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd2 = new SqlCommand();
            conn2.Open();
            string query2 = $@"SELECT 
            ROUND(cashPayment,2) AS cashPayment,
            ROUND(cardPayment,2) AS cardPayment,
            bonusPayment,
            clientName,
            header_id,
            cashPayment + cardPayment AS tot,
            (SELECT TOP 1 [COMPANY_NAME] FROM [COMPANY].[COMPANY]) AS sirket,
            (SELECT TOP 1 [SIRKET_VOEN] FROM [COMPANY].[COMPANY]) AS voen 
            FROM dbo.header
            WHERE userId = {Properties.Settings.Default.UserID}";

            cmd2.Connection = conn2;
            cmd2.CommandText = query2;

            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                string cash = dr2["cashPayment"].ToString();
                string card = dr2["cardPayment"].ToString();
                string tot = dr2["tot"].ToString();

                e.Graphics.DrawString(ustbaslik, fonta, Brushes.Black, new RectangleF(35F, 12F, 220F, 110F), sf);

                e.Graphics.DrawString("Satış Çeki", font4, Brushes.Black, new Point(110, offset2 + 110));
                e.Graphics.DrawString("Çek nömrəsi:" + fissayi, font2, Brushes.Black, new Point(95, offset2 + 125));
                e.Graphics.DrawString("Kassir: " + tUsername.Text, font, Brushes.Black, new Point(5, offset2 + 140));
                e.Graphics.DrawString("Tarix: " + DateTime.Now.ToString("dd.MM.yyyy"), font, Brushes.Black, new Point(190, offset2 + 140));
                e.Graphics.DrawString("Saat: " + DateTime.Now.ToString("HH:mm"), font, Brushes.Black, new Point(190, offset2 + 155));

                e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 170));
                e.Graphics.DrawString("Malın adı", f8, Brushes.Black, 5, offset2 + 180);
                e.Graphics.DrawString("Vh", f8, Brushes.Black, 130, offset2 + 180);
                e.Graphics.DrawString("Miqdar", f8, Brushes.Black, 150, offset2 + 180);
                e.Graphics.DrawString("Qiymət", f8, Brushes.Black, 200, offset2 + 180);
                e.Graphics.DrawString("Toplam", f8, Brushes.Black, 250, offset2 + 180);
                e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset2 + 185));

                tutar = tot;
                kart = card;
                nagd = cash;

            }

            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.SqlCon;
            conn.Open();
            string query = $@"select name,code,ROUND(salePrice,2) as salePrice,ROUND(quantity,2) AS quantity,case  vatType when 1 then '18' when 3 then '0'
            when 4 then '2' when 5 then '8' else 0 end as vatType,round(quantityType,2) as quantityType,ROUND(salePrice*quantity,2) as ssum ,
            case quantityType when 0 then N'əd' when 1 then 'kg' when 2 then 'lt' when 3 then 'mt' when 4 then 'm2' when 5 then 'm3' else '' end as vah from  dbo.item WHERE user_id = {Properties.Settings.Default.UserID};";

            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string name = dr["name"].ToString();
                string code = dr["code"].ToString();
                string sprice = dr["salePrice"].ToString();
                string qty = dr["quantity"].ToString();
                string vat = dr["vatType"].ToString();
                string qunit = dr["quantityType"].ToString();
                string ssum = dr["ssum"].ToString();
                string vah = dr["vah"].ToString();

                e.Graphics.DrawString(name + "\r\n" + "ƏDV" + vat, font, Brushes.Black, new Point(5, offset + m));
                e.Graphics.DrawString(vah, font, Brushes.Black, new Point(130, offset + n));
                e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(qty)), font, Brushes.Black, new Point(150, offset + n));
                e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(sprice)), font, Brushes.Black, new Point(200, offset + n));
                e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(ssum)), font, Brushes.Black, new Point(250, offset + n));


                offset += 20;
                m += 20;
                n += 20;
            }
            za++;
            e.HasMorePages = za < pagesCount;

            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset + m));
            e.Graphics.DrawString("YEKUN MƏBLƏĞ", f9, Brushes.Black, 5, offset + m + 20);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(YekunMebleg)), f9, Brushes.Black, new Point(240, offset + m + 20));
            e.Graphics.DrawString("YEKUN ƏDV", f9, Brushes.Black, 5, offset + m + 30);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(edvhesap2) + Convert.ToDouble(edvdenazada2)), f9, Brushes.Black, new Point(240, offset + m + 30));

            e.Graphics.DrawString("ƏDV-dən azad = " + String.Format("{0:0.00}", Convert.ToDouble(edvdenazada2)) + " AZN", font, Brushes.Black, 5, offset + m + 40);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(edvdenazada1)), font, Brushes.Black, new Point(240, offset + m + 40));

            e.Graphics.DrawString("ƏDV %18 =" + String.Format("{0:0.00}", Convert.ToDouble(edvhesap2)) + " AZN", font, Brushes.Black, 5, offset + m + 50);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(edvhesap1)), font, Brushes.Black, new Point(240, offset + m + 50));


            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset + m + 60));
            e.Graphics.DrawString("Ödəniş üsulu", f8, Brushes.Black, 5, offset + m + 70);
            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 5, offset + m + 80);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(nagd)), f8, Brushes.Black, 240, offset + m + 80);
            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 5, offset + m + 90);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(kart)), f8, Brushes.Black, 240, offset + m + 90);
            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 5, offset + m + 100);
            e.Graphics.DrawString("0", f8, Brushes.Black, 240, offset + m + 100);

            e.Graphics.DrawString("Ödənilib nağd AZN :", f8, Brushes.Black, 5, offset + m + 130);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(odenen)), f8, Brushes.Black, 240, offset + m + 130);
            e.Graphics.DrawString("Qalıq qaytarılıb nağd AZN :", f8, Brushes.Black, 5, offset + m + 140);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(qaliq)), f8, Brushes.Black, 240, offset + m + 140);
            //e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset + m + 150));
            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, 5, offset + m + 153);

            e.Graphics.DrawString("Növbə ərzində vurulmuş çek sayı: " + gunfissayi, font, Brushes.Black, 5, offset + m + 165);
            e.Graphics.DrawString("NKA-nın modeli:" + TerminalTokenData.NkaModel, font, Brushes.Black, 5, offset + m + 175);
            e.Graphics.DrawString("NKA-nın zavod nömrəsi:" + TerminalTokenData.NkaSerialNumber, font, Brushes.Black, 5, offset + m + 190);
            e.Graphics.DrawString("NMQ-nin qeydiyyat nömrəsi:" + TerminalTokenData.NMQRegistrationNumber, font, Brushes.Black, 5, offset + m + 205);
            e.Graphics.DrawString("Fiskal İD:" + sdocumentid, font, Brushes.Black, 5, offset + m + 215);
            e.Graphics.DrawString("www.e-kassa.gov.az", font, Brushes.Black, 90, offset + m + 230);
            e.Graphics.DrawImage(qrcodeimg, 90, offset + m + 255, width: 80, height: 80);
        }

        void nba_saleprinttekrar(object sender, PrintPageEventArgs e)
        {
            Font font = new System.Drawing.Font("Times New Roman", 8f);
            Font font2 = new System.Drawing.Font("Times New Roman", 8.75f, FontStyle.Bold);
            Font font4 = new System.Drawing.Font("Times New Roman", 10f, FontStyle.Bold);
            Font font3 = new System.Drawing.Font("Times New Roman", 8.75f);
            Font f8 = new System.Drawing.Font("Times New Roman", 7.5f, FontStyle.Bold);
            Font f9 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Bold);
            Font fonta = new System.Drawing.Font("Times New Roman", 8.5f);

            int offset = 210;
            int offset2 = 10;
            int m = 0;
            int n = 20;

            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            var qrcodeimg = barcode.Draw("https://monitoring.e-kassa.gov.az/#/index?doc=" + sdocumentid, 0, scale: 5);


            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            Rectangle rect2 = new Rectangle(70, 10, 100, 122);

            string ustbaslik = "TS Adı :" + obyetname + "\r\n" + "TS Ünvanı :" + obyektadres + "\r\n" + "\r\n" + "VÖ Adı :" + customer + "\r\n" + "VÖEN :" + customervoen + "\r\n" + "Obyektin kodu :" + obyektkod;

            using (SqlConnection connection1 = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection1.Open();
                string query1 = $@"
SELECT ISNULL(ROUND(cashPayment, 2), 0) AS cashPayment,
ISNULL(ROUND(cardPayment, 2), 0) AS cardPayment,
ISNULL(bonusPayment, 0) AS bonusPayment,
ISNULL(clientName, '') AS clientName,
ISNULL(header_id, 0) AS header_id,
ISNULL(cashPayment + cardPayment, 0) AS tot,
ISNULL((SELECT TOP 1 [COMPANY_NAME] FROM [COMPANY].[COMPANY]), '') AS sirket,
ISNULL((SELECT TOP 1 [SIRKET_VOEN] FROM [COMPANY].[COMPANY]), '') AS voen
FROM header
WHERE userId = 3044";
                using (SqlCommand cmd = new SqlCommand(query1, connection1))
                {
                    using (SqlDataReader dr1 = cmd.ExecuteReader())
                    {
                        while (dr1.Read())
                        {
                            string client = dr1["clientName"].ToString();
                            string sirket = dr1["sirket"].ToString();
                            string voen = dr1["voen"].ToString();


                            e.Graphics.DrawString(ustbaslik, fonta, Brushes.Black, new RectangleF(35F, 12F, 220F, 110F), sf);

                            e.Graphics.DrawString("Satış Çeki (Təkrar Nüsxə)", font4, Brushes.Black, new Point(85, offset2 + 110));
                            e.Graphics.DrawString("Çek nömrəsi No:" + nbaLastDocumentResponse.data.doc.docNumber, font2, Brushes.Black, new Point(95, offset2 + 125));
                            e.Graphics.DrawString("Kassir:" + nbaLastDocumentResponse.data.doc.cashier, font, Brushes.Black, new Point(5, offset2 + 140));
                            e.Graphics.DrawString("Tarix: " + DateTime.Now.ToString("dd.MM.yyyy"), font, Brushes.Black, new Point(190, offset2 + 140));
                            e.Graphics.DrawString("Vaxt: " + DateTime.Now.ToString("HH:mm"), font, Brushes.Black, new Point(190, offset2 + 155));

                            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 170));
                            e.Graphics.DrawString("Malın adı", f8, Brushes.Black, 5, offset2 + 180);
                            e.Graphics.DrawString("Vh", f8, Brushes.Black, 130, offset2 + 180);
                            e.Graphics.DrawString("Miqdar", f8, Brushes.Black, 150, offset2 + 180);
                            e.Graphics.DrawString("Qiymət", f8, Brushes.Black, 200, offset2 + 180);
                            e.Graphics.DrawString("Toplam", f8, Brushes.Black, 250, offset2 + 180);
                            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset2 + 185));

                        }
                    }
                }
            }


            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                string query = $@"select name,
code,
ROUND(salePrice,2) as salePrice,
ROUND(quantity,2) AS quantity,
case  vatType
when 1 then '18' 
when 3 then '0' 
when 4 then '2' 
when 5 then '8' 
else 0 end as vatType,
round(quantityType,2) as quantityType,
ROUND(salePrice*quantity,2) as ssum,
case quantityType 
when 0 then N'əd' 
when 1 then 'kg' 
when 2 then 'lt' 
when 3 then 'mt' 
when 4 then 'm2' 
when 5 then 'm3' 
else '' end as vah 
FROM  dbo.item WHERE user_id = {Properties.Settings.Default.UserID}";
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string name = dr["name"].ToString();
                            string code = dr["code"].ToString();
                            string sprice = dr["salePrice"].ToString();
                            string qty = dr["quantity"].ToString();
                            string vat = dr["vatType"].ToString();
                            string qunit = dr["quantityType"].ToString();
                            string ssum = dr["ssum"].ToString();
                            string vah = dr["vah"].ToString();

                            e.Graphics.DrawString(name + "\r\n" + "ƏDV" + vat, font, Brushes.Black, new Point(5, offset + m));
                            e.Graphics.DrawString(vah, font, Brushes.Black, new Point(130, offset + n));
                            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(qty)), font, Brushes.Black, new Point(150, offset + n));
                            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(sprice)), font, Brushes.Black, new Point(200, offset + n));
                            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(ssum)), font, Brushes.Black, new Point(250, offset + n));

                            offset += 20;
                            m += 20;
                            n += 20;

                        }
                    }
                }
            }


            string fiskalID = nbaLastDocumentResponse.data.document_id.Substring(0, 12);


            za++;
            e.HasMorePages = za < pagesCount;

            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset + m));
            e.Graphics.DrawString("YEKUN MƏBLƏĞ", f9, Brushes.Black, 5, offset + m + 20);
            e.Graphics.DrawString(nbaLastDocumentResponse.data.doc.sum.ToString("N2"), f9, Brushes.Black, new Point(240, offset + m + 20));
            e.Graphics.DrawString("YEKUN ƏDV", f9, Brushes.Black, 5, offset + m + 30);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(edvhesap2) + Convert.ToDouble(edvdenazada2)), f9, Brushes.Black, new Point(240, offset + m + 30));

            e.Graphics.DrawString("ƏDV-dən azad = " + String.Format("{0:0.00}", Convert.ToDouble(edvdenazada2)) + " AZN", font, Brushes.Black, 5, offset + m + 40);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(edvdenazada1)), font, Brushes.Black, new Point(240, offset + m + 40));

            e.Graphics.DrawString("ƏDV %18 = " + String.Format("{0:0.00}", Convert.ToDouble(edvhesap2)) + " AZN", font, Brushes.Black, 5, offset + m + 50);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(edvhesap1)), font, Brushes.Black, new Point(240, offset + m + 50));


            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset + m + 60));
            e.Graphics.DrawString("Ödəniş üsulu", f8, Brushes.Black, 5, offset + m + 70);
            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 5, offset + m + 80);
            e.Graphics.DrawString(nbaLastDocumentResponse.data.doc.cashSum.ToString("N2"), f8, Brushes.Black, 240, offset + m + 80);
            e.Graphics.DrawString("Nağdsız:", f8, Brushes.Black, 5, offset + m + 90);
            e.Graphics.DrawString(nbaLastDocumentResponse.data.doc.cashlessSum.ToString("N2"), f8, Brushes.Black, 240, offset + m + 90);
            e.Graphics.DrawString("Bonus:", f8, Brushes.Black, 5, offset + m + 100);
            e.Graphics.DrawString(nbaLastDocumentResponse.data.doc.bonusSum.ToString("N2"), f8, Brushes.Black, 240, offset + m + 100);

            e.Graphics.DrawString("Ödənilib nağd AZN :", f8, Brushes.Black, 5, offset + m + 130);
            e.Graphics.DrawString(nbaLastDocumentResponse.data.doc.incomingSum.ToString("N2"), f8, Brushes.Black, 240, offset + m + 130);
            e.Graphics.DrawString("Qalıq qaytarılıb nağd AZN :", f8, Brushes.Black, 5, offset + m + 140);
            e.Graphics.DrawString(nbaLastDocumentResponse.data.doc.changeSum.ToString("N2"), f8, Brushes.Black, 240, offset + m + 140);
            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset + m + 150));

            e.Graphics.DrawString($"Növbə ərzində vurulmuş çek sayı: {nbaLastDocumentResponse.data.doc.positionInShift}", font, Brushes.Black, 5, offset + m + 165);
            e.Graphics.DrawString("NKA-nın modeli:" + TerminalTokenData.NkaModel, font, Brushes.Black, 5, offset + m + 175);
            e.Graphics.DrawString("NKA-nın zavod nömrəsi:" + TerminalTokenData.NkaSerialNumber, font, Brushes.Black, 5, offset + m + 190);
            e.Graphics.DrawString("NMQ-nın qeydiyyat nömrəsi:" + TerminalTokenData.NMQRegistrationNumber, font, Brushes.Black, 5, offset + m + 205);
            e.Graphics.DrawString("Fiskal ID: " + fiskalID, font, Brushes.Black, 5, offset + m + 215);

            e.Graphics.DrawString("www.e-kassa.gov.az", font, Brushes.Black, 80, offset + m + 230);

            e.Graphics.DrawImage(qrcodeimg, 90, offset + m + 255, width: 80, height: 80);
        }






        void nba_deposit(object sender, PrintPageEventArgs e)
        {
            Font font = new System.Drawing.Font("Times New Roman", 8f);
            Font font2 = new System.Drawing.Font("Times New Roman", 8.75f, FontStyle.Bold);
            Font font4 = new System.Drawing.Font("Times New Roman", 10f, FontStyle.Bold);
            Font font3 = new System.Drawing.Font("Times New Roman", 8.75f);
            Font f8 = new System.Drawing.Font("Times New Roman", 7.7f, FontStyle.Bold);
            Font f9 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Bold);
            Font fonta = new System.Drawing.Font("Times New Roman", 8.5f);
            string tutar = "";
            string nagd = "";
            string kart = "";

            int offset = 210;
            int offset2 = 10;
            int m = 0;
            int n = 20;

            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            var qrcodeimg = barcode.Draw("https://monitoring.e-kassa.gov.az/#/index?doc=" + sdocumentid, 0, scale: 5);


            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            Rectangle rect2 = new Rectangle(70, 10, 100, 122);

            string ustbaslik = "TS Adı :" + obyetname + "\r\n" + "TS Ünvanı :" + obyektadres + "\r\n" + "\r\n" + "VÖ Adı :" + customer + "\r\n" + "VÖEN :" + customervoen + "\r\n" + "Obyektin kodu :" + obyektkod;

            e.Graphics.DrawString(ustbaslik, fonta, Brushes.Black, new RectangleF(50F, 10F, 200F, 90F), sf);

            e.Graphics.DrawString("Mədaxil Çeki", font4, Brushes.Black, new Point(90, offset2 + 105));
            e.Graphics.DrawString("Çek nömrəsi No:" + textEdit1.Text + "-" + fissayi, font2, Brushes.Black, new Point(70, offset2 + 120));
            e.Graphics.DrawString("Kassir:" + tUsername.Text, font, Brushes.Black, new Point(5, offset2 + 135));
            e.Graphics.DrawString("Tarix: " + DateTime.Now.ToString("dd.MM.yyyy"), font, Brushes.Black, new Point(190, offset2 + 135));
            e.Graphics.DrawString("Saat: " + DateTime.Now.ToString("HH:mm"), font, Brushes.Black, new Point(190, offset2 + 150));

            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 165));
            e.Graphics.DrawString("Mədaxil məbləğı", f8, Brushes.Black, 5, offset2 + 180);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(deposita)), f9, Brushes.Black, new Point(240, offset2 + 180));




            za++;
            e.HasMorePages = za < pagesCount;

            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset2 + 200));
            e.Graphics.DrawString("YEKUN MƏBLƏĞ", f9, Brushes.Black, 5, offset2 + 215);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(deposita)), f9, Brushes.Black, new Point(240, offset2 + 215));

            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 230));
            e.Graphics.DrawString("Mədaxil üsulu", f8, Brushes.Black, 5, offset2 + 245);
            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 5, offset2 + 260);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(deposita)), f8, Brushes.Black, 240, offset2 + 260);

            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 275));

            e.Graphics.DrawString("Növbə ərzində vurulmuş çek sayı:" + gunfissayi, font, Brushes.Black, 5, offset2 + 290);
            e.Graphics.DrawString("NKA-nın modeli:" + TerminalTokenData.NkaModel, font, Brushes.Black, 5, offset2 + 305);
            e.Graphics.DrawString("NKA-nın zavod nömrəsi:" + TerminalTokenData.NkaSerialNumber, font, Brushes.Black, 5, offset2 + 320);
            e.Graphics.DrawString("NMQ-nın qeydiyyat nömrəsi:" + TerminalTokenData.NMQRegistrationNumber, font, Brushes.Black, 5, offset2 + 335);
            e.Graphics.DrawString("Fiskal ID:" + sdocumentid, font, Brushes.Black, 5, offset2 + 350);

            e.Graphics.DrawString("www.e-kassa.gov.az", font, Brushes.Black, 60, offset2 + 365);

            e.Graphics.DrawImage(qrcodeimg, 90, offset2 + 380, width: 80, height: 80);
        }

        void nba_withdraw(object sender, PrintPageEventArgs e)
        {
            Font font = new System.Drawing.Font("Times New Roman", 8f);
            Font font2 = new System.Drawing.Font("Times New Roman", 8.75f, FontStyle.Bold);
            Font font4 = new System.Drawing.Font("Times New Roman", 10f, FontStyle.Bold);
            Font font3 = new System.Drawing.Font("Times New Roman", 8.75f);
            Font f8 = new System.Drawing.Font("Times New Roman", 7.5f, FontStyle.Bold);
            Font f9 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Bold);
            Font fonta = new System.Drawing.Font("Times New Roman", 8.5f);
            string tutar = "";
            string nagd = "";
            string kart = "";

            int offset = 210;
            int offset2 = 10;
            int m = 0;
            int n = 20;

            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            var qrcodeimg = barcode.Draw("https://monitoring.e-kassa.gov.az/#/index?doc=" + sdocumentid, 0, scale: 5);


            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            Rectangle rect2 = new Rectangle(70, 10, 100, 122);

            string ustbaslik = "TS Adı :" + obyetname + "\r\n" + "TS Ünvanı :" + obyektadres + "\r\n" + "\r\n" + "VÖ Adı :" + customer + "\r\n" + "VÖEN :" + customervoen + "\r\n" + "Obyektin kodu :" + obyektkod;

            e.Graphics.DrawString(ustbaslik, fonta, Brushes.Black, new RectangleF(50F, 10F, 200F, 90F), sf);

            e.Graphics.DrawString("Məxaric Çeki", font4, Brushes.Black, new Point(90, offset2 + 105));
            e.Graphics.DrawString("Çek nömrəsi No:" + textEdit1.Text + "-" + fissayi, font2, Brushes.Black, new Point(70, offset2 + 120));
            e.Graphics.DrawString("Kassir:" + tUsername.Text, font, Brushes.Black, new Point(5, offset2 + 135));
            e.Graphics.DrawString("Tarix: " + DateTime.Now.ToString("dd.MM.yyyy"), font, Brushes.Black, new Point(190, offset2 + 135));
            e.Graphics.DrawString("Saat: " + DateTime.Now.ToString("HH:mm"), font, Brushes.Black, new Point(190, offset2 + 150));

            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 165));
            e.Graphics.DrawString("Məxaric məbləği", f8, Brushes.Black, 5, offset2 + 180);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(withdrawa)), f9, Brushes.Black, new Point(240, offset2 + 180));




            za++;
            e.HasMorePages = za < pagesCount;

            e.Graphics.DrawString("_______________________________________________", font2, Brushes.Black, new Point(5, offset2 + 200));
            e.Graphics.DrawString("YEKUN MƏBLƏĞ", f9, Brushes.Black, 5, offset2 + 215);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(withdrawa)), f9, Brushes.Black, new Point(240, offset2 + 215));

            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 230));
            e.Graphics.DrawString("Məxaric üsulu", f8, Brushes.Black, 5, offset2 + 245);
            e.Graphics.DrawString("Nağd:", f8, Brushes.Black, 5, offset2 + 260);
            e.Graphics.DrawString(String.Format("{0:0.00}", Convert.ToDouble(withdrawa)), f8, Brushes.Black, 240, offset2 + 260);

            e.Graphics.DrawString("**********************************************", font2, Brushes.Black, new Point(5, offset2 + 275));

            e.Graphics.DrawString("Növbə ərzində vurulmuş çek sayı:" + gunfissayi, font, Brushes.Black, 5, offset2 + 290);
            e.Graphics.DrawString("NKA-nın modeli:" + TerminalTokenData.NkaModel, font, Brushes.Black, 5, offset2 + 305);
            e.Graphics.DrawString("NKA-nın zavod nömrəsi:" + TerminalTokenData.NkaSerialNumber, font, Brushes.Black, 5, offset2 + 320);
            e.Graphics.DrawString("NMQ-nın qeydiyyat nömrəsi:" + TerminalTokenData.NMQRegistrationNumber, font, Brushes.Black, 5, offset2 + 335);
            e.Graphics.DrawString("Fiskal ID:" + sdocumentid, font, Brushes.Black, 5, offset2 + 350);

            e.Graphics.DrawString("www.e-kassa.gov.az", font, Brushes.Black, 60, offset2 + 365);

            e.Graphics.DrawImage(qrcodeimg, 90, offset2 + 380, width: 80, height: 80);
        }

        void nba_bankprint(object sender, PrintPageEventArgs e)
        {
            Font font4 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Regular);
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






            //Font font4 = new System.Drawing.Font("Times New Roman", 8f, FontStyle.Regular);


            //int offset2 = 10;

            //StringFormat sf = new StringFormat();
            //sf.LineAlignment = StringAlignment.Center;
            //sf.Alignment = StringAlignment.Center;


            //e.Graphics.DrawString(bankdizi[0], font4, Brushes.Black, new Point(90, offset2 + 10));
            //e.Graphics.DrawString(bankdizi[1], font4, Brushes.Black, new Point(10, offset2 + 20));
            //e.Graphics.DrawString(bankdizi[2], font4, Brushes.Black, new Point(10, offset2 + 30));
            //e.Graphics.DrawString(bankdizi[3], font4, Brushes.Black, new Point(10, offset2 + 40));
            //e.Graphics.DrawString(bankdizi[4], font4, Brushes.Black, new Point(10, offset2 + 50));
            //e.Graphics.DrawString(bankdizi[5], font4, Brushes.Black, new Point(10, offset2 + 60));
            //e.Graphics.DrawString(bankdizi[6], font4, Brushes.Black, new Point(10, offset2 + 70));
            //e.Graphics.DrawString(bankdizi[7], font4, Brushes.Black, new Point(10, offset2 + 80));
            //e.Graphics.DrawString(bankdizi[8], font4, Brushes.Black, new Point(10, offset2 + 90));
            //e.Graphics.DrawString(bankdizi[9], font4, Brushes.Black, new Point(10, offset2 + 100));
            //e.Graphics.DrawString(bankdizi[10], font4, Brushes.Black, new Point(10, offset2 + 110));
            //e.Graphics.DrawString(bankdizi[11], font4, Brushes.Black, new Point(10, offset2 + 120));
            //e.Graphics.DrawString(bankdizi[12], font4, Brushes.Black, new Point(10, offset2 + 130));
            //e.Graphics.DrawString(bankdizi[13], font4, Brushes.Black, new Point(10, offset2 + 140));
            //e.Graphics.DrawString(bankdizi[14], font4, Brushes.Black, new Point(10, offset2 + 150));
            //e.Graphics.DrawString(bankdizi[15], font4, Brushes.Black, new Point(10, offset2 + 160));
            //e.Graphics.DrawString(bankdizi[16], font4, Brushes.Black, new Point(10, offset2 + 170));
            //e.Graphics.DrawString(bankdizi[17], font4, Brushes.Black, new Point(10, offset2 + 180));
        }


        void nba_bankprints(object sender, PrintPageEventArgs e)
        {
            Font font4 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Regular);
            int offsetY = 10;
            int lineSpacing = 15;

            float pageWidth = e.PageBounds.Width;

            foreach (string line in bankdizis)
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

        void nba_bankprintc(object sender, PrintPageEventArgs e)
        {
            Font font4 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Regular);
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


















            //Font font4 = new System.Drawing.Font("Times New Roman", 9f, FontStyle.Regular);

            //int offset2 = 10;

            //StringFormat sf = new StringFormat();
            //sf.LineAlignment = StringAlignment.Center;
            //sf.Alignment = StringAlignment.Center;


            //e.Graphics.DrawString(bankdizic[0], font4, Brushes.Black, new Point(90, offset2 + 10));
            //e.Graphics.DrawString(bankdizic[1], font4, Brushes.Black, new Point(10, offset2 + 20));
            //e.Graphics.DrawString(bankdizic[2], font4, Brushes.Black, new Point(10, offset2 + 30));
            //e.Graphics.DrawString(bankdizic[3], font4, Brushes.Black, new Point(10, offset2 + 40));
            //e.Graphics.DrawString(bankdizic[4], font4, Brushes.Black, new Point(10, offset2 + 50));
            //e.Graphics.DrawString(bankdizic[5], font4, Brushes.Black, new Point(10, offset2 + 60));
            //e.Graphics.DrawString(bankdizic[6], font4, Brushes.Black, new Point(10, offset2 + 70));
            //e.Graphics.DrawString(bankdizic[7], font4, Brushes.Black, new Point(10, offset2 + 80));
            //e.Graphics.DrawString(bankdizic[8], font4, Brushes.Black, new Point(10, offset2 + 90));
            //e.Graphics.DrawString(bankdizic[9], font4, Brushes.Black, new Point(10, offset2 + 100));
            //e.Graphics.DrawString(bankdizic[10], font4, Brushes.Black, new Point(10, offset2 + 110));
            //e.Graphics.DrawString(bankdizic[11], font4, Brushes.Black, new Point(10, offset2 + 120));
            //e.Graphics.DrawString(bankdizic[12], font4, Brushes.Black, new Point(10, offset2 + 130));
            //e.Graphics.DrawString(bankdizic[13], font4, Brushes.Black, new Point(10, offset2 + 140));
            //e.Graphics.DrawString(bankdizic[14], font4, Brushes.Black, new Point(10, offset2 + 150));
            //e.Graphics.DrawString(bankdizic[15], font4, Brushes.Black, new Point(10, offset2 + 160));
            //e.Graphics.DrawString(bankdizic[16], font4, Brushes.Black, new Point(10, offset2 + 170));
            //e.Graphics.DrawString(bankdizic[17], font4, Brushes.Black, new Point(10, offset2 + 180));
        }

        void tileproduct()
        {
            var control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("HotSalesShow").ToString());
            if (control)
            {
                SqlConnection conn2 = new SqlConnection(DbHelpers.DbConnectionString);
                SqlCommand cmd2 = new SqlCommand();
                conn2.Open();

                string query2 = @"WITH RankedProducts AS (
    SELECT 
        ct.SIRKET_ADI,
        md.MEHSUL_ADI,
        md.SATIS_GIYMETI,
        ROW_NUMBER() OVER (PARTITION BY md.MEHSUL_ADI, ct.SIRKET_ADI ORDER BY am.mal_details_id DESC) AS rn
    FROM ANBAR_MAGAZA am
    INNER JOIN MAL_ALISI_DETAILS md ON am.mal_details_id = md.MAL_ALISI_DETAILS_ID
    INNER JOIN MAL_ALISI_MAIN m ON m.MAL_ALISI_MAIN_ID = md.MAL_ALISI_MAIN_ID
    INNER JOIN COMPANY.TECHIZATCI ct ON m.TECHIZATCI_ID = ct.TECHIZATCI_ID
    WHERE md.ShowPosScreen = 1
)

SELECT 
    SIRKET_ADI AS N'TƏCHİZATÇI ADI',
    MEHSUL_ADI AS N'MƏHSUL ADI',
    SATIS_GIYMETI AS N'SATIŞ QİYMƏTİ'
FROM RankedProducts
WHERE rn = 1;";

                cmd2.Connection = conn2;
                cmd2.CommandText = query2;

                SqlDataReader dr2 = cmd2.ExecuteReader();


                tileControl1.Groups.Clear();
                tileControl1.ItemSize = 150;
                tileControl1.ItemPadding = new Padding(5);
                TileBarGroup group1 = new TileBarGroup();
                int counts = 1;

                while (dr2.Read())
                {
                    TileItem tileItem = new TileItem();
                    tileItem.ItemSize = TileItemSize.Medium;
                    tileItem.AppearanceItem.Normal.BackColor = Color.FromArgb(57, 123, 173);
                    tileItem.AppearanceItem.Normal.ForeColor = Color.White;

                    string productName = dr2["MƏHSUL ADI"].ToString();
                    string supplier = dr2["TƏCHİZATÇI ADI"].ToString();
                    double price = Convert.ToDouble(dr2["SATIŞ QİYMƏTİ"].ToString());

                    // Supplier
                    TileItemElement titleElement = new TileItemElement();
                    titleElement.Text = supplier;
                    titleElement.TextAlignment = TileItemContentAlignment.TopLeft;
                    titleElement.Appearance.Normal.Font = new Font("Tahoma", 10, FontStyle.Regular);

                    // Product Name
                    TileItemElement productElement = new TileItemElement();
                    productElement.Text = productName;
                    productElement.TextAlignment = TileItemContentAlignment.MiddleCenter;
                    productElement.MaxWidth = 120;
                    productElement.Appearance.Normal.Font = new Font("Nunito", 10);

                    // Price
                    TileItemElement priceElement = new TileItemElement();
                    priceElement.Text = price.ToString("C2");
                    priceElement.TextAlignment = TileItemContentAlignment.BottomRight;
                    priceElement.Appearance.Normal.Font = new Font("Tahoma", 10);


                    tileItem.Elements.Add(titleElement);
                    tileItem.Elements.Add(productElement);
                    tileItem.Elements.Add(priceElement);

                    group1.Items.Add(tileItem);
                    tileControl1.Groups.Add(group1);
                    counts++;
                }
            }
            else
            {
                tileControl1.Visible = false;
                layoutControlItem60.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void tileControl1_ItemClick(object sender, TileItemEventArgs e)
        {
            string clicks = e.Item.Elements[1].Text;
            getbarkod_mehsuladi(clicks);

            getall(tBarcode.Text);
            get(textEdit1.Text);
            get_say_birmal(tBarcode.Text, textEdit1.Text);
            tBarcode.Text = string.Empty;

            get_cem(textEdit1.Text);
            textBox5.Text = "";
        }

        private void bAddPosScreen_Click(object sender, EventArgs e)
        {
            var control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("HotSalesShow").ToString());
            if (control)
            {
                fQuickSale f = new fQuickSale();
                f.ShowDialog();
                tileproduct();
            }
            else
            {
                FormHelpers.Alert("İsti satışlar bölməsi aktiv deyil. Sazlamalardan aktiv edin.", MessageType.Info);
                return;
            }
        }

        private void Basket_Click(object sender, EventArgs e)
        {
            SimpleButton btn = (SimpleButton)sender;

            if (btn.Appearance.BackColor != Color.FromArgb(1, 133, 116)) //Yaşıl
            {
                if (gridView1.RowCount > 0)
                {
                    FormHelpers.Alert("Məhsul seçim səbətiniz boş deyil !", MessageType.Warning);
                    return;
                }

                fBasket f = new fBasket(btn.Text);
                f.Text = btn.Text;
                if (f.ShowDialog() is DialogResult.OK)
                {
                    string code = DbProsedures.GET_SalesProcessNo();
                    get(code);
                    get_cem(code);
                    gridView1.GroupPanelText = $"Məhsul sayı: {gridView1.RowCount}";
                    FormHelpers.Alert("Məhsullar səbətdən çıxarıldı", MessageType.Success);
                    btn.Appearance.BackColor = Color.FromArgb(1, 133, 116); //Yaşıl
                }
            }
            else
            {
                if (gridView1.RowCount > 0)
                {
                    if (DbProsedures.InsertPosBasketData(btn.Text))
                    {
                        btn.Appearance.BackColor = Color.FromArgb(231, 72, 86); //Qırmızı
                        BasketDataControl();
                        FormHelpers.Alert("Məhsullar səbətə əlavə edildi", MessageType.Success);
                        clear();
                    }
                }
                else
                {
                    FormHelpers.Alert("Məhsul seçimi edilmədi", MessageType.Warning);
                }
            }
        }

        private void BasketDataControl()
        {
            string query = $"SELECT BasketName FROM PosBaskets WHERE UserId = {Properties.Settings.Default.UserID} GROUP BY BasketName";
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string basketName = dr["BasketName"].ToString();

                            switch (basketName)
                            {
                                case "Səbət 1":
                                    bBasket1.Appearance.BackColor = Color.FromArgb(231, 72, 86);
                                    break;
                                case "Səbət 2":
                                    bBasket2.Appearance.BackColor = Color.FromArgb(231, 72, 86);
                                    break;
                                case "Səbət 3":
                                    bBasket3.Appearance.BackColor = Color.FromArgb(231, 72, 86);
                                    break;
                                case "Səbət 4":
                                    bBasket4.Appearance.BackColor = Color.FromArgb(231, 72, 86);
                                    break;
                                case "Səbət 5":
                                    bBasket5.Appearance.BackColor = Color.FromArgb(231, 72, 86);
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}