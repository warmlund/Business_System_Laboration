using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Business_System_Laboration_4
{
    public static class ApiHandler
    {
        public static async Task<string> SyncWithWebAPI()
        {
            string responseData = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://hex.cse.kau.se/~jonavest/csharp-api/");
                response.EnsureSuccessStatusCode();
                
                if(response.IsSuccessStatusCode)
                {
                   responseData = await response.Content.ReadAsStringAsync();
                }

                else
                {
                    MessageBox.Show($"Serverfel:{response.StatusCode}", "Fel");
                }
            }

            return responseData;
        }

    }
}
