using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public class ChemicalList : List<ChemicalDto>
    {
        PathHelper _pathHelper;
        public ChemicalList(PathHelper pathHelper)
        {
            _pathHelper = pathHelper;
        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public ChemicalList(List<ChemicalDto> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<ChemicalList> Create(IQueryable<Chemical> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new ChemicalList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<Chemical> source)
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

        public async Task<List<ChemicalDto>> GetDtos(IQueryable<Chemical> source, int skip, int pageSize)
        {
            var entities = await source
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .Select(cs => new ChemicalDto
                {
                    CasNumber = cs.CasNumber,
                    ChemicalDetailId = cs.ChemicalDetailId,
                    HBondAcceptor = cs.HBondAcceptor,
                    HBondDonor = cs.HBondDonor,
                    Id = cs.Id,
                    InChIKey = cs.InChIKey,
                    IUPACName = cs.IUPACName,
                    MolecularFormulla = cs.MolecularFormulla,
                    MolecularWeight = cs.MolecularWeight,
                    Name = cs.Name,
                    Synonyms = cs.Synonyms,
                    Url = string.IsNullOrWhiteSpace(cs.Url) ? _pathHelper.NoImageFound : Path.Combine(_pathHelper.ChemicalImagePath, cs.Url),
                    SupplierCount = cs.ChemicalSuppliers.Count(c => !c.Supplier.IsDeleted),
                    CustomerCount = cs.ChemicalCustomers.Count(cs => !cs.Customer.IsDeleted)
                })
                .ToListAsync();
            return entities;
        }
    }
}
