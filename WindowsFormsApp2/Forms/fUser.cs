using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Validations;

namespace WindowsFormsApp2.Forms
{
    public partial class fUser : DevExpress.XtraEditors.XtraForm
    {
        private int userId { get; set; }
        private string password { get; set; }
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
            if (chSaveMe.Checked)
            {
                Properties.Settings.Default.Username = tUsername.Text;
                Properties.Settings.Default.Password = string.IsNullOrWhiteSpace(tPassword.Text) ? password : tPassword.Text.Trim();
                Properties.Settings.Default.SaveMe = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = null;
                Properties.Settings.Default.Password = null;
                Properties.Settings.Default.SaveMe = false;
                Properties.Settings.Default.Save();
            }

            if (bSave.Text is "Yadda saxla")
            {
                AddUser();
            }
            else
            {
                EditUser();
            }


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
                    PosSaleScreen = chCashierPos.Checked == true ? true : false,
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

        void AddRole(int userId)
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

        private void EditUser()
        {
            DatabaseClasses.User user = new DatabaseClasses.User()
            {
                Id = userId,
                Username = tUsername.Text.Trim(),
                Password = string.IsNullOrWhiteSpace(tPassword.Text) ? password : tPassword.Text.Trim(),
                NameSurname = tFullName.Text.Trim(),
                IsAdmin = chAdmin.Checked == true ? true : false,
                PosSaleScreen = chCashierPos.Checked == true ? true : false,
                Email = tEmail.Text.Trim(),
                Phone = tPhone.Text.Trim(),
                DateBirth = dateBirth.DateTime,
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

            DbProsedures.UpdateUser(user);

            FormHelpers.Alert($"{user.Username} İstifadəçisində düzəliş edildi", Enums.MessageType.Success);

            EditRole();
            Clear();
        }

        void EditRole()
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

            DbProsedures.UpdateRole(role);
        }

        private void Clear()
        {
            tUsername.Clear();
            tPassword.Clear();
            tFullName.Clear();
            tEmail.Clear();
            tPhone.Clear();
            dateBirth.Clear();
            tSaleLimit.Clear();
            chPosSalePriceLimit.Checked = false;

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
            if (XtraMessageBox.Show("İstifadəçini silmək istədiyinizə əminsiniz ?", "Bildiriş", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr != null)
                {
                    int userID = Convert.ToInt32(dr["Id"].ToString());
                    DbProsedures.DeleteUser(userID);
                    string message = $"{dr["Username"]} istifadəçisi silindi";
                    FormHelpers.Alert(message, Enums.MessageType.Success);
                    FormHelpers.Log(message);
                    UserDataLoad();
                }
            }
        }

        private void bDetailRole_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                int userID = Convert.ToInt32(dr["Id"].ToString());
                fUserRoleShow f = new fUserRoleShow(userID);
                f.ShowDialog();
            }
        }

        private void bEditUser_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                userId = Convert.ToInt32(dr["Id"].ToString());
                password = dr["Password"].ToString();

                var data = DbProsedures.GetUser(userId);
                SelectUserDataLoad(data);
                bSave.Text = "Düzəliş et";
                bCancel.Visible = true;
            }
        }

        private void SelectUserDataLoad(DatabaseClasses.User user)
        {
            tUsername.Text = user.Username;
            tFullName.Text = user.NameSurname;
            tEmail.Text = user.Email;
            tPhone.Text = user.Phone;
            dateBirth.DateTime = user.DateBirth;
            if (user.IsAdmin)
                chAdmin.Checked = true;
            else
            {
                if (user.PosSaleScreen)
                {
                    chCashierPos.Checked = true;
                }
                else
                {
                    chCashier.Checked = true;
                }
            }

            //Roles
            chProductAdd.Checked = user.UserRole.ProductAdd;
            chRefundProduct.Checked = user.UserRole.RefundProduct;
            chProductDelete.Checked = user.UserRole.ProductDelete;
            chProductDiscount.Checked = user.UserRole.ProductDiscount;
            chProductBarcodePrint.Checked = user.UserRole.ProductBarcodePrint;
            chScalesProductDownload.Checked = user.UserRole.ScalesProductDownload;
            chSuppliers.Checked = user.UserRole.Suppliers;
            chCustomers.Checked = user.UserRole.Customers;
            chBankSale.Checked = user.UserRole.BankSale;
            chCredit.Checked = user.UserRole.Credit;
            chPosPrepayment.Checked = user.UserRole.PosPrepayment;
            chPosSale.Checked = user.UserRole.PosSale;
            chPosRefund.Checked = user.UserRole.PosRefund;
            chPosSalePriceEdit.Checked = user.UserRole.PosSalePriceEdit;
            chPosSalePriceLimit.Checked = user.UserRole.PosSalePriceLimit.HasValue;
            tSaleLimit.Text = user.UserRole.PosSalePriceLimit.HasValue
                ? user.UserRole.PosSalePriceLimit.Value.ToString()
                : null;
            chReport.Checked = user.UserRole.Report;
            chTerminalDelete.Checked = user.UserRole.TerminalDelete;
            chPayments.Checked = user.UserRole.Payments;
            chUsers.Checked = user.UserRole.Users;
            chBackups.Checked = user.UserRole.Backups;
            chLogs.Checked = user.UserRole.Logs;
            chScalesDelete.Checked = user.UserRole.ScalesDelete;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            bSave.Text = "Yadda saxla";
            Clear();
            chCashier.Checked = true;
            chAdmin.Checked = true;
            bCancel.Visible = false;
        }

        private void chCashierPos_CheckedChanged(object sender, EventArgs e)
        {
            if (chCashierPos.Checked)
            {
                chPosSale.Checked = true;
                chPosSale.Enabled = false;
                chProductAdd.Enabled = false;
                chProductAdd.Checked = false;
                chRefundProduct.Checked = false;
                chRefundProduct.Enabled = false;
                chProductDelete.Checked = false;
                chProductDelete.Enabled = false;
                chProductDiscount.Checked = false;
                chProductDiscount.Enabled = false;
                chProductBarcodePrint.Checked = false;
                chProductBarcodePrint.Enabled = false;
                chScalesProductDownload.Checked = false;
                chScalesProductDownload.Enabled = false;
                chSuppliers.Checked = false;
                chSuppliers.Enabled = false;
                chCustomers.Checked = false;
                chCustomers.Enabled = false;
                chBankSale.Checked = false;
                chBankSale.Enabled = false;
                chReport.Checked = false;
                chReport.Enabled = false;
                chTerminalDelete.Checked = false;
                chTerminalDelete.Enabled = false;
                chPayments.Checked = false;
                chPayments.Enabled = false;
                chUsers.Checked = false;
                chUsers.Enabled = false;
                chBackups.Checked = false;
                chBackups.Enabled = false;
                chLogs.Checked = false;
                chLogs.Enabled = false;
                chScalesDelete.Checked = false;
                chScalesDelete.Enabled = false;
            }
            else
            {
                chPosSale.Checked = true;
                chPosSale.Enabled = true;
                chProductAdd.Enabled = true;
                chRefundProduct.Enabled = true;
                chProductDelete.Enabled = true;
                chProductDiscount.Enabled = true;
                chProductBarcodePrint.Enabled = true;
                chScalesProductDownload.Enabled = true;
                chSuppliers.Enabled = true;
                chCustomers.Enabled = true;
                chBankSale.Enabled = true;
                chReport.Enabled = true;
                chTerminalDelete.Enabled = true;
                chPayments.Enabled = true;
                chUsers.Enabled = true;
                chBackups.Enabled = true;
                chLogs.Enabled = true;
                chScalesDelete.Enabled = true;
            }
        }
    }
}