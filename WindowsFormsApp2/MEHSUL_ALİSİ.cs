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

namespace WindowsFormsApp2
{
    public partial class MEHSUL_ALİSİ : DevExpress.XtraBars.Ribbon.RibbonForm
    {
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

        public MEHSUL_ALİSİ()
        {
            InitializeComponent();
            DevExpress.XtraEditors.Controls.Localizer.Active = new MsgBoxLocalizer();
        }

        private string qeryString = "EXEC  dbo.MAL_ALISI_EMELIYYAT_NOMRE";
        private string querytarix = " EXEC   dbo.mal_alisi_tarix";
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

                        textEdit16.Text = reader[0].ToString();


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
        private void MEHSUL_ALİSİ_Load(object sender, EventArgs e)
        {
            //textEdit9.Text = "0.00";
            simpleButton2.ForeColor = Color.Red;

            radioButton1.Checked = true;
            textEdit16.Enabled = false;
            GETKOD();
            //      GETTARIX();


            textEdit11.ForeColor = Color.Red;
            this.spinEdit1.Properties.MinValue = 0;
            this.spinEdit1.Properties.MaxValue = int.MaxValue;
            lookUpEdit3.Enabled = false;


            gridControl1.TabStop = false;
            tab();


            label1.Text = "0";

            lookupedittextxhange_main();
            //  ANBAR_main();
            //InitLookUpEdit();
            vahid_main();
            valyuta_main();
            VERGİ_main();
            Auto();



            DateTime dateTime = DateTime.UtcNow.Date;
            dateEdit1.Text = dateTime.ToString();



        }

        private void yen_clikc(object sender, EventArgs e)
        {
            MessageBox.Show("A");
            CL();
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
            string strQuery = "select TECHIZATCI_ID,SIRKET_ADI as N'ŞİRKƏT ADI' from COMPANY.TECHIZATCI";
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


        private void ANBAR_main()
        {
            //int id = Convert.ToInt32(lookUpEdit1.EditValue.ToString());

            //if (id > 0)
            //{
            string strQuery = "SELECT WAREHOUSE_ID,WAREHOUSE_NAME AS [Anbar] FROM COMPANY.WAREHOUSE  WHERE WAREHOUSE_NAME= N'- MƏRKƏZ ANBAR - (ƏSAS)'";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            lookUpEdit3.Properties.DisplayMember = "WAREHOUSE_NAME";
            lookUpEdit3.Properties.ValueMember = "WAREHOUSE_ID";
            lookUpEdit3.Properties.DataSource = dt;
            lookUpEdit3.Properties.NullText = dt.Columns[1].ToString();
            lookUpEdit3.Properties.PopulateColumns();
            lookUpEdit3.Properties.Columns[0].Visible = false;
            //}
        }
        private void vahid_main()
        {
            //int id = Convert.ToInt32(lookUpEdit1.EditValue.ToString());

            //if (id > 0)
            //{
            string strQuery = "select VAHIDLER_ID, VAHIDLER_NAME as N'VAHIDLƏR' from VAHIDLER";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            lookUpEdit5.Properties.DisplayMember = "VAHIDLƏR";
            lookUpEdit5.Properties.ValueMember = "VAHIDLER_ID";
            lookUpEdit5.Properties.DataSource = dt;
            lookUpEdit5.Properties.NullText = "VAHIDLƏR";
            lookUpEdit5.Properties.PopulateColumns();
            lookUpEdit5.Properties.Columns[0].Visible = false;
            //}
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
            lookUpEdit4.Properties.PopulateColumns();
            lookUpEdit4.Properties.Columns[0].Visible = false;
            //}
        }

        private void getyeni_mebleg(string emeliyyat_nomre, int t_id)
        {
            string queryString = " select cast(sum(isnull(d.ALIS_GIYMETI,0.00)*isnull(d.MIGDARI,0.00)) " +
                " as decimal(9,2)) as yeni_borc" +
                " from MAL_ALISI_MAIN m inner join MAL_ALISI_DETAILS d " +
                " on m.MAL_ALISI_MAIN_ID = d.MAL_ALISI_MAIN_ID " +
                " AND m.TECHIZATCI_ID=@pricePoint1 " +
                " where m.EMELIYYAT_NOMRE = @pricePoint";
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

        private void labelControl8_Click(object sender, EventArgs e)
        {

        }

        private void textEdit8_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl21_Click(object sender, EventArgs e)
        {

        }

        private void lookUpEdit4_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl25_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {



                if ((lookUpEdit2.EditValue is null) ||
                    string.IsNullOrEmpty(textBox1.Text) ||
                    string.IsNullOrEmpty(textEdit2.Text)
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
                    if (Convert.ToDecimal(textEdit8.Text.ToString()) > 0 && Convert.ToDecimal(textEdit9.Text.ToString()) > 0
                        && Convert.ToDecimal(textEdit6.Text.ToString()) > 0
                        )
                    {
                        //daxil etmek
                        int x = MSD.mehsul_kod_yoxl(textBox1.Text.ToString(), Convert.ToInt32(lookUpEdit2.EditValue.ToString()));
                        if (x > 1)
                        {
                            int ret = ms.Insertmal(textEdit5.Text.ToString(),
                          lookUpEdit2.Text.ToString(),
                                 Convert.ToDateTime(dateEdit1.Text.ToString()),
                                radio,
                                textEdit16.Text.ToString(), 1);

                            //   XtraMessageBox.Show(ret.ToString());
                            if (ret > 0)
                            {

                                int a = ms.Insertdetails(ret, textBox1.Text.ToString(), textEdit3.Text.ToString(), textEdit2.Text.ToString(), textEdit1.Text.ToString(),
                                    lookUpEdit3.Text.ToString(), textEdit8.Text.ToString(), lookUpEdit5.Text.ToString(), lookUpEdit4.Text.ToString(), lookUpEdit6.Text.ToString(),
                                    textEdit9.Text.ToString(), textEdit6.Text.ToString(), textEdit7.Text.ToString(), textEdit13.Text.ToString(), textEdit10.Text.ToString(),
                                    textEdit4.Text.ToString(), dateEdit2.Text.ToString(), dateEdit3.Text.ToString(), spinEdit1.Text.ToString());
                                //   XtraMessageBox.Show(a.ToString());

                                clear();
                                //if (a == 0)
                                //{
                                //    XtraMessageBox.Show("MƏHSUL ARTIQ DAXİL EDİLMİŞDİR");
                                //}
                                //else
                                //{

                                //}
                            }
                            clear();
                            GetallData(textEdit16.Text.ToString());

                            getyeni_mebleg(textEdit16.Text.ToString(), Convert.ToInt32(lookUpEdit2.EditValue.ToString()));
                            lookupedit2_enabled();
                        }
                        else
                        {
                            XtraMessageBox.Show("MIHSUL KODU BAŞQA TƏCHİZATÇIDA VAR ");
                        }

                    }
                    else
                    {
                        XtraMessageBox.Show("ALIS SATIS O DAN KICIN OLA BILMEZ");
                    }



                    //daxil etmek
                    //}
                    //else
                    //{
                    //    XtraMessageBox.Show("DAXİL ETDİYİNİZ RƏQƏM 0 DAN BOYÜK OLMALIDIR (MİQDAR,ALIŞ,SATIŞ)");
                    //}
                    clear();
                    getyeni_mebleg(textEdit16.Text.ToString(), Convert.ToInt32(lookUpEdit2.EditValue.ToString()));
                    lookupedit2_enabled();
                }
            }
            catch (Exception ECF)
            {
                XtraMessageBox.Show(ECF.Message.ToString());
            }
        }

        public void lookupedit2_enabled()
        {
            int x = MSD.count_mal_(textEdit16.Text.ToString());
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
                string queryString = "SELECT * FROM dbo.fn_MAL_ALISI_LOAD(@pricepoint) ";

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
        private void labelControl26_Click(object sender, EventArgs e)
        {

        }
        MEHSUL_ALISI MSD = new MEHSUL_ALISI();
        private NumberStyles culture;
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
        private void textEdit3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {

            }
            else
            {
                int A = MSD.COUNTKATEGORY(textBox1.Text.ToString());
                if (A == -1)
                {
                    DialogResult dialogResult = XtraMessageBox.Show("YENİ KATEGORİYA YARADILSIN ?", "XƏBƏRDALIQ", MessageBoxButtons.YesNo);
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

        private void textEdit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (
 string.IsNullOrEmpty(textBox1.Text))
            {

            }
            else
            {
                int A = MSD.COUNTKATEGORY(textBox1.Text.ToString());
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

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (
  string.IsNullOrEmpty(textBox1.Text))
            {

            }
            else
            {
                int A = MSD.COUNTKATEGORY(textBox1.Text.ToString());
                if (A == -1)
                {
                    DialogResult dialogResult = MessageBox.Show("YENİ KATEGORİYA YARADILSIN ?", "XƏBƏRDALIQ", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //do something
                        int f = MSD.InsertKATEGORY(textBox1.Text.ToString());
                        if (f > 0)
                        {
                            XtraMessageBox.Show("YENİ KATEQORİYA UĞURLA YARADILDI ");
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

        private void textEdit13_TextChanged(object sender, EventArgs e)
        {

            textEdit7.Text = "0.00";
            getmebleg(textEdit8.Text, textEdit9.Text.ToString(), textEdit7.Text, textEdit13.Text);
        }

        private void textEdit13_KeyPress(object sender, KeyPressEventArgs e)
        {

            textEdit7.Text = "0.00";
            getmebleg(textEdit8.Text, textEdit9.Text.ToString(), textEdit7.Text, textEdit13.Text);
        }

        private void textEdit7_KeyPress(object sender, KeyPressEventArgs e)
        {
            textEdit13.Text = "0.00";
            getmebleg(textEdit8.Text, textEdit9.Text.ToString(), textEdit7.Text, textEdit13.Text);
        }

        private void textEdit7_TextChanged(object sender, EventArgs e)
        {
            textEdit13.Text = "0.00";
            getmebleg(textEdit8.Text, textEdit9.Text.ToString(), textEdit7.Text, textEdit13.Text);
        }
        //public string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        public void getmebleg(string paramValue, string paramValue1, string paramValue2, string paramValue3)
        {

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

        private void textEdit8_KeyPress(object sender, KeyPressEventArgs e)
        {
            getmebleg(textEdit8.Text.ToString(), textEdit9.Text.ToString(), textEdit7.Text.ToString(), textEdit13.Text.ToString());
            //XtraMessageBox.Show(textEdit8.Text + " tx9: " + textEdit9.Text + " txt10: " + textEdit7.Text + " txt13 " + textEdit13.Text);
        }

        private void textEdit8_TextChanged(object sender, EventArgs e)
        {
            getmebleg(textEdit8.Text, textEdit9.Text.ToString(), textEdit7.Text.ToString(), textEdit13.Text.ToString());
            //XtraMessageBox.Show(textEdit8.Text + " tx9: " + textEdit9.Text + " txt10: " + textEdit7.Text + " txt13 " + textEdit13.Text);
        }

        private void textEdit9_KeyPress(object sender, KeyPressEventArgs e)
        {
            getmebleg(textEdit8.Text, textEdit9.Text.ToString(), textEdit7.Text.ToString(), textEdit13.Text.ToString());
        }

        private void textEdit9_TextChanged(object sender, EventArgs e)
        {
            getmebleg(textEdit8.Text.ToString(), textEdit9.Text.ToString(), textEdit7.Text.ToString(), textEdit13.Text.ToString());
        }

        private void labelControl28_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //negd
            radio = radioButton1.Text.ToString();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //nisye
            radio = radioButton2.Text.ToString();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //bank
            radio = radioButton3.Text.ToString();
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

        private void simpleButton5_Click(object sender, EventArgs e)
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
            GetallData(textEdit16.Text.ToString());
            clear();
            getyeni_mebleg(textEdit16.Text.ToString(), Convert.ToInt32(lookUpEdit2.EditValue.ToString()));
            getsum(Convert.ToInt32(lookUpEdit2.EditValue));
            yeni_borc_calc();
            lookupedit2_enabled();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void sdeyisdir_Click(object sender, EventArgs e)
        {
            int a =
                ms.updatedetails(update_mal_details_id, textBox1.Text.ToString(), textEdit3.Text.ToString(), textEdit2.Text.ToString(), textEdit1.Text.ToString(),
                    lookUpEdit3.Text.ToString(), textEdit8.Text.ToString(), lookUpEdit5.Text.ToString(), lookUpEdit4.Text.ToString(), lookUpEdit6.Text.ToString(),
                    textEdit9.Text.ToString(), textEdit6.Text.ToString(), textEdit7.Text.ToString(), textEdit13.Text.ToString(), textEdit10.Text.ToString(),
                    textEdit4.Text.ToString(), dateEdit2.Text.ToString(), dateEdit3.Text.ToString(), spinEdit1.Text.ToString());
            GetallData(textEdit16.Text.ToString());

            getyeni_mebleg(textEdit16.Text.ToString(), Convert.ToInt32(lookUpEdit2.EditValue.ToString()));
            lookupedit2_enabled();
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
        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
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

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
        
        }


        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            clear();
        }

        public void clear()
        {

            //  textEdit5.Text = "";
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
            //  dateEdit1.Text = "";
            dateEdit2.Text = "";
            dateEdit3.Text = "";
            spinEdit1.Text = "0";




        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        //private List<Account> datasource;
        //private void InitLookUpEdit()
        //{
        //    datasource = new List<Account>();
        //    Random random = new Random();
        //    datasource.Add(new Account("- MƏRKƏZ ANBAR - (ƏSAS)") { ID = 4 });
        //    //  datasource.Add(new Account("S"){ ID = random.Next(100)});
        //    lookUpEdit3.Properties.DataSource = datasource;
        //    lookUpEdit3.Properties.DisplayMember = "Name";
        //    lookUpEdit3.Properties.ValueMember = "ID";
        //}
        private void MEHSUL_ALİSİ_Shown(object sender, EventArgs e)
        {
            //if (datasource.Count == 1)
            //    lookUpEdit3.EditValue = datasource[0].ID;
        }

        public void getsum(int A)
        {
            int paramValue = A;


            string queryString = "SELECT Y.BORC - X.GAYTARMA_MEBLEG AS BORC FROM( " +
        " select 1 AS ID, cast(sum(isnull(BORC, 0.00)) as decimal(9, 2)) as BORC " +
                  " from(SELECT f.MAL_ALISI_MAIN_ID, f.[FAKTURA NÖMRƏ], " +
                   "   f.TARIX, f.QİYMƏT - isnull(t.odenis, 0.00) BORC, " +
                    "  0 AS 'ÖDƏNİŞ'FROM dbo.fn_TECHIZATCI_BORC(@pricePoint) f " +
                          "  left join(select  MAL_ALISI_MAIN_ID, " +
                       " sum(ODENIS) odenis from TECHIZATCI_ODENIS " +
                       " group by MAL_ALISI_MAIN_ID)t  on f.MAL_ALISI_MAIN_ID = t.MAL_ALISI_MAIN_ID)o " +
                       " )Y " +
                       " LEFT JOIN( " +
                       " SELECT 1 AS ID, ISNULL(CAST(SUM(MD.ALIS_GIYMETI * D.MIGDARI) AS decimal(9, 2)), 0.00) " +
                       " AS GAYTARMA_MEBLEG FROM MAL_GEYTARMA_MAIN M " +
                       " INNER JOIN  MAL_GEYTARMA_DETAILS D ON " +
                       " M.MAL_GEYTARMA_MAIN_ID = D.MAL_GEYTARMA_MAIN_ID " +
                       " INNER JOIN MAL_ALISI_DETAILS MD ON MD.MAL_ALISI_DETAILS_ID = D.MAL_ALISI_DETAILS_ID " +
                       " INNER JOIN MAL_ALISI_MAIN MM ON MM.MAL_ALISI_MAIN_ID = MD.MAL_ALISI_MAIN_ID " +
                       " WHERE MM.TECHIZATCI_ID = @pricePoint " +
                       "  )X ON X.ID = Y.ID ";
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.AddWithValue("@pricePoint", paramValue);

            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {

                textEdit11.Text = dr["BORC"].ToString();

            }
            connection.Close();


        }
        private void lookUpEdit2_TextChanged(object sender, EventArgs e)
        {
            //techiatci
            if (lookUpEdit2.EditValue != null)
            {


                getsum(Convert.ToInt32(lookUpEdit2.EditValue));
                getyeni_mebleg(textEdit16.Text.ToString(), Convert.ToInt32(lookUpEdit2.EditValue.ToString()));
                yeni_borc_calc();
                simpleButton1.Enabled = true;
            }
        }
        private void yeni_borc_calc()
        {
            decimal a = 0;
            decimal b = 0;
            if (string.IsNullOrEmpty(textEdit11.Text))
            {
                a = 0;
            }
            else
            {
                a = Convert.ToDecimal(textEdit11.Text);
            }

            if (string.IsNullOrEmpty(textEdit12.Text))
            {
                b = 0;
            }
            else
            {
                b = Convert.ToDecimal(textEdit12.Text);
            }

            decimal c = a + b;
            if (c > 0)
            {
                textEdit14.Text = c.ToString();
            }
        }
        private void textEdit12_TextChanged(object sender, EventArgs e)
        {
            //yekun borc
            yeni_borc_calc();
        }

        private void textEdit16_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl8_Click_1(object sender, EventArgs e)
        {

        }

        private void labelControl9_Click(object sender, EventArgs e)
        {

        }
        public void kategoriya(string f)
        {
            textBox1.Text = f.ToString();
        }

        public void mehsul_kod_axtar(string mehsul_ad, string mehsul_kod)
        {
            textEdit2.Text = mehsul_ad;
            textEdit1.Text = mehsul_kod;
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            //kategoriyalar k = new kategoriyalar(this);
            //k.Show();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //mehsul_adi_axtar k = new mehsul_adi_axtar(Convert.ToInt32(lookUpEdit2.EditValue), this);
            //k.Show();
        }

        private void MEHSUL_ALİSİ_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        protected override void OnFormClosing(System.Windows.Forms.FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            var result = XtraMessageBox.Show("SƏHİFƏDƏN ÇIXMAĞA ƏMİNSİNİZ ?", "XƏBƏRDARLIQ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            e.Cancel = result != DialogResult.Yes;
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SIL


            if (string.IsNullOrEmpty(textEdit16.Text))
            {

            }
            else
            {
                string x = textEdit16.Text.ToString();
                int f = ms.deletetmal_ALL(x);
            }
            GETKOD();
            clear();
            GetallData(textEdit16.Text.ToString());
            dateEdit1.Text = "";
            textEdit5.Text = "";
            lookUpEdit2.EditValue = null;
            textEdit12.Text = "";
            textEdit14.Text = "";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //   MessageBox.Show("a");
            clear();
            // GetallData(textEdit16.Text.ToString());
            dateEdit1.Text = "";
            textEdit5.Text = "";
            lookUpEdit2.EditValue = null;
            textEdit12.Text = "";
            textEdit14.Text = "";
            textEdit11.Text = "";
            GETKOD();
            GetallData_null();
            lookUpEdit2.Enabled = true;
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
                Console.WriteLine("Xəta!\n" + e);
            }
        }

        private void dateEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}