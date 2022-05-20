using ChemWebsite.Data;
using ChemWebsite.Data.Resources;
using ChemWebsite.Helper;
using ChemWebsite.Repository;
using MediatR;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllExpenseQuery : IRequest<ExpenseList>
    {
        public ExpenseResource ExpenseResource { get; set; }
    }
}
