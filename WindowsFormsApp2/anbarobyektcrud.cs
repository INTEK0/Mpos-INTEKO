using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{

    public    class anbarobyektcrud
    {
        //string connectionString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        string procedure = "INSERT_ANBAR_MAGAZA";
        string procedure1 = "UPDATE_ANBAR_MAGAZA";

        public int INSERT_ANBAR_MAGAZA(int ANBAR_ID ,int MAGAZA_ID ,DateTime TARIX ,string EMELIYYAT_NOMRE,int mal_details_id ,decimal migdar )
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@ANBAR_ID", SqlDbType.Int);
            param.Value = ANBAR_ID;
            param = cmd.Parameters.Add("@MAGAZA_ID", SqlDbType.Int);
            param.Value = MAGAZA_ID;
            param = cmd.Parameters.Add("@TARIX", SqlDbType.Date);
            param.Value = TARIX;
            param = cmd.Parameters.Add("@EMELIYYAT_NOMRE", SqlDbType.Int);
            param.Value = EMELIYYAT_NOMRE;
            param = cmd.Parameters.Add("@mal_details_id", SqlDbType.Int);
            param.Value = mal_details_id;
            param = cmd.Parameters.Add("@migdar", SqlDbType.Decimal);
            param.Value = migdar;
            // Add the output parameter.
            param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }

        public int MAGAZA_ANBAR_TRANFER(int ANBAR_ID, int MAGAZA_ID, DateTime TARIX, string EMELIYYAT_NOMRE, int mal_details_id, decimal migdar)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure1, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@ANBAR_ID", SqlDbType.Int);
            param.Value = ANBAR_ID;
            param = cmd.Parameters.Add("@MAGAZA_ID", SqlDbType.Int);
            param.Value = MAGAZA_ID;
            param = cmd.Parameters.Add("@TARIX", SqlDbType.Date);
            param.Value = TARIX;
            param = cmd.Parameters.Add("@EMELIYYAT_NOMRE", SqlDbType.Int);
            param.Value = EMELIYYAT_NOMRE;
            param = cmd.Parameters.Add("@mal_details_id", SqlDbType.Int);
            param.Value = mal_details_id;
            param = cmd.Parameters.Add("@migdar", SqlDbType.Decimal);
            param.Value = migdar;
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
