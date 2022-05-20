using ChemWebsite.Data.Dto;
using MediatR;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.Queries
{
    public class GetAllDocumentCategoryQuery : IRequest<List<DocumentCategoryDto>>
    {

    }
}
