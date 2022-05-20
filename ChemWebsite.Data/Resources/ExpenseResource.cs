using ChemWebsite.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Data.Resources
{
    public class ExpenseResource : ResourceParameters
    {
        public ExpenseResource() : base("CreatedDate")
        {
        }
        public string Reference { get; set; }
        public Guid? ExpenseCategoryId { get; set; }
        public string Description { get; set; }
        public Guid? ExpenseById { get; set; }
    }
}
