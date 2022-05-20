using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data
{
    public class Supplier : BaseEntity
    {
        public Guid Id { get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsVarified { get; set; }
        public bool IsUnsubscribe { get; set; }
        public string SupplierProfile { get; set; }
        public string BusinessType { get; set; }
        public List<ChemicalSupplier> ChemicalSuppliers { get; set; } = new List<ChemicalSupplier>();
        public List<SupplierAddress> SupplierAddresses { get; set; } = new List<SupplierAddress>();
        public List<SupplierEmail> SupplierEmails { get; set; } = new List<SupplierEmail>();
        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }
    }
}
