using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class NewsletterSubscriberProfile:Profile
    {
        public NewsletterSubscriberProfile()
        {
            CreateMap<NewsletterSubscriber, NewsletterSubscriberDto>().ReverseMap();
        }
    }
}
