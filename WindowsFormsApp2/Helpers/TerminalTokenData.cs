using Microsoft.Win32;
using System.Collections.Generic;
using WindowsFormsApp2.Helpers.Messages;

namespace WindowsFormsApp2.Helpers
{
    public static class TerminalTokenData
    {
        private static readonly Dictionary<string, string> _cache = new Dictionary<string, string>();
        private static string GetRegistryValue(string keyName)
        {
            if (_cache.ContainsKey(keyName))
            {
                return _cache[keyName];
            }
            try
            {
                using (var subkey = Registry.CurrentUser.OpenSubKey("Mpos")?.OpenSubKey("TokenData"))
                {
                    var value = subkey?.GetValue(keyName)?.ToString() ?? string.Empty;

                    _cache[keyName] = value;
                    return value;
                }
            }
            catch (System.Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE($"{keyName} oxunarkən xəta yarandı\n\n{e.Message}");
                return string.Empty;
            }
        }
        public static string TsName => GetRegistryValue("TsName");
        public static string CompanyName => GetRegistryValue("CompanyName");
        public static string Voen => GetRegistryValue("Voen");
        public static string Address => GetRegistryValue("Address");
        public static string ObjectTaxNumber => GetRegistryValue("ObjectTaxNumber"); //Obyekt kodu
        public static string NkaModel => GetRegistryValue("NKAModel"); //kassa model
        public static string NkaSerialNumber => GetRegistryValue("NKASerialNumber"); //Kassa seriya nömrəsi
        public static string NMQRegistrationNumber => GetRegistryValue("NMQRegistrationNumber"); //Nəzarət mexanizma qurğusunun nömrəsi
    }
}