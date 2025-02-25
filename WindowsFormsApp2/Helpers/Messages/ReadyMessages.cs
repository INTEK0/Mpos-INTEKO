using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using static WindowsFormsApp2.Helpers.Enums;

namespace WindowsFormsApp2.Helpers.Messages
{
    public static class ReadyMessages
    {

        #region [...SUCCESS MESSAGES...]

        public static void SUCCESS_OPEN_SHIFT_MESSAGE()
        {
            XtraMessageBox.Show(CommonData.SUCCESS_OPEN_SHIFT, nameof(HeaderMessage.Mesaj), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SUCCESS_SHIFT_STATUS_MESSAGE(string message = null)
        {
            if (message == null)
            {
                XtraMessageBox.Show($"{CommonData.SUCCESS_SHIFT_STATUS}", nameof(HeaderMessage.Mesaj), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show($"{CommonData.SUCCESS_SHIFT_STATUS}\n\nNövbənin açılma tarixi:  {message}", nameof(HeaderMessage.Mesaj), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void SUCCESS_CLOSE_SHIFT_MESSAGE()
        {
            XtraMessageBox.Show(CommonData.SUCCESS_CLOSE_SHIFT, nameof(HeaderMessage.Mesaj), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SUCCESS_X_REPORT_MESSAGE()
        {
            XtraMessageBox.Show(CommonData.SUCCESS_X_REPORT, nameof(HeaderMessage.Mesaj), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SUCCESS_LAST_DOCUMENT_MESSAGE()
        {
            XtraMessageBox.Show(CommonData.SUCCESS_LAST_DOCUMENT, nameof(HeaderMessage.Mesaj), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SUCCESS_SALES_MESSAGE()
        {
            XtraMessageBox.Show(CommonData.SUCCESS_SALES, nameof(HeaderMessage.Mesaj), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SUCCESS_ADVANCE_SALES_MESSAGE()
        {
            XtraMessageBox.Show(CommonData.SUCCESS_ADVANCE_SALES, nameof(HeaderMessage.Mesaj), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SUCCESS_RETURN_SALES_MESSAGE()
        {
            XtraMessageBox.Show(CommonData.SUCCESS_RETURN_SALES, nameof(HeaderMessage.Mesaj), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SUCCES_CREDIT_SALES_MESSAGE()
        {
            XtraMessageBox.Show(CommonData.SUCCESS_CREDIT_SALES, nameof(HeaderMessage.Mesaj), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SUCCES_CREDIT_PAYMENT_MESSAGE()
        {
            XtraMessageBox.Show(CommonData.SUCCESS_CREDIT_PAYMENT, nameof(HeaderMessage.Mesaj), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SUCCES_PERİODİC_Z_REPORT_MESSAGE()
        {
            XtraMessageBox.Show(CommonData.SUCCES_PERİODİC_Z_REPORT, nameof(HeaderMessage.Mesaj), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SUCCESS_DEFAULT_MESSAGE(string message)
        {
            XtraMessageBox.Show(message, nameof(HeaderMessage.Mesaj), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion [...SUCCESS MESSAGES...]



        #region [...ERROR MESSAGES...]
        public static void ERROR_BANK_MESSAGE(string message = null)
        {
            XtraMessageBox.Show($"{ErrorMessages.ERROR_BANK}\n\n{message}", nameof(HeaderMessage.Xəta), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ERROR_SERVER_CONNECTION_MESSAGE(string exception = null)
        {
            XtraMessageBox.Show($"{ErrorMessages.ERROR_CONNECTION_SERVER}\n\n{exception}", nameof(HeaderMessage.Xəta), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ERROR_LOGIN_MESSAGE(string exception)
        {
            XtraMessageBox.Show($"{ErrorMessages.ERROR_LOGIN}\n\n{exception}", nameof(HeaderMessage.Xəta), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ERROR_X_REPORT_MESSAGE(string message)
        {
            XtraMessageBox.Show($"{ErrorMessages.ERROR_X_REPORT} \n\n Xəta mesajı: {message}", nameof(HeaderMessage.Xəta), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ERROR_LAST_DOCUMENT_MESSAGE(string message)
        {
            XtraMessageBox.Show(message, nameof(HeaderMessage.Xəta), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ERROR_SALES_MESSAGE(string exception)
        {
            XtraMessageBox.Show($"{ErrorMessages.ERROR_SALES}\n\n{exception}", nameof(HeaderMessage.Xəta), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ERROR_RETURN_SALES_MESSAGE(string exception)
        {
            XtraMessageBox.Show($"{ErrorMessages.RETURN_SALES}\n\n{exception}", nameof(HeaderMessage.Xəta), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ERROR_OPENSHIFT_MESSAGE(string exception)
        {
            XtraMessageBox.Show($"{ErrorMessages.ERROR_OPENSHIFT}\n\n{exception}", nameof(HeaderMessage.Xəta), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ERROR_DATALOAD_MESSAGE(string exception)
        {
            XtraMessageBox.Show($"{exception}", nameof(HeaderMessage.Xəta), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ERROR_DEFAULT_MESSAGE(string exception)
        {
            XtraMessageBox.Show($"{exception}", nameof(HeaderMessage.Xəta), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ERROR_DATETIME_MESSAGE(string exception = null)
        {
            XtraMessageBox.Show($"{ErrorMessages.ERROR_DATETIME}\n\n{exception}", nameof(HeaderMessage.Xəta), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion [...ERROR MESSAGES...]



        #region [...WARNING MESSAGES...]

        public static void WARNING_DEFAULT_MESSAGE(string message)
        {
            XtraMessageBox.Show(message, nameof(HeaderMessage.Bildiriş), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion [...WARNING MESSAGES...]
    }
}
