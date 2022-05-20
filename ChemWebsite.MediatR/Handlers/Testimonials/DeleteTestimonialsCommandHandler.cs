﻿using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteTestimonialsCommandHandler : IRequestHandler<DeleteTestimonialsCommand, ServiceResponse<TestimonialsDto>>
    {
        private readonly ITestimonialsRepository _testimonialsRepository;
        private readonly ILogger<DeleteTestimonialsCommandHandler> _logger;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;

        public DeleteTestimonialsCommandHandler(ITestimonialsRepository testimonialsRepository,
            ILogger<DeleteTestimonialsCommandHandler> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow)
        {
            _testimonialsRepository = testimonialsRepository;
            _logger = logger;
            _uow = uow;
        }
        public async Task<ServiceResponse<TestimonialsDto>> Handle(DeleteTestimonialsCommand request, CancellationToken cancellationToken)
        {
            var testimonial = await _testimonialsRepository.FindAsync(request.Id);

            if (testimonial == null)
            {
                _logger.LogError("Testimonial not found.", request);
                return ServiceResponse<TestimonialsDto>.Return404("Testimonial not found");
            }

            testimonial.IsDeleted = true;
            _testimonialsRepository.Update(testimonial);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error to Deleting Testimonial");
                return ServiceResponse<TestimonialsDto>.Return500();
            }
            return ServiceResponse<TestimonialsDto>.ReturnSuccess();
        }
    }
}
