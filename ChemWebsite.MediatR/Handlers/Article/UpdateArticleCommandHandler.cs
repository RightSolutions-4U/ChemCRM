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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, ServiceResponse<ArticleDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateArticleCommandHandler> _logger;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PathHelper _pathHelper;

        public UpdateArticleCommandHandler(IArticleRepository articleRepository,
            IMapper mapper,
            ILogger<UpdateArticleCommandHandler> logger,
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
        public async Task<ServiceResponse<ArticleDto>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _articleRepository
              .FindByInclude(c => c.Id == request.Id)
              .FirstOrDefaultAsync();
            if (entity == null)
            {
                _logger.LogError("Article Not found", request);
                return ServiceResponse<ArticleDto>.Return404("Article Not found.");
            }

            var article = _mapper.Map(request, entity);
            var imageName = Guid.NewGuid().ToString() + ".png";
            var oldImageName = article.BannerUrl;
            if (request.IsImageUpload)
            {
                if (!string.IsNullOrWhiteSpace(request.BannerImageSrc))
                {
                    article.BannerUrl = imageName;
                }
                else
                {
                    article.BannerUrl = string.Empty;
                }
            }
            Regex reg = new Regex("[*'\",_&#^@]");
            article.ArticleUrl = reg.Replace(article.Title, "-").Replace(" ", "-");
            _articleRepository.Update(article);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while updating article", request);
                return ServiceResponse<ArticleDto>.ReturnFailed(500, $"Error while updating article.");
            }

            if (request.IsImageUpload)
            {
                string contentRootPath = _webHostEnvironment.WebRootPath;
                if (!string.IsNullOrWhiteSpace(request.BannerImageSrc))
                {
                    var pathToSave = Path.Combine(contentRootPath, _pathHelper.ArticleBannerImagePath, article.BannerUrl);
                    await FileData.SaveFile(pathToSave, request.BannerImageSrc);
                }

                var pathToDelete = Path.Combine(contentRootPath, _pathHelper.ArticleBannerImagePath, oldImageName);
                if (File.Exists(pathToDelete))
                {
                    File.Delete(pathToDelete);
                }

            }

            return ServiceResponse<ArticleDto>.ReturnResultWith200(_mapper.Map<ArticleDto>(article));
        }
    }
}
