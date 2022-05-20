using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddChemicalTypeCommand : IRequest<ServiceResponse<ChemicalTypeDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
