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
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2
{
    public partial class mehsul_adi_axtar : DevExpress.XtraEditors.XtraForm
    {
        private readonly MEHSUL_ALISI_LAYOUT frm1;
        public static int x;
        public mehsul_adi_axtar(int supplierId, MEHSUL_ALISI_LAYOUT frm)
        {
            InitializeComponent();
            frm1 = frm;
            x = supplierId;
        }

        private void mehsul_adi_axtar_Load(object sender, EventArgs e)
        {
            getall(x);
        }

        public void getall(int Y)
        {
            int paramValue =Y;
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                string queryString = @"
                select D.MEHSUL_ADI as N'MƏHSUL ADI',
                D.MEHSUL_KODU as N'MƏHSUL KODU', 
                D.ALIS_GIYMETI as N'SON ALIŞ QİYMƏTİ',
                d.SATIS_GIYMETI as N'SATIŞ QİYMƏTİ',
                d.BARKOD,
                V.VAHIDLER_ID as N'VAHID', 
                TAX.EDV_ID AS 'VERGI'
                from MAL_ALISI_MAIN M 
                INNER JOIN MAL_ALISI_DETAILS D 
                LEFT JOIN Vahidler V ON D.VAHID = V.VAHIDLER_ID 
                LEFT JOIN VERGI_DERECESI TAX ON D.VERGI_DERECESI = TAX.EDV_ID ON M.MAL_ALISI_MAIN_ID = D.MAL_ALISI_MAIN_ID WHERE M.TECHIZATCI_ID = @pricePoint";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", paramValue);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[5].Visible = false;
                gridView1.Columns[6].Visible = false;
                gridView1.OptionsSelection.MultiSelect = false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }

        }
        public static string m_ad;
        public static string m_kod;
        public static string m_alis_giymet;
        public static string satis_giymeti;
        public static string barkod;
        public static int vahid;
        public static int tax;
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            frm1.mehsul_kod_axtar(m_ad, m_kod,m_alis_giymet,satis_giymeti,barkod,vahid,tax);
            this.Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            //if (dr != null)
            //{
            //    m_ad = dr[0].ToString();

            //    //  XtraMessageBox.Show(id.ToString());
            //    m_kod = dr[1].ToString();
            //    m_alis_giymet = dr[2].ToString();
            //    satis_giymeti = dr[3].ToString();
            //    barkod = dr[4].ToString();
            //}
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                DatabaseClasses.Products products = new DatabaseClasses.Products
                {
                    ProductName = dr[0].ToString(),
                    ProductCode = dr[1].ToString(),
                    PurchasePrice = Convert.ToDecimal(dr[2].ToString()),
                    SalePrice = Convert.ToDecimal(dr[3].ToString()),
                    Barocde = dr[4].ToString(),
                    UnitId= Convert.ToInt32(dr[5].ToString()),
                    TaxId= Convert.ToInt32(dr[6].ToString()),
                };




                m_ad = dr[0].ToString();
                m_kod = dr[1].ToString();
                m_alis_giymet = dr[2].ToString();
                satis_giymeti = dr[3].ToString();
                barkod = dr[4].ToString();
                vahid = Convert.ToInt32(dr[5].ToString());
                tax = Convert.ToInt32(dr[6].ToString());
            }
        }
    }
}