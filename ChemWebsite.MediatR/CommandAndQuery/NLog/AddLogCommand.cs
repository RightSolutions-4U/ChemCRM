using MediatR;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddLogCommand : IRequest<ServiceResponse<NLogDto>>
    {
        public string ErrorMessage { get; set; }
        public string Stack { get; set; }
    }
}
