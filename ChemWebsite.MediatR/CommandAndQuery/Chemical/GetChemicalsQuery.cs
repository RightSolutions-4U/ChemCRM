using ChemWebsite.Data;
using ChemWebsite.Repository;
using MediatR;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetChemicalsQuery : IRequest<ChemicalList>
    {
        public ChemicalResource ChemicalResource { get; set; }
    }
}
