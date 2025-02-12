using System;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Validations;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;

namespace WindowsFormsApp2.Forms
{
    public partial class fAddGuarantor : BaseForm
    {
        public fAddGuarantor()
        {
            InitializeComponent();
        }

        private void fAddGuarantor_Load(object sender, EventArgs e)
        {
            Clear();
            VetendasliqDataLoad();
            GenderDataLoad();
            bAdd.Text = Enums.GetEnumDescription(Enums.Operation.Add);
            tProccessNo.Text = DbProsedures.GET_GuarantorProccessNo();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (bAdd.Text == Enums.GetEnumDescription(Enums.Operation.Add))
            {
                Add();
            }
            else if (bAdd.Text == Enums.GetEnumDescription(Enums.Operation.Update))
            {
                Edit();
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void bGuarantorList_Click(object sender, EventArgs e)
        {
            fGuarantors<fAddGuarantor> guarantors = new fGuarantors<fAddGuarantor>(this);
            guarantors.ShowDialog();
        }

        private void Add()
        {
            string name, surname, fatherName;

            string fullName = tNameSurname.Text.Trim();
            string[] nameParts = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length >= 3)
            {
                name = nameParts[0];
                surname = nameParts[1];
                fatherName = nameParts[2];
            }
            else
            {
                FormHelpers.Alert(GuarantorValidation.NAMESURNAME_NOTNULLMESSAGE, Enums.MessageType.Warning);
                return;
            }

            Guarantor item = new Guarantor();
            item.ProccessNo = tProccessNo.Text;
            item.CompanyName = tCompanyName.Text.Trim();
            item.Voen = tVoen.Text.Trim();
            item.Name = name;
            item.Surname = surname;
            item.FatherName = fatherName;
            item.DateBirth = dateBirth.DateTime;
            item.SvNo = tSVSerialNumber.Text.Trim();
            item.FinCode = tSVFinKod.Text.Trim();
            item.Address = tAddress.Text.Trim();
            item.ResidentialAddress = tResidentialAddress.Text.Trim();
            item.SV_Start = dateSVStart.DateTime;
            item.SV_End = dateSVEnd.DateTime;
            item.Gender = lookGender.Text;
            item.Nation = lookNation.Text;
            item.Email = tEmail.Text;
            item.MobPhone = tMobPhone.Text;
            item.HomePhone = tHomePhone.Text;
            item.Comment = tComment.Text.Trim();
            item.BankAccountNumber = tBankAccountNumber.Text.Trim();
            item.BankName = tBankName.Text.Trim();
            item.BankVoen = tBankVoen.Text.Trim();
            item.BankCode = tBankCode.Text.Trim();
            item.BankSwift = tBankSwift.Text.Trim();


            var validator = new GuarantorValidation();
            var validateResult = validator.Validate(item);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                    return;
                }
            }

            int response = DbProsedures.InsertGuarantor(item);
            if (response >= 0)
            {
                FormHelpers.Alert("Uğurla yaradıldı", Enums.MessageType.Success);
                FormHelpers.Log($"{tCompanyName.Text} zamini yaradıldı");
                Clear();
                tProccessNo.Text = DbProsedures.GET_GuarantorProccessNo();
            }
        }

        private void Edit()
        {
            string name, surname, fatherName;

            string fullName = tNameSurname.Text.Trim();
            string[] nameParts = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length >= 3)
            {
                name = nameParts[0];
                surname = nameParts[1];
                fatherName = nameParts[2];
            }
            else
            {
                FormHelpers.Alert(GuarantorValidation.NAMESURNAME_NOTNULLMESSAGE, Enums.MessageType.Warning);
                return;
            }

            Guarantor item = new Guarantor();
            item.ID = Convert.ToInt32(lZaminID.Text);
            item.ProccessNo = tProccessNo.Text;
            item.CompanyName = tCompanyName.Text.Trim();
            item.Voen = tVoen.Text.Trim();
            item.Name = name;
            item.Surname = surname;
            item.FatherName = fatherName;
            item.DateBirth = dateBirth.DateTime;
            item.SvNo = tSVSerialNumber.Text.Trim();
            item.FinCode = tSVFinKod.Text.Trim();
            item.Address = tAddress.Text.Trim();
            item.ResidentialAddress = tResidentialAddress.Text.Trim();
            item.SV_Start = dateSVStart.DateTime;
            item.SV_End = dateSVEnd.DateTime;
            item.Gender = lookGender.Text;
            item.Nation = lookNation.Text;
            item.Email = tEmail.Text;
            item.MobPhone = tMobPhone.Text;
            item.HomePhone = tHomePhone.Text;
            item.Comment = tComment.Text.Trim();
            item.BankAccountNumber = tBankAccountNumber.Text.Trim();
            item.BankName = tBankName.Text.Trim();
            item.BankVoen = tBankVoen.Text.Trim();
            item.BankCode = tBankCode.Text.Trim();
            item.BankSwift = tBankSwift.Text.Trim();


            var validator = new GuarantorValidation();
            var validateResult = validator.Validate(item);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                    return;
                }
            }

            bool response = DbProsedures.UpdateGuarantor(item);
            if (response is true)
            {
                string message = $"{tCompanyName.Text} zaminində düzəliş edildi";
                FormHelpers.Alert(message, Enums.MessageType.Success);
                FormHelpers.Log(message);
                Close();
            }
        }

        private void VetendasliqDataLoad()
        {
            string query = "SELECT vetendaslig_id,vetendaslig AS N'VƏTƏNDAŞLIQ' FROM vetendaslig";
            var data = DbProsedures.ConvertToDataTable(query);
            lookNation.Properties.DisplayMember = "VƏTƏNDAŞLIQ";
            lookNation.Properties.ValueMember = "vetendaslig_id";
            lookNation.Properties.DataSource = data;
            lookNation.Properties.PopulateColumns();
            lookNation.Properties.Columns["vetendaslig_id"].Visible = false;
        }

        private void GenderDataLoad()
        {
            string query = "SELECT cins_id,cins AS N'CİNSİ' FROM cins";
            var data = DbProsedures.ConvertToDataTable(query);
            lookGender.Properties.DisplayMember = "CİNSİ";
            lookGender.Properties.ValueMember = "cins_id";
            lookGender.Properties.DataSource = data;
            lookGender.Properties.PopulateColumns();
            lookGender.Properties.Columns["cins_id"].Visible = false;
        }

        private void Clear()
        {
            tCompanyName.Text = string.Empty;
            tVoen.Text = string.Empty;
            tNameSurname.Text = string.Empty;
            dateBirth.Text = null;
            tSVSerialNumber.Text = string.Empty;
            tSVFinKod.Text = string.Empty;
            tAddress.Text = string.Empty;
            tResidentialAddress.Text = string.Empty;
            dateSVStart.Text = string.Empty;
            dateSVEnd.Text = string.Empty;
            lookGender.Text = null;
            lookNation.Text = null;
            tEmail.Text = null;
            tMobPhone.Text = null;
            tHomePhone.Text = null;
            tComment.Text = null;
            tBankAccountNumber.Text = string.Empty;
            tBankName.Text = string.Empty;
            tBankVoen.Text = string.Empty;
            tBankCode.Text = string.Empty;
            tBankSwift.Text = string.Empty;
            lZaminID.Text = "0";
            bAdd.Text = Enums.GetEnumDescription(Enums.Operation.Add);
            tCompanyName.Focus();
        }

        public override void ReceiveData<T>(T data)
        {
            if (data is Guarantor item)
            {
                lZaminID.Text = item.ID.ToString();
                tProccessNo.Text = item.ProccessNo;
                tCompanyName.Text = item.CompanyName;
                tVoen.Text = item.Voen;
                tNameSurname.Text = $"{item?.Name} {item?.Surname} {item?.FatherName}";
                dateBirth.DateTime = item.DateBirth;
                tSVSerialNumber.Text = item.SvNo;
                tSVFinKod.Text = item.FinCode;
                tAddress.Text = item.Address;
                tResidentialAddress.Text = item.ResidentialAddress;
                dateSVStart.DateTime = item.SV_Start;
                dateSVEnd.DateTime = item.SV_End;
                lookGender.SelectedText = item.Gender;
                lookNation.SelectedText = item.Nation;
                tEmail.Text = item.Email;
                tMobPhone.Text = item.MobPhone;
                tHomePhone.Text = item.HomePhone;
                tComment.Text = item.Comment;
                tBankAccountNumber.Text = item.BankAccountNumber;
                tBankVoen.Text = item.BankVoen;
                tBankName.Text = item.BankName;
                tBankCode.Text = item.BankCode;
                tBankSwift.Text = item.BankSwift;
                bAdd.Text = Enums.GetEnumDescription(Enums.Operation.Update);
                tCompanyName.Enabled = false;
            }
        }
    }
}