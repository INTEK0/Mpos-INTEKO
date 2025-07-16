using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Helpers.CacheData
{
    public class UserCacheService
    {
        public static DatabaseClasses.User User = DbProsedures.GetUser();
    }
}
