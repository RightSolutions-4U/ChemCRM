using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddArticleCommand : IRequest<ServiceResponse<ArticleDto>>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string BannerUrl { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsImageUpload { get; set; }
        public string BannerImageSrc { get; set; }
    }
}
