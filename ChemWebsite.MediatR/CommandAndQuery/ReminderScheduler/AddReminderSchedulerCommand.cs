using ChemWebsite.Data.Entities;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddReminderSchedulerCommand : IRequest<ServiceResponse<bool>>
    {
        public ApplicationEnums Application { get; set; }
        public Guid ReferenceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Guid> UserIds { get; set; } = new List<Guid>();
        public bool IsEmailNotification { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
