using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Entities
{
   public class Genders
    {
        public int Id { set; get; }
        public string Gender { set; get; }
        public virtual ICollection<User> Users { get; set; }
    }
}
