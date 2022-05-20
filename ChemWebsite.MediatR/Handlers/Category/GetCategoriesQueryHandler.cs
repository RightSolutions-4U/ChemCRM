using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handler
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var skip = request.PageSize * (request.PageNumber - 1);
            var categories = await _categoryRepository.All.
              OrderBy(c => c.Name)
              .Skip(skip)
              .Take(request.PageSize)
              .ToListAsync();

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
            return categoriesDto;
        }
    }
}
