using DevExpress.XtraGrid.Localization;
using System;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fSuppliers<TParent> : BaseForm where TParent : BaseForm
    {
        private readonly TParent parentForm;
        private Supplier supplier;
        public fSuppliers(TParent _parent)
        {
            parentForm = _parent;
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void fSuppliers_Load(object sender, EventArgs e)
        {
            SupplierDataLoad();
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("TECHIZATCI_ID").ToString());

            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM SELECT_TECHIZATCI_DATA_LOAD(@SupplierID)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SupplierID", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            supplier = FormHelpers.MapReaderToObject<Supplier>(reader);
                        }
                    }
                }
            }


            var method = parentForm.GetType().GetMethod("ReceiveData");
            if (method != null)
            {
                method.MakeGenericMethod(supplier.GetType()).Invoke(parentForm, new object[] { supplier });
                this.Close();
            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();

            foreach (int item in selectedRows)
            {
                var row = gridView1.GetDataRow(item);
                if (row == null) { return; }
                int Id = Convert.ToInt32(row[0].ToString());
                string companyName = row[0].ToString();
                if (!string.IsNullOrWhiteSpace(Id.ToString()))
                {
                    bool response = DbProsedures.DeleteSupplier(Id);
                    if (response is true)
                    {
                        FormHelpers.Alert($"{companyName} təchizatçısı uğurla silindi", Enums.MessageType.Success);
                        FormHelpers.Log($"{companyName} təçhizatçısı silindi");
                        SupplierDataLoad();
                        break;
                    }
                }
            }
        }

        private void bPrint_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Müştərilərin siyahısı");
        }

        private void SupplierDataLoad()
        {
            var data = DbProsedures.ConvertToDataTable("SELECT * FROM dbo.fn_TECHIZATCI()");
            gridControl1.DataSource = data;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["IsDeleted"].Visible = false;
            gridView1.GroupPanelText = $"Təchizatçı sayı: {gridView1.RowCount}";
        }
    }
}