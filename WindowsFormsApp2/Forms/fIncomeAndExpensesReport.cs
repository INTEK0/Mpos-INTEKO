using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fIncomeAndExpensesReport : DevExpress.XtraEditors.XtraForm
    {
        public fIncomeAndExpensesReport()
        {
            InitializeComponent();
            GridPanelText(gridExpense);
            GridPanelText(gridIncome);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private enum ReportType
        {
            [Description("Xərc hesabatı")]
            Expenses,
            [Description("Əlavə gəlir hesabatı")]
            Incomes,
        }

        private void fIncomeAndExpensesReport_Load(object sender, EventArgs e)
        {
            var data = Enum.GetValues(typeof(ReportType))
                                                .Cast<ReportType>()
                                                .Select(x => new
                                                {
                                                    Key = (int)x,
                                                    Value = GetEnumDescription(x)
                                                })
                                                .ToList();

            lookReportType.Properties.DataSource = data;
            lookReportType.Properties.DisplayMember = "Value";
            lookReportType.Properties.ValueMember = "Key";
            lookReportType.Properties.PopulateColumns();
            lookReportType.Properties.Columns["Key"].Visible = false;
            lookReportType.EditValue = ReportType.Expenses;

            DateTime dateTime = DateTime.UtcNow.Date;

            dateStart.Text = dateTime.ToShortDateString();
            dateFinish.Text = dateTime.ToShortDateString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var data = (ReportType)lookReportType.EditValue;
            switch (data)
            {
                case ReportType.Expenses:
                    FormHelpers.ExcelExport(gridControl1, "Xərc hesabatı");
                    break;
                case ReportType.Incomes:
                    FormHelpers.ExcelExport(gridControl1, "Əlavə gəlir hesabatı");
                    break;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var data = (ReportType)lookReportType.EditValue;
            switch (data)
            {
                case ReportType.Expenses:
                    ExpensesReport(dateStart.DateTime, dateFinish.DateTime);
                    break;
                case ReportType.Incomes:
                    IncomesReport(dateStart.DateTime, dateFinish.DateTime);
                    break;
            }
        }

        private void IncomesReport(DateTime dateTime1, DateTime dateTime2)
        {
            string query = $@"SELECT 
i.[Id],
i.[IsDeleted],
i.[Type],
i.[Header],
i.[Amount],
i.[Comment],
i.[Date],
i.[UserId],
i.[LogDate]
FROM [IncomeAndExpensesData] i
WHERE IsDeleted = 0 AND i.Date BETWEEN '2025-03-05' AND '2025-03-07';";
            var data = DbProsedures.ConvertToDataTable(query);
        }

        private void ExpensesReport(DateTime dateTime1, DateTime dateTime2)
        {
            throw new NotImplementedException();
        }

        private void lookReportType_EditValueChanged(object sender, EventArgs e)
        {
            var data = (ReportType)lookReportType.EditValue;
            if (data is ReportType.Expenses)
            {
                gridControl1.MainView = gridExpense;
            }
            else if (data is ReportType.Incomes)
            {
                gridControl1.MainView = gridIncome;
            }

            simpleButton1_Click(null, null);
        }
    }
}