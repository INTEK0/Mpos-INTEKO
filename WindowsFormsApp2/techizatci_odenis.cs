using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2
{
 public    class techizatci_odenis
    {
       // string connectionString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        string procedure = "INSERT_TECHIZATCI_ODENIS";
        string proc_mus_odenis = "musteri_ODENIS_";
        string proce1 = "techizatci_odenis_emeliyyat_nomre";
        string proce_MUS = "MUSTERI_odenis_emeliyyat_nomre";
        string delete_odenis = "DELETE_TECHIZATCI_ODENISI";
        string update_techizatc = "update_techizatci";
        string UPDATE_KATEGORIYA = "kategory_update";
        string delete_excell_data = "dele_excell_import_data";
        string import_date = "bulk_insert";

        public void bulk_import_exc()
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(import_date, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter param;

            //param = cmd.Parameters.Add("@TECHIZATCI_ODENISI_ID", SqlDbType.Int);
            //param.Value = TECHIZATCI_ODENIS_ID;


            // Add the output parameter.
            //param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
            //param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //return Convert.ToInt32(param.Value);
        }
        public void DELETE_import_exc()
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(delete_excell_data, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter param;

            //param = cmd.Parameters.Add("@TECHIZATCI_ODENISI_ID", SqlDbType.Int);
            //param.Value = TECHIZATCI_ODENIS_ID;


            // Add the output parameter.
            //param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
            //param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //return Convert.ToInt32(param.Value);
        }


        public int update_kategoriya(int kategoriya_id, string kategoriya)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(UPDATE_KATEGORIYA, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;

            param = cmd.Parameters.Add("@KATEGORIYA_ID", SqlDbType.Int);
            param.Value = kategoriya_id;

            param = cmd.Parameters.Add("@KATEGORIYA", SqlDbType.NVarChar, 200);
            param.Value = kategoriya;

         
            // Add the output parameter.
            param = cmd.Parameters.Add("@EMCPOUNT", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }

        public int update_techizatci(int techizatci_id,string unvan,string mugavile_nom,string tel1,string tel2)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(update_techizatc, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;

            param = cmd.Parameters.Add("@TECHIZATCI_ID", SqlDbType.Int);
            param.Value = techizatci_id;

            param = cmd.Parameters.Add("@UNVAN", SqlDbType.NVarChar,500);
            param.Value = unvan;

            param = cmd.Parameters.Add("@MUVAGILE_NOM", SqlDbType.NVarChar, 500);
            param.Value = mugavile_nom;
            param = cmd.Parameters.Add("@TELEFON1", SqlDbType.NVarChar, 500);
            param.Value = tel1;
            param = cmd.Parameters.Add("@TELEFON2", SqlDbType.NVarChar, 500);
            param.Value = tel2;
            // Add the output parameter.
            param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }
        public int DELETE_ODENIS(int TECHIZATCI_ODENIS_ID)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(delete_odenis, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;

            param = cmd.Parameters.Add("@TECHIZATCI_ODENISI_ID", SqlDbType.Int);
            param.Value = TECHIZATCI_ODENIS_ID;
          

            // Add the output parameter.
            param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }


        
            public string musteri_emeliyyat_nomre()
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(proce_MUS, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;


            // Add the output parameter.
            param = cmd.Parameters.Add("@r", SqlDbType.NVarChar, 100);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToString(param.Value);
        }
        public string emeliyyat_nomre()
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(proce1, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;

          
            // Add the output parameter.
            param = cmd.Parameters.Add("@r", SqlDbType.NVarChar,100);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToString(param.Value);
        }

        
              public int INSERT_musteri_ODENIS(int _gaime_satis_main_id ,
string _odenis_tipi,
string _faktura_nom ,DateTime  date_ , int user_id ,Decimal  esas_borc_odenis,Decimal  edv_borc ,string _emeliyyet_nomre)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(proc_mus_odenis, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;

            param = cmd.Parameters.Add("@gaime_satis_main_id", SqlDbType.Int);
            param.Value = _gaime_satis_main_id;
            param = cmd.Parameters.Add("@odenis_tipi", SqlDbType.NVarChar,50);
            param.Value = _odenis_tipi;
            param = cmd.Parameters.Add("@faktura_nom", SqlDbType.NVarChar,50);
            param.Value = _faktura_nom;

            param = cmd.Parameters.Add("@date", SqlDbType.Date);
            param.Value = date_;
            param = cmd.Parameters.Add("@user_id", SqlDbType.Int);
            param.Value = user_id ;
            param = cmd.Parameters.Add("@esas_borc_odenis", SqlDbType.Decimal);
            param.Value = esas_borc_odenis;
            param = cmd.Parameters.Add("@edv_borc", SqlDbType.Decimal);
            param.Value = edv_borc;

            param = cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar,100);
            param.Value = _emeliyyet_nomre;

            // Add the output parameter.
            param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }

        public int INSERT_ODENIS(int MAL_ALISI_MAIN_ID , decimal ODENIS,string  ODENIS_TIPI ,
            string gaime_n,string geyd,DateTime tarix,string emeliyyat_nomre,string faktura_nom ,int _user_id ,decimal _ESAS_BORC_ODENIS,
            decimal _EDV_BORC)
        {
            SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString);
            SqlCommand cmd = new SqlCommand(procedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
         
            param = cmd.Parameters.Add("@MAL_ALISI_MAIN_ID", SqlDbType.Int);
            param.Value = MAL_ALISI_MAIN_ID;
            param = cmd.Parameters.Add("@ODENIS", SqlDbType.Decimal);
            param.Value = ODENIS;
            param = cmd.Parameters.Add("@ODENIS_TIPI", SqlDbType.NVarChar);
            param.Value = ODENIS_TIPI;

            param = cmd.Parameters.Add("@GAIME_N", SqlDbType.NVarChar);
            param.Value = gaime_n;
            param = cmd.Parameters.Add("@GEYD", SqlDbType.NVarChar);
            param.Value = geyd;
            param = cmd.Parameters.Add("@TARIX", SqlDbType.Date);
            param.Value = tarix;
            param = cmd.Parameters.Add("@EMELIYYAT_NOMRE", SqlDbType.NVarChar,250);
            param.Value = emeliyyat_nomre;

            param = cmd.Parameters.Add("@FAKTURA_NOMRE", SqlDbType.NVarChar, 50);
            param.Value = faktura_nom;

            param = cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
            param.Value = _user_id;

            param = cmd.Parameters.Add("@ESAS_BORC_ODENIS", SqlDbType.Decimal);
            param.Value = _ESAS_BORC_ODENIS;

            param = cmd.Parameters.Add("@EDV_BORC", SqlDbType.Decimal);
            param.Value = _EDV_BORC;

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
