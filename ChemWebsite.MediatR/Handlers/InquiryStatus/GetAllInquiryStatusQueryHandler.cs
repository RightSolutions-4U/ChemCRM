using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAllInquiryStatusQueryHandler : IRequestHandler<GetAllInquiryStatusQuery, List<InquiryStatusDto>>
    {
        private readonly IInquiryStatusRepository _inquiryStatusRepository;
        private readonly IMapper _mapper;
        public GetAllInquiryStatusQueryHandler(
            IInquiryStatusRepository inquiryStatusRepository,
            IMapper mapper)
        {
            _inquiryStatusRepository = inquiryStatusRepository;
            _mapper = mapper;
        }

        public async Task<List<InquiryStatusDto>> Handle(GetAllInquiryStatusQuery request, CancellationToken cancellationToken)
        {
            var inquiries = await _inquiryStatusRepository.All.ToListAsync();
            return _mapper.Map<List<InquiryStatusDto>>(inquiries);
        }
    }
}
