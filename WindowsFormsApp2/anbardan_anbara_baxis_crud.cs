using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
 public    class anbardan_anbara_baxis_crud
    {



        // string connectionString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        string procedure = "UPDATE_ANBAR_TRANSFER";
        string procedure2 = "anbardan_anbara_baxis_delete";

        public void crud_delete_TRANSFER(int ANBAR_TRANSFER_ID)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure2, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@ANBAR_TRANSFER_ID", SqlDbType.Int);
            param.Value = ANBAR_TRANSFER_ID;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
           
        }

        public int crud_update_TRANSFER(int ANBAR_TRANSFER_ID, decimal MIGDAR)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@ANBAR_TRANSFER_ID", SqlDbType.Int);
            param.Value = ANBAR_TRANSFER_ID;
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
