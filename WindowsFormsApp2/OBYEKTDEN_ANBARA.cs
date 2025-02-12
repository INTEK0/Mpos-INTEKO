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
    public partial class OBYEKTDEN_ANBARA : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public OBYEKTDEN_ANBARA()
        {
            InitializeComponent();
        }
        private void lookUpEdit8GEtData_yeni()
        {
            //int id = Convert.ToInt32(lookUpEdit7.EditValue.ToString());


             
            string strQuery = "SELECT STOREID,OBYEKT    FROM [dbo].[fn_MAGAZA_ANBAR_LOAD] ('')";
            //string strQuery = "select GRUPLAR_ID No,GRUP as N'Qrup' " +
            //   " From GRUPLAR  where IXTISAS_ID=@IDD";

            SqlCommand cmd = new SqlCommand(strQuery);

            //cmd.Parameters.AddWithValue("@IDD", a);

            DataTable dt = GetData(cmd);




            lookUpEdit1.Properties.DisplayMember = "OBYEKT";
            lookUpEdit1.Properties.ValueMember = "STOREID";
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

        private void OBYEKTDEN_ANBARA_Load(object sender, EventArgs e)
        {
            lookUpEdit8GEtData_yeni();
        }
        //public string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";

        private void lookUpEdit8GEtData_yeni_anbar(int a)
        {
            //int id = Convert.ToInt32(lookUpEdit7.EditValue.ToString());



            //string strQuery = "SELECT STOREID,OBYEKT    FROM [dbo].[fn_MAGAZA_ANBAR_LOAD] ('')";
            string strQuery = "SELECT WAREHOUSE_ID,ANBAR  FROM [dbo].[fn_MAGAZA_ANBAR_LOAD] ('')" +
               "  WHERE STOREID=@IDD";

            SqlCommand cmd = new SqlCommand(strQuery);

            cmd.Parameters.AddWithValue("@IDD", a);

            DataTable dt = GetData(cmd);




            lookUpEdit2.Properties.DisplayMember = "ANBAR";
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

               
                string queryString = "SELECT * FROM [dbo].[fn_MAGAZA_ANBAR_LOAD] ('') WHERE STOREID=@pricePoint";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", paramValue);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Visible = false;
                gridView1.Columns[2].Visible = false; //warehouse_id
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
            //LOAD
            getall(Convert.ToInt32(lookUpEdit1.EditValue));
            lookUpEdit8GEtData_yeni_anbar(Convert.ToInt32(lookUpEdit1.EditValue));
        }
        anbarobyektcrud ca = new anbarobyektcrud();
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            //TRANSFER 
            int MAGAZA = Convert.ToInt32(lookUpEdit1.EditValue);
            int ANBAR = Convert.ToInt32(lookUpEdit2.EditValue);

            Decimal B;

            //anbar transfer 
            //XtraMessageBox.Show("ugurlu");
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);
                //MessageBox.Show(i.ToString());
                //XtraMessageBox.Show(row[8].ToString());
                //B = Convert.ToDecimal(row[8].ToString());
                //XtraMessageBox.Show(B.ToString());
                int C = ca.MAGAZA_ANBAR_TRANFER(Convert.ToInt32(lookUpEdit2.EditValue), Convert.ToInt32(lookUpEdit1.EditValue),
                    Convert.ToDateTime(dateEdit1.Text), textEdit1.Text.ToString(), Convert.ToInt32(row[0]), Convert.ToDecimal(row[10]));
                   }
            XtraMessageBox.Show("ugurlu");
            getall(Convert.ToInt32(lookUpEdit1.EditValue));
            //getall();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Obyektden_anbara_gonderilenler oa = new Obyektden_anbara_gonderilenler();
            oa.Show();
        }
    }
}