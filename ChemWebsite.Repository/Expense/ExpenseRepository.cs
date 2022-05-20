using AutoMapper;
using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Resources;
using ChemWebsite.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public class ExpenseRepository : GenericRepository<Expense, ChemWebsiteDbContext>,
            IExpenseRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMapper _mapper;
        public ExpenseRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IPropertyMappingService propertyMappingService,
            IMapper mapper
            ) : base(uow)
        {
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
        }

        public async Task<ExpenseList> GetExpenses(ExpenseResource expenseResource)
        {
            var collectionBeforePaging = AllIncluding(c => c.ExpenseBy, cs => cs.ExpenseCategory).ApplySort(expenseResource.OrderBy,
                _propertyMappingService.GetPropertyMapping<ExpenseDto, Expense>());

            if (!string.IsNullOrEmpty(expenseResource.Reference))
            {
                // trim & ignore casing
                var referenceWhereClause = expenseResource.Reference
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Reference, $"{referenceWhereClause}%"));
            }
            if (!string.IsNullOrEmpty(expenseResource.Description))
            {
                // trim & ignore casing
                var descriptionWhereClause = expenseResource.Description
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Description, $"{descriptionWhereClause}%"));
            }

            if (expenseResource.ExpenseCategoryId.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.ExpenseCategoryId == expenseResource.ExpenseCategoryId);
            }

            if (expenseResource.ExpenseById.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.ExpenseById == expenseResource.ExpenseById);
            }

            return await new ExpenseList(_mapper).Create(collectionBeforePaging,
                expenseResource.Skip,
                expenseResource.PageSize);
        }
    }
}