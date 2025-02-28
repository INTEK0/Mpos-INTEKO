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
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Forms
{
    public partial class fDeletedProductsReport : DevExpress.XtraEditors.XtraForm
    {
        public fDeletedProductsReport()
        {
            InitializeComponent();
        }

        private void fDeletedProductsReport_Load(object sender, EventArgs e)
        {

        }

        private void bReport_Click(object sender, EventArgs e)
        {

        }

        private void bSearch_Click(object sender, EventArgs e)
        {

        }

        private void DataLoad(DateTime start, DateTime finish)
        {
            string query = $@"";
            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
        }
    }
}