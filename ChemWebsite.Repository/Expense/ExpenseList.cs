using AutoMapper;
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
    public class ExpenseList : List<ExpenseDto>
    {
        IMapper _mapper;
        public ExpenseList(IMapper mapper)
        {
            _mapper = mapper;
        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public ExpenseList(List<ExpenseDto> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<ExpenseList> Create(IQueryable<Expense> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new ExpenseList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<Expense> source)
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

        public async Task<List<ExpenseDto>> GetDtos(IQueryable<Expense> source, int skip, int pageSize)
        {
            var entities = await source
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .Select(cs => new ExpenseDto
                {
                    Id = cs.Id,
                    Amount = cs.Amount,
                    Description = cs.Description,
                    ExpenseBy = _mapper.Map<UserDto>(cs.ExpenseBy),
                    ExpenseById = cs.ExpenseById,
                    ExpenseCategory = _mapper.Map<ExpenseCategoryDto>(cs.ExpenseCategory),
                    ExpenseCategoryId = cs.ExpenseCategoryId,
                    Reference = cs.Reference,
                    CreatedDate = cs.CreatedDate,
                    ExpenseDate = cs.ExpenseDate,
                    ReceiptName = cs.ReceiptName
                })
                .ToListAsync();
            return entities;
        }
    }
}
