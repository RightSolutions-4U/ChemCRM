using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using System;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class PackagingTypeProfile : Profile
    {
        public PackagingTypeProfile()
        {
            CreateMap<PackagingType, PackagingTypeDto>().ReverseMap();
            CreateMap<AddPackagingTypeCommand, PackagingType>();
        }
    }
}
