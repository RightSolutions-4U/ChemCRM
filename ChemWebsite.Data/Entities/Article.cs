using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Data
{
    public class Article : BaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string BannerUrl { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid CategoryId { get; set; }
        public string ArticleUrl { get; set; }
        public ArticleCategory ArticleCategory { get; set; }
    }
}
