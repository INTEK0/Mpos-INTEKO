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
    public partial class fCreditRefund : DevExpress.XtraEditors.XtraForm
    {
        public fCreditRefund()
        {
            InitializeComponent();
            FormHelpers.GridPanelText(gridView1);
        }

        private enum SearchType
        {
            All,
            ReceiptNo,
            Date
        }

        private void fCreditRefund_Load(object sender, EventArgs e)
        {
            gridControl1.Focus();
            gridView1.Focus();
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            var check = groupControl1.Controls.OfType<CheckEdit>().FirstOrDefault(x => x.Checked);
            if (check == null) { FormHelpers.Alert("Axtarış növü seçilmədi", Enums.MessageType.Warning); return; }

            switch (check.Tag.ToString())
            {
                case "All":
                    DataLoad(SearchType.All);
                    break;
                case "ReceiptNo":
                    DataLoad(SearchType.ReceiptNo);
                    break;
                case "Date":
                    if (string.IsNullOrWhiteSpace(dateEnd.Text))
                    {
                        FormHelpers.Alert("Tarix seçimi edilmədi", Enums.MessageType.Warning);
                        return;
                    }
                    DataLoad(SearchType.Date);
                    break;
            }
        }

        private void DataLoad(SearchType type)
        {
            gridControl1.DataSource = null;
            string query = null;

            switch (type)
            {
                case SearchType.All:
                    query = @"SELECT 
  KREDIT_SATISI_MAIN_ID as Id, 
  EMELIIYYAT_NOMRE as ProccessNo, 
  TARIX as CreditDate, 
  GAIME_NOMRE as ContractNo, 
  MUSTERI as CustomerName, 
  ReceiptNo, 
  longids as FiscalId, 
  personel as Cashier, 
  CAST(ilkinodenis as decimal(18, 2)) as InitialAmount, 
  CAST(prd_qty * prd_price AS DECIMAL(18, 2)) AS Total, 
  CAST(yekun AS decimal(18, 2)) as CreditAmount 
FROM 
  [KREDIT_SATISI_MAIN]
";
                    break;
                case SearchType.ReceiptNo:
                    query = @"SELECT 
  KREDIT_SATISI_MAIN_ID as Id, 
  EMELIIYYAT_NOMRE as ProccessNo, 
  TARIX as CreditDate, 
  GAIME_NOMRE as ContractNo, 
  MUSTERI as CustomerName, 
  ReceiptNo, 
  longids as FiscalId, 
  personel as Cashier, 
  CAST(ilkinodenis as decimal(18, 2)) as InitialAmount, 
  CAST(prd_qty * prd_price AS DECIMAL(18, 2)) AS Total, 
  CAST(yekun AS decimal(18, 2)) as CreditAmount 
FROM 
  [KREDIT_SATISI_MAIN]
WHERE
--Çek nömrəsinə görə axtarışı əlavə et
";
                    break;
                case SearchType.Date:
                    string start = dateStart.DateTime.ToString("yyyy-MM-dd");
                    string end = dateEnd.DateTime.ToString("yyyy-MM-dd");
                    query = $@"SELECT 
  KREDIT_SATISI_MAIN_ID as Id, 
  EMELIIYYAT_NOMRE as ProccessNo, 
  TARIX as CreditDate, 
  GAIME_NOMRE as ContractNo, 
  MUSTERI as CustomerName, 
  ReceiptNo, 
  longids as FiscalId, 
  personel as Cashier, 
  CAST(ilkinodenis as decimal(18, 2)) as InitialAmount, 
  CAST(prd_qty * prd_price AS DECIMAL(18, 2)) AS Total, 
  CAST(yekun AS decimal(18, 2)) as CreditAmount 
FROM 
  [KREDIT_SATISI_MAIN]
WHERE 
  TARIX BETWEEN '{start}' AND '{end}';
";
                    break;
            }

            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;

        }

        private void chDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chDate.Checked)
            {
                labelControl1.Visible = true;
                labelControl2.Visible = true;
                dateStart.Visible = true;
                dateEnd.Visible = true;
                tSearch.Visible = false;
                dateStart.DateTime = DateTime.Now;
            }
            else
            {
                labelControl1.Visible = false;
                labelControl2.Visible = false;
                dateStart.Visible = false;
                dateEnd.Visible = false;
                tSearch.Visible = true;
                dateStart.Clear();
                dateEnd.Clear();
                dateStart.DateTime = DateTime.Now;
            }
        }

        private void chAll_CheckedChanged(object sender, EventArgs e)
        {
            tSearch.Enabled = !chAll.Checked;
        }

        private void bRefund_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void bDetail_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            fCreditRefundDetail f = new fCreditRefundDetail(Id);
            f.ShowDialog();
        }
    }
}