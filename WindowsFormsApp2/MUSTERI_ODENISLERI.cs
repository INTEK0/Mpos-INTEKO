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
    public partial class MUSTERI_ODENISLERI : DevExpress.XtraEditors.XtraForm
    {
        public static int t_odenis_user_id;
        public MUSTERI_ODENISLERI(int t_user_id)
        {
            InitializeComponent();
            t_odenis_user_id = t_user_id;
        }
        techizatci_odenis t = new techizatci_odenis();
        private void MUSTERI_ODENISLERI_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit1.Text = dateTime.ToShortDateString();
            textEdit5.Enabled = false;
            string a = t.musteri_emeliyyat_nomre();
            textEdit5.Text = a;
            lookUpEdit8GEtData_yeni_anbar();
        }
        private void lookUpEdit8GEtData_yeni_anbar()
        {
            //int id = Convert.ToInt32(lookUpEdit7.EditValue.ToString());



            //string strQuery = "SELECT STOREID,OBYEKT    FROM [dbo].[fn_MAGAZA_ANBAR_LOAD] ('')";
            string strQuery = " select * from [dbo].[musteri_borclu] () ";
            SqlCommand cmd = new SqlCommand(strQuery);

            //cmd.Parameters.AddWithValue("@IDD", a);

            DataTable dt = GetData(cmd);




            lookUpEdit1.Properties.DisplayMember = "MÜŞTƏRİ";
            lookUpEdit1.Properties.ValueMember = "MUSTERILER_ID";
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

        private void lookUpEdit1_TextChanged(object sender, EventArgs e)
        {
            getsum(Convert.ToInt32(lookUpEdit1.EditValue));
            getall(Convert.ToInt32(lookUpEdit1.EditValue));
        }
        public void getall(int A)
        {
            int paramValue = A;
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                string queryString = "   select* from dbo.musteri_borclu_all(@pricePoint) ";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", paramValue);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false; //MAL_ALISI_MAIN_ID
                gridView1.Columns[1].Visible = false;
                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }

        }
        public void getsum(int A)
        {
            int paramValue = A;


            string queryString = " select * from  dbo.musteri_borclu_CEM (@pricePoint) ";

            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.AddWithValue("@pricePoint", paramValue);

            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {

                textEdit2.Text = dr["esas_borc"].ToString();
                textEdit1.Text = dr["edv_borcu"].ToString();
                textEdit14.Text = dr["yekun_borc"].ToString();

            }
            connection.Close();



        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int conf = 0;
            //Decimal gh = confirmation_total();
            //if (gh <= 0)
            //{
            //    XtraMessageBox.Show("XƏBƏRDALIQ - ÖDƏNİŞ XANASI BOŞDUR");
            //}
            //else
            //{
            if (string.IsNullOrEmpty(dateEdit1.Text))
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
                    //decimal x = Convert.ToDecimal(row[4]);
                    //decimal y = Convert.ToDecimal(row[3]);
                    string fak_nom;
                    if (string.IsNullOrEmpty(textEdit17.Text))
                    {
                        fak_nom = "-";
                    }
                    else
                    {
                        fak_nom = textEdit17.Text.ToString();
                    }
                    //if (x <= y)
                    //{
                    //   XtraMessageBox.Show(row[4].ToString());
                    int a = t.INSERT_musteri_ODENIS(Convert.ToInt32(row[1].ToString()),
                         radio, 
                         fak_nom,
                        // memoEdit1.Text.ToString(),
                        Convert.ToDateTime(dateEdit1.Text), t_odenis_user_id,
                         Convert.ToDecimal(row[7]), Convert.ToDecimal(row[8]),
                        textEdit5.Text.ToString() //emeliyyat nomre
                                                 );

                    conf = conf + a;

                    //}
                    //else
                    //{
                    //    XtraMessageBox.Show("ODƏNİLƏN MƏBLƏĞ BORCDAN ARTIQ OLA BİLMƏZ");
                    //}

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
            //}
        }
        public static string radio = "NƏGD";

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textEdit17.Enabled = false;
            radio = "NƏGD";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textEdit17.Enabled = true;
            radio = "BANK";
        }
    }
}