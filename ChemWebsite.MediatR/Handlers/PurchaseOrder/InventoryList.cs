using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class InventoryList : List<InventoryDto>
    {
        public InventoryList()
        {
        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public InventoryList(List<InventoryDto> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<InventoryList> Create(IQueryable<PurchaseOrder> source, int skip, int pageSize)
        {
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new InventoryList(dtoList.Item1, dtoList.Item2, skip, pageSize);
            return dtoPageList;
        }

        public async Task<(List<InventoryDto>, int)> GetDtos(IQueryable<PurchaseOrder> source, int skip, int pageSize)
        {
            var entities = await source
                .AsNoTracking()
                .ToListAsync();
            var count = entities
                .GroupBy(c => new
                {
                    c.ChemicalId,
                    c.Chemical.Name,
                    c.Chemical.CasNumber
                }).Count();

            var selectEntities = entities
                .GroupBy(c => new
                {
                    c.ChemicalId,
                    c.Chemical.Name,
                    c.Chemical.CasNumber
                })
                .Skip(skip)
                .Take(pageSize)
                .Select(po => new InventoryDto
                {
                    ChemicalId = po.Key.ChemicalId,
                    ChemicalName = po.Key.Name,
                    CasNo = po.Key.CasNumber,
                    Quantity = po.Sum(x => x.InStockQuantity)
                }).ToList();

            return (selectEntities,count);
        }
    }
}

