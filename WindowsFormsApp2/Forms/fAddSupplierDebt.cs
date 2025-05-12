using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;

namespace WindowsFormsApp2.Forms
{
    public partial class fAddSupplierDebt : DevExpress.XtraEditors.XtraForm
    {
        public fAddSupplierDebt()
        {
            InitializeComponent();
        }

        private void fAddSupplierDebt_Load(object sender, EventArgs e)
        {
            SupplierLoad();
            dateEdit1.DateTime = DateTime.Now;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            Add();
        }

        private async void Add()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lookSupplier.Text) || lookSupplier.EditValue is null)
                {
                    FormHelpers.Alert("Təchizatçı seçimi edilmədi", MessageType.Success);
                    return;
                }

                SupplierDebt debt = new SupplierDebt()
                {
                    SupplierId = Convert.ToInt32(lookSupplier.EditValue.ToString()),
                    ContractNo = tContractNo.Text.Trim(),
                    Amount = Convert.ToDecimal(tAmount.Text),
                    Comment = tComment.Text.Trim(),
                    ContractDate = dateEdit1.DateTime,
                };

                int IsSuccess = await DbProsedures.InsertSupplierDebt(debt);
                if (IsSuccess > 0)
                {
                    FormHelpers.Alert($"{lookSupplier.Text} təchizatçısına {tAmount.Text} AZN borc uğurla yaradıldı", MessageType.Success);
                    Clear();
                }
            }
            catch (Exception)
            {

            }
        }

        private void SupplierLoad()
        {
            string query = $@"SELECT TECHIZATCI_ID,SIRKET_ADI AS N'TƏCHİZATÇI ADI' FROM COMPANY.TECHIZATCI WHERE IsDeleted = 0";
            var data = DbProsedures.ConvertToDataTable(query);
            lookSupplier.Properties.DisplayMember = "TƏCHİZATÇI ADI";
            lookSupplier.Properties.ValueMember = "TECHIZATCI_ID";
            lookSupplier.Properties.DataSource = data;
            lookSupplier.Properties.PopulateColumns();
            lookSupplier.Properties.Columns[0].Visible = false;
        }

        private void Clear()
        {
            tContractNo.Clear();
            tAmount.Clear();
            tComment.Clear();
            dateEdit1.Clear();
            tDebtBalance.Clear();
            tDebtNew.Clear();
            tDebtTotal.Clear();
            QaliqBorcHesabla(Convert.ToInt32(lookSupplier.EditValue));
            TotalDebtCalc();
            tContractNo.Focus();
        }

        private void lookSupplier_TextChanged(object sender, EventArgs e)
        {
            if (lookSupplier.EditValue != null)
            {
                QaliqBorcHesabla(Convert.ToInt32(lookSupplier.EditValue));
                TotalDebtCalc();
                //lDebtHistory.Visible = true;
            }
            else
            {
                lDebtHistory.Visible = false;
            }
        }

        private async void QaliqBorcHesabla(int supplierId)
        {
            var debt = await DbProsedures.GET_SupplierTotalDebt(supplierId);
            tDebtBalance.Text = debt.totalAmount.ToString("N2");
        }

        private void TotalDebtCalc()
        {
            decimal qaliqBorc = Convert.ToDecimal(tDebtBalance.Text);
            decimal yeniBorc = Convert.ToDecimal(tDebtNew.Text);
            decimal yekunBorc = qaliqBorc + yeniBorc;
            if (yekunBorc > 0)
            {
                tDebtTotal.EditValue = yekunBorc;
            }
        }

        private void tAmount_EditValueChanged(object sender, EventArgs e)
        {
            tDebtNew.Text = tAmount.Text;
            TotalDebtCalc();
        }

        private void lDebtHistory_Click(object sender, EventArgs e)
        {

        }
    }
}