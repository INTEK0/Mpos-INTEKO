using System;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.CacheData;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Services;
using WindowsFormsApp2.Validations;

namespace WindowsFormsApp2.Forms
{
    public partial class fAddSalePersonnel : DevExpress.XtraEditors.XtraForm
    {
        public fAddSalePersonnel()
        {
            InitializeComponent();
        }

        private void fAddSalePersonnel_Load(object sender, EventArgs e)
        {
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Add()
        {
            try
            {
                decimal bonusPercantage = Convert.ToDecimal(tBonusPercantage.Text);
                var item = new DbAccess.Entities.SalePersonnel
                {
                    FullName = tNameSurname.Text.Trim(),
                    DateBirth = dateBirth.DateTime,
                    BonusPercantage = bonusPercantage,
                    Phone = tMobPhone.Text,
                    Email = tEmail.Text.Trim(),
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = UserCacheService.User.Id,
                    CreatedAt = DatetimeService.CurrentDateTime
                };

                var validator = new SalePersonnelValidation();
                var validateResult = validator.Validate(item);

                if (!validateResult.IsValid)
                {
                    foreach (var error in validateResult.Errors)
                    {
                        FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                        return;
                    }
                }

                int response = DbProsedures.InsertSalePersonnel(item);
                if (response >= 0)
                {
                    FormHelpers.Alert("Satıcı uğurla yaradıldı", Enums.MessageType.Success);
                    FormHelpers.Log($"{tNameSurname.Text} satıcısı yaradıldı");
                    Clear();
                }
            }
            catch (Exception e)
            {
                throw ;
            }

            
        }

        private void Clear()
        {
            tNameSurname.Clear();
            dateBirth.Clear();
            tBonusPercantage.EditValue = 0;
            tMobPhone.Clear();
            tEmail.Clear();
        }
    }
}