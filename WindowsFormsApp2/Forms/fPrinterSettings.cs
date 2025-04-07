using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management;
using DevExpress.XtraEditors;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.Validations;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;

namespace WindowsFormsApp2.Forms
{
    public partial class fPrinterSettings : DevExpress.XtraEditors.XtraForm
    {
        public fPrinterSettings()
        {
            InitializeComponent();
        }

        private void fPrinterSettings_Load(object sender, EventArgs e)
        {
            PrinterDataLoad();
            GetallData();
            UserDataLoad();
        }

        private void PrinterDataLoad()
        {
            var printerList = new List<PrinterInfo>();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

            foreach (ManagementObject printer in searcher.Get())
            {
                string name = printer["Name"]?.ToString();
                string port = printer["PortName"]?.ToString();

                printerList.Add(new PrinterInfo(name, port));
            }

            lookPrinter.Properties.DataSource = printerList;
            lookPrinter.Properties.DisplayMember = "PrinterAdı";
            lookPrinter.Properties.ValueMember = "Port";
        }

        private void UserDataLoad()
        {
            string strQuery = "SELECT id,AD as N'KASSİR' FROM userParol where IsDeleted = 0";
            var data = DbProsedures.ConvertToDataTable(strQuery);

            lookUser.Properties.DisplayMember = "KASSİR";
            lookUser.Properties.ValueMember = "id";
            lookUser.Properties.DataSource = data;
            lookUser.Properties.NullText = "--Seçin--";
            lookUser.Properties.PopulateColumns();
            lookUser.Properties.Columns[0].Visible = false;
        }

        private void GetallData()
        {
            try
            {
                string queryString = @"SELECT 
p.Id,
p.PrinterName,
p.IpAddress,
p.PrintType,
isnull(u.AD,'') AS Username
FROM PRINTERS p 
INNER JOIN userParol u ON u.id = p.UserId";
                var data = DbProsedures.ConvertToDataTable(queryString);
                gridControl1.DataSource = data;
                gridView1.RefreshData();
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DATALOAD_MESSAGE(e.Message);
            }
        }

        public class PrinterInfo
        {
            public string PrinterAdı { get; set; }
            public string Port { get; set; }

            //public string DisplayName => $"{PrinterAdı} ({Port})";

            public PrinterInfo(string name, string portName)
            {
                PrinterAdı = name;
                Port = portName;
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            var selectedType = groupControl1.Controls.OfType<CheckEdit>().FirstOrDefault(x => x.Checked);
            Printer printer = new Printer
            {
                PrinterName = lookPrinter.Text.ToString(),
                PortName = lookPrinter.EditValue.ToString(),
                PrintType = selectedType.Text,
                UserId = lookUser.EditValue == null ? 0 : Convert.ToInt32(lookUser.EditValue.ToString())
            };


            var validator = new PrinterValidation();
            var validateResult = validator.Validate(printer);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                    return;
                }
            }

            int result = DbProsedures.PrinterAdd(printer);
            if (result == 0)
            {
                FormHelpers.Alert($"Kassa daha öncə əlavə edilib", MessageType.Info);
                return;
            }
            else
            {
                FormHelpers.Log($"{lookPrinter.Text} printeri sistemə əlavə edildi");
            }
            GetallData();
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);

                int Id = Convert.ToInt32(row["Id"].ToString());
                if (Id > 0)
                {
                    DbProsedures.PrinterRemove(Id);
                    FormHelpers.Log($"{row["Username"]} istifadəçisinin printeri({row["PrinterName"]}) silindi");
                }
                GetallData();
            }
        }
    }
}