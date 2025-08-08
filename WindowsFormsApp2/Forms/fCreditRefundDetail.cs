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
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Forms
{
    public partial class fCreditRefundDetail : DevExpress.XtraEditors.XtraForm
    {
        private readonly int _Id;
        public fCreditRefundDetail(int id)
        {
            InitializeComponent();
            _Id = id;
            FormHelpers.GridPanelText(gridView1);
        }

        private void fCreditRefundDetail_Load(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void DataLoad()
        {
            string query = "";

            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
        }
    }
}