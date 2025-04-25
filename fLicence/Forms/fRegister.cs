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
using fLicence.Helpers;
using fLicence.Helpers.Implementations;

namespace fLicence.Forms
{
    public partial class fRegister : DevExpress.XtraEditors.XtraForm
    {
        LocalDateTimeService dateTimeService = new LocalDateTimeService();

        public fRegister()
        {
            InitializeComponent();
        }

        private void fRegister_Load(object sender, EventArgs e)
        {
           dateRegister.DateTime = dateTimeService.Current;
            lookTerminalType.Properties.DataSource = Enum.GetValues(typeof(Enums.TerminalType));
        }
    }
}