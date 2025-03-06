using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fQuickSale : DevExpress.XtraEditors.XtraForm
    {
        private readonly string AddQuickSaleProduct = "AddQuickSaleProduct";
        private readonly string DeleteQuickSaleProduct = "DeleteQuickSaleProduct";
        public fQuickSale()
        {
            InitializeComponent();
            GridPanelText(gridProducts);
            GridPanelText(gridSelected);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void ProductsLoad()
        {
            string query = $@"WITH RankedProducts AS (
    SELECT 
        ct.SIRKET_ADI,
        md.MEHSUL_ADI,
        md.SATIS_GIYMETI,
        ROW_NUMBER() OVER (PARTITION BY md.MEHSUL_ADI, ct.SIRKET_ADI ORDER BY am.mal_details_id DESC) AS rn
    FROM ANBAR_MAGAZA am
    INNER JOIN MAL_ALISI_DETAILS md ON am.mal_details_id = md.MAL_ALISI_DETAILS_ID
    INNER JOIN MAL_ALISI_MAIN m ON m.MAL_ALISI_MAIN_ID = md.MAL_ALISI_MAIN_ID
    INNER JOIN COMPANY.TECHIZATCI ct ON m.TECHIZATCI_ID = ct.TECHIZATCI_ID
    WHERE md.ShowPosScreen = 0
    AND md.IsDeleted = 0
)

SELECT 
    SIRKET_ADI AS N'TƏCHİZATÇI ADI',

    MEHSUL_ADI AS N'MƏHSUL ADI',
    SATIS_GIYMETI AS N'SATIŞ QİYMƏTİ'
FROM RankedProducts
WHERE rn = 1;";

            var data = DbProsedures.ConvertToDataTable(query);
            gridControlProducts.DataSource = data;
        }

        private void SelectedProductsLoad()
        {
            string query = $@"WITH RankedProducts AS (
    SELECT 
        ct.SIRKET_ADI,
        md.MEHSUL_ADI,
        md.SATIS_GIYMETI,
        ROW_NUMBER() OVER (PARTITION BY md.MEHSUL_ADI, ct.SIRKET_ADI ORDER BY am.mal_details_id DESC) AS rn
    FROM ANBAR_MAGAZA am
    INNER JOIN MAL_ALISI_DETAILS md ON am.mal_details_id = md.MAL_ALISI_DETAILS_ID
    INNER JOIN MAL_ALISI_MAIN m ON m.MAL_ALISI_MAIN_ID = md.MAL_ALISI_MAIN_ID
    INNER JOIN COMPANY.TECHIZATCI ct ON m.TECHIZATCI_ID = ct.TECHIZATCI_ID
    WHERE md.ShowPosScreen = 1
    AND md.IsDeleted = 0
)

SELECT 
    SIRKET_ADI AS N'TƏCHİZATÇI ADI',

    MEHSUL_ADI AS N'MƏHSUL ADI',
    SATIS_GIYMETI AS N'SATIŞ QİYMƏTİ'
FROM RankedProducts
WHERE rn = 1;
";

            var data = DbProsedures.ConvertToDataTable(query);
            gridControlSelected.DataSource = data;
        }

        private void Add()
        {
            int[] selectedRows = gridProducts.GetSelectedRows();

            foreach (int item in selectedRows)
            {
                var row = gridProducts.GetDataRow(item);
                string product = row[1].ToString();
                string supplier = row[0].ToString();
                if (!string.IsNullOrWhiteSpace(product))
                {
                    using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(AddQuickSaleProduct, con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlParameter param;
                            param = cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, int.MaxValue);
                            param.Value = product;
                            param = cmd.Parameters.Add("@SupplierName", SqlDbType.NVarChar, int.MaxValue);
                            param.Value = supplier;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            SelectedProductsLoad();
            ProductsLoad();
        }

        private void Delete()
        {
            int[] selectedRows = gridSelected.GetSelectedRows();

            foreach (int item in selectedRows)
            {
                var row = gridSelected.GetDataRow(item);

                string product = row[1].ToString();
                string supplier = row[0].ToString();

                if (!string.IsNullOrWhiteSpace(product))
                {
                    using (SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(DeleteQuickSaleProduct, con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlParameter param;
                            param = cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, int.MaxValue);
                            param.Value = product;
                            param = cmd.Parameters.Add("@SupplierName", SqlDbType.NVarChar, int.MaxValue);
                            param.Value = supplier;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            SelectedProductsLoad();
            ProductsLoad();
        }

        private void fQuickSale_Load(object sender, EventArgs e)
        {
            ProductsLoad();
            SelectedProductsLoad();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }
    }
}