using Rita.System.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Domain.Entities
{
    public class ArticleDetail:BaseEntity,IStored
    {
        public ArticleDetail() {
            IsUse = false;
        }
        public string Content { get; set; }

        public int UserId { get; set; }

        public bool IsUse { get; set; }
    }
}
