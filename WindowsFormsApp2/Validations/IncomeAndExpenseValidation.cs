using FluentValidation;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;

namespace WindowsFormsApp2.Validations
{
    public class IncomeAndExpenseValidation:AbstractValidator<IncomeAndExpense>
    {
        public IncomeAndExpenseValidation()
        {
            RuleFor(x => x.Header).NotEmpty().WithMessage("Başlıq daxil edilmədi");
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Məbləğ daxil edilmədi");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Məbləğ 0 və ya 0-dan kiçik olabilməz");
        }
    }
}
