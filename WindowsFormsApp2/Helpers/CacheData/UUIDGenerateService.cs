using System;
namespace WindowsFormsApp2.Helpers.CacheData
{
    public class UUIDGenerateService
    {
        private static string _uuid;

        public static string UUID
        {
            get
            {
                if (_uuid == null)
                    Refreshid();

                return _uuid;
            }
        }

        public static void Refreshid()
        {
            _uuid = Guid.NewGuid().ToString();
        }

        public static string GetToken()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}