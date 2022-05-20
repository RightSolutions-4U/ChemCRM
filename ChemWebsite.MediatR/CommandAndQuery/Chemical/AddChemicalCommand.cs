using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddChemicalCommand : IRequest<ServiceResponse<ChemicalDto>>
    {
        public string Name { get; set; }
        public Guid? ChemicalTypeId { get; set; }
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
        public List<string> LstChemicalIndustries { get; set; } = new List<string>();
    }
}
