using System;

namespace ChemWebsite.Data
{
    public class ExpenseCategory: BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
