using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using WindowsFormsApp2.App.Application;
using WindowsFormsApp2.Helpers.CacheData;
using WindowsFormsApp2.Helpers.Messages;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.DB.DTOs;
using static WindowsFormsApp2.Helpers.Enums;

namespace WindowsFormsApp2.Helpers.DB
{
    public static class DbProsedures
    {

        #region [...PROCEDURES QUERY...]

        private const string DELETE_ItemQuery = "delete_item";
        private const string INSERT_HeaderQuery = "INSERT_header";
        private const string INSERT_CalculationQuery = "insert_calculation";
        private const string INSERT_PosBasketQuery = "InsertBasketData";
        private const string ExportPosBasketQuery = "ExportBasketDataToCalculation";
        private const string GET_BasketDataLoadQuery = "PosBasketDataLoad";
        private const string GET_CategoryExistsQuery = "SELECT_COUNT_KATEGORY";
        private const string INSERT_CategoryQuery = "SELECT_KATEGORY";
        private const string INSERT_CustomerQuery = "INSERT_MUSTERI";
        private const string INSERT_DoctorQuery = "INSERT_DOCTOR";
        private const string DELETE_DoctorQuery = "delete_doctor";
        private const string GET_DoctorProccessNoQuery = "EXEC dbo.DOCTOR_EMELIYYAT_NOMRE";
        private const string UPDATE_DoctorDataQuery = "UPDATE_DOCTOR";
        private const string GET_SupplierProccessNoQuery = "EXEC dbo.TECHIZATCI_NOMRE";
        private const string DELETE_SupplierQuery = "search_techizatci_delete";
        private const string GET_GuarantorProccessNoQuery = "EXEC dbo.ZAMIN_EMELIYYAT_NOMRE";
        private const string INSERT_GuarantorQuery = "INSERT_ZAMIN";
        private const string DELETE_GuarantorQuery = "delete_zamin";
        private const string UPDATE_GuarantorDataQuery = "UPDATE_ZAMIN";
        private const string GET_RefundProccesNoQuery = "EXEC dbo.POS_GAYTARMA";
        private const string INSERT_ClinicDataQuery = "ClinicReportInsertData";
        public static readonly string GET_ClinicDataLoadQuery = $"EXEC [dbo].[ClinicReportDataLoad]@UserID = {Properties.Settings.Default.UserID}";
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
                Cursor.Current = Cursors.WaitCursor;
                using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
                using (SqlCommand cmd = new SqlCommand(SqlQuery, connection))
                {
                    connection.Open();
                    cmd.CommandType = type;
                    cmd.CommandTimeout = 150;
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
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
                FormHelpers.Log(ex.Message);
                return null;
            }
            finally { Cursor.Current = Cursors.Default; }
        }


        #region [..COMPANY..]

        public static int InsertCompany(Company data)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = "INSERT_COMPANY";
                using (SqlCommand cmd = new SqlCommand(query, connection))
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

        public static Company GetCompany()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = "SELECT *  FROM SELECT_COMPANY_DATA_LOAD(@userID)";
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userID", Properties.Settings.Default.UserID);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            var data = FormHelpers.MapReaderToObject<Company>(dr);
                            return data;
                        }
                        return null;
                    }
                }
            }
        }

        #endregion [..COMPANY..]



        #region [.. USER AND ROLE ..]

        public static User GetUser(int userId = 0)
        {
            if (userId is 0)
                userId = Properties.Settings.Default.UserID;

            string query = "SELECT * FROM SELECT_USER_DATA_LOAD(@userID)";

            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue("@userID", userId);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        var user = FormHelpers.MapReaderToObject<User>(dr);
                        user.UserRole = GetRole(userId);
                        return user;
                    }
                    return null;
                }
            }
        }

        public static int InsertUser(User item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = "userParol_insert";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@login", SqlDbType.NVarChar, 100);
                    param.Value = item.Username;
                    param = cmd.Parameters.Add("@parol", SqlDbType.NVarChar, 100);
                    param.Value = item.Password;
                    param = cmd.Parameters.Add("@admin", SqlDbType.Bit);
                    param.Value = item.IsAdmin;
                    param = cmd.Parameters.Add("@AD", SqlDbType.NVarChar, 100);
                    param.Value = item.NameSurname;
                    param = cmd.Parameters.Add("@EMAILL", SqlDbType.NVarChar, 100);
                    param.Value = item.Email;
                    param = cmd.Parameters.Add("@TELEFON", SqlDbType.NVarChar, 50);
                    param.Value = item.Phone;
                    param = cmd.Parameters.Add("@DOGUM_TARIXI", SqlDbType.Date);
                    param.Value = item.DateBirth;
                    param = cmd.Parameters.Add("@PosSaleScreen", SqlDbType.Bit);
                    param.Value = item.PosSaleScreen;
                    SqlParameter outputIdParam = new SqlParameter("@UserId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputIdParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    int UserId = (int)outputIdParam.Value;
                    return UserId;
                }
            }
        }

        public static void UpdateUser(User item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = "userParol_update";
                using (SqlCommand cmd = new SqlCommand(query, connection))
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
                    param = cmd.Parameters.Add("@DOGUM_TARIXI", SqlDbType.Date);
                    param.Value = item.DateBirth;
                    param = cmd.Parameters.Add("PosSaleScreen", SqlDbType.Bit);
                    param.Value = item.PosSaleScreen;

                    cmd.ExecuteNonQuery();
                    FormHelpers.Log($"{item.Username} İstifadəçisində düzəliş edildi");
                }
            }
        }

        public static void DeleteUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = "userParol_delete";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@userID", SqlDbType.Int);
                    param.Value = userId;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    DeleteRole(userId);
                }
            }
        }

        public static void InsertRole(UserRole item)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("userRole_insert", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", item.UserId);
                cmd.Parameters.AddWithValue("@ProductAdd", item.ProductAdd);
                cmd.Parameters.AddWithValue("@RefundProduct", item.RefundProduct);
                cmd.Parameters.AddWithValue("@ProductDelete", item.ProductDelete);
                cmd.Parameters.AddWithValue("@ProductDiscount", item.ProductDiscount);
                cmd.Parameters.AddWithValue("@ProductBarcodePrint", item.ProductBarcodePrint);
                cmd.Parameters.AddWithValue("@ScalesProductDownload", item.ScalesProductDownload);
                cmd.Parameters.AddWithValue("@Suppliers", item.Suppliers);
                cmd.Parameters.AddWithValue("@Customers", item.Customers);
                cmd.Parameters.AddWithValue("@BankSale", item.BankSale);
                cmd.Parameters.AddWithValue("@Credit", item.Credit);
                cmd.Parameters.AddWithValue("@PosPrepayment", item.PosPrepayment);
                cmd.Parameters.AddWithValue("@PosSale", item.PosSale);
                cmd.Parameters.AddWithValue("@PosRefund", item.PosRefund);
                cmd.Parameters.AddWithValue("@PosSalePriceEdit", item.PosSalePriceEdit);
                var param = new SqlParameter("@PosSalePriceLimit", SqlDbType.Decimal);
                param.Precision = 18;
                param.Scale = 2;
                param.Value = item.PosSalePriceLimit.HasValue ? (object)item.PosSalePriceLimit.Value : DBNull.Value;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@Report", item.Report);
                cmd.Parameters.AddWithValue("@TerminalDelete", item.TerminalDelete);
                cmd.Parameters.AddWithValue("@Payments", item.Payments);
                cmd.Parameters.AddWithValue("@Users", item.Users);
                cmd.Parameters.AddWithValue("@Backups", item.Backups);
                cmd.Parameters.AddWithValue("@Logs", item.Logs);
                cmd.Parameters.AddWithValue("@ScalesDelete", item.ScalesDelete);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateRole(UserRole item)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("userRole_update", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", item.UserId);
                cmd.Parameters.AddWithValue("@ProductAdd", item.ProductAdd);
                cmd.Parameters.AddWithValue("@RefundProduct", item.RefundProduct);
                cmd.Parameters.AddWithValue("@ProductDelete", item.ProductDelete);
                cmd.Parameters.AddWithValue("@ProductDiscount", item.ProductDiscount);
                cmd.Parameters.AddWithValue("@ProductBarcodePrint", item.ProductBarcodePrint);
                cmd.Parameters.AddWithValue("@ScalesProductDownload", item.ScalesProductDownload);
                cmd.Parameters.AddWithValue("@Suppliers", item.Suppliers);
                cmd.Parameters.AddWithValue("@Customers", item.Customers);
                cmd.Parameters.AddWithValue("@BankSale", item.BankSale);
                cmd.Parameters.AddWithValue("@Credit", item.Credit);
                cmd.Parameters.AddWithValue("@PosPrepayment", item.PosPrepayment);
                cmd.Parameters.AddWithValue("@PosSale", item.PosSale);
                cmd.Parameters.AddWithValue("@PosRefund", item.PosRefund);
                cmd.Parameters.AddWithValue("@PosSalePriceEdit", item.PosSalePriceEdit);
                var param = new SqlParameter("@PosSalePriceLimit", SqlDbType.Decimal);
                param.Precision = 18;
                param.Scale = 2;
                param.Value = item.PosSalePriceLimit.HasValue ? (object)item.PosSalePriceLimit.Value : DBNull.Value;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@Report", item.Report);
                cmd.Parameters.AddWithValue("@TerminalDelete", item.TerminalDelete);
                cmd.Parameters.AddWithValue("@Payments", item.Payments);
                cmd.Parameters.AddWithValue("@Users", item.Users);
                cmd.Parameters.AddWithValue("@Backups", item.Backups);
                cmd.Parameters.AddWithValue("@Logs", item.Logs);
                cmd.Parameters.AddWithValue("@ScalesDelete", item.ScalesDelete);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static void DeleteRole(int userId)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = $"DELETE FROM UserRole WHERE UserId = {userId}";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static UserRole GetRole(int userId = 0)
        {
            if (userId is 0)
                userId = Properties.Settings.Default.UserID;

            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = "SELECT * FROM GetUserRole(@userID)";
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userID", userId);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            var data = FormHelpers.MapReaderToObject<UserRole>(dr);
                            return data;
                        }
                        return null;
                    }
                }
            }
        }

        #endregion [.. USER AND ROLE ..]



        #region [.. SALES AND REFUND..]

        public static int InsertPosSales(PosSales item)
        {
            string query = "azmart_sale_insert";
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter parameter;

                parameter = cmd.Parameters.Add("@documentID", SqlDbType.NVarChar, 100);
                parameter.Value = item.posNomre;

                parameter = cmd.Parameters.Add("@fiscalID", SqlDbType.NVarChar, 500);
                parameter.Value = item.longFiskalId;

                parameter = cmd.Parameters.Add("@user_id", SqlDbType.Int);
                parameter.Value = Properties.Settings.Default.UserID;

                parameter = cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar, 50);
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

                parameter = cmd.Parameters.Add("@bankTransactionId", SqlDbType.NVarChar, size: 50);
                parameter.Value = item.BankTransactionId;

                parameter = cmd.Parameters.Add("@bankTransactionNumber", SqlDbType.NVarChar, size: 50);
                parameter.Value = item.BankTransactionNumber;

                parameter = cmd.Parameters.Add("@bankApprovalCode", SqlDbType.NVarChar, size: 50);
                parameter.Value = item.BankApprovalCode;

                parameter = cmd.Parameters.Add("@customerId", SqlDbType.Int);
                parameter.Value = item.customerId;

                parameter = cmd.Parameters.Add("@doctorId", SqlDbType.Int);
                parameter.Value = item.doctorId;

                parameter = cmd.Parameters.Add("@Prepayment", SqlDbType.Int);
                parameter.Value = item.Prepayment;

                connection.Open();
                parameter = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
                parameter.Direction = ParameterDirection.Output;
                var result = cmd.ExecuteNonQuery();

                bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("CloudApp").ToString());
                if (control && result > 0)
                {
                    Task.Run(async () =>
                    {
                        var facade = new SyncFacade();
                        await facade.SendSalesAsync();
                        await facade.SendStockAsync();
                        await facade.SendPaymentsAsync();
                        await facade.SendProfitAsync();
                    });
                }

                return Convert.ToInt32(parameter.Value);
            }
        }

        public static void InsertItem(DatabaseClasses.Item item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = "INSERT_Item";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@name", SqlDbType.NVarChar, int.MaxValue);
                    param.Value = item.Name;

                    param = cmd.Parameters.Add("@code", SqlDbType.NVarChar, 500);
                    param.Value = item.Code;

                    param = cmd.Parameters.Add("@quantity", SqlDbType.Decimal);
                    param.Value = item.Quantity;

                    param = cmd.Parameters.Add("@salePrice", SqlDbType.Decimal);
                    param.Value = item.SalePrice;

                    param = cmd.Parameters.Add("@discount", SqlDbType.Decimal);
                    param.Value = item.Discount;

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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            const string query = "insert_pos_gaytarma_manual";
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
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

        public static string GET_SalesProcessNo()
        {
            string query = "exec dbo.pos_emeliyyat_nomre";
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        return dr["col1"].ToString();
                    return null;
                }
            }
        }

        public async static Task<string> GET_TotalSalesCount()
        {
            const string query = @"SELECT COUNT(*) 
FROM pos_satis_check_main
WHERE date_ BETWEEN CAST(GETDATE() AS DATE) AND DATEADD(DAY, 1, CAST(GETDATE() AS DATE));";
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                await connection.OpenAsync();
                var result = await cmd.ExecuteScalarAsync();
                int count = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                return count.ToString();
            }
        }

        public static bool InsertPosBasketData(string basketName)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
                using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
                using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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

        public static void INSERT_PosDiscount(PosDiscount item)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pos_guzest_insert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar, 100);
                    param.Value = item.ProccessNo;

                    param = cmd.Parameters.Add("@mal_details_id", SqlDbType.Int);
                    param.Value = item.ProductId;

                    param = cmd.Parameters.Add("@endirim_faiz", SqlDbType.NVarChar, 100);
                    param.Value = item.DiscountPercent;

                    param = cmd.Parameters.Add("@endirim_azn", SqlDbType.NVarChar, 100);
                    param.Value = item.DiscountAmount;

                    param = cmd.Parameters.Add("@userId", SqlDbType.Int);
                    param.Value = item.UserId;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DELETE_PosGridScreen(int productId, string proccessNo)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("delete_grid_pos", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@mal_details_id", SqlDbType.Int).Value = productId;
                cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar, 100).Value = proccessNo;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = UserCacheService.User.Id;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        #endregion [.. SALES AND REFUND ..]



        #region [..CATEGORY..]

        public static int Exists_Category(string CategoryName)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            string query = "yoxlama_mehsul_kodu";
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param;

                param = cmd.Parameters.Add("@mehsul_kodu", SqlDbType.NVarChar, 500);
                param.Value = productCode;

                param = cmd.Parameters.Add("@techizatci_id", SqlDbType.Int);
                param.Value = supplierId;

                param = cmd.Parameters.Add("@empcount", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                connection.Open();
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(param.Value);
            }
        }

        /// <summary>
        /// 0 - Məhsul yoxdur
        /// 1 - Barkod fərqli məhsulda istifadə olunur
        /// 2 - Barkod sadəcə daxil edilən məhsul adında istifadə olunur
        /// </summary>
        public static int Exists_ProductBarcode(string barcode, string productName)
        {
            string query = "CheckProductBarcodeControl";

            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param;

                param = cmd.Parameters.Add("@barcode", SqlDbType.NVarChar, 200);
                param.Value = barcode;

                param = cmd.Parameters.Add("@name", SqlDbType.NVarChar, Int32.MaxValue);
                param.Value = productName;

                param = cmd.Parameters.Add("@empcount", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                connection.Open();
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(param.Value);
            }
        }

        public static int InsertProductMain(ProductsMain item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT_MAL_ALISI_MAIN", connection))
            {
                connection.Open();
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

        public static int InsertImportProductMain(ProductsMain item)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT_IMPORT_MAL_ALISI_MAIN", connection))
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

                connection.Open();
                cmd.ExecuteNonQuery();


                FormHelpers.OperationLog(new OperationLogs
                {
                    OperationType = OperationType.AddProduct,
                    OperationId = Convert.ToInt32(param.Value)
                });

                return Convert.ToInt32(param.Value);
            }
        }

        public static async Task<int?> InsertProductDetails(ProductsDetail item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
                using (SqlCommand cmd = new SqlCommand("INSERT_MAL_ALISI_DETAILS", connection))
                {
                    connection.Open();
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
                    param = cmd.Parameters.Add("@MIGDARI", SqlDbType.Decimal);
                    param.Value = item.Quantity;
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
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
                return null;
            }
        }

        private async static Task UpdateProductImage(int Id, string barcode)
        {
            //Məhsulda şəkil varsa yalnız bu kod işləyir
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("DELETE_PRODUCT_MAL_ALIS_DETAILS", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param;

                param = cmd.Parameters.Add("@TECHIZATCI", SqlDbType.NVarChar, 500);
                param.Value = item.SupplierName;

                param = cmd.Parameters.Add("@BARCODE", SqlDbType.NVarChar, 500);
                param.Value = item.Barocde;

                param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                connection.Open();
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(param.Value);
            }
        }

        public static string GET_ProductProcessNo()
        {
            string query = "EXEC dbo.MAL_ALISI_EMELIYYAT_NOMRE";
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        return dr[0].ToString();
                    return null;
                }
            }
        }

        public static int ProductNegativeStatus(bool status)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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

        private static void ProductNegativeStatus()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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

        public static string GET_ProductReturnProcessNo()
        {
            string query = "EXEC  dbo.MAL_GAYTARMA_KOD";
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        return dr[0].ToString();
                    return null;
                }
            }
        }

        public static string GET_ProductReturnDebtTotal(int SupplierId)
        {
            /*           string oldquery = $@"SELECT 
            //  Y.BORC - X.GAYTARMA_MEBLEG AS BORC 
            //FROM 
            //  (
            //    select 
            //      1 AS ID, 
            //      cast(
            //        sum(
            //          isnull(BORC, 0.00)
            //        ) as decimal(18, 3)
            //      ) as BORC 
            //    from 
            //      (
            //        SELECT 
            //          f.MAL_ALISI_MAIN_ID, 
            //          f.[FAKTURA NÖMRƏ], 
            //          f.TARIX, 
            //          f.QİYMƏT - isnull(t.odenis, 0.00) BORC, 
            //          0 AS 'ÖDƏNİŞ' 
            //        FROM 
            //          dbo.fn_TECHIZATCI_BORC({SupplierId}) f 
            //          left join(
            //            select 
            //              MAL_ALISI_MAIN_ID, 
            //              sum(ODENIS) odenis 
            //            from 
            //              TECHIZATCI_ODENIS 
            //            group by 
            //              MAL_ALISI_MAIN_ID
            //          ) t on f.MAL_ALISI_MAIN_ID = t.MAL_ALISI_MAIN_ID
            //      ) o
            //  ) Y 
            //  LEFT JOIN(
            //    SELECT 
            //      1 AS ID, 
            //      ISNULL(
            //        CAST(
            //          SUM(MD.ALIS_GIYMETI * D.MIGDARI) AS decimal(18, 3)
            //        ), 
            //        0.00
            //      ) AS GAYTARMA_MEBLEG 
            //    FROM 
            //      MAL_GEYTARMA_MAIN M 
            //      INNER JOIN MAL_GEYTARMA_DETAILS D ON M.MAL_GEYTARMA_MAIN_ID = D.MAL_GEYTARMA_MAIN_ID 
            //      INNER JOIN MAL_ALISI_DETAILS MD ON MD.MAL_ALISI_DETAILS_ID = D.MAL_ALISI_DETAILS_ID 
            //      INNER JOIN MAL_ALISI_MAIN MM ON MM.MAL_ALISI_MAIN_ID = MD.MAL_ALISI_MAIN_ID 
            //    WHERE 
            //      MM.TECHIZATCI_ID = {SupplierId}
            //  ) X ON X.ID = Y.ID
            //";*/

            string query = $@"WITH DEBT AS (
  SELECT 
    SUM(
      ISNULL(f.QİYMƏT, 0) - ISNULL(t.odenis, 0)
    ) AS BORC 
  FROM 
    dbo.fn_TECHIZATCI_BORC({SupplierId}) f 
    LEFT JOIN (
      SELECT 
        MAL_ALISI_MAIN_ID, 
        SUM(ODENIS) AS odenis 
      FROM 
        TECHIZATCI_ODENIS 
      GROUP BY 
        MAL_ALISI_MAIN_ID
    ) t ON t.MAL_ALISI_MAIN_ID = f.MAL_ALISI_MAIN_ID
), 
REFUND AS (
  SELECT 
    ISNULL(
      SUM(MD.ALIS_GIYMETI * D.MIGDARI), 
      0
    ) AS GAYTARMA_MEBLEG 
  FROM 
    MAL_GEYTARMA_MAIN M 
    INNER JOIN MAL_GEYTARMA_DETAILS D ON M.MAL_GEYTARMA_MAIN_ID = D.MAL_GEYTARMA_MAIN_ID 
    INNER JOIN MAL_ALISI_DETAILS MD ON MD.MAL_ALISI_DETAILS_ID = D.MAL_ALISI_DETAILS_ID 
    INNER JOIN MAL_ALISI_MAIN MM ON MM.MAL_ALISI_MAIN_ID = MD.MAL_ALISI_MAIN_ID 
  WHERE 
    MM.TECHIZATCI_ID = {SupplierId}
) 
SELECT 
  Y.BORC - X.GAYTARMA_MEBLEG AS BORC 
FROM 
  DEBT Y CROSS 
  JOIN REFUND X;";

            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        return dr[0].ToString();
                    return null;
                }
            }
        }

        public static int InsertRefundProductMain(string proccessNo, DateTime date)
        {
            string query = "INSERT_MAL_GAYTARMA_MAIN";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param;
                param = cmd.Parameters.Add("@EMELIYYAT_NOMRE", SqlDbType.NVarChar, 100);
                param.Value = proccessNo;
                param = cmd.Parameters.Add("@TARIX", SqlDbType.Date);
                param.Value = date;
                param = cmd.Parameters.Add("@_USER_ID", SqlDbType.Int);
                param.Value = UserCacheService.User.Id;
                param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Convert.ToInt32(param.Value);
            }
        }

        public static int InsertRefundProductDetail(int RefundProductId, int ProductId, decimal RefundQuantity, string Comment = null)
        {
            string query = "INSERT_MAL_GAYTARMA_DETAILS";

            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param;
                param = cmd.Parameters.Add("@MAL_GEYTARMA_MAIN_ID", SqlDbType.Int);
                param.Value = RefundProductId;
                param = cmd.Parameters.Add("@MAL_ALISI_DETAILS_ID", SqlDbType.Int);
                param.Value = ProductId;
                param = cmd.Parameters.Add("@MIGDARI", SqlDbType.Decimal);
                param.Value = RefundQuantity;
                param = cmd.Parameters.Add("@COMMENT", SqlDbType.NVarChar, int.MaxValue);
                param.Value = Comment;
                param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Convert.ToInt32(param.Value);
            }
        }

        #endregion [..PRODUCTS..]



        #region [..CUSTOMERS..]

        public static string GET_CustomerProccessNo()
        {
            const string query = "EXEC dbo.MUSTERI_EMELIYYAT_NOMRE";
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                var result = cmd.ExecuteScalar();
                return result.ToString();
            }
        }

        public static int InsertCustomer(Customer data)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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

        public static bool DeleteCustomer(int customerId)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("delete_customer", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param;
                param = cmd.Parameters.Add("@id", SqlDbType.Int);
                param.Value = customerId;
                param = cmd.Parameters.Add("@emp_count", SqlDbType.Bit);
                param.Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Convert.ToBoolean(param.Value);
            }
        }

        public static bool UpdateCustomer(Customer data)
        {
            const string query = "UPDATE_MUSTERI";
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param;

                param = cmd.Parameters.Add("@CustomerID", SqlDbType.NVarChar, 100);
                param.Value = data.CustomerID;

                param = cmd.Parameters.Add("@COMPANYNAME", SqlDbType.NVarChar, 500);
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

                param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);

                param.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();


                return Convert.ToBoolean(param.Value);
            }
        }

        public static void InsertCustomerDebt(CustomerDebtType type, DateTime date, int customerId, decimal amount)
        {
            string _date = date.ToString("yyyy-MM-dd HH:mm:ss");
            string query = $@"INSERT INTO [MUSTERILER_DEBTS] (OperationType, OperationDate, CustomerId, Amount) VALUES (
    {(int)type},
    '{_date}',
    {customerId},
    {amount.ToString().Replace(",", ".")})";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        #endregion [..CUSTOMERS..]



        #region [..DOCTORS..]

        public static string GET_DoctorProccessNo()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT_TECHIZATCI", connection))
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param;

                param = cmd.Parameters.Add("@TECHIZATCI_NOMRE", SqlDbType.NVarChar, 500);
                param.Value = data.ProccessNo;
                param = cmd.Parameters.Add("@CONTRACTDATE", SqlDbType.Date);
                param.Value = data.ContractDate;
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

        public static bool UpdateSupplier(Supplier data)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("search_techizatci_update", connection))
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param;

                param = cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
                param.Value = data.SupplierID;
                param = cmd.Parameters.Add("@CONTRACTDATE", SqlDbType.Date);
                param.Value = data.ContractDate;
                param = cmd.Parameters.Add("@SupplierName", SqlDbType.NVarChar, 500);
                param.Value = data.SupplierName;
                param = cmd.Parameters.Add("@MUGAVİLE_NOM", SqlDbType.NVarChar, 500);
                param.Value = data.ContractNo;
                param = cmd.Parameters.Add("@UNVAN", SqlDbType.NVarChar, 500);
                param.Value = data.Address;
                param = cmd.Parameters.Add("@ELAGE_NOMRE", SqlDbType.NVarChar, 500);
                param.Value = data.MobPhone;
                param = cmd.Parameters.Add("@ELEKTRON_POCT", SqlDbType.NVarChar, 500);
                param.Value = data.Email;
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

        public static bool DeleteSupplier(int customerID)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
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

        public static async Task<int> InsertSupplierDebt(SupplierDebt item)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = "INSERT_SUPPLIER_DEBT";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@SupplierId", SqlDbType.Int);
                    param.Value = item.SupplierId;
                    param = cmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                    param.Value = item.Amount;
                    param = cmd.Parameters.Add("@ContractNo", SqlDbType.NVarChar, 200);
                    param.Value = item.ContractNo;
                    param = cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, int.MaxValue);
                    param.Value = item.Comment;
                    param = cmd.Parameters.Add("@ContractDate", SqlDbType.Date);
                    param.Value = item.ContractDate;
                    param = cmd.Parameters.Add("@UserId", SqlDbType.Int);
                    param.Value = Properties.Settings.Default.UserID;

                    param = cmd.Parameters.Add("@ResultId", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    await cmd.ExecuteNonQueryAsync();
                    return Convert.ToInt32(param.Value);
                }
            }
        }

        public static string GET_SupplierDebtPayProccessNo()
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = "techizatci_odenis_emeliyyat_nomre";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@r", SqlDbType.NVarChar, 100);
                    param.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return param.Value.ToString();
                }
            }
        }

        public static async Task<int> InsertSupplierPay(SupplierDebtPay item)
        {
            string query = "INSERT_TECHIZATCI_ODENIS";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param;
                param = cmd.Parameters.Add("@MAL_ALISI_MAIN_ID", SqlDbType.Int);
                param.Value = item.ProductMainId;
                param = cmd.Parameters.Add("@SupplierDebtId", SqlDbType.Int);
                param.Value = item.SupplierDebtId;
                param = cmd.Parameters.Add("@SupplierId", SqlDbType.Int);
                param.Value = item.SupplierId;
                param = cmd.Parameters.Add("@ODENIS", SqlDbType.Decimal);
                param.Value = item.Pay;
                param = cmd.Parameters.Add("@ODENIS_TIPI", SqlDbType.NVarChar);
                param.Value = item.PaymentType;
                param = cmd.Parameters.Add("@GAIME_N", SqlDbType.NVarChar);
                param.Value = item.GaimeNo;
                param = cmd.Parameters.Add("@GEYD", SqlDbType.NVarChar);
                param.Value = item.Comment;
                param = cmd.Parameters.Add("@TARIX", SqlDbType.Date);
                param.Value = item.PayDate;
                param = cmd.Parameters.Add("@EMELIYYAT_NOMRE", SqlDbType.NVarChar, 250);
                param.Value = item.ProccessNo;
                param = cmd.Parameters.Add("@FAKTURA_NOMRE", SqlDbType.NVarChar, 50);
                param.Value = item.ContractNo;
                param = cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
                param.Value = Properties.Settings.Default.UserID;
                param = cmd.Parameters.Add("@ESAS_BORC_ODENIS", SqlDbType.Decimal);
                param.Value = item.MainDebtAmount;
                param = cmd.Parameters.Add("@EDV_BORC", SqlDbType.Decimal);
                param.Value = item.TaxDebtAmount;

                param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return Convert.ToInt32(param.Value);
            }
        }

        public static async Task<(decimal totalAmount, decimal mainAmount, decimal taxAmount)> GET_SupplierTotalDebt(int supplierId)
        {
            string query = "sp_GetSupplierDebt";

            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                await connection.OpenAsync();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SupplierId", supplierId);

                var totalAmountParam = new SqlParameter("@TOTAL_DEBT", SqlDbType.Decimal)
                {
                    Precision = 18,
                    Scale = 4,
                    Direction = ParameterDirection.Output
                };
                var mainAmountParam = new SqlParameter("@MAIN_DEBT", SqlDbType.Decimal)
                {
                    Precision = 18,
                    Scale = 4,
                    Direction = ParameterDirection.Output
                };
                var taxAmountParam = new SqlParameter("@TAX_DEBT", SqlDbType.Decimal)
                {
                    Precision = 18,
                    Scale = 4,
                    Direction = ParameterDirection.Output
                };

                cmd.Parameters.Add(totalAmountParam);
                cmd.Parameters.Add(mainAmountParam);
                cmd.Parameters.Add(taxAmountParam);

                await cmd.ExecuteNonQueryAsync();


                decimal totalAmount = (decimal)totalAmountParam.Value;
                decimal mainAmount = (decimal)mainAmountParam.Value;
                decimal taxAmount = (decimal)taxAmountParam.Value;

                return (totalAmount, mainAmount, taxAmount);
            }
        }

        #endregion [..SUPPLİERS..]



        #region [..CLINIC REPORT..]

        public static DataTable Get_ClinicDataLoad()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ClinicDataLoadQuery, connection))
            {
                connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserID", UserCacheService.User.Id);
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

        public static void Insert_ClinicData(string customerName, string doctorName)
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_GaimeSalesProccessNoQuery, connection))
            {
                connection.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        return dr[0].ToString();
                    return null;
                }
            }
        }

        public static string GET_GaimeRefundProccessNo()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(GET_GaimeRefundProccessNoQuery, connection))
            {
                connection.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        return dr[0].ToString();
                    return null;
                }
            }
        }

        public static int InsertGaimeMain(GaimeMain data)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT_GAIME_SATISI_MAIN", con))
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
                param.Value = UserCacheService.User.Id;
                param = cmd.Parameters.Add("@ODENILEN_EDV_SIZ_MEBLEG", SqlDbType.NVarChar, 20);
                param.Value = data.Edvsiz;
                param = cmd.Parameters.Add("@ODENILEN_MEBLEG_EDV", SqlDbType.NVarChar, 20);
                param.Value = data.Edvli;
                param = cmd.Parameters.Add("@musteri_main_id", SqlDbType.Int);
                param.Value = data.CustomerId;

                param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
                param.Direction = ParameterDirection.Output; ;

                con.Open();
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(param.Value);
            }
        }

        #endregion [..GAİME SALES..]



        #region [.. REPORTS ..]

        public static async Task<DataTable> Get_ProductSalesDataAsync(string barcode)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
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
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
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



        #region [.. TERMINALS ..]

        public static int TerminalAdd(Terminal item)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("KASSA_IP_INSERT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param;
                param = cmd.Parameters.Add("@KASSA_FIRMA_IP", SqlDbType.Int);
                param.Value = item.ModelId;
                param = cmd.Parameters.Add("@IP_ADRESS", SqlDbType.NVarChar, 100);
                param.Value = item.IpAddress;
                param = cmd.Parameters.Add("@BANK_NAME", SqlDbType.NVarChar, 100);
                param.Value = item.BankName;
                param = cmd.Parameters.Add("@KASSIR_ID", SqlDbType.Int);
                param.Value = item.UserId;
                param = cmd.Parameters.Add("@merchant_id", SqlDbType.NVarChar, 1000);
                param.Value = item.MerchantIdKey;

                param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(param.Value);
            }
        }

        public static void TerminalRemove(int Id)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = $"DELETE FROM KASSA_IP WHERE KASSA_IP_ID ={Id}";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion [.. TERMINALS ..]



        #region [.. TƏRƏZİ ..]

        public static bool TereziAdd(Terezi item)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("TERAZI_IP_INSERT", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@TERAZI_FIRMA_IP", SqlDbType.Int);
                    param.Value = item.ModelId;
                    param = cmd.Parameters.Add("@IP_ADRESS", SqlDbType.NVarChar, 100);
                    param.Value = item.IpAddress;
                    param = cmd.Parameters.Add("@UserID", SqlDbType.Int);
                    param.Value = item.UserId;
                    param = cmd.Parameters.Add("@FILEPATH", SqlDbType.NVarChar, int.MaxValue);
                    param.Value = item.FilePath;
                    param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    int result = Convert.ToInt32(param.Value);
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static void TereziRemove(int Id)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = $"DELETE FROM TERAZI_IP WHERE TERAZI_IP_ID={Id}";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static TeraziDTO GetTerezi()
        {
            string query = $@"SELECT 
TERAZI_IP_ID AS Id,
tf.TERAZI_FIRMALAR AS ModelName,
ti.IP_ADRESS AS IpAddress,
FilePath,
UserId
FROM TERAZI_IP ti
INNER JOIN TERAZI_FIRMALAR tf ON tf.TERAZI_FIRMALAR_ID = ti.TERAZI_FIRMA_IP
WHERE UserId = {Properties.Settings.Default.UserID}";
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        var data = FormHelpers.MapReaderToObject<TeraziDTO>(dr);
                        return data;
                    }
                    return null;
                }
            }
        }

        #endregion [.. TƏRƏZİ ..]



        #region [.. PRINTERS ..]

        public static int PrinterAdd(Printer item)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = "PRINTER_INSERT";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@PrinterName", SqlDbType.NVarChar, 100);
                    param.Value = item.PrinterName;
                    param = cmd.Parameters.Add("@PortName", SqlDbType.NVarChar, 100);
                    param.Value = item.PortName;
                    //param = cmd.Parameters.Add("@IpAddress", SqlDbType.NVarChar, 100);
                    //param.Value = item.IpAddress;
                    param = cmd.Parameters.Add("@PrintType", SqlDbType.NVarChar, 100);
                    param.Value = item.PrintType;
                    param = cmd.Parameters.Add("@UserId", SqlDbType.Int);
                    param.Value = item.UserId;

                    param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(param.Value);
                }
            }
        }

        public static void PrinterRemove(int Id)
        {
            string query = $"DELETE FROM PRINTERS WHERE Id ={Id}";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Printer> GetSelectedPrinter()
        {
            string query = "SELECT * FROM SELECT_PRINTER_DATA_LOAD(@userID)";

            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue("@userID", Properties.Settings.Default.UserID);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    List<Printer> data = FormHelpers.MapReaderToList<Printer>(dr);
                    return data;
                }
            }
        }

        #endregion [.. PRINTERS ..]



        #region [.. DISCOUNT PRODUCT ..]

        public async static Task INSERT_DiscountProductAsync(List<DiscountProduct> items)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                await con.OpenAsync();
                foreach (DiscountProduct item in items)
                {
                    string query = $@"INSERT INTO [dbo].[DISCOUNT_PRODUCTS]
           ([Barcode]
           ,[DiscountPercent]
           ,[DiscountAmount]
           ,[DiscountTotal]
           ,[StartDate]
           ,[EndDate]
           ,[Status]
           ,[UserId])
     VALUES(@Barcode,@DiscountPercent,@DiscountAmount,@DiscountTotal,@StartDate,@EndDate,@Status,@UserId)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Barcode", item.Barcode);
                        cmd.Parameters.AddWithValue("@DiscountPercent", item.DiscountPercent);
                        cmd.Parameters.AddWithValue("@DiscountAmount", item.DiscountAmount);
                        cmd.Parameters.AddWithValue("@DiscountTotal", item.DiscountTotal);
                        cmd.Parameters.AddWithValue("@StartDate", item.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", (object)item.EndDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", item.Status);
                        cmd.Parameters.AddWithValue("@UserId", item.UserId);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        public async static Task UPDATE_DiscountProductAsync(DiscountProduct item)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                await con.OpenAsync();

                string query = $@"UPDATE [DISCOUNT_PRODUCTS]
SET 
    [DiscountPercent] = @DiscountPercent,
    [DiscountAmount] = @DiscountAmount,
    [DiscountTotal] = @DiscountTotal,
    [StartDate] = @StartDate,
    [EndDate] = @EndDate,
    [Status] = @Status,
    [UserId] = @UserId
WHERE 
    [Barcode] = @Barcode";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Barcode", item.Barcode);
                    cmd.Parameters.AddWithValue("@DiscountPercent", item.DiscountPercent);
                    cmd.Parameters.AddWithValue("@DiscountAmount", item.DiscountAmount);
                    cmd.Parameters.AddWithValue("@DiscountTotal", item.DiscountTotal);
                    cmd.Parameters.AddWithValue("@StartDate", item.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", (object)item.EndDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", item.Status);
                    cmd.Parameters.AddWithValue("@UserId", item.UserId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async static Task DELETE_DiscountProductAsync(List<DiscountProduct> items)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                await con.OpenAsync();
                foreach (DiscountProduct item in items)
                {
                    string query = $@"DELETE FROM DISCOUNT_PRODUCTS WHERE Barcode = @Barcode";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Barcode", item.Barcode);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        public async static Task DiscountProduct_UpdateStatusAsync(string barcode, bool status)
        {
            string query = "UPDATE DISCOUNT_PRODUCTS SET Status = @Status WHERE Barcode = @barcode";

            using (SqlConnection conn = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Barcode", barcode);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        #endregion [.. DISCOUNT PRODUCT ..]


        #region CREDIT

        public static string GET_CreditSaleProccessNo()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("EXEC dbo.KREDIT_SATISI_EMELIYYAT_NOMRE", connection))
            {
                connection.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        return dr[0].ToString();
                    return null;
                }
            }
        }

        public async static Task<int> Insert_CreditMain(CreditMain item)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT_KREDIT_SATISI_MAIN", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProcessNo", item.ProcessNo);
                cmd.Parameters.AddWithValue("@ContractNo", item.ContractNo);
                cmd.Parameters.AddWithValue("@OdenilenMebleg", item.OdenilenMebleg);
                cmd.Parameters.AddWithValue("@PaymentType", item.PaymentType);
                cmd.Parameters.AddWithValue("@Tarix", item.Date);
                cmd.Parameters.AddWithValue("@CustomerName", item.CustomerName);
                cmd.Parameters.AddWithValue("@CustomerId", item.CustomerId);
                cmd.Parameters.AddWithValue("@ZaminName", item.ZaminName);
                cmd.Parameters.AddWithValue("@ZaminId", item.ZaminId);
                cmd.Parameters.AddWithValue("@SupplierName", item.SupplierName);
                cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", item.ProductName);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@SalePrice", item.SalePrice);
                cmd.Parameters.AddWithValue("@DiscountPercent", item.DiscountPercent);
                cmd.Parameters.AddWithValue("@DiscountAmount", item.DiscountAmount);
                cmd.Parameters.AddWithValue("@Taksit", item.Taksit);
                cmd.Parameters.AddWithValue("@Total", item.Total);
                cmd.Parameters.AddWithValue("@IlkinOdenis", item.IlkinOdenis);
                cmd.Parameters.AddWithValue("@Comment", item.Comment);
                cmd.Parameters.AddWithValue("@MonthAmount", item.MonthAmount);
                cmd.Parameters.AddWithValue("@Cashier", item.Cashier);
                cmd.Parameters.AddWithValue("@UserId", item.UserId);
                cmd.Parameters.AddWithValue("@LonfFiskalId", item.LonfFiskalId ?? "");
                cmd.Parameters.AddWithValue("@ShortFiskalId", item.ShortFiskalId ?? "");
                cmd.Parameters.AddWithValue("@ReceiptNo", item.ReceiptNo ?? "");

                SqlParameter outputIdParam = new SqlParameter("@ReturnId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                int newId = (int)outputIdParam.Value;
                return newId;
            }
        }

        public async static Task Insert_CreditMonth(CreditSaleMonth item)
        {
            string query = $@"INSERT INTO [dbo].[KREDIT_SATISI_AYLIKODEME] 
([kredit_id], [taksitno], [DATEODEMEGUNU_], 
  [ODENILECEK_MEBLEG], longidsana) 
VALUES 
  (@CreditSaleId, @Month, @PaymentDay, 
    @Amount, @CreditSaleFiscalId)";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CreditSaleId", item.CreditSaleId);
                cmd.Parameters.AddWithValue("@Month", item.Month);
                cmd.Parameters.AddWithValue("@PaymentDay", item.PaymentDay);
                cmd.Parameters.AddWithValue("@Amount", item.Amount);
                cmd.Parameters.AddWithValue("@CreditSaleFiscalId", item.CreditSaleFiscalId);
                //cmd.Parameters.Add("@ReceiptNo", SqlDbType.NVarChar).Value = (object)item.ReceiptNo ?? DBNull.Value;
                //cmd.Parameters.AddWithValue("@PaymentTypeId", item.PaymentTypeId ?? null);


                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static void UPDATE_CreditPay(string shortId, string longId, string receiptNo, short paymentTypeId, int Id)
        {
            string query = $@"UPDATE [dbo].[KREDIT_SATISI_AYLIKODEME] SET [DATE2_]=GETDATE(),
[ODENILEN_MEBLEG]=[ODENILECEK_MEBLEG],
[longids]=N'{longId}',
[shortids]=N'{shortId}',
ReceiptNo = N'{receiptNo}',
PaymentTypeId = {paymentTypeId}
WHERE KREDIT_SATISI_AYLIK_ID= {Id}";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public async static Task Insert_CreditSaleRefund(CreditSaleRefund item)
        {
            dynamic query = @"INSERT INTO KREDIT_SATISI_MAIN_QAYTARMA 
VALUES 
  (
    @CreditSaleId, @RefundDate, @PaymentTypeId, 
    @TotalAmount, @Comment, @LongId, 
    @ReceiptNo, @UserId
  );
";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CreditSaleId", item.CreditSaleId);
                cmd.Parameters.AddWithValue("@RefundDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@PaymentTypeId", item.PaymentTypeId);
                cmd.Parameters.AddWithValue("@TotalAmount", item.TotalAmount);
                cmd.Parameters.AddWithValue("@Comment", item.Comment);
                cmd.Parameters.AddWithValue("@LongId", item.LongFiscalId);
                cmd.Parameters.AddWithValue("@ReceiptNo", item.ReceiptNo);
                cmd.Parameters.AddWithValue("@UserId", item.UserId);
                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async static Task Insert_CreditPayRefund(CreditPayRefund item)
        {
            dynamic query = @"INSERT INTO KREDIT_SATISI_AYLIQ_QAYTARMA 
VALUES (@CreditPayId, @RefundDate, @FiscalId, @ReceiptNo, @UserId);
";

            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CreditPayId", item.CreditPayId);
                cmd.Parameters.AddWithValue("@RefundDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@FiscalId", item.FiscalId);
                cmd.Parameters.AddWithValue("@ReceiptNo", item.ReceiptNo);
                cmd.Parameters.AddWithValue("@UserId", item.UserId);
                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        #endregion

        #endregion [...PROCEDURES METHODS...]
    }
}