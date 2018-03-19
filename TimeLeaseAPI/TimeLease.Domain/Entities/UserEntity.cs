using Rita.System.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Domain.Entities
{
    public class UserEntity : BaseEntity, IStored
    {
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
