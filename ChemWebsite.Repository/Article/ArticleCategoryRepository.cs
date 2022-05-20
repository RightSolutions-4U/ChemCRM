using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public class ArticleCategoryRepository : GenericRepository<ArticleCategory, ChemWebsiteDbContext>, IArticleCategoryRepository
    {
        public ArticleCategoryRepository(IUnitOfWork<ChemWebsiteDbContext> uow)
            : base(uow)
        {
        }
    }
}
