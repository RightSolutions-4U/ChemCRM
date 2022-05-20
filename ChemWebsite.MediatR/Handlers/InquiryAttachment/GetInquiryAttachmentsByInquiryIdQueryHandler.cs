using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetInquiryAttachmentsByInquiryIdQueryHandler : IRequestHandler<GetInquiryAttachmentsByInquiryIdQuery, List<InquiryAttachmentDto>>
    {

        private readonly IInquiryAttachmentRepository _inquiryAttachmentRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly PathHelper _pathHelper;

        public GetInquiryAttachmentsByInquiryIdQueryHandler(
            IInquiryAttachmentRepository inquiryAttachmentRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
            PathHelper pathHelper
          )
        {
            _inquiryAttachmentRepository = inquiryAttachmentRepository;
            _uow = uow;
            _mapper = mapper;
            _pathHelper = pathHelper;
        }

        public async Task<List<InquiryAttachmentDto>> Handle(GetInquiryAttachmentsByInquiryIdQuery request, CancellationToken cancellationToken)
        {
            var inquiryAttachments = await _inquiryAttachmentRepository.All.Include(c => c.CreatedByUser)
                .Where(c => c.InquiryId == request.InquiryId)
                .ToListAsync();

            var inquiryAttachmentsDto = _mapper.Map<List<InquiryAttachmentDto>>(inquiryAttachments);

            return inquiryAttachmentsDto;
        }
    }
}
