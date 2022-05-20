using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteExpenseCommandHanlder 
        : IRequestHandler<DeleteExpenseCommand, ServiceResponse<bool>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ILogger<DeleteExpenseCommandHanlder> _logger;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        public DeleteExpenseCommandHanlder(IExpenseRepository expenseRepository,
            ILogger<DeleteExpenseCommandHanlder> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow)
        {
            _expenseRepository = expenseRepository;
            _logger = logger;
            _uow = uow;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _expenseRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Expense does not exists.");
                return ServiceResponse<bool>.Return404();
            }

            entityExist.IsDeleted = true;
            _expenseRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while saving Expense.");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith204();
        }
    }
}
