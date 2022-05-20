using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class UpdateChemicalCommand : IRequest<ServiceResponse<ChemicalDto>>
    {
        public Guid Id { get; set; }
        public Guid? ChemicalTypeId { get; set; }
        public string Name { get; set; }
        public string CasNumber { get; set; }
        public string HBondAcceptor { get; set; }
        public string HBondDonor { get; set; }
        public string IUPACName { get; set; }
        public string InChIKey { get; set; }
        public string MolecularFormulla { get; set; }
        public string MolecularWeight { get; set; }
        public string Synonyms { get; set; }
        public string Url { get; set; }
        public string ChemicalImage { get; set; }
        public bool IsImageUpdate { get; set; }
        public List<Guid> LstChemicalIndustries { get; set; } = new List<Guid>();

    }
}
