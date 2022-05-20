using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Resources;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public class ArticleRepository : GenericRepository<Article, ChemWebsiteDbContext>, IArticleRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly PathHelper _pathHelper;

        public ArticleRepository(IUnitOfWork<ChemWebsiteDbContext> uow,
            IPropertyMappingService propertyMappingService,
            PathHelper pathHelper)
            : base(uow)
        {
            _propertyMappingService = propertyMappingService;
            _pathHelper = pathHelper;
        }

        public async Task<ArticleList> GetAllArticle(ArticleResource articleResource)
        {
            var collectionBeforePaging = All;
            collectionBeforePaging =
               collectionBeforePaging.ApplySort(articleResource.OrderBy,
               _propertyMappingService.GetPropertyMapping<ArticleDto, Article>());

            if (!string.IsNullOrWhiteSpace(articleResource.Title))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => EF.Functions.Like(c.Title, $"{articleResource.Title}%"));
            }

            if (!string.IsNullOrWhiteSpace(articleResource.ShortDescription))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => EF.Functions.Like(c.Title, $"%{articleResource.ShortDescription}%"));
            }

            var articles = new ArticleList(_pathHelper);
            return await articles.Create(
                collectionBeforePaging,
                articleResource.Skip,
                articleResource.PageSize
                );
        }
    }
}
