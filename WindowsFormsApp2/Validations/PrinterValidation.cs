using FluentValidation;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Validations
{
    public class PrinterValidation:AbstractValidator<DatabaseClasses.Printer>
    {
        public PrinterValidation()
        {
            RuleFor(x=> x.PrinterName).NotEmpty().WithMessage("Printer seçimi edilmədi");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("İstifadəçi seçimi edilmədi");
        }
    }
}