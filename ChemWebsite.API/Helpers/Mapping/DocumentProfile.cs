using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using ChemWebsite.MediatR.Commands;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, DocumentDto>().ReverseMap();
            CreateMap<AddDocumentCommand, Document>();
            CreateMap<UpdateDocumentCommand, Document>();
        }
    }
}
