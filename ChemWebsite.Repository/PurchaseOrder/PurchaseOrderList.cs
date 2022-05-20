using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public class PurchaseOrderList : List<PurchaseOrderDto>
    {
        public PurchaseOrderList()
        {

        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public PurchaseOrderList(List<PurchaseOrderDto> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<PurchaseOrderList> Create(IQueryable<PurchaseOrder> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new PurchaseOrderList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<PurchaseOrder> source)
        {
            try
            {
                return await source.AsNoTracking().CountAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<PurchaseOrderDto>> GetDtos(IQueryable<PurchaseOrder> source, int skip, int pageSize)
        {
            var entities = await source
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .Select(cs => new PurchaseOrderDto
                {
                    Id = cs.Id,
                    POCreatedDate = cs.POCreatedDate,
                    OrderNumber = cs.OrderNumber,
                    SupplierName = cs.Supplier.SupplierName,
                    SupplierId= cs.SupplierId,
                    ChemicalName = cs.Chemical.Name,
                    PackagingTypeName = cs.PackagingType.Name,
                    TotalAmount = cs.TotalAmount,
                    IsClosed = cs.IsClosed,
                    ClosedDate = cs.ClosedDate,
                    TotalQuantity = cs.TotalQuantity,
                    PricePerUnit = cs.PricePerUnit,
                    InStockQuantity= cs.InStockQuantity,
                    Tax= cs.Tax
                })
                .ToListAsync();
            return entities;
        }
    }
}
