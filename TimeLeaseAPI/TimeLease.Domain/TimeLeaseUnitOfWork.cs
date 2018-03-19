using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rita.System.UnitOfWork;

namespace TimeLease.Domain
{
    public class TimeLeaseUnitOfWork : UnitOfWork<TimeLeaseDbContext>
    {
        public TimeLeaseUnitOfWork(TimeLeaseDbContext context) : base(context)
        {
        }
    }
}
