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
using WindowsFormsApp2.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2
{
    public partial class MUSTERILER_LAYOUT : DevExpress.XtraEditors.XtraForm
    {
        public MUSTERILER_LAYOUT()
        {
            InitializeComponent();
        }

        private void GETVETENDASLIQ()
        {

            //if (id > 0)
            //{
            string strQuery = "  select vetendaslig_id,vetendaslig as N'VƏTƏNDAŞLIQ' from vetendaslig ";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            lookUpEdit2.Properties.DisplayMember = "VƏTƏNDAŞLIQ";
            lookUpEdit2.Properties.ValueMember = "vetendaslig_id";
            lookUpEdit2.Properties.DataSource = dt;
            lookUpEdit2.Properties.NullText = "--Seçin--";
            lookUpEdit2.Properties.PopulateColumns();
            lookUpEdit2.Properties.Columns[0].Visible = false;
        }

        private void GETCINS()
        {
            string strQuery = "    select cins_id,cins AS N'CİNSİ' from cins";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            lookUpEdit1.Properties.DisplayMember = "CİNSİ";
            lookUpEdit1.Properties.ValueMember = "cins_id";
            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.NullText = "--Seçin--";
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns[0].Visible = false;
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





        private void CLEAR()
        {

            textEdit4.Text = "";
            textEdit5.Text = "";//finkod 
            textEdit3.Text = "";
            textEdit1.Text = "";//unvan
            dateEdit1.Text = "";
            dateEdit2.Text = "";
            dateEdit3.Text = "";
            textEdit16.Text = ""; //faktiki yasayis yeri 
            textEdit2.Text = "";//ad
            textEdit6.Text = "";//soyad
           /* textEdit7.Text = "";*/ //ata adi
            lookUpEdit1.Text = "";
            lookUpEdit2.Text = "";
            textEdit10.Text = "";//mobil
            textEdit11.Text = ""; //ev
            memoEdit1.Text = "";
            textEdit17.Text = "";
          
            textEdit7.Text = "";
            textEdit20.Text = "";
            textEdit22.Text = "";
            textEdit18.Text = "";
            textEdit21.Text = "";
            textEdit23.Text = "";
        }

        private void MUSTERILER_LAYOUT_Load_1(object sender, EventArgs e)
        {
            textEdit17.Text = DbProsedures.GET_CustomerProccessNo();
            GETCINS();
            GETVETENDASLIQ();
            DateTime dateTime = DateTime.UtcNow.Date;
            dateEdit4.Text = dateTime.ToString();
        }

        private void lookUpEdit1_TextChanged_1(object sender, EventArgs e)
        {
            GETCINS();
        }

        private void lookUpEdit2_TextChanged_1(object sender, EventArgs e)
        {
            GETVETENDASLIQ();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            //FormHelpers.OpenForm<fCustomers>();
        }
    }
}