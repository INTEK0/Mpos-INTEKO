using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Licence.Entities;
using Licence.Helpers;
using Newtonsoft.Json;

namespace Licence.Operations
{
    public class LicenceOperation
    {
        

        public static async Task<User> LicenceStatusControl(string key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(key) || key is "Yoxdur")
                {
                    return null;
                }

                ApiHandler api = new ApiHandler()
                {
                    Url = $"https://intekoservice-default-rtdb.firebaseio.com/Users/{key}.json",
                    Method = HttpMethod.Get,
                };

                var response = await api.SendRequest();
                if (response.StatusCode is System.Net.HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(responseBody))
                        return null;
                    else
                    {
                        User user = System.Text.Json.JsonSerializer.Deserialize<User>(responseBody);

                        if (user == null || user.IsActive is false)
                        {
                            //Lisenziya deaktiv ekranı
                        }
                        
                        bool expireDateControl = LicenceExpireDateControl(user);

                        if (!expireDateControl)
                        {
                            //Lisenziya deaktiv ekranı
                        }

                        return user;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private static bool LicenceExpireDateControl(User user)
        {
            DateTime currentDate = DateTime.Now.Date;
            if (currentDate == user.LicenceExpireDate.Date)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
