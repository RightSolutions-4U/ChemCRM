using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public class ChemicalStatisticsRepository : GenericRepository<ChemicalStatistics, ChemWebsiteDbContext>, IChemicalStatisticsRepository
    {
        public ChemicalStatisticsRepository(IUnitOfWork<ChemWebsiteDbContext> uow)
          : base(uow)
        {
        }

        public async Task IncreseMostSearchedChemical(Guid id)
        {
            var chemicalStatastic = await All.FirstOrDefaultAsync(c => c.ChemicalId == id);
            if (chemicalStatastic != null)
            {
                chemicalStatastic.TotalSearch = chemicalStatastic.TotalSearch + 1;
                Update(chemicalStatastic);
            }
            else
            {
                Add(new ChemicalStatistics
                {
                    Id = Guid.NewGuid(),
                    ChemicalId = id,
                    TotalSearch = 1,
                    TotalView = 0
                });
            }
            await _uow.SaveAsync();
        }

        public async Task IncreseMostViewedChemical(Guid id)
        {
            var chemicalStatastic = await All.FirstOrDefaultAsync(c => c.ChemicalId == id);
            if (chemicalStatastic != null)
            {
                chemicalStatastic.TotalView = chemicalStatastic.TotalView + 1;
                Update(chemicalStatastic);
            }
            else
            {
                Add(new ChemicalStatistics
                {
                    Id = Guid.NewGuid(),
                    ChemicalId = id,
                    TotalSearch = 0,
                    TotalView = 1
                });
            }
            await _uow.SaveAsync();
        }
    }
}
