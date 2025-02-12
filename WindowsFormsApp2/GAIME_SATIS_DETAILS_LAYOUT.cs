using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
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
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class GAIME_SATIS_DETAILS_LAYOUT : DevExpress.XtraEditors.XtraForm
    {
        CRUD_GAIME_SATISI cgs = new CRUD_GAIME_SATISI();
        private readonly GAIME_SATISI_LAYOUT frm1;
        public GAIME_SATIS_DETAILS_LAYOUT(GAIME_SATISI_LAYOUT frm)
        {
            InitializeComponent();
            frm1 = frm;
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void GAIME_SATIS_DETAILS_LAYOUT_Load(object sender, EventArgs e)
        {
            getall();

            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridControl1.TabStop = false;
        }
        private void getall()
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            string queryString = "select EMMELIYYAT_NOMRE AS N'ƏMƏLİYYAT №'," +
                "GAIME_NOM AS N'QAİMƏ №',TARIX AS N'TARİX',MUSTERI AS N'MÜŞTƏRİ' " +
                " ,MOBIL AS N'MOBİL',odenilecek_mebleg AS N'ÖDƏNİLƏCƏK MƏBLƏĞ', " +
                " ODENILEN_MEBLEG AS N'ƏSAS AZN', yekun_mebleg AS N'ƏDV AZN' " +
                " from dbo.fn_GAIME_SATISI_LOAD_axtaris() " +
                " 	order by cast( REPLACE (isnull(EMMELIYYAT_NOMRE,'QS-0'),'QS-','') as int) desc ";
            SqlCommand command = new SqlCommand(queryString, connection);


            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gridControl1.DataSource = dt;
        }
        public static string gaime_satis_id;

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                string id = dr[0].ToString();

                //  XtraMessageBox.Show(id.ToString());
                gaime_satis_id = dr[0].ToString();

            }
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            frm1.GetallData(gaime_satis_id);
            frm1.get_em(gaime_satis_id);
            frm1.clear_details();
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = "output.xlsx";
                gridControl1.ExportToXlsx(path);
                // Open the created XLSX file with the default application. 
                Process.Start(path);
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }
    }
}