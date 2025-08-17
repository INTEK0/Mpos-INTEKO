using DevExpress.XtraGrid.Localization;
using System;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fSelectedData<TParent> : BaseForm where TParent : BaseForm
    {
        private SelectedDataType _selectedData;
        private readonly TParent _parent;
        private Customer _customer;
        private Doctor _doctor;
        private Guarantor _zamin;

        public fSelectedData(TParent parentForm, SelectedDataType selectedData)
        {
            InitializeComponent();
            _parent = parentForm;
            _selectedData = selectedData;
            GridPanelText(gridCustomers);
            GridPanelText(gridDoctor);
            GridPanelText(gridZamin);
        }

        private void fSelectedData_Load(object sender, EventArgs e)
        {
            switch (_selectedData)
            {
                case SelectedDataType.Customer:
                    CustomerDataLoad();
                    gridControl1.MainView = gridCustomers;
                    bAdd.Text = "YENİ MÜŞTƏRİ";
                    break;
                case SelectedDataType.Guarantor:
                    GuarantorDataLoad();
                    gridControl1.MainView = gridZamin;
                    bAdd.Text = "YENİ ZAMİN";
                    break;
                case SelectedDataType.Doctor:
                    DoctorDataLoad();
                    gridControl1.MainView = gridDoctor;
                    bAdd.Text = "YENİ HƏKİM";
                    break;
                default:
                    break;
            }
        }

        private void CustomerDataLoad()
        {
            this.Text = "MÜŞTƏRİ SEÇİMİ";
            var data = DbProsedures.ConvertToDataTable("SELECT * FROM dbo.fn_MUSTERI()");
            gridControl1.DataSource = data;
            gridCustomers.GroupPanelText = $"Müştəri sayı: {gridCustomers.RowCount}";
        }

        private void GuarantorDataLoad()
        {
            this.Text = "ZAMİN SEÇİMİ";
            string query = @"SELECT 
  ZAMINLER_ID As ID, 
  CompanyName, 
  AD as [Name], 
  SOYAD as Surname, 
  ATAADI AS FatherName,
  AD + ' ' + SOYAD + '' + ATAADI AS NameSurname,
  DOGUM_TARIX as DateBirth, 
  FINKOD as FinCode, 
  MOBIL as MobPhone
FROM 
  ZAMINLER
WHERE IsDeleted = 0";
            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
            gridDoctor.GroupPanelText = $"Zamin sayı: {gridZamin.RowCount}";
        }

        private void DoctorDataLoad()
        {
            this.Text = "HƏKİM SEÇİMİ";
            var data = DbProsedures.ConvertToDataTable("SELECT * FROM dbo.fn_DOCTOR()");
            gridControl1.DataSource = data;
            gridDoctor.GroupPanelText = $"Həkim sayı: {gridDoctor.RowCount}";
        }

        private void gridCustomers_DoubleClick(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(gridCustomers.GetFocusedRowCellValue("MUSTERILER_ID").ToString());

            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM SELECT_MUSTERI_DATA_LOAD(@CustomerID)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _customer = FormHelpers.MapReaderToObject<DatabaseClasses.Customer>(reader);
                        }
                    }
                }
            }


            var method = _parent.GetType().GetMethod("ReceiveData");
            if (method != null)
            {
                method.MakeGenericMethod(_customer.GetType()).Invoke(_parent, new object[] { _customer });
                this.Close();
            }
        }

        private void gridDoctor_DoubleClick(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(gridDoctor.GetFocusedRowCellValue("Id").ToString());

            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
                            _doctor = FormHelpers.MapReaderToObject<DatabaseClasses.Doctor>(reader);
                        }
                    }
                }
            }


            var method = _parent.GetType().GetMethod("ReceiveData");
            if (method != null)
            {
                method.MakeGenericMethod(_doctor.GetType()).Invoke(_parent, new object[] { _doctor });
                this.Close();
            }
        }

        private void gridZamin_DoubleClick(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(gridZamin.GetFocusedRowCellValue("ID").ToString());

            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                connection.Open();
                string query = $@"SELECT 
  ZAMINLER_ID As ID, 
  CompanyName, 
  AD as [Name], 
  SOYAD as Surname, 
  ATAADI AS FatherName,
  DOGUM_TARIX as DateBirth, 
  FINKOD as FinCode, 
  MOBIL as MobPhone
FROM 
  ZAMINLER
WHERE IsDeleted = 0 AND ZAMINLER_ID = @Id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _zamin = FormHelpers.MapReaderToObject<DatabaseClasses.Guarantor>(reader);
                        }
                    }
                }
            }


            var method = _parent.GetType().GetMethod("ReceiveData");
            if (method != null)
            {
                method.MakeGenericMethod(_zamin.GetType()).Invoke(_parent, new object[] { _zamin });
                this.Close();
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            switch (_selectedData)
            {
                case SelectedDataType.Customer:
                    OpenForm<fAddCustomer>();
                    break;
                case SelectedDataType.Guarantor:
                    OpenForm<fAddGuarantor>();
                    break;
                case SelectedDataType.Doctor:
                    OpenForm<fAddDoctor>();
                    break;
                default:
                    break;
            }
            gridControl1.RefreshDataSource();
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            switch (_selectedData)
            {
                case SelectedDataType.Customer:
                    CustomerDataLoad();
                    break;
                case SelectedDataType.Guarantor:
                    GuarantorDataLoad();
                    break;
                case SelectedDataType.Doctor:
                    DoctorDataLoad();
                    break;
            }
        }
    }
}