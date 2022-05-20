using System;
using System.ComponentModel.DataAnnotations;

namespace ChemWebsite.Data.Dto
{
    public class IndustryDto
    {
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }
}
