using FluentValidation;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Validations
{
    public class GuarantorValidation: AbstractValidator<DatabaseClasses.Guarantor>
    {
        public static readonly string NAMESURNAME_NOTNULLMESSAGE = "Ad, Soyad, Ata adı məlumatları doğru daxil edilmədi";
        public GuarantorValidation()
        {
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("F.Şəxs vəya H.Şəxsin adını daxil edin");
            RuleFor(x => x.Voen).NotEmpty().WithMessage("VÖEN kodunu daxil edin");
            //RuleFor(x=> x.DateBirth).NotEmpty().WithMessage("Doğum tarixini daxil edin");
            //RuleFor(x=> x.SvNo).NotEmpty().WithMessage("ŞV seriya nömrəsini daxil edin");
            //RuleFor(x=> x.FinCode).NotEmpty().WithMessage("ŞV FİN kodunu daxil edin");
            //RuleFor(x=> x.Address).NotEmpty().WithMessage("Ünvanı daxil edin");
            //RuleFor(x=> x.ResidentialAddress).NotEmpty().WithMessage("Faktiki yaşayış ünvanını daxil edin");
            //RuleFor(x=> x.SV_Start).NotEmpty().WithMessage("ŞV verilmə tarixini daxil edin");
            //RuleFor(x=> x.SV_End).NotEmpty().WithMessage("ŞV bitmə tarixini daxil edin");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Cinsiyyət seçimi edilmədi");
            //RuleFor(x=> x.Nation).NotEmpty().WithMessage("Vətəndaşlıq seçimi edilmədi");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-poçt ünvanını daxil edin");
            RuleFor(x => x.Email).EmailAddress().WithMessage("E-poçt ünvanı düzgün daxil edilmədi");
            RuleFor(x => x.MobPhone).NotEmpty().WithMessage("Mobil nömrəni daxil edin");
        }
    }
}
