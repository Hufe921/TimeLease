using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Dto.Article
{
    public class ArticleAdd
    {
        public string Title { get; set; }
        public string Cover { get; set; }
        public string City { get; set; }
        public string Remark { get; set; }
        public int ArticleDetailId { get; set; }//关联文章
        public int BonusPoints { get; set; }//悬赏积分  
        public string Time { get; set; }//日期
        public bool IsFinsh { get; set; }//是否完成
        public int TagId { get; set; }//标签
        public int TypeId { get; set; }//类型{出租时间or爱心公益}
        public int UserId { get; set; }
    }
}
