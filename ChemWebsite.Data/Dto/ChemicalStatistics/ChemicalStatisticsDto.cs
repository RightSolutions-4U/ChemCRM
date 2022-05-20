using System;

namespace ChemWebsite.Data.Dto
{
    public class ChemicalStatisticsDto
    {
        public Guid ChemicalId { get; set; }
        public string ChemicalName { get; set; }
        public string CasNumber { get; set; }
        public string Url { get; set; }
        public int TotalView { get; set; }
        public int TotalSearch { get; set; }
    }
}
