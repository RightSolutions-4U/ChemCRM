using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAllArticleCategoriesQueryHandler : IRequestHandler<GetAllArticleCategoriesQuery, List<ArticleCategoryDto>>
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IMapper _mapper;
        public GetAllArticleCategoriesQueryHandler(IArticleCategoryRepository articleCategoryRepository,
            IMapper mapper)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _mapper = mapper;
        }
        public async Task<List<ArticleCategoryDto>> Handle(GetAllArticleCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _articleCategoryRepository.All.ToListAsync();
            return _mapper.Map<List<ArticleCategoryDto>>(categories);
        }
    }
}
