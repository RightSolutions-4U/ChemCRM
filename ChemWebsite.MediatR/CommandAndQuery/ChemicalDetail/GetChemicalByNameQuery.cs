using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetChemicalByNameQuery : IRequest<ServiceResponse<ChemicalDto>>
    {
        public string Name { get; set; }
    }
}
