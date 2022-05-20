using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetTestimonialsQuery : IRequest<ServiceResponse<TestimonialsDto>>
    {
        public Guid Id { get; set; }
    }
}
