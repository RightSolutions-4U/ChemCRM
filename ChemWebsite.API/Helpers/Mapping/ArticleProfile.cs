using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleCategoryDto, ArticleCategory>().ReverseMap();
            CreateMap<Article, ArticleDto>().ReverseMap();
            CreateMap<AddArticleCommand, Article>();
            CreateMap<UpdateArticleCommand, Article>();
        }
    }
}
