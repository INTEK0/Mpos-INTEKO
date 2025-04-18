using System;
using FluentValidation;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;

namespace WindowsFormsApp2.Validations
{
    public class DiscountProductValidation : AbstractValidator<DiscountProduct>
    {
        public DiscountProductValidation()
        {
            RuleFor(x => x.Barcode).NotEmpty().WithMessage("Məhsul seçimi edilmədi");

            RuleFor(x => x)
           .Must(x => x.DiscountPercent > 0 || x.DiscountAmount > 0)
           .WithMessage("Endirim məbləği təyin edilmədi");

            RuleFor(x => x.StartDate).Custom((data, context) =>
            {
                if (data == DateTime.MinValue)
                {
                    context.InstanceToValidate.StartDate = DateTime.Now;
                }
                else if (data < DateTime.Now.Date)
                {
                    context.AddFailure("Başlanğıc tarixi bugünün tarixindən kiçik ola bilməz");
                }
            });

            RuleFor(x => x.EndDate).Custom((data, context) =>
            {
                if (data == DateTime.MinValue)
                {
                    context.InstanceToValidate.EndDate = null;
                }
            });

            //Null deyilsə bitiş tarixi başlanğıc tarixindən kiçik olabilməz validasiyası işləyəcək.
            RuleFor(x => x.EndDate)
     .GreaterThanOrEqualTo(x => x.StartDate)
     .WithMessage("Bitiş tarixi, başlanğıc tarixindən kiçik ola bilməz")
     .When(x => x.EndDate.HasValue);



            RuleFor(x => x.DiscountTotal)
             .GreaterThanOrEqualTo(0)
             .WithMessage("Endirim məbləği sıfır vəya sıfırdan kiçik ola bilməz");
        }
    }
}