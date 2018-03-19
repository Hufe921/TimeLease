using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Dto.Article
{
    public class ArticleCommentAdd
    {
        public string Content { get; set; }
        public int ArticleId { get; set; }
        public int UserId { get; set; }
    }
}
