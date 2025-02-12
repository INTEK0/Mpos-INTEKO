using DevExpress.XtraGrid.Localization;
using System;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fDoctors<TParent> : BaseForm where TParent : BaseForm
    {
        private readonly TParent parentForm;
        private DatabaseClasses.Doctor doctor;
        public fDoctors(TParent _parent)
        {
            parentForm = _parent;
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void fDoctors_Load(object sender, EventArgs e)
        {
            DoctorDataLoad();
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();

            foreach (int item in selectedRows)
            {
                var row = gridView1.GetDataRow(item);
                if (row == null) { return; }
                int Id = Convert.ToInt32(row[0].ToString());
                string nameSurname = row[1].ToString();
                if (!string.IsNullOrWhiteSpace(Id.ToString()))
                {
                    bool response = DbProsedures.DeleteDoctor(Id);
                    if (response is true)
                    {
                        Alert($"{nameSurname} həkimi uğurla silindi", Enums.MessageType.Success);
                        Log($"{nameSurname} həkimi silindi");
                        DoctorDataLoad();
                    }
                }
            }
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id").ToString());

            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM SELECT_DOCTOR_DATA_LOAD(@DoctorID)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@DoctorID", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            doctor = FormHelpers.MapReaderToObject<DatabaseClasses.Doctor>(reader);
                        }
                    }
                }
            }


            var method = parentForm.GetType().GetMethod("ReceiveData");
            if (method != null)
            {
                method.MakeGenericMethod(doctor.GetType()).Invoke(parentForm, new object[] { doctor });
                this.Close();
            }
        }

        private void bPrint_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Həkimlərin siyahısı");
        }

        private void DoctorDataLoad()
        {
            var data = DbProsedures.ConvertToDataTable("SELECT * FROM dbo.fn_DOCTOR()");
            gridControl1.DataSource = data;
            gridView1.Columns[0].Visible = false;
            gridView1.GroupPanelText = $"Həkim sayı: {gridView1.RowCount}";
        }
    }
}