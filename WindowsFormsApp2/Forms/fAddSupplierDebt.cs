using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
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
                lDebtHistory.Visible = false;
        }

        private async void QaliqBorcHesabla(int supplierId)
        {
            //var debt = await DbProsedures.GET_SupplierTotalDebt(supplierId);
            var data = GetDebtData(supplierId);
            tDebtBalance.Text = data.ToString("N2");
        }

        private decimal GetDebtData(int supplierId)
        {
            try
            {
                string queryString = @"
SELECT 
    MAL_ALISI_MAIN_ID,
    SupplierDebtId,
    [FAKTURA NÖMRƏ] as ContractNo,
    TARIX,
    CAST(SUM(ISNULL(ESAS_BORC, 0.00)) AS decimal(18, 3)) AS ESAS_BORC,
    CAST(SUM(ISNULL(EDV_BORC, 0.00)) AS decimal(18, 3)) AS EDV_BORC,
    CAST(SUM(ISNULL(BORC, 0.00)) AS decimal(18, 3)) AS BORC,
    0.00 AS payEdv,
    0.00 AS payDebt
FROM (
    SELECT 
        f.MAL_ALISI_MAIN_ID,
        f.SupplierDebtId,
        f.[FAKTURA NÖMRƏ],
        f.TARIX,
        f.QİYMƏT - ISNULL(t.odenis, 0.00) AS BORC,
        ISNULL(f.ESAS_BORC, 0.00) - ISNULL(t.ESAS_BORC_ODENIS, 0.00) AS ESAS_BORC,
        ISNULL(f.VERGI, 0.00) - ISNULL(t.EDV_BORC, 0.00) AS EDV_BORC,
        0 AS 'ÖDƏNİŞ'
    FROM dbo.fn_TECHIZATCI_BORC(@pricePoint) f
    LEFT JOIN (
        SELECT 
            MAL_ALISI_MAIN_ID,
            SupplierDebtId,
            SUM(ISNULL(ESAS_BORC_ODENIS, 0.00)) AS ESAS_BORC_ODENIS,
            SUM(ISNULL(EDV_BORC, 0.00)) AS EDV_BORC,
            SUM(ISNULL(ESAS_BORC_ODENIS, 0.00)) + SUM(ISNULL(EDV_BORC, 0.00)) AS odenis
        FROM TECHIZATCI_ODENIS
        GROUP BY MAL_ALISI_MAIN_ID, SupplierDebtId
    ) t ON (
        (f.MAL_ALISI_MAIN_ID IS NOT NULL AND f.MAL_ALISI_MAIN_ID = t.MAL_ALISI_MAIN_ID)
        OR
        (f.MAL_ALISI_MAIN_ID IS NULL AND f.SupplierDebtId = t.SupplierDebtId)
    )
) o
WHERE BORC > 0.00
GROUP BY 
    MAL_ALISI_MAIN_ID,
    SupplierDebtId,
    [FAKTURA NÖMRƏ],
    TARIX
ORDER BY TARIX;
";
                using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
                using (SqlCommand cmd = new SqlCommand(queryString, connection))
                {
                    cmd.Parameters.AddWithValue("@pricePoint", supplierId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);

                            var totalAmount = dt.AsEnumerable()
                                                    .Sum(row => row.Field<decimal>("BORC"));
                            return totalAmount;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
                return 0;
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
    }
}