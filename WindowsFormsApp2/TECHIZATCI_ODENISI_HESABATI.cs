using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
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
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class TECHIZATCI_ODENISI_HESABATI : DevExpress.XtraEditors.XtraForm
    {
        public TECHIZATCI_ODENISI_HESABATI()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ExcelExport(gridControl1, "Təchizatçı ödənişi hesabatı");
        }

        private void TECHIZATCI_ODENISI_HESABATI_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();
            lookupedittextxhange_main();
        }

        private void lookupedittextxhange_main()
        {
            string query = "select TECHIZATCI_ID,SIRKET_ADI AS N'TƏCHİZATÇI ADI' from COMPANY.TECHIZATCI WHERE IsDeleted = 0";
            var dataTable = DbProsedures.ConvertToDataTable(query);

            lookUpEdit1.Properties.DisplayMember = "TƏCHİZATÇI ADI";
            lookUpEdit1.Properties.ValueMember = "TECHIZATCI_ID";
            lookUpEdit1.Properties.DataSource = dataTable;
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns[0].Visible = false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lookUpEdit1.Text))
                {
                    GetallData(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
                }
                else
                {
                    GetallData_t_id(Convert.ToDateTime(dateEdit1.Text),
                                    Convert.ToDateTime(dateEdit2.Text),
                                    Convert.ToInt32(lookUpEdit1.EditValue));
                }
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
            }
        }

        private void GetallData_t_id(DateTime d1, DateTime d2, int v)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            string queryString = "SELECT * FROM dbo.fn_TECHIZATCI_ODENILENLER_hesabat_t_id (cast(@pricePoint AS DATE) , CAST(@pricePoint1 AS DATE),@pricePoint2)  ";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@pricepoint", d1);
            command.Parameters.AddWithValue("@pricepoint1", d2);
            command.Parameters.AddWithValue("@pricepoint2", v);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void GetallData(DateTime D1_, DateTime D2_)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            string queryString = "SELECT * FROM dbo.fn_TECHIZATCI_ODENILENLER_hesabat (cast(@pricePoint AS DATE) , CAST(@pricePoint1 AS DATE))  ";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@pricepoint", D1_);
            command.Parameters.AddWithValue("@pricepoint1", D2_);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
    }
}