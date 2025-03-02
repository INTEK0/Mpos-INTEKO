using DevExpress.XtraBars.Navigation;
using DevExpress.XtraCharts.Designer.Native;
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
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.FormComponents;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.Reports;

namespace WindowsFormsApp2.Forms
{
    public partial class fTest : DevExpress.XtraEditors.XtraForm
    {
        public fTest()
        {
            InitializeComponent();
        }

        private void fTest_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 12; i++)
            {
                controlModule card = new controlModule();
                card.Name = i.ToString();
                flowLayoutPanel1.Controls.Add(card);
            }
            getall(DateTime.Now);
        }

        private void getall(DateTime D2_)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    string queryString = "gaime_Satis_mal_load_tarixle  @d1 = @pricepoint1 ";
                    using (SqlCommand cmd = new SqlCommand(queryString, con))
                    {
                        cmd.Parameters.AddWithValue("@pricepoint1", D2_);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                gridControl1.DataSource = dt;
                                gridView1.Columns["TECHIZATCI_ID"].Visible = false;
                                gridView1.Columns["MAL_ALISI_DETAILS_ID"].Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE("Xəta!\n" + e);
            }
        }
    }
}