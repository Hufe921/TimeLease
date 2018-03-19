using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLease.Dto.User
{
   public  class UserRegister
    {
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
