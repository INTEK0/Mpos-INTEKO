using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;

namespace WindowsFormsApp2
{
    public partial class bank_odenisleri : DevExpress.XtraEditors.XtraForm
    {
        public static int t_odenis_user_id;
        public bank_odenisleri(int t_user_id)
        {
            InitializeComponent();
            t_odenis_user_id = t_user_id;
        }
        techizatci_odenis t = new techizatci_odenis();
        private void bank_odenisleri_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit1.Text = dateTime.ToShortDateString();
            textEdit5.Enabled = false;
            // XtraMessageBox.Show(t_odenis_user_id.ToString());
            string a = t.emeliyyat_nomre();
            textEdit5.Text = a;
            lookUpEdit8GEtData_yeni_anbar();
            radioButton2.Checked = true;
            textEdit17.Enabled = false;

            //   dateEdit1.TabIndex = 1;
            textEdit5.TabIndex = 2;
            textEdit17.TabIndex = 3;
            lookUpEdit1.TabIndex = 4;
            memoEdit1.TabIndex = 5;
            gridControl1.TabStop = false;
        }
        private void lookUpEdit8GEtData_yeni_anbar()
        {
            string strQuery = @" SELECT distinct( c.TECHIZATCI_ID),c.SIRKET_ADI 
 AS N'TƏCHİZATÇI ADI' FROM COMPANY.TECHIZATCI c
 inner join MAL_ALISI_MAIN m on m.TECHIZATCI_ID = c.TECHIZATCI_ID 
 inner join (  select MAL_ALISI_MAIN_ID from ( 
 SELECT M.MAL_ALISI_MAIN_ID, M.FAKTURA_NOMRE AS N'FAKTURA NÖMRƏ',M.TARIX, 
 CAST(SUM(MD.ALIS_GIYMETI * MD.MIGDARI) AS DECIMAL(9, 2)) AS N'QİYMƏT' 
 FROM MAL_ALISI_MAIN M INNER JOIN MAL_ALISI_DETAILS MD 
 ON M.MAL_ALISI_MAIN_ID = MD.MAL_ALISI_MAIN_ID  
 INNER JOIN COMPANY.TECHIZATCI CT ON M.TECHIZATCI_ID = CT.TECHIZATCI_ID 
 GROUP BY FAKTURA_NOMRE,M.MAL_ALISI_MAIN_ID,TARIX )t ) x on x.MAL_ALISI_MAIN_ID = m.MAL_ALISI_MAIN_ID
 WHERE c.IsDeleted = 0";

          var data =  DbProsedures.ConvertToDataTable(strQuery);




            lookUpEdit1.Properties.DisplayMember = "TƏCHİZATÇI ADI";
            lookUpEdit1.Properties.ValueMember = "TECHIZATCI_ID";
            lookUpEdit1.Properties.DataSource = data;
            lookUpEdit1.Properties.NullText = "--Seçin--";
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns[0].Visible = false;

        }
       
        public void getsum(int A)
        {
            int paramValue = A;


            string queryString = "  select cast(sum(isnull(BORC,0.00)) as decimal(9, 2)) as BORC , " +
                " cast(sum(isnull(ESAS_BORC, 0.00)) as decimal(9, 2)) ESAS_BORC , " +
                 " cast(sum(isnull(EDV_BORC, 0.00)) as decimal(9, 2)) EDV_BORC from(SELECT f.MAL_ALISI_MAIN_ID, f.[FAKTURA NÖMRƏ] , " +
                   "   f.TARIX , f.QİYMƏT-isnull(t.odenis, 0.00) N'BORC', " +
                " isnull(f.ESAS_BORC,0.00) - isnull(t.ESAS_BORC_ODENIS,0.00) as ESAS_BORC,isnull(f.VERGI,0.00) - isnull(t.EDV_BORC,0.00) as EDV_BORC , " +
             "	0 AS 'ÖDƏNİŞ'FROM dbo.fn_TECHIZATCI_BORC(@pricePoint) f " +
                  "  left join(select  MAL_ALISI_MAIN_ID , " +
                  " (SUM(ISNULL(ESAS_BORC_ODENIS, 0.00)) + SUM(ISNULL(EDV_BORC, 0.00))) AS odenis , " +
                 " SUM(ISNULL(ESAS_BORC_ODENIS, 0.00))ESAS_BORC_ODENIS, SUM(ISNULL(EDV_BORC, 0.00))EDV_BORC " +
                   "   from TECHIZATCI_ODENIS group by MAL_ALISI_MAIN_ID)t on f.MAL_ALISI_MAIN_ID = t.MAL_ALISI_MAIN_ID )o ";

            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.AddWithValue("@pricePoint", paramValue);

            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {

                textEdit14.Text = dr["BORC"].ToString();
                textEdit2.Text = dr["ESAS_BORC"].ToString();
                textEdit1.Text = dr["EDV_BORC"].ToString();

            }
            connection.Close();


        }
        //public void getall_BANK(int A)
        //{
        //    int paramValue = A;
        //    try
        //    {
        //        SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


        //        string queryString = " SELECT * FROM [dbo].[fn_TECHIZATCI_BORC_EDV] (@pricePoint)";
        //        SqlCommand command = new SqlCommand(queryString, connection);
        //        command.Parameters.AddWithValue("@pricePoint", paramValue);
        //        SqlDataAdapter da = new SqlDataAdapter(command);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        gridControl1.DataSource = dt;
        //        gridView1.Columns[0].Visible = false; //MAL_ALISI_MAIN_ID

        //        gridView1.OptionsSelection.MultiSelect = true;
        //        gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Xəta!\n" + e);
        //    }

        //}

        public void getall(int A)
        {
            int paramValue = A;
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                string queryString = "  select MAL_ALISI_MAIN_ID, [FAKTURA NÖMRƏ] , TARIX  , " +
                 " cast(sum(isnull(ESAS_BORC, 0.00)) as decimal(9, 2)) N'ƏSAS BORC' , " +
                 " cast(sum(isnull(EDV_BORC, 0.00)) as decimal(9, 2))  N'ƏDV BORC' " +
                  " ,  cast(sum(isnull(BORC, 0.00)) as decimal(9, 2)) as N'YEKUN BORC' " +
                 " ,0.00 AS N'ƏDV ÖDƏ',0.00 AS N'YEKUN BORC ÖDƏ' from( " +
               " SELECT f.MAL_ALISI_MAIN_ID, f.[FAKTURA NÖMRƏ], f.TARIX, f.QİYMƏT - isnull(t.odenis, 0.00) N'BORC', " +
               " isnull(f.ESAS_BORC, 0.00) - isnull(t.ESAS_BORC_ODENIS, 0.00) as ESAS_BORC, isnull(f.VERGI, 0.00) -  " +
               " isnull(t.EDV_BORC, 0.00) as EDV_BORC, " +
               "  0 AS 'ÖDƏNİŞ' FROM dbo.fn_TECHIZATCI_BORC(@pricePoint) f  " +
               " left join(select  MAL_ALISI_MAIN_ID, (SUM(ISNULL(ESAS_BORC_ODENIS, 0.00)) + " +
               "  SUM(ISNULL(EDV_BORC, 0.00))) AS odenis, SUM(ISNULL(ESAS_BORC_ODENIS, 0.00))ESAS_BORC_ODENIS, " +
               " SUM(ISNULL(EDV_BORC, 0.00))EDV_BORC " +
               "  from TECHIZATCI_ODENIS group by MAL_ALISI_MAIN_ID)t on f.MAL_ALISI_MAIN_ID = t.MAL_ALISI_MAIN_ID)o where  BORC > 0.00 " +
               " group by MAL_ALISI_MAIN_ID, [FAKTURA NÖMRƏ] ,    TARIX ";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", paramValue);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false; //MAL_ALISI_MAIN_ID

                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }

        }

        private void lookUpEdit1_TextChanged(object sender, EventArgs e)
        {
            textEdit2.Text = "";
            textEdit1.Text = "";
            textEdit14.Text = "";
            getall(Convert.ToInt32(lookUpEdit1.EditValue));
            getsum(Convert.ToInt32(lookUpEdit1.EditValue));

        }
        public void refresh()
        {
            int a = Convert.ToInt32(lookUpEdit1.EditValue);
            if (a > 0)
            {
                getall(Convert.ToInt32(lookUpEdit1.EditValue));
                getsum(Convert.ToInt32(lookUpEdit1.EditValue));
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int conf = 0;

            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);
                string fak_nom;
                if (string.IsNullOrEmpty(textEdit17.Text))
                {
                    fak_nom = "-";
                }
                else
                {
                    fak_nom = textEdit17.Text;
                }

                decimal yekunborc = Convert.ToDecimal(row["YEKUN BORC ÖDƏ"].ToString());
                decimal edv = Convert.ToDecimal(row["ƏDV ÖDƏ"].ToString());

                decimal odenilen = yekunborc + edv;


                int a = t.INSERT_ODENIS(Convert.ToInt32(row[0].ToString()),
                    odenilen, 
                    radio, 
                    fak_nom, 
                    memoEdit1.Text,
                    Convert.ToDateTime(dateEdit1.Text),
                    textEdit5.Text,
                    row[1].ToString(),
                    t_odenis_user_id,
                    yekunborc,
                    edv);

                conf = conf + a;

            }

            if (conf > 0)
            {
                XtraMessageBox.Show("ÖDƏNİŞ UĞURLA TAMAMLANDI");
                textEdit5.Text = "";
                textEdit17.Text = "";
                string a = t.emeliyyat_nomre();
                textEdit5.Text = a;

            }
            getall(Convert.ToInt32(lookUpEdit1.EditValue));
            getsum(Convert.ToInt32(lookUpEdit1.EditValue));
            gridControl1.RefreshDataSource();
        }
        public Decimal confirmation_total()
        {
            Decimal a = 0;
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);

                a = (Decimal)(a + Convert.ToDecimal(row[4]));
            }
            return a;
        }
        public static string radio = "NAĞD";
        public static int r_int = 0;

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //BANK
            textEdit17.Enabled = true;
            radio = "BANK";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //NEGD
            textEdit17.Enabled = false;
            radio = "NAĞD";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.OpenForm<TECHIZATCI_ODENILENLER>(this);
            //TECHIZATCI_ODENILENLER TO = new TECHIZATCI_ODENILENLER(this);
            //TO.Show();
        }
    }
}