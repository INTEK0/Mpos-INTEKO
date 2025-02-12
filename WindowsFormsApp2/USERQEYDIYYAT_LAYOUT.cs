using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Validations;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class USERQEYDIYYAT_LAYOUT : DevExpress.XtraEditors.XtraForm
    {
        public static int id_;

        public USERQEYDIYYAT_LAYOUT()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
            getall();
            clear();
        }

        private void getall()
        {
            dynamic query = @"SELECT 
id,
Ulogin as N'İstifadəçi adı',
Uparol as N'Şifrə',
Uadmin as 'Rol', 
AD as 'Ad Soyad', 
EMAILL as 'Elektron poçt',
TELEFON as 'Telefon', 
SV_NO as N'ŞV No',
UNVAN as N'Ünvan',
DOGUM_TARIXI as N'Doğum tarixi',
GAN_GRUPU as 'Qan qrupu'
FROM userParol where IsDeleted = 0";
            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
            gridView1.Columns[0].Visible = false;
        }

        private void USERQEYDIYYAT_LAYOUT_Load(object sender, EventArgs e)
        {
            dateBirth.DateTime = DateTime.Now;
            if (Properties.Settings.Default.SaveMe is true)
            {
                chSaveMe.Checked = true;
            }
            else
            {
                chSaveMe.Checked = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!UserValidation.ExistsUser(tUsername.Text.Trim()))
            {
                DatabaseClasses.User user = new DatabaseClasses.User()
                {
                    Username = tUsername.Text.Trim(),
                    Password = tPassword.Text.Trim(),
                    NameSurname = tNameSurname.Text.Trim(),
                    IsAdmin = checkBox1.Checked,
                    Email = tEmail.Text.Trim(),
                    Phone = tPhone.Text.Trim(),
                    SvNo = tSvNo.Text.Trim(),
                    Address = tAddress.Text.Trim(),
                    DateBirth = dateBirth.DateTime,
                    BloodType = tBloodType.Text.Trim(),
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

                DbProsedures.InsertUser(user);

                dynamic message = $"{user.Username} istifadəçisi yaradıldı";
                Log(message);
                Alert(message, Enums.MessageType.Success);


                if (chSaveMe.Checked)
                {
                    Properties.Settings.Default.Username = tUsername.Text;
                    Properties.Settings.Default.Password = tPassword.Text;
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

                getall();
                clear();
            }
        }

        private void clear()
        {
            tUsername.Text = "";
            tPassword.Text = "";
            tNameSurname.Text = "";
            tEmail.Text = "";
            tPhone.Text = "";
            tSvNo.Text = "";
            tAddress.Text = "";
            dateBirth.Text = "";
            tBloodType.Text = "";
            checkBox1.Checked = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
                DatabaseClasses.User user = new DatabaseClasses.User()
                {
                    Id = id_,
                    Username = tUsername.Text.Trim(),
                    Password = tPassword.Text.Trim(),
                    NameSurname = tNameSurname.Text.Trim(),
                    IsAdmin = checkBox1.Checked,
                    Email = tEmail.Text.Trim(),
                    Phone = tPhone.Text.Trim(),
                    SvNo = tSvNo.Text.Trim(),
                    Address = tAddress.Text.Trim(),
                    DateBirth = dateBirth.DateTime,
                    BloodType = tBloodType.Text.Trim(),
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

                DbProsedures.UpdatetUser(user);

                FormHelpers.Alert("istifadəçidə düzəliş edildi", Enums.MessageType.Success);
                FormHelpers.Log($"{user.Id} id nömrəsinə sahib istifadəçidə düzəliş edildi");

                if (chSaveMe.Checked)
                {
                    Properties.Settings.Default.Username = tUsername.Text;
                    Properties.Settings.Default.Password = tPassword.Text;
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

                getall();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                int userID = Convert.ToInt32(dr[0].ToString());
                DbProsedures.DeleteUser(userID);
                FormHelpers.Log($"{dr[4]} istifadəçisi silindi");
                getall();
                clear();
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                int iu = Convert.ToInt32(dr[3]);
                id_ = Convert.ToInt32(dr[0]);
                tUsername.Text = dr[1].ToString();//istifadeci adi
                tPassword.Text = dr[2].ToString();//parol
                                                  // int admin_ = Convert.ToInt32(dr[3].ToString());
                tNameSurname.Text = dr[4].ToString();
                tEmail.Text = dr[5].ToString();
                tPhone.Text = dr[6].ToString();
                tSvNo.Text = dr[7].ToString();
                tAddress.Text = dr[8].ToString();
                dateBirth.Text = dr[9].ToString();
                tBloodType.Text = dr[10].ToString();

                if (iu > 0)
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
            }
        }
    }
}