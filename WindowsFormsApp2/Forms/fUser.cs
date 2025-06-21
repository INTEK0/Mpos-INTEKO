using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Internal;
using DevExpress.XtraEditors;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Validations;

namespace WindowsFormsApp2.Forms
{
    public partial class fUser : DevExpress.XtraEditors.XtraForm
    {
        public fUser()
        {
            InitializeComponent();
        }

        private void fUser_Load(object sender, EventArgs e)
        {
            UserDataLoad();
        }

        private void UserDataLoad()
        {
            dynamic query = @"SELECT 
  id AS Id, 
  Ulogin AS Username, 
  Uparol AS [Password], 
  Uadmin AS [Role], 
  AD AS FullName, 
  EMAILL AS Email, 
  TELEFON AS Phone, 
  DOGUM_TARIXI AS DateBirth 
FROM 
  userParol 
where 
  IsDeleted = 0";

            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;

            dateBirth.DateTime = new DateTime(2000, 01, 01);
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            AddUser();
        }

        private void AddUser()
        {
            if (!UserValidation.ExistsUser(tUsername.Text.Trim()))
            {
                DatabaseClasses.User user = new DatabaseClasses.User()
                {
                    Username = tUsername.Text.Trim(),
                    Password = tPassword.Text.Trim(),
                    NameSurname = tFullName.Text.Trim(),
                    Email = tEmail.Text.Trim(),
                    Phone = tPhone.Text.Trim(),
                    IsAdmin = chAdmin.Checked == true ? true : false,
                    DateBirth = dateBirth.DateTime
                };

                var validator = new UserValidation();
                var validateResult = validator.Validate(user);

                if (!validateResult.IsValid)
                {
                    foreach (var error in validateResult.Errors)
                    {
                        FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                        return;
                    }
                }

                int userId = DbProsedures.InsertUser(user);

                if (userId > 0)
                {
                    AddRole(userId);

                    dynamic message = $"{user.Username} istifadəçisi yaradıldı";
                    FormHelpers.Log(message);
                    FormHelpers.Alert(message, Enums.MessageType.Success);

                    Clear();
                    UserDataLoad();
                }
                else
                {
                    FormHelpers.Alert($"İstifadəçi yaradarkən xəta yarandı", Enums.MessageType.Error);
                }
            }
        }

        private void AddRole(int userId)
        {
            DatabaseClasses.UserRole role = new DatabaseClasses.UserRole();
            role.UserId = userId;
            role.ProductAdd = chProductAdd.Checked;
            role.RefundProduct = chRefundProduct.Checked;
            role.ProductDelete = chProductDelete.Checked;
            role.ProductDiscount = chProductDiscount.Checked;
            role.ProductBarcodePrint = chProductBarcodePrint.Checked;
            role.ScalesProductDownload = chScalesProductDownload.Checked;
            role.Suppliers = chSuppliers.Checked;
            role.Customers = chCustomers.Checked;
            role.BankSale = chBankSale.Checked;
            role.Credit = chCredit.Checked;
            role.PosPrepayment = chPosPrepayment.Checked;
            role.PosSale = chPosSale.Checked;
            role.PosRefund = chPosRefund.Checked;
            role.PosSalePriceEdit = chPosSalePriceEdit.Checked;
            if (chPosSalePriceLimit.Checked)
            {
                if (!string.IsNullOrWhiteSpace(tSaleLimit.Text))
                    role.PosSalePriceLimit = Convert.ToDecimal(tSaleLimit.EditValue);
                else
                    role.PosSalePriceLimit = null;
            }
            else
                role.PosSalePriceLimit = null;
            role.Report = chReport.Checked;
            role.TerminalDelete = chTerminalDelete.Checked;
            role.Payments = chPayments.Checked;
            role.Users = chUsers.Checked;
            role.Backups = chBackups.Checked;
            role.Logs = chLogs.Checked;
            role.ScalesDelete = chScalesDelete.Checked;

            DbProsedures.InsertRole(role);
        }

        private void DeleteUser()
        {

        }

        private void DeleteRole(int userId)
        {

        }

        private void Clear()
        {
            tUsername.Clear();
            tPassword.Clear();
            tFullName.Clear();
            tEmail.Clear();
            tPhone.Clear();
            dateBirth.Clear();


            tUsername.Focus();
        }

        private void chCashier_CheckedChanged(object sender, EventArgs e)
        {
            if (chCashier.Checked)
            {
                chProductAdd.Checked = false;
                chRefundProduct.Checked = false;
                chProductDelete.Checked = false;
                chProductDiscount.Checked = false;
                chProductBarcodePrint.Checked = false;
                chScalesProductDownload.Checked = false;
                chSuppliers.Checked = false;
                chCustomers.Checked = true;
                chBankSale.Checked = false;
                chCredit.Checked = false;
                chPosPrepayment.Checked = true;
                chPosSale.Checked = true;
                chReport.Checked = false;
                chTerminalDelete.Checked = false;
                chPayments.Checked = false;
                chUsers.Checked = false;
                chBackups.Checked = false;
                chLogs.Checked = false;
                chScalesDelete.Checked = false;
            }
        }

        private void chPosSalePriceLimit_CheckedChanged(object sender, EventArgs e)
        {
            if (chPosSalePriceLimit.Checked)
                tSaleLimit.Visible = true;
            else
                tSaleLimit.Visible = false;
        }

        private void chAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (chAdmin.Checked)
            {
                chProductAdd.Checked = true;
                chRefundProduct.Checked = true;
                chProductDelete.Checked = true;
                chProductDiscount.Checked = true;
                chProductBarcodePrint.Checked = true;
                chScalesProductDownload.Checked = true;
                chSuppliers.Checked = true;
                chCustomers.Checked = true;
                chBankSale.Checked = true;
                chCredit.Checked = true;
                chPosPrepayment.Checked = true;
                chPosSale.Checked = true;
                chReport.Checked = true;
                chTerminalDelete.Checked = true;
                chPayments.Checked = true;
                chUsers.Checked = true;
                chBackups.Checked = true;
                chLogs.Checked = true;
                chScalesDelete.Checked = true;
            }
        }

        private void bDeleteUser_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                int userID = Convert.ToInt32(dr["Id"].ToString());
                DbProsedures.DeleteUser(userID);
                FormHelpers.Log($"{dr["Username"]} istifadəçisi silindi");
                UserDataLoad();
            }
        }
    }
}