using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class MUSTERI_AXTAR : DevExpress.XtraEditors.XtraForm
    {
        private readonly GAIME_SATISI_LAYOUT frm1;
        public MUSTERI_AXTAR(GAIME_SATISI_LAYOUT frm)
        {
            InitializeComponent();
            frm1 = frm;
            FormHelpers.GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }
        
        private void MUSTERI_AXTAR_Load(object sender, EventArgs e)
        {
            getall();
        }

        public void getall()
        {
            try
            {
                string query = @"SELECT MUSTERILER_ID,
                                 CompanyName AS N'F/Ş - H/Ş ADI',
                                 AD +' '+ SOYAD+ ' ' + ATAADI as [AD SOYAD ATA ADI],
                                 MOBIL AS N'MOBİL TEL' 
                                 FROM MUSTERILER
                                 WHERE IsDeleted = 0";
                var data = DbProsedures.ConvertToDataTable(query);
                gridControl1.DataSource = data;
                gridView1.Columns[0].Visible = false;
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            frm1.MUSTERI(ID, aD);
            
            this.Close();
        }
        public static string aD;
        public static string ID;

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                string id = dr[0].ToString();

                ID = id;
                aD = dr[1].ToString();
            }
        }
    }
}