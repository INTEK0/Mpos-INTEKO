using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using Microsoft.Win32;
using WindowsFormsApp2.Helpers.CacheData;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.DB.DTOs;

namespace WindowsFormsApp2.Helpers.DB
{
    public class DbHelpers
    {
        private static string DbConnectionString = Properties.Settings.Default.SqlCon;
        public static readonly string LastDocumentFiskalId = $"SELECT TOP 1 fiscal_id FROM pos_satis_check_main WHERE user_id_ = {Properties.Settings.Default.UserID} ORDER BY pos_satis_check_main_id DESC";
        public static readonly string GetItemDataQuery = $"select name,Item.item_id,salePrice,quantity,case vatType when 1 then '18' when 3 then '0' when 4 then '2' when 5 then '8' else 0 end as vatType,quantityType,salePrice*quantity as ssum from  dbo.item where user_id = {Properties.Settings.Default.UserID}";
        public static readonly string GetHeaderDataQuery = $@"SELECT 
                                                              cashPayment,
                                                              cardPayment,
	                                                          bonusPayment,
	                                                          paidPayment,
	                                                          clientName,
	                                                          header_id,
	                                                          cashPayment + cardPayment as tot
	                                                          FROM  dbo.header WHERE userId = {Properties.Settings.Default.UserID}";
        public static readonly string GetPosGaytarmaManualQuery = $@"SELECT [pos_satis_check_main_id],
        [pos_nomre],
        [fiscal_id],
        [NEGD_],
        [KART_],
        [UMUMI_MEBLEG], 
        [fiscalNum]
        FROM [pos_satis_check_main] WHERE [pos_satis_check_main_id] IN 
        (SELECT [pos_satis_check_main_id] FROM [pos_gaytarma_manual] WHERE [pos_gaytarma_manual_id] = 
        (SELECT MAX([pos_gaytarma_manual_id]) FROM [pos_gaytarma_manual] WHERE user_id_ = {Properties.Settings.Default.UserID}));";

        private static string _currentConnectionString = DbConnectionString;

        public static string CurrentConnectionString
        {
            get => _currentConnectionString;
            set => _currentConnectionString = value;
        }

        public static void UseRemoteConnection(string con)
        {
            _currentConnectionString = con;
        }

        public static void UseLocalConnection()
        {
            _currentConnectionString = DbConnectionString;
        }

        public static void DatabaseBackup()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SaveFileDialog save = new SaveFileDialog();

                save.FileName = $"Mpos_v{Application.ProductVersion}_backup_{DateTime.Now.ToShortDateString()}.bak";
                save.InitialDirectory = Path.Combine(Application.StartupPath, "backup");
                save.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                save.OverwritePrompt = true; //varsa soruşmadan üstünə yazması üçün false olaraq qalmalıdır

                if (save.ShowDialog() is DialogResult.OK)
                {
                    using (SqlConnection connection = new SqlConnection(CurrentConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand($@"BACKUP DATABASE {connection.Database} TO DISK='{save.FileName}'", connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    // Registry-ə yaz
                    Registry.CurrentUser
                        .CreateSubKey("Mpos")
                        ?.CreateSubKey("Backup")
                        ?.SetValue("History", DateTime.Now.ToString("dd.MM.yyyy - HH:mm"));

                    string successMessage = "Verilənlər bazasının nüsxəsi uğurla yaradıldı";
                    FormHelpers.Log(successMessage);
                    FormHelpers.Alert(successMessage, Enums.MessageType.Success);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Verilənlər bazasının nüsxəsi yaradılarkən xəta yarandı.";
                FormHelpers.Log(errorMessage);
                ReadyMessages.ERROR_DEFAULT_MESSAGE($"{errorMessage} \nXəta mesajı: {ex.Message}");
                return;
            }
            finally { Cursor.Current = Cursors.Default; }
        }

        private static void DbBackupSettingSeedDataInsert()
        {
            string query = @"INSERT INTO DbBackupSettings (DailyBackup, IsDailyDeleted, UserId) 
VALUES (@DailyBackup, @IsDailyDeleted, @UserId)";

            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@DailyBackup", false);
                cmd.Parameters.AddWithValue("@IsDailyDeleted", 0);
                cmd.Parameters.AddWithValue("@UserId", UserCacheService.User.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public static DbBackupSettingDto DbBackupSettingLoad()
        {
            string query = @"SELECT [Id]
      ,[DailyBackup]
      ,[Email]
      ,[UserId]
  FROM [DbBackupSettings]
  WHERE UserId = @userId";

            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@userId", UserCacheService.User.Id);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        var data = FormHelpers.MapReaderToObject<DbBackupSettingDto>(dr);
                        return data;
                    }
                }
            }

            DbBackupSettingSeedDataInsert();
            return DbBackupSettingLoad();
        }

        public static void AutoBackupOnLogin()
        {
            try
            {
                string backupPath = Path.Combine(Application.StartupPath, "backup");
                if (!Directory.Exists(backupPath))
                    Directory.CreateDirectory(backupPath);

                string lastBackup = Registry.CurrentUser
                    .OpenSubKey("Mpos\\Backup")
                    ?.GetValue("History")
                    ?.ToString();

                if (lastBackup != null)
                    if (DateTime.TryParseExact(lastBackup, "dd.MM.yyyy - HH:mm",
                            null, System.Globalization.DateTimeStyles.None, out DateTime lastDate))
                    {
                        if (lastDate.Date == DateTime.Today)
                            return;
                    }

                string fileName = Path.Combine(backupPath,
                    $"Mpos_v{Application.ProductVersion}_backup_{DateTime.Now.ToShortDateString()}.bak");

                using (SqlConnection connection = new SqlConnection(CurrentConnectionString))
                {
                    connection.Open();
                    string safePath = fileName.Replace("'", "''");

                    using (SqlCommand command = new SqlCommand(
                               $"BACKUP DATABASE [{connection.Database}] TO DISK=N'{safePath}' WITH INIT", connection))
                    {
                        command.CommandTimeout = 300; // 5 dəqiqə
                        command.ExecuteNonQuery();
                    }
                }

                // Registry-ə yaz
                Registry.CurrentUser
                    .CreateSubKey("Mpos")
                    ?.CreateSubKey("Backup")
                    ?.SetValue("History", DateTime.Now.ToString("dd.MM.yyyy - HH:mm"));

                FormHelpers.Log("Avtomatik backup uğurla tamamlandı.");

            }
            catch (Exception ex)
            {
                FormHelpers.Log($"Avtomatik backup xətası: {ex.Message}");
                ReadyMessages.ERROR_DEFAULT_MESSAGE($"Xəta mesajı: {ex.Message}");
            }
        }

        public static void CleanOldBackups(int retentionDays)
        {
            string backupPath = Path.Combine(Application.StartupPath, "backup");

            if (string.IsNullOrWhiteSpace(backupPath) || retentionDays <= 0)
                return;

            if (!Directory.Exists(backupPath))
                return;

            DateTime cutoffDate = DateTime.Now.AddDays(-retentionDays);

            var files = Directory.EnumerateFiles(backupPath, "*.bak", SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                try
                {
                    var fileInfo = new FileInfo(file);

                    // Fayl hələ istifadə olunursa skip et
                    if (IsFileLocked(fileInfo))
                        continue;

                    if (fileInfo.LastWriteTime < cutoffDate)
                        fileInfo.Delete();
                }
                catch (Exception ex)
                {
                    FormHelpers.Log($"Backup silinə bilmədi: {file} | {ex.Message}");
                }
            }
        }

        private static bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                    return false;
            }
            catch
            {
                return true;
            }
        }
    }
}