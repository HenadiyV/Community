using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Entities
{
  public  class ProfilUser
    {
        public User User{ set; get; }
        public Role Role { set; get; }
       /// public UserSeting UserSeting { set; get; }
        public Address UserAddress { set; get; }
    }
}
