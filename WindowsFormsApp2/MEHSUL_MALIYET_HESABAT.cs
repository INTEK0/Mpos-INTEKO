using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;

using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class MEHSUL_MALIYET_HESABAT : DevExpress.XtraEditors.XtraForm
    {
        public MEHSUL_MALIYET_HESABAT()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void MEHSUL_MALIYET_HESABAT_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.SqlCon;
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            string queryStringINSERTDELETE = "EXEC dbo.malyetraporinsertdelete";

            DateTime start = Convert.ToDateTime(dateEdit1.Text);
            DateTime finish = Convert.ToDateTime(dateEdit2.Text);

            string queryStringRUN = "EXEC dbo.[maliyetrapor] ";
            string queryString = "SELECT [TUR] as [Növü]\r\n      ,[DiscountDate] as Tarix\r\n      ,[MEHSUL_KODU] as [Məhsul Kodu]\r\n      ,[BARKOD] as [Barkod]\r\n      ,[MEHSUL_ADI] as [Məhsul Adı]\r\n      ,[gununevveli] as [Günün Əvvəlinə Anbar qalıq say (ədəd)]\r\n      ,[satisnovu] as [Satış növü]\r\n      ,[yenisatismiktari] as [Yeni Satış miqdarı (ədəd)]\r\n      ,[MIGDARI] as [Yeni Alış miqdarı (ədəd)]\r\n      ,[ALIS_GIYMETI] as [Alış Qiyməti (AZN)]\r\n      ,[gelir] as [Üzərinə əlavə edilən Gəlir ]\r\n      ,[SATIS_GIYMETI] as [ilkin Satış Qiyməti (AZN)]\r\n      ,[indirim] as [Satış və Alış Dövründə endirim (azn)]\r\n      ,[SONSATIS] as [Son satış qiyməti ]\r\n      ,[SONHALIS] as [Xalis gəlir (AZN)]\r\n      ,[STOKSON] as [Anbar qalıq sayı (ədəd)]\r\n      ,[SATICI] as [Satan şəxs]\r\n      ,[ALISMALIYET] as [Anbar üzrə qalıq Alış Dəyəri (AZN)]\r\n      ,[SATISMALIYET] as [Anbar üzrə Ümumi Satış Dəyəri (AZN)]\r\n      ,[KARSA] as [Anbar qalıq üzrə ümumi xalis gəlir  (AZN)]\r\n  FROM [dbo].[MALZEMEMALIYETREPORT] where  [DiscountDate]>='" + start.ToString("yyyy-MM-dd") + "'  and [DiscountDate]<='" + finish.AddDays(1).ToString("yyyy-MM-dd") + "'   ";

            con.Open();
            SqlCommand commandinsertdelete = new SqlCommand(queryStringINSERTDELETE, con);

            commandinsertdelete.ExecuteNonQuery();
            con.Close();


            con.Open();
            SqlCommand commandrun = new SqlCommand(queryStringRUN, con);

            commandrun.ExecuteNonQuery();
            con.Close();


            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@date1", dateEdit1.Text);
            command.Parameters.AddWithValue("@date2", dateEdit2.Text);

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Məhsul maliyet Hesabatı");
        }
    }
}