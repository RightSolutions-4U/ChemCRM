using ChemWebsite.Data.Dto;
using MediatR;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddNewsletterSubscriberCommand : IRequest<NewsletterSubscriberDto>
    {
        public string Email { get; set; }
    }
}
