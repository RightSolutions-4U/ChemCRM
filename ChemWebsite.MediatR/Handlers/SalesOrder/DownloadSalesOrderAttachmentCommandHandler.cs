using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DownloadSalesOrderAttachmentCommandHandler
        : IRequestHandler<DownloadSalesOrderAttachmentCommand, string>
    {
        private readonly ISalesOrderAttachmentRepository _salesOrderAttachmentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;

        public DownloadSalesOrderAttachmentCommandHandler(
            ISalesOrderAttachmentRepository salesOrderAttachmentRepository,
            IWebHostEnvironment webHostEnvironment,
            PathHelper pathHelper)
        {
            _salesOrderAttachmentRepository = salesOrderAttachmentRepository;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
        }

        public async Task<string> Handle(DownloadSalesOrderAttachmentCommand request, CancellationToken cancellationToken)
        {
            var attachement = await _salesOrderAttachmentRepository.FindAsync(request.Id);
            if (attachement == null)
            {
                return "";
            }
            string contentRootPath = _webHostEnvironment.WebRootPath;
            return Path.Combine(contentRootPath, _pathHelper.Attachments, attachement.Path);
        }
    }
}
