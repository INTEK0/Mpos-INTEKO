using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fCustomers<TParent> : BaseForm where TParent : BaseForm
    {
        private readonly TParent parentForm;
        private DatabaseClasses.Customer customer;

        public fCustomers(TParent _parent)
        {
            parentForm = _parent;
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void fCustomers_Load(object sender, EventArgs e)
        {
            CustomerDataLoad();
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();

            foreach (int item in selectedRows)
            {
                var row = gridView1.GetDataRow(item);
                if (row == null) { return; }
                int customerID = Convert.ToInt32(row[0].ToString());
                string companyName = row[0].ToString();
                if (!string.IsNullOrWhiteSpace(customerID.ToString()))
                {
                    bool response = DbProsedures.DeleteCustomer(customerID);
                    if (response is true)
                    {
                        Alert($"{companyName} müştərisi uğurla silindi", Enums.MessageType.Success);
                        Log($"{companyName} müştərisi silindi");
                        CustomerDataLoad();
                    }
                }
            }
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("MUSTERILER_ID").ToString());

            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM SELECT_MUSTERI_DATA_LOAD(@CustomerID)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer = FormHelpers.MapReaderToObject<DatabaseClasses.Customer>(reader);
                        }
                    }
                }
            }


            var method = parentForm.GetType().GetMethod("ReceiveData");
            if (method != null)
            {
                method.MakeGenericMethod(customer.GetType()).Invoke(parentForm, new object[] { customer });
                this.Close();
            }
        }

        private void bPrint_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Müştərilərin siyahısı");
        }

        private void CustomerDataLoad()
        {
            var data = DbProsedures.ConvertToDataTable("SELECT * FROM dbo.fn_MUSTERI()");
            gridControl1.DataSource = data;
            gridView1.Columns[0].Visible = false;
            gridView1.GroupPanelText = $"Müştəri sayı: {gridView1.RowCount}";
        }
    }
}