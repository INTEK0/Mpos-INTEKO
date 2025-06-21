using FluentValidation;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Validations
{
    public class UserRoleValidation : AbstractValidator<DatabaseClasses.UserRole>
    {
        public UserRoleValidation()
        {

        }
    }
}
