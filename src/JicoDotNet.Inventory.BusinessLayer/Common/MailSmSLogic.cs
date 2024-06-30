using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;
using JicoDotNet.Inventory.BusinessLayer.DTO.Core;
using DataAccess.AzureStorage;
using JicoDotNet.Inventory.BusinessLayer.DTO;
using System.Web.Configuration;
using RestSharp;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;

namespace JicoDotNet.Inventory.BusinessLayer.Common
{
    public class MailLogic
    {
        private readonly static string baseUrl = "https://itemblobmailapi.azurewebsites.net/Mail/Send";
        private static bool willMail = true;

        public static async Task MailApi(string toEmail, string mailSubject, string mailbody, string requestId)
        {
            if (willMail)
            {
                try
                {
                    RestClient restClient = new RestClient(baseUrl);
                    restClient.Timeout = -1;
                    RestRequest request = new RestRequest(Method.POST);
                    request.AddJsonBody(new { ToMail = toEmail, MailSubject = mailSubject, MailBody = mailbody, RequestId = requestId });
                    Task.Run(() => { restClient.Execute(request); });
                    return;
                }
                catch
                {
                    return;
                }
            }
            return;
        }
    }
}
