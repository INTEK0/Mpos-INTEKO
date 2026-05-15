using System;
using System.Net.Http;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Licence.Entities;
using Licence.Helpers;
using Licence.Helpers.Implementations;
using Licence.Helpers.Services;
using Licence.Services;
using Licence.Validations;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Licence.Forms
{
    public partial class fRegister : DevExpress.XtraEditors.XtraForm
    {
        LocalDateTimeService dateTimeService = new LocalDateTimeService();
        IGuidKeyService guidKeyService;

        public fRegister()
        {
            InitializeComponent();
        }

        private void fRegister_Load(object sender, EventArgs e)
        {
            dateRegister.DateTime = dateTimeService.Current;

            guidKeyService = new GuidGeneratorService();
            tLicenceKey.Text = guidKeyService.CurrentString;

            TerminalDataLoad();
        }

        private void TerminalDataLoad()
        {
            var data = Terminals.SeedTerminalData();
            lookTerminalType.Properties.DataSource = data;
            lookTerminalType.Properties.DisplayMember = "CompanyModel";
            lookTerminalType.Properties.ValueMember = "Id";
            lookTerminalType.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Company"));
            lookTerminalType.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Model"));
        }

        private void tLicenceKey_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string tag = e.Button.Tag?.ToString();
            switch (tag)
            {
                case "Copy":
                    Clipboard.SetText(tLicenceKey.Text);
                    break;
                case "NewKey":
                    guidKeyService = new GuidGeneratorService();
                    tLicenceKey.Text = guidKeyService.CurrentString;
                    break;
            }
        }

        private async void bSave_Click(object sender, EventArgs e)
        {
            User user = new User()
            {
                CompanyName = tTaxNameSurname.Text.Trim(),
                Name = tCompanyName.Text.Trim(),
                Voen = tVoen.Text.Trim(),
                CompanyCode = tCompanyCode.Text.Trim(),
                Address = tAddress.Text.Trim(),
                Phone = tPhone.Text.Trim(),
                ContractNo = tContractNo.Text.Trim(),
                RegisterDate = dateRegister.DateTime,
                TerminalModel = lookTerminalType.Text,
                TerminalSerialNumber = tTerminalSN.Text.Trim(),
                LicenceVersion = Application.ProductVersion,
                IsActive = chIsActive.Checked,
                LicenceExpireDate = dateRegister.DateTime.AddMonths(1),
                CustomerStatus = true
            };

            var validator = new UserValidation();
            var validateResult = validator.Validate(user);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    XtraMessageBox.Show(error.ErrorMessage, "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string json = JsonConvert.SerializeObject(user, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            bool IsSuccess = await LicenseService.Instance.Create(json, tLicenceKey.Text.Trim());
            if (IsSuccess)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("ProductID", tLicenceKey.Text.Trim());
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("Company", tTaxNameSurname.Text);
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("Voen", tVoen.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }


        }
    }
}