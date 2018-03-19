using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Dto.ShopExchange
{
    public class ShopExchangeAdd
    {
        public int ShopId { get; set; }
        public int UserId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int State { get; set; }//送货状态0送货中，1完成交易
    }
}
