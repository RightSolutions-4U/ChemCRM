using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class TestimonialsProfile : Profile
    {
        public TestimonialsProfile()
        {
            CreateMap<TestimonialsDto, Testimonials>().ReverseMap();
            CreateMap<AddTestimonialsCommand, Testimonials>();
            CreateMap<UpdateTestimonialsCommand, Testimonials>();
        }
    }
}
