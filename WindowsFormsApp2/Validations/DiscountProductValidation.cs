using System;
using FluentValidation;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;

namespace WindowsFormsApp2.Validations
{
    public class DiscountProductValidation:AbstractValidator<DiscountProduct>
    {
        public DiscountProductValidation()
        {
            RuleFor(x=> x.ProductId).NotEmpty().WithMessage("Məhsul seçimi edilmədi");
            RuleFor(x=> x.DiscountTotal).NotEmpty().WithMessage("Endirim məbləği təyin edilmədi");
            RuleFor(x => x.StartDate).Custom((data, context) =>
            {
                if (data == DateTime.MinValue)
                {
                    context.InstanceToValidate.StartDate = DateTime.Now;
                }
            });
        }
    }
}