using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomerController : ApiController
    {
        [HttpGet, Route("")]
        public IHttpActionResult GetAll()
        {
            var list = new List<Customer>();
            using (var conn = new SqlConnection("Data Source=localhost;Initial Catalog=Neroli;Persist Security Info=True;Integrated Security=true;"))
            using (var cmd = new SqlCommand("SELECT * FROM dbo.fn_MUSTERI()", conn))
            {
                conn.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                    list.Add(new Customer
                    {
                        CustomerID = (int)dr["MUSTERILER_ID"],
                        Name = dr["AD SOYAD ATA ADI"].ToString()
                    });
            }
            return Ok(list);
        }

        [HttpPost]
        [Route("api/customer")]
        public IHttpActionResult Insert()
        {
            return null;
        }
    }
}
