using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Commands
{
    public class DownloadDocumentCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }
}
