using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using ChemWebsite.MediatR.Commands;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class DocumentAuditTrailProfile : Profile
    {
        public DocumentAuditTrailProfile()
        {
            CreateMap<DocumentAuditTrailDto, DocumentAuditTrail>().ReverseMap();
            CreateMap<AddDocumentAuditTrailCommand, DocumentAuditTrail>();
        }
    }
}
