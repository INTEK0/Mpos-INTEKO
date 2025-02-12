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
    public partial class TECHIZATCI_ODENISI : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public static int t_odenis_user_id;
        public TECHIZATCI_ODENISI(int t_user_id)
        {
            InitializeComponent();
            t_odenis_user_id = t_user_id;
            

        }
       // public string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
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
        private void TECHIZATCI_ODENISI_Load(object sender, EventArgs e)
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
            //int id = Convert.ToInt32(lookUpEdit7.EditValue.ToString());



            //string strQuery = "SELECT STOREID,OBYEKT    FROM [dbo].[fn_MAGAZA_ANBAR_LOAD] ('')";
            string strQuery = " SELECT distinct( c.TECHIZATCI_ID),c.SIRKET_ADI " +
                              " AS N'TƏCHİZATÇI ADI' FROM COMPANY.TECHIZATCI c " +
                              " inner join MAL_ALISI_MAIN m on m.TECHIZATCI_ID = c.TECHIZATCI_ID " +
                              " inner join (  select MAL_ALISI_MAIN_ID from ( " +
                              " SELECT M.MAL_ALISI_MAIN_ID, M.FAKTURA_NOMRE AS N'FAKTURA NÖMRƏ',M.TARIX, " +
                              " CAST(SUM(MD.ALIS_GIYMETI * MD.MIGDARI) AS DECIMAL(9, 2)) AS N'QİYMƏT' " +
                              " FROM MAL_ALISI_MAIN M INNER JOIN MAL_ALISI_DETAILS MD " +
                              " ON M.MAL_ALISI_MAIN_ID = MD.MAL_ALISI_MAIN_ID  " +
                              " INNER JOIN COMPANY.TECHIZATCI CT ON M.TECHIZATCI_ID = CT.TECHIZATCI_ID " +
                              " GROUP BY FAKTURA_NOMRE,M.MAL_ALISI_MAIN_ID,TARIX )t ) x on x.MAL_ALISI_MAIN_ID = m.MAL_ALISI_MAIN_ID  ";
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
        public void getall_BANK(int A)
        {
            int paramValue = A;
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                string queryString = " SELECT * FROM [dbo].[fn_TECHIZATCI_BORC_EDV] (@pricePoint)";
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

        public void getall(int A)
        {
            int paramValue = A;
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                string queryString = " select * from ( SELECT f.MAL_ALISI_MAIN_ID,f.[FAKTURA NÖMRƏ] ,f.TARIX ," +
                    "f.QİYMƏT-isnull(t.odenis,0.00) N'BORC',0.00 AS 'ÖDƏNİŞ'FROM dbo.fn_TECHIZATCI_BORC(@pricePoint) f  " +
                    "left join  ( select  MAL_ALISI_MAIN_ID,  sum(ODENIS) odenis from TECHIZATCI_ODENIS group by MAL_ALISI_MAIN_ID )t  " +
                    "on f.MAL_ALISI_MAIN_ID=t.MAL_ALISI_MAIN_ID ) x where  BORC>0.00	";
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

        private void lookUpEdit1_TextChanged(object sender, EventArgs e)
        {



            //getall(Convert.ToInt32(lookUpEdit1.EditValue));
            //getsum(Convert.ToInt32(lookUpEdit1.EditValue));


            //string t = radio;
            //XtraMessageBox.Show(t);

            //int s = r_int;

            //if(r_int==1)
            //{
            //getall_BANK(Convert.ToInt32(lookUpEdit1.EditValue));
            //getsum(Convert.ToInt32(lookUpEdit1.EditValue));
            //}
            //else
            //{
            getall(Convert.ToInt32(lookUpEdit1.EditValue));
            getsum(Convert.ToInt32(lookUpEdit1.EditValue));
            //}

        }

        public  void refresh()
        {
            int a = Convert.ToInt32(lookUpEdit1.EditValue);
            if (a > 0)
            {
                getall(Convert.ToInt32(lookUpEdit1.EditValue));
                getsum(Convert.ToInt32(lookUpEdit1.EditValue));
            }
        }

        techizatci_odenis t = new techizatci_odenis();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int conf = 0;
            Decimal gh = confirmation_total();
            if (gh <= 0)
            {
                XtraMessageBox.Show("XƏBƏRDALIQ - ÖDƏNİŞ XANASI BOŞDUR");
            }
            else
            {
                if ( string.IsNullOrEmpty(dateEdit1.Text))
                {
                    XtraMessageBox.Show("XƏBƏRDALIQ:XANALAR TAM DOLDURULMALIDIR");
                }
                else
                {
                    foreach (int i in gridView1.GetSelectedRows())
                    {
                        DataRow row = gridView1.GetDataRow(i);
                        //MessageBox.Show(i.ToString());

                        //  int a = mg.InsertMalGaytarmaDetails(ret.ToString(), row[1].ToString(), row[8].ToString());
                        decimal x = Convert.ToDecimal(row[4]);
                        decimal y = Convert.ToDecimal(row[3]);
                        string fak_nom;
                        if (string.IsNullOrEmpty(textEdit17.Text))
                        {
                            fak_nom = "-";
                        }
                        else
                        {
                            fak_nom = textEdit17.Text.ToString();
                        }
                        if (x <= y)
                        {
                         //   XtraMessageBox.Show(row[4].ToString());
                            int a = t.INSERT_ODENIS(
                                Convert.ToInt32(row[0].ToString()), Convert.ToDecimal(row[4].ToString()), radio, fak_nom, memoEdit1.Text.ToString(),
                                Convert.ToDateTime(dateEdit1.Text), textEdit5.Text.ToString(), row[1].ToString(), t_odenis_user_id, Convert.ToDecimal(row[7]), Convert.ToDecimal(row[8]));

                            conf = conf + a;

                        }
                        else
                        {
                            XtraMessageBox.Show("ODƏNİLƏN MƏBLƏĞ BORCDAN ARTIQ OLA BİLMƏZ");
                        }

                    }
                }
               if (conf > 0)
                {
                    XtraMessageBox.Show("ÖDƏNİŞ UĞURLA TAMAMLANDI");
                 //   dateEdit2.Text = "";
                    textEdit5.Text = "";
                    textEdit17.Text = "";
                    string a = t.emeliyyat_nomre();
                    textEdit5.Text = a;

                }
                getall(Convert.ToInt32(lookUpEdit1.EditValue));
                getsum(Convert.ToInt32(lookUpEdit1.EditValue));
                gridControl1.DataSource = null;
            }
            
        }
        //public static int FGT = 0;
        public Decimal confirmation_total()
        {
            Decimal a=0;
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);
               
                a= (Decimal)(a + Convert.ToDecimal(row[4]));
          }
            return a;
        }
        public static string radio = "NAGD";
        public static int r_int = 0;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           
            //XtraMessageBox.Show(radio);
            //int a = Convert.ToInt32(lookUpEdit1.EditValue);
            //if (a > 0)
            //{
            //    XtraMessageBox.Show("a");
            //getall_BANK(Convert.ToInt32(lookUpEdit1.EditValue));
            ////     getsum(Convert.ToInt32(lookUpEdit1.EditValue));
            //}
            //r_int = 2;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {   

            //XtraMessageBox.Show(radio);
            //getall(Convert.ToInt32(lookUpEdit1.EditValue));
            //getsum(Convert.ToInt32(lookUpEdit1.EditValue));
            //r_int = 2;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            //getall_BANK(Convert.ToInt32(lookUpEdit1.EditValue));
            //    getsum(Convert.ToInt32(lookUpEdit1.EditValue));
            //r_int = 1;
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            //BANK
            textEdit17.Enabled = true;
            radio = "BANK";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //NEGD
            textEdit17.Enabled = false;
            radio = "NAGD";
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            //TECHIZATCI_ODENILENLER TO = new TECHIZATCI_ODENILENLER(this);
            //TO.Show();
        }

        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}