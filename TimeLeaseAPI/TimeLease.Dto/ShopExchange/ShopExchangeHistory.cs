using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Dto.ShopExchange
{
    public class ShopExchangeHistory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Cover { get; set; }
        public string CreatedOn { get; set; }
        public int BonusPoint { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int State { get; set; }//送货状态0送货中，1完成交易
    }
}
