using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Localization;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class TECHIZATCI_ODENISI_HESABATI : DevExpress.XtraEditors.XtraForm
    {
        private GridColumnSummaryItem _debtSum;
        public TECHIZATCI_ODENISI_HESABATI()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ExcelExport(gridControl1, "Təchizatçı ödənişi hesabatı");
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
                    await GetallDataAsync(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
                }
                else
                {
                    await GetallData_t_id(Convert.ToDateTime(dateEdit1.Text),
                                     Convert.ToDateTime(dateEdit2.Text),
                                     Convert.ToInt32(lookUpEdit1.EditValue));
                }
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
            }
        }

        private async Task GetallData_t_id(DateTime start, DateTime end, int supplierId)
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
                            _debtSum = new GridColumnSummaryItem
                            {
                                FieldName = "QALIQ MƏBLƏĞ",
                                SummaryType = DevExpress.Data.SummaryItemType.Sum,
                                DisplayFormat = "{0:C2}",

                            };
                            gridView1.Columns["QALIQ MƏBLƏĞ"].Summary.Add(_debtSum);
                        }
                    }
                }
            }
        }

        private async Task GetallDataAsync(DateTime start, DateTime end)
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
                            _debtSum = new GridColumnSummaryItem
                            {
                                FieldName = "QALIQ MƏBLƏĞ",
                                SummaryType = DevExpress.Data.SummaryItemType.Sum,
                                DisplayFormat = "{0:C2}",

                            };
                            gridView1.Columns["QALIQ MƏBLƏĞ"].Summary.Add(_debtSum);
                        }
                    }
                }
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
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
                data = ShowPurchaseDetails(malAlisiId);
            else if (supplierDebtId > 0)
                data = ShowSupplierDebtDetails(supplierDebtId);

            gridControl2.DataSource = data;
            gridView2.Columns["Payment"].Summary.Clear();
            GridColumnSummaryItem payment = new GridColumnSummaryItem
            {
                FieldName = "Payment",
                SummaryType = DevExpress.Data.SummaryItemType.Sum,
                DisplayFormat = "{0:N2}",

            };
            gridView2.Columns["Payment"].Summary.Add(payment);


            lookUpEdit1.Enabled = false;
            navigationFrame1.SelectedPage = page2;
            tSupplierName.Text = supplierName;
            tContractNo.Text = contractNo;
            TotalSupplierDebt(supplierId);
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
            decimal debt = await DbProsedures.GET_SupplierTotalDebt(supplierId);
            tTotalDebt.Text = debt.ToString();
        }
    }
}