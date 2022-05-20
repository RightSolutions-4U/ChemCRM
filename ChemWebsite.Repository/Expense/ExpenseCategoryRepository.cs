using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
   public class ExpenseCategoryRepository : GenericRepository<ExpenseCategory, ChemWebsiteDbContext>,
          IExpenseCategoryRepository
    {
        public ExpenseCategoryRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {
        }
    }
}