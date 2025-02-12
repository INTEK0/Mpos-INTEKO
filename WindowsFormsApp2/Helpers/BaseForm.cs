using DevExpress.XtraEditors;

namespace WindowsFormsApp2.Helpers
{
    public class BaseForm : XtraForm
    {
        public virtual void ReceiveData<T>(T data)
        {

        }
    }
}