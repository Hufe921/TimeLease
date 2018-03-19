using Rita.System.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Domain.Entities
{
    public class ShopExchange : BaseEntity, IStored
    {
        public int ShopId { get; set; }
        public int UserId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int State { get; set; }//送货状态
    }
}
