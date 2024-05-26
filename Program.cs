using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace DuoApiUserFetcher
{
    class Program
    {
        private static readonly string ikey = "DI8ZNUUX03BY7DJSEKM4";
        private static readonly string skey = "EBQHkENPL0oitZKtmYHGKAdAgR5Ie8uuYGDJDljz";
        private static readonly string host = "api-ff7def68.duosecurity.com";

        static async Task Main(string[] args)
        {
            var users = await GetDuoUsersAsync();
            WriteUsersToCsv(users, "DuoUsers.csv");
        }

        private static async Task<JArray> GetDuoUsersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string method = "GET";
                string path = "/admin/v1/users";
                string date = DateTime.UtcNow.ToString("r", CultureInfo.InvariantCulture);
                string canonical = $"{date}\n{method}\n{host}\n{path}\n";
                string signature = CreateSignature(canonical);

                string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ikey}:{signature}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
                client.DefaultRequestHeaders.Date = DateTimeOffset.UtcNow;

                HttpResponseMessage response = await client.GetAsync($"https://{host}{path}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseBody);
                return (JArray)jsonResponse["response"];
            }
        }

        private static string CreateSignature(string canonical)
        {
            using (var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(skey)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(canonical));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        private static void WriteUsersToCsv(JArray users, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("user_id,username,email");
                foreach (var user in users)
                {
                    writer.WriteLine($"{user["user_id"]},{user["username"]},{user["email"]}");
                }
            }
        }
    }
}
