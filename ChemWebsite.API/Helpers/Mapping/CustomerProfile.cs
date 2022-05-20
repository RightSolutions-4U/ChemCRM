using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<AddCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>();
        }
    }
}
