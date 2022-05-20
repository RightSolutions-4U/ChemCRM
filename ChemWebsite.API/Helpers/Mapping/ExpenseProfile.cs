using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<ExpenseCategory, ExpenseCategoryDto>().ReverseMap();
            CreateMap<Expense, ExpenseDto>().ReverseMap();
            CreateMap<AddExpenseCommand, Expense>();
            CreateMap<UpdateExpenseCommand, Expense>();
            CreateMap<AddExpenseCategoryCommand, ExpenseCategory>();
            CreateMap<UpdateExpenseCategoryCommand, ExpenseCategory>();
        }
    }
}
