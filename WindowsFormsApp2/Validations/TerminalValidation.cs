using FluentValidation;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;

namespace WindowsFormsApp2.Validations
{
    public class TerminalValidation : AbstractValidator<Terminal>
    {
        public TerminalValidation()
        {
            RuleFor(x=> x.ModelId).GreaterThan(0).WithMessage("Kassa model seçimini edin");
            RuleFor(x => x.IpAddress).NotEmpty().WithMessage("Ip adresi daxil edilmədi");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("İstifadəçi seçimi edilmədi");
            RuleFor(x => x.BankName).Custom((name, context) =>
            {
                if (string.IsNullOrWhiteSpace(name) || name == "--Bank seçimi--")
                {
                    context.InstanceToValidate.BankName = "";
                }
            });
        }
    }
}
