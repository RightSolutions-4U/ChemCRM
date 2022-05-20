using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChemWebsite.Data
{
    public class City : BaseEntity
    {
        public Guid Id { get; set; }
        public string CityName { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }
    }
}
