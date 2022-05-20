using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GmailTest.Models
{
    public static class GmailAPIHelper
    {
        private static string[] Scopes = { GmailService.Scope.MailGoogleCom };
        static string ApplicationName = "ChemCRM";

        [Obsolete]
        public static GmailService GetService()
        {
            UserCredential credential;
            using (var stream =
               new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }
        public static string Base64Decode(string Body)
        {
            if (!string.IsNullOrEmpty(Body))
            {
                string EncodTxt = string.Empty;
                EncodTxt = Body.Replace("-", "+");
                EncodTxt = Body.Replace("_", "/");
                EncodTxt = Body.Replace(" ", "+");
                EncodTxt = Body.Replace("=", "+");

                if (EncodTxt.Length % 4 > 0)
                {
                    EncodTxt += new string('=', 4 - EncodTxt.Length % 4);
                }
                else if (EncodTxt.Length % 4 > 0)
                {
                    EncodTxt = EncodTxt.Substring(0, EncodTxt.Length - 1);
                    if (EncodTxt.Length % 4 > 0)
                    {
                        EncodTxt += new string('=', 4 - EncodTxt.Length % 4);
                    }
                }
                EncodTxt = EncodTxt.Replace("-", "+");
                EncodTxt = EncodTxt.Replace("-", "+");
                EncodTxt = EncodTxt.Replace("_", "/");
                EncodTxt = EncodTxt.Replace(" ", "+");
                EncodTxt = EncodTxt.Replace("=", "+");
                byte[] ByteArray = Convert.FromBase64String(EncodTxt);
                return Encoding.UTF8.GetString(ByteArray);
            }
            return null;



            }

    }
}
