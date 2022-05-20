using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public class SalesOrderList : List<SalesOrderListDto>
    {
        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public SalesOrderList()
        {
        }

        public SalesOrderList(List<SalesOrderListDto> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<SalesOrderList> Create(IQueryable<SalesOrder> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new SalesOrderList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<SalesOrder> source)
        {
            return await source.AsNoTracking().CountAsync();
        }

        public async Task<List<SalesOrderListDto>> GetDtos(IQueryable<SalesOrder> source, int skip, int pageSize)
        {
            var entities = await source
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .Select(c => new SalesOrderListDto
                {
                    Id = c.Id,
                    CustomerId = c.CustomerId,
                    CustomerName = c.Customer.CustomerName,
                    ChemicalId = c.ChemicalId,
                    ChemicalName = string.IsNullOrEmpty(c.Chemical.CasNumber) ? c.Chemical.CasNumber : "( " + c.Chemical.CasNumber + " )" + c.Chemical.Name,
                    SalesOrderNumber = c.SalesOrderNumber,
                    SalesOrderDate = c.SalesOrderDate,
                    Quantity = c.Quantity,
                    Tax = c.Tax,
                    Discount = c.Discount,
                    Rate = c.Rate,
                    Total = c.Total,
                    IsClosed = c.IsClosed

                }).ToListAsync();
            return entities;
        }
    }
}
