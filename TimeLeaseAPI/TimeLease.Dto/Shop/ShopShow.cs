using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Dto.Shop
{
    public class ShopShow
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Cover { get; set; }
        public int BonusPoint { get; set; }
        public string Remark { get; set; }
        public int Stock { get; set; }
    }
}
