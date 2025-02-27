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
using DevExpress.XtraGrid.Localization;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fCreditPay : DevExpress.XtraEditors.XtraForm
    {
        public fCreditPay()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridPanelText(gridView2);
            GridLocalizer.Active = new MyGridLocalizer();
        }
    }
}