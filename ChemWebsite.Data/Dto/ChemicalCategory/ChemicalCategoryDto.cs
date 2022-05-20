using System.Collections.Generic;

namespace ChemWebsite.Data.Dto
{
    public class ChemicalCategoryDto
    {
        public CategoryDto Category { get; set; }
        public List<ChemicalDto> Chemicals { get; set; }
        public int TotalChemicals { get; set; }
    }
}
