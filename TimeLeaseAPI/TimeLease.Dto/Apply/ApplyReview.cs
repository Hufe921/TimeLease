using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Dto.Apply
{
    public class ApplyReview
    {
        public int ID { get; set; }
        public string Phone { get; set; }
        public string Remark { get; set; }
        public string User { get; set; }
        public string ArticleTitle { get; set; }
        public string Type { get; set; }
        public string CreatedOn { get; set; }
        public int State { get; set; }//0待审，-1拒绝，1合作
        public bool ArticleFinish { get; set; }
    }
}
