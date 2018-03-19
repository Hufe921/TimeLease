using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Dto.Article
{
    public class ArticleShow
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string City { get; set; }
        public string Remark { get; set; }
        public int BonusPoints { get; set; }//悬赏积分  
        public string Time { get; set; }//日期
        public string Type { get; set; }//类型{出租时间or爱心公益}
        public string User { get; set; }
        public string CreatedOn { get; set; }
        public int Praise { get; set; }
        public int Look { get;set; }
    }
}
