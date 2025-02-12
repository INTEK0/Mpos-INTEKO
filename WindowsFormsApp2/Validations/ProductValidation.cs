using FluentValidation;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Validations
{
    public class ProductValidation:AbstractValidator<DatabaseClasses.ProductsDetail>
    {
        public ProductValidation()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kateqoriya seçimi edilmədi");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Məhsul adını daxil edin");
            RuleFor(x => x.Barocde).NotEmpty().WithMessage("Barkodu daxil edin");
            RuleFor(x => x.ProductCode).NotEmpty().WithMessage("Məhsul kodunu daxil edin");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Miqdarını daxil edin daxil edin");
            RuleFor(x => x.UnitName).NotEmpty().WithMessage("Vahid seçimi edilmədi");
            RuleFor(x => x.TaxName).NotEmpty().WithMessage("Vergi dərəcəsi təyin edilmədi");

            RuleFor(x => x.SalePrice).GreaterThan(0).WithMessage("Satış qiyməti 0 və ya 0-dan kiçik olabilməz");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Miqdar 0 və ya 0-dan kiçik olabilməz");
        }
    }
}