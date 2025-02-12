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

namespace WindowsFormsApp2
{
    public partial class BANK_ODENİS : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public BANK_ODENİS()
        {
            InitializeComponent();
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
        private void lookUpEdit8GEtData_yeni_anbar()
        {
            //int id = Convert.ToInt32(lookUpEdit7.EditValue.ToString());



            //string strQuery = "SELECT STOREID,OBYEKT    FROM [dbo].[fn_MAGAZA_ANBAR_LOAD] ('')";
            string strQuery = "SELECT TECHIZATCI_ID,SIRKET_ADI AS N'TƏCHİZATÇI ADI' FROM COMPANY.TECHIZATCI ";
            SqlCommand cmd = new SqlCommand(strQuery);

            //cmd.Parameters.AddWithValue("@IDD", a);

            DataTable dt = GetData(cmd);




            lookUpEdit1.Properties.DisplayMember = "TƏCHİZATÇI ADI";
            lookUpEdit1.Properties.ValueMember = "TECHIZATCI_ID";
            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.NullText = "--Seçin--";
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns[0].Visible = false;

        }

        private void textEdit17_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl20_Click(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        public void getbank()
        {
            string strQuery = "select BANK_ID,BANK_NAME as 'BANK' from BANK ";
            SqlCommand cmd = new SqlCommand(strQuery);

            //cmd.Parameters.AddWithValue("@IDD", a);

            DataTable dt = GetData(cmd);




            lookUpEdit3.Properties.DisplayMember = "BANK";
            lookUpEdit3.Properties.ValueMember = "BANK_ID";
            lookUpEdit3.Properties.DataSource = dt;
            lookUpEdit3.Properties.NullText = "--Seçin--";
            lookUpEdit3.Properties.PopulateColumns();
            lookUpEdit3.Properties.Columns[0].Visible = false;
        }
        private void BANK_ODENİS_Load(object sender, EventArgs e)
        {
            //XtraMessageBox.Show("Sistemdə Bankla İnteqrasiya Mövcud deyildir");
            lookUpEdit8GEtData_yeni_anbar();
            getbank();
            gridControl1.TabStop = false;
        }
       // public string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        public void getall_BANK(int A)
        {
            int paramValue = A;
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                string queryString = " 	SELECT f.MAL_ALISI_MAIN_ID,f.TARIX,f.FAKTURA_NOMRE AS N'FAKTURA NÖMRƏ'," +
                                     "f.ESAS_MEBLEG - isnull(t.odenis, 0.00) AS N'ƏSAS MƏBLƏĞ', f.EDV AS N'ƏDV',f.BORC - isnull(t.odenis, 0.00) AS N'YEKUN BORC'," +
                                     "f.ODENILEN_MEBLEG AS N'ÖDƏNİLƏN MƏBLƏĞ',f.ODENILEN_EDV AS N'ÖDƏNİLƏN ƏDV'" +
                                     "FROM[dbo].[fn_TECHIZATCI_BORC_EDV](2)  f" +
                                    " LEFT JOIN   (select  MAL_ALISI_MAIN_ID,sum(ODENIS) odenis from TECHIZATCI_ODENIS " +
                                     " group by MAL_ALISI_MAIN_ID )  t  on f.MAL_ALISI_MAIN_ID = t.MAL_ALISI_MAIN_ID";
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
                Console.WriteLine("Xəta!\n" + e);
            }

        }

        public void getsum(int A)
        {
            int paramValue = A;


            string queryString = "  select cast(sum(isnull(BORC,0.00)) as decimal(9,2)) as BORC from ( SELECT f.MAL_ALISI_MAIN_ID,f.[FAKTURA NÖMRƏ] ," +
                    "f.TARIX ,f.QİYMƏT-isnull(t.odenis,0.00) N'BORC',0 AS 'ÖDƏNİŞ'FROM dbo.fn_TECHIZATCI_BORC(@pricePoint) f  " +
                    "left join  ( select  MAL_ALISI_MAIN_ID, " +
                    "  sum(ODENIS) odenis from TECHIZATCI_ODENIS group by MAL_ALISI_MAIN_ID )t  on f.MAL_ALISI_MAIN_ID=t.MAL_ALISI_MAIN_ID )o";
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.AddWithValue("@pricePoint", paramValue);

            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {

                textEdit14.Text = dr["BORC"].ToString();

            }
            connection.Close();


        }
        private void lookUpEdit1_TextChanged(object sender, EventArgs e)
        {
            //textchanged
            getall_BANK(Convert.ToInt32(lookUpEdit1.EditValue));
            getsum(Convert.ToInt32(lookUpEdit1.EditValue));
          //  getsum(Convert.ToInt32(lookUpEdit1.EditValue));
        }
    }
}