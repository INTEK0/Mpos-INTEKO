using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;

namespace WindowsFormsApp2.Helpers.DB
{
    public static class DbProsedures
    {

        #region [...PROCEDURES QUERY...]

        private const string INSERT_CompanyQuery = "INSERT_COMPANY";
        private const string INSERT_PosSalesQuery = "azmart_sale_insert";
        private const string INSERT_ItemQuery = "INSERT_Item";
        private const string DELETE_ItemQuery = "delete_item";
        private const string INSERT_HeaderQuery = "INSERT_header";
        private const string INSERT_HeaderQueryPre = "INSERT_headerpre";
        private const string INSERT_CalculationQuery = "insert_calculation";
        private const string INSERT_PosRefundQuery = "insert_pos_gaytarma_manual";
        private const string GET_PosSalesProccesNoQuery = "exec dbo.pos_emeliyyat_nomre";
        private const string GET_TotalSalesCountQuery = "select count(*) as say from pos_satis_check_main";
        private const string INSERT_PosBasketQuery = "InsertBasketData";
        private const string ExportPosBasketQuery = "ExportBasketDataToCalculation";
        private const string GET_BasketDataLoadQuery = "PosBasketDataLoad";
        private const string GET_CategoryExistsQuery = "SELECT_COUNT_KATEGORY";
        private const string INSERT_CategoryQuery = "SELECT_KATEGORY";
        private const string GET_ProductExistsQuery = "yoxlama_mehsul_kodu";
        private const string INSERT_MALALISIMAINQuery = "INSERT_MAL_ALISI_MAIN";
        private const string INSERT_IMPORT_MALALISIMAINQuery = "INSERT_IMPORT_MAL_ALISI_MAIN";
        private const string INSERT_MALALISIDETAILQuery = "INSERT_MAL_ALISI_DETAILS";
        private const string DELETE_MALALISIDETAILQuery = "DELETE_PRODUCT_MAL_ALIS_DETAILS";
        private const string GET_ProductProccesNoLQuery = "EXEC  dbo.MAL_ALISI_EMELIYYAT_NOMRE";
        private const string INSERT_CustomerQuery = "INSERT_MUSTERI";
        private const string INSERT_DoctorQuery = "INSERT_DOCTOR";
        private const string DELETE_CustomerQuery = "delete_customer";
        private const string DELETE_DoctorQuery = "delete_doctor";
        private const string GET_CustomerProccessNoQuery = "EXEC dbo.MUSTERI_EMELIYYAT_NOMRE";
        private const string GET_DoctorProccessNoQuery = "EXEC dbo.DOCTOR_EMELIYYAT_NOMRE";
        private const string UPDATE_CustomerDataQuery = "UPDATE_MUSTERI";
        private const string UPDATE_DoctorDataQuery = "UPDATE_DOCTOR";
        private const string GET_SupplierProccessNoQuery = "EXEC dbo.TECHIZATCI_NOMRE";
        private const string INSERT_SupplierQuery = "INSERT_TECHIZATCI";
        private const string UPDATE_SupplierQuery = "search_techizatci_update";
        private const string DELETE_SupplierQuery = "search_techizatci_delete";
        private const string GET_GuarantorProccessNoQuery = "EXEC dbo.ZAMIN_EMELIYYAT_NOMRE";
        private const string INSERT_GuarantorQuery = "INSERT_ZAMIN";
        private const string DELETE_GuarantorQuery = "delete_zamin";
        private const string UPDATE_GuarantorDataQuery = "UPDATE_ZAMIN";
        private const string GET_RefundProccesNoQuery = "EXEC dbo.POS_GAYTARMA";
        private const string INSERT_UserQuery = "userParol_insert";
        private const string UPDATE_UserQuery = "userParol_update";
        private const string DELETE_UserQuery = "userParol_delete";
        private const string INSERT_ClinicDataQuery = "ClinicReportInsertData";
        public static readonly string GET_ClinicDataLoadQuery = $"EXEC [dbo].[ClinicReportDataLoad]@UserID = {Properties.Settings.Default.UserID}";
        private const string INSERT_GaimeSalesMainQuery = "INSERT_GAIME_SATISI_MAIN";
        private static readonly string GET_GaimeSalesProccessNoQuery = "EXEC dbo.GAIME_SATISI_EMELIYYAT_NOMRE";
        private static readonly string GET_GaimeRefundProccessNoQuery = "EXEC dbo.GAIME_SATISI_GAYTARMA";
        private const string GET_GetProductSalesDataQuery = "GetProductSalesData";
        private const string GET_GetProductPurchaseDataQuery = "GetProductPurchaseData";
        private const string INSERT_IncomeAndExpenseDataQuery = "INSERT_INCOME_AND_EXPENSE";

        #endregion [...PROCEDURES QUERY...]





        #region [...PROCEDURES METHODS...]


        public static DataTable ConvertToDataTable(string SqlQuery, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(SqlQuery, connection))
                    {
                        connection.Open();
                        cmd.CommandType = type;
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                        {
                            using (DataTable data = new DataTable())
                            {
                                dataAdapter.Fill(data);
                                return data;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
                FormHelpers.Log(ex.Message);
                return null;
            }
        }

        #region [..COMPANY..]

        public static int InsertCompany(Company data)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(INSERT_CompanyQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@COMPANY_NAME", SqlDbType.NVarChar, 500);
                    param.Value = data.CompanyName;

                    param = cmd.Parameters.Add("@ADRESS", SqlDbType.NVarChar, 500);
                    param.Value = data.Address;

                    param = cmd.Parameters.Add("@PHONE", SqlDbType.NVarChar, 500);
                    param.Value = data.Phone;

                    param = cmd.Parameters.Add("@EMAILL", SqlDbType.NVarChar, 500);
                    param.Value = data.Email;

                    param = cmd.Parameters.Add("@HN", SqlDbType.NVarChar, 500);
                    param.Value = data.AccountNumber;

                    param = cmd.Parameters.Add("@BANK_ADI", SqlDbType.NVarChar, 500);
                    param.Value = data.BankName;

                    param = cmd.Parameters.Add("@VOEN", SqlDbType.NVarChar, 500);
                    param.Value = data.BankVoen;

                    param = cmd.Parameters.Add("@KOD", SqlDbType.NVarChar, 500);
                    param.Value = data.BankCode;

                    param = cmd.Parameters.Add("@MH", SqlDbType.NVarChar, 500);
                    param.Value = data.MH;

                    param = cmd.Parameters.Add("@SWIFT", SqlDbType.NVarChar, 500);
                    param.Value = data.SWIFT;

                    param = cmd.Parameters.Add("@MESUL_SEXS", SqlDbType.NVarChar, 500);
                    param.Value = data.User;

                    param = cmd.Parameters.Add("@START_DATE", SqlDbType.Date);
                    param.Value = data.DateRegister;

                    param = cmd.Parameters.Add("@SIRKET_VOEN", SqlDbType.NVarChar, 250);
                    param.Value = data.Voen;

                    param = cmd.Parameters.Add("@OBYEKT_KODU", SqlDbType.NVarChar, 250);
                    param.Value = data.CompanyCode;

                    param = cmd.Parameters.Add("@WEB_SAYTI", SqlDbType.NVarChar, 250);
                    param.Value = data.WebSite;

                    param = cmd.Parameters.Add("@UserID", SqlDbType.Int);
                    param.Value = Properties.Settings.Default.UserID;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);

                    param.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    return Convert.ToInt32(param.Value);
                }
            }
        }

        #endregion [..COMPANY..]

        public static User GetUser()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                string query = "SELECT *  FROM SELECT_USER_DATA_LOAD(@userID)";
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userID", Properties.Settings.Default.UserID);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            var user = FormHelpers.MapReaderToObject<User>(dr);
                            return user;
                        }
                        return null;
                    }
                }
            }
        }

        public static void InsertUser(User item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(INSERT_UserQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@login", SqlDbType.VarChar);
                    param.Value = item.Username;
                    param = cmd.Parameters.Add("@parol", SqlDbType.VarChar);
                    param.Value = item.Password;
                    param = cmd.Parameters.Add("@admin", SqlDbType.Bit);
                    param.Value = item.IsAdmin;
                    param = cmd.Parameters.Add("@AD", SqlDbType.NVarChar, 100);
                    param.Value = item.NameSurname;
                    param = cmd.Parameters.Add("@EMAILL", SqlDbType.NVarChar, 100);
                    param.Value = item.Email;
                    param = cmd.Parameters.Add("@TELEFON", SqlDbType.NVarChar, 100);
                    param.Value = item.Phone;
                    param = cmd.Parameters.Add("@SV_NO", SqlDbType.NVarChar, 100);
                    param.Value = item.SvNo;
                    param = cmd.Parameters.Add("@UNVAN", SqlDbType.NVarChar, 100);
                    param.Value = item.Address;
                    param = cmd.Parameters.Add("@DOGUM_TARIXI", SqlDbType.Date);
                    param.Value = item.DateBirth;
                    param = cmd.Parameters.Add("@GAN_GRUPU", SqlDbType.NVarChar, 100);
                    param.Value = item.BloodType;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdatetUser(User item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE_UserQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@id", SqlDbType.Int);
                    param.Value = item.Id;
                    param = cmd.Parameters.Add("@parol", SqlDbType.VarChar);
                    param.Value = item.Password;
                    param = cmd.Parameters.Add("@admin", SqlDbType.Bit);
                    param.Value = item.IsAdmin;
                    param = cmd.Parameters.Add("@AD", SqlDbType.NVarChar, 100);
                    param.Value = item.NameSurname;
                    param = cmd.Parameters.Add("@EMAILL", SqlDbType.NVarChar, 100);
                    param.Value = item.Email;
                    param = cmd.Parameters.Add("@TELEFON", SqlDbType.NVarChar, 100);
                    param.Value = item.Phone;
                    param = cmd.Parameters.Add("@SV_NO", SqlDbType.NVarChar, 100);
                    param.Value = item.SvNo;
                    param = cmd.Parameters.Add("@UNVAN", SqlDbType.NVarChar, 100);
                    param.Value = item.Address;
                    param = cmd.Parameters.Add("@DOGUM_TARIXI", SqlDbType.Date);
                    param.Value = item.DateBirth;
                    param = cmd.Parameters.Add("@GAN_GRUPU", SqlDbType.NVarChar, 100);
                    param.Value = item.BloodType;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteUser(int userID)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DELETE_UserQuery, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@userID", SqlDbType.Int);
                    param.Value = userID;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static int InsertPosSales(PosSales item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(INSERT_PosSalesQuery, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter parameter;

                    parameter = cmd.Parameters.Add("@documentID", SqlDbType.NVarChar, 100);
                    parameter.Value = item.posNomre;

                    parameter = cmd.Parameters.Add("@fiscalID", SqlDbType.NVarChar, 500);
                    parameter.Value = item.longFiskalId;

                    parameter = cmd.Parameters.Add("@user_id", SqlDbType.Int);
                    parameter.Value = Properties.Settings.Default.UserID;

                    parameter = cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar, 100);
                    parameter.Value = item.proccessNo;

                    parameter = cmd.Parameters.Add("@negd", SqlDbType.Decimal);
                    parameter.Value = item.cash;

                    parameter = cmd.Parameters.Add("@kart", SqlDbType.Decimal);
                    parameter.Value = item.card;

                    parameter = cmd.Parameters.Add("@umumi_mebleg", SqlDbType.Decimal);
                    parameter.Value = item.total;

                    parameter = cmd.Parameters.Add("@json_", SqlDbType.NVarChar, int.MaxValue);
                    parameter.Value = item.json;

                    parameter = cmd.Parameters.Add("@fiscalNum", SqlDbType.NVarChar, 250);
                    parameter.Value = item.shortFiskalId;

                    parameter = cmd.Parameters.Add("@rrncode", SqlDbType.NVarChar);
                    parameter.Value = item.rrn;

                    parameter = cmd.Parameters.Add("@customerId", SqlDbType.Int);
                    parameter.Value = item.customerId;

                    parameter = cmd.Parameters.Add("@doctorId", SqlDbType.Int);
                    parameter.Value = item.doctorId;

                    parameter = cmd.Parameters.Add("@Prepayment", SqlDbType.Int);
                    parameter.Value = item.Prepayment;

                    connection.Open();
                    parameter = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
                    parameter.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    FormHelpers.OperationLog(new OperationLogs
                    {
                        OperationType = OperationType.PosSales,
                        OperationId = Convert.ToInt32(parameter.Value)
                    });


                    return Convert.ToInt32(parameter.Value);
                }
            }
        }

        public static void InsertItem(DatabaseClasses.Item item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(INSERT_ItemQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@name", SqlDbType.NVarChar, 500);
                    param.Value = item.Name;

                    param = cmd.Parameters.Add("@code", SqlDbType.NVarChar, 500);
                    param.Value = item.Code;

                    param = cmd.Parameters.Add("@quantity", SqlDbType.Decimal);
                    param.Value = item.Quantity;

                    param = cmd.Parameters.Add("@salePrice", SqlDbType.Decimal);
                    param.Value = item.SalePrice;

                    param = cmd.Parameters.Add("@purchasePrice", SqlDbType.Decimal);
                    param.Value = item.PurchasePrice;

                    param = cmd.Parameters.Add("@vatType", SqlDbType.Int);
                    param.Value = item.vatType;

                    param = cmd.Parameters.Add("@quantityType", SqlDbType.Int);
                    param.Value = item.QuantityType;

                    param = cmd.Parameters.Add("@mal_alisi_details_id", SqlDbType.Int);
                    param.Value = item.ProductId;

                    param = cmd.Parameters.Add("@user_id", SqlDbType.Int);
                    param.Value = Properties.Settings.Default.UserID;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteItem()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DELETE_ItemQuery, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@userID", SqlDbType.Int);
                    param.Value = Properties.Settings.Default.UserID;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void InsertHeader(Header item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(INSERT_HeaderQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@cashPayment", SqlDbType.Decimal);
                    param.Value = item.cash;

                    param = cmd.Parameters.Add("@cardPayment", SqlDbType.Decimal);
                    param.Value = item.card;

                    param = cmd.Parameters.Add("@bonusPayment", SqlDbType.Decimal);
                    param.Value = item.bonus;

                    param = cmd.Parameters.Add("@clientName", SqlDbType.NVarChar, 100);
                    param.Value = item.CustomerName;

                    param = cmd.Parameters.Add("@paidPayment", SqlDbType.Decimal);
                    param.Value = item.paidPayment;

                    if (item.PayType is PayType.Prepayment)
                    {
                        param = cmd.Parameters.Add("@prepayment", SqlDbType.Decimal);
                        param.Value = (item.cash + item.card);
                    }

                    param = cmd.Parameters.Add("@userID", SqlDbType.Int);
                    param.Value = Properties.Settings.Default.UserID;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteHeader()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM header WHERE userId = {Properties.Settings.Default.UserID}", connection))
                {
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void InsertCalculation(Calculation item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(INSERT_CalculationQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar, 20);
                    param.Value = item.proccessNo;
                    param = cmd.Parameters.Add("@mal_alisi_details_id", SqlDbType.Int);
                    param.Value = item.ProductID;
                    param = cmd.Parameters.Add("@barkod", SqlDbType.NVarChar, 100);
                    param.Value = item.Barcode;
                    param = cmd.Parameters.Add("@mehsul_adi", SqlDbType.NVarChar, 250);
                    param.Value = item.ProductName;
                    param = cmd.Parameters.Add("@satis_qiymeti", SqlDbType.Decimal);
                    param.Value = item.SalePrice;
                    param = cmd.Parameters.Add("@alis_qiymeti", SqlDbType.Decimal);
                    param.Value = item.PurchasePrice;
                    param = cmd.Parameters.Add("@userID", SqlDbType.Int);
                    param.Value = Properties.Settings.Default.UserID;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static int InsertPosRefund(PosRefund item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(INSERT_PosRefundQuery, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar, 100);
                    param.Value = item.proccessNo;

                    param = cmd.Parameters.Add("@pos_satis_check_main_id", SqlDbType.Int);
                    param.Value = item.pos_satis_check_main_id;

                    param = cmd.Parameters.Add("@pos_satis_check_details", SqlDbType.Int);
                    param.Value = item.pos_satis_check_details_id;

                    param = cmd.Parameters.Add("@say", SqlDbType.Decimal);
                    param.Value = item.quantity;

                    param = cmd.Parameters.Add("@user_id_", SqlDbType.Int);
                    param.Value = Properties.Settings.Default.UserID;

                    param = cmd.Parameters.Add("@GEYD", SqlDbType.NVarChar, 250);
                    param.Value = item.comment;

                    connection.Open();
                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    FormHelpers.OperationLog(new OperationLogs
                    {
                        OperationType = OperationType.RefundPosSales,
                        OperationId = Convert.ToInt32(param.Value)
                    });


                    return Convert.ToInt32(param.Value);
                }
            }
        }

        public static string GET_SalesProcessNo()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(GET_PosSalesProccesNoQuery, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            return dr["col1"].ToString();
                        }
                        return null;
                    }
                }
            }
        }

        public static string GET_TotalSalesCount()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(GET_TotalSalesCountQuery, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                        return null;
                    }
                }
            }
        }

        public static bool InsertPosBasketData(string basketName)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(INSERT_PosBasketQuery, connection))
                    {
                        SqlParameter param;
                        param = cmd.Parameters.Add("@BasketName", SqlDbType.NVarChar, 20);
                        param.Value = basketName;

                        param = cmd.Parameters.Add("@UserID", SqlDbType.Int);
                        param.Value = Properties.Settings.Default.UserID;

                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        Cursor.Current = Cursors.Default;
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
                FormHelpers.Log(e.Message);
                return false;
            }
            finally { Cursor.Current = Cursors.Default; }
        }

        public static bool ExportPosBasketData(string basketName)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(ExportPosBasketQuery, connection))
                    {
                        SqlParameter param;
                        param = cmd.Parameters.Add("@BasketName", SqlDbType.NVarChar, 20);
                        param.Value = basketName;

                        param = cmd.Parameters.Add("@UserID", SqlDbType.Int);
                        param.Value = Properties.Settings.Default.UserID;

                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        Cursor.Current = Cursors.Default;
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
                FormHelpers.Log(e.Message);
                return false;
            }
            finally { Cursor.Current = Cursors.Default; }
        }

        public static DataTable GET_BasketDataLoad(string basketName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(GET_BasketDataLoadQuery, connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BasketName", basketName);
                        cmd.Parameters.AddWithValue("@UserId", Properties.Settings.Default.UserID);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
                FormHelpers.Log(e.Message);
                return null;
            }
        }

        public static string GET_RefundProccessNo()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(GET_RefundProccesNoQuery, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                        return null;
                    }
                }
            }
        }


        #region [..CATEGORY..]

        public static int Exists_Category(string CategoryName)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(GET_CategoryExistsQuery, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@KATEGORY", SqlDbType.NVarChar, 500);
                    param.Value = CategoryName;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(param.Value);
                }
            }
        }

        public static int Insert_Category(string CategoryName)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(INSERT_CategoryQuery, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@KATEGORY", SqlDbType.NVarChar, 500);
                    param.Value = CategoryName;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(param.Value);
                }
            }
        }

        public static int UpdateCategory(Categories item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@KATEGORIYA_ID", SqlDbType.Int);
                    param.Value = item.KATEGORIYA_ID;

                    param = cmd.Parameters.Add("@KATEGORIYA", SqlDbType.NVarChar, 200);
                    param.Value = item.KATEGORIYA;

                    param = cmd.Parameters.Add("@EMCPOUNT", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(param.Value);
                }
            }
        }

        #endregion [..CATEGORY..]



        #region [..PRODUCTS..]

        public static int Exists_ProductCode(string productCode, int supplierId)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(GET_ProductExistsQuery, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@mehsul_kodu", SqlDbType.NVarChar, 500);
                    param.Value = productCode;

                    param = cmd.Parameters.Add("@techizatci_id", SqlDbType.Int);
                    param.Value = supplierId;

                    param = cmd.Parameters.Add("@empcount", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(param.Value);
                }
            }
        }

        public static int InsertProductMain(ProductsMain item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(INSERT_MALALISIMAINQuery, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@FAKTURA_NOMRE", SqlDbType.NVarChar, 500);
                    param.Value = item.FakturaNo;

                    param = cmd.Parameters.Add("@TECHIZATCI", SqlDbType.NVarChar, 500);
                    param.Value = item.SupplierName;

                    param = cmd.Parameters.Add("@TARIX", SqlDbType.Date);
                    param.Value = item.Date;

                    param = cmd.Parameters.Add("@ODEME_TIPI", SqlDbType.NVarChar, 500);
                    param.Value = item.PaymentType;

                    param = cmd.Parameters.Add("@EMELIYYAT_NOMRE", SqlDbType.NVarChar, 100);
                    param.Value = item.ProccessNo;

                    param = cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar, 100);
                    param.Value = item.Status;

                    param = cmd.Parameters.Add("@USER_ID_", SqlDbType.Int);
                    param.Value = Properties.Settings.Default.UserID;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    FormHelpers.OperationLog(new OperationLogs
                    {
                        OperationType = OperationType.AddProduct,
                        OperationId = Convert.ToInt32(param.Value)
                    });


                    return Convert.ToInt32(param.Value);
                }
            }
        }

        public static int InsertImportProductMain(ProductsMain item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(INSERT_IMPORT_MALALISIMAINQuery, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@FAKTURA_NOMRE", SqlDbType.NVarChar, 500);
                    param.Value = item.FakturaNo;

                    param = cmd.Parameters.Add("@TECHIZATCI", SqlDbType.Int);
                    param.Value = item.SupplierId;

                    param = cmd.Parameters.Add("@TARIX", SqlDbType.Date);
                    param.Value = item.Date;

                    param = cmd.Parameters.Add("@ODEME_TIPI", SqlDbType.NVarChar, 500);
                    param.Value = item.PaymentType;

                    param = cmd.Parameters.Add("@EMELIYYAT_NOMRE", SqlDbType.NVarChar, 100);
                    param.Value = item.ProccessNo;

                    param = cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar, 100);
                    param.Value = item.Status;

                    param = cmd.Parameters.Add("@USER_ID_", SqlDbType.Int);
                    param.Value = Properties.Settings.Default.UserID;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    FormHelpers.OperationLog(new OperationLogs
                    {
                        OperationType = OperationType.AddProduct,
                        OperationId = Convert.ToInt32(param.Value)
                    });



                    return Convert.ToInt32(param.Value);
                }
            }
        }

        public static async Task<int?> InsertProductDetails(ProductsDetail item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(INSERT_MALALISIDETAILQuery, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter param;

                        param = cmd.Parameters.Add("@MAL_ALISI_MAIN_ID", SqlDbType.Int);
                        param.Value = item.ProductMainId;
                        param = cmd.Parameters.Add("@KATEGORIYA", SqlDbType.NVarChar, 500);
                        param.Value = item.CategoryName;
                        param = cmd.Parameters.Add("@BARKOD", SqlDbType.NVarChar, 500);
                        param.Value = item.Barocde;
                        param = cmd.Parameters.Add("@MEHSUL_ADI", SqlDbType.NVarChar, 500);
                        param.Value = item.ProductName;
                        param = cmd.Parameters.Add("@MEHSUL_KODU", SqlDbType.NVarChar, 500);
                        param.Value = item.ProductCode;
                        param = cmd.Parameters.Add("@ANBAR", SqlDbType.NVarChar, 500);
                        param.Value = item.WarehouseName;
                        param = cmd.Parameters.Add("@MIGDARI", SqlDbType.NVarChar, 500);
                        param.Value = item.Quantity.ToString();
                        param = cmd.Parameters.Add("@VAHID", SqlDbType.NVarChar, 500);
                        param.Value = item.UnitName;
                        param = cmd.Parameters.Add("@VALYUTA", SqlDbType.NVarChar, 500);
                        param.Value = item.CurrencyName;
                        param = cmd.Parameters.Add("@VERGI_DERECESI", SqlDbType.NVarChar, 500);
                        param.Value = item.TaxName;
                        param = cmd.Parameters.Add("@ALIS_GIYMETI", SqlDbType.NVarChar, 500);
                        param.Value = item.PurchasePrice.ToString();
                        param = cmd.Parameters.Add("@SATIS_GIYMETI", SqlDbType.NVarChar, 500);
                        param.Value = item.SalePrice.ToString();
                        param = cmd.Parameters.Add("@ENDIRIM_FAIZ", SqlDbType.NVarChar, 500);
                        param.Value = item.DiscountPercent.ToString();
                        param = cmd.Parameters.Add("@ENDIRIM_AZN", SqlDbType.NVarChar, 500);
                        param.Value = item.DiscountAZN.ToString();
                        param = cmd.Parameters.Add("@ENDIRIM_MEBLEGI", SqlDbType.NVarChar, 500);
                        param.Value = item.DiscountAmount.ToString();
                        param = cmd.Parameters.Add("@YEKUN_MEBLEG", SqlDbType.NVarChar, 500);
                        param.Value = item.TotalAmount.ToString();
                        param = cmd.Parameters.Add("@ISTEHSAL_TARIXI", SqlDbType.NVarChar, 20);
                        param.Value = item.IstehsalTarixi;
                        param = cmd.Parameters.Add("@BITIS_TARIXI", SqlDbType.NVarChar, 20);
                        param.Value = item.BitisTarixi;
                        param = cmd.Parameters.Add("@XEBERDAR_ET", SqlDbType.NVarChar, 500);
                        param.Value = item.XeberdarEt;
                        param = cmd.Parameters.Add("@ShowPosScreen", SqlDbType.Bit);
                        param.Value = false;
                        param = cmd.Parameters.Add("@SEKIL", SqlDbType.VarBinary, int.MaxValue);
                        param.Value = item.imageBytes;

                        var empCountParam = new SqlParameter("@emp_count", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        var malDetailIdParam = new SqlParameter("@MalDetailId", SqlDbType.Int) { Direction = ParameterDirection.Output };

                        cmd.Parameters.Add(empCountParam);
                        cmd.Parameters.Add(malDetailIdParam);
                       
                        cmd.ExecuteNonQuery();

                        int empCount = (int)empCountParam.Value;
                        int malDetailId = (int)malDetailIdParam.Value;

                        if (item.imageBytes != null)
                        {
                           await UpdateProductImage(malDetailId, item.Barocde);
                        }

                        FormHelpers.Log($"{item.ProductName} məhsulundan {item.Quantity} {item.UnitName} alış edildi");
                        return Convert.ToInt32(empCount);
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
                return null;
            }
        }

        private async static Task UpdateProductImage(int Id, string barcode)
        {
            //Məhsulda şəkil varsa yalnız bu kod işləyir
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                await con.OpenAsync();
                string query = $@"
IF EXISTS (SELECT 1 FROM MAL_ALISI_DETAILS WHERE MAL_ALISI_DETAILS_ID = {Id} AND SEKIL IS NOT NULL)

UPDATE MAL_ALISI_DETAILS
SET SEKIL = (SELECT SEKIL FROM MAL_ALISI_DETAILS WHERE MAL_ALISI_DETAILS_ID = {Id})
WHERE BARKOD = '{barcode}'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                   await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public static int DeleteProduct(ProductsDetail item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(DELETE_MALALISIDETAILQuery, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@TECHIZATCI", SqlDbType.NVarChar, 500);
                    param.Value = item.SupplierName;

                    param = cmd.Parameters.Add("@PRODUCTID", SqlDbType.Int);
                    param.Value = item.ProductId;

                    param = cmd.Parameters.Add("@BARCODE", SqlDbType.NVarChar, 500);
                    param.Value = item.Barocde;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(param.Value);
                }
            }
        }      

        public static string GET_ProductProcessNo()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(GET_ProductProccesNoLQuery, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                        return null;
                    }
                }
            }
        }

        public static int ProductNegativeStatus(bool status)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("MENFI_AC_BAGLA_CRUD", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@CHECK", SqlDbType.Int);
                    param.Value = status;

                    param = cmd.Parameters.Add("@empcount", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(param.Value);
                }
            }
        }

        private static void ProductNegativeStatus()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT STATUS FROM MENFI_AC_BAGLA", connection))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
        }


        #endregion [..PRODUCTS..]



        #region [..CUSTOMERS..]

        public static string GET_CustomerProccessNo()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(GET_CustomerProccessNoQuery, connection))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                        return null;
                    }
                }
            }
        }

        public static int InsertCustomer(Customer data)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(INSERT_CustomerQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@musteri_nomre", SqlDbType.NVarChar, 250);
                    param.Value = data.ProccessNo;

                    param = cmd.Parameters.Add("@TARIX", SqlDbType.Date);
                    param.Value = DateTime.Now;

                    param = cmd.Parameters.Add("@COMPANYNAME", SqlDbType.NVarChar, 100);
                    param.Value = data.CompanyName;

                    param = cmd.Parameters.Add("@VOEN", SqlDbType.NVarChar, 500);
                    param.Value = data.Voen;

                    param = cmd.Parameters.Add("@AD", SqlDbType.NVarChar, 250);
                    param.Value = data.Name;

                    param = cmd.Parameters.Add("@SOYAD", SqlDbType.NVarChar, 250);
                    param.Value = data.Surname;

                    param = cmd.Parameters.Add("@ATAADI", SqlDbType.NVarChar, 250);
                    param.Value = data.FatherName;

                    param = cmd.Parameters.Add("@DOGUM_TARIX", SqlDbType.Date);
                    param.Value = data.DateBirth;

                    param = cmd.Parameters.Add("@SVNO", SqlDbType.NVarChar, 250);
                    param.Value = data.SvNo;

                    param = cmd.Parameters.Add("@FINKOD", SqlDbType.NVarChar, 250);
                    param.Value = data.FinCode;

                    param = cmd.Parameters.Add("@UNVAN", SqlDbType.NVarChar, 500);
                    param.Value = data.Address;

                    param = cmd.Parameters.Add("@FAKTIKI_YASAYIS_YERI", SqlDbType.NVarChar, 500);
                    param.Value = data.ResidentialAddress;

                    param = cmd.Parameters.Add("@SV_VERILME_TARIX", SqlDbType.Date);
                    param.Value = data.SV_Start;

                    param = cmd.Parameters.Add("@SV_BITME_TARIX", SqlDbType.Date);
                    param.Value = data.SV_End;

                    param = cmd.Parameters.Add("@CINSI", SqlDbType.NVarChar, 20);
                    param.Value = data.Gender;

                    param = cmd.Parameters.Add("@VETENDASLIG", SqlDbType.NVarChar, 250);
                    param.Value = data.Nation;

                    param = cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar, 250);
                    param.Value = data.Email;

                    param = cmd.Parameters.Add("@MOBIL", SqlDbType.NVarChar, 250);
                    param.Value = data.MobPhone;

                    param = cmd.Parameters.Add("@EV", SqlDbType.NVarChar, 250);
                    param.Value = data.HomePhone;

                    param = cmd.Parameters.Add("@GEYD", SqlDbType.NVarChar, 250);
                    param.Value = data.Comment;

                    param = cmd.Parameters.Add("@HESAB_NOM", SqlDbType.NVarChar, 500);
                    param.Value = data.BankAccountNumber;

                    param = cmd.Parameters.Add("@BANK_ADI", SqlDbType.NVarChar, 500);
                    param.Value = data.BankName;

                    param = cmd.Parameters.Add("@BANK_VOEN", SqlDbType.NVarChar, 500);
                    param.Value = data.BankVoen;

                    param = cmd.Parameters.Add("@KOD", SqlDbType.NVarChar, 500);
                    param.Value = data.BankCode;

                    param = cmd.Parameters.Add("@SWIFT", SqlDbType.NVarChar, 500);
                    param.Value = data.BankSwift;

                    param = cmd.Parameters.Add("@ISDELETED", SqlDbType.Int);
                    param.Value = 0;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);

                    param.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    return Convert.ToInt32(param.Value);
                }
            }
        }

        public static bool DeleteCustomer(int customerID)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DELETE_CustomerQuery, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@id", SqlDbType.Int);
                    param.Value = customerID;
                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return Convert.ToBoolean(param.Value);
                }
            }
        }

        public static bool UpdateCustomer(Customer data)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE_CustomerDataQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@CustomerID", SqlDbType.NVarChar, 100);
                    param.Value = data.CustomerID;

                    param = cmd.Parameters.Add("@VOEN", SqlDbType.NVarChar, 500);
                    param.Value = data.Voen;

                    param = cmd.Parameters.Add("@AD", SqlDbType.NVarChar, 250);
                    param.Value = data.Name;

                    param = cmd.Parameters.Add("@SOYAD", SqlDbType.NVarChar, 250);
                    param.Value = data.Surname;

                    param = cmd.Parameters.Add("@ATAADI", SqlDbType.NVarChar, 250);
                    param.Value = data.FatherName;

                    param = cmd.Parameters.Add("@DOGUM_TARIX", SqlDbType.Date);
                    param.Value = data.DateBirth;

                    param = cmd.Parameters.Add("@SVNO", SqlDbType.NVarChar, 250);
                    param.Value = data.SvNo;

                    param = cmd.Parameters.Add("@FINKOD", SqlDbType.NVarChar, 250);
                    param.Value = data.FinCode;

                    param = cmd.Parameters.Add("@UNVAN", SqlDbType.NVarChar, 500);
                    param.Value = data.Address;

                    param = cmd.Parameters.Add("@FAKTIKI_YASAYIS_YERI", SqlDbType.NVarChar, 500);
                    param.Value = data.ResidentialAddress;

                    param = cmd.Parameters.Add("@SV_VERILME_TARIX", SqlDbType.Date);
                    param.Value = data.SV_Start;

                    param = cmd.Parameters.Add("@SV_BITME_TARIX", SqlDbType.Date);
                    param.Value = data.SV_End;

                    param = cmd.Parameters.Add("@CINSI", SqlDbType.NVarChar, 20);
                    param.Value = data.Gender;

                    param = cmd.Parameters.Add("@VETENDASLIG", SqlDbType.NVarChar, 250);
                    param.Value = data.Nation;

                    param = cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar, 250);
                    param.Value = data.Email;

                    param = cmd.Parameters.Add("@MOBIL", SqlDbType.NVarChar, 250);
                    param.Value = data.MobPhone;

                    param = cmd.Parameters.Add("@EV", SqlDbType.NVarChar, 250);
                    param.Value = data.HomePhone;

                    param = cmd.Parameters.Add("@GEYD", SqlDbType.NVarChar, 250);
                    param.Value = data.Comment;

                    param = cmd.Parameters.Add("@HESAB_NOM", SqlDbType.NVarChar, 500);
                    param.Value = data.BankAccountNumber;

                    param = cmd.Parameters.Add("@BANK_ADI", SqlDbType.NVarChar, 500);
                    param.Value = data.BankName;

                    param = cmd.Parameters.Add("@BANK_VOEN", SqlDbType.NVarChar, 500);
                    param.Value = data.BankVoen;

                    param = cmd.Parameters.Add("@KOD", SqlDbType.NVarChar, 500);
                    param.Value = data.BankCode;

                    param = cmd.Parameters.Add("@SWIFT", SqlDbType.NVarChar, 500);
                    param.Value = data.BankSwift;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);

                    param.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    return Convert.ToBoolean(param.Value);
                }
            }
        }

        #endregion [..CUSTOMERS..]



        #region [..DOCTORS..]

        public static string GET_DoctorProccessNo()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(GET_DoctorProccessNoQuery, connection))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                        return null;
                    }
                }
            }
        }

        public static int InsertDoctor(Doctor data)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(INSERT_DoctorQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@ProccessNo", SqlDbType.NVarChar, 50);
                    param.Value = data.ProccessNo;

                    param = cmd.Parameters.Add("@NameSurname", SqlDbType.NVarChar, 500);
                    param.Value = data.NameSurname;

                    param = cmd.Parameters.Add("@Position", SqlDbType.NVarChar, 500);
                    param.Value = data.Position;

                    param = cmd.Parameters.Add("@DateBirth", SqlDbType.Date);
                    param.Value = data.DateBirth;

                    param = cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 500);
                    param.Value = data.Phone;

                    param = cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 500);
                    param.Value = data.Email;

                    param = cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 5);
                    param.Value = data.Gender;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);

                    param.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    return Convert.ToInt32(param.Value);
                }
            }
        }

        public static bool DeleteDoctor(int doctorID)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DELETE_DoctorQuery, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@id", SqlDbType.Int);
                    param.Value = doctorID;
                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return Convert.ToBoolean(param.Value);
                }
            }
        }

        public static bool UpdateDoctor(Doctor data)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE_DoctorDataQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@Id", SqlDbType.Int);
                    param.Value = data.Id;

                    param = cmd.Parameters.Add("@Position", SqlDbType.NVarChar, 100);
                    param.Value = data.Position;

                    param = cmd.Parameters.Add("@DateBirth", SqlDbType.Date);
                    param.Value = data.DateBirth;

                    param = cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 500);
                    param.Value = data.Phone;

                    param = cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 500);
                    param.Value = data.Email;

                    param = cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 5);
                    param.Value = data.Gender;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);

                    param.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    return Convert.ToBoolean(param.Value);
                }
            }
        }


        #endregion [..DOCTORS..]



        #region [..GUARANTOR..]

        public static string GET_GuarantorProccessNo()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(GET_GuarantorProccessNoQuery, connection))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                        return null;
                    }
                }
            }
        }

        public static int InsertGuarantor(Guarantor data)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(INSERT_GuarantorQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@zamin_nomre", SqlDbType.NVarChar, 250);
                    param.Value = data.ProccessNo;

                    param = cmd.Parameters.Add("@TARIX", SqlDbType.Date);
                    param.Value = DateTime.Now;

                    param = cmd.Parameters.Add("@COMPANYNAME", SqlDbType.NVarChar, 100);
                    param.Value = data.CompanyName;

                    param = cmd.Parameters.Add("@VOEN", SqlDbType.NVarChar, 500);
                    param.Value = data.Voen;

                    param = cmd.Parameters.Add("@AD", SqlDbType.NVarChar, 250);
                    param.Value = data.Name;

                    param = cmd.Parameters.Add("@SOYAD", SqlDbType.NVarChar, 250);
                    param.Value = data.Surname;

                    param = cmd.Parameters.Add("@ATAADI", SqlDbType.NVarChar, 250);
                    param.Value = data.FatherName;

                    param = cmd.Parameters.Add("@DOGUM_TARIX", SqlDbType.Date);
                    param.Value = data.DateBirth;

                    param = cmd.Parameters.Add("@SVNO", SqlDbType.NVarChar, 250);
                    param.Value = data.SvNo;

                    param = cmd.Parameters.Add("@FINKOD", SqlDbType.NVarChar, 250);
                    param.Value = data.FinCode;

                    param = cmd.Parameters.Add("@UNVAN", SqlDbType.NVarChar, 500);
                    param.Value = data.Address;

                    param = cmd.Parameters.Add("@FAKTIKI_YASAYIS_YERI", SqlDbType.NVarChar, 500);
                    param.Value = data.ResidentialAddress;

                    param = cmd.Parameters.Add("@SV_VERILME_TARIX", SqlDbType.Date);
                    param.Value = data.SV_Start;

                    param = cmd.Parameters.Add("@SV_BITME_TARIX", SqlDbType.Date);
                    param.Value = data.SV_End;

                    param = cmd.Parameters.Add("@CINSI", SqlDbType.NVarChar, 20);
                    param.Value = data.Gender;

                    param = cmd.Parameters.Add("@VETENDASLIG", SqlDbType.NVarChar, 250);
                    param.Value = data.Nation;

                    param = cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar, 250);
                    param.Value = data.Email;

                    param = cmd.Parameters.Add("@MOBIL", SqlDbType.NVarChar, 250);
                    param.Value = data.MobPhone;

                    param = cmd.Parameters.Add("@EV", SqlDbType.NVarChar, 250);
                    param.Value = data.HomePhone;

                    param = cmd.Parameters.Add("@GEYD", SqlDbType.NVarChar, 250);
                    param.Value = data.Comment;

                    param = cmd.Parameters.Add("@HESAB_NOM", SqlDbType.NVarChar, 500);
                    param.Value = data.BankAccountNumber;

                    param = cmd.Parameters.Add("@BANK_ADI", SqlDbType.NVarChar, 500);
                    param.Value = data.BankName;

                    param = cmd.Parameters.Add("@BANK_VOEN", SqlDbType.NVarChar, 500);
                    param.Value = data.BankVoen;

                    param = cmd.Parameters.Add("@KOD", SqlDbType.NVarChar, 500);
                    param.Value = data.BankCode;

                    param = cmd.Parameters.Add("@SWIFT", SqlDbType.NVarChar, 500);
                    param.Value = data.BankSwift;

                    param = cmd.Parameters.Add("@ISDELETED", SqlDbType.Int);
                    param.Value = 0;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);

                    param.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    return Convert.ToInt32(param.Value);
                }
            }
        }

        public static bool DeleteGuarantor(int customerID)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DELETE_GuarantorQuery, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@id", SqlDbType.Int);
                    param.Value = customerID;
                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return Convert.ToBoolean(param.Value);
                }
            }
        }

        public static bool UpdateGuarantor(Guarantor data)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE_GuarantorDataQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@ID", SqlDbType.NVarChar, 100);
                    param.Value = data.ID;

                    param = cmd.Parameters.Add("@VOEN", SqlDbType.NVarChar, 500);
                    param.Value = data.Voen;

                    param = cmd.Parameters.Add("@AD", SqlDbType.NVarChar, 250);
                    param.Value = data.Name;

                    param = cmd.Parameters.Add("@SOYAD", SqlDbType.NVarChar, 250);
                    param.Value = data.Surname;

                    param = cmd.Parameters.Add("@ATAADI", SqlDbType.NVarChar, 250);
                    param.Value = data.FatherName;

                    param = cmd.Parameters.Add("@DOGUM_TARIX", SqlDbType.Date);
                    param.Value = data.DateBirth;

                    param = cmd.Parameters.Add("@SVNO", SqlDbType.NVarChar, 250);
                    param.Value = data.SvNo;

                    param = cmd.Parameters.Add("@FINKOD", SqlDbType.NVarChar, 250);
                    param.Value = data.FinCode;

                    param = cmd.Parameters.Add("@UNVAN", SqlDbType.NVarChar, 500);
                    param.Value = data.Address;

                    param = cmd.Parameters.Add("@FAKTIKI_YASAYIS_YERI", SqlDbType.NVarChar, 500);
                    param.Value = data.ResidentialAddress;

                    param = cmd.Parameters.Add("@SV_VERILME_TARIX", SqlDbType.Date);
                    param.Value = data.SV_Start;

                    param = cmd.Parameters.Add("@SV_BITME_TARIX", SqlDbType.Date);
                    param.Value = data.SV_End;

                    param = cmd.Parameters.Add("@CINSI", SqlDbType.NVarChar, 20);
                    param.Value = data.Gender;

                    param = cmd.Parameters.Add("@VETENDASLIG", SqlDbType.NVarChar, 250);
                    param.Value = data.Nation;

                    param = cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar, 250);
                    param.Value = data.Email;

                    param = cmd.Parameters.Add("@MOBIL", SqlDbType.NVarChar, 250);
                    param.Value = data.MobPhone;

                    param = cmd.Parameters.Add("@EV", SqlDbType.NVarChar, 250);
                    param.Value = data.HomePhone;

                    param = cmd.Parameters.Add("@GEYD", SqlDbType.NVarChar, 250);
                    param.Value = data.Comment;

                    param = cmd.Parameters.Add("@HESAB_NOM", SqlDbType.NVarChar, 500);
                    param.Value = data.BankAccountNumber;

                    param = cmd.Parameters.Add("@BANK_ADI", SqlDbType.NVarChar, 500);
                    param.Value = data.BankName;

                    param = cmd.Parameters.Add("@BANK_VOEN", SqlDbType.NVarChar, 500);
                    param.Value = data.BankVoen;

                    param = cmd.Parameters.Add("@KOD", SqlDbType.NVarChar, 500);
                    param.Value = data.BankCode;

                    param = cmd.Parameters.Add("@SWIFT", SqlDbType.NVarChar, 500);
                    param.Value = data.BankSwift;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);

                    param.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    return Convert.ToBoolean(param.Value);
                }
            }
        }

        #endregion [..GUARANTOR..]



        #region [..SUPPLİERS..]

        public static string GET_SupplierProccessNo()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(GET_SupplierProccessNoQuery, connection))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                        return null;
                    }
                }
            }
        }

        public static int InsertSupplier(Supplier data)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(INSERT_SupplierQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@TECHIZATCI_NOMRE", SqlDbType.NVarChar, 500);
                    param.Value = data.ProccessNo;
                    param = cmd.Parameters.Add("@MUGAVİLE_NOM", SqlDbType.NVarChar, 500);
                    param.Value = data.ContractNo;
                    param = cmd.Parameters.Add("@SIRKET_ADI", SqlDbType.NVarChar, 500);
                    param.Value = data.SupplierName;
                    param = cmd.Parameters.Add("@UNVAN", SqlDbType.NVarChar, 500);
                    param.Value = data.Address;
                    param = cmd.Parameters.Add("@ELAGE_NOMRE", SqlDbType.NVarChar, 500);
                    param.Value = data.MobPhone;
                    param = cmd.Parameters.Add("@ELEKTRON_POCT", SqlDbType.NVarChar, 500);
                    param.Value = data.Email;
                    param = cmd.Parameters.Add("@ILKIN_BORC", SqlDbType.Decimal);
                    param.Value = data.Debt;
                    param = cmd.Parameters.Add("@SAHIBKAR_TECHIZATCI", SqlDbType.NVarChar, 500);
                    param.Value = data.BorcTeyinati;
                    param = cmd.Parameters.Add("@TECHIZATCI_VOEN", SqlDbType.NVarChar, 500);
                    param.Value = data.Voen;
                    param = cmd.Parameters.Add("@HESAB_AD", SqlDbType.NVarChar, 500);
                    param.Value = data.BankAccountNumber;
                    param = cmd.Parameters.Add("@BANK_ADI", SqlDbType.NVarChar, 500);
                    param.Value = data.BankName;
                    param = cmd.Parameters.Add("@BANK_VOEN", SqlDbType.NVarChar, 500);
                    param.Value = data.BankVoen;
                    param = cmd.Parameters.Add("@KOD", SqlDbType.NVarChar, 500);
                    param.Value = data.BankCode;
                    param = cmd.Parameters.Add("@SWIFT", SqlDbType.NVarChar, 500);
                    param.Value = data.BankSwift;
                    param = cmd.Parameters.Add("@DESCRIPTION", SqlDbType.NVarChar, 500);
                    param.Value = data.Comment;
                    param = cmd.Parameters.Add("@ISDELETED", SqlDbType.Int);
                    param.Value = 0;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);

                    param.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    return Convert.ToInt32(param.Value);
                }
            }
        }

        public static bool UpdateSupplier(Supplier data)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE_SupplierQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
                    param.Value = data.SupplierID;
                    param = cmd.Parameters.Add("@MUGAVİLE_NOM", SqlDbType.NVarChar, 500);
                    param.Value = data.ContractNo;
                    param = cmd.Parameters.Add("@UNVAN", SqlDbType.NVarChar, 500);
                    param.Value = data.Address;
                    param = cmd.Parameters.Add("@ELAGE_NOMRE", SqlDbType.NVarChar, 500);
                    param.Value = data.MobPhone;
                    param = cmd.Parameters.Add("@ELEKTRON_POCT", SqlDbType.NVarChar, 500);
                    param.Value = data.Email;
                    param = cmd.Parameters.Add("@ILKIN_BORC", SqlDbType.Decimal);
                    param.Value = data.Debt;
                    param = cmd.Parameters.Add("@SAHIBKAR_TECHIZATCI", SqlDbType.Int);
                    param.Value = data.BorcTeyinati;
                    param = cmd.Parameters.Add("@TECHIZATCI_VOEN", SqlDbType.NVarChar, 500);
                    param.Value = data.Voen;
                    param = cmd.Parameters.Add("@HESAB_AD", SqlDbType.NVarChar, 500);
                    param.Value = data.BankAccountNumber;
                    param = cmd.Parameters.Add("@BANK_ADI", SqlDbType.NVarChar, 500);
                    param.Value = data.BankName;
                    param = cmd.Parameters.Add("@BANK_VOEN", SqlDbType.NVarChar, 500);
                    param.Value = data.BankVoen;
                    param = cmd.Parameters.Add("@KOD", SqlDbType.NVarChar, 500);
                    param.Value = data.BankCode;
                    param = cmd.Parameters.Add("@SWIFT", SqlDbType.NVarChar, 500);
                    param.Value = data.BankSwift;
                    param = cmd.Parameters.Add("@DESCRIPTION", SqlDbType.NVarChar, 500);
                    param.Value = data.Comment;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);

                    param.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();


                    return Convert.ToBoolean(param.Value);
                }
            }
        }

        public static bool DeleteSupplier(int customerID)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(DELETE_SupplierQuery, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@id", SqlDbType.Int);
                    param.Value = customerID;
                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return Convert.ToBoolean(param.Value);
                }
            }
        }

        #endregion [..SUPPLİERS..]



        #region [..CLINIC REPORT..]

        public static DataTable Get_ClinicDataLoad()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(GET_ClinicDataLoadQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserID", Properties.Settings.Default.UserID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        public static void Insert_ClinicData(string customerName, string doctorName)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(INSERT_ClinicDataQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;

                    param = cmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar, 500);
                    param.Value = customerName;
                    param = cmd.Parameters.Add("@DoctorName", SqlDbType.NVarChar, 500);
                    param.Value = doctorName;
                    param = cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 500);
                    param.Value = Properties.Settings.Default.UserID;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion [..CLINIC REPORT..]



        #region [..GAİME SALES..]

        public static string GET_GaimeSalesProccessNo()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(GET_GaimeSalesProccessNoQuery, connection))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                        return null;
                    }
                }
            }
        }

        public static string GET_GaimeRefundProccessNo()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(GET_GaimeRefundProccessNoQuery, connection))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                        return null;
                    }
                }
            }
        }

        public static int InsertGaimeMain(GaimeMain data)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(INSERT_GaimeSalesMainQuery, con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@EMELIYYAT_NOMRE", SqlDbType.NVarChar, 50);
                    param.Value = data.ProccessNo;
                    param = cmd.Parameters.Add("@GAIME_NOMRE", SqlDbType.NVarChar, 20);
                    param.Value = data.QaimeNomre = string.IsNullOrWhiteSpace(data.QaimeNomre) ? data.ProccessNo.Replace("QS-", "") : data.QaimeNomre;
                    param = cmd.Parameters.Add("@ODENILEN_MEBLEG", SqlDbType.NVarChar, 100);
                    param.Value = data.TotalPaid;
                    param = cmd.Parameters.Add("@TARIX", SqlDbType.Date);
                    param.Value = data.Date;
                    param = cmd.Parameters.Add("@ODEME_TIPI", SqlDbType.NVarChar, 50);
                    param.Value = data.PaymentType;
                    param = cmd.Parameters.Add("@musteri", SqlDbType.NVarChar, 500);
                    param.Value = data.Customer;
                    param = cmd.Parameters.Add("@u_id", SqlDbType.Int);
                    param.Value = Properties.Settings.Default.UserID;
                    param = cmd.Parameters.Add("@ODENILEN_EDV_SIZ_MEBLEG", SqlDbType.NVarChar, 20);
                    param.Value = data.Edvsiz;
                    param = cmd.Parameters.Add("@ODENILEN_MEBLEG_EDV", SqlDbType.NVarChar, 20);
                    param.Value = data.Edvli;
                    param = cmd.Parameters.Add("@musteri_main_id", SqlDbType.Int);
                    param.Value = data.CustomerId;

                    param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output; ;

                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(param.Value);
                }
            }
        }

        #endregion [..GAİME SALES..]



        #region [.. REPORTS ..]

        public static async Task<DataTable> Get_ProductSalesDataAsync(string barcode)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(GET_GetProductSalesDataQuery, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = barcode;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
        }

        public static async Task<DataTable> Get_ProductPurchasesDataAsync(string barcode)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(GET_GetProductPurchaseDataQuery, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = barcode;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
        }

        #endregion [.. REPORTS ..]



        #region [.. INCOME AND EXPENSE..]

        public static async Task<int> InsertIncomeAndExpense(IncomeAndExpense item)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(INSERT_IncomeAndExpenseDataQuery, con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@Type", SqlDbType.Int);
                    param.Value = item.Type;
                    param = cmd.Parameters.Add("@Header", SqlDbType.NVarChar, 200);
                    param.Value = item.Header;
                    param = cmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                    param.Value = item.Amount;
                    param = cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 500);
                    param.Value = item.Comment;
                    param = cmd.Parameters.Add("@Date", SqlDbType.Date);
                    param.Value = item.Date;
                    param = cmd.Parameters.Add("@UserId", SqlDbType.Int);
                    param.Value = Properties.Settings.Default.UserID;
                    param = cmd.Parameters.Add("@LogDate", SqlDbType.DateTime);
                    param.Value = DateTime.Now;

                    param = cmd.Parameters.Add("@ResultId", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    await cmd.ExecuteNonQueryAsync();
                    return Convert.ToInt32(param.Value);
                }
            }
        }

        #endregion [.. INCOME AND EXPENSE ..]


        #endregion [...PROCEDURES METHODS...]
    }
}