using MediatR;
using System.Collections.Generic;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery.Email
{

    public class GetEmailsCommand : IRequest<ServiceResponse<MessagesDto>>
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime MailDateTime { get; set; }
        public List<FileInfo> Attachments { get; set; } = new List<FileInfo>();
        public string MsgID { get; set; }
    }
}
