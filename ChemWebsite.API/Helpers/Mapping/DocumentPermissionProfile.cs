using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class DocumentPermissionProfile : Profile
    {
        public DocumentPermissionProfile()
        {
            CreateMap<DocumentUserPermission, DocumentUserPermissionDto>().ReverseMap();
            CreateMap<DocumentRolePermission, DocumentRolePermissionDto>().ReverseMap();
            CreateMap<DocumentUserPermission, DocumentPermissionDto>().ReverseMap();
            CreateMap<DocumentRolePermission, DocumentPermissionDto>().ReverseMap();
        }
    }
}
