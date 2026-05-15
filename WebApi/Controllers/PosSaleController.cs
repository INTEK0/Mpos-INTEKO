using System;
using System.Data.SqlClient;
using System.Web.Http;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Sale")]
    public class PosSaleController : ApiController
    {
        private readonly string DbConnection = "Data Source=localhost;Initial Catalog=Neroli;Persist Security Info=True;Integrated Security=true;";
        string _customerName, _customerSurname, _customerFatherName;


        [HttpPost, Route("")]
        public IHttpActionResult CreateSale([FromBody] SalesDto data)
        {
            /*
             * => Kasssir seç və Id nömrəsini al
             *
             * => Müştərini seç və Id nömrəsini al. Yoxdursa yarat və yenidən seçili hala gətir
             *
             * => Məhsulun adını sistemdən kontrol et və Id nömrəsini al
             *
             * => Item cədvəlinə məhsulu yazdır
             *
             * => Gridi refresh elətdir
             */

            try
            {
                var cashierId = SelectedCashier(data);
                var customerId = SelectedCustomer(data, cashierId);


                return Ok(new {cashierId = cashierId, customerId = customerId, message ="Successfull Operation" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        private int SelectedCashier(SalesDto data)
        {
            int cashierId = default;

            string selectQuery = @"SELECT * FROM userParol WHERE Ulogin = @CashierName";
            using (var conn = new SqlConnection(DbConnection))
            using (var cmd = new SqlCommand(selectQuery, conn))
                try
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@CashierName", data.CashierName.Trim());

                    var result = cmd.ExecuteScalar();

                    if (result != null)
                        cashierId = Convert.ToInt32(result);

                    return cashierId;
                }
                catch (Exception)
                {
                    //0 gələrsə error versin APİ tərəfə mesaj göndər - Null references əlavə etmək olar
                    return 0;
                }
        }

        private int SelectedCustomer(SalesDto data, int cashierId)
        {
            using (var conn = new SqlConnection(DbConnection))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int customerId;
                        string proccessNo;

                        // 1. Müştərini yoxla
                        string selectQuery = @"SELECT * FROM MUSTERILER WHERE CompanyName = @CustomerFullName";

                        using (var cmd = new SqlCommand(selectQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@CustomerFullName", data.CustomerName.Trim());

                            var result = cmd.ExecuteScalar();

                            if (result != null)
                                customerId = Convert.ToInt32(result);
                            else
                            {
                                // Müştəri yoxdur > yarat


                                #region [..ƏMƏLİYYAT NÖMRƏSİ..]

                                const string query = "EXEC dbo.MUSTERI_EMELIYYAT_NOMRE";
                                using (SqlConnection connection = new SqlConnection(DbConnection))
                                using (SqlCommand proccessNocmd = new SqlCommand(query, connection))
                                {
                                    connection.Open();
                                    var resultProccessNo = proccessNocmd.ExecuteScalar();
                                    proccessNo = resultProccessNo.ToString();
                                }

                                #endregion [..ƏMƏLİYYAT NÖMRƏSİ..]


                                #region [..INSERT CUSTOMER..]

                                string fullName = data.CustomerName.Trim();
                                string[] nameParts = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                if (nameParts.Length >= 2)
                                {
                                    _customerName = nameParts[0];
                                    _customerSurname = nameParts[1];
                                    _customerFatherName = nameParts.Length > 2 ? nameParts[2] : string.Empty;
                                }

                                string insertCustomer = @"INSERT INTO MUSTERILER (CompanyName,AD,SOYAD,ATAADI,MOBIL,m_no_char,TARIX)
                        OUTPUT INSERTED.MUSTERILER_ID
                        VALUES (@CustomerFullName,@CustomerName,@CustomerSurname,@CustomerFatherName,@Phone,@ProccessNo,@CreateDate)";


                                using (var insertCmd = new SqlCommand(insertCustomer, conn, transaction))
                                {
                                    insertCmd.Parameters.AddWithValue("@CustomerFullName", data.CustomerName.Trim());
                                    insertCmd.Parameters.AddWithValue("@CustomerName", _customerName);
                                    insertCmd.Parameters.AddWithValue("@CustomerSurname", _customerSurname);
                                    insertCmd.Parameters.AddWithValue("@CustomerFatherName", _customerFatherName);
                                    insertCmd.Parameters.AddWithValue("@Phone", data.CustomerPhone.Trim());
                                    insertCmd.Parameters.AddWithValue("@ProccessNo", proccessNo);
                                    insertCmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);

                                    customerId = (int)insertCmd.ExecuteScalar();
                                }

                                #endregion [..INSERT CUSTOMER..]
                            }
                        }


                        // 2. SelectedCustomers-ə əlavə et

                        #region [..SELECTED CUSTOMER INSERT DB..]

                        string insertSelected = @"DELETE FROM SelectedCustomers WHERE CreatedUserId = @UserId

INSERT INTO SelectedCustomers (CustomerId,CreatedUserId) VALUES (@CustomerId,@UserId)";

                        using (var cmd = new SqlCommand(insertSelected, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@CustomerId", customerId);
                            cmd.Parameters.AddWithValue("@UserId", cashierId);
                            cmd.ExecuteNonQuery();
                        }

                        #endregion [..SELECTED CUSTOMER INSERT DB..]

                        transaction.Commit();

                        return customerId;
                    }
                    catch (Exception ex)
                    {
                        //0 gələrsə error versin APİ tərəfə mesaj göndər - Null references əlavə etmək olar
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }

        private int SelectedProduct()
        {
            return 0;
        }
    }
}