using DevExpress.XtraGrid.Localization;
using System;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fGuarantors<TParent> : BaseForm where TParent : BaseForm
    {
        private readonly TParent parentForm;
        private Guarantor _guarantor;
        public fGuarantors(TParent _parent)
        {
            parentForm = _parent;
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void fGuarantors_Load(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ZAMINLER_ID").ToString());

            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM SELECT_ZAMIN_DATA_LOAD(@ID)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _guarantor = FormHelpers.MapReaderToObject<DatabaseClasses.Guarantor>(reader);
                        }
                    }
                }
            }


            var method = parentForm.GetType().GetMethod("ReceiveData");
            if (method != null)
            {
                method.MakeGenericMethod(_guarantor.GetType()).Invoke(parentForm, new object[] { _guarantor });
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
                string companyName = row[2].ToString();
                if (!string.IsNullOrWhiteSpace(Id.ToString()))
                {
                    bool response = DbProsedures.DeleteGuarantor(Id);
                    if (response is true)
                    {
                        Alert($"{companyName} uğurla silindi", Enums.MessageType.Success);
                        Log($"{companyName} zamin silindi");
                        DataLoad();
                    }
                }
            }
        }

        private void bPrint_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Zaminlərin siyahısı");
        }

        private void DataLoad()
        {
            var data = DbProsedures.ConvertToDataTable("SELECT * FROM dbo.fn_ZAMIN()");
            gridControl1.DataSource = data;
            gridView1.Columns[0].Visible = false;
            gridView1.GroupPanelText = $"Zamin sayı: {gridView1.RowCount}";
        }
    }
}