using DevExpress.XtraGrid.Localization;
using Microsoft.Win32;

namespace Licence.Helpers
{
    public class FormHelpers
    {
        /// <summary>
        /// Gridin axtarış çubuğundakı düymənin adını dəyiştirir
        /// </summary>
        public class MyGridLocalizer : GridLocalizer
        {
            public override string GetLocalizedString(GridStringId id)
            {
                if (id == GridStringId.FindControlFindButton)
                    return "Axtar";
                return base.GetLocalizedString(id);
            }
        }

        public static void FolderControl()
        {
            #region [..: REGEDIT FILE :..]

            if (Registry.GetValue($@"{Registry.CurrentUser}\Mpos", "ProductID", null) == null)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("ProductID", "Yoxdur");
            }

            #endregion [..: REGEDIT FILE :..]
        }

        public static bool HasInternetConnection()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
