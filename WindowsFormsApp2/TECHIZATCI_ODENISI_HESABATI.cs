using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DevExpress.XtraGrid;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class TECHIZATCI_ODENISI_HESABATI : DevExpress.XtraEditors.XtraForm
    {
        GridColumnSummaryItem _summaryItemPayment;
        public TECHIZATCI_ODENISI_HESABATI()
        {
            InitializeComponent();
            GridPanelText(gridView1);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (navigationFrame1.SelectedPage == page1)
            {
                ExcelExport(gridControl1, "Təchizatçı ödəniş hesabatı");
            }
            else
            {
                ExcelExport(gridControl2, $"{tSupplierName.Text} təchizatçısının ödəniş hesabatı");
            }
        }

        private void TECHIZATCI_ODENISI_HESABATI_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();
            lookupedittextxhange_main();


        }

        private void lookupedittextxhange_main()
        {
            string query = "select TECHIZATCI_ID,SIRKET_ADI AS N'TƏCHİZATÇI ADI' from COMPANY.TECHIZATCI WHERE IsDeleted = 0";
            var dataTable = DbProsedures.ConvertToDataTable(query);

            lookUpEdit1.Properties.DisplayMember = "TƏCHİZATÇI ADI";
            lookUpEdit1.Properties.ValueMember = "TECHIZATCI_ID";
            lookUpEdit1.Properties.DataSource = dataTable;
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns[0].Visible = false;
        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lookUpEdit1.Text))
                {
                    await DataLoadAsync(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
                }
                else
                {
                    await DataLoadToSupplier(Convert.ToDateTime(dateEdit1.Text),
                                     Convert.ToDateTime(dateEdit2.Text),
                                     Convert.ToInt32(lookUpEdit1.EditValue));
                }
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
            }
        }

        private async Task DataLoadToSupplier(DateTime start, DateTime end, int supplierId)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                await con.OpenAsync();
                string query = "SELECT * FROM dbo.fn_TECHIZATCI_ODENILENLER_hesabat_t_id (CAST(@startDate AS DATE) , CAST(@endDate AS DATE),@supplierId)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@startDate", start);
                    cmd.Parameters.AddWithValue("@endDate", end);
                    cmd.Parameters.AddWithValue("@supplierId", supplierId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            gridControl1.DataSource = dt;
                            gridView1.Columns["QALIQ MƏBLƏĞ"].Summary.Clear();
                            GridColumnSummaryItem debtSum = new GridColumnSummaryItem
                            {
                                FieldName = "QALIQ MƏBLƏĞ",
                                SummaryType = DevExpress.Data.SummaryItemType.Sum,
                                DisplayFormat = "{0:C2}",

                            };
                            gridView1.Columns["QALIQ MƏBLƏĞ"].Summary.Add(debtSum);
                        }
                    }
                }
            }
        }

        private async Task DataLoadAsync(DateTime start, DateTime end)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                string queryString = "SELECT * FROM dbo.fn_TECHIZATCI_ODENILENLER_hesabat (cast(@startDate AS DATE), CAST(@endDate AS DATE))";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(queryString, con))
                {
                    cmd.Parameters.AddWithValue("@startDate", start);
                    cmd.Parameters.AddWithValue("@endDate", end);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            gridControl1.DataSource = dt;
                            gridView1.Columns["QALIQ MƏBLƏĞ"].Summary.Clear();
                            GridColumnSummaryItem debtSum = new GridColumnSummaryItem
                            {
                                FieldName = "QALIQ MƏBLƏĞ",
                                SummaryType = DevExpress.Data.SummaryItemType.Sum,
                                DisplayFormat = "{0:C2}",

                            };
                            gridView1.Columns["QALIQ MƏBLƏĞ"].Summary.Add(debtSum);
                        }
                    }
                }
            }
        }

        private async void gridView1_DoubleClick(object sender, EventArgs e)
        {
            var selectedRow = gridView1.GetFocusedDataRow();
            if (selectedRow == null) return;
            object data = null;

            int malAlisiId = selectedRow.Field<int?>("MAL_ALISI_MAIN_ID") ?? 0;
            int supplierDebtId = selectedRow.Field<int?>("SupplierDebtId") ?? 0;
            int supplierId = selectedRow.Field<int?>("TECHIZATCI_ID") ?? 0;
            string supplierName = selectedRow.Field<string>("TƏCHİZATÇI ADI") ?? null;
            string contractNo = selectedRow.Field<string>("TƏCHİZATÇI FAKTURA №") ?? null;

            if (malAlisiId > 0)
            {
                data = ShowPurchaseDetails(malAlisiId);
            }
            else if (supplierDebtId > 0)
            {
                data = ShowSupplierDebtDetails(supplierDebtId);
            }

            gridControl2.DataSource = data;
            gridView2.Columns["Payment"].Summary.Clear();
            _summaryItemPayment = new GridColumnSummaryItem
            {
                FieldName = "Payment",
                SummaryType = DevExpress.Data.SummaryItemType.Sum,
                DisplayFormat = "N2",

            };
            gridView2.Columns["Payment"].Summary.Add(_summaryItemPayment);

            lookUpEdit1.Enabled = false;
            navigationFrame1.SelectedPage = page2;
            tSupplierName.Text = supplierName;
            tContractNo.Text = contractNo;
            TotalSupplierDebt(supplierId);

            await SupplierDebtDataLoad(supplierDebtId);
            await ProductPurchaseDataLoad(malAlisiId);
        }

        private DataTable ShowPurchaseDetails(int productMainId)
        {
            string query = $@"
 DECLARE @malAlisiMainId INT = {productMainId};

WITH DebtAction AS (
    SELECT 
        t.TARIX AS PayDate,
		t.GAIME_N AS GaimeNo,
        ISNULL(u.AD, 'YOXDUR') AS Username,
        t.ODENIS AS Payment,
        t.ODENIS_TIPI AS PaymentType,
        t.MAL_ALISI_MAIN_ID,
        CAST(t.DATE_ AS DATETIME) AS OrderDate
    FROM TECHIZATCI_ODENIS t
    LEFT JOIN userParol u ON u.id = t._USER_ID
    WHERE t.MAL_ALISI_MAIN_ID = @malAlisiMainId
),
TotalDebt AS (
    SELECT 
        SUM((ISNULL(d.ALIS_GIYMETI,0)-ISNULL(d.ENDIRIM_MEBLEGI,0)) * ISNULL(d.MIGDARI,0)) AS Borc
    FROM MAL_ALISI_DETAILS d
    WHERE d.MAL_ALISI_MAIN_ID = @malAlisiMainId
)
SELECT 
    h.PayDate,
	h.GaimeNo,
    h.Username,
    h.PaymentType,
    h.Payment,
    tb.Borc - SUM(h.Payment) OVER (ORDER BY h.OrderDate, h.MAL_ALISI_MAIN_ID ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS RemainingAmount
FROM DebtAction h
CROSS JOIN TotalDebt tb
ORDER BY h.OrderDate;
    ";

            var data = DbProsedures.ConvertToDataTable(query);
            return data;
        }

        private DataTable ShowSupplierDebtDetails(int supplierDebtId)
        {
            string query = $@"
        DECLARE @supplierDebtId INT = {supplierDebtId};

WITH Hareket AS (
    SELECT 
        t.TARIX AS PayDate,
		t.GAIME_N AS GaimeNo,
        ISNULL(u.AD, 'YOXDUR') AS Username,
        t.ODENIS AS Payment,
        t.ODENIS_TIPI AS PaymentType,
        t.SupplierDebtId,
        CAST(t.DATE_ AS DATETIME) AS SiraTarihi
    FROM TECHIZATCI_ODENIS t
    LEFT JOIN userParol u ON u.id = t._USER_ID
    WHERE t.SupplierDebtId = @supplierDebtId
),
ToplamBorc AS (
    SELECT Amount AS Borc
    FROM COMPANY.SupplierDebt
    WHERE Id = @supplierDebtId
)
SELECT 
    h.PayDate,
	h.GaimeNo,
    h.Username,
    h.PaymentType,
    h.Payment,
    tb.Borc - SUM(h.Payment) OVER (ORDER BY h.SiraTarihi, h.SupplierDebtId ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS RemainingAmount
FROM Hareket h
CROSS JOIN ToplamBorc tb
ORDER BY h.SiraTarihi;
    ";
            var data = DbProsedures.ConvertToDataTable(query);

            return data;
        }

        private void Clear()
        {
            tSupplierName.Clear();
            tContractNo.Clear();
            tContractDebtBalance.Clear();
            tDebtDate.Clear();
            tContractDebt.Clear();
            tTotalDebt.Clear();
            lookUpEdit1.Enabled = true;
            navigationFrame1.SelectedPage = page1;
        }

        private void bBack_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private async void TotalSupplierDebt(int supplierId)
        {
            var debt = await DbProsedures.GET_SupplierTotalDebt(supplierId);

            tTotalDebt.Text = debt.totalAmount.ToString("N2");
        }

        private async Task<(DateTime contractDate, decimal Amount)> ContractDebt_SupplierDebtAsync(int supplierDebtId)
        {
            DateTime contractDate = DateTime.MinValue;
            decimal amount = default;
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                await con.OpenAsync();
                string query = @"SELECT ContractDate, Amount FROM COMPANY.SupplierDebt WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", supplierDebtId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (await reader.ReadAsync())
                        {
                            contractDate = reader.GetDateTime(reader.GetOrdinal("ContractDate"));
                            amount = reader.GetDecimal(reader.GetOrdinal("Amount"));
                        }
                        return (contractDate, amount);
                    }
                }
            }
        }

        private async Task<(DateTime contractDate, decimal Amount)> ContractDebt_ProductPurchaseAsync(int productMainId)
        {
            DateTime contractDate = DateTime.MinValue;
            decimal amount = default;
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                await con.OpenAsync();
                string query = @"SELECT 
    MAX(ma.TARIX) AS ContractDate, 
    SUM(md.YEKUN_MEBLEG) AS Amount
FROM 
    dbo.MAL_ALISI_DETAILS md
INNER JOIN MAL_ALISI_MAIN ma ON ma.MAL_ALISI_MAIN_ID = md.MAL_ALISI_MAIN_ID
WHERE 
   md.MAL_ALISI_MAIN_ID = @Id
GROUP BY 
    md.MAL_ALISI_MAIN_ID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", productMainId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (await reader.ReadAsync())
                        {
                            contractDate = reader.GetDateTime(reader.GetOrdinal("ContractDate"));
                            amount = reader.GetDecimal(reader.GetOrdinal("Amount"));
                        }
                        return (contractDate, amount);
                    }
                }
            }
        }

        private async Task SupplierDebtDataLoad(int supplierDebtId)
        {
            if (supplierDebtId > 0)
            {
              
                var supplierDebtData = await ContractDebt_SupplierDebtAsync(supplierDebtId);
                tDebtDate.Text = supplierDebtData.contractDate == DateTime.MinValue ? null : supplierDebtData.contractDate.ToString("dd.MM.yyyy");
                tContractDebt.Text = supplierDebtData.Amount.ToString("N2");

                var totalPaid = _summaryItemPayment.SummaryValue.ToString();
                decimal balance =  Convert.ToDecimal(tContractDebt.Text) - Convert.ToDecimal(totalPaid);
                tContractDebtBalance.Text = balance.ToString("N2");
            }
        }

        private async Task ProductPurchaseDataLoad(int productMainId)
        {
            if (productMainId > 0)
            {
                var productPurchaseData = await ContractDebt_ProductPurchaseAsync(productMainId);
                tDebtDate.Text = productPurchaseData.contractDate == DateTime.MinValue ? null : productPurchaseData.contractDate.ToString("dd.MM.yyyy");
                tContractDebt.Text = productPurchaseData.Amount.ToString("N2");

                var totalPaid = _summaryItemPayment.SummaryValue.ToString();
                decimal balance = Convert.ToDecimal(tContractDebt.Text) - Convert.ToDecimal(totalPaid);
                tContractDebtBalance.Text = balance.ToString("N2");
            }
        }
    }
}