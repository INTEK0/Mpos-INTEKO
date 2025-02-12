using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;

namespace WindowsFormsApp2
{
    public partial class PASSWORDEMAIL : DevExpress.XtraEditors.XtraForm
    {
        string unames, uparol, uemail, uNameSurname;
        int count = 0;

        public PASSWORDEMAIL()
        {
            InitializeComponent();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string epoct = tEmail.Text;
            if (string.IsNullOrWhiteSpace(epoct))
            {
                FormHelpers.Alert("E-poçt ünvanını daxil etmədiniz", Enums.MessageType.Warning);
                return;
            }

            Cursor = Cursors.WaitCursor;
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            conn.ConnectionString = Properties.Settings.Default.SqlCon;
            conn.Open();
            string query = "SELECT top 1 [Ulogin],[Uparol],[AD],[EMAILL] FROM [dbo].[userParol] where [EMAILL]='" + epoct + "'";

            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    count++;
                    uNameSurname = dr["AD"].ToString();
                    unames = dr["Ulogin"].ToString();
                    uparol = dr["Uparol"].ToString();
                    uemail = dr["EMAILL"].ToString();
                }

            }
            if (count > 0)
            {

                string body = $"Hörmətli <b>{uNameSurname}</b><br>Unutduğunuz MPOS istifadəçi məlumatları<br>İstifadəçi adı: <b>{unames}</b><br>Şifrəniz: <b> {uparol}</b>";
                if (FormHelpers.SendEmail(body, uemail))
                {
                    FormHelpers.Alert("MPOS Şifrəsi email ünvanınıza göndərildi", Enums.MessageType.Success);
                }
                Cursor = Cursors.Default;
                this.Close();

            }

            else
            {

                FormHelpers.Alert("E-poçt ünvanı sistemdə mövcud deyil", Enums.MessageType.Error);
                Cursor = Cursors.Default;
                return;

            }
        }
    }
}