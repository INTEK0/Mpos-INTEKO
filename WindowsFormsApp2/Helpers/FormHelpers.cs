using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Win32;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.NKA;

namespace WindowsFormsApp2.Helpers
{
    public static class FormHelpers
    {
        static FormHelpers()
        {
            MessageBoxManager.Yes = "Bəli";
            MessageBoxManager.No = "Xeyr";
            MessageBoxManager.OK = "OK";
            MessageBoxManager.Retry = "Təkrar yoxla";
            MessageBoxManager.Cancel = "Ləğv et";
        }

        public static void ControlLoad<T>(T data, Control control, string displayMember = "Name", string valueMember = "Id") where T : class
        {
            if (control is System.Windows.Forms.ComboBox ctrl)
            {
                //var ctrl = control as System.Windows.Forms.ComboBox;
                ctrl.DisplayMember = displayMember;
                ctrl.ValueMember = valueMember;
                ctrl.DataSource = data;
            }
            else if (control is GridControl)
            {
                var grid = control as GridControl;
                var view = grid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                grid.DataSource = data;
                view.GroupPanelText = "Qruplaşdırmaq üçün sütun başlıqlarını buraya sürükləyin";
                view.OptionsFind.FindNullPrompt = "Axtarış edin..";
                view.RefreshData();
            }
            else if (control is LookUpEdit look)
            {
                look.Properties.Columns.Clear();

                look.Properties.DataSource = data;
                look.Properties.DisplayMember = displayMember;
                look.Properties.ValueMember = valueMember;
                look.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(displayMember));

                int rowCount = 0;
                if (data is IList list)
                    rowCount = list.Count;
                else if (data is DataTable dt)
                    rowCount = dt.Rows.Count;
                else if (data is IEnumerable<object> enumerable)
                    rowCount = enumerable.Count();

                look.Properties.DropDownRows = rowCount > 7 ? 7 : rowCount;
            }
        }

        public static void GridPanelText(GridView grid)
        {
            grid.GroupPanelText = "Qruplaşdırmaq üçün sütun başlıqlarını buraya sürükləyin";
            grid.OptionsFind.FindNullPrompt = "Axtarış edin";
        }

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

        public static void Log(string message)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {

                string query = "INSERT INTO Logs (UserID, Operation, Date, Time) VALUES (@UserID, @Operation, @Date, @Time)";

                SqlCommand command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@UserID", Properties.Settings.Default.UserID);
                command.Parameters.AddWithValue("@Operation", message);
                command.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                command.Parameters.AddWithValue("@Time", DateTime.Now.TimeOfDay);

                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Əməliyyat səhvi\n\n{ex.Message}", "Logs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static void OperationLog(DatabaseClasses.OperationLogs logs)
        {
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                string query = "INSERT INTO OperationLogs (UserID, TypeId,OperationId, Tarix, Saat, Message,RequestCode,ResponseCode) VALUES (@UserID, @TypeId, @OperationId, @Date, @Time, @Message, @RequestCode, @ResponseCode)";

                SqlCommand command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@UserID", Properties.Settings.Default.UserID);
                command.Parameters.AddWithValue("@OperationId", logs.OperationId);
                command.Parameters.AddWithValue("@TypeId", (int)logs.OperationType);
                command.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                command.Parameters.AddWithValue("@Time", DateTime.Now.TimeOfDay);
                command.Parameters.AddWithValue("@Message", (object)logs.Message ?? DBNull.Value);
                command.Parameters.AddWithValue("@RequestCode", (object)logs.RequestCode ?? DBNull.Value);
                command.Parameters.AddWithValue("@ResponseCode", (object)logs.ResponseCode ?? DBNull.Value);


                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Əməliyyat səhvi\n\n{ex.Message}", "OperationLogs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static void Alert(string msg, Enums.MessageType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }

        public static void ExcelExport(GridControl gridControl, string FileName)
        {
            GridView gridView = gridControl.MainView as GridView;

            if (gridView.RowCount is 0)
            {
                FormHelpers.Alert("Çap ediləcək məlumat yoxdur", Enums.MessageType.Info);
                return;
            }

            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Excel faylı|*.xlsx";
                saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveFile.OverwritePrompt = true; //varsa soruşmadan üstünə yazması üçün false olaraq qalmalıdır
                saveFile.FileName = $"{FileName}_{DateTime.Now.ToShortDateString()}.xlsx";
                if (saveFile.ShowDialog() is DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    gridControl.ExportToXlsx(saveFile.FileName);
                    FormHelpers.Log($"{FileName} çap edildi");
                }
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE($"Çap zamanı xəta yarandı\n\n {ex.Message}");
            }
            finally { Cursor.Current = Cursors.Default; }
        }

        public static bool SendEmail(string bodyMessage, string toEmail, string subject = "MPOS Giriş məlumatları")
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                MailMessage sms = new MailMessage();
                smtp.Credentials = new NetworkCredential(Emaildata.Email, Emaildata.Password);
                smtp.Port = Emaildata.Port;
                smtp.Host = Emaildata.SMTPAdress;
                smtp.EnableSsl = true;
                sms.To.Add(toEmail);
                sms.From = new MailAddress(Emaildata.Email, Emaildata.Header);
                sms.Subject = subject;
                sms.IsBodyHtml = true;
                sms.Body = bodyMessage;
                smtp.Send(sms);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static T OpenForm<T>(params object[] constructorArgs) where T : Form
        {
            T form = Application.OpenForms.OfType<T>().FirstOrDefault();

            if (form == null)
            {
                form = (T)Activator.CreateInstance(typeof(T), constructorArgs);
                form.Show();
            }
            else
            {
                form.WindowState = FormWindowState.Maximized;
                form.BringToFront();
            }

            return form;
        }

        public static IpModel GetIpModel()
        {
            try
            {
                string ip = null, model = null, merchantId = null, cashier = null, bank = null;
                using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
                {
                    connection.Open();
                    //AzSmart - 8008
                    //NBA - 9898
                    //NBA BANK - 9999

                    string query = $@"select 
u.AD as Cashier,
model = case 
when kf.KASSA_FIRMALAR=N'SUNMI' then 1 
when kf.KASSA_FIRMALAR=N'AzSMART' then 2 
when kf.KASSA_FIRMALAR=N'OMNITECH' then 3 
when kf.KASSA_FIRMALAR=N'DATAPAY'then 5 
when kf.KASSA_FIRMALAR=N'NBA'then 6 
when kf.KASSA_FIRMALAR=N'EKASAM' then 7 
when kf.KASSA_FIRMALAR=N'XPRINTER' then 4 else  0 end , 
ip_ = case 
when kf.KASSA_FIRMALAR = N'SUNMI' THEN  'http://' + ki.IP_ADRESS + ':5544'
when kf.KASSA_FIRMALAR = N'AzSMART' then 'http://' + ki.IP_ADRESS + ':{AzSmart.FiskalPort}' 
when kf.KASSA_FIRMALAR = N'DATAPAY' then 'http://'+ ki.IP_ADRESS + ':2222'
when kf.KASSA_FIRMALAR = N'NBA' then 'http://'+ ki.IP_ADRESS + ':{NBA.NBA_FISCAL_SERVICE_PORT}/api/v1'
when kf.KASSA_FIRMALAR = N'OMNITECH' then 'http://'+ ki.IP_ADRESS + ':8989/v2'
when kf.KASSA_FIRMALAR = N'EKASAM' then 'http://'+ ki.IP_ADRESS + ':9876/api/'
else '' end  ,
rtrim(ltrim(isnull(ki.merchant_id,'yox'))) merchant_id,
rtrim(ltrim(isnull(ki.Bank, ''))) bank
FROM KASSA_IP ki 
inner join KASSA_FIRMALAR kf on ki.KASSA_FIRMA_IP = kf.KASSA_FIRMALAR_ID
inner join userParol u on u.id = ki.KASSIR_ID where u.id = {Properties.Settings.Default.UserID}";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                ip = dr["ip_"].ToString();
                                model = dr["model"].ToString();
                                merchantId = dr["merchant_id"].ToString();
                                cashier = dr["Cashier"].ToString();
                                bank = dr["bank"].ToString();
                            }

                            IpModel ıpModel = new IpModel
                            {
                                Ip = ip,
                                Model = model,
                                MerchantId = merchantId,
                                Cashier = cashier,
                                BankName = bank,
                            };

                            return ıpModel;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
                return null;
            }
        }

        public static void FolderControl()
        {
            #region [..BankTTNM DOCUMENT..]
            if (!File.Exists(Application.StartupPath + @"\BankTTNM.txt"))
                File.Create(Application.StartupPath + @"\BankTTNM.txt");
            #endregion [..BankTTNM.txt DOCUMENT..]


            #region [..TEMP FOLDER..]
            //if (!Directory.Exists(Application.StartupPath + @"\temp\"))
            //    Directory.CreateDirectory(Application.StartupPath + @"\temp\");
            //else
            //{
            //    Directory.Delete(Application.StartupPath + @"\temp\", true);
            //    Directory.CreateDirectory(Application.StartupPath + @"\temp\");
            //}
            #endregion [..TEMP FOLDER..]


            #region [..BACKUP FOLDER..]
            string backupFolderPath = Path.Combine(Application.StartupPath, "backup");
            if (!Directory.Exists(backupFolderPath))
            {
                Directory.CreateDirectory(backupFolderPath);
            }
            //else
            //{
            //    Directory.Delete(backupFolderPath, true);
            //    Directory.CreateDirectory(backupFolderPath);
            //}

            #endregion [..BACKUP FOLDER..]


            #region [..REGEDIT FILE..]

            //if (Registry.GetValue(@"HKEY_CURRENT_USER\Mpos", "AutoUpdate", null) == null)
            //{ Registry.CurrentUser.CreateSubKey("Mpos").SetValue("AutoUpdate", "False"); }

            if (Registry.GetValue(@"HKEY_CURRENT_USER\Mpos\", "DecreasingAmount", null) == null)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("DecreasingAmount", false);
            }

            if (Registry.GetValue(@"HKEY_CURRENT_USER\Mpos\", "SuccessMessageVisible", null) == null)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("SuccessMessageVisible", true);
            }

            if (Registry.GetValue(@"HKEY_CURRENT_USER\Mpos\", "HotSalesShow", null) == null)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("HotSalesShow", false);
            }

            if (Registry.GetValue(@"HKEY_CURRENT_USER\Mpos\", "SendToKassa", null) == null)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("SendToKassa", false);
            }

            if (Registry.GetValue(@"HKEY_CURRENT_USER\Mpos\", "IsReceipt", null) == null)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("IsReceipt", false);
            }

            if (Registry.GetValue(@"HKEY_CURRENT_USER\Mpos\", "TerminalCashierPrint", null) == null)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("TerminalCashierPrint", true);
            }

            if (Registry.GetValue(@"HKEY_CURRENT_USER\Mpos\", "ClinicModule", null) == null)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("ClinicModule", false);
            }

            if (Registry.GetValue(@"HKEY_CURRENT_USER\Mpos\", "OtherPay", null) == null)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("OtherPay", false);
            }

            if (Registry.GetValue(@"HKEY_CURRENT_USER\Mpos\", "CloudApp", null) == null)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("CloudApp", false);
            }
            #endregion [..REGEDIT FILE..]
        }

        public static void PingHostAsync(string host)
        {
            using (Ping ping = new Ping())
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    PingReply reply = ping.Send(host);
                    if (reply.Status == IPStatus.Success)
                    {
                        Alert($"{host} adresi ilə əlaqə mövcuddur", Enums.MessageType.Success);
                    }
                    else
                    {
                        Alert($"{host} adresi ilə əlaqə yoxdur", Enums.MessageType.Error);
                    }
                }
                catch (Exception ex)
                {
                    ReadyMessages.ERROR_DEFAULT_MESSAGE($"Error ping {host}: {ex.Message}");
                }
                finally { Cursor.Current = Cursors.Default; }
            }
        }

        public static T MapReaderToObject<T>(IDataRecord record) where T : new()
        {
            T obj = new T();

            var columns = Enumerable.Range(0, record.FieldCount)
                        .Select(i => record.GetName(i))
                        .ToHashSet(StringComparer.OrdinalIgnoreCase); // böyük-kiçik hərf həssaslığı üçün

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (columns.Contains(property.Name))
                {
                    if (!record.IsDBNull(record.GetOrdinal(property.Name)))
                    {
                        property.SetValue(obj, record[property.Name], null);
                    }
                }
            }
            return obj;
        }

        public static List<T> MapReaderToList<T>(IDataReader reader) where T : new()
        {
            List<T> list = new List<T>();

            while (reader.Read())
            {
                T obj = new T();
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    if (reader.GetOrdinal(property.Name) >= 0 && !reader.IsDBNull(reader.GetOrdinal(property.Name)))
                    {
                        property.SetValue(obj, reader[property.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public static bool SuccessMessageVisible()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("SuccessMessageVisible").ToString());
            if (control)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static class Emaildata
        {
            public static string Email { get; set; } = "support@inteko.az";
            public static string Password { get; set; } = "Hh143268842800";
            public static int Port { get; set; } = 587;
            public static string SMTPAdress { get; set; } = "mail.inteko.az";
            public static string Header { get; set; } = "MPOS - İNTEKO";
        }

        public class IpModel
        {
            public string Model { get; set; }
            public string Ip { get; set; }
            public string MerchantId { get; set; } = null;
            public string Cashier { get; set; } = null;
            public string BankName { get; set; }
        }
    }
}