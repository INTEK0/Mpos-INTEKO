using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class TECHIZATCI_ODENILENLER : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public TECHIZATCI_ODENILENLER(bank_odenisleri frm)
        {
            InitializeComponent();
            frm1 = frm;
        }

        private readonly bank_odenisleri frm1;
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
            string strQuery = " select DISTINCT( M.TECHIZATCI_ID),CT.SIRKET_ADI AS N'TƏCHİZATÇI ADI' from TECHIZATCI_ODENIS T " +
                 " INNER JOIN MAL_ALISI_MAIN M ON T.MAL_ALISI_MAIN_ID=M.MAL_ALISI_MAIN_ID " +
                 " INNER JOIN COMPANY.TECHIZATCI CT ON CT.TECHIZATCI_ID=M.TECHIZATCI_ID ";
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
        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void TECHIZATCI_ODENILENLER_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();
            lookUpEdit8GEtData_yeni_anbar();

           // dateEdit1.TabIndex = 1;
            //dateEdit2.TabIndex = 2;
            lookUpEdit1.TabIndex = 3;
            simpleButton1.TabIndex = 4;
            gridControl1.TabStop = false;
        }



        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //GRID 
            if (string.IsNullOrEmpty(dateEdit1.Text) || string.IsNullOrEmpty(dateEdit2.Text) || (lookUpEdit1.EditValue is null))
            {
                XtraMessageBox.Show("XANALAR TAM OLARAQ DOLDURULMAYIB");
            }
            else
            {
                getall(Convert.ToInt32(lookUpEdit1.EditValue), Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
                getsum(Convert.ToInt32(lookUpEdit1.EditValue), Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
            }

        }
        //public string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";

        public void getsum(int A, DateTime d1, DateTime d2)
        {
            int paramValue = A;
            DateTime paramValue1 = d1;
            DateTime paramValue2 = d2;

            string queryString = " select * from  [dbo].[fn_TECHIZATCI_ODENILENLER_CEM] (@pricePoint,@pricePoint1 ,@pricePoint2)   ";
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.AddWithValue("@pricePoint", paramValue);
            command.Parameters.AddWithValue("@pricePoint1", paramValue1);
            command.Parameters.AddWithValue("@pricePoint2", paramValue2);

            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {

                textEdit14.Text = dr["CEM"].ToString();

            }
            connection.Close();


        }
        public void getall(int A,DateTime d1,DateTime d2)
        {
            int paramValue = A;
            DateTime paramValue1 = d1;
            DateTime paramValue2 = d2;
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                string queryString = "select * from  [dbo].[fn_TECHIZATCI_ODENILENLER] (@pricePoint,@pricePoint1 ,@pricePoint2)	";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", paramValue);
                command.Parameters.AddWithValue("@pricePoint1", paramValue1);
                command.Parameters.AddWithValue("@pricePoint2", paramValue2);
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
        techizatci_odenis t = new techizatci_odenis();
     
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SIL
          try
            {
                foreach (int i in gridView1.GetSelectedRows())
                {
                    DataRow row = gridView1.GetDataRow(i);
                    //MessageBox.Show(i.ToString());

                    //  int a = mg.InsertMalGaytarmaDetails(ret.ToString(), row[1].ToString(), row[8].ToString());
                    //decimal x = Convert.ToDecimal(row[4]);
                    //decimal y = Convert.ToDecimal(row[3]);


                    int a = t.DELETE_ODENIS(Convert.ToInt32(row[0]));

                    //conf = conf + a;


                    // XtraMessageBox.Show("ODƏNİLƏN MƏBLƏĞ BORCDAN ARTIQ OLA BİLMƏZ");


                }
                XtraMessageBox.Show("SƏHV ODƏNİLƏN MƏBLƏĞ GERİ QAYTARILDI");
                getall(Convert.ToInt32(lookUpEdit1.EditValue), Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
                getsum(Convert.ToInt32(lookUpEdit1.EditValue), Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
                dateEdit1.Text = "";
                dateEdit2.Text = "";
                lookUpEdit8GEtData_yeni_anbar();
            }
            catch
            {

            }
            
        }

        private void TECHIZATCI_ODENILENLER_FormClosing(object sender, FormClosingEventArgs e)
        {
            //formclosin refresh
            frm1.refresh();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string path = "output.xlsx";
                gridControl1.ExportToXlsx(path);
                // Open the created XLSX file with the default application. 
                Process.Start(path);
            }
            catch
            {
                MessageBox.Show("Fayl aciqdir");
            }
        }
    }
}