using System;

namespace ChemWebsite.Data.Dto
{
    public class CityDto
    {
        public Guid Id { get; set; }
        public string CityName { get; set; }
        public Guid CountryId { get; set; }
    }
}
