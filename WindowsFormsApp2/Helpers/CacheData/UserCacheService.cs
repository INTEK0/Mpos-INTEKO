using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Helpers.CacheData
{
    public class UserCacheService
    {
        public static DatabaseClasses.User User = DbProsedures.GetUser();
        private static IpModel _terminal;

        public static IpModel Terminal
        {
            get
            {
                if (_terminal == null)
                    RefreshTerminal();

                return _terminal;
            }
        }

        public static void RefreshTerminal()
        {
            _terminal = GetIpModel();
        }
    }
}