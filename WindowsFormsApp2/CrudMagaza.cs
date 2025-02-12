using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using System.Data.SqlClient;


namespace WindowsFormsApp2
{

    public class CrudMagaza
    {
       // string connectionString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";

        string procedure = "INSERT_store";
        string DELETEPROC = "delete_store";
        string updateproc = "update_store";
        // Create ADO.NET objects.


        public int InsertMagaza(string storename,string adress,string telefon)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@STORE_NAME", SqlDbType.NVarChar, 500);
            param.Value = storename;
            param = cmd.Parameters.Add("@ADRESS", SqlDbType.NVarChar, 500);
            param.Value = adress;
            param = cmd.Parameters.Add("@TELEFON", SqlDbType.NVarChar, 500);
            param.Value = telefon;
            // Add the output parameter.
            param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32( param.Value);
        }
        public int updateMagaza(int id,string storename, string adress, string telefon)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(updateproc, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@STORE_NAME", SqlDbType.NVarChar, 500);
            param.Value = storename;
            param = cmd.Parameters.Add("@ADRESS", SqlDbType.NVarChar, 500);
            param.Value = adress;
            param = cmd.Parameters.Add("@TELEFON", SqlDbType.NVarChar, 500);
            param.Value = telefon;
            param = cmd.Parameters.Add("@STORE_ID", SqlDbType.Int);
            param.Value = id;
            // Add the output parameter.
            param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }
        public int deleteMagaza(int id)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(DELETEPROC, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;         
            param = cmd.Parameters.Add("@STORE_ID", SqlDbType.Int);
            param.Value = id;
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