using DevExpress.XtraBars;
using DevExpress.XtraEditors;
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
using System.Globalization;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Forms;

namespace WindowsFormsApp2
{
    public partial class MEHSUL_ALISI_LAYOUT : DevExpress.XtraEditors.XtraForm
    {
        public static int mal_user_id;
        public MEHSUL_ALISI_LAYOUT(int m_userid)
        {
            InitializeComponent();
            DevExpress.XtraEditors.Controls.Localizer.Active = new MsgBoxLocalizer();
            mal_user_id = m_userid;
        }



        public static byte[] picture_edit_value = null;
        public static string radio = "";
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        private const String CATEGORIES_TABLE = "Categories";
        // field name constants

        private DataTable dt;
        private SqlDataAdapter da;

        //  SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = NewInteko; Integrated Security = True");

        public void tab()
        {
            gridControl1.TabIndex = 100;
            //simpleButton2.TabIndex = 99;
            //simpleButton1.TabIndex = 98;
            simpleButton3.TabIndex = 97;

            dateEdit1.TabIndex = 1;
            textEdit5.TabIndex = 2;
            lookUpEdit2.TabIndex = 3;
            textBox1.TabIndex = 4; //kateqoriya
            textEdit2.TabIndex = 4; //mehsul adi 
            textEdit3.TabIndex = 6; //barkod

            textEdit1.TabIndex = 7;
            lookUpEdit3.TabIndex = 8;
            memoEdit1.TabIndex = 9;
            textEdit8.TabIndex = 10;
            lookUpEdit5.TabIndex = 11;
            lookUpEdit4.TabIndex = 12;
            lookUpEdit6.TabIndex = 13;
            textEdit9.TabIndex = 14;
            textEdit6.TabIndex = 15;
            textEdit7.TabIndex = 16;
            textEdit13.TabIndex = 17;
            textEdit10.TabIndex = 18;
            textEdit4.TabIndex = 19;
            dateEdit2.TabIndex = 20;
            dateEdit3.TabIndex = 21;
            spinEdit1.TabIndex = 22;
            //simpleButton4.TabIndex = 22;
            //simpleButton7.TabIndex = 23;
            //simpleButton5.TabIndex = 24;
            //simpleButton6.TabIndex = 25;

        }



        private string qeryString = "EXEC  dbo.MAL_ALISI_EMELIYYAT_NOMRE";
        private string querytarix = " EXEC dbo.mal_alisi_tarix";
        public void GETTARIX()
        {

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
            {

                SqlCommand command = new SqlCommand(querytarix, connection);


                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //XtraMessageBox.Show(reader[0].ToString());

                        dateEdit1.Text = reader[0].ToString();


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

        private void MEHSUL_ALISI_LAYOUT_Load(object sender, EventArgs e)
        {
            //XtraMessageBox.Show(mal_user_id.ToString());
            InitLookUpEdit_();
            //textEdit9.Text = "0.00";
            simpleButton2.ForeColor = Color.Red;

            radioButton1.Checked = true;
            textEdit16.Enabled = false;
            textEdit16.Text = DbProsedures.GET_ProductProcessNo();
            //      GETTARIX();


            textEdit11.ForeColor = Color.Red;
            this.spinEdit1.Properties.MinValue = 0;
            this.spinEdit1.Properties.MaxValue = int.MaxValue;
            lookUpEdit3.Enabled = false;


            gridControl1.TabStop = false;
            //tab();


            label1.Text = "0";

            lookupedittextxhange_main();
            //  ANBAR_main();
            InitLookUpEdit();
            vahid_main();
            valyuta_main();
            VERGİ_main();
            Auto();



            DateTime dateTime = DateTime.UtcNow.Date;
            dateEdit1.Text = dateTime.ToString();



        }

        public void Auto()

        {

            da = new SqlDataAdapter("select KATEGORIYA from KATEGORIYA", Properties.Settings.Default.SqlCon);

            DataTable dt = new DataTable();

            da.Fill(dt);

            if (dt.Rows.Count > 0)

            {

                for (int i = 0; i < dt.Rows.Count; i++)

                {

                    coll.Add(dt.Rows[i]["KATEGORIYA"].ToString());

                }

            }
            else

            {

                //MessageBox.Show("Name not found");

            }

            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;

            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            textBox1.AutoCompleteCustomSource = coll;

        }
        private void lookupedittextxhange_main()
        {
            //int id = Convert.ToInt32(lookUpEdit1.EditValue.ToString());

            //if (id > 0)
            //{
            string strQuery = "select TECHIZATCI_ID,SIRKET_ADI AS N'ŞİRKƏT ADI' from COMPANY.TECHIZATCI";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            lookUpEdit2.Properties.DisplayMember = "ŞİRKƏT ADI";
            lookUpEdit2.Properties.ValueMember = "TECHIZATCI_ID";
            lookUpEdit2.Properties.DataSource = dt;
            lookUpEdit2.Properties.NullText = "TƏCHİZATÇINI SEÇİN";
            lookUpEdit2.Properties.PopulateColumns();
            lookUpEdit2.Properties.Columns[0].Visible = false;

            //}
        }

        private void vahid_main()
        {
            string strQuery = "select VAHIDLER_ID, VAHIDLER_NAME as N'VAHIDLƏR' from VAHIDLER";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            lookUpEdit5.Properties.DisplayMember = "VAHIDLƏR";
            lookUpEdit5.Properties.ValueMember = "VAHIDLER_ID";
            lookUpEdit5.Properties.DataSource = dt;
            lookUpEdit5.Properties.NullText = "VAHIDLƏR";
            lookUpEdit5.Properties.PopulateColumns();
            lookUpEdit5.Properties.Columns[0].Visible = false;
        }

        private void VERGİ_main()
        {
            //int id = Convert.ToInt32(lookUpEdit1.EditValue.ToString());

            //if (id > 0)
            //{
            string strQuery = "select EDV_ID,EDV as N'VERGİ DƏRƏCƏSİ' from VERGI_DERECESI";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            lookUpEdit6.Properties.DisplayMember = "VERGİ DƏRƏCƏSİ";
            lookUpEdit6.Properties.ValueMember = "EDV_ID";
            lookUpEdit6.Properties.DataSource = dt;
            lookUpEdit6.Properties.NullText = "VERGİ";
            lookUpEdit6.Properties.PopulateColumns();
            lookUpEdit6.Properties.Columns[0].Visible = false;
            //}
        }
        private void valyuta_main()
        {
            //int id = Convert.ToInt32(lookUpEdit1.EditValue.ToString());

            //if (id > 0)
            //{
            string strQuery = "select VALYUTALAR_ID,VALYUTALAR from VALYUTALAR";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            lookUpEdit4.Properties.DisplayMember = "VALYUTALAR";
            lookUpEdit4.Properties.ValueMember = "VALYUTALAR_ID";
            lookUpEdit4.Properties.DataSource = dt;
            lookUpEdit4.Properties.NullText = "VALYUTALAR";
            lookUpEdit4.EditValue = 1;
            lookUpEdit4.Properties.PopulateColumns();
            lookUpEdit4.Properties.Columns[0].Visible = false;
            //}
        }

        private void getyeni_mebleg(string emeliyyat_nomre, int t_id)
        {
            string queryString = @"
            SELECT 
            cast(sum(isnull(d.ALIS_GIYMETI,0.00)*isnull(d.MIGDARI,0.00)) as decimal(9,2)) as yeni_borc
            from MAL_ALISI_MAIN m inner join MAL_ALISI_DETAILS d on m.MAL_ALISI_MAIN_ID = d.MAL_ALISI_MAIN_ID 
            AND m.TECHIZATCI_ID=@pricePoint1 
            where m.EMELIYYAT_NOMRE = @pricePoint";

            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.AddWithValue("@pricePoint", emeliyyat_nomre);
            command.Parameters.AddWithValue("@pricePoint1", t_id);
            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {

                textEdit12.Text = dr["yeni_borc"].ToString();

            }
            connection.Close();
        }

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

        public void lookupedit2_enabled()
        {
            int x = MSD.count_mal_(textEdit16.Text);
            if (x > 0)
            {
                lookUpEdit2.Enabled = false;
            }
            else
            {
                lookUpEdit2.Enabled = true;
            }
        }
        public void GetallData(string emeliyyat_nomre)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                string queryString = "SELECT * FROM dbo.fn_MAL_ALISI_LOAD(@pricepoint)";

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

        MEHSUL_ALISI MSD = new MEHSUL_ALISI();
        public class MsgBoxLocalizer : DevExpress.XtraEditors.Controls.Localizer
        {
            public override string GetLocalizedString(DevExpress.XtraEditors.Controls.StringId id)
            {
                if (id == DevExpress.XtraEditors.Controls.StringId.XtraMessageBoxYesButtonText)
                    return "BƏLİ";
                if (id == DevExpress.XtraEditors.Controls.StringId.XtraMessageBoxNoButtonText)
                    return "XEYR";
                return base.GetLocalizedString(id);
            }
        }
        private void textEdit3_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {

            }
            else
            {
                int A = DbProsedures.Exists_Category(textBox1.Text);
                if (A == -1)
                {
                    DialogResult dialogResult = XtraMessageBox.Show("YENİ KATEGORİYA YARADILSIN ?", "BİLDİRİŞ", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //do something
                        int f = MSD.InsertKATEGORY(textBox1.Text);
                        if (f > 0)
                        {
                            XtraMessageBox.Show("YENİ KATEQORİYA UĞURLA YARADILDI");
                        }

                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                        textBox1.Text = "";
                    }
                }
            }


        }

        private void textEdit2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {

            }
            else
            {
                int A = DbProsedures.Exists_Category(textBox1.Text);
                if (A == -1)
                {
                    DialogResult dialogResult = MessageBox.Show("YENİ KATEGORİYA YARADILSIN ?", "XƏBƏRDALIQ", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //do something
                        int f = MSD.InsertKATEGORY(textBox1.Text.ToString());
                        if (f > 0)
                        {
                            XtraMessageBox.Show("YENİ KATEQORİYA UĞURLA YARADILDI");
                        }

                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                        textBox1.Text = "";
                    }
                }
            }

        }

        private void textEdit1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {

            }
            else
            {
                int A = DbProsedures.Exists_Category(textBox1.Text);
                if (A == -1)
                {
                    DialogResult dialogResult = MessageBox.Show("YENİ KATEGORİYA YARADILSIN ?", "XƏBƏRDALIQ", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //do something
                        int f = MSD.InsertKATEGORY(textBox1.Text);
                        if (f > 0)
                        {
                            FormHelpers.Alert("Yeni kateqoriya uğurla yaradıldı", Enums.MessageType.Success);
                        }

                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                        textBox1.Text = "";
                    }
                }
            }
        }

        private void textEdit13_TextChanged_1(object sender, EventArgs e)
        {
            //ok
            textEdit7.Text = "0.00";
            getmebleg(textEdit8.Text, textEdit9.Text.ToString(), textEdit7.Text, textEdit13.Text);
        }

        private void textEdit13_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //ok
            textEdit7.Text = "0.00";
            getmebleg(textEdit8.Text, textEdit9.Text.ToString(), textEdit7.Text, textEdit13.Text);
        }

        public void getmebleg(string paramValue, string paramValue1, string paramValue2, string paramValue3)
        {
            string queryString = " exec yekun_mebleg_calc @migdar =@pricePoint,@alis_giymet =@pricePoint1,@endirim_faiz =@pricePoint2,@endirim_azn =@pricePoint3";
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
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

        private void textEdit8_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //ok
            getmebleg(textEdit8.Text, textEdit9.Text, textEdit7.Text, textEdit13.Text);
        }

        private void textEdit8_TextChanged_1(object sender, EventArgs e)
        {
            //ok
            getmebleg(textEdit8.Text, textEdit9.Text, textEdit7.Text, textEdit13.Text);
        }

        private void textEdit9_TextChanged_1(object sender, EventArgs e)
        {
            //ok
            getmebleg(textEdit8.Text, textEdit9.Text, textEdit7.Text, textEdit13.Text);
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            //negd
            radio = radioButton1.Text;
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            //nisye
            radio = radioButton2.Text;
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            //bank
            radio = radioButton3.Text;
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //textEdit3.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BARKOD").ToString();
        }
        public static int update_mal_details_id;
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {


                int paramValue = Convert.ToInt32(dr[0]);
                update_mal_details_id = Convert.ToInt32(dr[0]);
                //XtraMessageBox.Show(paramValue.ToString());
                string queryString =
                    "SELECT D.MAL_ALISI_DETAILS_ID,K.KATEGORIYA,D.BARKOD,D.MEHSUL_ADI,D.MEHSUL_KODU,D.MIGDARI,V.VAHIDLER_NAME " +
                     " , VA.VALYUTALAR,VDS.EDV,D.ALIS_GIYMETI,D.SATIS_GIYMETI,D.ENDIRIM_FAIZ,D.ENDIRIM_AZN, " +
                     " D.ENDIRIM_MEBLEGI,D.YEKUN_MEBLEG,D.ISTEHSAL_TARIXI,ISNULL(D.BITIS_TARIXI, ''),ISNULL(D.XEBERDAR_ET, '') " +
                     " FROM MAL_ALISI_DETAILS D INNER JOIN KATEGORIYA K ON K.KATEGORIYA_ID = D.KATEGORIYA " +
                     " INNER JOIN COMPANY.WAREHOUSE CW ON CW.WAREHOUSE_ID = D.ANBAR_ID " +
                     " INNER JOIN VAHIDLER V ON V.VAHIDLER_ID = D.VAHID " +
                     "  INNER JOIN VALYUTALAR VA ON VA.VALYUTALAR_ID = D.VALYUTA " +
                     " INNER JOIN VERGI_DERECESI VDS ON VDS.EDV_ID = D.VERGI_DERECESI " +
                     " WHERE MAL_ALISI_DETAILS_ID = @pricePoint ";

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
                            textBox1.Text = reader[1].ToString();
                            textEdit3.Text = reader[2].ToString();
                            textEdit2.Text = reader[3].ToString();
                            textEdit1.Text = reader[4].ToString();
                            textEdit8.Text = reader[5].ToString();
                            lookUpEdit5.Text = reader[6].ToString();
                            lookUpEdit4.Text = reader[7].ToString();
                            lookUpEdit6.Text = reader[8].ToString();
                            textEdit9.Text = reader[9].ToString();
                            textEdit6.Text = reader[10].ToString();
                            textEdit7.Text = reader[11].ToString();
                            textEdit13.Text = reader[12].ToString();
                            textEdit10.Text = reader[13].ToString();
                            textEdit4.Text = reader[14].ToString();
                            dateEdit2.Text = reader[15].ToString();
                            dateEdit3.Text = reader[16].ToString();
                            spinEdit1.Text = reader[17].ToString();

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



        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }


        MAL_ALISI_CRUD ms = new MAL_ALISI_CRUD();

        public void CL()
        {

            clear();
            GetallData("");
            dateEdit1.Text = "";
            textEdit5.Text = "";
            lookUpEdit2.EditValue = null;
            textEdit12.Text = "";
            textEdit14.Text = "";
        }
        private void pictureEdit1_EditValueChanged_1(object sender, EventArgs e)
        {
            if (pictureEdit1.Image == null)
            {

            }
            else
            {
                MemoryStream stream = new MemoryStream();
                pictureEdit1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] pic = stream.ToArray();
                picture_edit_value = pic;
            }
        }

        public void clear()
        {
            /*kateqoriya
             * barkod
             * mehsuladı
             * mehsulkodu
             * comment
             * mıqdar
             * satış
             * alış
             * endirim faiz
             * endirim azn
             * istehsal
             * bitiş
             * xeberdaret
            */
            textBox1.Text = "";
            textEdit3.Text = "";
            textEdit2.Text = "";
            textEdit1.Text = "";
            memoEdit1.Text = "";
            textEdit8.Text = "";
            textEdit6.Text = "";
            textEdit9.Text = "";
            textEdit7.Text = "";
            textEdit13.Text = "";
            dateEdit2.Text = "";
            dateEdit3.Text = "";
            spinEdit1.Text = "0";
        }

        private List<Account> datasource;
        private void InitLookUpEdit()
        {
            datasource = new List<Account>();
            datasource.Add(new Account("- MƏRKƏZ ANBAR - (ƏSAS)") { ID = 4 });
            lookUpEdit3.Properties.DataSource = datasource;
            lookUpEdit3.Properties.DisplayMember = "Name";
            lookUpEdit3.Properties.ValueMember = "ID";
        }

        public void getsum(int supplierId)
        {
            string queryString = @"
            SELECT Y.BORC - X.GAYTARMA_MEBLEG AS BORC FROM( select 1 AS ID, cast(sum(isnull(BORC, 0.00)) as decimal(9, 2)) as BORC
            FROM (SELECT f.MAL_ALISI_MAIN_ID, f.[FAKTURA NÖMRƏ],f.TARIX, f.QİYMƏT - isnull(t.odenis, 0.00) BORC,0 AS 'ÖDƏNİŞ'
            FROM dbo.fn_TECHIZATCI_BORC(@pricePoint) f 
            left join(select  MAL_ALISI_MAIN_ID, sum(ODENIS) odenis FROM TECHIZATCI_ODENIS
            group by MAL_ALISI_MAIN_ID)t  on f.MAL_ALISI_MAIN_ID = t.MAL_ALISI_MAIN_ID)o )Y
            LEFT JOIN(SELECT 1 AS ID, ISNULL(CAST(SUM(MD.ALIS_GIYMETI * D.MIGDARI) AS decimal(9, 2)), 0.00)
            AS GAYTARMA_MEBLEG FROM MAL_GEYTARMA_MAIN M
            INNER JOIN  MAL_GEYTARMA_DETAILS D ON
            M.MAL_GEYTARMA_MAIN_ID = D.MAL_GEYTARMA_MAIN_ID
            INNER JOIN MAL_ALISI_DETAILS MD ON MD.MAL_ALISI_DETAILS_ID = D.MAL_ALISI_DETAILS_ID
            INNER JOIN MAL_ALISI_MAIN MM ON MM.MAL_ALISI_MAIN_ID = MD.MAL_ALISI_MAIN_ID
            WHERE MM.TECHIZATCI_ID = @pricePoint)X ON X.ID = Y.ID";
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.AddWithValue("@pricePoint", supplierId);

            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {

                textEdit11.Text = dr["BORC"].ToString();

            }
            connection.Close();


        }


        private void lookUpEdit2_TextChanged_1(object sender, EventArgs e)
        {
            //techiatci
            if (lookUpEdit2.EditValue != null)
            {


                getsum(Convert.ToInt32(lookUpEdit2.EditValue));
                getyeni_mebleg(textEdit16.Text, Convert.ToInt32(lookUpEdit2.EditValue.ToString()));
                yeni_borc_calc();
                simpleButton1.Enabled = true;
                simpleButton6.Enabled = true;
                textBox1.Text = "";
            }
        }
        private void yeni_borc_calc()
        {
            decimal qaliqBorc = 0;
            decimal yeniBorc = 0;
            if (string.IsNullOrEmpty(textEdit11.Text))
            {
                qaliqBorc = 0;
            }
            else
            {
                qaliqBorc = Convert.ToDecimal(textEdit11.Text);
            }

            if (string.IsNullOrEmpty(textEdit12.Text))
            {
                yeniBorc = 0;
            }
            else
            {
                yeniBorc = Convert.ToDecimal(textEdit12.Text);
            }

            decimal yekunBorc = qaliqBorc + yeniBorc;
            if (yekunBorc > 0)
            {
                textEdit14.Text = yekunBorc.ToString();
            }
        }
        private void textEdit12_TextChanged_1(object sender, EventArgs e)
        {
            //yekun borc
            yeni_borc_calc();
        }


        public void kategoriya(string f)
        {
            textBox1.Text = f.ToString();
        }

        public void mehsul_kod_axtar(string mehsul_ad, string mehsul_kod, string alis_giymet, string satis_giymeti, string barkod, int vahid, int tax)
        {
            textEdit2.Text = mehsul_ad;
            textEdit1.Text = mehsul_kod;
            textEdit9.Text = alis_giymet;
            textEdit6.Text = satis_giymeti;
            textEdit3.Text = barkod;
            lookUpEdit5.EditValue = vahid;
            lookUpEdit6.EditValue = tax;
        }

        public void GetallData_null()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                string queryString = "SELECT * FROM dbo.fn_MAL_ALISI_LOAD('') ";

                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@pricepoint", emeliyyat_nomre);
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
                throw e;
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            clear();
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            FormHelpers.OpenForm<fReceivedProducts>();
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            //SIL


            if (string.IsNullOrEmpty(textEdit16.Text))
            {

            }
            else
            {
                int f = ms.deletetmal_ALL(textEdit16.Text);
            }
            //   GETKOD(); // HASAN
            clear();
            GetallData(textEdit16.Text);
            textEdit5.Text = "";
            lookUpEdit2.EditValue = null;
            textEdit12.Text = "";
            textEdit14.Text = "";
        }
        kategoriyalar kk;
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            //kategoriyalar k = new kategoriyalar(this);
            //k.Show();
            if (Application.OpenForms["kategoriyalar"] != null)
            {
                var Main = Application.OpenForms["ANBAR_GALIGI"] as kategoriyalar;
                if (Main != null)
                {

                }
                //    Main.Close();
            }
            else
            {
                kk = new kategoriyalar(this);
                kk.ShowDialog();

            }
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

        mehsul_adi_axtar k;
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            //mehsul_adi_axtar k = new mehsul_adi_axtar(Convert.ToInt32(lookUpEdit2.EditValue), this);
            //k.Show();
            if (Application.OpenForms["mehsul_adi_axtar"] != null)
            {
                var Main = Application.OpenForms["mehsul_adi_axtar"] as mehsul_adi_axtar;
                if (Main != null)
                {

                }
                // Main.Close();
            }
            else
            {
                k = new mehsul_adi_axtar(Convert.ToInt32(lookUpEdit2.EditValue), this);
                k.ShowDialog();
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            try
            {
                if ((lookUpEdit2.EditValue is null) || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textEdit2.Text)
                    || string.IsNullOrEmpty(textEdit1.Text)
                    || string.IsNullOrEmpty(textEdit8.Text)
                    || (lookUpEdit5.EditValue is null)
                    || (lookUpEdit4.EditValue is null)
                    || (lookUpEdit6.EditValue is null)
                    || string.IsNullOrEmpty(textEdit9.Text)
                    || string.IsNullOrEmpty(textEdit6.Text) || string.IsNullOrEmpty(radio))
                {
                    XtraMessageBox.Show("MƏLUMATLAR TAM DOLDURULMALIDIR");
                }
                else
                {
                    if (Convert.ToDecimal(textEdit8.Text) > 0 && Convert.ToDecimal(textEdit6.Text) > 0)
                    {
                        int x = DbProsedures.Exists_ProductCode(textEdit1.Text, Convert.ToInt32(lookUpEdit2.EditValue.ToString()));
                        if (x > 1)
                        {
                            int ret = ms.Insertmal(textEdit5.Text, lookUpEdit2.Text, Convert.ToDateTime(dateEdit1.Text), radio, textEdit16.Text, mal_user_id);

                            if (ret > 0)
                            {
                                int a = ms.Insertdetails(ret, textBox1.Text, textEdit3.Text, textEdit2.Text, textEdit1.Text,
                                    lookUpEdit3.Text, textEdit8.Text, lookUpEdit5.Text, lookUpEdit4.Text, lookUpEdit6.Text,
                                    textEdit9.Text, textEdit6.Text, textEdit7.Text, textEdit13.Text, textEdit10.Text,
                                    textEdit4.Text, dateEdit2.Text, dateEdit3.Text, spinEdit1.Text);

                                FormHelpers.Log($"{textEdit2.Text} adlı yeni bir məhsul yaradıldı");
                            }

                            clear();
                            GetallData(textEdit16.Text);
                        }
                        else
                        {
                            XtraMessageBox.Show("MƏHSUL KODU BAŞQA TƏCHİZATÇIDA VAR ");
                        }

                    }
                    else
                    {
                        XtraMessageBox.Show("MƏHSULUN MİQDARI VƏYA SATIŞ QİYMƏTİ 0 OLABİLMƏZ");
                    }




                    getyeni_mebleg(textEdit16.Text, Convert.ToInt32(lookUpEdit2.EditValue.ToString()));
                    lookupedit2_enabled();
                }
            }
            catch (Exception ECF)
            {
                XtraMessageBox.Show(ECF.Message);
            }
        }

        /// <summary>
        /// DÜZƏLİŞ ET
        /// </summary>
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            ms.updatedetails(update_mal_details_id, textBox1.Text, textEdit3.Text, textEdit2.Text, textEdit1.Text,
                   lookUpEdit3.Text, textEdit8.Text, lookUpEdit5.Text, lookUpEdit4.Text, lookUpEdit6.Text,
                   textEdit9.Text, textEdit6.Text, textEdit7.Text, textEdit13.Text, textEdit10.Text,
                   textEdit4.Text, dateEdit2.Text, dateEdit3.Text, spinEdit1.Text);
            GetallData(textEdit16.Text);

            getyeni_mebleg(textEdit16.Text, Convert.ToInt32(lookUpEdit2.EditValue.ToString()));
            lookupedit2_enabled();
        }

        /// <summary>
        /// SİLMƏ
        /// </summary>
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            //SILME

            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);

                int B = Convert.ToInt32(row[0].ToString());
                if (B > 0)
                {
                    int x = ms.deletetmal(B);
                }


            }
            GetallData(textEdit16.Text);
            clear();
            getyeni_mebleg(textEdit16.Text, Convert.ToInt32(lookUpEdit2.EditValue.ToString()));
            getsum(Convert.ToInt32(lookUpEdit2.EditValue));
            yeni_borc_calc();
            lookupedit2_enabled();
        }

        /// <summary>
        /// ANBARA YOLLA
        /// </summary>
        private void simpleButton10_Click(object sender, EventArgs e)
        {
            clear();
            textEdit5.Text = ""; //Faktura nömrə
            lookUpEdit2.EditValue = null; //təchizatçı
            textEdit12.Text = ""; //Yeni borc
            textEdit14.Text = ""; //Yekun borc
            textEdit11.Text = ""; // qalıq borc
            gridControl1.DataSource = null;
            lookUpEdit2.Enabled = true;
        }

        private void textEdit9_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //ok
            getmebleg(textEdit8.Text, textEdit9.Text, textEdit7.Text, textEdit13.Text);
        }

        private void textEdit7_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //ok
            textEdit13.Text = "0.00";
            getmebleg(textEdit8.Text, textEdit9.Text, textEdit7.Text, textEdit13.Text);
        }

        private void textEdit7_TextChanged_1(object sender, EventArgs e)
        {
            //ok
            textEdit13.Text = "0.00";
            getmebleg(textEdit8.Text, textEdit9.Text, textEdit7.Text, textEdit13.Text);
        }

        private void dateEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }
        private List<Account> datasource_;
        private void InitLookUpEdit_()
        {
            datasource_ = new List<Account>();
            Random random = new Random();
            datasource_.Add(new Account("- MƏRKƏZ ANBAR - (ƏSAS)") { ID = 4 });
            //  datasource.Add(new Account("S"){ ID = random.Next(100)});
            lookUpEdit3.Properties.DataSource = datasource_;
            lookUpEdit3.Properties.DisplayMember = "Name";
            lookUpEdit3.Properties.ValueMember = "ID";
        }
        private void MEHSUL_ALISI_LAYOUT_Shown(object sender, EventArgs e)
        {
            if (datasource_.Count == 1)
                lookUpEdit3.EditValue = datasource_[0].ID;
        }

        private void gridView1_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {


                int paramValue = Convert.ToInt32(dr[0]);
                update_mal_details_id = Convert.ToInt32(dr[0]);
                //XtraMessageBox.Show(paramValue.ToString());
                string queryString =
                    "SELECT D.MAL_ALISI_DETAILS_ID,K.KATEGORIYA,D.BARKOD,D.MEHSUL_ADI,D.MEHSUL_KODU,D.MIGDARI,V.VAHIDLER_NAME " +
                     " , VA.VALYUTALAR,VDS.EDV,D.ALIS_GIYMETI,D.SATIS_GIYMETI,D.ENDIRIM_FAIZ,D.ENDIRIM_AZN, " +
                     " D.ENDIRIM_MEBLEGI,D.YEKUN_MEBLEG,D.ISTEHSAL_TARIXI,ISNULL(D.BITIS_TARIXI, ''),ISNULL(D.XEBERDAR_ET, '') " +
                     " FROM MAL_ALISI_DETAILS D INNER JOIN KATEGORIYA K ON K.KATEGORIYA_ID = D.KATEGORIYA " +
                     " INNER JOIN COMPANY.WAREHOUSE CW ON CW.WAREHOUSE_ID = D.ANBAR_ID " +
                     " INNER JOIN VAHIDLER V ON V.VAHIDLER_ID = D.VAHID " +
                     "  INNER JOIN VALYUTALAR VA ON VA.VALYUTALAR_ID = D.VALYUTA " +
                     " INNER JOIN VERGI_DERECESI VDS ON VDS.EDV_ID = D.VERGI_DERECESI " +
                     " WHERE MAL_ALISI_DETAILS_ID = @pricePoint ";

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
                            textBox1.Text = reader[1].ToString();
                            textEdit3.Text = reader[2].ToString();
                            textEdit2.Text = reader[3].ToString();
                            textEdit1.Text = reader[4].ToString();
                            textEdit8.Text = reader[5].ToString();
                            lookUpEdit5.Text = reader[6].ToString();
                            lookUpEdit4.Text = reader[7].ToString();
                            lookUpEdit6.Text = reader[8].ToString();
                            textEdit9.Text = reader[9].ToString();
                            textEdit6.Text = reader[10].ToString();
                            textEdit7.Text = reader[11].ToString();
                            textEdit13.Text = reader[12].ToString();
                            textEdit10.Text = reader[13].ToString();
                            textEdit4.Text = reader[14].ToString();
                            dateEdit2.Text = reader[15].ToString();
                            dateEdit3.Text = reader[16].ToString();
                            spinEdit1.Text = reader[17].ToString();

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

        private void textEdit4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textEdit4.Text))
            {
                getmebleg_(textEdit4.Text, lookUpEdit6.Text);
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

                textEdit15.Text = dr["vergisiz"].ToString();
                textEdit17.Text = dr["vergi"].ToString();
            }
            connection.Close();
        }

        private void lookUpEdit6_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textEdit4.Text))
            {

            }
            else
            {
                getmebleg_(textEdit4.Text.ToString(), lookUpEdit6.Text.ToString());
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            Random rastgele = new Random();
            string ida = "";
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.SqlCon;
            conn.Open();
            string query = "SELECT MAX([MAL_ALISI_DETAILS_ID])+1 AS id  FROM  [MAL_ALISI_DETAILS]";// position column from position table

            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {


                string id = dr["id"].ToString();
                ida = id;

            }


            string a1 = "994";
            string a2 = "";
            string a3 = "";
            string barkoda1 = "";
            if (ida.Length == 1)
            {
                int sayi = rastgele.Next(1000, 9999);
                a2 = sayi.ToString();
            }
            else if (ida.Length == 2)
            {
                int sayi2 = rastgele.Next(100, 999);
                a2 = sayi2.ToString();
            }
            else if (ida.Length == 3)
            {
                int sayi3a = rastgele.Next(10, 999);
                a2 = sayi3a.ToString();
            }
            else if (ida.Length == 4)
            {
                int sayi4 = rastgele.Next(0, 9);
                a2 = sayi4.ToString();
            }

            int sayi3 = rastgele.Next(1000, 9999);
            a3 = sayi3.ToString();

            barkoda1 = a1 + ida + a2 + a3;

            string code = barkoda1;
            var reversed = code.Reverse().ToArray();
            var sum =
               (from i in Enumerable.Range(0, reversed.Count())
                let digit = (int)char.GetNumericValue(reversed[i])
                select digit * (i % 2 == 0 ? 3 : 1)).Sum();
            var kontrol1 = (10 - sum % 10) % 10;

            textEdit3.Text = barkoda1 + kontrol1.ToString();
        }
    }
}