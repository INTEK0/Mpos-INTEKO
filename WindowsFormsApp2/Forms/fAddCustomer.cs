using System;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Validations;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;

namespace WindowsFormsApp2.Forms
{
    public partial class fAddCustomer : BaseForm
    {
        public fAddCustomer()
        {
            InitializeComponent();
        }

        private void fAddCustomer_Load(object sender, EventArgs e)
        {
            Clear();
            VetendasliqDataLoad();
            GenderDataLoad();
            bAdd.Text = Enums.GetEnumDescription(Enums.Operation.Add);
            tProccessNo.Text = DbProsedures.GET_CustomerProccessNo();
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

        private void bCustomerList_Click(object sender, EventArgs e)
        {
            fCustomers<fAddCustomer> customers = new fCustomers<fAddCustomer>(this);
            customers.ShowDialog();
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
                FormHelpers.Alert(CustomerValidation.NAMESURNAME_NOTNULLMESSAGE, Enums.MessageType.Warning);
                return;
            }

            Customer customer = new Customer();
            customer.ProccessNo = tProccessNo.Text;
            customer.CompanyName = tCompanyName.Text.Trim();
            customer.Voen = tVoen.Text.Trim();
            customer.Name = name;
            customer.Surname = surname;
            customer.FatherName = fatherName;
            customer.DateBirth = dateBirth.DateTime;
            customer.SvNo = tSVSerialNumber.Text.Trim();
            customer.FinCode = tSVFinKod.Text.Trim();
            customer.Address = tAddress.Text.Trim();
            customer.ResidentialAddress = tResidentialAddress.Text.Trim();
            customer.SV_Start = dateSVStart.DateTime;
            customer.SV_End = dateSVEnd.DateTime;
            customer.Gender = lookGender.Text;
            customer.Nation = lookNation.Text;
            customer.Email = tEmail.Text;
            customer.MobPhone = tMobPhone.Text;
            customer.HomePhone = tHomePhone.Text;
            customer.Comment = tComment.Text.Trim();
            customer.BankAccountNumber = tBankAccountNumber.Text.Trim();
            customer.BankName = tBankName.Text.Trim();
            customer.BankVoen = tBankVoen.Text.Trim();
            customer.BankCode = tBankCode.Text.Trim();
            customer.BankSwift = tBankSwift.Text.Trim();


            var validator = new CustomerValidation();
            var validateResult = validator.Validate(customer);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                    return;
                }
            }

            int response = DbProsedures.InsertCustomer(customer);
            if (response >= 0)
            {
                FormHelpers.Alert($"{tCompanyName.Text} müştərisi uğurla yaradıldı", Enums.MessageType.Success);
                FormHelpers.Log($"{tCompanyName.Text} müştərisi yaradıldı");
                Clear();
                tProccessNo.Text = DbProsedures.GET_CustomerProccessNo();
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
                FormHelpers.Alert(CustomerValidation.NAMESURNAME_NOTNULLMESSAGE, Enums.MessageType.Warning);
                return;
            }

            Customer customer = new Customer();
            customer.CustomerID = Convert.ToInt32(lCustomerID.Text);
            customer.ProccessNo = tProccessNo.Text;
            customer.CompanyName = tCompanyName.Text.Trim();
            customer.Voen = tVoen.Text.Trim();
            customer.Name = name;
            customer.Surname = surname;
            customer.FatherName = fatherName;
            customer.DateBirth = dateBirth.DateTime;
            customer.SvNo = tSVSerialNumber.Text.Trim();
            customer.FinCode = tSVFinKod.Text.Trim();
            customer.Address = tAddress.Text.Trim();
            customer.ResidentialAddress = tResidentialAddress.Text.Trim();
            customer.SV_Start = dateSVStart.DateTime;
            customer.SV_End = dateSVEnd.DateTime;
            customer.Gender = lookGender.Text;
            customer.Nation = lookNation.Text;
            customer.Email = tEmail.Text;
            customer.MobPhone = tMobPhone.Text;
            customer.HomePhone = tHomePhone.Text;
            customer.Comment = tComment.Text.Trim();
            customer.BankAccountNumber = tBankAccountNumber.Text.Trim();
            customer.BankName = tBankName.Text.Trim();
            customer.BankVoen = tBankVoen.Text.Trim();
            customer.BankCode = tBankCode.Text.Trim();
            customer.BankSwift = tBankSwift.Text.Trim();


            var validator = new CustomerValidation();
            var validateResult = validator.Validate(customer);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                    return;
                }
            }

            bool response = DbProsedures.UpdateCustomer(customer);
            if (response is true)
            {
                string message = $"{tCompanyName.Text} müştərisində düzəliş edildi";
                FormHelpers.Alert(message, Enums.MessageType.Success);
                FormHelpers.Log(message);
                Close();
            }
        }

        public override void ReceiveData<T>(T data)
        {
            if (data is Customer customer)
            {
                lCustomerID.Text = customer.CustomerID.ToString();
                tProccessNo.Text = customer.ProccessNo;
                tCompanyName.Text = customer.CompanyName;
                tVoen.Text = customer.Voen;
                tNameSurname.Text = $"{customer?.Name} {customer?.Surname} {customer?.FatherName}";
                dateBirth.DateTime = customer.DateBirth;
                tSVSerialNumber.Text = customer.SvNo;
                tSVFinKod.Text = customer.FinCode;
                tAddress.Text = customer.Address;
                tResidentialAddress.Text = customer.ResidentialAddress;
                dateSVStart.DateTime = customer.SV_Start;
                dateSVEnd.DateTime = customer.SV_End;
                lookGender.SelectedText = customer.Gender;
                lookNation.SelectedText = customer.Nation;
                tEmail.Text = customer.Email;
                tMobPhone.Text = customer.MobPhone;
                tHomePhone.Text = customer.HomePhone;
                tComment.Text = customer.Comment;
                tBankAccountNumber.Text = customer.BankAccountNumber;
                tBankVoen.Text = customer.BankVoen;
                tBankName.Text = customer.BankName;
                tBankCode.Text = customer.BankCode;
                tBankSwift.Text = customer.BankSwift;
                bAdd.Text = Enums.GetEnumDescription(Enums.Operation.Update);
                tCompanyName.Enabled = false;
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
            lCustomerID.Text = "0";
            bAdd.Text = Enums.GetEnumDescription(Enums.Operation.Add);
            tCompanyName.Focus();
        }
    }
}