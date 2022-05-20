using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{

    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, ServiceResponse<ArticleDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ILogger<DeleteArticleCommandHandler> _logger;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;

        public DeleteArticleCommandHandler(IArticleRepository articleRepository,
            ILogger<DeleteArticleCommandHandler> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow)
        {
            _articleRepository = articleRepository;
            _logger = logger;
            _uow = uow;
        }

        public async Task<ServiceResponse<ArticleDto>> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.FindAsync(request.Id);

            if (article == null)
            {
                _logger.LogError("Article not found.", request);
                return ServiceResponse<ArticleDto>.Return404("Article not found.");
            }

            article.IsDeleted = true;
            _articleRepository.Update(article);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error to Deleting Article.");
                return ServiceResponse<ArticleDto>.Return500();
            }
            return ServiceResponse<ArticleDto>.ReturnSuccess();
        }
    }
}
