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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddArticleCommandHandler : IRequestHandler<AddArticleCommand, ServiceResponse<ArticleDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddArticleCommandHandler> _logger;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;

        public AddArticleCommandHandler(IArticleRepository articleRepository,
            IMapper mapper,
            ILogger<AddArticleCommandHandler> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IWebHostEnvironment webHostEnvironment,
            PathHelper pathHelper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
            _webHostEnvironment = webHostEnvironment;
            _pathHelper = pathHelper;
        }

        public async Task<ServiceResponse<ArticleDto>> Handle(AddArticleCommand request, CancellationToken cancellationToken)
        {
            var imageName = Guid.NewGuid().ToString() + ".png";
            var article = _mapper.Map<Article>(request);
            if (request.IsImageUpload && !string.IsNullOrWhiteSpace(request.BannerImageSrc))
            {
                article.BannerUrl = imageName;
            }

            Regex reg = new Regex("[*'\",_&#^@]");
            article.ArticleUrl = reg.Replace(article.Title, "-");
            _articleRepository.Add(article);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while creating article", request);
                return ServiceResponse<ArticleDto>.ReturnFailed(500, $"Error while creating article.");
            }
            if (request.IsImageUpload && !string.IsNullOrWhiteSpace(request.BannerImageSrc))
            {
                string contentRootPath = _webHostEnvironment.WebRootPath;
                var pathToSave = Path.Combine(contentRootPath, _pathHelper.ArticleBannerImagePath, article.BannerUrl);
                await FileData.SaveFile(pathToSave, request.BannerImageSrc);
            }
            return ServiceResponse<ArticleDto>.ReturnResultWith200(_mapper.Map<ArticleDto>(article));
        }
    }
}
