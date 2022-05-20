using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class ChemicalTypeProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ChemicalTypeProfile()
        {
            CreateMap<ChemicalType, ChemicalTypeDto>().ReverseMap();
            CreateMap<AddChemicalTypeCommand, ChemicalType>();
        }
    }
}
