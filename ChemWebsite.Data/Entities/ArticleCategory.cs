using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Data
{
    public class ArticleCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
