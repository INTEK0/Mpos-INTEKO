using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2
{
    public class satis_json
    {
        string update_tr_calculation = "update_calc_tr";
        string del_migdar = "delete_calaculation_";
        string del_migdarnewsa = "delete_calaculation_newsa";
        string update_satis_giymeti = "update_giymet_calaculation_";
        string insert_pos_guzest = "pos_guzest_insert";
        string delete_grid_pos = "delete_grid_pos";
        string asmart_tekrar = "azsmart_tekrar_";
        readonly string azsmart_rollback = "azsmart_rollback";

        public string AzsmartRollback(int _main_id)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(azsmart_rollback, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@main_id", SqlDbType.Int);
            param.Value = _main_id;
            param = cmd.Parameters.Add("@json_", SqlDbType.NVarChar, int.MaxValue);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return param.Value.ToString();
        }

        public void del_grid_data(int mal_id_, string emeliyyat_nomr_)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString);
            SqlCommand cmd = new SqlCommand(delete_grid_pos, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@mal_details_id", SqlDbType.Int);
            param.Value = mal_id_;
            param = cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar, 100);
            param.Value = emeliyyat_nomr_;
            param = cmd.Parameters.Add("@userId", SqlDbType.NVarChar, 100);
            param.Value = Properties.Settings.Default.UserID;


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void pos_guzest_insert_(string emeliyyat_nomre_, int mal_details_id_, string endirim_faiz_, string endirim_azn_)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(insert_pos_guzest, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar, 100);
            param.Value = emeliyyat_nomre_;

            param = cmd.Parameters.Add("@mal_details_id", SqlDbType.Int);
            param.Value = mal_details_id_;

            param = cmd.Parameters.Add("@endirim_faiz", SqlDbType.NVarChar, 100);
            param.Value = endirim_faiz_;

            param = cmd.Parameters.Add("@endirim_azn", SqlDbType.NVarChar, 100);
            param.Value = endirim_azn_;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void update_satis_giymeti_(int mal_details_id_, decimal giymet_)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(update_satis_giymeti, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@mal_details_id", SqlDbType.Int);
            param.Value = mal_details_id_;

            param = cmd.Parameters.Add("@giymet", SqlDbType.Decimal);
            param.Value = giymet_;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void del_migdar_calculation(string mal_id_, string say_, string emeliyyat_nomr_)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(del_migdar, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@mal_details_id", SqlDbType.NVarChar, 100);
            param.Value = mal_id_;

            param = cmd.Parameters.Add("@say", SqlDbType.NVarChar, 100);
            param.Value = say_;

            param = cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar, 100);
            param.Value = emeliyyat_nomr_;

            param = cmd.Parameters.Add("@userID", SqlDbType.Int);
            param.Value = Properties.Settings.Default.UserID;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void del_migdarnewsa_calculation(string mal_id_, string say_, string emeliyyat_nomr_)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(del_migdarnewsa, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@mal_details_id", SqlDbType.NVarChar, 100);
            param.Value = mal_id_;

            param = cmd.Parameters.Add("@say", SqlDbType.NVarChar, 100);
            param.Value = say_;

            param = cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar, 100);
            param.Value = emeliyyat_nomr_;

            param = cmd.Parameters.Add("@userID", SqlDbType.Int);
            param.Value = Properties.Settings.Default.UserID;


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void update_calculation_tr()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(update_tr_calculation, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.Add("@userID", SqlDbType.Int);
                    param.Value = Properties.Settings.Default.UserID;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
