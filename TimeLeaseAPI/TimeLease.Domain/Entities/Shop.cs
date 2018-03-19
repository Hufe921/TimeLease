using Rita.System.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Domain.Entities
{
    public class Shop : BaseEntity, IStored
    {
        public string Name { get; set; }
        public string Cover { get; set; }
        public int BonusPoint { get; set; }
        public string Remark { get; set; }
        public int Stock { get; set; }
        public int ShopTypeId { get; set; }
    }
}
