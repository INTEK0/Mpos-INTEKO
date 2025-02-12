using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class ZAMINKREDIT : DevExpress.XtraEditors.XtraForm
    {
       
        private readonly KREDITSATISLAYOUTSA frm1;
        public ZAMINKREDIT(KREDITSATISLAYOUTSA frm)
        {
            InitializeComponent();
            frm1 = frm;
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void ZAMINKREDIT_Load(object sender, EventArgs e)
        {
            getall();
        }

        public void getall()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                //string queryString =
                //  "SELECT  ZAMINLER_ID, AD +' ' " +
                //   " +SOYAD + ' ' + ATAADI + ' ' +( CASE WHEN CINSI=N'KİŞİ' " +
                //    " THEN N'O' ELSE N'Q' END) AS [AD SOYAD ATA ADI], " +
                //    " SVNO AS N'ŞV-№',FINKOD AS N'FİN',MOBIL AS N'ƏLAQƏ' " +
                //    " FROM ZAMINLER ";

                string queryString = "SELECT  ZAMINLER_ID, AD + SOYAD + ATAADI as [AD SOYAD ATA ADI],SVNO AS N'ŞV-№',FINKOD AS N'FİN',MOBIL AS N'ƏLAQƏ' FROM ZAMINLER";

                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@pricePoint", paramValue);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;

                //gridView1.OptionsSelection.MultiSelect = true;
                //gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;


            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Xəta!\n" + e);
            }
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            frm1.ZAMIN(ID, aD);
            
            this.Close();
        }
        public static string aD;
        public static string ID;

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                string id = dr[0].ToString();
                ID = dr[0].ToString();
                aD = dr[1].ToString();
            }
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                string id = dr[0].ToString();
                ID = dr[0].ToString();
                aD = dr[1].ToString();
            }
        }
    }
}