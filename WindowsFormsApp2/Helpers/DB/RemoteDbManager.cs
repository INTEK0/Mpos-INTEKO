using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2.Helpers.DB
{
    public static class RemoteDbManager
    {
        private static string _con;

        public static void SetConnectionString(string con)
        {
            _con = con;
        }
        public static string BranchConnectionString => _con;

        public static async Task<bool> ServerConnectionAsync()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var builder = new SqlConnectionStringBuilder(_con)
                {
                    ConnectTimeout = 3 
                };
                using (var con = new SqlConnection(builder.ConnectionString))
                {
                    await con.OpenAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public static void ClearConnection()
        {
            _con = null;
        }
    }
}
