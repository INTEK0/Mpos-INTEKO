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
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class TECHIZATCIKREDIT : DevExpress.XtraEditors.XtraForm
    {
      
        private readonly KREDITSATISLAYOUTSA frm1;
        public TECHIZATCIKREDIT( KREDITSATISLAYOUTSA frm_)
        {
            InitializeComponent();
            frm1 = frm_;
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void TECHIZATCIKREDIT_Load(object sender, EventArgs e)
        {
            int f_ = GETSTATUS();
            //  XtraMessageBox.Show(f_.ToString());
            switch (f_)
            {
                case 0:
                    //menfi baglidir
                    getall_menfi_bagli();
                    break;
                case 1:
                    //menfi aciqdir
                    getall_menfi_ACIG();
                    //getall();
                    break;

            }

        }

        public int GETSTATUS()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                string queryString = "SELECT STATUS FROM MENFI_AC_BAGLA ";
                SqlCommand command = new SqlCommand(queryString, connection);


                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                int number = dt.Rows[0].Field<int>("STATUS");
                // XtraMessageBox.Show(number.ToString());
                //if (number > 0)
                //{
                //    checkBox1.Checked = true;
                //    checkBox1.Text = "AÇIQDIR";
                //}
                //else
                //{
                //    checkBox1.Checked = false;
                //    checkBox1.Text = "BAĞLIDIR";
                //}

                return number;

            }
            catch (Exception e)
            {

                XtraMessageBox.Show("Xəta!\n" + e);
                return -100;
            }

        }

        public void getall_menfi_ACIG()
        {
            try
            {
                var data = DbProsedures.ConvertToDataTable("exec dbo.gaime_Satis_mal_load");
        
                gridControl1.DataSource = data;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[2].Visible = false;
                gridView1.Columns["EDV"].Visible = false;
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }
        public void getall_menfi_bagli()
        {
            try
            {
                var data = DbProsedures.ConvertToDataTable("exec dbo.gaime_Satis_mal_menfi_bagli_load");

                gridControl1.DataSource = data;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[2].Visible = false;
                gridView1.Columns["EDV"].Visible = false;
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }

        public static string techizatci_adi;
        public static string mehsul_adi;
        public static string satis_giymeti;
        public static int mal_det_id;
        public static string anbar_g;
        public static string edv_;
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            //if (dr != null)
            //{


            //    //  XtraMessageBox.Show(id.ToString());
            //    techizatci_adi = dr[1].ToString();
            //    mehsul_adi = dr[3].ToString();
            //    satis_giymeti = dr[5].ToString();
            //    mal_det_id = Convert.ToInt32(dr[2].ToString());
            //    anbar_g = dr[6].ToString();
            //    edv_ = dr["EDV"].ToString();
            //}
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            frm1.techizatci_axtar(techizatci_adi, mehsul_adi, satis_giymeti, mal_det_id, anbar_g, edv_);
           
            // frm1.lookUpEdit8GEtData_yeni(mal_det_id);

            this.Close();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                techizatci_adi = dr[1].ToString();
                mehsul_adi = dr[3].ToString();
                satis_giymeti = dr[5].ToString();
                mal_det_id = Convert.ToInt32(dr[2].ToString());
                anbar_g = dr[6].ToString();
                edv_ = dr["EDV"].ToString();
            }
        }
    }
}