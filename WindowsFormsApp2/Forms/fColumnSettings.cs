using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsApp2.Forms
{
    public partial class fColumnSettings : DevExpress.XtraEditors.XtraForm
    {
        private readonly string filePath = $@"{Application.StartupPath}\LocalFiles\GridColumnsSettings.json";
        private readonly string _tableName; 
        public fColumnSettings(string tableName)
        {
            InitializeComponent();
            _tableName = tableName;
            JsonDataLoad();
        }

        private void JsonDataLoad()
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);

                var tables = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

                var table = tables.FirstOrDefault(t => t.TableName == _tableName);
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

            settings.RemoveAll(s => s.TableName == _tableName);

            var newSettings = new
            {
                TableName = _tableName,  
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

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (_tableName == "Customers")
            {
                string fieldName = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "FieldName").ToString();
                if (fieldName == "MUSTERILER_ID")
                {
                    e.Cancel = true;
                }
            }
        }
    }
}