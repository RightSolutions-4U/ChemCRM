using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddNewsletterSubscriberCommandHandler : IRequestHandler<AddNewsletterSubscriberCommand, NewsletterSubscriberDto>
    {
        private readonly INewsletterSubscriberRepository _newsletterSubscriberRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;

        public AddNewsletterSubscriberCommandHandler(INewsletterSubscriberRepository newsletterSubscriberRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow)
        {
            _newsletterSubscriberRepository = newsletterSubscriberRepository;
            _uow = uow;
        }
        public async Task<NewsletterSubscriberDto> Handle(AddNewsletterSubscriberCommand request, CancellationToken cancellationToken)
        {
            var newsletterSubscriber = new NewsletterSubscriber
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                CreatedDate = DateTime.Now.UTCDateTime()
            };

            _newsletterSubscriberRepository.Add(newsletterSubscriber);
            await _uow.SaveAsync();
            return new NewsletterSubscriberDto { };
        }
    }
}
