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

namespace WindowsFormsApp2
{
    public partial class TECIZATCI_LAYOUT : DevExpress.XtraEditors.XtraForm
    {
        CrudTechizatci ct = new CrudTechizatci();
        public int grup_edit_value = 0;
        public TECIZATCI_LAYOUT()
        {
            InitializeComponent();
        }
        private string qeryString2 = "select TECHIZATCI_NOMRE=case when (TECHIZATCI_NOMRE is null or TECHIZATCI_NOMRE='' ) " +
          "then 'T-1' else 'T-'+ RTRIM( LTRIM(CAST(MAX(CAST(REPLACE(TECHIZATCI_NOMRE,'T-','')  AS INT)+1) AS NCHAR(10)))) end " +
          "from COMPANY.TECHIZATCI group by TECHIZATCI_NOMRE";

        private string qeryString = "   EXEC dbo.TECHIZATCI_NOMRE";
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

                        textEdit14.Text = reader[0].ToString();


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

        private void TECIZATCI_LAYOUT_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            dateEdit1.Text = dateTime.ToString("dd/MM/yyyy");
            layoutControlItem26.Enabled = false;
            GETKOD();
            lookupedittextxhange_main();
            label1.Text = "0";

            textEdit14.Enabled = false;
            //textEdit14.TabIndex = 1;
            textEdit15.TabIndex = 1;
            buttonEdit1.TabIndex = 2;
            textEdit1.TabIndex = 3;
            textEdit13.TabIndex = 4;
          
            textEdit3.TabIndex = 5;
            //textEdit16.TabIndex = 7;
            textEdit4.TabIndex = 6;
            textEdit5.TabIndex = 7;
            textEdit6.TabIndex = 8;
            lookUpEdit1.TabIndex = 9;
            memoEdit1.TabIndex = 10;
            textEdit7.TabIndex = 11;
            textEdit8.TabIndex = 12;
            textEdit9.TabIndex = 13;
            textEdit10.TabIndex = 14;
            textEdit11.TabIndex = 15;
            textEdit12.TabIndex = 16;
            textEdit2.TabIndex = 17;



        }
        public void CLEAR()
        {
            dateEdit1.Text = "";
            textEdit14.Text = "";            
            textEdit15.Text = "";
            buttonEdit1.Text = "";
            textEdit1.Text = "";
            textEdit13.Text = "";
            textEdit3.Text = "";            
            textEdit4.Text = "";
            textEdit5.Text = "";
            textEdit6.Text = "";
            //lookUpEdit1.TabIndex = 9;
            memoEdit1.Text = "";
            textEdit7.Text = "";
            textEdit8.Text = "";
            textEdit9.Text = "";
            textEdit10.Text = "";
            textEdit11.Text = "";
            textEdit12.Text = "";
            textEdit2.Text = "";
            textEdit16.Text = "";

            GETKOD();
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

        private void lookupedittextxhange_main()
        {
            //int id = Convert.ToInt32(lookUpEdit1.EditValue.ToString());

            //if (id > 0)
            //{



            string strQuery = "select * from BORC_TEYINATI";

            SqlCommand cmd = new SqlCommand(strQuery);



            DataTable dt = GetData(cmd);




            lookUpEdit1.Properties.DisplayMember = "BORC_TEYINATI";
            lookUpEdit1.Properties.ValueMember = "BORC_TEYINATI_ID";
            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.NullText = "BORC TƏYİNATINI SEÇİN";
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns[0].Visible = false;


            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            if (
    string.IsNullOrEmpty(dateEdit1.Text)
   || string.IsNullOrEmpty(textEdit1.Text) ||
    string.IsNullOrEmpty(textEdit3.Text) //|| string.IsNullOrEmpty(textEdit12.Text)  

    )
            {
                XtraMessageBox.Show("MƏLUMATLAR TAM OLARAQ DOLDURULMALIDIR");
            }
            else
            {
                if (Convert.ToInt32(label1.Text) > 0)
                {
                    //update();
                }
                else
                {
                    Insert();
                }
            }
        }
        private void Insert()
        {
            string TECHIZATCI_NOMRE = textEdit14.Text.ToString();
            string MUGAVİLE_NOM = textEdit15.Text.ToString();
            string SIRKET_ADI = textEdit1.Text.ToString();
            string TECHIZATCI_VOEN = textEdit13.Text.ToString();
            string UNVAN = textEdit3.Text.ToString();
            string ELAGE_NOMRE = textEdit4.Text.ToString();
            string ELEKTRON_POCT = textEdit6.Text.ToString();
            decimal ILKIN_BORC;
            int SAHIBKAR_TECHIZATCI;

            string HESAB_AD = textEdit7.Text.ToString();
            string BANK_ADI = textEdit8.Text.ToString();
            string BANK_VOEN = textEdit9.Text.ToString();
            string KOD = textEdit10.Text.ToString();
         
            string MH = textEdit11.Text.ToString();

            string SWIFT = textEdit12.Text.ToString();

            string MESUL_SEXS = textEdit2.Text.ToString();

            string DESCRIPTION = memoEdit1.Text.ToString();

            string ELAGE_NOM3 = "";  //textEdit16.Text.ToString();
            string ELAGE_NOM2 = textEdit5.Text.ToString();
            int aa = 0;
            if (string.IsNullOrEmpty(textEdit16.Text))
            {
                ILKIN_BORC = 0;
            }
            else
            {
                ILKIN_BORC = Convert.ToDecimal(textEdit16.Text.ToString());
            }


            if (lookUpEdit1.Text == "BORC TƏYİNATINI SEÇİN")
            {
                SAHIBKAR_TECHIZATCI = 0;
            }
            else
            {
                SAHIBKAR_TECHIZATCI = Convert.ToInt32(lookUpEdit1.EditValue.ToString());
            }
            int a = ct.InsertTechizatci(TECHIZATCI_NOMRE, MUGAVİLE_NOM, SIRKET_ADI, UNVAN, ELAGE_NOMRE, ELEKTRON_POCT, ILKIN_BORC, SAHIBKAR_TECHIZATCI,
                TECHIZATCI_VOEN, HESAB_AD, BANK_ADI, BANK_VOEN, KOD, MH, SWIFT, MESUL_SEXS, DESCRIPTION, ELAGE_NOM2, ELAGE_NOM3);
            if (a == 0)
            {
                XtraMessageBox.Show("ƏLAVƏ ETMƏK İSTƏDİYNİZ TƏCHİZATÇI ADI ARTIQ MÖVCUDDUR");


                //clear();

            }
            if (a == 1)
            {
                XtraMessageBox.Show("MƏLUMATLARINIZ UĞURLA ƏLAVƏ EDİLDİ");

                //clear();
                CLEAR();
            }
            else
            {
                //   XtraMessageBox.Show("Ugrusuz");
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            SearchTechizatci_LAYOUT fr = new SearchTechizatci_LAYOUT();
            if (Application.OpenForms["SearchTechizatci_LAYOUT"] != null)
            {
                if (fr.WindowState == FormWindowState.Minimized)
                {
                    fr.WindowState = FormWindowState.Normal;
                }
                var Main = Application.OpenForms["SearchTechizatci_LAYOUT"] as SearchTechizatci_LAYOUT;
                if (Main != null)
                {

                }
                // Main.Close();
            }
            else
            {
                fr = new SearchTechizatci_LAYOUT();
                fr.Show();
            }
           // fr.Show();
            //SearchTechizatci fr = new SearchTechizatci();
            ////fr.MdiParent = this;
            //fr.Show();
        }
    }
}