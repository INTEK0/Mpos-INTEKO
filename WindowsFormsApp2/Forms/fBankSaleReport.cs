using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts.Design;
using DevExpress.XtraEditors;
using WindowsFormsApp2.Helpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fBankSaleReport : DevExpress.XtraEditors.XtraForm
    {
        public fBankSaleReport()
        {
            InitializeComponent();
        }

        private void fBankSaleReport_Load(object sender, EventArgs e)
        {

        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            //getall(Convert.ToDateTime(dateStart.Text), Convert.ToDateTime(dateEnd.Text).AddDays(1));
        }

        private void bPrint_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Qaimə satış hesabatı");
        }
    }
}