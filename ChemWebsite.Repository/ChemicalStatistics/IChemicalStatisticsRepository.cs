using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data;
using System;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public interface IChemicalStatisticsRepository : IGenericRepository<ChemicalStatistics>
    {
        Task IncreseMostSearchedChemical(Guid id);
        Task IncreseMostViewedChemical(Guid id);
    }
}
