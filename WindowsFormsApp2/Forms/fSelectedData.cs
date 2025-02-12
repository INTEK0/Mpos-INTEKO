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

        public fSelectedData(TParent parentForm, SelectedDataType selectedData)
        {
            InitializeComponent();
            _parent = parentForm;
            _selectedData = selectedData;
            GridPanelText(gridCustomers);
            GridPanelText(gridDoctor);
            GridLocalizer.Active = new MyGridLocalizer();
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

            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
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

        private void bAdd_Click(object sender, EventArgs e)
        {
            switch (_selectedData)
            {
                case SelectedDataType.Customer:
                    OpenForm<fAddCustomer>();
                    break;
                case SelectedDataType.Guarantor:
                    break;
                case SelectedDataType.Doctor:
                    OpenForm<fAddDoctor>();
                    break;
                default:
                    break;
            }
            gridControl1.RefreshDataSource();
        }

        
    }
}