using FluentValidation;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Validations
{
    public class CompanyValidation: AbstractValidator<DatabaseClasses.Company>
    {
        public CompanyValidation()
        {
            RuleFor(x=> x.CompanyName).NotEmpty().WithMessage("Vergi ödəyicisinin adını daxil edin");
            RuleFor(x=> x.Voen).NotEmpty().WithMessage("VÖEN nömrəsini daxil edin");
            RuleFor(x=> x.CompanyCode).NotEmpty().WithMessage("Obyekt kodunu daxil edin");
            RuleFor(x=> x.Address).NotEmpty().WithMessage("Obyektin ünvanını daxil edin");
            RuleFor(x=> x.Phone).NotEmpty().WithMessage("Əlaqə nömrəsini daxil edin");
            //RuleFor(x=> x.Email).EmailAddress().WithMessage("Elektron poçt ünvanı düzgün deyil");
            //RuleFor(x=> x.Email).NotEmpty().WithMessage("Elektron poçt ünvanını daxil edin");
            RuleFor(x=> x.User).NotEmpty().WithMessage("Məsul şəxsin adını daxil edin");
            RuleFor(x=> x.DateRegister).NotEmpty().WithMessage("Qeydiyyat tarixi seçimini edin");
        }
    }
}