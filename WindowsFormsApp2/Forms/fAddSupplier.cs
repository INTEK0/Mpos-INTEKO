using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.Validations;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;

namespace WindowsFormsApp2.Forms
{
    public partial class fAddSupplier : BaseForm
    {
        private int supplierID = 0;
        public fAddSupplier()
        {
            InitializeComponent();
        }

        private void fAddSupplier_Load(object sender, EventArgs e)
        {
            dateContractDate.DateTime = DateTime.Now;
            Clear();
            BorcTeyinatiDataLoad();
            tProccessNo.Text = DbProsedures.GET_SupplierProccessNo();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (bAdd.Text == Enums.GetEnumDescription(Enums.Operation.Add))
                {
                    Add();
                    DialogResult = DialogResult.OK;
                }
                else if (bAdd.Text == Enums.GetEnumDescription(Enums.Operation.Update))
                {
                    Edit();
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
            }
            finally { Cursor.Current = Cursors.Default; }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void bSupplierList_Click(object sender, EventArgs e)
        {
            fSuppliers<fAddSupplier> searchProduct = new fSuppliers<fAddSupplier>(this);
            searchProduct.ShowDialog();
        }

        private void Add()
        {
            Supplier supplier = new Supplier();
            supplier.ProccessNo = tProccessNo.Text;
            supplier.SupplierName = tSupplierName.Text.Trim();
            supplier.Voen = tVoen.Text.Trim();
            supplier.Address = tAddress.Text.Trim();
            supplier.ContractDate = dateContractDate.DateTime;
            supplier.ContractNo = tConractNo.Text.Trim();
            supplier.Email = tEmail.Text;
            supplier.MobPhone = tMobPhone.Text;
            supplier.Comment = tComment.Text.Trim();
            supplier.BankAccountNumber = tBankAccountNumber.Text.Trim();
            supplier.BankName = tBankName.Text.Trim();
            supplier.BankVoen = tBankVoen.Text.Trim();
            supplier.BankCode = tBankCode.Text.Trim();
            supplier.BankSwift = tBankSwift.Text.Trim();


            var validator = new SupplierValidation();
            var validateResult = validator.Validate(supplier);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                    return;
                }
            }

            int response = DbProsedures.InsertSupplier(supplier);
            if (response == 1)
            {
                FormHelpers.Alert($"{tSupplierName.Text} təchizatçısı uğurla yaradıldı", Enums.MessageType.Success);
                FormHelpers.Log($"{tSupplierName.Text} təchizatçısı yaradıldı");
                Clear();
                tProccessNo.Text = DbProsedures.GET_SupplierProccessNo();
            }
            else if (response == 0)
            {
                FormHelpers.Alert(SupplierValidation.SUPPLIER_ALREADY_EXISTS, Enums.MessageType.Info);
                return;
            }
        }

        private void Edit()
        {
            Supplier supplier = new Supplier();
            supplier.SupplierID = supplierID;
            supplier.ProccessNo = tProccessNo.Text;
            supplier.SupplierName = tSupplierName.Text.Trim();
            supplier.Voen = tVoen.Text.Trim();
            supplier.Address = tAddress.Text.Trim();
            supplier.ContractDate = dateContractDate.DateTime;
            supplier.ContractNo = tConractNo.Text.Trim();
            supplier.Email = tEmail.Text;
            supplier.MobPhone = tMobPhone.Text;
            supplier.Comment = tComment.Text.Trim();
            supplier.BankAccountNumber = tBankAccountNumber.Text.Trim();
            supplier.BankName = tBankName.Text.Trim();
            supplier.BankVoen = tBankVoen.Text.Trim();
            supplier.BankCode = tBankCode.Text.Trim();
            supplier.BankSwift = tBankSwift.Text.Trim();


            var validator = new SupplierValidation();
            var validateResult = validator.Validate(supplier);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                    return;
                }
            }

            if (DbProsedures.UpdateSupplier(supplier))
            {
                string message = $"{tSupplierName.Text} təchizatçısında düzəliş edildi";
                FormHelpers.Alert(message, Enums.MessageType.Success);
                FormHelpers.Log(message);
                Clear();
                tProccessNo.Text = DbProsedures.GET_SupplierProccessNo();
            }
        }

        public override void ReceiveData<T>(T data)
        {
            if (data is Supplier supplier)
            {
                supplierID = supplier.SupplierID;
                tProccessNo.Text = supplier.ProccessNo;
                tSupplierName.Text = supplier?.SupplierName;
                tVoen.Text = supplier?.Voen;
                tAddress.Text = supplier?.Address;
                tConractNo.Text = supplier?.ContractNo;
                tComment.Text = supplier?.Comment;
                tEmail.Text = supplier?.Email;
                tMobPhone.Text = supplier?.MobPhone;
                tBankAccountNumber.Text = supplier?.BankAccountNumber;
                tBankName.Text = supplier?.BankName;
                tBankVoen.Text = supplier?.BankVoen;
                tBankCode.Text = supplier?.BankCode;
                tBankSwift.Text = supplier?.BankSwift;
                bAdd.Text = Enums.GetEnumDescription(Enums.Operation.Update);

                string query = $@"SELECT COUNT(*) from MAL_ALISI_MAIN ma
inner join COMPANY.TECHIZATCI t ON ma.TECHIZATCI_ID = t.TECHIZATCI_ID
inner join MAL_ALISI_DETAILS md ON md.MAL_ALISI_MAIN_ID = ma.MAL_ALISI_MAIN_ID
where ma.TECHIZATCI_ID = {supplier.SupplierID} AND md.IsDeleted = 0";
                using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
                using (SqlCommand cmd = new SqlCommand(query,con))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int count = (int)cmd.ExecuteScalar();
                    if (count is 0)
                        tSupplierName.Enabled = true;
                    else
                        tSupplierName.Enabled = false;
                }
            }
        }

        private void Clear()
        {
            tSupplierName.Text = string.Empty;
            tVoen.Text = string.Empty;
            tAddress.Text = string.Empty;
            tConractNo.Text = string.Empty;
            tEmail.Text = null;
            tMobPhone.Text = null;
            tComment.Text = null;
            tBankAccountNumber.Text = string.Empty;
            tBankName.Text = string.Empty;
            tBankVoen.Text = string.Empty;
            tBankCode.Text = string.Empty;
            tBankSwift.Text = string.Empty;
            supplierID = 0;
            bAdd.Text = Enums.GetEnumDescription(Enums.Operation.Add);
            dateContractDate.DateTime = DateTime.Now;
            tSupplierName.Focus();
        }

        private void BorcTeyinatiDataLoad()
        {
            //string query = "select BORC_TEYINATI AS [BORC TƏYİNATI], BORC_TEYINATI_ID from BORC_TEYINATI";
            //var data = DbProsedures.ConvertToDataTable(query);
            //lookBorcTeyinati.Properties.DisplayMember = "BORC TƏYİNATI";
            //lookBorcTeyinati.Properties.ValueMember = "BORC_TEYINATI_ID";
            //lookBorcTeyinati.Properties.DataSource = data;
            //lookBorcTeyinati.Properties.PopulateColumns();
            //lookBorcTeyinati.Properties.Columns["BORC_TEYINATI_ID"].Visible = false;
        }
    }
}