using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Helpers.CacheData
{
    public static class CommonData
    {
        public static DatabaseClasses.User User = DbProsedures.GetUser();
    }
}
