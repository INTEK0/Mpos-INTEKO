using FluentValidation;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;

namespace WindowsFormsApp2.Validations
{
    public class TerminalValidation : AbstractValidator<Terminal>
    {
        public TerminalValidation()
        {
            RuleFor(x=> x.ModelId).NotEmpty().WithMessage("Kassa model seçimini edin");
            RuleFor(x => x.IpAddress).NotEmpty().WithMessage("Ip adresi daxil edilmədi");
        }
    }
}
