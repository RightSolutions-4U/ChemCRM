using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Data.Dto
{
    public class ChemicalListDto
    {
        public List<ChemicalDto> Chemicals { get; set; }
        public int TotalCount { get; set; }
    }
}
