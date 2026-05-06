using System.Data.SqlClient;
using System.Web.Http;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Sale")]
    public class PosSaleController : ApiController
    {
        [HttpPost, Route("")]
        public IHttpActionResult CreateSale([FromBody] SalesDto data)
        {
            string query = "";

            using (var conn = new SqlConnection("Data Source=localhost;Initial Catalog=Neroli;Persist Security Info=True;Integrated Security=true;"))
            using (var cmd = new SqlCommand("SELECT * FROM dbo.fn_MUSTERI()", conn))
            {
               
            }
            return Ok(new { message = "Data qəbul olundu" });
        }
    }
}
