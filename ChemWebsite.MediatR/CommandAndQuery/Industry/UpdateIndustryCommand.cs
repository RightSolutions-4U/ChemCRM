using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChemWebsite.MediatR.Command
{
    public class UpdateIndustryCommand : IRequest<ServiceResponse<IndustryDto>>
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }
}
