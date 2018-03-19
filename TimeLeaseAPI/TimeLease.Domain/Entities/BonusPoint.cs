using Rita.System.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Domain.Entities
{
    public class BonusPoint : BaseEntity, IStored
    {
        public int Quantity { get; set; }//积分点

        public int UserId { get; set; }

    }
}
