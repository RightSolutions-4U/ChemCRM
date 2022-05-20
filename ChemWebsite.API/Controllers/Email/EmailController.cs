using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChemWebsite.MediatR.CommandAndQuery;
using Microsoft.AspNetCore.Authorization;
using ChemWebsite.MediatR.CommandAndQuery.Email;
using GmailTest.Models;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;


namespace ChemWebsite.API.Controllers.Email
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailController : BaseController
    {
        IMediator _mediator;
        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Send mail.
        /// </summary>
        /// <param name="sendEmailCommand"></param>
        /// <returns></returns>
        [HttpPost(Name = "SendEmail")]
        [Produces("application/json", "application/xml", Type = typeof(void))]
        public async Task<IActionResult> SendEmail(SendEmailCommand sendEmailCommand)
        {
            var result = await _mediator.Send(sendEmailCommand);
            return ReturnFormattedResponse(result);
        }
        [HttpGet(Name = "GetEmails")]
        //[Produces("application/json", "application/xml", Type = typeof(void))]
        [Obsolete]
        public ActionResult<IEnumerable<GetEmailsCommand>> GetEmails()
        {
            //var result = await _mediator.Send(sendEmailCommand);
            GmailService GmailService = GmailAPIHelper.GetService();
            List<GetEmailsCommand> EmailList = new List<GetEmailsCommand>();
            UsersResource.MessagesResource.ListRequest listRequest = GmailService.Users.Messages.List("r4ubirds@gmail.com");
            listRequest.LabelIds = "INBOX";
            listRequest.IncludeSpamTrash = false;
            /*listRequest.q Q = "is:unread";*/

            ListMessagesResponse ListResponse = listRequest.Execute();
            if (listRequest != null && ListResponse.Messages != null)
            {
                foreach (Message Msg in ListResponse.Messages)
                {
                    UsersResource.MessagesResource.GetRequest Message = GmailService.Users.Messages.Get("r4ubirds@gmail.com", Msg.Id);
                    Message MsgContent = Message.Execute();
                    if (MsgContent != null)
                    {
                        string FromAddress = string.Empty;
                        string Date = string.Empty;
                        string Subject = string.Empty;
                        string MailBody = string.Empty;
                        string ReadableText = string.Empty;
                        foreach (var MessageParts in MsgContent.Payload.Headers)
                        {
                            if (MessageParts.Name == "From")
                            {
                                FromAddress = MessageParts.Value;
                            }
                            if (MessageParts.Name == "Date")
                            {
                                Date = MessageParts.Value;
                            }
                            if (MessageParts.Name == "Subject")
                            {
                                Subject = MessageParts.Value;
                            }
                            //if (MsgContent.Payload.Parts == null && MsgContent.Payload.Body!=null)
                            if (MsgContent.Payload.Body != null)
                            {
                                MailBody = MsgContent.Payload.Body.Data;
                            }
                            ReadableText = string.Empty;
                            ReadableText = GmailAPIHelper.Base64Decode(MailBody);
                            if (string.IsNullOrEmpty(ReadableText))
                            {
                                ReadableText = "No Body";
                            }
                           
                            GetEmailsCommand Gmail = new GetEmailsCommand();
                            Gmail.From = FromAddress;
                            Gmail.Subject = Subject;
                            Gmail.Body = ReadableText;
                            if (Date != "")
                            {
                                /*DateTime dt = DateTime.ParseExact(Date, "yyyy-MM-ddTHH:mm:ss.fffffff",
                                CultureInfo.InvariantCulture);
                                dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);*/
                                Date = "Wed, 18 May 2022 05:02:48";
                                Gmail.MailDateTime = DateTime.Parse(Date);
                                //Gmail.MailDateTime = dt;
                                //}
                                EmailList.Add(Gmail);
                            }

                        }

                    }
                }
            }

            return EmailList;
        }
    }
}
