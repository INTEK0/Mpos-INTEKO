using FluentValidation;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Validations
{
    public class DoctorValidation : AbstractValidator<DatabaseClasses.Doctor>
    {
        public DoctorValidation()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Ad və Soyad boş ola bilməz");
        }
    }
}
