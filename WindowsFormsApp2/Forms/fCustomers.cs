using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraGrid.Localization;
using Newtonsoft.Json;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fCustomers<TParent> : BaseForm where TParent : BaseForm
    {
        private readonly TParent parentForm;
        private DatabaseClasses.Customer customer;
        private readonly string filePath = $@"{Application.StartupPath}\LocalFiles\GridColumnsSettings.json";


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
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);

                var tables = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

                var table = tables.FirstOrDefault(t => t.TableName == "Customers");
                if (table != null)
                {
                    List<string> visibleColumns = new List<string>();
                    foreach (var column in table.Columns)
                    {
                        if ((bool)column.Visible == true)
                        {
                            visibleColumns.Add(column.FieldName.ToString());  // Visible olan sütunları ekle
                        }
                    }

                    if (visibleColumns != null)
                    {
                        
                        var data = DbProsedures.ConvertToDataTable("SELECT * FROM dbo.fn_MUSTERI()");

                        foreach (DataColumn column in data.Columns.Cast<DataColumn>().ToList())
                        {
                            // Eğer sütun visibleColumns içinde yoksa, sütunu gizle
                            if (!visibleColumns.Contains(column.ColumnName))
                            {
                                data.Columns.Remove(column);
                            }
                        }


                        gridControl1.DataSource = data;
                        //gridView1.Columns[0].Visible = false;
                        gridView1.GroupPanelText = $"Müştəri sayı: {gridView1.RowCount}";
                    }
                }
            }
            
        }

        private void bShowColumns_Click(object sender, EventArgs e)
        {
            fColumnSettings f = new fColumnSettings();
            f.ShowDialog();
        }
    }
}