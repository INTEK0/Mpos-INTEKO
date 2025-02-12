using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.Validations;

namespace WindowsFormsApp2
{
    public partial class avtorizasiya : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        bool mouseDown;
        private Point offset;
        string unames, uparol, uemail, uNameSurname;
        SqlConnection Con = new SqlConnection(Properties.Settings.Default.SqlCon);

        public avtorizasiya()
        {
            InitializeComponent();
            textEdit1.Focus();
        }

        private void avtorizasiya_Load(object sender, EventArgs e)
        {
            this.Hide();
            textEdit1.Focus();
            if (Properties.Settings.Default.SaveMe is true)
            {
                string username = Properties.Settings.Default.Username;
                string password = Properties.Settings.Default.Password;
                if (!string.IsNullOrWhiteSpace(username) || !string.IsNullOrWhiteSpace(textEdit2.Text))
                {
                    textEdit1.Text = username;
                    textEdit2.Text = password;
                    kryptonButton2.PerformClick();
                }

            }
        }



        private void textEdit2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                kryptonButton1.PerformClick();

                e.SuppressKeyPress = true;
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Up)
            {
                textEdit1.Select();
            }
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                kryptonButton1.PerformClick();

                e.SuppressKeyPress = true;
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Down)
            {
                textEdit2.Select();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string target = "https://www.instagram.com/inteko.az/";
            System.Diagnostics.Process.Start(target);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string target = "https://api.whatsapp.com/send?phone=994552062366&text=Sual%20v%C9%99%20t%C9%99klifl%C9%99rinizi%20biz%C9%99%20yaz%C4%B1n";
            System.Diagnostics.Process.Start(target);
        }


        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            var user = UserValidation.ValidateUser(textEdit1.Text.Trim(), textEdit2.Text.Trim());
            if (user != null)
            {
                if (user.IsAdmin)
                {
                    //admin kimi daxil ol
                    this.Hide();
                    MAINSCRRENS f2 = new MAINSCRRENS(1);
                    f2.Show();
                }
                else
                {
                    // istifadeci kimi daxil ol
                    this.Hide();
                    MAINSCRRENS f2 = new MAINSCRRENS(0);
                    f2.Show();
                }
                Properties.Settings.Default.UserID = user.Id;
                Properties.Settings.Default.Save();
                FormHelpers.Log("Sistemə daxil oldu");
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE("İstifadəçi adı vəya şifrə səhvdir");
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lForgetPassword_Click(object sender, EventArgs e)
        {
            PASSWORDEMAIL PE = new PASSWORDEMAIL();
            PE.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string target = "https://www.facebook.com/inteko.az/";
            System.Diagnostics.Process.Start(target);
        }
    }
}