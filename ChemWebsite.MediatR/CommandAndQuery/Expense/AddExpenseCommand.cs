using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddExpenseCommand : IRequest<ServiceResponse<ExpenseDto>>
    {
        public Guid? Id { get; set; }
        public string Reference { get; set; }
        public Guid ExpenseCategoryId { get; set; }
        public decimal Amount { get; set; }
        public Guid? ExpenseById { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ReceiptName { get; set; }
        public string DocumentData { get; set; }
    }
}
