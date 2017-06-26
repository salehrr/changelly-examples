using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Application
{
    public class ChangellyExample
    {
        private static readonly WebClient Client = new WebClient();
        private static readonly Encoding U8 = Encoding.UTF8;
        private static readonly string apiKey = "place_your_api_key_here";
        private static readonly string apiSecret = "place_your_api_secret_here";
        private static readonly string apiUrl = "https://api.changelly.com";

        public static string ToHexString(byte[] array)
        {
            StringBuilder hex = new StringBuilder(array.Length * 2);
            foreach (byte b in array)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        private static void Main(string[] args)
        {
            string message = @"{
		        ""jsonrpc"": ""2.0"",
		        ""id"": 1,
		        ""method"": ""getCurrencies"",
		        ""params"": []
			}";

            HMACSHA512 hmac = new HMACSHA512(U8.GetBytes(apiSecret));
            byte[] hashmessage = hmac.ComputeHash(U8.GetBytes(message));
            string sign = ToHexString(hashmessage);

            Client.Headers.Set("Content-Type", "application/json");
            Client.Headers.Add("api-key", apiKey);
            Client.Headers.Add("sign", sign);

            string result = Client.UploadString(apiUrl, message);
            Console.WriteLine(result);
        }
    }
}