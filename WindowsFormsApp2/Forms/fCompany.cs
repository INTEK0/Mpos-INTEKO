using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.NKA;
using WindowsFormsApp2.Validations;

namespace WindowsFormsApp2.Forms
{
    public partial class fCompany : DevExpress.XtraEditors.XtraForm
    {
        private DatabaseClasses.Company _company;
        private FormHelpers.IpModel _terminal { get; set; } = FormHelpers.GetIpModel();

        public fCompany()
        {
            InitializeComponent();
        }

        private void fCompany_Load(object sender, EventArgs e)
        {
            CompanyCount();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            fAdminPassword f = new fAdminPassword();
            if (f.ShowDialog() is DialogResult.OK)
            {
                Delete();
            }
        }

        private void Add()
        {
            try
            {
                _company = new DatabaseClasses.Company()
                {
                    CompanyName = tCompanyName.Text,
                    Voen = tVoen.Text,
                    CompanyCode = tCompanyCode.Text,
                    Address = tAddress.Text,
                    Phone = tPhone.Text,
                    WebSite = tWebSite.Text,
                    Email = tEmail.Text,
                    User = tUser.Text,
                    DateRegister = dateRegister.DateTime,
                    AccountNumber = tAccountNumber.Text,
                    BankName = tBankName.Text,
                    BankVoen = tBankVoen.Text,
                    BankCode = tBankCode.Text,
                    MH = textEdit9.Text,
                    SWIFT = tSwift.Text,
                };

                var validator = new CompanyValidation();
                var validateResult = validator.Validate(_company);

                if (!validateResult.IsValid)
                {
                    foreach (var error in validateResult.Errors)
                    {
                        FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                        return;
                    }
                }

                int response = DbProsedures.InsertCompany(_company);

                if (response > 0)
                {
                    TextboxReadonly();
                    FormHelpers.Alert("Obyekt məlumatları uğurla daxil edildi", Enums.MessageType.Success);
                    FormHelpers.Log("Obyekt məlumatları əlavə edildi");
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }

        private void Delete()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                string query = $"DELETE FROM COMPANY.COMPANY WHERE UserId = {Properties.Settings.Default.UserID}";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    dynamic message = "Obyekt məlumatları silindi";
                    FormHelpers.Alert(message, Enums.MessageType.Success);
                    FormHelpers.Log(message);
                    CompanyCount();
                }
            }
        }

        private void TextboxReadonly(bool request = true)
        {
            if (request)
            {
                tCompanyName.ReadOnly = true;
                tVoen.ReadOnly = true;
                tCompanyCode.ReadOnly = true;
                tAddress.ReadOnly = true;
                tPhone.ReadOnly = true;
                tWebSite.ReadOnly = true;
                tEmail.ReadOnly = true;
                tUser.ReadOnly = true;
                dateRegister.ReadOnly = true;
                tAccountNumber.ReadOnly = true;
                tBankName.ReadOnly = true;
                tBankVoen.ReadOnly = true;
                tBankCode.ReadOnly = true;
                textEdit9.ReadOnly = true;
                tSwift.ReadOnly = true;

                bSave.Enabled = false;
                bGetDataToken.Visible = false;
                bDelete.Visible = true;
            }
            else
            {
                tCompanyName.ReadOnly = false;
                tVoen.ReadOnly = false;
                tCompanyCode.ReadOnly = false;
                tAddress.ReadOnly = false;
                tPhone.ReadOnly = false;
                tWebSite.ReadOnly = false;
                tEmail.ReadOnly = false;
                tUser.ReadOnly = false;
                dateRegister.ReadOnly = false;
                tAccountNumber.ReadOnly = false;
                tBankName.ReadOnly = false;
                tBankVoen.ReadOnly = false;
                tBankCode.ReadOnly = false;
                textEdit9.ReadOnly = false;
                tSwift.ReadOnly = false;
                bSave.Enabled = true;
                bGetDataToken.Visible = true;
                bDelete.Visible = false;
            }
        }

        private void CompanyCount()
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                string query = $"SELECT COUNT(*) FROM COMPANY.COMPANY WHERE UserId = {Properties.Settings.Default.UserID}";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count = Convert.ToInt32(reader[0]);
                        }
                    }
                }
            }

            if (count > 0)
            {
                CompanyDataLoad();
                TextboxReadonly();
            }
            else
            {
                TextboxReadonly(false);
                Clear();
                dateRegister.DateTime = DateTime.Now;
            }
        }

        private void CompanyDataLoad()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                string query = "select * from dbo.fn_company(@userID)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@userID", Properties.Settings.Default.UserID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lCompanyId.Text = reader[0].ToString();
                            tCompanyName.Text = reader[1].ToString();
                            tAddress.Text = reader[2].ToString();
                            tPhone.Text = reader[3].ToString();
                            tEmail.Text = reader[4].ToString();
                            tAccountNumber.Text = reader[5].ToString();
                            tBankName.Text = reader[6].ToString();
                            tBankVoen.Text = reader[7].ToString();
                            tBankCode.Text = reader[8].ToString();
                            textEdit9.Text = reader[9].ToString();
                            tSwift.Text = reader[10].ToString();
                            tUser.Text = reader[11].ToString();
                            dateRegister.Text = reader[13].ToString();
                            tVoen.Text = reader[14].ToString();
                            tCompanyCode.Text = reader[15].ToString();
                            tWebSite.Text = reader[16].ToString();
                        }
                    }
                }
            }
        }

        private void Clear()
        {
            tCompanyName.Text = null;
            tVoen.Text = null;
            tCompanyCode.Text = null;
            tAddress.Text = null;
            tPhone.Text = null;
            tWebSite.Text = null;
            tEmail.Text = null;
            tUser.Text = null;
            dateRegister.Text = null;
            tAccountNumber.Text = null;
            tBankName.Text = null;
            tBankVoen.Text = null;
            tBankCode.Text = null;
            textEdit9.Text = null;
            tSwift.Text = null;
        }

        private void bGetDataToken_Click(object sender, EventArgs e)
        {
            //fAdminPassword f = new fAdminPassword();
            //if (f.ShowDialog() is DialogResult.OK)
            //{
            switch (_terminal.Model)
            {
                case "1": SunmiGetInfo(); break;
                case "2": break;
                case "3": break;
                case "4": FormHelpers.Alert("NKA seçimi edilmədiyi üçün məlumatlar manual daxil edilməlidir", Enums.MessageType.Info); break;
                case "5": break;
                case "6": NbaGetInfo(); break;
                default:
                    break;
            }
            //}
        }

        private void SunmiGetInfo()
        {
            var response = Sunmi.GetInfo(_terminal.Ip);
            if (response != null)
            {
                tCompanyName.Text = response.data.company_name;
                tVoen.Text = response.data.company_tax_number;
                tCompanyCode.Text = new string(response.data.object_tax_number.SkipWhile(x => x != '-').Skip(1).ToArray());
                tAddress.Text = response.data.object_address;



                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("TsName", response.data.object_name);
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("Address", response.data.object_address);
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("CompanyName", response.data.company_name);
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("Voen", response.data.company_tax_number);
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("ObjectTaxNumber", response.data.object_tax_number);
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("NKAModel", response.data.cashregister_model);
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("NKASerialNumber", response.data.cashbox_serial_number);
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("NMQRegistrationNumber", response.data.cashbox_tax_number);
            }
        }

        private void NbaGetInfo()
        {
            var response = NBA.GetInfo(_terminal.Ip);
            if (response != null)
            {
                tCompanyName.Text = response.data.company_name;
                tVoen.Text = response.data.company_tax_number;
                tCompanyCode.Text = new string(response.data.object_tax_number.SkipWhile(x => x != '-').Skip(1).ToArray());
                tAddress.Text = response.data.object_address;



                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("TsName", response.data.object_name);
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("Address", response.data.object_address);
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("CompanyName", response.data.company_name);
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("Voen", response.data.company_tax_number);
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("ObjectTaxNumber", response.data.object_tax_number);
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("NKAModel", response.data.cashregister_model);
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("NKASerialNumber", response.data.cashregister_factory_number);
                Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("TokenData").SetValue("NMQRegistrationNumber", response.data.cashbox_tax_number);
            }
        }
    }
}