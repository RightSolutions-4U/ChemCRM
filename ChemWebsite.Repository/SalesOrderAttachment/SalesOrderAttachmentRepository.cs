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
    public class SalesOrderAttachmentRepository 
        : GenericRepository<SalesOrderAttachment, ChemWebsiteDbContext>, ISalesOrderAttachmentRepository
    {
        public SalesOrderAttachmentRepository(IUnitOfWork<ChemWebsiteDbContext> uow) : base(uow)
        {
        }
    }
}
