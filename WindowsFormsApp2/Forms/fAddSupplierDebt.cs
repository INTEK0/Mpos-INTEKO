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
                lDebtHistory.Visible = true;
            }
            else
            {
                lDebtHistory.Visible = false;
            }
        }

        private void QaliqBorcHesabla(int supplierId)
        {
            string query = @"
            SELECT Y.BORC - X.GAYTARMA_MEBLEG AS BORC FROM( select 1 AS ID, cast(sum(isnull(BORC, 0.00)) as decimal(18, 2)) as BORC
            FROM (SELECT f.MAL_ALISI_MAIN_ID, f.[FAKTURA NÖMRƏ],f.TARIX, f.QİYMƏT - isnull(t.odenis, 0.00) BORC,0 AS 'ÖDƏNİŞ'
            FROM dbo.fn_TECHIZATCI_BORC(@pricePoint) f 
            left join(select  MAL_ALISI_MAIN_ID, sum(ODENIS) odenis FROM TECHIZATCI_ODENIS
            group by MAL_ALISI_MAIN_ID)t  on f.MAL_ALISI_MAIN_ID = t.MAL_ALISI_MAIN_ID)o )Y
            LEFT JOIN(SELECT 1 AS ID, ISNULL(CAST(SUM(MD.ALIS_GIYMETI * D.MIGDARI) AS decimal(18, 2)), 0.00)
            AS GAYTARMA_MEBLEG FROM MAL_GEYTARMA_MAIN M
            INNER JOIN  MAL_GEYTARMA_DETAILS D ON
            M.MAL_GEYTARMA_MAIN_ID = D.MAL_GEYTARMA_MAIN_ID
            INNER JOIN MAL_ALISI_DETAILS MD ON MD.MAL_ALISI_DETAILS_ID = D.MAL_ALISI_DETAILS_ID
            INNER JOIN MAL_ALISI_MAIN MM ON MM.MAL_ALISI_MAIN_ID = MD.MAL_ALISI_MAIN_ID
            WHERE MM.TECHIZATCI_ID = @pricePoint)X ON X.ID = Y.ID";

            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@pricePoint", supplierId);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            tDebtBalance.Text = dr["BORC"].ToString();
                        }
                    }
                }
            }
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