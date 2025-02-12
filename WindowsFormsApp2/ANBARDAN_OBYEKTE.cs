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
    public partial class ANBARDAN_OBYEKTE : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //public string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        public ANBARDAN_OBYEKTE()
        {
            InitializeComponent();
        }

        private void ANBARDAN_OBYEKTE_Load(object sender, EventArgs e)
        {
            lookUpEdit8GEtData_yeni();
            //gridView1.Columns[0].Visible = false;
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridControl1.TabStop = false;
        }
        private void lookUpEdit8GEtData_yeni()
        {
            //int id = Convert.ToInt32(lookUpEdit7.EditValue.ToString());
            string strQuery = "select WAREHOUSE_ID,WAREHOUSE_NAME as 'Anbar' from COMPANY.WAREHOUSE  WHERE WAREHOUSE_NAME !=N'- MƏRKƏZ ANBAR - (ƏSAS)'";
            //string strQuery = "select GRUPLAR_ID No,GRUP as N'Qrup' " +
            //   " From GRUPLAR  where IXTISAS_ID=@IDD";

            SqlCommand cmd = new SqlCommand(strQuery);

            //cmd.Parameters.AddWithValue("@IDD", a);

            DataTable dt = GetData(cmd);




            lookUpEdit1.Properties.DisplayMember = "Anbar";
            lookUpEdit1.Properties.ValueMember = "WAREHOUSE_ID";
            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.NullText = "--Seçin--";
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns[0].Visible = false;

            //lookUpEdit2.Properties.DisplayMember = "Anbar";
            //lookUpEdit2.Properties.ValueMember = "WAREHOUSE_ID";
            //lookUpEdit2.Properties.DataSource = dt;
            //lookUpEdit2.Properties.NullText = "--Seçin--";
            //lookUpEdit2.Properties.PopulateColumns();
            //lookUpEdit2.Properties.Columns[0].Visible = false;
        }

        private void lookUpEdit8GEtData_yeni2(int a)
        {
            //int id = Convert.ToInt32(lookUpEdit7.EditValue.ToString());
            string strQuery = " SELECT CT.STOREID,CT.STORE_NAME AS N'MAGAZA' FROM COMPANY.WAREHOUSE CW" +
                " INNER JOIN COMPANY.STORE_WAREHOUSE CS " +
                "ON CW.WAREHOUSE_ID=CS.WAREHOUSE_ID INNER JOIN" +
                " COMPANY.STORE CT ON CT.STOREID=CS.STORE_ID " +
                "WHERE CW.WAREHOUSE_ID=@IDD";
            //string strQuery = "select GRUPLAR_ID No,GRUP as N'Qrup' " +
            //   " From GRUPLAR  where IXTISAS_ID=@IDD";

            SqlCommand cmd = new SqlCommand(strQuery);

            cmd.Parameters.AddWithValue("@IDD", a);

            DataTable dt = GetData(cmd);





            lookUpEdit2.Properties.DisplayMember = "MAGAZA";
            lookUpEdit2.Properties.ValueMember = "STOREID";
            lookUpEdit2.Properties.DataSource = dt;
            lookUpEdit2.Properties.NullText = "--Seçin--";
            lookUpEdit2.Properties.PopulateColumns();
            lookUpEdit2.Properties.Columns[0].Visible = false;




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

        private void getlookupiki(int t)
        {
            //int id = Convert.ToInt32(lookUpEdit7.EditValue.ToString());



            string strQuery = "select WAREHOUSE_ID,WAREHOUSE_NAME as 'Anbar' from [dbo].[fn_anbardan_anbara_load_ikinci] (@pricePoint)";
            //string strQuery = "select GRUPLAR_ID No,GRUP as N'Qrup' " +
            //   " From GRUPLAR  where IXTISAS_ID=@IDD";

            SqlCommand cmd = new SqlCommand(strQuery);

            cmd.Parameters.AddWithValue("@pricePoint", t);

            DataTable dt = GetData(cmd);

            lookUpEdit2.Properties.DisplayMember = "Anbar";
            lookUpEdit2.Properties.ValueMember = "WAREHOUSE_ID";
            lookUpEdit2.Properties.DataSource = dt;
            lookUpEdit2.Properties.NullText = "--Seçin--";
            lookUpEdit2.Properties.PopulateColumns();
            lookUpEdit2.Properties.Columns[0].Visible = false;




        }
        public void getall(int A)
        {
            int paramValue = A;
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

                // Provide the query string with a parameter placeholder.
                //string queryString = " select MAL_ALISI_DETAILS_ID,[ANBAR], [TƏCHİZATÇI ADI], " +
                //    "[MƏHSUL KODU],[MƏHSUL ADI],ALIS_GIYMETI,SATIS_GIYMETI" +
                //    " ,[MİQDARI] AS 'QALIQ MİQDARI',0 'GÖNDƏRİLƏN MİQDAR' from [dbo].[anbar_deyisme] WHERE ANBAR=@pricePoint ";
                string queryString = " select * from [dbo].[fn_anbardan_anbara] (@pricePoint)";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", paramValue);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[9].Visible = false;
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
            getall(Convert.ToInt32(lookUpEdit1.EditValue.ToString()));
         //   getlookupiki(Convert.ToInt32(lookUpEdit1.EditValue.ToString()));



            ////MessageBox.Show(lookUpEdit1.Text.ToString());
            //getall(lookUpEdit1.Text.ToString());
            int c = Convert.ToInt32(lookUpEdit1.EditValue.ToString());
            if (c > 0)
            {
                lookUpEdit8GEtData_yeni2(c);
            }

        }

        private void lookUpEdit1_Validated(object sender, EventArgs e)
        {
            //LOAD

            getall(lookUpEdit1.Text.ToString());
        }
        public void getall(string A)
        {
            string paramValue = A;
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

                // Provide the query string with a parameter placeholder.
                //string queryString = " select MAL_ALISI_DETAILS_ID,[ANBAR], [TƏCHİZATÇI ADI], " +
                //    "[MƏHSUL KODU],[MƏHSUL ADI],ALIS_GIYMETI,SATIS_GIYMETI" +
                //    " ,[MİQDARI] AS 'QALIQ MİQDARI',0 'GÖNDƏRİLƏN MİQDAR' from [dbo].[anbar_deyisme] WHERE ANBAR=@pricePoint ";
                string queryString = "select fa.MAL_ALISI_DETAILS_ID,fa.WAREHOUSE_NAME,fa.SIRKET_ADI, "+
 " fa.MEHSUL_KODU,fa.MEHSUL_ADI,fa.ALIS_GIYMETI,fa.SATIS_GIYMETI,fa.MIGDARI - isnull(opl.migdar, 0.00) as MIGDARI, " +
 " fa.GONDERILECEK_MIGDAR from[dbo].[fn_anbardan_anbara] (@pricePoint)fa " +
 " left join ( select WAREHOUSE_NAME, mal_details_id, sum(migdar) migdar from ( " +
 " select am.ANBAR_MAGAZA_ID, cw.WAREHOUSE_NAME, am.MAGAZA_ID, am.TARIX, am.EMELIYYAT_NOMRE, am.mal_details_id, am.migdar " +
  " from ANBAR_MAGAZA am inner join COMPANY.WAREHOUSE cw on cw.WAREHOUSE_ID= am.ANBAR_ID " + 
  " )x group by WAREHOUSE_NAME, mal_details_id ) opl on fa.MAL_ALISI_DETAILS_ID = opl.mal_details_id and " +
  " fa.WAREHOUSE_NAME = opl.WAREHOUSE_NAME ";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", paramValue);
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
        anbarobyektcrud sc = new anbarobyektcrud();
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            int ANBAR = Convert.ToInt32(lookUpEdit1.EditValue);
            int magaza = Convert.ToInt32(lookUpEdit2.EditValue);

            Decimal B;

            //anbar transfer 
            //XtraMessageBox.Show("ugurlu");
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);
                //MessageBox.Show(i.ToString());
                XtraMessageBox.Show(row[8].ToString());
                B = Convert.ToDecimal(row[8].ToString());
                int id = Convert.ToInt32(row[0].ToString());
                int a = sc.INSERT_ANBAR_MAGAZA
                    (ANBAR, magaza,
                    Convert.ToDateTime(dateEdit1.Text), textEdit1.Text.ToString(),id,B);
            }
            //XtraMessageBox.Show("ugurlu");
            //getall(lookUpEdit1.Text.ToString());
            getall(Convert.ToInt32(lookUpEdit1.EditValue.ToString()));
            //getall();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Anbarda_obyekte_gonderilenler a = new Anbarda_obyekte_gonderilenler();
            a.Show();
        }
    }
}