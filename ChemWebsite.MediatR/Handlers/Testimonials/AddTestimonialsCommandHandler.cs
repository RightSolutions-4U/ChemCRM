using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddTestimonialsCommandHandler : IRequestHandler<AddTestimonialsCommand, ServiceResponse<TestimonialsDto>>
    {
        private readonly ITestimonialsRepository _testimonialsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddTestimonialsCommandHandler> _logger;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;

        public AddTestimonialsCommandHandler(ITestimonialsRepository testimonialsRepository,
            IMapper mapper,
            ILogger<AddTestimonialsCommandHandler> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IWebHostEnvironment webHostEnvironment,
            PathHelper pathHelper)
        {
            _testimonialsRepository = testimonialsRepository;
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
        }
        public async Task<ServiceResponse<TestimonialsDto>> Handle(AddTestimonialsCommand request, CancellationToken cancellationToken)
        {
            var imageName = Guid.NewGuid().ToString() + ".png";
            var testimonials = _mapper.Map<Testimonials>(request);
            if (request.IsImageUpload && !string.IsNullOrWhiteSpace(request.ImageSrc))
            {
                testimonials.Url = imageName;
            }
            _testimonialsRepository.Add(testimonials);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while creating Testimonial", request);
                return ServiceResponse<TestimonialsDto>.ReturnFailed(500, $"Error while creating Testimonial.");
            }
            if (request.IsImageUpload && !string.IsNullOrWhiteSpace(request.ImageSrc))
            {
                string contentRootPath = _webHostEnvironment.WebRootPath;
                var pathToSave = Path.Combine(contentRootPath, _pathHelper.TestimonialsImagePath, testimonials.Url);
                await FileData.SaveFile(pathToSave, request.ImageSrc);
            }
            return ServiceResponse<TestimonialsDto>.ReturnResultWith200(_mapper.Map<TestimonialsDto>(testimonials));
        }
    }
}
