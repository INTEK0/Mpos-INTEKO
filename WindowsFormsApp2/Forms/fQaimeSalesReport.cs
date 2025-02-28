using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fQaimeSalesReport : DevExpress.XtraEditors.XtraForm
    {
        private readonly GAIME_SATISI_LAYOUT frm1;
        private static string gaime_satis_id;

        public fQaimeSalesReport(GAIME_SATISI_LAYOUT frm)
        {
            InitializeComponent();
            frm1 = frm;
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void fQaimeSalesReport_Load(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1,"Qaimə satış hesabatı");
        }

        private void DataLoad()
        {
            string query = @"SELECT EMMELIYYAT_NOMRE AS N'ƏMƏLİYYAT №',
GAIME_NOM AS N'QAİMƏ №',
TARIX AS N'TARİX',
MUSTERI AS N'MÜŞTƏRİ' ,
MOBIL AS N'MOBİL',
odenilecek_mebleg AS N'ÖDƏNİLƏCƏK MƏBLƏĞ',
ODENILEN_MEBLEG AS N'ƏSAS AZN',
yekun_mebleg AS N'ƏDV AZN'
FROM dbo.fn_GAIME_SATISI_LOAD_axtaris()
ORDER BY cast
  (REPLACE (isnull(EMMELIYYAT_NOMRE, 'QS-0'), 'QS-', '') AS int) DESC ";

            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridControl1.TabStop = false;
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            frm1.GetallData(gaime_satis_id);
            frm1.get_em(gaime_satis_id);
            frm1.clear_details();
            this.Close();
        }

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
    }
}