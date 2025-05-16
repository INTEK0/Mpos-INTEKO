using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Licence.Forms;

namespace WindowsFormsApp2.Helpers
{
    public class CustomAppContext: ApplicationContext
    {
        public CustomAppContext()
        {
            ShowDeactiveForm();
        }

        private void ShowDeactiveForm()
        {
            var deactive = new fDeactive();
            deactive.LoginRequested += OnLoginRequested; // özel event
            deactive.FormClosed += (s, e) => ExitThread();
            deactive.Show();
        }

        private void OnLoginRequested(object sender, EventArgs e)
        {
            // fDeactive formunu kapat
            (sender as Form)?.Close();

            // fLogin formunu göster
            var loginForm = new avtorizasiya();
            loginForm.FormClosed += (s, args) => ExitThread(); // uygulama login kapanınca da kapanır
            loginForm.Show();
        }
    }
}
