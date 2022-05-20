using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetChemicalByNameQueryHandler : IRequestHandler<GetChemicalByNameQuery, ServiceResponse<ChemicalDto>>
    {
        private readonly IChemicalRepository _chemicalRepository;
        private readonly IMapper _mapper;
        public GetChemicalByNameQueryHandler(IChemicalRepository chemicalRepository,
            IMapper mapper)
        {
            _chemicalRepository = chemicalRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ChemicalDto>> Handle(GetChemicalByNameQuery request, CancellationToken cancellationToken)
        {
            var chemical = await _chemicalRepository.AllIncluding(c => c.ChemicalCategories, cs => cs.ChemicalIndustries)
                .FirstOrDefaultAsync(c => c.Name == request.Name);
            if (chemical == null)
            {
                return ServiceResponse<ChemicalDto>.Return404();
            }
            return ServiceResponse<ChemicalDto>.ReturnResultWith200(_mapper.Map<ChemicalDto>(chemical));
        }
    }
}
