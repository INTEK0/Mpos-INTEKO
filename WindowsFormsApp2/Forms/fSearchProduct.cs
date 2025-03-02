using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fSearchProduct<TParent> : BaseForm where TParent : BaseForm
    {
        private readonly int supplierId;
        private readonly int categoryId;
        private readonly TParent parentForm;
        public fSearchProduct(int _supplierId, int _categoryId, TParent _parent)
        {
            InitializeComponent();
            supplierId = _supplierId;
            categoryId = _categoryId;
            parentForm = _parent;
            GetDataLoad();
            GridLocalizer.Active = new MyGridLocalizer();
            GridPanelText(gridView1);
        }

        private void GetDataLoad()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string query = @"
                SELECT
                D.MAL_ALISI_DETAILS_ID as 'ProductID',
                D.MEHSUL_ADI as N'MƏHSUL ADI',
                D.MEHSUL_KODU as N'MƏHSUL KODU', 
                D.ALIS_GIYMETI as N'SON ALIŞ QİYMƏTİ',
                d.SATIS_GIYMETI as N'SATIŞ QİYMƏTİ',
                d.BARKOD,
                V.VAHIDLER_ID as N'VAHİD', 
                TAX.EDV_ID AS 'VERGİ',
                d.SEKIL
                from MAL_ALISI_MAIN M 
                INNER JOIN MAL_ALISI_DETAILS D 
                LEFT JOIN Vahidler V ON D.VAHID = V.VAHIDLER_ID 
                LEFT JOIN VERGI_DERECESI TAX ON D.VERGI_DERECESI = TAX.EDV_ID ON M.MAL_ALISI_MAIN_ID = D.MAL_ALISI_MAIN_ID 
                WHERE M.TECHIZATCI_ID = @pricePoint
                AND d.KATEGORIYA = @categoryID
                AND D.IsDeleted = 0";

                using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@pricePoint", supplierId);
                        cmd.Parameters.AddWithValue("@categoryID", categoryId);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                gridControl1.DataSource = dt;
                                gridView1.Columns[0].Visible = false;
                                gridView1.Columns["BARKOD"].Visible = false;
                                gridView1.Columns["VAHİD"].Visible = false;
                                gridView1.Columns["VERGİ"].Visible = false;
                                gridView1.Columns["SEKIL"].Visible = false;
                                gridView1.OptionsSelection.MultiSelect = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                GridPanelText(gridView1);
                GridLocalizer.Active = new MyGridLocalizer();
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                DatabaseClasses.Products products = new DatabaseClasses.Products
                {
                    Id = Convert.ToInt32(dr[0].ToString()),
                    ProductName = dr[1].ToString(),
                    ProductCode = dr[2].ToString(),
                    PurchasePrice = Convert.ToDecimal(dr[3].ToString()),
                    SalePrice = Convert.ToDecimal(dr[4].ToString()),
                    Barocde = dr[5].ToString(),
                    UnitId = Convert.ToInt32(dr[6].ToString()),
                    TaxId = Convert.ToInt32(dr[7].ToString()),
                    ProductImage = dr[8] != DBNull.Value ? (byte[])dr[8] : null,
                };

                if (parentForm.Name is "fAddProduct" && products != null)
                {
                    var method = parentForm.GetType().GetMethod("ReceiveData");
                    if (method != null)
                    {
                        method.MakeGenericMethod(products.GetType()).Invoke(parentForm, new object[] { products });
                        this.Close();
                    }
                }
            }
        }
    }
}