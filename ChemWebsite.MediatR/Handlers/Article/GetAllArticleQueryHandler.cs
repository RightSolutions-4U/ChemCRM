using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAllArticleQueryHandler : IRequestHandler<GetAllArticleQuery, ArticleList>
    {
        private readonly IArticleRepository _articleRepository;

        public GetAllArticleQueryHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<ArticleList> Handle(GetAllArticleQuery request, CancellationToken cancellationToken)
        {
            return await _articleRepository.GetAllArticle(request.ArticleResource);
        }
    }
}
