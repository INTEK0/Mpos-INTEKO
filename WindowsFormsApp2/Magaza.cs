using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
    public partial class Magaza : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Magaza()
        {
            InitializeComponent();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            clear();
        }

        private void Magaza_Load(object sender, EventArgs e)
        {
            tab();
            clear();
            labelControl4.Text = "0";
            getall();
            labelControl4.Visible = false;
            gridControl1.TabStop = false;

         
        }
        private void tab()
        {
            textEdit3.TabIndex = 1;
            textEdit1.TabIndex = 2;
            textEdit2.TabIndex = 3;
            
        }
        CrudMagaza cm = new CrudMagaza();
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (String.IsNullOrEmpty(textEdit3.Text) || string.IsNullOrEmpty(textEdit1.Text))
            {
                XtraMessageBox.Show("Məlumatlar tam doldurun");
            }
            else
            {

                if (Convert.ToInt32(labelControl4.Text.ToString()) < 1) //insert
                {
                    int ret = cm.InsertMagaza(textEdit3.Text, textEdit1.Text, textEdit2.Text);
                    getall();
                    if (ret > 0)
                    {

                        XtraMessageBox.Show("Məlumatlarınız uğurla daxil edildi");

                        clear();

                    }
                    if (ret == 0)
                    {
                        XtraMessageBox.Show("MAĞAZA ARTIQ MÖVCUDDUR");
                        //clear();
                        getall();
                    }

                }
                else  //update
                {

                    int f = countmal(Convert.ToInt32(labelControl4.Text));
                    if (f > 0)
                    {
                        XtraMessageBox.Show("MAĞAZANIZDA " + f.ToString() + " ƏDƏD MAL VAR");
                    }
                    else
                    {
                        int ret = cm.updateMagaza(Convert.ToInt32(labelControl4.Text), textEdit3.Text, textEdit1.Text, textEdit2.Text);
                        getall();
                        if (ret > 0)
                        {

                            XtraMessageBox.Show("Məlumatlarınız uğurla dəyişdirildi");

                            //clear();

                        }
                        if (ret == 0)
                        {
                            XtraMessageBox.Show("Uğursuz Əməliyyat");
                            //clear();
                            getall();
                        }
                    }
                }
                }

            
        }
        public void clear()
        {
            getall();
           
                textEdit1.Text = "";
                textEdit2.Text = "";
                textEdit3.Text = "";
                labelControl4.Text = "0";

            tab();
        }
       // string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";

        public void getall()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                // Provide the query string with a parameter placeholder.
                string queryString =
                  "select * from fn_store() order by 1 ";


                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@pricePoint", paramValue);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;



                tab();
            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }

        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {

                int id = Convert.ToInt32(dr[0]);
                labelControl4.Text = dr[0].ToString();
                textEdit3.Text = dr[1].ToString();
                textEdit1.Text = dr[2].ToString();
                textEdit2.Text = dr[3].ToString();
                //XtraMessageBox.Show(id.ToString());
            }
        }
        string qa = "SELECT count(*) FROM [dbo].[fn_MAGAZA_ANBAR_LOAD] ('') WHERE STOREID=@pricepoint";
        public static int cnt;
        private int countmal(int a)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
            {

                SqlCommand command = new SqlCommand(qa, connection);
                command.Parameters.AddWithValue("@pricepoint", a);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                       cnt = Convert.ToInt32(reader[0]);


                    }
                    reader.Close();


                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    XtraMessageBox.Show(ex.Message);
                }
            }
            return cnt;
        }
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

            //delete 
            int f =countmal(Convert.ToInt32(labelControl4.Text));
            if (f > 0)
            {
                XtraMessageBox.Show("MAĞAZANIZDA "+ f.ToString()+" ƏDƏD MAL VAR");
            }
            else
            {
                int did = Convert.ToInt32(labelControl4.Text.ToString());
                if (did > 0)
                {
                    int ret = cm.deleteMagaza(did);

                    getall();
                    if (ret > 0)
                    {

                        XtraMessageBox.Show("Məlumatlarınız uğurla silindi");
                        getall();
                        clear();

                    }
                    if (ret == 0)
                    {
                        XtraMessageBox.Show("Uğursuz Əməliyyat");
                        getall();
                        //clear();
                    }

                }
                else
                {
                    XtraMessageBox.Show("Silmək üçün müvafiq xana seçilməyib");
                }
            }
          
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
          
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            
            //if ((e as DXMouseEventArgs).Button == System.Windows.Forms.MouseButtons.Left)
            //{
                //XtraMessageBox.Show("sol");
                GridHitInfo hitInfo = gridView1.CalcHitInfo(gridView1.GridControl.PointToClient(MousePosition));
                if (hitInfo.HitTest == GridHitTest.RowCell)
                {
                    labelControl4.Text = Convert.ToString(gridView1.GetListSourceRowCellValue(gridView1.FocusedRowHandle, "STOREID"));
                    //XtraMessageBox.Show(Convert.ToString(gridView1.GetListSourceRowCellValue(gridView1.FocusedRowHandle, "STOREID")));
                    textEdit3.Text = Convert.ToString(gridView1.GetListSourceRowCellValue(gridView1.FocusedRowHandle, "Mağaza"));
                    textEdit1.Text = Convert.ToString(gridView1.GetListSourceRowCellValue(gridView1.FocusedRowHandle, "Ünvan"));
                    textEdit2.Text = Convert.ToString(gridView1.GetListSourceRowCellValue(gridView1.FocusedRowHandle, "Telefon"));

                    // Do something with FNAME and LNAME....  

                }
            //}
               
            
             
        }

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
    }
}