using DbAccess.Entities;
using FluentValidation;

namespace WindowsFormsApp2.Validations
{
    public class SalePersonnelValidation : AbstractValidator<SalePersonnel>
    {
        public SalePersonnelValidation()
        {
            RuleFor(x=> x.FullName).NotEmpty().WithMessage("Satış personelinin adı daxil edilmədi");
        }
    }
}
