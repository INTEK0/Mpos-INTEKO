using System;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Validations;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;

namespace WindowsFormsApp2.Forms
{
    public partial class fAddIncomeAndExpenses : DevExpress.XtraEditors.XtraForm
    {
        private readonly Enums.SelectedDataType _type;
        private readonly string _headerName;
        public fAddIncomeAndExpenses(Enums.SelectedDataType type, string header)
        {
            InitializeComponent();
            _type = type;
            _headerName = header;
        }

        private void fAddIncomeAndExpenses_Load(object sender, EventArgs e)
        {
            switch (_type)
            {
                case Enums.SelectedDataType.Income:
                    lHeader.Text = "GƏLİR ƏLAVƏSİ";
                    tHeader.Text = _headerName;
                    this.Text = lHeader.Text;
                    break;
                case Enums.SelectedDataType.Expense:
                    lHeader.Text = "XƏRC ƏLAVƏSİ";
                    tHeader.Text = _headerName;
                    this.Text = lHeader.Text;
                    break;
            }
            dateEdit1.DateTime = DateTime.Now;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            AddIncomeAndExpense();
        }

        private async void AddIncomeAndExpense()
        {
            IncomeAndExpense item = new IncomeAndExpense
            {
                Type = _type,
                Header = tHeader.Text.Trim(),
                Amount = Convert.ToDecimal(tAmount.Text),
                Comment = tComment.Text.Trim(),
                Date = dateEdit1.DateTime,
            };

            var validator = new IncomeAndExpenseValidation();
            var validateResult = validator.Validate(item);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                    return;
                }
            }

            int result = await DbProsedures.InsertIncomeAndExpense(item);
            if (result > 0)
            {
                switch (_type)
                {
                    case Enums.SelectedDataType.Income:
                        FormHelpers.Alert("Yeni əlavə gəlir uğurla yaradıldı", Enums.MessageType.Success);
                        break;
                    case Enums.SelectedDataType.Expense:
                        FormHelpers.Alert("Yeni xərc uğurla yaradıldı", Enums.MessageType.Success);
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                        break;
                }
                Clear();
            }
        }

        private void Clear()
        {
            tHeader.Text = null;
            tAmount.EditValue = 0 ;
            tComment.Text = null;
            dateEdit1.Text = null;
            tHeader.Focus();
        }
    }
}