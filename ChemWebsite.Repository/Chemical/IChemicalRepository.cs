using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public interface IChemicalRepository : IGenericRepository<Chemical>
    {
        Task<ChemicalList> GetChemicals(ChemicalResource chemicalResource);
    }
}
