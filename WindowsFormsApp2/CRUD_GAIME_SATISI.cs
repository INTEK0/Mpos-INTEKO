using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.Enums;

namespace WindowsFormsApp2
{
    public class CRUD_GAIME_SATISI
    {
        //string connectionString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        string procedure = "INSERT_GAIME_SATISI_MAIN";
        string procedure2 = "INSERT_GAIME_SATIS_DAXIL";
        string procedure3 = "GAIME_SATISI_MAIN_delete";
        string procedure4 = "GAIME_SATIS_MAIN_DETAILS_UPDATE";
        string procedure5 = "insert_gaime_satisi_gaytarma_details";
        string procedure6 = "insert_gaime_satisi_gaytarma_main";
        string DELETE_GAIME = "GAIME_SATISI_DETAILS_DELETE";
        string update_gaime_satisi = "upda_gaime_satisi_details";

        string update_check_status = "update_gaime_satis";

        string insert_gaime_satis_gaytarma_proc = "insert_gaime_satis_gaytarma";
        string proceduretest = "test_musteriA_di";

        public int test_proc_(string EMELIYYAT_NOMRE_, string musteri_)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(proceduretest, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@EMELIYYAT_NOMRE", SqlDbType.NVarChar, 100);
            param.Value = EMELIYYAT_NOMRE_;
            param = cmd.Parameters.Add("@musteri", SqlDbType.NVarChar, 500);
            param.Value = musteri_;


            // Add the output parameter.
            param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }

        public int insert_gaime_satis_gaytarma_proc_(string gaime_satis_details_id, string migdar,
            string emeliyyat_nomre, DateTime tarix_, string geyd_, int _user_id)
        {
            //SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            //SqlCommand cmd = new SqlCommand(insert_gaime_satis_gaytarma_proc, con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter param;
            //param = cmd.Parameters.Add("@gaime_satis_details_id", SqlDbType.NVarChar, 20);
            //param.Value = gaime_satis_details_id;
            //param = cmd.Parameters.Add("@migdar", SqlDbType.NVarChar, 20);
            //param.Value = migdar;

            //param = cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar, 100);
            //param.Value = emeliyyat_nomre;

            //param = cmd.Parameters.Add("@tarix_", SqlDbType.DateTime);
            //param.Value = tarix_;
            //param = cmd.Parameters.Add("@geyd", SqlDbType.NVarChar, 500);
            //param.Value = geyd_;

            //param = cmd.Parameters.Add("@user_id", SqlDbType.Int);
            //param.Value = _user_id;

            //// Add the output parameter.
            //param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
            //param.Direction = ParameterDirection.Output;
            //// Execute the command.
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
            //return Convert.ToInt32(param.Value);


            //-------------------------------------------------------

            int empCount = 0;
            int returnMainId = 0;

            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(insert_gaime_satis_gaytarma_proc, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add("@gaime_satis_details_id", SqlDbType.NVarChar, 100).Value = gaime_satis_details_id;
                    cmd.Parameters.Add("@migdar", SqlDbType.NVarChar, 100).Value = migdar;
                    cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar, 100).Value = emeliyyat_nomre;
                    cmd.Parameters.Add("@tarix_", SqlDbType.NVarChar, 100).Value = tarix_;
                    cmd.Parameters.Add("@geyd", SqlDbType.NVarChar, 100).Value = geyd_;
                    cmd.Parameters.Add("@user_id", SqlDbType.NVarChar, 100).Value = _user_id;



                    SqlParameter empCountParam = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
                    empCountParam.Direction = ParameterDirection.Output;

                    SqlParameter returnMainIdParam = cmd.Parameters.Add("@returnMainId", SqlDbType.Int);
                    returnMainIdParam.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    empCount = Convert.ToInt32(empCountParam.Value);
                    returnMainId = Convert.ToInt32(returnMainIdParam.Value);
                }
            }

            FormHelpers.OperationLog(OperationType.RefundQaimeSales, returnMainId);

            return empCount;
        }
        public int GAIME_SATISI_check_status(string emeliyyat_nomre_)
        {
            int empCount = 0;
            int returnMainId = 0;

            using (SqlConnection con = new SqlConnection(DbHelpers.DbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("update_gaime_satis", con)) 
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@emeliyyat_nomre", SqlDbType.NVarChar, 100).Value = emeliyyat_nomre_;

                    SqlParameter empCountParam = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
                    empCountParam.Direction = ParameterDirection.Output;

                    SqlParameter returnMainIdParam = cmd.Parameters.Add("@returnMainId", SqlDbType.Int);
                    returnMainIdParam.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    empCount = Convert.ToInt32(empCountParam.Value);
                    returnMainId = Convert.ToInt32(returnMainIdParam.Value);
                }
            }

            FormHelpers.OperationLog(OperationType.QaimeSales, returnMainId);

            return empCount;
        }

        public int GAIME_SATISI_udapte_(int GAIME_SATISI_DETAILS_ID, string GAIME_NOM,
            decimal ODENILEN_MEBLEG,
            int MAL_DETAILS_ID, int MAGAZA_,
                string MIGDARI, string SATIS_GIYMETI, string ENDIRIM_FAIZ, string ENDIRIM_AZN, string ENDIRIM_MEBLEGI,
                   string YEKUN_MEBLEG, string GEYD)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(update_gaime_satisi, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@GAIME_SATISI_DETAILS_ID", SqlDbType.Int);
            param.Value = GAIME_SATISI_DETAILS_ID;
            param = cmd.Parameters.Add("@GAIME_NOM", SqlDbType.NVarChar, 500);
            param.Value = GAIME_NOM;
            param = cmd.Parameters.Add("@ODENILEN_MEBLEG", SqlDbType.Decimal);
            param.Value = ODENILEN_MEBLEG;
            param = cmd.Parameters.Add("@MAL_DETAILS_ID", SqlDbType.Int);
            param.Value = MAL_DETAILS_ID;
            param = cmd.Parameters.Add("@MAGAZA", SqlDbType.Int);
            param.Value = MAGAZA_;
            param = cmd.Parameters.Add("@MIGDARI", SqlDbType.NVarChar, 500);

            param.Value = MIGDARI;
            param = cmd.Parameters.Add("@SATIS_GIYMETI", SqlDbType.NVarChar, 50);
            param.Value = SATIS_GIYMETI;
            param = cmd.Parameters.Add("@ENDIRIM_FAIZ", SqlDbType.NVarChar, 500);
            param.Value = ENDIRIM_FAIZ;

            param = cmd.Parameters.Add("@ENDIRIM_AZN", SqlDbType.NVarChar, 500);
            param.Value = ENDIRIM_AZN;

            param = cmd.Parameters.Add("@ENDIRIM_MEBLEGI", SqlDbType.NVarChar, 500);
            param.Value = ENDIRIM_MEBLEGI;

            param = cmd.Parameters.Add("@YEKUN_MEBLEG", SqlDbType.NVarChar, 500);
            param.Value = YEKUN_MEBLEG;


            param = cmd.Parameters.Add("@GEYD", SqlDbType.NVarChar, 500);
            param.Value = GEYD;
            // Add the output parameter.
            param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }




        public int GAIME_SATISI_DELETE(int gaime_satisi_details_id)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(DELETE_GAIME, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@GAIME_SATISI_DETAILS_ID", SqlDbType.Int);
            param.Value = gaime_satisi_details_id;


            // Add the output parameter.
            param = cmd.Parameters.Add("@empcount", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }

        public int GAIME_SATISI_GAYTARMA_DETAILS(int gaime_satisi_gaytarma_main_id, int GAIME_SATIS_DETAILS_ID,
    Decimal MIGDAR)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure5, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@gaime_satisi_gaytarma_main_id", SqlDbType.Int);
            param.Value = gaime_satisi_gaytarma_main_id;
            param = cmd.Parameters.Add("@gaime_satisi_details_id", SqlDbType.Int);
            param.Value = GAIME_SATIS_DETAILS_ID;
            param = cmd.Parameters.Add("@migdar", SqlDbType.Decimal);
            param.Value = MIGDAR;

            // Add the output parameter.
            param = cmd.Parameters.Add("@empcount", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }

        public int GAIME_SATISI_GAYTARMA_MAIN(string EMELIYYAT_NOMRE, string GAIME_NOMRE,
            DateTime TARIX, string geyd)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure6, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@EMELIYYAT_NOMRE", SqlDbType.NVarChar, 50);
            param.Value = EMELIYYAT_NOMRE;
            param = cmd.Parameters.Add("@GAIME_NOMRE", SqlDbType.NVarChar, 20);
            param.Value = GAIME_NOMRE;
            param = cmd.Parameters.Add("@TARIX", SqlDbType.Date);
            param.Value = TARIX;
            param = cmd.Parameters.Add("@geyd", SqlDbType.NVarChar, 100);
            param.Value = geyd;
            // Add the output parameter.
            param = cmd.Parameters.Add("@empcount", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }


        public int GAIME_SATISI_MAIN(string EMELIYYAT_NOMRE, string GAIME_NOMRE,
    string ODENILEN_MEBLEG, DateTime TARIX, string ODEME_TIPI, string musteri, int _u_id, string EDV_SIZ, string EDV_LI, int musteri_id_)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@EMELIYYAT_NOMRE", SqlDbType.NVarChar, 50);
            param.Value = EMELIYYAT_NOMRE;
            param = cmd.Parameters.Add("@GAIME_NOMRE", SqlDbType.NVarChar, 20);
            param.Value = GAIME_NOMRE;
            param = cmd.Parameters.Add("@ODENILEN_MEBLEG", SqlDbType.NVarChar, 100);
            param.Value = ODENILEN_MEBLEG;
            param = cmd.Parameters.Add("@TARIX", SqlDbType.Date);
            param.Value = TARIX;
            param = cmd.Parameters.Add("@ODEME_TIPI", SqlDbType.NVarChar, 50);
            param.Value = ODEME_TIPI;

            param = cmd.Parameters.Add("@musteri", SqlDbType.NVarChar, 500);
            param.Value = musteri;
            param = cmd.Parameters.Add("@u_id", SqlDbType.Int);
            param.Value = _u_id;
            param = cmd.Parameters.Add("@ODENILEN_EDV_SIZ_MEBLEG", SqlDbType.NVarChar, 20);
            param.Value = EDV_SIZ;
            param = cmd.Parameters.Add("@ODENILEN_MEBLEG_EDV", SqlDbType.NVarChar, 20);
            param.Value = EDV_LI;

            param = cmd.Parameters.Add("@musteri_main_id", SqlDbType.Int);
            param.Value = musteri_id_;

            // Add the output parameter.
            param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Convert.ToInt32(param.Value);
        }


        public int GAIME_SATISI_DETAILS(int GAIME_SATISI_MAIN_ID, DateTime TARIX, string EMMELIYYAT_NOMRE, string ODEME_TIPI, string GAIME_NOM,
            string ODENILEN_MEBLEG, int MAL_DETAILS_ID, int MAGAZA, int ANBAR, string MIGDARI, string SATIS_GIYMETI, string ENDIRIM_FAIZ,
            string ENDIRIM_AZN, string ENDIRIM_MEBLEGI, string YEKUN_MEBLEG, string GEYD, DateTime tarix_, string EDV_SIZ, string EDV_LI)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure2, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@GAIME_SATISI_MAIN_ID", SqlDbType.Int);
            param.Value = GAIME_SATISI_MAIN_ID;
            param = cmd.Parameters.Add("@TARIX", SqlDbType.DateTime);
            param.Value = TARIX;
            param = cmd.Parameters.Add("@EMMELIYYAT_NOMRE", SqlDbType.NVarChar, 100);
            param.Value = EMMELIYYAT_NOMRE;
            param = cmd.Parameters.Add("@ODEME_TIPI", SqlDbType.NVarChar, 100);
            param.Value = ODEME_TIPI;
            param = cmd.Parameters.Add("@GAIME_NOM", SqlDbType.NVarChar, 100);
            param.Value = GAIME_NOM;
            param = cmd.Parameters.Add("@ODENILEN_MEBLEG", SqlDbType.NVarChar, 30);
            param.Value = ODENILEN_MEBLEG;
            param = cmd.Parameters.Add("@MAL_DETAILS_ID", SqlDbType.Int);
            param.Value = MAL_DETAILS_ID;
            param = cmd.Parameters.Add("@MAGAZA", SqlDbType.Int);
            param.Value = MAGAZA;
            param = cmd.Parameters.Add("@ANBAR", SqlDbType.Int);
            param.Value = ANBAR;
            param = cmd.Parameters.Add("@MIGDARI", SqlDbType.NVarChar, 100);
            param.Value = MIGDARI;
            param = cmd.Parameters.Add("@SATIS_GIYMETI", SqlDbType.NVarChar, 100);
            param.Value = SATIS_GIYMETI;
            param = cmd.Parameters.Add("@ENDIRIM_FAIZ", SqlDbType.NVarChar, 100);
            param.Value = ENDIRIM_FAIZ;
            param = cmd.Parameters.Add("@ENDIRIM_AZN", SqlDbType.NVarChar, 100);
            param.Value = ENDIRIM_AZN;
            param = cmd.Parameters.Add("@ENDIRIM_MEBLEGI", SqlDbType.NVarChar, 100);
            param.Value = ENDIRIM_MEBLEGI;
            param = cmd.Parameters.Add("@YEKUN_MEBLEG", SqlDbType.NVarChar, 100);
            param.Value = YEKUN_MEBLEG;
            param = cmd.Parameters.Add("@GEYD", SqlDbType.NVarChar, 100);
            param.Value = GEYD;
            param = cmd.Parameters.Add("@DATE", SqlDbType.Date);
            param.Value = tarix_;
            param = cmd.Parameters.Add("@ODENILEN_EDV_SIZ_MEBLEG", SqlDbType.NVarChar, 30);
            param.Value = EDV_SIZ;
            param = cmd.Parameters.Add("@ODENILEN_MEBLEG_EDV", SqlDbType.NVarChar, 30);
            param.Value = EDV_LI;

            // Add the output parameter.
            param = cmd.Parameters.Add("@EMPCOUNT", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            //Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


            return Convert.ToInt32(param.Value);
        }

        public void GAIME_SATISI_MAIN_DEATAILS_DELETE(int GAIME_SATISI_MAIN_ID)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure3, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@GAIME_SATISI_MAIN_ID", SqlDbType.Int);
            param.Value = GAIME_SATISI_MAIN_ID;


            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void GAIME_SATISI_MAIN_DEATAILS_UPDATE(int GAIME_SATISI_DETAILS_ID, DateTime TARIX, string EMMELIYYAT_NOMRE, string ODEME_TIPI, string GAIME_NOM,
            decimal ODENILEN_MEBLEG, int MAL_DETAILS_ID, string MAGAZA, string ANBAR, string MIGDARI, string SATIS_GIYMETI, string ENDIRIM_FAIZ,
            string ENDIRIM_AZN, string ENDIRIM_MEBLEGI, string YEKUN_MEBLEG, string GEYD)

        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure4, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@GAIME_SATISI_DETAILS_ID", SqlDbType.Int);
            param.Value = GAIME_SATISI_DETAILS_ID;
            param = cmd.Parameters.Add("@TARIX", SqlDbType.DateTime);
            param.Value = TARIX;
            param = cmd.Parameters.Add("@EMMELIYYAT_NOMRE", SqlDbType.NVarChar, 100);
            param.Value = EMMELIYYAT_NOMRE;
            param = cmd.Parameters.Add("@ODEME_TIPI", SqlDbType.NVarChar, 100);
            param.Value = ODEME_TIPI;
            param = cmd.Parameters.Add("@GAIME_NOM", SqlDbType.NVarChar, 100);
            param.Value = GAIME_NOM;
            param = cmd.Parameters.Add("@ODENILEN_MEBLEG", SqlDbType.Decimal);
            param.Value = ODENILEN_MEBLEG;
            param = cmd.Parameters.Add("@MAL_DETAILS_ID", SqlDbType.Int);
            param.Value = MAL_DETAILS_ID;
            param = cmd.Parameters.Add("@MAGAZA", SqlDbType.NVarChar, 100);
            param.Value = MAGAZA;
            param = cmd.Parameters.Add("@ANBAR", SqlDbType.NVarChar, 100);
            param.Value = ANBAR;
            param = cmd.Parameters.Add("@MIGDARI", SqlDbType.NVarChar, 100);
            param.Value = MIGDARI;
            param = cmd.Parameters.Add("@SATIS_GIYMETI", SqlDbType.NVarChar, 100);
            param.Value = SATIS_GIYMETI;
            param = cmd.Parameters.Add("@ENDIRIM_FAIZ", SqlDbType.NVarChar, 100);
            param.Value = ENDIRIM_FAIZ;
            param = cmd.Parameters.Add("@ENDIRIM_AZN", SqlDbType.NVarChar, 100);
            param.Value = ENDIRIM_AZN;
            param = cmd.Parameters.Add("@ENDIRIM_MEBLEGI", SqlDbType.NVarChar, 100);
            param.Value = ENDIRIM_MEBLEGI;
            param = cmd.Parameters.Add("@YEKUN_MEBLEG", SqlDbType.NVarChar, 100);
            param.Value = YEKUN_MEBLEG;
            param = cmd.Parameters.Add("@GEYD", SqlDbType.NVarChar, 100);
            param.Value = GEYD;


            // Add the output parameter.
            //param = cmd.Parameters.Add("@emp_count", SqlDbType.Int);
            //param.Direction = ParameterDirection.Output;
            // Execute the command.
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            // return Convert.ToInt32(param.Value);
        }
    }
}
