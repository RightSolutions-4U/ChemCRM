using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class UpdateInquiryCommand : IRequest<ServiceResponse<InquiryDto>>
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public string CityName { get; set; }
        public string Website { get; set; }
        public string CountryName { get; set; }
        public Guid? CityId { get; set; }
        public Guid? CountryId { get; set; }
        public List<InquiryChemicalDto> InquiryChemicals { get; set; } = new List<InquiryChemicalDto>();
        public Guid InquirySourceId { get; set; }
        public Guid InquiryStatusId { get; set; }
        public Guid? AssignTo { get; set; }
    }
}
