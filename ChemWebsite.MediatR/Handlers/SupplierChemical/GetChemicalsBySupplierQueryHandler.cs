using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers.SupplierChemical
{
    public class GetChemicalsBySupplierQueryHandler : IRequestHandler<GetChemicalsBySupplierQuery, ChemicalListDto>
    {
        private readonly ISupplierChemicalRepository _supplierChemicalRepository;
        private readonly IMapper _mapper;

        public GetChemicalsBySupplierQueryHandler(
            ISupplierChemicalRepository supplierChemicalRepository,
            IMapper mapper)
        {
            _supplierChemicalRepository = supplierChemicalRepository;
            _mapper = mapper;
        }
        public async Task<ChemicalListDto> Handle(GetChemicalsBySupplierQuery request, CancellationToken cancellationToken)
        {
            var chemicalsQuery = _supplierChemicalRepository.AllIncluding(c => c.Chemical)
                .OrderBy(c => c.Chemical.Name)
                .Where(c => c.SupplierId == request.Id);

            if (!string.IsNullOrWhiteSpace(request.ChemicalName))
            {
                var genreForWhereClause = request.ChemicalName.Trim().ToLowerInvariant();
                var name = Uri.UnescapeDataString(genreForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                chemicalsQuery = chemicalsQuery.Where(a => EF.Functions.Like(a.Chemical.Name, $"%{encodingName}%", @"\"));
            }

            if (!string.IsNullOrWhiteSpace(request.CasNumber))
            {
                // trim & ignore casing
                var genreForWhereClause = request.CasNumber
                    .Trim().ToLowerInvariant();
                chemicalsQuery = chemicalsQuery
                    .Where(a => EF.Functions.Like(a.Chemical.CasNumber, $"{genreForWhereClause}%"));
            }

            var chemicals = await chemicalsQuery
                .Select(c => c.Chemical)
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync();
            var chemicalsDtos = _mapper.Map<List<ChemicalDto>>(chemicals);
            var result = new ChemicalListDto
            {
                Chemicals = chemicalsDtos,
                TotalCount = chemicalsQuery.Count()
            };
            return result;
        }
    }
}
