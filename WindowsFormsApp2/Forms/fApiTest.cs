using System;
using DevExpress.XtraEditors;
using WindowsFormsApp2.App.Application;
using WindowsFormsApp2.App.Services;
using static WindowsFormsApp2.App.Helpers.Enums;

namespace WindowsFormsApp2.Forms
{
    public partial class fApiTest : XtraForm
    {
        public fApiTest()
        {
            InitializeComponent();
        }

        private  void fApiTest_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var facade = new SyncFacade();
            var result = await facade.SendProfitAsync();

            if (result.Ok)
                XtraMessageBox.Show($"Uğurlu: {result.Inserted} sətir göndərildi");
            else
                XtraMessageBox.Show($"Xəta: {result.Error}");
        }
    }
}