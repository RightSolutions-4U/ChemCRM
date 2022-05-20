using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class IncreaseMostViewedChemicalCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
