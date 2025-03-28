using FluentValidation;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Validations
{
    public class TereziValidation:AbstractValidator<DatabaseClasses.Terezi>
    {
        public TereziValidation()
        {
            RuleFor(x => x.ModelId).GreaterThan(0).WithMessage("Tərəzi model seçimini edin");
            RuleFor(x => x.IpAddress).NotEmpty().WithMessage("Ip adres daxil edilmədi");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("İstifadəçi seçimi edilmədi");
        }
    }
}
