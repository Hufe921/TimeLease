using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Dto.Apply
{
    public class ApplyAdd
    {
        public string Phone { get; set; }

        public string Remark { get; set; }

        public int UserId { get; set; }

        public int ArticleId { get; set; }

        public int ArticleUserId { get; set; }

        public int State { get; set; }//0待审，-1拒绝，1合作
    }
}
