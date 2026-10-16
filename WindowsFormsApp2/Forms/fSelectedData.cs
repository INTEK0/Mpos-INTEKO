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
        private DbAccess.Entities.Customer _customer;
        private DbAccess.Entities.SalePersonnel _salePersonnel;
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
                case SelectedDataType.SalePersonnel:
                    SalePersonnelDataLoad();
                    gridControl1.MainView = tileView1;
                    bAdd.Text = "YENİ SATICI";
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

        private void SalePersonnelDataLoad()
        {
            this.Text = "SATICI SEÇİMİ";
            string query = @"select Id,
FullName
from SalePersonnel
where IsActive = 1 AND IsDeleted = 0";
            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
            gridDoctor.GroupPanelText = $"Satıcı sayı: {tileView1.RowCount}";
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
                            _customer = FormHelpers.MapReaderToObject<DbAccess.Entities.Customer>(reader);
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

        private void tileView1_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            int Id = Convert.ToInt32(tileView1.GetFocusedRowCellValue("Id").ToString());

            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM SELECT_SALEPERSONEL_DATA_LOAD(@personelID)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@personelID", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _salePersonnel = FormHelpers.MapReaderToObject<DbAccess.Entities.SalePersonnel>(reader);
                        }
                    }
                }
            }

            var method = _parent.GetType().GetMethod("ReceiveData");
            if (method != null)
            {
                method.MakeGenericMethod(_salePersonnel.GetType()).Invoke(_parent, new object[] { _salePersonnel });
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
                case SelectedDataType.SalePersonnel:
                    OpenForm<fAddSalePersonnel>();
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
                case SelectedDataType.SalePersonnel:
                    SalePersonnelDataLoad();
                    break;
            }
        }

        
    }
}