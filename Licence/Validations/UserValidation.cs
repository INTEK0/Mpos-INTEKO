using Licence.Entities;
using FluentValidation;
using static Licence.Helpers.Enums;

namespace Licence.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(x => x.CompanyName).NotEmpty()
                                       .WithMessage("Obyektin adı daxil edilmədi");

            RuleFor(x => x.Name).NotEmpty()
                                      .WithMessage("Vergi ödəyicisinin adı daxil edilməsi");

            RuleFor(x => x.Voen).MinimumLength(10)
                                .WithMessage("VÖEN nömrəsi minimum 10 simvoldan ibarət olmalıdır");

            RuleFor(x => x.CompanyCode).NotEmpty()
                                .WithMessage("Obyekt kodu daxil edilmədi");

            //RuleFor(x => x.Phone).NotEmpty()
            //                     .NotNull()
            //                     .WithMessage("Telefon nömrəsi daxil edilmədi");

            RuleFor(x => x.Address).NotEmpty()
                                   .WithMessage("Ünvan daxil edilmədi");

            RuleFor(x => x.TerminalModel).NotEmpty()
                          .WithMessage("Kassa seçimi edilmədi");

            RuleFor(x => x.TerminalSerialNumber).NotEmpty()
                                       .When(x => x.TerminalModel != nameof(TerminalType.YOXDUR))
                                       .When(x => x.TerminalModel != nameof(TerminalType.XPRINTER))
                                       .WithMessage("Kassanın seriya nömrəsi daxil edilmədi");

        }
    }
}
