using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class UpdateSupplierCommand : IRequest<ServiceResponse<SupplierDto>>
    {
        public Guid Id { get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
        public bool? IsVarified { get; set; }
        public bool? IsUnsubscribe { get; set; }
        public bool IsImageUpload { get; set; }
        public string SupplierProfile { get; set; }
        public List<SupplierAddressDto> SupplierAddresses { get; set; } = new List<SupplierAddressDto>();
        public List<SupplierEmailDto> SupplierEmails { get; set; } = new List<SupplierEmailDto>();
    }
}
