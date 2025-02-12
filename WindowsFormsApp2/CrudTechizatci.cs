using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
  public   class CrudTechizatci
    {

       // string connectionString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";

        string procedure = "INSERT_TECHIZATCI";

        public int InsertTechizatci(string TECHIZATCI_NOMRE ,string MUGAVİLE_NOM, string	SIRKET_ADI,	string UNVAN,string ELAGE_NOMRE,
	string ELEKTRON_POCT,decimal ILKIN_BORC ,int SAHIBKAR_TECHIZATCI ,string TECHIZATCI_VOEN,string HESAB_AD,
	string BANK_ADI,string BANK_VOEN,string KOD,string MH,string SWIFT,string MESUL_SEXS,
	string DESCRIPTION,string ELAGE_NOM2,string ELAGE_NOM3)
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand(procedure, con);
            // Configure command and add input parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@TECHIZATCI_NOMRE", SqlDbType.NVarChar, 500);
            param.Value = TECHIZATCI_NOMRE;
            param = cmd.Parameters.Add("@MUGAVİLE_NOM", SqlDbType.NVarChar, 500);
            param.Value = MUGAVİLE_NOM;
            param = cmd.Parameters.Add("@SIRKET_ADI", SqlDbType.NVarChar, 500);
            param.Value = SIRKET_ADI;
            param = cmd.Parameters.Add("@UNVAN", SqlDbType.NVarChar, 500);
            param.Value = UNVAN;
            param = cmd.Parameters.Add("@ELAGE_NOMRE", SqlDbType.NVarChar, 500);
            param.Value = ELAGE_NOMRE;

            param = cmd.Parameters.Add("@ELEKTRON_POCT", SqlDbType.NVarChar, 500);
            param.Value = ELEKTRON_POCT;
            param = cmd.Parameters.Add("@ILKIN_BORC", SqlDbType.NVarChar, 500);
            param.Value = ILKIN_BORC;
            param = cmd.Parameters.Add("@SAHIBKAR_TECHIZATCI", SqlDbType.NVarChar, 500);
            param.Value = SAHIBKAR_TECHIZATCI;
            param = cmd.Parameters.Add("@TECHIZATCI_VOEN", SqlDbType.NVarChar, 500);
            param.Value = TECHIZATCI_VOEN;
            param = cmd.Parameters.Add("@HESAB_AD", SqlDbType.NVarChar, 500);
            param.Value = HESAB_AD;
            param = cmd.Parameters.Add("@BANK_ADI", SqlDbType.NVarChar, 500);
            param.Value = BANK_ADI;
            param = cmd.Parameters.Add("@BANK_VOEN", SqlDbType.NVarChar, 500);
            param.Value = BANK_VOEN;
            param = cmd.Parameters.Add("@KOD", SqlDbType.NVarChar, 500);
            param.Value = KOD;
            param = cmd.Parameters.Add("@MH", SqlDbType.NVarChar, 500);
            param.Value = MH;
            param = cmd.Parameters.Add("@SWIFT", SqlDbType.NVarChar, 500);
            param.Value = SWIFT;
            param = cmd.Parameters.Add("@MESUL_SEXS", SqlDbType.NVarChar, 500);
            param.Value = MESUL_SEXS;
            param = cmd.Parameters.Add("@DESCRIPTION", SqlDbType.NVarChar, 500);
            param.Value = DESCRIPTION;
            param = cmd.Parameters.Add("@ELAGE_NOMRE2", SqlDbType.NVarChar, 500);
            param.Value = ELAGE_NOM2;
            param = cmd.Parameters.Add("@ELAGE_NOMRE3", SqlDbType.NVarChar, 500);
            param.Value = ELAGE_NOM3;
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
