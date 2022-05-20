using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class IncreseMostSearchedChemicalCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
