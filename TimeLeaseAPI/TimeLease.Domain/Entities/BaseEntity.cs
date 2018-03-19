using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rita.System.UnitOfWork;

namespace TimeLease.Domain.Entities
{
    public class BaseEntity : IStored
    {
        public BaseEntity()
        {
            CreatedOn = DateTime.Now;
            IsActive = true;
        }
        public int ID { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public byte[] Version { get; set; }
    }
}
