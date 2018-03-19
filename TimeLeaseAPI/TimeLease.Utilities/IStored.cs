using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rita.System.UnitOfWork
{
    public interface IStored
    {
        int ID { get; set; }
        bool IsActive { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime? LastUpdatedOn { get; set; }
        byte[] Version { get; set; }
    }
}
