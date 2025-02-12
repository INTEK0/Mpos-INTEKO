using WindowsFormsApp2.Forms;

namespace WindowsFormsApp2.Helpers.GenericFormHelpers
{
    public partial class fCustomerDerived : fCustomers<BaseForm>
    {
        public fCustomerDerived() : base(null)
        {

        }
    }
}