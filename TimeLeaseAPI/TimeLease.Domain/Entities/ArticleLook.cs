using Rita.System.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Domain.Entities
{
    public class ArticleLook:BaseEntity,IStored
    {
        public int UserId { get; set; }

        public int ArticleId { get; set; }
    }
}
