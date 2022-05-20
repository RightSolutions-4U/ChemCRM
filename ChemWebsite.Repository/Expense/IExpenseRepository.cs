using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data;
using ChemWebsite.Data.Resources;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        Task<ExpenseList> GetExpenses(ExpenseResource expenseResource);
    }
}
