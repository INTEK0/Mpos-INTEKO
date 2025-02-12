using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApp2.Validations
{
    public class UserValidation : AbstractValidator<DatabaseClasses.User>
    {
        public UserValidation()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("İstifadəçi adını daxil edin");
            RuleFor(x => x.Password).NotEmpty().WithMessage("İstifadəçi parolunu daxil edin");
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Ad və Soyadı daxil edin");
            //RuleFor(x => x.Email).NotEmpty().WithMessage("Elektron poçt ünvanını daxil edin");
            //RuleFor(x => x.Email).EmailAddress().WithMessage("Elekton poçt ünvanı düzgün daxil edilmədi");
        }

        public static DatabaseClasses.User ValidateUser(string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("ValidateUser", connection))
                    {
                        cmd.Parameters.AddWithValue("username", username);
                        cmd.Parameters.AddWithValue("password", password);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                var user = FormHelpers.MapReaderToObject<DatabaseClasses.User>(dr);
                                return user;
                            }
                            else
                            {
                                return null;
                            }
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

        public static bool ExistsUser(string username)
        {
            bool exists = false;

            try
            {
                string query = $"SELECT COUNT(1) FROM userParol WHERE Ulogin = @username";
                using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    connection.Open();
                    
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        int count = (int)cmd.ExecuteScalar();
                        exists = count > 0;

                        if (exists)
                        {
                            FormHelpers.Alert("İstifadəçi adı sistemdə mövcuddur", Enums.MessageType.Warning);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }

            return exists;
        }
    }
}