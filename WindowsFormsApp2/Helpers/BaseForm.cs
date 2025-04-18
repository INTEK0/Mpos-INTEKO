using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace WindowsFormsApp2.Helpers
{
    public class BaseForm : XtraForm
    {
        public virtual void ReceiveData<T>(T data)
        {

        }

        public class CustomLocalizer : Localizer
        {
            public override string GetLocalizedString(StringId id)
            {
                switch (id)
                {
                    case StringId.DateEditClear:
                        return "Təmizlə";
                    default:
                        return base.GetLocalizedString(id);
                }
            }
        }
    }
}