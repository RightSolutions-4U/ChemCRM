using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetInquiryChemicalsQueryHandler : IRequestHandler<GetInquiryChemicalsQuery, List<ChemicalSupplierCountDto>>
    {
        private readonly IInquiryChemicalRepository _inquiryChemicalRepository;

        public GetInquiryChemicalsQueryHandler(IInquiryChemicalRepository inquiryChemicalRepository)
        {
            _inquiryChemicalRepository = inquiryChemicalRepository;
        }
        public async Task<List<ChemicalSupplierCountDto>> Handle(GetInquiryChemicalsQuery request, CancellationToken cancellationToken)
        {
            var chemicals = await _inquiryChemicalRepository
                .AllIncluding(c => c.Chemical, cs => cs.Chemical.ChemicalSuppliers)
                .Where(c => c.InquiryId == request.Id)
                .Select(c => new ChemicalSupplierCountDto
                {
                    CasNumber = c.Chemical.CasNumber,
                    Name = c.Chemical.Name,
                    Id = c.Chemical.Id,
                    SupplierCount = c.Chemical.ChemicalSuppliers.Count()
                }).ToListAsync();
            return chemicals;
        }
    }
}
