using System;
using System.Data;
using DevExpress.XtraGrid.Localization;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class musteri_gaytar : DevExpress.XtraEditors.XtraForm
    {
        private readonly QAIME_SATISI_QAYTARMA_LAYOUT frm1;
        public musteri_gaytar(QAIME_SATISI_QAYTARMA_LAYOUT frm)
        {
            InitializeComponent();
            frm1 = frm;

            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }
        public static string musteri_adi;
        public static string emeliyyat_nomre;
        public static string gaime_main_id;
        public static string gaime_nom;
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                gaime_main_id = dr[0].ToString();

                //  XtraMessageBox.Show(id.ToString());
                musteri_adi = dr[2].ToString();
                emeliyyat_nomre = dr[3].ToString();
                gaime_nom = dr[4].ToString();
            }
        }

        private void getall()
        {
            string queryString = @"SELECT
  GAIME_SATISI_MAIN_ID, 
  rn, 
  MUSTERI as N'MÜŞTƏRİ', 
  EMELIIYYAT_NOMRE as N'ƏMƏLİYYAT №', 
  GAIME_NOMRE as N'ALIŞ QAİMƏ №', 
  TARIX as N'ALIŞ TARİXİ' 
FROM 
  dbo.fn_GAIME_SATISI_gaytarma_axtaris_LOAD() 
ORDER BY GAIME_SATISI_MAIN_ID DESC";

            var data = DbProsedures.ConvertToDataTable(queryString);

            gridControl1.DataSource = data;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Visible = false;
        }

        private void musteri_gaytar_Load(object sender, EventArgs e)
        {
            getall();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            frm1.data_(musteri_adi, emeliyyat_nomre, gaime_main_id,gaime_nom);
            this.Close();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}