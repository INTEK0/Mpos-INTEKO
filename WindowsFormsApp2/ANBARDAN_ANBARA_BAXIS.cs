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
    public partial class ANBARDAN_ANBARA_BAXIS : DevExpress.XtraBars.Ribbon.RibbonForm
    {
       // public string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        public ANBARDAN_ANBARA_BAXIS()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //LOAD
            geta(dateEdit1.Text.ToString(), dateEdit4.Text.ToString());
        }

        public void getal()
        {
          
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

                // Provide the query string with a parameter placeholder.
                string queryString = " select * from  dbo.fn_AXTARIS_ANBARDAN_ANBARA_full () ";
                SqlCommand command = new SqlCommand(queryString, connection);
               
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
        public void geta(string A,string B)
        {
            string paramValue = A;
            string paramvalue1 = B;
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

                // Provide the query string with a parameter placeholder.
                string queryString = " select * from  dbo.fn_AXTARIS_ANBARDAN_ANBARA (@pricePoint,@pricePoint1) ";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint",Convert.ToDateTime( paramValue));
                command.Parameters.AddWithValue("@pricePoint1", Convert.ToDateTime(  paramvalue1));
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
        anbardan_anbara_baxis_crud ab = new anbardan_anbara_baxis_crud();
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            //update 

           

         
            //anbar transfer 
            //XtraMessageBox.Show("ugurlu");
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);
                //MessageBox.Show(i.ToString());
                //XtraMessageBox.Show(row[0].ToString());
                //XtraMessageBox.Show(row[7].ToString());
                //B = Convert.ToDecimal(row[8].ToString());
                //XtraMessageBox.Show(B.ToString());
                //int a = ca.crud_ANBAR_TRANSFER(Convert.ToInt32(row[0]), ANBAR_1, anbar_2, Convert.ToDateTime(dateEdit1.Text), textEdit1.Text.ToString(), Convert.ToDecimal(row[8].ToString()));
                int x = ab.crud_update_TRANSFER(Convert.ToInt32(row[0]), Convert.ToInt32(row[7]));
            }
            geta(dateEdit1.Text.ToString(), dateEdit4.Text.ToString());
        }

        private void ANBARDAN_ANBARA_BAXIS_Load(object sender, EventArgs e)
        {
            getal();
            gridControl1.TabStop = false;
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (var i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);
                if (row != null)
                { 
                    ab.crud_delete_TRANSFER(Convert.ToInt32(row[0]));
                }
            }
            getal();
        }
    }
}