using ChemWebsite.Data.Resources;
using ChemWebsite.Repository;
using MediatR;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllArticleQuery : IRequest<ArticleList>
    {
        public ArticleResource ArticleResource { get; set; }
    }
}
