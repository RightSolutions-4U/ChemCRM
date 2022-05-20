using ChemWebsite.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetInquiryChemicalsQuery : IRequest<List<ChemicalSupplierCountDto>>
    {
        public Guid Id { get; set; }
    }
}
