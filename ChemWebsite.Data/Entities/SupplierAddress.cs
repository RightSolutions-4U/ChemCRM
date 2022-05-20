using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data
{
    public class SupplierAddress
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public string Address { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? CityId { get; set; }
        public City City { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public bool IsDeleted { get; set; }
    }
}
