using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Entities
{
   public class Role
    {
        [Key]
        public int IdRole { set; get; }
        public string NameRole { set; get; }
        public Role()
        {
            Users = new List<User>();
        }
        public virtual ICollection<User> Users { get; set; }
    }
}
