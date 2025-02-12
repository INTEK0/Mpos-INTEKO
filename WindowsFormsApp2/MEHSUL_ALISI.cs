using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
   public  class MEHSUL_ALISI
    {
        string procedure = "SELECT_KATEGORY";
        string count_mal = "count_mal";
        string mehsul_kodu_yaxla = "yoxlama_mehsul_kodu";

        public int mehsul_kod_yoxl(string mehsul_k,int techizatci_id)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(mehsul_kodu_yaxla, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@mehsul_kodu", SqlDbType.NVarChar, 500);
            param.Value = mehsul_k;
            param = cmd.Parameters.Add("@techizatci_id", SqlDbType.Int);
            param.Value = techizatci_id;
            // Add the output parameter.
            param = cmd.Parameters.Add("@empcount", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }
        public int count_mal_(string emeliyyat)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(count_mal, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar, 500);
            param.Value = emeliyyat;

            // Add the output parameter.
            param = cmd.Parameters.Add("@empcount", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }
        public int InsertKATEGORY(string KATEGORIYA)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@KATEGORY", SqlDbType.NVarChar, 500);
            param.Value = KATEGORIYA;
          
            // Add the output parameter.
            param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }

        public int COUNTKATEGORY(string KATEGORIYA)
        {
            return 0;
            //// Create ADO.NET objects.
            //SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            //SqlCommand cmd = new SqlCommand(COUTprocedure, con);
            //// Configure command and add input parameters.
            //cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter param;
            //param = cmd.Parameters.Add("@KATEGORY", SqlDbType.NVarChar, 500);
            //param.Value = KATEGORIYA;

            //// Add the output parameter.
            //param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            //param.Direction = ParameterDirection.Output;
            //// Execute the command.
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
            //return Convert.ToInt32(param.Value);
        }

    }
}
