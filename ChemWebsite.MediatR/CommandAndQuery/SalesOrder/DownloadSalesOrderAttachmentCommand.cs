﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DownloadSalesOrderAttachmentCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }
}
