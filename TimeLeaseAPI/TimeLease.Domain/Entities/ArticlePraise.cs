using Rita.System.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Domain.Entities
{
    public class ArticlePraise:BaseEntity,IStored
    {
        public int ArticleId { get; set; }

        public int UserId { get; set; }
    }
}
