using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Domain;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DbMigrationCommandHandler : IRequestHandler<DbMigrationCommand, bool>
    {
       
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        public DbMigrationCommandHandler(
            IUnitOfWork<ChemWebsiteDbContext> uow
            )
        {
            _uow = uow;
        }
        public async Task<bool> Handle(DbMigrationCommand request, CancellationToken cancellationToken)
        {
            _uow.Context.Database.Migrate();
            return true;
        }
    }
}
