using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
public     class CrudAnbar
    {

        //string connectionString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";

        string procedure = "INSERT_warehouse";
        string DELETEPROC = "delete_WAREHOUSE";
        string updateproc = "update_WAREHOUSE";
        string STORE_WAREHOUSE = "INSERT_STORE_WAREHOUSE";
        string DELETE_STORE_WAREHOUSE = "delete_store_warehouse";
        public int InsertAnbar(string WAREHOUSE_NAME, string ADDRESS, string TELEFON)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@WAREHOUSE_NAME", SqlDbType.NVarChar, 500);
            param.Value = WAREHOUSE_NAME;
            param = cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar, 500);
            param.Value = ADDRESS;
            param = cmd.Parameters.Add("@TELEFON", SqlDbType.NVarChar, 500);
            param.Value = TELEFON;
            // Add the output parameter.
            param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }

        public int updateAnbar(int id, string WAREHOUSE_NAME, string ADDRESS, string TELEFON)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(updateproc, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@WAREHOUSE_ID", SqlDbType.Int);
            param.Value = id;
            param = cmd.Parameters.Add("@WAREHOUSE_NAME", SqlDbType.NVarChar, 500);
            param.Value = WAREHOUSE_NAME;
            param = cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar, 500);
            param.Value = ADDRESS;
            param = cmd.Parameters.Add("@TELEFON", SqlDbType.NVarChar, 500);
            param.Value = TELEFON;
           
            // Add the output parameter.
            param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }

        public int deleteAnbar(int id)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(DELETEPROC, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@WAREHOUSE_ID", SqlDbType.Int);
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
        public int deletestorewarehouse(int id)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(DELETE_STORE_WAREHOUSE, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@WAREHOUSE_ID", SqlDbType.Int);
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
        public int CRSTORE_WAREHOUSE(int STORE_ID ,int WAREHOUSE_ID)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(STORE_WAREHOUSE, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@STORE_ID", SqlDbType.Int);
            param.Value = STORE_ID;
            param = cmd.Parameters.Add("@WAREHOUSE_ID", SqlDbType.Int);
            param.Value = WAREHOUSE_ID;
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
