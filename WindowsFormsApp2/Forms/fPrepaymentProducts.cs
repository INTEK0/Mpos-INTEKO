using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fPrepaymentProducts : DevExpress.XtraEditors.XtraForm
    {
        private readonly int _posMainId = 0;
        public fPrepaymentProducts(int posMainId)
        {
            InitializeComponent();
            GridLocalizer.Active = new MyGridLocalizer();
            GridPanelText(gridAvans);
            _posMainId = posMainId;
        }

        private void fPrepaymentProducts_Load(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void DataLoad()
        {
            string query = $@"select t.SIRKET_ADI,
       k.KATEGORIYA,
       mad.MEHSUL_KODU,
       mad.MEHSUL_ADI,
       mad.BARKOD,
       psd.count_,
       v.VAHIDLER_NAME,
       psd.satis_giymet,
       psd.satis_giymet * psd.count_ as Total
from [pos_satis_check_details] psd
inner join MAL_ALISI_DETAILS mad ON mad.MAL_ALISI_DETAILS_ID = psd.mal_alisi_details_id
inner join KATEGORIYA k on k.KATEGORIYA_ID = mad.KATEGORIYA
inner join MAL_ALISI_MAIN man on man.MAL_ALISI_MAIN_ID = mad.MAL_ALISI_MAIN_ID
inner join COMPANY.TECHIZATCI t on t.TECHIZATCI_ID = man.TECHIZATCI_ID
inner join VAHIDLER v ON v.VAHIDLER_ID = psd.quantity_type
where psd.pos_satis_check_main_id = {_posMainId}";

            var data = DbProsedures.ConvertToDataTable(query);
            gridControlAvans.DataSource = data;
        }
    }
}