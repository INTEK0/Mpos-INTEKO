using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ExcelDataReader;

namespace WindowsFormsApp2.Forms
{
    public partial class fPrinterSettings : DevExpress.XtraEditors.XtraForm
    {
        public fPrinterSettings()
        {
            InitializeComponent();
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            //using (OpenFileDialog openFile = new OpenFileDialog())
            //{
            //    openFile.Title = "Support - Xanlaroğlu";
            //    openFile.Filter = "TSPL Fayl seçimi|.tspl";
            //    if (openFile.ShowDialog() is DialogResult.OK)
            //    {
            //        tFilePath.Text = openFile.FileName;
            //        using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
            //        {
            //            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
            //            {
            //                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
            //                {
            //                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
            //                });
            //                tableCollection = result.Tables;
            //                comboBox1.Items.Clear();
            //                foreach (System.Data.DataTable table in tableCollection)
            //                    comboBox1.Items.Add(table.TableName);
            //            }

            //        }
            //    }
            //}
        }
    }
}