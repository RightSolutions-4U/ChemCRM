using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Data.Resources
{
    public class ArticleResource : ResourceParameter
    {
        public ArticleResource() : base("CreatedDate")
        {
        }

        public string Title { get; set; }
        public string ShortDescription { get; set; }
    }
}
