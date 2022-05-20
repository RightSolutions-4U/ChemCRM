using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Data.Dto
{
    public class MessagesDto
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime MailDateTime { get; set; }
        public List<string> Attachments { get; set; }
        public string MsgID { get; set; }
    }
}
