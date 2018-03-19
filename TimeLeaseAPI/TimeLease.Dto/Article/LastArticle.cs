using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Dto.Article
{
    public class LastArticle
    {
        public int ID { get; set; }
        public int Look { get; set; }
        public int Praise { get; set; }
        public string User { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Phone { get; set; }
        public string Cover { get; set; }
        public string CreatedOn { get; set; }
    }
}
