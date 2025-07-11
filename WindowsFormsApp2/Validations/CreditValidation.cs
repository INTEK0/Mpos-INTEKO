using FluentValidation;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Validations
{
    public class CreditValidation:AbstractValidator<DatabaseClasses.CreditMain>
    {
        public CreditValidation()
        {
            RuleFor(x => x.ContractNo).NotEmpty().WithMessage("Müqavilə nömrəsi daxil edilmədi");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Müştəri seçimi edilmədi");
            RuleFor(x => x.ZaminId).NotEmpty().WithMessage("Zamin seçimi edilmədi");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Məhsul seçimi edilmədi");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Məhsul miqdarı 0-dan böyük olmalıdır");
            RuleFor(x => x.SalePrice).GreaterThan(0).WithMessage("Satış qiyməti 0-dan böyük olmalıdır");
            RuleFor(x => x.IlkinOdenis).GreaterThanOrEqualTo(0).WithMessage("İlkin ödəniş məbləği neqativ dəyər ola bilməz !");
            RuleFor(x => x.Taksit).NotEmpty().WithMessage("Kredit müddəti daxil edilmədi");

        }
    }
}
