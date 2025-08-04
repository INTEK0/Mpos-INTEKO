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
    public partial class fCreditSale : DevExpress.XtraEditors.XtraForm
    {
        private readonly DatabaseClasses.User _user = DbProsedures.GetUser();
        public fCreditSale()
        {
            InitializeComponent();
        }

        private void fCreditSale_Load(object sender, EventArgs e)
        {
            tProccessNo.Text = DbProsedures.GET_CreditSaleProccessNo();
            tCashier.Text = _user.NameSurname;
        }
    }
}