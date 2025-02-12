using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
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

namespace WindowsFormsApp2
{
    public partial class Anbarda_obyekte_gonderilenler : DevExpress.XtraBars.Ribbon.RibbonForm
    {
       // public string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        public Anbarda_obyekte_gonderilenler()
        {
            InitializeComponent();
        }

        private void Anbarda_obyekte_gonderilenler_Load(object sender, EventArgs e)
        {
            gridControl1.TabStop = false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //load
            getall(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));

        }

        public void getall(DateTime d1,DateTime d2)
        {
            DateTime paramValue = d1;
            DateTime paramvalue1 = d2;
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);

                // Provide the query string with a parameter placeholder.
                string queryString = " select WAREHOUSE_NAME, mal_details_id, sum(migdar) migdar from( " +
                                     " select am.ANBAR_MAGAZA_ID,cw.WAREHOUSE_NAME, am.MAGAZA_ID, " +
                                     " am.TARIX, am.EMELIYYAT_NOMRE, am.mal_details_id, am.migdar " +
                                     " from ANBAR_MAGAZA am inner join COMPANY.WAREHOUSE cw on cw.WAREHOUSE_ID= am.ANBAR_ID)x " +
                                     " group by WAREHOUSE_NAME, mal_details_id ";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@pricePoint", Convert.ToDateTime(paramValue));
                command.Parameters.AddWithValue("@pricePoint1", Convert.ToDateTime(paramvalue1));
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            }
            catch (Exception e)
            {
                Console.WriteLine("Xəta!\n" + e);
            }
        }
    }

}