using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, ServiceResponse<ArticleDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetArticleQueryHandler> _logger;
        private readonly PathHelper _pathHelper;

        public GetArticleQueryHandler(IArticleRepository articleRepository,
            IMapper mapper,
            ILogger<GetArticleQueryHandler> logger,
            PathHelper pathHelper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _logger = logger;
            _pathHelper = pathHelper;
        }

        public async Task<ServiceResponse<ArticleDto>> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.FindAsync(request.Id);
            if (article == null)
            {
                _logger.LogError("Article Not found", request);
                return ServiceResponse<ArticleDto>.Return404("Article Not found");
            }
            if (!string.IsNullOrWhiteSpace(article.BannerUrl))
            {
                article.BannerUrl = Path.Combine(_pathHelper.ArticleBannerImagePath, article.BannerUrl);
            }
            return ServiceResponse<ArticleDto>.ReturnResultWith200(_mapper.Map<ArticleDto>(article));
        }
    }
}
