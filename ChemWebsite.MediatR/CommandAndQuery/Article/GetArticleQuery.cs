using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetArticleQuery : IRequest<ServiceResponse<ArticleDto>>
    {
        public Guid Id { get; set; }
    }
}
