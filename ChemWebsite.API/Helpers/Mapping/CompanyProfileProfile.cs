using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using System;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class CompanyProfileProfile : Profile
    {
        public CompanyProfileProfile()
        {
            CreateMap<CompanyProfile, CompanyProfileDto>().ReverseMap();
            CreateMap<UpdateCompanyProfileCommand, CompanyProfile>();
        }
    }
}
