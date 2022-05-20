using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using ChemWebsite.MediatR.Commands;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class DocumentCategoryProfile : Profile
    {
        public DocumentCategoryProfile()
        {
            CreateMap<DocumentCategory, DocumentCategoryDto>().ReverseMap();
            CreateMap<AddDocumentCategoryCommand, DocumentCategory>().ReverseMap();
            CreateMap<UpdateDocumentCategoryCommand, DocumentCategory>().ReverseMap();
        }
    }
}
