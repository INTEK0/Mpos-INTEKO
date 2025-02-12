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
using WindowsFormsApp2.Helpers.FormComponents;
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

        }
    }
}