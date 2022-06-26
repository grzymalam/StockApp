using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Shared
{
    public class ArticleDTO
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public DateTime DatePublished { get; set; }
        public string URL { get; set; }
    }
}
