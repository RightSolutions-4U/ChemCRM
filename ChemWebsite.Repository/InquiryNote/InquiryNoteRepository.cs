using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Entities;
using ChemWebsite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
   public class InquiryNoteRepository : GenericRepository<InquiryNote, ChemWebsiteDbContext>, IInquiryNoteRepository
    {
        public InquiryNoteRepository(IUnitOfWork<ChemWebsiteDbContext> uow) : base(uow)
        {
        }
    }
}
