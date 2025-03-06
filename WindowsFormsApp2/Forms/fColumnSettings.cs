using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Forms
{
    public partial class fColumnSettings : DevExpress.XtraEditors.XtraForm
    {
        private readonly string filePath = $@"{Application.StartupPath}\LocalFiles\GridColumnsSettings.json";
        public fColumnSettings()
        {
            InitializeComponent();
            JsonDataLoad();
        }

        private void JsonDataLoad()
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);

                var tables = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

                var table = tables.FirstOrDefault(t => t.TableName == "Customers");
                if (table != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("FieldName");
                    dt.Columns.Add("Visible", typeof(bool));

                    foreach (var column in table.Columns)
                    {
                        dt.Rows.Add(column.FieldName.ToString(), (bool)column.Visible);
                    }

                    gridControl1.DataSource = dt;

                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        bool isVisible = (bool)gridView1.GetRowCellValue(i, "Visible");
                        if (isVisible)
                        {
                            gridView1.SelectRow(i);
                        }
                    }
                }
            }
        }

        private void SaveSettings()
        {
            int rowCount = gridView1.RowCount;
            List<Dictionary<string, object>> allRows = new List<Dictionary<string, object>>();

            for (int i = 0; i < rowCount; i++)
            {
                var rowData = new Dictionary<string, object>();

                string name = gridView1.GetRowCellValue(i, "FieldName").ToString();
                bool isVisible = (bool)gridView1.GetRowCellValue(i, "Visible");
                rowData["FieldName"] = name;   
                rowData["Visible"] = isVisible; 

                allRows.Add(rowData); 
            }


            string jsonData = File.ReadAllText(filePath);
            var settings = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

            settings.RemoveAll(s => s.TableName == "Customers");

            var newSettings = new
            {
                TableName = "Customers",  
                Columns = allRows        
            };

            settings.Add(newSettings);

            File.WriteAllText(filePath, JsonConvert.SerializeObject(settings, Formatting.Indented));
        }

        private void fColumnSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}