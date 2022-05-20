using System;

namespace ChemWebsite.Data
{
    public class ChemicalStatistics
    {
        public Guid Id { get; set; }
        public Guid ChemicalId { get; set; }
        public int TotalView { get; set; }
        public int TotalSearch { get; set; }
        public Chemical Chemical { get; set; }
    }
}
