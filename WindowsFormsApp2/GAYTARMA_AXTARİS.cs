using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraEditors.Controls;
using System.Data.SqlClient;
using DevExpress.XtraEditors;

namespace WindowsFormsApp2
{
    public partial class GAYTARMA_AXTARİS : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly MEHSUL_GAYTARMA_LAYOUT frm1;
        public GAYTARMA_AXTARİS(MEHSUL_GAYTARMA_LAYOUT frm   )
        {
           frm1 = frm;
            GridLocalizer.Active = new GermanGridLocalizer();
            Localizer.Active = new GermanEditorsLocalizer();
            InitializeComponent();
        }

        private void GAYTARMA_AXTARİS_Load(object sender, EventArgs e)
        {
            //DateTime dateTime = DateTime.UtcNow.Date;
            //dateEdit4.Text = dateTime.ToString();
            //dateEdit1.Text = dateTime.ToString();
            DateTime dateTime = DateTime.UtcNow.Date;

            //dateEdit1.Text = dateTime.ToShortDateString();
            //dateEdit4.Text = dateTime.ToShortDateString();

            dateEdit3.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();
            gridControl1.TabStop = false;

    
            labelControl29.Visible = false;
            labelControl36.Visible = false;
            labelControl35.Visible = false;
            labelControl34.Visible = false;
            lookUpEdit2.Visible = false;
            gridLookUpEdit1.Visible = false;
            textEdit4.Visible = false;
            textEdit5.Visible = false;
            //gridView1.GroupPanelText = "QRUPLAŞDIRMAQ İSTƏDİYİNİZ SÜTUNU SEÇİN";


         
        }
        //string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        public void getall()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

                // Provide the query string with a parameter placeholder.
                string queryString = "select * from  [MAL_GEYTARMA_DETAILS] where [QAYTARILMIŞ MİQDAR] !=0.00";
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@pricePoint", paramValue);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[6].Visible = false;

                gridControl1.TabStop = false;

                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }

        }
//        public void getal()
//        {

//            try
//            {
//                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

//                // Provide the query string with a parameter placeholder.
//                string queryString = "select d.* from  [MAL_GEYTARMA_DETAILS] d " +
//" inner join MAL_GEYTARMA_MAIN m on m.MAL_GEYTARMA_MAIN_ID = d.MAL_GEYTARMA_MAIN_ID " +
//                    " where m.TARIX between CAST(@pricePoint AS DATE) and CAST(@pricePoint1 AS DATE) ";
//                SqlCommand command = new SqlCommand(queryString, connection);
//                command.Parameters.AddWithValue("@pricePoint", dateEdit4.Text.ToString());
//                command.Parameters.AddWithValue("@pricePoint1", dateEdit1.Text.ToString());
//                SqlDataAdapter da = new SqlDataAdapter(command);
//                DataTable dt = new DataTable();
//                da.Fill(dt);
//                gridControl1.DataSource = dt;
//              //  gridView1.Columns[0].Visible = false;
//                //gridView1.OptionsSelection.MultiSelect = true;
//                //gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine("Xəta!\n" + e);
//            }

//        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dateEdit2.Text)
                || string.IsNullOrEmpty(dateEdit3.Text))
                {

                XtraMessageBox.Show("MƏLUMATLAR TAM OLARAQ DOLDURULMALIDIR");
              
            }
            else
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                // Provide the query string with a parameter placeholder.
                string queryString = "SELECT * FROM dbo.GAYTARMA_HESABAT (cast(@pricePoint AS DATE) , CAST(@pricePoint1 AS DATE)) ";


                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", dateEdit3.Text.ToString());
                command.Parameters.AddWithValue("@pricePoint1", dateEdit2.Text.ToString());
                SqlDataAdapter da = new SqlDataAdapter(command);
               
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                //gridView1.Columns[0].Visible = false;
                //gridView1.Columns[6].Visible = false;
            }
        }
        MEHSUL_GAYTARMA_AXTARIS_CRUD mg = new MEHSUL_GAYTARMA_AXTARIS_CRUD();
        public void refresh()
        {

        }
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMessageBox.Show("MƏHSULUN ALIŞI İLƏ BAĞLI DƏYİŞİKLİK ETMƏK VƏ YA SİLMƏK ÜÇÜN MƏHSUL QAYTARMA BÖLMƏSİNƏ DAXİL OLARAQ ƏMƏLİYYAT APARA BİLƏRSİNİZ");
            //int ret = 1;
            //if (ret > 0)
            //{
            //    //XtraMessageBox.Show("ugurlu");
            //    foreach (int i in gridView1.GetSelectedRows())
            //    {
            //        DataRow row = gridView1.GetDataRow(i);
            //        //MessageBox.Show(i.ToString());

            //        int a = mg.updateMehsulgaytarma(Convert.ToInt32(row[0]), Convert.ToDecimal(row[8]));
            //    }
            //    XtraMessageBox.Show("ugurlu");
            //}
            //getall();
          
        }

        private void GAYTARMA_AXTARİS_FormClosing(object sender, FormClosingEventArgs e)
        {

         frm1.getall();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}