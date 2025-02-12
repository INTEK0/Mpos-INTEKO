using FluentValidation;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Validations
{
    public class SupplierValidation: AbstractValidator<DatabaseClasses.Supplier>
    {
        public static readonly string SUPPLIER_ALREADY_EXISTS = "Əlavə etmək istədiyiniz təchizatçı sistemdə mövcuddur";

        public SupplierValidation()
        {
            RuleFor(x => x.SupplierName).NotEmpty().WithMessage("Təchizatçı adını daxil edin");
            //RuleFor(x => x.Voen).NotEmpty().WithMessage("Vöen nömrəsini daxil edin");
        }
    }
}
