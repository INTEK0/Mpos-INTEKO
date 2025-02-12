using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
  public   class MEHSUL_GAYTARMA_AXTARIS_CRUD
    {
       // string connectionString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        string procedure = "update_MAL_GAYTARMA_DETAILS";

        public int updateMehsulgaytarma( int MAL_GEYTARMA_DETAILS,decimal MIGDARI )
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@MAL_GEYTARMA_DETAILS", SqlDbType.Int);
            param.Value = @MAL_GEYTARMA_DETAILS;
            param = cmd.Parameters.Add("@MIGDARI", SqlDbType.Decimal);
            param.Value = @MIGDARI;
                       
            // Add the output parameter.
            param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }
    }
}
