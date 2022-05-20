using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.PurchaseOrder
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PurchaseOrderAttachmentController : ControllerBase
    {
        private IMediator _mediator;
        public PurchaseOrderAttachmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Download File
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            var commnad = new DownloadPurchaseOrderAttachmentCommand
            {
                Id = id,
            };
            var path = await _mediator.Send(commnad);

            if (string.IsNullOrWhiteSpace(path) || !System.IO.File.Exists(path))
                return NotFound("File not found.");

            byte[] newBytes;
            await using (var stream = new FileStream(path, FileMode.Open))
            {
                byte[] bytes = new byte[stream.Length];
                int numBytesToRead = (int)stream.Length;
                int numBytesRead = 0;
                while (numBytesToRead > 0)
                {
                    // Read may return anything from 0 to numBytesToRead.
                    int n = stream.Read(bytes, numBytesRead, numBytesToRead);

                    // Break when the end of the file is reached.
                    if (n == 0)
                        break;

                    numBytesRead += n;
                    numBytesToRead -= n;
                }
                newBytes = bytes;
            }
            return File(newBytes, GetContentType(path), path);
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
