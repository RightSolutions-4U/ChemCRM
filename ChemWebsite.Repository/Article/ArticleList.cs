using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public class ArticleList : List<ArticleDto>
    {
        private readonly PathHelper _pathHeper;
        public ArticleList(PathHelper pathHeper)
        {
            _pathHeper = pathHeper;
        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public ArticleList(List<ArticleDto> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<ArticleList> Create(IQueryable<Article> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new ArticleList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<Article> source)
        {
            return await source.AsNoTracking().CountAsync();
        }

        public async Task<List<ArticleDto>> GetDtos(IQueryable<Article> source, int skip, int pageSize)
        {
            var entities = await source
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .Select(c => new ArticleDto
                {
                    Id = c.Id,
                    BannerUrl = !string.IsNullOrWhiteSpace(c.BannerUrl) ? Path.Combine(_pathHeper.ArticleBannerImagePath, c.BannerUrl) : string.Empty,
                    CategoryId = c.CategoryId,
                    LongDescription = c.LongDescription,
                    PublishDate = c.PublishDate,
                    ShortDescription = c.ShortDescription,
                    Title = c.Title,
                    CreatedDate = c.CreatedDate,
                    ArticleUrl = c.ArticleUrl
                }).ToListAsync();
            return entities;
        }
    }
}
