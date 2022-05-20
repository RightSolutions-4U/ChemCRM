using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteExpenseCategoryCommandHandler
        : IRequestHandler<DeleteExpenseCategoryCommand, ServiceResponse<bool>>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<DeleteExpenseCategoryCommandHandler> _logger;
        public DeleteExpenseCategoryCommandHandler(
           IExpenseCategoryRepository expenseCategoryRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<DeleteExpenseCategoryCommandHandler> logger
            )
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _expenseCategoryRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (existingEntity == null)
            {
                _logger.LogError("Expense Category does not Exist");
                return ServiceResponse<bool>.Return409("Expense Category does not  Exist.");
            }
            _expenseCategoryRepository.Delete(existingEntity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error while saving Expense Category.");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
