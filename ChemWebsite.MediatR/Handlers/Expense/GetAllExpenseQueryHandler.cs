using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR
{
    public class GetAllExpenseQueryHandler : IRequestHandler<GetAllExpenseQuery, ExpenseList>
    {

        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public GetAllExpenseQueryHandler(
            IExpenseRepository expenseRepository,
            IMapper mapper,
            IPropertyMappingService propertyMappingService)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<ExpenseList> Handle(GetAllExpenseQuery request, CancellationToken cancellationToken)
        {
            return await _expenseRepository.GetExpenses(request.ExpenseResource);
        }
    }
}
