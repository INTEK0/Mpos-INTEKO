using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers.Messages;

namespace WindowsFormsApp2.Helpers.DB
{
    public class DbHelpers
    {
        public static readonly string DbConnectionString = Properties.Settings.Default.SqlCon;
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


        public static void DatabaseBackup()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SaveFileDialog save = new SaveFileDialog();

                save.FileName = "MPOS_backup_" + DateTime.Now.ToShortDateString() + ".bak";
                save.InitialDirectory = Path.Combine(Application.StartupPath, "backup");
                save.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                save.OverwritePrompt = true; //varsa soruşmadan üstünə yazması üçün false olaraq qalmalıdır

                if (save.ShowDialog() is DialogResult.OK)
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand($@"BACKUP DATABASE {connection.Database} TO DISK='{save.FileName}'", connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    Registry.CurrentUser.CreateSubKey("Mpos").CreateSubKey("Backup").SetValue("History", DateTime.Now.ToString("dd.MM.yyyy - HH:mm"));
                    string successMessage = "Verilənlər bazasının nüsxəsi uğurla yaradıldı";
                    FormHelpers.Log(successMessage);
                    FormHelpers.Alert(successMessage, Enums.MessageType.Success);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Verilənlər bazasının nüsxəsi yaradılarkən xəta yarandı.";
                FormHelpers.Log(errorMessage);
                ReadyMessages.ERROR_DEFAULT_MESSAGE($"{errorMessage} Xəta mesajı: {ex.Message}");
                return;
            }
            finally { Cursor.Current = Cursors.Default; }
        }
    }
}