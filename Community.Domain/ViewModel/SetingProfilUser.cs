using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.ViewModel
{
   public class SetingProfilUser
    {
        public User User { set; get; }
        public Role Role { set; get; }
        public UserSeting UserSeting { set; get; }
        public Address UserAddress { set; get; }
    }
}
