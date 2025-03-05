using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2
{
    public  class KASSA_IP_CRUD
    {
        string procedure = "KASSA_IP_INSERT";
        string procedure_DELETE = "KASSA_IP_delete";

        string procedure22 = "TERAZI_IP_INSERT";
        string procedure_DELETE22 = "TERAZI_IP_delete";

        public int DELETE_IP(int _KASSA_IP_ID)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure_DELETE, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@KASSA_IP_ID", SqlDbType.Int);
            param.Value = _KASSA_IP_ID;
           
            // Add the output parameter.
            param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }
        public int Insert_IP(int KASSA_FIRMA_IP_, string  IP_ADRESS_, int KASSIR_ID_,string merchant_id_)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@KASSA_FIRMA_IP", SqlDbType.Int);
            param.Value = KASSA_FIRMA_IP_;
            param = cmd.Parameters.Add("@IP_ADRESS", SqlDbType.NVarChar,100);
            param.Value = IP_ADRESS_;
            param = cmd.Parameters.Add("@KASSIR_ID", SqlDbType.Int);
            param.Value = KASSIR_ID_;
            param = cmd.Parameters.Add("@merchant_id", SqlDbType.NVarChar,1000);
            param.Value = merchant_id_;

            // Add the output parameter.
            param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }

        public void DELETE_IPT(int TERAZI_FIRMA_IP)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(procedure_DELETE22,con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param;
                    param = cmd.Parameters.Add("@TERAZI_IP_ID", SqlDbType.Int);
                    param.Value = TERAZI_FIRMA_IP;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int Insert_IPT(int TERAZI_FIRMA_IP, string IP_ADRESS_)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure22, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@TERAZI_FIRMA_IP", SqlDbType.Int);
            param.Value = TERAZI_FIRMA_IP;
            param = cmd.Parameters.Add("@IP_ADRESS", SqlDbType.NVarChar, 100);
            param.Value = IP_ADRESS_;
           

            // Add the output parameter.
            param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }
    }

  
}
