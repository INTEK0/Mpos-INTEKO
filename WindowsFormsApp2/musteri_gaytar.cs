using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
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
        public void getall()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                // Provide the query string with a parameter placeholder.
                //          string queryString =
                //            "select * from (  " +
                //             "  select GAIME_SATISI_MAIN_ID, MUSTERI, " +
                //               " ROW_NUMBER() over(partition by  EMELIIYYAT_NOMRE order by EMELIIYYAT_NOMRE) rn " +
                //   " ,EMELIIYYAT_NOMRE,GAIME_NOMRE,TARIX from GAIME_SATISI_MAIN " +
                //   " )t where rn = 1 " +
                //   " and t.GAIME_SATISI_MAIN_ID in ( " +
                //   " select GAIME_SATISI_MAIN_ID from( " +
                //   " select gd.GAIME_SATISI_MAIN_ID, gd.MIGDARI-gg.migdar mig " +
                //   " from GAIME_SATISI_DETAILS gd inner " +
                //   " join(select gaime_satis_details_id, " +
                //   " sum(isnull(migdar,0.00)) migdar from gaime_satis_gaytarma " +
                //   " group  by gaime_satis_details_id)  gg " +
                //   " on gd.GAIME_SATISI_DETAILS_ID = gg.gaime_satis_details_id " +
                //   "  )x where x.mig > 0 " +
                //" ) " +
                //   "  order by 6 desc ";

                string queryString = "select GAIME_SATISI_MAIN_ID,rn,MUSTERI as N'MÜŞTƏRİ', " +
                    " EMELIIYYAT_NOMRE as N'ƏMƏLİYYAT №', " +
                    " GAIME_NOMRE as N'ALIŞ QAİMƏ №',TARIX as N'ALIŞ TARİXİ' " +
                    " from dbo.fn_GAIME_SATISI_gaytarma_axtaris_LOAD() order by 5 desc ";


                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@pricePoint", paramValue);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Visible = false;

                //gridView1.OptionsSelection.MultiSelect = true;
                //gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;


            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }
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