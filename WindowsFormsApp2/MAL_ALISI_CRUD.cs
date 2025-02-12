using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
   public class MAL_ALISI_CRUD
    {
        //1723; 838

       // string connectionString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";

        string procedure = "INSERT_MAL_ALISI_MAIN";

        string procedure_detail = "INSERT_MAL_ALISI_DETAILS";

        string delete_mal_details = "delete_mal_alisi_details";

        string update_mal_details = "update_MAL_ALISI_DETAILS";
        string delete_mal_all = "DEL_MAL_ALISI_DETAILS";
        public int deletetmal_ALL(string  mal_detail)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(delete_mal_all, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@EMELIYYAT_NOMRE", SqlDbType.NVarChar,50);
            param.Value = mal_detail;
            // Add the output parameter.
            param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);

        }
        public int deletetmal(int mal_detail)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(delete_mal_details, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@mal_alisi_details_id", SqlDbType.Int);
            param.Value = mal_detail;
            // Add the output parameter.
            param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);

        }

        public int Insertmal(string FAKTURA_NOMRE ,string TECHIZATCI ,DateTime TARIX ,
            string  ODEME_TIPI,string EMELIYYAT_NOMRE ,int _US_ID)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@FAKTURA_NOMRE", SqlDbType.NVarChar, 500);
            param.Value = FAKTURA_NOMRE;
            param = cmd.Parameters.Add("@TECHIZATCI", SqlDbType.NVarChar, 500);
            param.Value = TECHIZATCI;
            param = cmd.Parameters.Add("@TARIX", SqlDbType.Date);
            param.Value = TARIX;
            param = cmd.Parameters.Add("@ODEME_TIPI", SqlDbType.NVarChar, 500);
            param.Value = ODEME_TIPI;

            param = cmd.Parameters.Add("@EMELIYYAT_NOMRE", SqlDbType.NVarChar, 100);
            param.Value = EMELIYYAT_NOMRE;
           
            param = cmd.Parameters.Add("@USER_ID_", SqlDbType.Int);
            param.Value = _US_ID;
            
            // Add the output parameter.
            param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }
        public int updatedetails(int MAL_ALISI_details_ID, string KATEGORIYA, string BARKOD, string MEHSUL_ADI, string MEHSUL_KODU,
string ANBAR, string MIGDARI, string VAHID, string VALYUTA, string VERGI_DERECESI, string ALIS_GIYMETI, string SATIS_GIYMETI,
string ENDIRIM_FAIZ, string ENDIRIM_AZN, string ENDIRIM_MEBLEGI, string YEKUN_MEBLEG, string ISTEHSAL_TARIXI,
string BITIS_TARIXI, string XEBERDAR_ET)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(update_mal_details, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@MAL_ALISI_details_ID", SqlDbType.Int);
            param.Value = MAL_ALISI_details_ID;
            param = cmd.Parameters.Add("@KATEGORIYA", SqlDbType.NVarChar, 500);
            param.Value = KATEGORIYA;
            param = cmd.Parameters.Add("@BARKOD", SqlDbType.NVarChar, 500);
            param.Value = BARKOD;
            param = cmd.Parameters.Add("@MEHSUL_ADI", SqlDbType.NVarChar, 500);
            param.Value = MEHSUL_ADI;
            param = cmd.Parameters.Add("@MEHSUL_KODU", SqlDbType.NVarChar, 500);
            param.Value = MEHSUL_KODU;
            param = cmd.Parameters.Add("@ANBAR", SqlDbType.NVarChar, 500);
            param.Value = ANBAR;
            param = cmd.Parameters.Add("@MIGDARI", SqlDbType.NVarChar, 500);
            param.Value = MIGDARI;
            param = cmd.Parameters.Add("@VAHID", SqlDbType.NVarChar, 500);
            param.Value = VAHID;
            param = cmd.Parameters.Add("@VALYUTA", SqlDbType.NVarChar, 500);
            param.Value = VALYUTA;
            param = cmd.Parameters.Add("@VERGI_DERECESI", SqlDbType.NVarChar, 500);
            param.Value = VERGI_DERECESI;
            param = cmd.Parameters.Add("@ALIS_GIYMETI", SqlDbType.NVarChar, 500);
            param.Value = ALIS_GIYMETI;
            param = cmd.Parameters.Add("@SATIS_GIYMETI", SqlDbType.NVarChar, 500);
            param.Value = SATIS_GIYMETI;
            param = cmd.Parameters.Add("@ENDIRIM_FAIZ", SqlDbType.NVarChar, 500);
            param.Value = ENDIRIM_FAIZ;
            param = cmd.Parameters.Add("@ENDIRIM_AZN", SqlDbType.NVarChar, 500);
            param.Value = ENDIRIM_AZN;
            param = cmd.Parameters.Add("@ENDIRIM_MEBLEGI", SqlDbType.NVarChar, 500);
            param.Value = ENDIRIM_MEBLEGI;
            param = cmd.Parameters.Add("@YEKUN_MEBLEG", SqlDbType.NVarChar, 500);
            param.Value = YEKUN_MEBLEG;
            param = cmd.Parameters.Add("@ISTEHSAL_TARIXI", SqlDbType.NVarChar, 20);
            param.Value = ISTEHSAL_TARIXI;
            param = cmd.Parameters.Add("@BITIS_TARIXI", SqlDbType.NVarChar, 20);
            param.Value = BITIS_TARIXI;
            param = cmd.Parameters.Add("@XEBERDAR_ET", SqlDbType.NVarChar, 500);
            param.Value = XEBERDAR_ET;




            // Add the output parameter.
            param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }

        public int Insertdetails(int MAL_ALISI_MAIN_ID ,string KATEGORIYA ,string BARKOD ,string MEHSUL_ADI ,string MEHSUL_KODU,
	string ANBAR,string MIGDARI ,string VAHID ,string VALYUTA ,string VERGI_DERECESI ,string ALIS_GIYMETI ,string SATIS_GIYMETI,
	string ENDIRIM_FAIZ ,string ENDIRIM_AZN ,string ENDIRIM_MEBLEGI ,string YEKUN_MEBLEG ,string ISTEHSAL_TARIXI ,
    string  BITIS_TARIXI ,string XEBERDAR_ET )
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure_detail, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@MAL_ALISI_MAIN_ID", SqlDbType.Int);
            param.Value = MAL_ALISI_MAIN_ID;
            param = cmd.Parameters.Add("@KATEGORIYA", SqlDbType.NVarChar, 500);
            param.Value = KATEGORIYA;
            param = cmd.Parameters.Add("@BARKOD", SqlDbType.NVarChar, 500);
            param.Value = BARKOD;
            param = cmd.Parameters.Add("@MEHSUL_ADI", SqlDbType.NVarChar, 500);
            param.Value = MEHSUL_ADI;
            param = cmd.Parameters.Add("@MEHSUL_KODU", SqlDbType.NVarChar, 500);
            param.Value = MEHSUL_KODU;
            param = cmd.Parameters.Add("@ANBAR", SqlDbType.NVarChar, 500);
            param.Value = ANBAR;
            param = cmd.Parameters.Add("@MIGDARI", SqlDbType.NVarChar, 500);
            param.Value = MIGDARI;
            param = cmd.Parameters.Add("@VAHID", SqlDbType.NVarChar, 500);
            param.Value = VAHID;
            param = cmd.Parameters.Add("@VALYUTA", SqlDbType.NVarChar, 500);
            param.Value = VALYUTA;
            param = cmd.Parameters.Add("@VERGI_DERECESI", SqlDbType.NVarChar, 500);
            param.Value = VERGI_DERECESI;
            param = cmd.Parameters.Add("@ALIS_GIYMETI", SqlDbType.NVarChar, 500);
            param.Value = ALIS_GIYMETI;
            param = cmd.Parameters.Add("@SATIS_GIYMETI", SqlDbType.NVarChar, 500);
            param.Value = SATIS_GIYMETI;
            param = cmd.Parameters.Add("@ENDIRIM_FAIZ", SqlDbType.NVarChar, 500);
            param.Value = ENDIRIM_FAIZ;
            param = cmd.Parameters.Add("@ENDIRIM_AZN", SqlDbType.NVarChar, 500);
            param.Value = ENDIRIM_AZN;
            param = cmd.Parameters.Add("@ENDIRIM_MEBLEGI", SqlDbType.NVarChar, 500);
            param.Value = ENDIRIM_MEBLEGI;
            param = cmd.Parameters.Add("@YEKUN_MEBLEG", SqlDbType.NVarChar, 500);
            param.Value = YEKUN_MEBLEG;
            param = cmd.Parameters.Add("@ISTEHSAL_TARIXI", SqlDbType.NVarChar,20);
            param.Value = ISTEHSAL_TARIXI;
            param = cmd.Parameters.Add("@BITIS_TARIXI", SqlDbType.NVarChar,20);
            param.Value = BITIS_TARIXI;
            param = cmd.Parameters.Add("@XEBERDAR_ET", SqlDbType.NVarChar, 500);
            param.Value = XEBERDAR_ET;
            param = cmd.Parameters.Add("@ShowPosScreen", SqlDbType.Bit);
            param.Value = false;



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
