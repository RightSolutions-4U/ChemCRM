using ChemWebsite.Data.Resources;
using ChemWebsite.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllReminderQuery : IRequest<ReminderList>
    {
        public ReminderResource ReminderResource { get; set; }
    }
}
