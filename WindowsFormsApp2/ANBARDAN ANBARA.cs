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
    public partial class ANBARDAN_ANBARA : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //public string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        public ANBARDAN_ANBARA()
        {
            InitializeComponent();
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

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
        private void lookUpEdit8GEtData_yeni()
        {
            //int id = Convert.ToInt32(lookUpEdit7.EditValue.ToString());

            

                string strQuery = "select WAREHOUSE_ID,WAREHOUSE_NAME as 'Anbar' from COMPANY.WAREHOUSE";
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


            lookUpEdit2.Properties.DisplayMember = "Anbar";
            lookUpEdit2.Properties.ValueMember = "WAREHOUSE_ID";
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

        private void ANBARDAN_ANBARA_Load(object sender, EventArgs e)
        {
            lookUpEdit8GEtData_yeni();
            gridControl1.TabStop = false;
            GETKOD();
            DateTime dateTime = DateTime.UtcNow.Date;
            dateEdit1.Text = dateTime.ToString();
        }

      
        private string qeryString = "EXEC  [dbo].[ANBAR_TRANSFER_EMELIYYAT_NOMRE] ";
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

                        textEdit1.Text = reader[0].ToString();
                        textEdit1.Enabled = false;

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
            //GRID 
          
            getall(Convert.ToInt32(lookUpEdit1.EditValue.ToString()));
            getlookupiki(Convert.ToInt32(lookUpEdit1.EditValue.ToString()));

        }

        CRUD_ANBARDAN_ANBARA ca = new CRUD_ANBARDAN_ANBARA();
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            int ANBAR_1 = Convert.ToInt32(lookUpEdit1.EditValue);
            int anbar_2 = Convert.ToInt32(lookUpEdit2.EditValue);

            decimal C;
            decimal B;


            //anbar transfer 
            //XtraMessageBox.Show("ugurlu");
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);
                //MessageBox.Show(i.ToString());
                //XtraMessageBox.Show(row[8].ToString());
                C = Convert.ToDecimal(row[7].ToString());
                B = Convert.ToDecimal(row[8].ToString());
            //    XtraMessageBox.Show(B.ToString());

   
              
                //if (C >= B)
                //{
                    int a = ca.crud_ANBAR_TRANSFER(Convert.ToInt32(row[0]), ANBAR_1, anbar_2, Convert.ToDateTime(dateEdit1.Text), textEdit1.Text.ToString(), Convert.ToDecimal(row[8].ToString()));
                    XtraMessageBox.Show("ugurlu");
                //}
                //else
                //{
                //    XtraMessageBox.Show("GÖNDƏRİLƏN MİQDAR QALIQDAN ÇOX OLA BİLMƏZ");
                //}
            }
         //   XtraMessageBox.Show("ugurlu");
            getall(Convert.ToInt32(lookUpEdit1.EditValue.ToString()));
            //getall();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            ANBARDAN_ANBARA_BAXIS AB = new ANBARDAN_ANBARA_BAXIS();
            AB.Show();
        }
    }
}