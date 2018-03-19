using Rita.System.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Domain.Entities
{
    public class TypeEntity:BaseEntity,IStored
    {
        public string Name { get; set; }

        public string Remark { get; set; }
    }
}
