using DevExpress.XtraBars;
using DevExpress.XtraEditors;
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
    public partial class Anbar : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //public string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        public Anbar()
        {
            InitializeComponent();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            clear();
        }

        private void buttonEdit1_EditValueChanged(object sender, EventArgs e)
        {
            
        }
     
        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void Anbar_Load(object sender, EventArgs e)
        {
            labelControl5.Text = "0";
            getall();
            getlistbox();
            labelControl5.Visible = false;

            textEdit1.TabIndex = 1;
            textEdit2.TabIndex = 2;
            textEdit4.TabIndex = 4;
            gridControl1.TabStop = false;
        } //load

        public void getall()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

                // Provide the query string with a parameter placeholder.
                string queryString = "select * from dbo.fn_store_warehouse() order by 1 ";
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@pricePoint", paramValue);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }

        }

        int conf;
        public void clear()
        {
            textEdit1.Text = "";
            textEdit2.Text = "";
            textEdit4.Text = "";
            labelControl5.Text = "0";
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);//First uncheck the old value!             
            }
        }  //clear
        CrudAnbar cm = new CrudAnbar();
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (string.IsNullOrEmpty(textEdit1.Text) || string.IsNullOrEmpty(textEdit2.Text)
                || string.IsNullOrEmpty(textEdit4.Text))
            {
                XtraMessageBox.Show("Məlumatlar Tam olaraq doldurulmalıdır");
            }
            else
            {
                if (Convert.ToInt32(labelControl5.Text.ToString()) < 1) //insert
                {
                    int ret = cm.InsertAnbar(textEdit1.Text, textEdit2.Text, textEdit4.Text);
                    
                    if (ret > 0)
                    {
                      
                        foreach (object i in checkedListBox1.CheckedItems)
                        {
                            ListItem item = i as ListItem;
                            //AA = item.Value.ToString() + ":" + item.Text.ToString();
                            //XtraMessageBox.Show(AA);

                            conf = cm.CRSTORE_WAREHOUSE(Convert.ToInt32(item.Value), ret);

                            //now you can use i.Value 
                        }

                        XtraMessageBox.Show("Məlumatlarınız uğurla daxil edildi");

                        clear();
                        getall();
                    }
                    if (ret == 0)
                    {
                        XtraMessageBox.Show("Məlumatlarınız artıq database də mövcuddur");
                        clear();
                        //getall();
                    }

                }
                else //update
                {
                    int f = countmalanbar(Convert.ToInt32(labelControl5.Text.ToString()));
                    int M = countmalmagaza(Convert.ToInt32(labelControl5.Text.ToString()));
                    int c = f + M;
                    if (c > 0)
                    {
                        XtraMessageBox.Show("ANBARINIZDA VƏ YA ANBARA BAĞLI OBYEKTİNİZDƏ  " + c.ToString() + "  ƏDƏD MAL VAR");
                    }
                    else
                    {

                        int ret = cm.updateAnbar(Convert.ToInt32(labelControl5.Text), textEdit1.Text, textEdit2.Text, textEdit4.Text);
                        //getall();
                        if (ret > 0)
                        {



                            //clear();
                            int g = cm.deletestorewarehouse(ret);
                            foreach (object i in checkedListBox1.CheckedItems)
                            {
                                ListItem item = i as ListItem;
                                //AA = item.Value.ToString() + ":" + item.Text.ToString();
                                //XtraMessageBox.Show(AA);

                                conf = cm.CRSTORE_WAREHOUSE(Convert.ToInt32(item.Value), ret);

                                //now you can use i.Value 
                            }
                            getall();
                            XtraMessageBox.Show("Məlumatlarınız uğurla dəyişdirildi");
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


        } //crud

        public void getlistbox()
        {
            SqlConnection conn = new SqlConnection();   //check
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.SqlCon;
            conn.Open();
            string query = "select STOREID,STORE_NAME from COMPANY.STORE order by 1 ";// position column from position table
            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //string myItem = dr["STORE_NAME"].ToString();

                //checkedListBox1.Items.Add(myItem, false);//true means check the items. use false if you don't want to check the items or simply .....Items.Add(myItem);

                ListItem item = new ListItem();
                item.Text = dr["STORE_NAME"].ToString();
                item.Value = dr["STOREID"].ToString();
                checkedListBox1.Items.Add(item);
            }

        }
        public class ListItem : Object
        {
            private object _value;
            private string _text;

            public object Value
            {
                get { return _value; }
                set { _value = value; }
            }

            public string Text
            {
                get { return _text; }
                set { _text = value; }
            }

            public override string ToString()
            {
                return _text;
            }
        }
        int paramValue;
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);//First uncheck the old value!             
                }
                paramValue = Convert.ToInt32(dr[0]);
                //XtraMessageBox.Show(paramValue.ToString());
                string queryString =
                    "exec sp_get_warehouse @id=@pricePoint ;exec dbo.sp_get_warehouse_store_check @id=@pricePoint";
               
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@pricePoint", paramValue);
                  
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            //            
                            labelControl5.Text = reader[0].ToString();
                            textEdit1.Text = reader[1].ToString();
                            textEdit2.Text = reader[2].ToString();
                            textEdit4.Text = reader[3].ToString();

                        }
                        reader.NextResult();
                        //sifirlamaq listbox u
                        for (int i = 0; i < checkedListBox1.Items.Count; i++)
                        {
                            checkedListBox1.SetItemChecked(i, false);//First uncheck the old value!             
                        }
                        while (reader.Read())
                        {

                            ListItem item = new ListItem();
                            item.Text = reader[1].ToString();
                            item.Value = reader[0].ToString();
                            //XtraMessageBox.Show(item.Text.ToString());
                            string ad;
                            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                            {
                                ad = checkedListBox1.Items[i].ToString();

                                if (item.Text == ad)
                                {
                                    
                                    checkedListBox1.SetItemChecked(i, true);
                                }
                               
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                    }

                }
            }
        }
        string qa = "declare @i nvarchar(250) =(select WAREHOUSE_NAME from COMPANY.WAREHOUSE where WAREHOUSE_ID=@pricepoint) " +
            " select count(*) from ( " +
            " select fa.MAL_ALISI_DETAILS_ID, " +
  " fa.WAREHOUSE_NAME, fa.SIRKET_ADI, fa.MEHSUL_KODU,"+
  " fa.MEHSUL_ADI, fa.ALIS_GIYMETI, fa.SATIS_GIYMETI," +
            " fa.MIGDARI - isnull(opl.migdar, 0.00) as MIGDARI, "+  
 " fa.GONDERILECEK_MIGDAR from[dbo].[fn_anbardan_anbara] " +
        " (@i)fa   left join(select WAREHOUSE_NAME, mal_details_id, " +
      "  sum(migdar) migdar from (select am.ANBAR_MAGAZA_ID, " +
      "  cw.WAREHOUSE_NAME, am.MAGAZA_ID,am.TARIX, am.EMELIYYAT_NOMRE, am.mal_details_id, am.migdar "+
      "  from ANBAR_MAGAZA am inner join COMPANY.WAREHOUSE cw on cw.WAREHOUSE_ID= am.ANBAR_ID)x group by WAREHOUSE_NAME, mal_details_id ) opl "+
      "  on fa.MAL_ALISI_DETAILS_ID = opl.mal_details_id and fa.WAREHOUSE_NAME = opl.WAREHOUSE_NAME )ty";
        public static int cnt;
        public static int cntmagaaza;
        string qa1 = "SELECT count(*) FROM [dbo].[fn_MAGAZA_ANBAR_LOAD] ('') WHERE WAREHOUSE_ID=@pricepoint1";
        private int countmalmagaza(int a)
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

                        cntmagaaza = Convert.ToInt32(reader[0]);


                    }
                    reader.Close();


                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    XtraMessageBox.Show(ex.Message);
                }
            }
            return cntmagaaza;
        }
        private int countmalanbar(int a)
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
            int f = countmalanbar(Convert.ToInt32(labelControl5.Text.ToString()));
            int M = countmalmagaza(Convert.ToInt32(labelControl5.Text.ToString()));
            int c = f + M;
            if (c > 0)
            { 
                XtraMessageBox.Show("ANBARINIZDA VƏ YA ANBARA BAĞLI OBYEKTİNİZDƏ " + c.ToString() +  " ƏDƏD MAL VAR");
            }
            else
            {
                int D = Convert.ToInt32(labelControl5.Text.ToString());
                if (D > 0)
                {
                    int ret = cm.deleteAnbar(D);

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
            //XtraMessageBox.Show(f.ToString());

        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            //EXCELL 
            try
            {
                string path = "output.xlsx";
                gridControl1.ExportToXlsx(path);
                // Open the created XLSX file with the default application. 
                Process.Start(path);
            }
            catch(Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        
    }
}