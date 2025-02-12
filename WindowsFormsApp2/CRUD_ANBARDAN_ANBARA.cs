using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
   public  class CRUD_ANBARDAN_ANBARA
    {
       // string connectionString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        string procedure = "INSERT_ANBAR_TRANSFER";

        public int crud_ANBAR_TRANSFER(int MAL_ALISI_DETAILS_ID,int MAIN , int DETAILS, DateTime TARIX, string  EMELIYYAT_NOMRE,Decimal MIGDAR)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@MAL_ALISI_DETAILS_ID", SqlDbType.Int);
            param.Value = MAL_ALISI_DETAILS_ID;
            param = cmd.Parameters.Add("@MAIN", SqlDbType.Int);
            param.Value = MAIN;
            param = cmd.Parameters.Add("@DETAILS", SqlDbType.Int);
            param.Value = DETAILS;
            param = cmd.Parameters.Add("@TARIX", SqlDbType.Date);
            param.Value = TARIX;
            param = cmd.Parameters.Add("@EMELIYYAT_NOMRE", SqlDbType.NVarChar,20);
            param.Value = EMELIYYAT_NOMRE;
            param = cmd.Parameters.Add("@MIGDAR", SqlDbType.Decimal);
            param.Value = MIGDAR;
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
