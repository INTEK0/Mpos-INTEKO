using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using WindowsFormsApp2.Validations;

namespace WindowsFormsApp2.Forms
{
    public partial class fAddDoctor : BaseForm
    {
        public fAddDoctor()
        {
            InitializeComponent();
        }

        private void fAddDoctor_Load(object sender, EventArgs e)
        {
            Clear();
            GenderDataLoad();
            bAdd.Text = Enums.GetEnumDescription(Enums.Operation.Add);
            tProccessNo.Text = DbProsedures.GET_DoctorProccessNo();
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

        private void bDoctorList_Click(object sender, EventArgs e)
        {
            fDoctors<fAddDoctor> doctor = new fDoctors<fAddDoctor>(this);
            doctor.ShowDialog();
        }

        private void Add()
        {
            Doctor doctor = new Doctor()
            {
                ProccessNo = tProccessNo.Text,
                NameSurname = tNameSurname.Text.Trim(),
                Position = tPosition.Text.Trim(),
                DateBirth = dateBirth.DateTime,
                Gender = lookGender.Text,
                Email = tEmail.Text.Trim(),
                Phone = tMobPhone.Text.Trim()
            };



            var validator = new DoctorValidation();
            var validateResult = validator.Validate(doctor);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                    return;
                }
            }

            int response = DbProsedures.InsertDoctor(doctor);
            if (response >= 0)
            {
                FormHelpers.Alert($"{doctor.NameSurname} həkimi uğurla yaradıldı", Enums.MessageType.Success);
                FormHelpers.Log($"{doctor.NameSurname} həkimi yaradıldı");
                Clear();
                tProccessNo.Text = DbProsedures.GET_DoctorProccessNo();
            }
        }

        private void Edit()
        {
            Doctor doctor = new Doctor();
            doctor.Id = Convert.ToInt32(lDoctorID.Text);
            doctor.NameSurname = tNameSurname.Text.Trim();
            doctor.Position = tPosition.Text.Trim();
            doctor.Email = tEmail.Text.Trim();
            doctor.Phone = tMobPhone.Text.Trim();
            doctor.DateBirth = dateBirth.DateTime;
            doctor.Gender = lookGender.Text;

            var validator = new DoctorValidation();
            var validateResult = validator.Validate(doctor);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                    return;
                }
            }

            bool response = DbProsedures.UpdateDoctor(doctor);
            if (response is true)
            {
                string message = $"{doctor.NameSurname} müştərisində düzəliş edildi";
                FormHelpers.Alert(message, Enums.MessageType.Success);
                FormHelpers.Log(message);
                Close();
            }
        }

        public override void ReceiveData<T>(T data)
        {
            if (data is Doctor doctor)
            {
                lDoctorID.Text = doctor.Id.ToString();
                tNameSurname.Text = doctor.NameSurname;
                tPosition.Text = doctor.Position;
                dateBirth.DateTime = doctor.DateBirth;
                tEmail.Text = doctor.Email;
                tMobPhone.Text = doctor.Phone;
                tProccessNo.Text = doctor.ProccessNo;
                lookGender.Text = doctor.Gender;
                tNameSurname.Enabled = false;
                bAdd.Text = Enums.GetEnumDescription(Enums.Operation.Update);
            }
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
            tNameSurname.Text = null;
            dateBirth.Text = null;
            tEmail.Text = null;
            tMobPhone.Text = null;
            lookGender.Text = null;
        }
    }
}